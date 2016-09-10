
using System;

namespace ModbusTcpToSnmp.PCL.Interfaces.IoC
{
    public interface IParameterOverride
    {
        string Name { get; }
        object Value { get; }
    }

    public interface IContainer {
        IContainer CreateChildContainer();

        IContainer Register(Type from, Type to, string name = null);
        IContainer Register<TFrom, TTo>(string name = null) where TTo : TFrom;
        IContainer RegisterInstance<T>(T instance, string name = null);
        IContainer RegisterAsSingleton<TFrom, TTo>(string name = null) where TTo : TFrom;
		T Resolve<T>(string name = null, params IParameterOverride[] paramOverrides);
        T Resolve<T>(params IParameterOverride[] paramOverrides);
    }
}
