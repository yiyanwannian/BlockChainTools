using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Helpers
{
    /// <summary>
    /// Class LinqHelper.
    /// </summary>
    public static class LinqHelper
    {
        /// <summary>
        /// Determines whether the specified source is empty.
        /// </summary>
        /// <typeparam name="TSource">The type of the t source.</typeparam>
        /// <param name="source">The source.</param>
        /// <returns><c>true</c> if the specified source is empty; otherwise, <c>false</c>.</returns>
        public static bool IsEmpty<TSource>(this IEnumerable<TSource> source)
        {
            return (source is null) ? true : !source.Any();
        }

        /// <summary>
        /// Determines whether the specified source is empty.
        /// </summary>
        /// <typeparam name="TSource">The type of the t source.</typeparam>
        /// <param name="source">The source.</param>
        /// <returns><c>true</c> if the specified source is empty; otherwise, <c>false</c>.</returns>
        public static bool IsEmpty<TSource>(this TSource[] source)
        {
            return (source is null) ? true : !source.Any();
        }
    }
}
