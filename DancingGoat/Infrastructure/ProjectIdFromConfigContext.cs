using System;
using DancingGoat.Areas.Admin;

namespace DancingGoat.Infrastructure
{
    public class ProjectIdFromConfigContext : IProjectContext
    {
        public Guid? GetProjectId()
        {
            return AppSettingProvider.ProjectId;
        }
    }
}