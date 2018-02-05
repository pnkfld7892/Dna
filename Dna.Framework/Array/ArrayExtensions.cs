using System.Collections.Generic;

namespace Dna
{
    /// <summary>
    /// Extension methods for arrays
    /// </summary>
    public static class ArrayExtensions
    {
        /// <summary>
        /// Append the given objects to the original source array
        /// </summary>
        /// <typeparam name="T">The type of array</typeparam>
        /// <param name="source">The original array of values</param>
        /// <param name="toAdd">the values to append to the source</param>
        /// <returns></returns>
        public static T[] Append<T>(this T[] source, params T[] toAdd)
        {
            //convert source to a list
            var list = new List<T>(source);
            
            //append to source
            list.AddRange(toAdd);

            //return appended source as an array
            return list.ToArray();
        }

        /// <summary>
        /// Prepend the given objects to the original source array
        /// </summary>
        /// <typeparam name="T">The type of array</typeparam>
        /// <param name="source">The original array of values</param>
        /// <param name="toAdd">the values to prepended to the source</param>
        /// <returns></returns>
        public static T[] Prepend<T>(this T[] source, params T[] toAdd)
        {
            //convert toAdd to a list
            var list = new List<T>(toAdd);

            //prepend the source
            list.AddRange(source);

            //return prepended list as an array
            return list.ToArray();
        }
    }
}
