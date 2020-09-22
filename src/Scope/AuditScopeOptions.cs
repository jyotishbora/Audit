namespace IAS.Audit.Scope
{
    public class AuditScopeOptions
    {
        public int EntityId { get; set; } // Unique id identifying the Audit Entity
        public object TargetEntity { get; set; }
        public string EventName { get; set; }
        public int UserId { get; set; }
     
    }
}
