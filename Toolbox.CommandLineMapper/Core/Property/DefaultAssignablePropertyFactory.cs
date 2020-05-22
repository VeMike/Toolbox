// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-05-22 22:42
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using Toolbox.CommandLineMapper.Specification;

namespace Toolbox.CommandLineMapper.Core.Property
{
    /// <summary>
    ///     The default implementation of <see cref="IAssignablePropertyFactory{TAttribute}"/>
    /// </summary>
    /// <typeparam name="TAttribute">
    ///    The type of attribute applied to the property
    /// </typeparam>
    public class DefaultAssignablePropertyFactory<TAttribute> : IAssignablePropertyFactory<TAttribute> where TAttribute : AttributeBase
    {
        #region IAssignablePropertyFactory Implementation

        /// <inheritdoc />
        /// <exception cref="NotSupportedException">
        ///    Thrown if this factory does not support creating an object for the
        ///     passed <paramref name="type"/>
        /// </exception>
        public IAssignableProperty<TAttribute> CreatePropertyForType(Type type)
        {
            if (type == typeof(string))
            {
                return new StringAssignableProperty<TAttribute>();
            }
            if (type == typeof(int))
            {
                return new IntegerAssignableProperty<TAttribute>();
            }
            if (type == typeof(bool))
            {
                return new BooleanAssignableProperty<TAttribute>();
            }
            if (type == typeof(char))
            {
                return new CharAssignableProperty<TAttribute>();
            }
            if (type == typeof(byte))
            {
                return new ByteAssignableProperty<TAttribute>();
            }
            if (type == typeof(short))
            {
                return new ShortAssignableProperty<TAttribute>();
            }
            if (type == typeof(long))
            {
                return new LongAssignableProperty<TAttribute>();
            }
            if (type == typeof(float))
            {
                return new FloatAssignableProperty<TAttribute>();
            }
            if (type == typeof(double))
            {
                return new DoubleAssignableProperty<TAttribute>();
            }

            throw new NotSupportedException($"Mapping to properties of type '{type.Name}' is currently not supported");
        }

        #endregion
    }
}