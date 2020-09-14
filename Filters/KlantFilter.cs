using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WellStralerWebshop.Models.Domain;

namespace WellStralerWebshop.Filters
{
    [AttributeUsageAttribute(AttributeTargets.All, AllowMultiple = false)]
    public class KlantFilter: ActionFilterAttribute
    {
        private readonly IKlantLoginRepository _klantLoginRepository;

        public KlantFilter(IKlantLoginRepository klantLoginRepo)
        {
            _klantLoginRepository = klantLoginRepo;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            context.ActionArguments["klantLogin"] = context.HttpContext.User.Identity.IsAuthenticated ? _klantLoginRepository.getLoginByLoginID(Convert.ToInt64(context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value)) : null;
            base.OnActionExecuting(context);
        }
    }
}
