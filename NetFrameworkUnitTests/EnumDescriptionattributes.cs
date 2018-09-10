using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
namespace CodeCraft.EnumExtension.NetFrameworkUnitTests
{
    [TestClass]
    public class EnumDescriptionattributes
    {
        public class MyDescriptionAttribute : Attribute
        {
            public int Numerical { get; }
            public string Literal { get; }

            public MyDescriptionAttribute(int order, string literal)
            {
                Numerical = order;
                Literal = literal;
            }
        }

        public enum ETestEnum
        {
            [MyDescription(1, "One")]
            [System.ComponentModel.Description("First enum")]
            First,
            [MyDescription(2, "Two")]
            [System.ComponentModel.Description("Second enum")]
            Second,
            [MyDescription(3, "Three")]
            [System.ComponentModel.Description("Third enum")]
            Third
        }

        [TestMethod]
        public void RetrieveAllDescriptionsAttributes()
        {
            var allDescriptions = Enum<ETestEnum>.GetDescriptions().ToList();
            Assert.AreEqual(3, allDescriptions.Count());
            Assert.AreEqual("First enum", allDescriptions[0]);
            Assert.AreEqual("Second enum", allDescriptions[1]);
            Assert.AreEqual("Third enum", allDescriptions[2]);
        }

        [TestMethod]
        public void RetrieveAllDescriptionsAttributesAsPair()
        {
            var allDescriptions = Enum<ETestEnum>.GetEnumDescriptionPairs().ToList();
            Assert.AreEqual(3, allDescriptions.Count());

            Assert.AreEqual(ETestEnum.First, allDescriptions[0].Key);
            Assert.AreEqual(ETestEnum.Second, allDescriptions[1].Key);
            Assert.AreEqual(ETestEnum.Third, allDescriptions[2].Key);

            Assert.AreEqual("First enum", allDescriptions[0].Value);
            Assert.AreEqual("Second enum", allDescriptions[1].Value);
            Assert.AreEqual("Third enum", allDescriptions[2].Value);
        }

        [TestMethod]
        public void RetrieveAllSpecifiAttributes()
        {
            var allAttributes = Enum<ETestEnum>.GetAttributes<MyDescriptionAttribute>().ToList();
            Assert.AreEqual(3, allAttributes.Count());

            Assert.AreEqual(1, allAttributes[0].Numerical);
            Assert.AreEqual(2, allAttributes[1].Numerical);
            Assert.AreEqual(3, allAttributes[2].Numerical);

            Assert.AreEqual("One", allAttributes[0].Literal);
            Assert.AreEqual("Two", allAttributes[1].Literal);
            Assert.AreEqual("Three", allAttributes[2].Literal);
        }

        [TestMethod]
        public void RetrieveAllSpecifiAttributessAsPair()
        {
            var allDescriptions = Enum<ETestEnum>.GetEnumAttributePairs<MyDescriptionAttribute>().ToList();
            Assert.AreEqual(3, allDescriptions.Count());

            Assert.AreEqual(ETestEnum.First, allDescriptions[0].Key);
            Assert.AreEqual(ETestEnum.Second, allDescriptions[1].Key);
            Assert.AreEqual(ETestEnum.Third, allDescriptions[2].Key);

            Assert.AreEqual("One", allDescriptions[0].Value.Literal);
            Assert.AreEqual("Two", allDescriptions[1].Value.Literal);
            Assert.AreEqual("Three", allDescriptions[2].Value.Literal);

            Assert.AreEqual(1, allDescriptions[0].Value.Numerical);
            Assert.AreEqual(2, allDescriptions[1].Value.Numerical);
            Assert.AreEqual(3, allDescriptions[2].Value.Numerical);

        }
        [TestMethod]
        [Description("Retrieve all values of an enumerator.")]
        public void GetAllEnumValues()
        {
            var allEnums = Enum<ETestEnum>.GetValues().ToList();
            Assert.AreEqual(3, allEnums.Count());
            Assert.AreEqual(ETestEnum.First, allEnums[0]);
            Assert.AreEqual(ETestEnum.Second, allEnums[1]);
            Assert.AreEqual(ETestEnum.Third, allEnums[2]);
        }

        [TestMethod]
        [Description("Test if exception thrown if developer try to use another type than an enum")]
        [ExpectedException(typeof(ArgumentException))]
        public void GetValuesException()
            => Enum<double>.GetValues();


        [TestMethod]
        [Description("Test if exception thrown if developer try to use another type than an enum")]
        [ExpectedException(typeof(ArgumentException))]
        public void GetDescriptionException()
            => Enum<double>.GetDescriptions();


        [TestMethod]
        [Description("Test if exception thrown if developer try to use another type than an enum")]
        [ExpectedException(typeof(ArgumentException))]
        public void GetAttributesException()
            => Enum<int>.GetAttributes<MyDescriptionAttribute>();

        [TestMethod]
        [Description("Test if exception thrown if developer try to use another type than an enum")]
        [ExpectedException(typeof(ArgumentException))]
        public void GetEnumAttributePairsException()
         => Enum<int>.GetEnumAttributePairs<MyDescriptionAttribute>();


        [TestMethod]
        [Description("Test if exception thrown if developer try to use another type than an enum")]
        [ExpectedException(typeof(ArgumentException))]
        public void GetEnumDescriptionPairsException()
         => Enum<int>.GetEnumAttributePairs<MyDescriptionAttribute>().ToList();
    }
}
