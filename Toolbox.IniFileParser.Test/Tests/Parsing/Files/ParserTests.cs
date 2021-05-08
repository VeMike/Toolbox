// ===================================================================================================
// = Author      :  Mike
// = Created     :  2021-03-06 15:05
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
    public class ParserTests
    {
        [Test]
        public void ConstructorInitializesDefaultOptions()
        {
            var parser = new Parser();
            
            Assert.IsNotNull(parser.Options);
        }

        [Test]
        public void ConstructorThrowsOnNullArgument()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var _ = new Parser(null);
            });
        }

        [Test]
        public void ParseThrowsOnNullArgument()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new Parser().Parse(null);
            });
        }

        [Test]
        public void SectionEventIsRaisedIfSectionIsEncountered()
        {
            const string EXPECTED_SECTION = "Foo";
            
            var lines = new List<string>
            {
                $"[{EXPECTED_SECTION}]",
                "Bar = Baz",
                "Baz = Bar"
            };

            var receivedSections = new List<string>();

            var parser = new Parser();
            
            parser.Section += (_, args) =>
            {
                receivedSections.Add(args.Content);
            };
            
            parser.Parse(new MockReadableTextFile(lines));

            Assert.AreEqual(EXPECTED_SECTION, receivedSections[0]);
        }

        [Test]
        public void CommentEventIsRaisedIfCommentIsEncountered()
        {
            const string EXPECTED_COMMENT = "Hello World";
            
            var lines = new List<string>
            {
                "[Foo]",
                "# Hello World",
                "Bar = Baz",
                "Baz = Bar"
            };

            var receivedComments = new List<string>();

            var parser = new Parser();

            parser.Comment += (_, args) =>
            {
                receivedComments.Add(args.Content);
            };
            
            parser.Parse(new MockReadableTextFile(lines));
            
            Assert.AreEqual(EXPECTED_COMMENT, receivedComments[0]);
        }

        [Test]
        public void PropertyEventIsRaisedIfPropertyIsEncountered()
        {
            var expectedProperties = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("Bar", "Baz"),
                new KeyValuePair<string, string>("Baz", "Bar"),
                new KeyValuePair<string, string>("Foo", "Bar")
            };
            
            var lines = new List<string>
            {
                "[Foo]",
                "Bar = Baz",
                "Baz = Bar",
                "Foo = Bar"
            };

            var receivedProperties = new List<KeyValuePair<string, string>>();

            var parser = new Parser();

            parser.Property += (_, args) =>
            {
                receivedProperties.Add(args.Property);
            };
            
            parser.Parse(new MockReadableTextFile(lines));
            
            CollectionAssert.AreEquivalent(expectedProperties, receivedProperties);
        }
    }
}