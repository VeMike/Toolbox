// ===================================================================================================
// = Author      :  Mike
// = Created     :  2019-08-10 14:39
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using System.Collections.Generic;
using NUnit.Framework;
using Toolbox.Utils.Probing;

namespace Toolbox.Utils.Test.Tests.Probing
{
    [TestFixture]
    public class GuardTest
    {
        #region Tests

        [Test]
        [Description("Passes 'null' to Guard.AgainstNullArgument")]
        public void GuardAgainstNullArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => this.ArgumentReceivingMethod(null));
        }

        [Test]
        [Description("Passes a non-null argument to Guard.AgainstNullArgument")]
        public void GuardAgainstNullArgumentNotNull()
        {
            Assert.DoesNotThrow(() => this.ArgumentReceivingMethod(new object()));
        }

        [Test]
        [Description("Passes 'null' to Guard.AgainstNullProperty")]
        public void GuardAgainstNullPropertyNull()
        {
            var nullPropertyClass = new GuardTestHelpers();

            Assert.Throws<ArgumentException>(() => nullPropertyClass.NullProperty = null);
        }

        [Test]
        [Description("Passes non-null value to Guard.AgainstNullProperty")]
        public void GuardAgainstNullPropertyNotNull()
        {
            var nullPropertyClass = new GuardTestHelpers();

            Assert.DoesNotThrow(() => nullPropertyClass.NullProperty = new object());
        }

        [Test]
        [Description("Passes an empty string to 'Guard.AgainstEmptyString'")]
        public void GuardAgainstEmptyStringEmpty()
        {
            Assert.Throws<ArgumentException>(() => Guard.AgainstEmptyString(string.Empty));
        }

        [Test]
        [Description("Passes an non-empty string to 'Guard.AgainstEmptyString'")]
        public void GuardAgainstEmptyStringNotEmpty()
        {
            Assert.DoesNotThrow(() => Guard.AgainstEmptyString("A non-empty string"));
        }

        [Test]
        [Description("Passes an empty collection to 'Guard.AgainstEmptyCollection'")]
        public void GuardAgainstEmptyCollectionEmpty()
        {
            Assert.Throws<ArgumentException>(() => Guard.AgainstEmptyCollection(new List<int>()));
        }

        [Test]
        [Description("Passes an non-empty collection to 'Guard.AgainstEmptyCollection'")]
        public void GuardAgainstEmptyCollectionNotEmpty()
        {
            var collection = new List<int>{1,2,3};

            Assert.DoesNotThrow(() => Guard.AgainstEmptyCollection(collection));
        }

        #endregion

        #region Helpers

        /// <summary>
        ///     A method that receives an argument
        /// </summary>
        /// <param name="argument">
        ///     The argument to receive
        /// </param>
        private void ArgumentReceivingMethod(object argument)
        {
            Guard.AgainstNullArgument(nameof(argument), argument);
        }

        /// <summary>
        ///     Just a class, that contains properties and
        ///     methods used for tests of <see cref="Guard"/>
        /// </summary>
        private sealed class GuardTestHelpers
        {
            #region Attributes

            /// <summary>
            ///     The backing field of the null property
            /// </summary>
            // ReSharper disable once NotAccessedField.Local
            private object nullPropertyBackingField;

            #endregion

            #region Prpoerties

            /// <summary>
            ///     A property, that does not accept
            ///     null values
            /// </summary>
            public object NullProperty
            {
                set
                {
                    Guard.AgainstNullProperty(nameof(GuardTestHelpers),
                                              nameof(this.NullProperty),
                                              value);

                    this.nullPropertyBackingField = value;
                }
            }

            #endregion
        }

        #endregion
    }
}