using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
namespace CodeCraft.EnumExtension.CoreUnitTests
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
        public void GetEnumValueDescription()
        {
            var firsDescription = ETestEnum.First.DescriptionAttribute();
            Assert.AreEqual("First enum", firsDescription);

        }
        [TestMethod]
        public void GetEnumValueSpecifiAttributes()
        {
            var firstSpecificAttr = ETestEnum.First.SpecificAttribute<MyDescriptionAttribute>();
            Assert.AreEqual("One", firstSpecificAttr.Literal);
            Assert.AreEqual(1, firstSpecificAttr.Numerical);
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
    }
}
