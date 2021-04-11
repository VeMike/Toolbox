// ===================================================================================================
// = Author      :  Mike
// = Created     :  2021-03-07 16:12
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using NUnit.Framework;
using Toolbox.IniFileParser.Parsing.Files;

namespace Toolbox.IniFileParser.Test.Tests.Parsing.Files
{
    [TestFixture]
    public class PropertyTests
    {
        [TestCase("Foo", null)]
        [TestCase(null, "Foo")]
        public void ConstructorThrowsOnNullArgument(string name, string value)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var _ = new Property(name, value);
            });
        }

        [TestCase("Foo", "Bar", "Foo", "Bar", ExpectedResult = true)]
        [TestCase("Foo", "Baz", "Foo", "Bar", ExpectedResult = false)]
        [TestCase("Baz", "Bar", "Foo", "Bar", ExpectedResult = false)]
        public bool InstanceEqualityTests(string firstName,
                                          string firstValue,
                                          string secondName,
                                          string secondValue)
        {
            var first = new Property(firstName, firstValue);
            var second = new Property(secondName, secondValue);

            return first.Equals(second);
        }

        [Test]
        public void PropertyNameAndValueIsAssignedInConstructor()
        {
            var property = new Property("Foo", "Bar");
            
            Assert.Multiple(() =>
            {
                Assert.AreEqual("Foo", property.Name);
                Assert.AreEqual("Bar", property.Value);
            });
        }

        [Test]
        public void ToStringCreatesIniFileProperty()
        {
            const string EXPECTED = "Foo=Bar";

            var property = new Property("Foo", "Bar");
            
            Assert.AreEqual(EXPECTED, property.ToString());
        }
    }
}