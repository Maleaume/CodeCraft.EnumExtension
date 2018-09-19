using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace CodeCraft.EnumExtension
{
    
    public static class EnumExtensions
    {
        public static TResult SpecificAttribute<TResult>(this Enum source)
                 where TResult : Attribute
        {
            var fieldInfo = source.GetType().GetField(source.ToString());
            return fieldInfo.GetCustomAttributes<TResult>(false).FirstOrDefault();
        }

        public static string DescriptionAttribute(this Enum source)
        {
            var descriptionAttribute = SpecificAttribute<DescriptionAttribute>(source);
            return descriptionAttribute?.Description ?? source.ToString();
        }
    }

    public static class Enum<E>
        where E : Enum
    {
        /// <summary>
        /// Retrieves an enumerable of the values of the constants in a specified enumeration <typeparamref name="E"/> 
        /// It's equivalent to <code> Enum.GetValues(typeof(E))</code>
        /// </summary>
        /// <exception cref="ArgumentException"> Thrown if <typeparamref name="E"/> is not an Enum</exception>
        /// <returns> An enumerable of the values of the constants <typeparamref name="E"/> </returns>
        public static IEnumerable<E> GetValues()
            =>  Enum.GetValues(typeof(E)).Cast<E>();

        /// <summary>
        /// Retrieve <see cref="DescriptionAttribute"/> for each enum value of <typeparamref name="E"/> Enum.
        /// </summary>
        /// <exception cref="ArgumentException"> Thrown if <typeparamref name="E"/> is not an Enum</exception>
        /// <returns>An enumerable that contains descriptions of each enum value</returns>
        public static IEnumerable<string> GetDescriptions()
            =>GetValues().Select(e => e.DescriptionAttribute());

        /// <summary>
        /// Retrieve specific <typeparamref name="TResult"/> attribute for each enum value of <typeparamref name="E"/>  Enum.
        /// </summary>
        /// <exception cref="ArgumentException"> Thrown if <typeparamref name="E"/> is not an Enum</exception>
        /// <returns>An enumerable that contains descriptions of each enum value</returns>
        public static IEnumerable<TResult> GetAttributes<TResult>()
            where TResult : Attribute
            => GetValues().Select(e => e.SpecificAttribute<TResult>());

        /// <summary>
        /// Retieve an enumarable of key value pair, where key is <typeparamref name="E"/> enum, and value the Description string of <see cref="DescriptionAttribute"/>
        /// </summary>
        /// <exception cref="ArgumentException"> Thrown if <typeparamref name="E"/> is not an Enum</exception>
        /// <returns>An enumerable of key value pair</returns>
        public static IEnumerable<KeyValuePair<E, string>> GetEnumDescriptionPairs()
            => GetValues().Select(e => new KeyValuePair<E, string>(e, e.DescriptionAttribute()));

        /// <summary>
        ///  Retieve an enumarable of <see cref="KeyValuePair{TKey, TValue}"/> , where key is <typeparamref name="E"/> enum, and value <typeparamref name="TResult"/> instance.
        ///  <typeparamref name="TResult"/>  must be derive from <see cref="Attribute"/>
        /// </summary>
        /// <typeparam name="TResult">Attribute Type</typeparam>
        /// <exception cref="ArgumentException"> Thrown if <typeparamref name="E"/> is not an Enum</exception>
        /// <returns>An enumerable of key value pair</returns>
        /// <returns>An enumerable of key value pair</returns>
        public static IEnumerable<KeyValuePair<E, TResult>> GetEnumAttributePairs<TResult>()
                where TResult : Attribute
             =>  GetValues().Select(e => new KeyValuePair<E, TResult>(e, e.SpecificAttribute<TResult>()));
    }
}

