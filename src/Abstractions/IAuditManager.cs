using System.Threading.Tasks;
using IAS.Audit;

namespace IAS.Audit.Abstractions
{
    public interface IAuditManager
    {
        Task SaveAuditEvent(AuditEvent auditEvent);
    }
}
