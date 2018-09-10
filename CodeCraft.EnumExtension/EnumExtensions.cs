using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace CodeCraft.EnumExtension
{
    public static class EnumValueExtensions
    {
        public static TResult SpecificAttribute<E, TResult>(this E source)
            where E : struct, IComparable, IConvertible, IFormattable
            where TResult : Attribute
        {
            var fieldInfo = source.GetType().GetField(source.ToString());
            return fieldInfo.GetCustomAttributes<TResult>(false).FirstOrDefault();
        }

        public static string DescriptionAttribute<E>(this E source)
            where E : struct, IComparable, IConvertible, IFormattable
        {
            var descriptionAttribute = SpecificAttribute<E, DescriptionAttribute>(source);
            return descriptionAttribute?.Description ?? source.ToString();
        }
    }

    public class Enum<E>
        where E : struct, IComparable, IConvertible, IFormattable
    {
        /// <summary>
        /// Test if <typeparamref name="E"/> is a Enum.
        /// </summary>
        private static bool IsEnum => typeof(E).IsEnum;
        /// <summary>
        /// Exception message if E is not Enum struct.
        /// </summary>
        private static readonly string IsNotEnumMessage = "E must be an enumerated type";

        /// <summary>
        /// Retrieves an enumerable of the values of the constants in a specified enumeration <typeparamref name="E"/> 
        /// It's equivalent to <code> Enum.GetValues(typeof(E))</code>
        /// </summary>
        /// <exception cref="ArgumentException"> Thrown if <typeparamref name="E"/> is not an Enum</exception>
        /// <returns> An enumerable of the values of the constants <typeparamref name="E"/> </returns>
        public static IEnumerable<E> GetValues()
            => IsEnum ? GetValuesSafe() : throw new ArgumentException(IsNotEnumMessage);

        /// <summary>
        /// Retrieves an enumerable of the values of the constants in a specified enumeration <typeparamref name="E"/> 
        /// </summary>
        /// <returns> An enumerable of the values of the constants <typeparamref name="E"/> </returns>
        private static IEnumerable<E> GetValuesSafe()
            => Enum.GetValues(typeof(E)).Cast<E>();

        /// <summary>
        /// Retrieve <see cref="DescriptionAttribute"/> for each enum value of <typeparamref name="E"/> Enum.
        /// </summary>
        /// <exception cref="ArgumentException"> Thrown if <typeparamref name="E"/> is not an Enum</exception>
        /// <returns>An enumerable that contains descriptions of each enum value</returns>
        public static IEnumerable<string> GetDescriptions()
            => IsEnum ? GetDescriptionsSafe() : throw new ArgumentException(IsNotEnumMessage);

        /// <summary>
        /// Retrieve <see cref="DescriptionAttribute"/> for each enum value of <typeparamref name="E"/>  Enum.
        /// </summary>
        /// <returns>An enumerable that contains descriptions of each enum value</returns>
        private static IEnumerable<string> GetDescriptionsSafe()
            => GetValuesSafe().Select(e => e.DescriptionAttribute());

        /// <summary>
        /// Retrieve specific <typeparamref name="TResult"/> attribute for each enum value of <typeparamref name="E"/>  Enum.
        /// </summary>
        /// <exception cref="ArgumentException"> Thrown if <typeparamref name="E"/> is not an Enum</exception>
        /// <returns>An enumerable that contains descriptions of each enum value</returns>
        public static IEnumerable<TResult> GetAttributes<TResult>()
            where TResult : Attribute
            => IsEnum ? GetAttributesSafe<TResult>() : throw new ArgumentException(IsNotEnumMessage);

        /// <summary>
        /// Retrieve specific <typeparamref name="TResult"/> attribute for each enum value of <typeparamref name="E"/>  Enum.
        /// </summary>
        /// <returns>An enumerable that contains <typeparamref name="TResult"/> attribute of each enum value</returns>
        private static IEnumerable<TResult> GetAttributesSafe<TResult>()
        where TResult : Attribute
            => GetValuesSafe().Select(e => e.SpecificAttribute<E, TResult>());

        /// <summary>
        /// Retieve an enumarable of key value pair, where key is <typeparamref name="E"/> enum, and value the Description string of <see cref="DescriptionAttribute"/>
        /// </summary>
        /// <exception cref="ArgumentException"> Thrown if <typeparamref name="E"/> is not an Enum</exception>
        /// <returns>An enumerable of key value pair</returns>
        public static IEnumerable<KeyValuePair<E, string>> GetEnumDescriptionPairs()
            => IsEnum ? GetEnumDescriptionPairsSafe() : throw new ArgumentException(IsNotEnumMessage);

        /// <summary>
        /// Retieve an enumarable of key value pair, where key is <typeparamref name="E"/> enum, and value the Description string of <see cref="DescriptionAttribute"/>
        /// </summary>
        /// <returns>An enumerable of key value pair</returns>
        private static IEnumerable<KeyValuePair<E, string>> GetEnumDescriptionPairsSafe()
            => GetValuesSafe().Select(e => new KeyValuePair<E, string>(e, e.DescriptionAttribute()));

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
             => IsEnum ? GetEnumAttributePairsSafe<TResult>() : throw new ArgumentException(IsNotEnumMessage);

        /// <summary>
        ///  Retieve an enumarable of <see cref="KeyValuePair{ <typeparamref name="E"/>, <typeparamref name="TResult"/>}"/> , where key is <typeparamref name="E"/> enum, and value <typeparamref name="TResult"/> instance.
        ///  <typeparamref name="TResult"/>  must be derive from <see cref="Attribute"/>
        /// </summary>
        /// <typeparam name="TResult">Attribute Type</typeparam>
        /// <returns>An enumerable of key value pair</returns>
        private static IEnumerable<KeyValuePair<E, TResult>> GetEnumAttributePairsSafe<TResult>()
            where TResult : Attribute
            => GetValuesSafe().Select(e => new KeyValuePair<E, TResult>(e, e.SpecificAttribute<E, TResult>()));
    }
}

