using System;
using System.Collections.Generic;
using JacksonVeroneze.StockService.Core.Messages;

namespace JacksonVeroneze.StockService.Core.DomainObjects
{
    public class Entity : EntityId
    {
        public DateTime CreatedAt { get; private set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; private set; } = null;

        public DateTime? DeletedAt { get; private set; } = null;

        public int Version { get; private set; } = 1;

        private readonly List<Event> _notifications = new List<Event>();

        public IReadOnlyCollection<Event> Notifications => _notifications?.AsReadOnly();

        protected Entity()
        {
        }

        public void AddEvent(Event evento)
            => _notifications.Add(evento);

        public void RemoveEvent(Event evento)
            => _notifications.Remove(evento);

        public void ClearEvents()
            => _notifications.Clear();

        public void SetDeletedAt() => DeletedAt = DateTime.Now;

        public override string ToString()
            => $"{GetType().Name}: Id: {Id}, CreatedAt: {CreatedAt}, " +
               $"UpdatedAt: {UpdatedAt}, DeletedAt: {DeletedAt}, Version: {Version}";
    }
}
