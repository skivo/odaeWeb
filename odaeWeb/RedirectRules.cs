using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Rewrite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace odaeWeb
{
    public class RedirectRules : IRule
    {
        public void ApplyRule(RewriteContext context)
        {
            var request = context.HttpContext.Request;

            if (request.Path.Value.Contains("/.well-known"))
            {
                context.Result = RuleResult.ContinueRules;
                return;
            }

            if (string.Equals(request.Scheme, "http", StringComparison.OrdinalIgnoreCase))
            {
                string path = "https://" + request.Host.Value + request.PathBase + request.Path + request.QueryString;
                context.HttpContext.Response.Redirect(path, true);
                context.Result = RuleResult.EndResponse;
            }
        }
    }
}
