using Microsoft.Practices.Unity;
using PCL.Interfaces.IoC;
using PCL.Interfaces.Logging;
using PCL.ViewModel.Logging;
using System;
using System.Linq;

namespace PCL.ViewModel.IoC
{
    public class ServiceLocator : IContainer, IDisposable
    {
        private IUnityContainer _container;
        private static readonly ServiceLocator DefaultContainer = new ServiceLocator();

        public static IContainer Default
        {
            get
            {
                return DefaultContainer;
            }
        }

        private ServiceLocator(IUnityContainer container = null)
        {
            if (container == null)
            {
                container = new UnityContainer();
            }
            this._container = container;
            this._container.RegisterInstance<IContainer>(this);
        }

        public void Dispose()
        {
            if (this._container != null)
            {
                this._container.Dispose();
                this._container = null;
            }
        }

        public IContainer CreateChildContainer()
        {
            var child = this._container.CreateChildContainer();
            return new ServiceLocator(child);
        }

        public IContainer Register(Type from, Type to, string name = null)
        {
            this._container.RegisterType(from, to, name ?? string.Empty);
            return this;
        }

        public IContainer Register<TFrom, TTo>(string name = null) where TTo : TFrom
        {
            this._container.RegisterType<TFrom, TTo>(name ?? string.Empty);
            return this;
        }
        public IContainer RegisterInstance<T>(T instance, string name)
        {
            this._container.RegisterInstance(name ?? string.Empty, instance);
            return this;
        }

        public IContainer RegisterAsSingleton<TFrom, TTo>(string name = null) where TTo : TFrom
        {
            this._container.RegisterType<TFrom, TTo>(new ContainerControlledLifetimeManager());
            return this;
        }

        public T Resolve<T>(string name = null, params IParameterOverride[] paramOverrides)
        {
            try
            {
                var overrides = paramOverrides
                .Select(p => (ResolverOverride)new Microsoft.Practices.Unity.ParameterOverride(p.Name, new InjectionParameter(p.Value.GetType(), p.Value)))
                .ToArray();

                return this._container.Resolve<T>(name, overrides);
            }
            catch (Exception e)
            {
                Logger.Current.LogException(e);
                throw;
            }
        }
        public T Resolve<T>(params IParameterOverride[] paramOverrides)
        {
            return this.Resolve<T>(null, paramOverrides);
        }
    }
}

    