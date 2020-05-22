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
using Toolbox.CommandLineMapper.Core.Property;
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
            
            mapper.RegistrationService.Register<Options>();

            Assert.Throws<ArgumentException>(() =>
            {
                mapper.GetMapperResult<OtherOptions>();
            });
        }
        
        [Test]
        public void SingleObjectPropertyIsNotFound()
        {
            var result = MapToIntegratedTypesOptions("-stringProperty C:\\some\\file\\path -foo 200");
            
            Assert.IsInstanceOf<PropertyNotFoundException>(result.Errors[0].Cause);
        }

        [Test]
        public void SingleObjectPropertyCanNotBeCast()
        {
            var result = MapToIntegratedTypesOptions("-stringProperty C:\\some\\file\\path -integerProperty bar");
            
            //Parameter '-s' expects an integer
            Assert.IsInstanceOf<InvalidCastException>(result.Errors[0].Cause);
        }

        [Test]
        public void FirstPropertyIsMappedIfShortNameConflicts()
        {
            var mapper = new DefaultMapper();
            mapper.RegistrationService.Register<ConflictingShortNames>();
            
            mapper.Map("-a TheValue".SimpleSplitArguments());
            var result = mapper.GetMapperResult<ConflictingShortNames>().Value;
            
            Assert.Multiple(() =>
            {
                Assert.AreEqual("TheValue", result.FirstProperty);
                Assert.AreEqual("", result.SecondProperty);
            });
            
        }
        
        [Test]
        public void FirstPropertyIsMappedIfLongNameConflicts()
        {
            var mapper = new DefaultMapper();
            mapper.RegistrationService.Register<ConflictingLongNames>();
            
            mapper.Map("-firstProperty TheValue".SimpleSplitArguments());
            var result = mapper.GetMapperResult<ConflictingLongNames>().Value;
            
            Assert.Multiple(() =>
            {
                Assert.AreEqual("TheValue", result.FirstProperty);
                Assert.AreEqual("", result.SecondProperty);
            });
            
        }
        
        [Test]
        public void BooleanPropertyIsMappedWithoutValue()
        {
            var result = MapToIntegratedTypesOptions("-booleanProperty").Value;
            
            Assert.IsTrue(result.BooleanProperty);
        }

        [Test]
        public void BooleanPropertyIsMappedWithValue()
        {
            var result = MapToIntegratedTypesOptions("-booleanProperty true").Value;
            
            Assert.IsTrue(result.BooleanProperty);
        }

        [Test]
        public void BytePropertyIsMapped()
        {
            var result = MapToIntegratedTypesOptions("-byteProperty 15").Value;
            
            Assert.AreEqual(15, result.ByteProperty);
        }
        
        [Test]
        public void CharPropertyIsMapped()
        {
            var result = MapToIntegratedTypesOptions("-charProperty A").Value;
            
            Assert.AreEqual('A', result.CharProperty);
        }
        
        [Test]
        public void DoublePropertyIsMapped()
        {
            var result = MapToIntegratedTypesOptions("-doubleProperty 1.234").Value;
            
            Assert.AreEqual(1.234, result.DoubleProperty, 0.005);
        }
        
        [Test]
        public void FloatPropertyIsMapped()
        {
            var result = MapToIntegratedTypesOptions("-floatProperty 1.234").Value;
            
            Assert.AreEqual(1.234, result.FloatProperty, 0.005);
        }
        
        [Test]
        public void IntegerPropertyIsMapped()
        {
            var result = MapToIntegratedTypesOptions("-integerProperty 12").Value;
            
            Assert.AreEqual(12, result.IntegerProperty);
        }
        
        [Test]
        public void LongPropertyIsMapped()
        {
            var result = MapToIntegratedTypesOptions("-longProperty 5000").Value;
            
            Assert.AreEqual(5000, result.LongProperty);
        }
        
        [Test]
        public void ShortPropertyIsMapped()
        {
            var result = MapToIntegratedTypesOptions("-shortProperty 4").Value;
            
            Assert.AreEqual(4, result.ShortProperty);
        }

        [Test]
        public void StringPropertyIsMapped()
        {
            var result = MapToIntegratedTypesOptions("-stringProperty C:\\some\\file\\path").Value;
            
            Assert.AreEqual("C:\\some\\file\\path", result.StringProperty);
        }

        [Test]
        public void ShortNameOptionFoundIfLongNameSpecified()
        {
            var mapper = new DefaultMapper();
            
            mapper.RegistrationService.Register<NamedOptions>();
            
            //Property 'NumberLongName' should be recognized by short name 'n' even if not specified as attribute
            mapper.Map("-n 100".SimpleSplitArguments());
            
            var result = mapper.GetMapperResult<NamedOptions>();
            
            Assert.AreEqual(100, result.Value.NumberLongName);
        }

        [Test]
        public void LongNameOptionFoundIfShortNameSpecified()
        {
            var mapper = new DefaultMapper();
            
            mapper.RegistrationService.Register<NamedOptions>();
            
            //Property 'TextShortName' should be recognized by its full name even if not specified as attribute
            mapper.Map("-textShortName TheText".SimpleSplitArguments());
            
            var result = mapper.GetMapperResult<NamedOptions>();
            
            Assert.AreEqual("TheText", result.Value.TextShortName);
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
        private static IMapperResult<IntegratedTypesOptions> MapToIntegratedTypesOptions(string arguments)
        {
            var mapper = new DefaultMapper();

            mapper.RegistrationService.Register<IntegratedTypesOptions>();

            mapper.Map(arguments.SimpleSplitArguments());

            return mapper.GetMapperResult<IntegratedTypesOptions>();
        }

        #endregion
    }
}