using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class VisitEntity
    {
        public Guid Id { get; private set; }
        public Guid PersonId { get; private set; }
        public DateTime EntryTime { get; private set; }
        public DateTime? ExitTime { get; private set; }
        public PersonEntity? Person { get; private set; }
        public bool Active => ExitTime == null;
        public TimeSpan? Duration => ExitTime.HasValue ? ExitTime.Value - EntryTime : null;

        public VisitEntity(Guid personId, DateTime? timeEntry=null)
        {
            ArgumentNullException.ThrowIfNull(personId, nameof(personId));

            Id = Guid.NewGuid();
            PersonId = personId;
            EntryTime = timeEntry ?? DateTime.UtcNow;
            ExitTime = null;
        }

        public void RegisterExit(DateTime? timeExit = null)
        {
            if (ExitTime != null)
                throw new InvalidOperationException("Exit time already registered.");
            ExitTime = timeExit ?? DateTime.UtcNow;
        }
    }
}
