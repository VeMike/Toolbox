// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-01-14 19:56
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using NUnit.Framework;
using Toolbox.CommandLineMapper.Mapper;
using Toolbox.CommandLineMapper.Test.MockData.MockObjects;

namespace Toolbox.CommandLineMapper.Test.Tests.Mapper
{
    [TestFixture]
    public class MapperTests
    {
        #region Tests

        [Test]
        public void TypeCanBeRegistered()
        {
            var mapper = new DefaultMapper();

            mapper.Register<Options>();

            Assert.IsTrue(mapper.IsRegistered<Options>());
        }

        [Test]
        public void SameTypeOnlyRegisteredOnce()
        {
            var mapper = new DefaultMapper();

            mapper.Register<Options>();
            mapper.Register<Options>();

            Assert.AreEqual(1, mapper.Registrations);
        }

        [Test]
        public void RegisteredTypeCanBeRemoved()
        {
            var mapper = new DefaultMapper();

            mapper.Register<Options>();
            mapper.UnRegister<Options>();

            Assert.IsFalse(mapper.IsRegistered<Options>());
        }

        [Test]
        public void DifferentTypesCanBeRegistered()
        {
            var mapper = new DefaultMapper();

            mapper.Register<Options>();
            mapper.Register<OtherOptions>();

            Assert.Multiple(() =>
            {
                Assert.IsTrue(mapper.IsRegistered<Options>());
                Assert.IsTrue(mapper.IsRegistered<OtherOptions>());
            });
        }

        #endregion
    }
}