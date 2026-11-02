using System;
using System.Collections.Generic;
using System.Text;

namespace HonyStore.Domain.Shared
{
    public abstract class Entity<TId> where TId : IEntityId<TId>
    {
        protected Entity(TId id)
        {
            Id = id;
        }
        public TId Id { get; private set; }
    }
}
