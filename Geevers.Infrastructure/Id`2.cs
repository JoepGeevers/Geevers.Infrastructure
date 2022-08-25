namespace Geevers.Infrastructure
{
    using System;
    using System.Collections.Generic;

    public class Id<TEntity, TKey>
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
                .Equals(value, default) //stackoverflow.com/a/864860 ~ "Wow, how delightfully obscure! This is definitely the way to go though, kudos. – Nick Farina"
            ||
                value as string == "";
    }
}