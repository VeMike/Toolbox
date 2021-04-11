// ===================================================================================================
// = Author      :  Mike
// = Created     :  2021-03-07 16:22
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using System.Collections.Generic;
using NUnit.Framework;
using Toolbox.IniFileParser.Parsing.Files;

namespace Toolbox.IniFileParser.Test.Tests.Parsing.Files
{
    [TestFixture]
    public class SectionTests
    {
        [Test]
        public void ConstructorThrowsOnNullArgument()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var _ = new Section(null);
            });
        }

        [Test]
        public void SectionNameIsAssignedInConstructor()
        {
            var section = new Section("Foo");
            
            Assert.AreEqual("Foo", section.Name);
        }

        [Test]
        public void PropertyByNameIndexerThrowsIfPropertyNotFound()
        {
            var section = new Section("Foo");

            Assert.Throws<KeyNotFoundException>(() =>
            {
                var _ = section["Bar"];
            });
        }

        [Test]
        public void PropertyByNameIndexerGetsPropertyWithName()
        {
            var section = new Section("Foo");
            section.Add(new Property("Foo", "Bar"));

            var property = section["Foo"];
            
            Assert.Multiple(() =>
            {
                Assert.AreEqual("Foo", property.Name);
                Assert.AreEqual("Bar", property.Value);
            });
        }

        [Test]
        public void PropertyByIndexIndexerThrowsIfPropertyNotFound()
        {
            var section = new Section("Foo");

            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var _ = section[5];
            });
        }

        [Test]
        public void PropertyByIndexIndexerGetsPropertyAtIndex()
        {
            var section = new Section("Foo");
            section.Add(new Property("Foo", "Bar"));

            var property = section[0];
            
            Assert.Multiple(() =>
            {
                Assert.AreEqual("Foo", property.Name);
                Assert.AreEqual("Bar", property.Value);
            });
        }

        [Test]
        public void AddPropertyThrowsIfArgumentIsNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new Section("Foo").Add(null);
            });
        }

        [Test]
        public void AddPropertyThrowsIfPropertyAlreadyExists()
        {
            var section = new Section("Bar");
            section.Add(new Property("Foo", "Bar"));

            Assert.Throws<ArgumentException>(() =>
            {
                section.Add(new Property("Foo", "Bar"));
            });
        }

        [Test]
        public void RemovePropertyThrowsIfPropertyNotFound()
        {
            var section = new Section("Foo");

            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                section.Remove(10);
            });
        }

        [Test]
        public void RemovePropertyRemovesPropertyAtIndex()
        {
            var section = new Section("Foo");
            section.Add(new Property("Bar", "Baz"));
            
            if(!section.Contains("Bar"))
                Assert.Inconclusive("A property with name 'Bar' was not found");
            
            section.Remove(0);

            //The property was removed. The access should throw now
            Assert.Throws<KeyNotFoundException>(() =>
            {
                var _ = section["Bar"];
            });
        }

        [Test]
        public void RemovePropertyThrowsOnNullArgument()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new Section("Foo").Remove(null);
            });
        }

        [Test]
        public void RemovePropertyRemovesProperty()
        {
            var section = new Section("Foo");
            var property = new Property("Bar", "Baz");
            
            section.Add(property);
            
            if(!section.Contains(property.Name))
                Assert.Inconclusive($"The property '{property}' was not found");
            
            section.Remove(property);
            
            //The property was removed. The access should throw now
            Assert.Throws<KeyNotFoundException>(() =>
            {
                var _ = section["Bar"];
            });
        }

        [Test]
        public void ContainsThrowsIfArgumentIsNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new Section("").Contains(null);
            });
        }

        [Test]
        public void ToStringCreatesIniFileSection()
        {
            const string EXPECTED = "[Foo]\r\nBar=Baz\r\nToz=Taz\r\n\r\n";
            
            var section = new Section("Foo");
            section.Add(new Property("Bar", "Baz"));
            section.Add(new Property("Toz", "Taz"));
            
            Assert.AreEqual(EXPECTED, section.ToString());
        }
    }
}