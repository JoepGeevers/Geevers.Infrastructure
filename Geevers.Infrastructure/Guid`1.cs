﻿namespace Geevers.Infrastructure
{
    using System;

    [Serializable]
    public class Guid<TEntity> : Id<TEntity, Guid>
    {
        public Guid(Guid value) : base(value)
        {
        }

        public static Guid<TEntity> New()
        {
            return Guid.NewGuid();
        }

        public static implicit operator Guid<TEntity>(Guid value) => new Guid<TEntity>(value);
    }
}