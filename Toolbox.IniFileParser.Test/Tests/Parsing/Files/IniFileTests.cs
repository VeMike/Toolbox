// ===================================================================================================
// = Author      :  Mike
// = Created     :  2021-03-13 16:59
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using System.Collections.Generic;
using NUnit.Framework;
using Toolbox.IniFileParser.Parsing.Files;
using Toolbox.IniFileParser.Test.Mock;

namespace Toolbox.IniFileParser.Test.Tests.Parsing.Files
{
    [TestFixture]
    public class IniFileTests
    {
        [Test]
        public void DefaultConstructorCreatesNewParserInstance()
        {
            var ini = new IniFile();
            
            Assert.IsNotNull(ini.Parser);
        }

        [Test]
        public void TextFileConstructorThrowsIfArgumentIsNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var _ = new IniFile(null);
            });
        }

        [Test]
        public void ParserAndFileConstructorThrowsIfArgumentIsNull()
        {
            Assert.Multiple(() =>
            {
                Assert.Throws<ArgumentNullException>(() =>
                {
                    var _ = new IniFile(null, new MockReadableTextFile(new List<string>()));
                });

                Assert.Throws<ArgumentNullException>(() =>
                {
                    var _ = new IniFile(new Parser(), null);
                });
            });
        }

        [Test]
        public void AddThrowsIfArgumentIsNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new IniFile().Add(null);
            });
        }

        [Test]
        public void AddThrowsIfSectionIsAlreadyPresent()
        {
            var ini = new IniFile();

            var section = new Section("Foo");
            ini.Add(section);

            Assert.Throws<ArgumentException>(() =>
            {
                ini.Add(section);
            });
        }

        [Test]
        public void RemoveByIndexThrowsIfIndexIsOutOfRange()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                new IniFile().Remove(99);
            });
        }

        [Test]
        public void RemoveByIndexRemovesSectionAtSpecifiedIndex()
        {
            var ini = new IniFile();
            ini.Add(new Section("Foo"));
            ini.Add(new Section("Bar"));
            
            ini.Remove(1);
            
            Assert.IsFalse(ini.Contains("Bar"));
        }

        [Test]
        public void RemoveBySectionThrowsIfArgumentIsNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new IniFile().Remove(null);
            });
        }

        [Test]
        public void RemoveBySectionRemovesSpecifiedSection()
        {
            var ini = new IniFile();
            var section = new Section("Foo");
            ini.Add(section);
            ini.Add(new Section("Bar"));
            
            ini.Remove(section);
            
            Assert.IsFalse(ini.Contains("Foo"));
        }

        [Test]
        public void SectionByNameIndexerThrowsOnNullArgument()
        {
            var ini = new IniFile();

            Assert.Throws<ArgumentNullException>(() =>
            {
                var _ = ini[null];
            });
        }

        [Test]
        public void SectionByNameIndexerReturnsSpecifiedSection()
        {
            var ini = new IniFile();
            var section = new Section("Foo");
            ini.Add(section);

            var result = ini["Foo"];
            
            Assert.AreEqual(section, result);
        }

        [Test]
        public void SectionByIndexIndexerThrowsIfIndexIsOutOfRange()
        {
            var ini = new IniFile();

            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var _ = ini[99];
            });
        }

        [Test]
        public void SectionByIndexIndexerReturnsSpecifiedSection()
        {
            var ini = new IniFile();
            var section = new Section("Foo");
            ini.Add(section);

            var result = ini[0];
            
            Assert.AreEqual(section, result);
        }

        [Test]
        public void CorrectIniFileIsParsed()
        {
            var lines = new List<string>
            {
                "[Foo]",
                "Bar=Baz",
                "Toz=Taz"
            };

            var file = new IniFile(new MockReadableTextFile(lines));
            
            Assert.Multiple(() =>
            {
                var section = file["Foo"];
                var bar = section["Bar"];
                var toz = section["Toz"];
                
                Assert.AreEqual("Baz", bar.Value);
                Assert.AreEqual("Taz", toz.Value);
            });
        }

        [Test]
        public void ConstructorThrowsIfParserEncountersPropertyBeforeSection()
        {
            var lines = new List<string>
            {
                "Bar=Baz",
                "[Foo]",
                "Toz=Taz"
            };

            Assert.Throws<InvalidOperationException>(() =>
            {
                var _ = new IniFile(new MockReadableTextFile(lines));
            });
        }

        [Test]
        public void ToStringCreatesIniFileContents()
        {
            var lines = new List<string>
            {
                "[Foo]",
                "Bar=Baz",
                "Toz=Taz"
            };

            var file = new IniFile(new MockReadableTextFile(lines));

            var content = file.ToString();
            
            Assert.AreEqual("[Foo]\r\nBar=Baz\r\nToz=Taz\r\n\r\n", content);
        }
    }
}