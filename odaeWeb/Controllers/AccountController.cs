using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using odaeWeb.Helpers;
using odaeWeb.Models;
using odaeWeb.Models.DB;

namespace odaeWeb.Controllers
{

    public class AccountController : Controller
    {

        private readonly odaeDBContext _context;

        public AccountController(odaeDBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult ResetPassword(string tkn)
        {
            var user = _context.User.Where(u => u.Token == tkn).SingleOrDefault();

            if (user != null)
            {
                if (user.UserActivo && DateTime.Now < user.TokenExpiration)
                {
                    return View(new ResetPasswordViewModel { Token = tkn } );
                }
            }
            return NotFound();
        }


        public IActionResult Codificador()
        {
            var identity = (ClaimsIdentity)HttpContext.User.Identity;
            var codVM = new CodificadorViewModel();
            string userId = identity.GetSpecificClaim(ClaimTypes.Sid);
            var userfasecod = _context.UserFaseCodificador.FirstOrDefault(u => u.UserId == userId);
            var codificador = _context.Codificador.FirstOrDefault(c => c.CodificadorId == userfasecod.CodificadorId);
            codVM.UserId = userId;
            codVM.NombreCodificador = codificador.NombreCodificador;
            codVM.Email = codificador.Email;
            return View(codVM);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            var user = _context.User.FirstOrDefault(u => u.UserId == loginModel.Username && u.Password == Cripto.HashPassword(loginModel.Password));

            if (user != null)
            {
                if (user.UserActivo)
                {
                    string redirect = "/Home";
                    string userName = "";
                    string codificadorId = "";
                    string faseId = "";
                    string email = "";

                    if (user.ProfileId == 2)
                    {
                        var userfasecod = _context.UserFaseCodificador.FirstOrDefault(u => u.UserId == user.UserId);
                        var codificador = _context.Codificador.FirstOrDefault(c => c.CodificadorId == userfasecod.CodificadorId);
                        userName = codificador.NombreCodificador;
                        codificadorId = userfasecod.CodificadorId.ToString();
                        faseId = userfasecod.FaseId.ToString();
                        email = codificador.Email;
                        redirect = "/Codificacion";
                    }
                    else if (user.ProfileId == 0)
                    {
                        userName = "Administrador";
                        redirect = "/Admin";
                    }
                    var claims = new List<Claim> {
                        new Claim(ClaimTypes.Sid, user.UserId),
                        new Claim(ClaimTypes.Role, user.ProfileId.ToString()),
                        new Claim(ClaimTypes.UserData, codificadorId),
                        new Claim(ClaimTypes.Version, faseId),
                        new Claim(ClaimTypes.Name, userName),
                        new Claim(ClaimTypes.Email, email) };
                    var userIdentity = new ClaimsIdentity(claims, "login");
                    ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                    await HttpContext.SignInAsync(principal);

                    return Redirect(redirect);
                }
                else
                {
                    TempData["Message"] = "Usuario desactivado.";
                }
            }
            else TempData["Message"] = "Usuario o contraseña no válidos.";
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            User usuario = new User
            {
                UserId = model.UserId,
                Password = Cripto.HashPassword(model.Password),
                ProfileId = 2,
                UserActivo = true
            };

            if (ModelState.IsValid)
            {
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return Redirect(returnUrl);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Codificador([Bind("UserId,NombreCodificador,Email")] CodificadorViewModel codVM)
        {
            var identity = (ClaimsIdentity)HttpContext.User.Identity;
            string userId = identity.GetSpecificClaim(ClaimTypes.Sid);
            var userfasecod = _context.UserFaseCodificador.FirstOrDefault(u => u.UserId == userId);
            var codificador = _context.Codificador.FirstOrDefault(c => c.CodificadorId == userfasecod.CodificadorId);
            codificador.NombreCodificador = codVM.NombreCodificador;
            codificador.Email = codVM.Email;

            if (ModelState.IsValid)
            {
                _context.Update(codificador);
                _context.Entry(codificador).State = EntityState.Modified;
                _context.Entry(codificador).Property(o => o.NombreCodificador).IsModified = true;
                _context.Entry(codificador).Property(o => o.Email).IsModified = true;
                await _context.SaveChangesAsync();
                TempData["Message"] = "Información actualizada correctamente.";
                return RedirectToAction("Codificador", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(codVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(string Password, string ConfirmPassword, string Token)
        {
            var usuario = new User();
            usuario = _context.User.Where(u => u.Token == Token).SingleOrDefault();
            usuario.Password = Cripto.HashPassword(Password);
            usuario.Token = null;
            usuario.TokenExpiration = null;

            if (ModelState.IsValid)
            {
                _context.Update(usuario);
                await _context.SaveChangesAsync();
                TempData["Message"] = "La contraseña fue cambiada correctamente.";
                return RedirectToAction("Login", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(new ResetPasswordViewModel { Token = Token });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(string OldPassword, string NewPassword)
        {
            var identity = (ClaimsIdentity)HttpContext.User.Identity;
            string userId = identity.GetSpecificClaim(ClaimTypes.Sid);
            if (userId == null)
            {
                return NotFound();
            }

            var usuario = _context.User.FirstOrDefault(u => u.UserId == userId && u.Password == Cripto.HashPassword(OldPassword));

            if (usuario != null && await PasswordChange(usuario, NewPassword))
            {
                TempData["Message"] = "La contraseña fue cambiada correctamente.";
            }
            else
            {
                TempData["Message"] = "Error en los datos. No se pudo cambiar la contraseña.";
            }

            return RedirectToAction("Codificador", "Account");
        }

        private async Task<bool> PasswordChange(User usuario, string NewPassword)
        {
            try
            {
                usuario.Password = Cripto.HashPassword(NewPassword);
                usuario.Token = null;
                usuario.TokenExpiration = null;
                _context.Update(usuario);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception) { }
            return false;
        }

        private User LoginUser(string username, string password)
        {
            //As an example. This method would go to our data store and validate that the combination is correct. 
            //For now just return true. 

            var usuario = _context.User.FirstOrDefault(u => u.UserId == username && u.Password == Cripto.HashPassword(password));

            return usuario;
        }
    }
}