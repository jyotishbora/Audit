using System;
using Newtonsoft.Json;

namespace IAS.Audit
{

    public class AuditEvent
    {
        /// <summary>
        /// Id needs to be newly generated guid
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }
        public int EntityId { get; set; }
        public string EntityName => Target?.Name;
        public string AuditKey => (EventType == AuditEventType.EntityMutation)
                                ? EntityName + EntityId
                                : UserId.ToString();
        public string ApplicationName { get; set; }
        public string UserId { get; set; }
        public string EventName { get; set; }
        public AuditEventType EventType { get; set; }
        public string AuditText { get; set; }
        public DateTime TimeStamp { get; set; }
        public AuditEntity Target { get; set; }
    }
    

    public class AuditEntity
    {
        public string Name { get; set; }
        public string FullyQualifiedName { get; set; }
        public object InitialState { get; set; }
        public object FinalState { get; set; }
    }

    public enum AuditEventType
    {
        UserAction = 1,
        EntityMutation = 2
    }


}
