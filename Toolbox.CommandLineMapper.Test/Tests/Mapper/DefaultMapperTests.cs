// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-01-14 19:56
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using NUnit.Framework;
using Toolbox.CommandLineMapper.Common;
using Toolbox.CommandLineMapper.Mapper;
using Toolbox.CommandLineMapper.Test.MockData.MockObjects;

namespace Toolbox.CommandLineMapper.Test.Tests.Mapper
{
    [TestFixture]
    public class DefaultMapperTests
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

        [Test]
        public void GetMapperResultThrowsIfObjectNotRegistered()
        {
            var mapper = new DefaultMapper();
            
            mapper.Register<Options>();

            Assert.Throws<ArgumentException>(() =>
            {
                mapper.GetMapperResult<OtherOptions>();
            });
        }

        [Test]
        public void SingleObjectIsMappedFromValidString()
        {
            var result = MapSingleOptionsObject("-p C:\\some\\file\\path -s 200");

            CollectionAssert.IsEmpty(result.Errors);
        }

        [Test]
        public void SingleObjectValuesAreAssignedCorrectly()
        {
            var result = MapSingleOptionsObject("-p C:\\some\\file\\path -s 200").Value;
            
            Assert.Multiple(() =>
            {
                Assert.AreEqual("C:\\some\\file\\path", result.Path);
                Assert.AreEqual(200, result.Size);
            });
        }

        #endregion

        #region Helpers

        /// <summary>
        ///     Maps the passed command line string to an
        ///     <see cref="Options"/> mock object
        /// </summary>
        /// <param name="arguments">
        ///    The arguments, that should be mapped
        /// </param>
        /// <returns>
        ///    The result of this operation
        /// </returns>
        private static IMapperResult<Options> MapSingleOptionsObject(string arguments)
        {
            var mapper = new DefaultMapper();

            mapper.Register<Options>();

            mapper.Map(arguments.SplitArguments());

            return mapper.GetMapperResult<Options>();
        }

        #endregion
    }
}