namespace Geevers.Infrastructure
{
    using System;

    public class Guid<TEntity> : Id<TEntity, Guid>
    {
        public Guid(Guid value) : base(value)
        {
        }

        public static implicit operator Guid<TEntity>(Guid value) => new Guid<TEntity>(value);
    }
}