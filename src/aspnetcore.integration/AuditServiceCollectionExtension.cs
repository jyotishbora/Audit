using System;
using IAS.Audit.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace IAS.Audit.aspnetcore.integration
{
    public static class AuditServiceCollectionExtension
    {
        public static IServiceCollection AddIasAudit(this IServiceCollection collection, Action<AuditManagerBuilder> builderAction)
        {
            var builder = new AuditManagerBuilder();
            
            builderAction.Invoke(builder);

            collection.AddScoped<IAuditManager>(_ => builder.Build());
            return collection;
        }
    }
}
