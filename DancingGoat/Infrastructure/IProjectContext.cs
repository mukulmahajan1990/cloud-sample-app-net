using System;

namespace DancingGoat.Infrastructure
{
    public interface IProjectContext
    {
        Guid? GetProjectId();
    }
}