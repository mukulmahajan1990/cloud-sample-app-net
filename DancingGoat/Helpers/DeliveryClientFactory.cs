using DancingGoat.Areas.Admin;
using DancingGoat.Infrastructure;
using DancingGoat.Models;

using KenticoCloud.Delivery;

namespace DancingGoat.Helpers
{
    public class DeliveryClientFactory
    {
        public static DeliveryClient CreateDeliveryClient(IProjectContext projectContext)
        {
            // Use the provider to get environment variables.
            var provider = new ConfigurationManagerProvider();

            // Build DeliveryOptions with default or explicit values.
            var options = provider.GetDeliveryOptions();

            var contextProjectId = projectContext.GetProjectId();
            options.ProjectId = contextProjectId == null ?
                AppSettingProvider.DefaultProjectId.ToString() : contextProjectId.Value.ToString();

            var clientInstance = new DeliveryClient(options);
            clientInstance.CodeFirstModelProvider.TypeProvider = new CustomTypeProvider();
            clientInstance.ContentLinkUrlResolver = new CustomContentLinkUrlResolver();
            return clientInstance;
        }
    }
}