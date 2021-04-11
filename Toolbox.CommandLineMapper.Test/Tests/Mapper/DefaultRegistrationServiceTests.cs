// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-05-21 23:58
// ===================================================================================================
// = Description :
// ===================================================================================================

using NUnit.Framework;
using Toolbox.CommandLineMapper.Mapper;
using Toolbox.CommandLineMapper.Test.MockData.MockObjects;

namespace Toolbox.CommandLineMapper.Test.Tests.Mapper
{
    [TestFixture]
    public class DefaultRegistrationServiceTests
    {
        [Test]
        public void TypeCanBeRegistered()
        {
            var service = new DefaultRegistrationService();

            service.Register<Options>();

            Assert.IsTrue(service.IsRegistered<Options>());
        }

        [Test]
        public void SameTypeOnlyRegisteredOnce()
        {
            var service = new DefaultRegistrationService();

            service.Register<Options>();
            service.Register<Options>();

            Assert.AreEqual(1, service.Registrations);
        }

        [Test]
        public void RegisteredTypeCanBeRemoved()
        {
            var service = new DefaultRegistrationService();

            service.Register<Options>();
            service.UnRegister<Options>();

            Assert.IsFalse(service.IsRegistered<Options>());
        }

        [Test]
        public void DifferentTypesCanBeRegistered()
        {
            var service = new DefaultRegistrationService();

            service.Register<Options>();
            service.Register<OtherOptions>();

            Assert.Multiple(() =>
            {
                Assert.IsTrue(service.IsRegistered<Options>());
                Assert.IsTrue(service.IsRegistered<OtherOptions>());
            });
        }

        [Test]
        public void InstanceOfTypeIsReturned()
        {
            var service = new DefaultRegistrationService();
            
            service.Register<Options>();

            var instance = service.GetInstanceOf(typeof(Options));
            
            Assert.IsInstanceOf<Options>(instance);
        }
    }
}