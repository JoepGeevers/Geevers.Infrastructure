namespace Geevers.Infrastructure
{
    using System;
    using System.Collections.Generic;

    public class Id<TEntity, TKey> : IEquatable<Id<TEntity, TKey>>
    {
        public readonly TKey Value;

        public Id(TKey value)
        {
            if (this.IsNullDefaultOrEmpty(value))
            {
                throw new ArgumentException("Id<T> cannot be null, default or empty");
            }

            this.Value = value;
        }

        private bool IsNullDefaultOrEmpty(TKey value) =>
            EqualityComparer<TKey>
                .Default
                .Equals(value, default) // https://stackoverflow.com/a/864860 ~ "Wow, how delightfully obscure! This is definitely the way to go though, kudos. – Nick Farina"
            ||
                value as string == "";

        public bool Equals(Id<TEntity, TKey> other)
        {
            return this.Value.Equals(other.Value);
        }

        public override bool Equals(object other)
        {
            if (false == other is Id<TEntity, TKey>)
            {
                return false;
            }

            return this.Equals((Id<TEntity, TKey>)other);
        }

        public override int GetHashCode()
        {
            return -1937169414 + this.Value.GetHashCode();
        }

        public static bool operator ==(Id<TEntity, TKey> p, Id<TEntity, TKey> q) => p.Equals(q);
        public static bool operator !=(Id<TEntity, TKey> p, Id<TEntity, TKey> q) => !(p == q);
    }
}