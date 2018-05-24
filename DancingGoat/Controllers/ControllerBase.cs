using System;
using System.Globalization;
using System.Web.Mvc;

using DancingGoat.Helpers;
using DancingGoat.Infrastructure;
using DancingGoat.Localization;

using KenticoCloud.Delivery;

namespace DancingGoat.Controllers
{
    [SelfConfigActionFilter]
    public class ControllerBase : AsyncController
    {
        protected readonly DeliveryClient baseClient;
        public readonly IDeliveryClient client;

        public ControllerBase(IProjectContext projectContext)
        {
            baseClient = DeliveryClientFactory.CreateDeliveryClient(projectContext);
            var currentCulture = CultureInfo.CurrentUICulture.Name;
            if (currentCulture.Equals(LanguageClient.DEFAULT_LANGUAGE, StringComparison.InvariantCultureIgnoreCase))
            {
                client = baseClient;
            }
            else
            {
                client = new LanguageClient(baseClient, currentCulture);
            }
        }
    }
}

