// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-06-08 20:42
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using NUnit.Framework;
using Toolbox.Utils.Common;
using Toolbox.Utils.Test.MockObjects.Common;

namespace Toolbox.Utils.Test.Tests.Common
{
    [TestFixture]
    public class PropertyByNameSetterTests
    {
        #region Tests

        [Test]
        public void ConstructorThrowsIfArgumentIsNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var _ = new PropertyByNameReaderWriter(null);
            });
        }

        [TestCase(null, "")]
        [TestCase("", null)]
        public void SetPropertyThrowsOnInvalidArgument(string name, object value)
        {
            //Does not really matter what we pass here
            var readerWriter = new PropertyByNameReaderWriter(new object());

            Assert.Throws<ArgumentNullException>(() =>
            {
                readerWriter.SetProperty(name, value);
            });
        }
        
        [TestCase(null)]
        public void GetPropertyThrowsOnInvalidArgument(string name)
        {
            //Does not really matter what we pass here
            var readerWriter = new PropertyByNameReaderWriter(new object());

            Assert.Throws<ArgumentNullException>(() =>
            {
                readerWriter.GetProperty(name);
            });
        }

        [Test]
        public void SetPropertyThrowsIfNameIsNotFound()
        {
            //Does not really matter what we pass here
            var readerWriter = new PropertyByNameReaderWriter(new object());

            Assert.Throws<InvalidOperationException>(() =>
            {
                readerWriter.SetProperty("ThisNameIsNotFound", "Foo");
            });
        }
        
        [Test]
        public void GetPropertyThrowsIfNameIsNotFound()
        {
            //Does not really matter what we pass here
            var readerWriter = new PropertyByNameReaderWriter(new object());

            Assert.Throws<InvalidOperationException>(() =>
            {
                var _ = readerWriter.GetProperty("ThisNameIsNotFound");
            });
        }

        [Test]
        public void PrivatePropertyIsNotWritable()
        {
            var readerWriter = new PropertyByNameReaderWriter(new ReaderWriter(10,
                                                                               "getterOnly",
                                                                               "setterPrivateGet",
                                                                               "privateSetterGet"));

            Assert.Throws<InvalidOperationException>(() =>
            {
                readerWriter.SetProperty("PrivateProperty", "TheValue");
            });
        }

        [Test]
        public void PrivatePropertyIsNotReadable()
        {
            var readerWriter = new PropertyByNameReaderWriter(new ReaderWriter(10,
                                                                               "getterOnly",
                                                                               "setterPrivateGet",
                                                                               "privateSetterGet"));

            Assert.Throws<InvalidOperationException>(() =>
            {
                readerWriter.GetProperty("PrivateProperty");
            });
        }

        [Test]
        public void SetPropertyThrowsIfSetterIsNotPublic()
        {
            var readerWriter = new PropertyByNameReaderWriter(new ReaderWriter(10,
                                                                                "getterOnly",
                                                                                "setterPrivateGet",
                                                                                "privateSetterGet"));

            Assert.Throws<FieldAccessException>(() =>
            {
                readerWriter.SetProperty(nameof(ReaderWriter.StringPrivateSetterGet), "TheNewValue");
            });
        }

        [Test]
        public void GetPropertyThrowsIfGetterIsNotPublic()
        {
            var readerWriter = new PropertyByNameReaderWriter(new ReaderWriter(10,
                                                                               "getterOnly",
                                                                               "setterPrivateGet",
                                                                               "privateSetterGet"));

            Assert.Throws<FieldAccessException>(() =>
            {
                readerWriter.GetProperty(nameof(ReaderWriter.StringSetterPrivateGet));
            });
        }

        [Test]
        public void SetValueOfGetOnlyPropertyThrows()
        {
            var readerWriter = new PropertyByNameReaderWriter(new ReaderWriter(10,
                                                                               "getterOnly",
                                                                               "setterPrivateGet",
                                                                               "privateSetterGet"));

            Assert.Throws<FieldAccessException>(() =>
            {
                readerWriter.SetProperty(nameof(ReaderWriter.StringGetterOnly), "NewValue");
            });
        }

        [TestCase(nameof(AllGettersAndSettersPublic.BooleanProperty), true)]
        [TestCase(nameof(AllGettersAndSettersPublic.DoubleProperty), 100.0d)]
        [TestCase(nameof(AllGettersAndSettersPublic.IntProperty), 5)]
        [TestCase(nameof(AllGettersAndSettersPublic.StringProperty), "Foo")]
        public void GetAndSetValuesOfProperties(string propertyName, object expectedValue)
        {
            var target = new AllGettersAndSettersPublic();
            var readerWriter = new PropertyByNameReaderWriter(target);
            
            readerWriter.SetProperty(propertyName, expectedValue);

            var actualValue = readerWriter.GetProperty(propertyName);
            
            Assert.AreEqual(expectedValue, actualValue);
        }

        #endregion
    }
}