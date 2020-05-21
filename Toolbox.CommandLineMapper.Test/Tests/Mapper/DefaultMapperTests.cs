// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-01-14 19:56
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Toolbox.CommandLineMapper.Common;
using Toolbox.CommandLineMapper.Core.Wrappers;
using Toolbox.CommandLineMapper.Mapper;
using Toolbox.CommandLineMapper.Test.MockData.MockObjects;

namespace Toolbox.CommandLineMapper.Test.Tests.Mapper
{
    [TestFixture]
    public class DefaultMapperTests
    {
        #region Tests

        [Test]
        public void MapMethodThrowsIfArgumentsAreNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var mapper = new DefaultMapper();

                mapper.Map(null);
            });
        }

        [Test]
        public void MapMethodThrowsIfOptionsAreNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var mapper = new DefaultMapper();

                mapper.Map(Enumerable.Empty<string>(), null);
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

        [Test]
        public void SingleObjectPropertyIsNotFound()
        {
            var result = MapSingleOptionsObject("-p C:\\some\\file\\path -x 200");
            
            Assert.IsInstanceOf<PropertyNotFoundException>(result.Errors[0].Cause);
        }

        [Test]
        public void SingleObjectPropertyCanNotBeCast()
        {
            var result = MapSingleOptionsObject("-p C:\\some\\file\\path -s foo");
            
            //Parameter '-s' expects an integer
            Assert.IsInstanceOf<InvalidCastException>(result.Errors[0].Cause);
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