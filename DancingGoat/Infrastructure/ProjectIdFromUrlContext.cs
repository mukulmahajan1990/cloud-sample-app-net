using System;
using System.Web;
using DancingGoat.Utils;

namespace DancingGoat.Infrastructure
{
    public class ProjectIdFromUrlContext : IProjectContext
    {
        public Guid? GetProjectId()
        {
            var requestUrl = HttpContext.Current.Request.Url;
            var subdomain = requestUrl.GetSubdomain();
            var decodedGuid = GuidUtils.FromShortString(subdomain);
            return decodedGuid;
        }
    }
}