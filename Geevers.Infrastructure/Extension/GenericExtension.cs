namespace Geevers.Infrastructure.Extension
{
    using System;

    public static class GenericExtension
    {
        public static TResult Map<TSource, TResult>(this TSource source, Func<TSource, TResult> selector)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector == null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return selector(source);
        }
    }
}