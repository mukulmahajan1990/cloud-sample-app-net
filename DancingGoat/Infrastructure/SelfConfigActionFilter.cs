using System;
using System.Web.Mvc;

using DancingGoat.Areas.Admin.Models;

namespace DancingGoat.Infrastructure
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class SelfConfigActionFilter : ActionFilterAttribute
    {
        private readonly IProjectContext _projectContext;

        public SelfConfigActionFilter() : this(DependencyResolver.Current.GetService<IProjectContext>())
        {
        }

        public SelfConfigActionFilter(IProjectContext projectContext)
        {
            _projectContext = projectContext;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            DateTime? subscriptionExpiresAt = Areas.Admin.AppSettingProvider.SubscriptionExpiresAt;
            Guid? projectId = _projectContext.GetProjectId();

            if (!projectId.HasValue)
            {
                filterContext.Result = Helpers.RedirectHelpers.GetSelfConfigIndexResult(null);
            }
            else if (subscriptionExpiresAt.HasValue && subscriptionExpiresAt <= DateTime.Now)
            {
                filterContext.Result = Helpers.RedirectHelpers.GetSelfConfigIndexResult(new MessageModel(){ Message = "Current subscription is expired.", MessageType = MessageType.Error});
            }
        }
    }
}