// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-01-14 19:56
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using System.Collections;
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
            var expectedUnmappedArg = new Argument("-", "-foo", "200");
            
            var mapper = new DefaultMapper();
            
            mapper.RegistrationService.Register<IntegratedTypesOptions>();
            mapper.Map("-stringProperty C:\\some\\file\\path -foo 200".SimpleSplitArguments());

            var unmapped = mapper.UnmappedArguments.ToList()[0];
            
            Assert.AreEqual(expectedUnmappedArg, unmapped);
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
        
        [TestCaseSource(typeof(DefaultMapperTests), nameof(GetAllPropertyTypesTestCases))]
        public object PropertyIsMappedToValidArgument(string arguments, 
                                                          Func<IntegratedTypesOptions, object> resultFactory)
        {
            var result = MapArgumentsToType<IntegratedTypesOptions>(arguments).Value;

            return resultFactory(result);
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

        [Test]
        public void MultipleObjectsAreMappedToValidArgumentString()
        {
            var mapper =
                MapArgumentsToTwoTypes<Options, OtherOptions>("-path C:\\temp\\test.txt -s 1000 -timeout 10 -verbose");

            var optionsResult = mapper.GetMapperResult<Options>();
            var otherOptionsResult = mapper.GetMapperResult<OtherOptions>();
            
            PrintAndAssertMappingError(optionsResult.Errors);
            PrintAndAssertMappingError(otherOptionsResult.Errors);
            
            Assert.Multiple(() =>
            {
                Assert.AreEqual("C:\\temp\\test.txt" ,optionsResult.Value.Path);
                Assert.AreEqual(1000, optionsResult.Value.Size);
                
                Assert.AreEqual(10, otherOptionsResult.Value.Timeout);
                Assert.IsTrue(otherOptionsResult.Value.Verbose);
            });
        }

        [Test]
        public void OneObjectMappedEvenIfOtherObjectIsNotFullyMapped()
        {
            var expectedUnmappedArgument = new Argument("-", "-foo", "10");
            
            var mapper =
                MapArgumentsToTwoTypes<Options, OtherOptions>("-path C:\\temp\\test.txt -s 1000 -foo 10 -verbose");

            var optionsResult = mapper.GetMapperResult<Options>();

            Assert.Multiple(() =>
            {
                Assert.AreEqual("C:\\temp\\test.txt" ,optionsResult.Value.Path);
                Assert.AreEqual(1000, optionsResult.Value.Size);
                
                Assert.AreEqual(expectedUnmappedArgument, mapper.UnmappedArguments.ToList()[0]);
            });
        }

        #endregion

        #region Test Data

        /// <summary>
        ///     Gets test cases for all default available mappable properties 
        /// </summary>
        private static IEnumerable GetAllPropertyTypesTestCases
        {
            get
            {
                yield return new TestCaseData("-booleanProperty", 
                                              new Func<IntegratedTypesOptions, object>(r => r.BooleanProperty)).Returns(true);
                yield return new TestCaseData("-booleanProperty true", 
                                              new Func<IntegratedTypesOptions, object>(r => r.BooleanProperty)).Returns(true);
                yield return new TestCaseData("-byteProperty 15", 
                                              new Func<IntegratedTypesOptions, object>(r => r.ByteProperty)).Returns(15);
                yield return new TestCaseData("-charProperty A", 
                                              new Func<IntegratedTypesOptions, object>(r => r.CharProperty)).Returns('A');
                yield return new TestCaseData("-doubleProperty 1.234", 
                                              new Func<IntegratedTypesOptions, object>(r => r.DoubleProperty)).Returns(1.234d);
                yield return new TestCaseData("-floatProperty 1.234", 
                                              new Func<IntegratedTypesOptions, object>(r => r.FloatProperty)).Returns(1.234f);
                yield return new TestCaseData("-integerProperty 12", 
                                              new Func<IntegratedTypesOptions, object>(r => r.IntegerProperty)).Returns(12);
                yield return new TestCaseData("-longProperty 5000", 
                                              new Func<IntegratedTypesOptions, object>(r => r.LongProperty)).Returns(5000);
                yield return new TestCaseData("-shortProperty 4", 
                                              new Func<IntegratedTypesOptions, object>(r => r.ShortProperty)).Returns(4);
                yield return new TestCaseData("-stringProperty C:\\some\\file\\path", 
                                              new Func<IntegratedTypesOptions, object>(r => r.StringProperty)).Returns("C:\\some\\file\\path");
            }
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

            return result;
        }

        /// <summary>
        ///     Maps the passed arguments to the specified types
        /// </summary>
        /// <param name="arguments">
        ///    The arguments to map
        /// </param>
        /// <typeparam name="TFirst">
        ///    The first type to map
        /// </typeparam>
        /// <typeparam name="TSecond">
        ///    The second type to map
        /// </typeparam>
        /// <returns>
        ///    The mapper that contains the result of the operation
        /// </returns>
        private static ICommandLineMapper MapArgumentsToTwoTypes<TFirst, TSecond>(string arguments)
            where TFirst : class, new()
            where TSecond : class, new()
        {
            var mapper = new DefaultMapper();
            
            mapper.RegistrationService.Register<TFirst>();
            mapper.RegistrationService.Register<TSecond>();
            
            mapper.Map(arguments.SimpleSplitArguments());

            return mapper;
        }

        /// <summary>
        ///     Checks if the passed list contains any erros
        ///     and if so print them to the console and calls
        ///     <see cref="Assert.Inconclusive(string,object[])"/>
        /// </summary>
        /// <param name="errors">
        ///    A list that can contains errors
        /// </param>
        private static void PrintAndAssertMappingError(IList<MappingError> errors)
        {
            if (errors.Count == 0)
                return;

            foreach (var mappingError in errors)
            {
                Console.WriteLine($"{mappingError}");
            }
            
            Assert.Inconclusive($"'{errors.Count}' errors occured while mapping args ");
        }

        #endregion
    }
}