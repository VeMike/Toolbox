// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-05-19 17:41
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using NUnit.Framework;
using Toolbox.CommandLineMapper.Mapper;

namespace Toolbox.CommandLineMapper.Test.Tests.Mapper
{
    [TestFixture]
    public class MapperOptionsTests
    {
        #region Tests

        [TestCase("")]
        [TestCase(null)]
        public void InvalidOptionPrefixThrows(string prefix)
        {
            var options = new MapperOptions()
            {
                OptionPrefix = prefix
            };

            Assert.Throws<OptionsException>(() =>
            {
                options.ValidateOptions();
            });
        }

        #endregion
    }
}