// ===================================================================================================
// = Author      :  Mike
// = Created     :  2021-04-03 14:33
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using NUnit.Framework;
using Toolbox.Utils.Resource;
using Toolbox.Utils.Test.MockObjects.Resource;

namespace Toolbox.Utils.Test.Tests.Resource
{
    [TestFixture]
    public class SharedHandleTests
    {
        [Test]
        public void ResourceConstructorThrowsIfInstanceIsNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var _ = new SharedHandle<MockResource>((MockResource) null);
            });
        }
        
        [Test]
        public void FactoryConstructorThrowsIfInstanceIsNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var _ = new SharedHandle<MockResource>((Func<MockResource>)null);
            });
        }

        [Test]
        public void AccessCreatesAndAcquiredResource()
        {
            var resource = new MockResource();
            var handle = new SharedHandle<MockResource>(resource);

            var _ = handle.Access();
            
            Assert.Multiple(() =>
            {
                Assert.AreEqual(1, handle.Tokens);
                
                Assert.AreEqual(1, resource.AcquireCalls);
            });
        }

        [Test]
        public void LastTokenReleasesResource()
        {
            var resource = new MockResource();
            var handle = new SharedHandle<MockResource>(resource);

            var token = handle.Access();
            token.Dispose();
            
            Assert.Multiple(() =>
            {
                Assert.AreEqual(1, resource.ReleaseCalls);
                
                Assert.AreEqual(0, handle.Tokens);
            });
        }

        [Test]
        public void AccessAcquiresResourceOnlyOnce()
        {
            var resource = new MockResource();
            var handle = new SharedHandle<MockResource>(() => resource);

            handle.Access();
            handle.Access();
            
            Assert.AreEqual(1, resource.AcquireCalls);
        }

        [Test]
        public void TokenDisposeDoesNotReleaseResourceIfTokensLeft()
        {
            var resource = new MockResource();
            var handle = new SharedHandle<MockResource>(() => resource);

            var first = handle.Access();
            var _ = handle.Access();
            
            first.Dispose();
            
            Assert.AreEqual(0, resource.ReleaseCalls);
        }

        [Test]
        public void ResourceIsReacquiredAfterIsWasReleased()
        {
            var resource = new MockResource();
            var handle = new SharedHandle<MockResource>(() => resource);

            //This acquires the resource
            var first = handle.Access();
            //This releases the resource
            first.Dispose();

            //This should now reacquire the resource
            first = handle.Access();

            Assert.AreEqual(2, resource.AcquireCalls);
        }

        [Test]
        public void ResourceAccessOnTokenThrowsIfTokenIsDisposed()
        {
            var handle = new SharedHandle<MockResource>(new MockResource());

            var token = handle.Access();
            token.Dispose();

            Assert.Throws<ObjectDisposedException>(() =>
            {
                var _ = token.Resource;
            });
        }
    }
}