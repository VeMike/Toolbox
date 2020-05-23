// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-01-14 19:56
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
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
            var result = MapArgumentsToType<IntegratedTypesOptions>("-stringProperty C:\\some\\file\\path -foo 200");
            
            Assert.IsInstanceOf<PropertyNotFoundException>(result.Errors[0].Cause);
        }

        [Test]
        public void SingleObjectPropertyCanNotBeCast()
        {
            var result = MapArgumentsToType<IntegratedTypesOptions>("-stringProperty C:\\some\\file\\path -integerProperty bar");
            
            //Parameter '-s' expects an integer
            Assert.IsInstanceOf<InvalidCastException>(result.Errors[0].Cause);
        }

        [Test]
        public void FirstPropertyIsMappedIfShortNameConflicts()
        {
            var result = MapArgumentsToType<ConflictingShortNames>("-a TheValue").Value;

            Assert.Multiple(() =>
            {
                Assert.AreEqual("TheValue", result.FirstProperty);
                Assert.AreEqual("", result.SecondProperty);
            });
        }
        
        [Test]
        public void FirstPropertyIsMappedIfLongNameConflicts()
        {
            var result = MapArgumentsToType<ConflictingShortNames>("-firstProperty TheValue").Value;

            Assert.Multiple(() =>
            {
                Assert.AreEqual("TheValue", result.FirstProperty);
                Assert.AreEqual("", result.SecondProperty);
            });
            
        }
        
        [Test]
        public void BooleanPropertyIsMappedWithoutValue()
        {
            var result = MapArgumentsToType<IntegratedTypesOptions>("-booleanProperty").Value;
            
            Assert.IsTrue(result.BooleanProperty);
        }

        [Test]
        public void BooleanPropertyIsMappedWithValue()
        {
            var result = MapArgumentsToType<IntegratedTypesOptions>("-booleanProperty true").Value;
            
            Assert.IsTrue(result.BooleanProperty);
        }

        [Test]
        public void BytePropertyIsMapped()
        {
            var result = MapArgumentsToType<IntegratedTypesOptions>("-byteProperty 15").Value;
            
            Assert.AreEqual(15, result.ByteProperty);
        }
        
        [Test]
        public void CharPropertyIsMapped()
        {
            var result = MapArgumentsToType<IntegratedTypesOptions>("-charProperty A").Value;
            
            Assert.AreEqual('A', result.CharProperty);
        }
        
        [Test]
        public void DoublePropertyIsMapped()
        {
            var result = MapArgumentsToType<IntegratedTypesOptions>("-doubleProperty 1.234").Value;
            
            Assert.AreEqual(1.234, result.DoubleProperty, 0.005);
        }
        
        [Test]
        public void FloatPropertyIsMapped()
        {
            var result = MapArgumentsToType<IntegratedTypesOptions>("-floatProperty 1.234").Value;
            
            Assert.AreEqual(1.234, result.FloatProperty, 0.005);
        }
        
        [Test]
        public void IntegerPropertyIsMapped()
        {
            var result = MapArgumentsToType<IntegratedTypesOptions>("-integerProperty 12").Value;
            
            Assert.AreEqual(12, result.IntegerProperty);
        }
        
        [Test]
        public void LongPropertyIsMapped()
        {
            var result = MapArgumentsToType<IntegratedTypesOptions>("-longProperty 5000").Value;
            
            Assert.AreEqual(5000, result.LongProperty);
        }
        
        [Test]
        public void ShortPropertyIsMapped()
        {
            var result = MapArgumentsToType<IntegratedTypesOptions>("-shortProperty 4").Value;
            
            Assert.AreEqual(4, result.ShortProperty);
        }

        [Test]
        public void StringPropertyIsMapped()
        {
            var result = MapArgumentsToType<IntegratedTypesOptions>("-stringProperty C:\\some\\file\\path").Value;
            
            Assert.AreEqual("C:\\some\\file\\path", result.StringProperty);
        }

        [Test]
        public void OptionLongNameDifferentFromPropertyName()
        {
            var result = MapArgumentsToType<PropertyNamesOtherThanOptionName>("-foo 300").Value;

            Assert.AreEqual(300, result.LongNameDifferent);
        }

        [Test]
        public void OptionShortNameDifferentFromPropertyName()
        {
            var result = MapArgumentsToType<PropertyNamesOtherThanOptionName>("-a 12").Value;

            Assert.AreEqual(12, result.ShortNameDifferent);
        }

        [Test]
        public void ShortNameOptionFoundIfLongNameSpecified()
        {
            var result = MapArgumentsToType<NamedOptions>("-n 100").Value;
            
            Assert.AreEqual(100, result.NumberLongName);
        }

        [Test]
        public void LongNameOptionFoundIfShortNameSpecified()
        {
            var result = MapArgumentsToType<NamedOptions>("-textShortName TheText").Value;
            
            Assert.AreEqual("TheText", result.TextShortName);
        }

        #endregion

        #region Helpers

        /// <summary>
        ///     Maps the passed arguments to the specified type
        /// </summary>
        /// <param name="arguments">
        ///    The arguments mapped to the specified type
        /// </param>
        /// <typeparam name="T">
        ///    The type to whom the arguments are mapped
        /// </typeparam>
        /// <returns>
        ///    An instance of the specified type with all
        ///     properties mapped to the passed arguments
        /// </returns>
        private static IMapperResult<T> MapArgumentsToType<T>(string arguments) where T : class, new()
        {
            var mapper = new DefaultMapper();
            
            mapper.RegistrationService.Register<T>();
            
            mapper.Map(arguments.SimpleSplitArguments());

            var result = mapper.GetMapperResult<T>();

            if (result.Errors.Count > 0)
            {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine(error);
                }
                
                Assert.Inconclusive($"Failed to map arguments {arguments} to '{typeof(T).Name}'");
            }

            return result;
        }

        #endregion
    }
}