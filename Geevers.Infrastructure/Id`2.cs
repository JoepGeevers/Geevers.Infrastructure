namespace Geevers.Infrastructure
{
    using System;
    using System.Collections;
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

        private bool IsNullDefaultOrEmpty(TKey value)
        {
            return this.IsNullOrDefault(value)
                || this.IsEmpty(value);
        }

        private bool IsEmpty(TKey value)
        {
            var enumerable = value as IEnumerable;

            if (enumerable == null)
            {
                return false;
            }

            if (enumerable.GetEnumerator().MoveNext())
            {
                return false;
            }

            return true;
        }

        private bool IsNullOrDefault(TKey value)
        {
            // https://stackoverflow.com/a/864860 ~ "Wow, how delightfully obscure!"
            return EqualityComparer<TKey>.Default.Equals(value, default);
        }

        public bool Equals(Id<TEntity, TKey> other)
        {
            if (EqualityComparer<Id<TEntity, TKey>>.Default.Equals(other, default(Id<TEntity, TKey>)))
            {
                return false;
            }

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

        public static bool operator ==(Id<TEntity, TKey> p, Id<TEntity, TKey> q)
        {
            if (p is null && q is null)
            {
                return true;
            }

            return p?.Equals(q) ?? false;
        }

        public override string ToString()
        {
            return this.Value.ToString();
        }

        public static bool operator !=(Id<TEntity, TKey> p, Id<TEntity, TKey> q) => !(p == q);

        public static implicit operator Id<TEntity, TKey>(TKey value) => new Id<TEntity, TKey>(value);
    }
}