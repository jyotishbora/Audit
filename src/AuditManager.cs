using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IAS.Audit.Abstractions;

namespace IAS.Audit
{
    public class AuditManager : IAuditManager
    {
        private readonly IEnumerable<IAuditSink> _sinks;

        public AuditManager(IEnumerable<IAuditSink> sinks)
        {
            _sinks = sinks;
        }

        public async Task SaveAuditEvent(AuditEvent auditEvent)
        {
            foreach (var s in _sinks)
            {
                await s.Dispatch(auditEvent);
            }
        }

    }
}
