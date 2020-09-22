using System;
using IAS.Audit.Abstractions;
using IAS.Audit.Scope;


namespace IAS.Audit.Extensions
{
    public static class AuditManagerScopeExtension
    {
        public static AuditScope CreateScope(this IAuditManager manager, Action<AuditScopeOptions> optionsAction)
        {
            var options = new AuditScopeOptions();
            optionsAction(options);
            var auditScope = new AuditScope(options, manager);
            auditScope.StartScope();
            return auditScope;
        }
    }
}
