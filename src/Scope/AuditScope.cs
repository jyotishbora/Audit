using System;
using System.Threading.Tasks;
using Force.DeepCloner;
using IAS.Audit.Abstractions;

namespace IAS.Audit.Scope
{
    public class AuditScope : IDisposable

    #if NETSTANDARD2_1
        , IAsyncDisposable
    #endif
    {

        private AuditEvent _auditEvent;
        private readonly IAuditManager _manager;
        private readonly AuditScopeOptions _scopeOptions;

        public AuditScope(AuditScopeOptions options, IAuditManager manager)
        {

            if (options.TargetEntity == null)
            {
                throw new ArgumentException("Target Object can not be null");
            }

            _scopeOptions = options;
            _manager = manager;
        }

        internal Task StartScope()
        {
            var auditEntity =  new AuditEntity
            {
                Name = _scopeOptions.TargetEntity.GetType().Name,
                FullyQualifiedName = _scopeOptions.TargetEntity.GetType().FullName,
                InitialState = _scopeOptions.TargetEntity.DeepClone()
            };


            _auditEvent = new AuditEvent()
            {
                EventName = _scopeOptions.EventName,
                TimeStamp = DateTime.Now,
                UserId = _scopeOptions.UserId.ToString(),
                Target = auditEntity,
                EntityId = _scopeOptions.EntityId,
                EventType = AuditEventType.EntityMutation
            };

            return Task.CompletedTask;
        }

        private async Task EndAsync()
        {
            _auditEvent.Target.FinalState = _scopeOptions.TargetEntity.DeepClone();
            await _manager.SaveAuditEvent(_auditEvent);
        }

        private void End()
        {
            _auditEvent.Target.FinalState = _scopeOptions.TargetEntity.DeepClone();
             _manager.SaveAuditEvent(_auditEvent);
        }

        public ValueTask DisposeAsync()
        {
            return new ValueTask(EndAsync());
        }

        public void Dispose()
        {
            End();
        }
    }
}
