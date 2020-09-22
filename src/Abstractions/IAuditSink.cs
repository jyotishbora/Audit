using System.Threading.Tasks;
using IAS.Audit;

namespace IAS.Audit.Abstractions
{
    public interface IAuditSink
    {
        Task Dispatch(AuditEvent auditEvent);
    }
}
