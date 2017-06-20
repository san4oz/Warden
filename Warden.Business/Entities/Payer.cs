namespace Warden.Business.Entities
{
    public class Payer : Entity
    {
        public virtual string PayerId { get; set; }

        public virtual string Name { get; set; }

        public virtual string Logo { get; set; }
    }
}
