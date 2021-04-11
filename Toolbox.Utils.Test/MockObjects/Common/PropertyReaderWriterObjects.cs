// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-06-08 23:32
// ===================================================================================================
// = Description :
// ===================================================================================================

namespace Toolbox.Utils.Test.MockObjects.Common
{
    public class ReaderWriter
    {
        public ReaderWriter(int intGetterAndSetter, 
                            string stringGetterOnly, 
                            string stringSetterPrivateGet, 
                            string stringPrivateSetterGet)
        {
            this.IntGetterAndSetter = intGetterAndSetter;
            this.StringGetterOnly = stringGetterOnly;
            this.StringSetterPrivateGet = stringSetterPrivateGet;
            this.StringPrivateSetterGet = stringPrivateSetterGet;
            this.PrivateProperty = "privateProperty";
        }
        
        public int IntGetterAndSetter { get; set; }
        
        public string StringGetterOnly { get; }
        
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        public string StringSetterPrivateGet { private get; set; }
        
        public string StringPrivateSetterGet { get; private set; }
        
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        private string PrivateProperty { get; set; }
    }

    public class AllGettersAndSettersPublic
    {
        public bool BooleanProperty { get; set; }
        
        public string StringProperty { get; set; }

        public double DoubleProperty { get; set; }

        public int IntProperty { get; set; }
    }
}