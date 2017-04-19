using System;

namespace Warden.Business.Entities
{
    public class Entity
    {
        public virtual Guid Id { get; set; }

        public Entity()
        {
            Id = Guid.NewGuid();
        }
    }
}
