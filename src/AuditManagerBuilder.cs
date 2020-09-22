using System.Collections.Generic;
using IAS.Audit.Abstractions;

namespace IAS.Audit
{
    public class AuditManagerBuilder
    {
        public IList<IAuditSink> Sinks { get; } = new List<IAuditSink>();

        public AuditManagerBuilder AddSink(IAuditSink sink)
        {
            Sinks.Add(sink);
            return this;
        }

        public IAuditManager Build()
        {
            return new AuditManager(Sinks);
        }

    }
}
