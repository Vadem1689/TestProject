using System;
using System.Collections.Generic;

namespace DI
{
    public class DIСontainer
    {
        private readonly DIСontainer _parentDIContainer;
        private readonly Dictionary<(string, Type), DIRegistrations> _registrations = new(); 
        
        private readonly HashSet<(string, Type)> _resolutions = new();

        public DIСontainer(DIСontainer parentDiContainer = null)
        {
            _parentDIContainer = parentDiContainer;
        }

        public void RegisterSingleton<T>(Func<DIСontainer, T> factory)
        {
            RegisterSingleton(null, factory);
        }
        
        public void RegisterSingleton<T>(string tag ,Func<DIСontainer, T> factory)
        {
            var key = (tag, typeof(T));
            Register(key, factory, true);
        }

        public void RegisterTransient<T>(Func<DIСontainer, T> factory)
        {
            RegisterTransient(null, factory);
        }

        public void RegisterTransient<T>(string tag ,Func<DIСontainer, T> factory)
        {
            var key = (tag, typeof(T));
            Register(key, factory);

        }

        public void RegisterInstance<T>(T instance)
        {
            RegisterInstance(null, instance);
        }
        
        public void RegisterInstance<T>(string tag ,T instance)
        {
            var key = (tag, typeof(T));
            
            if (_registrations.ContainsKey(key))
            {
                throw new Exception($"DI: Factory with tag {key.Item1} and type {key.Item2.FullName} already registered.");
            }

            _registrations[key] = new DIRegistrations
            {
                Instance = instance,
                IsSingleton = true
            };
        }

        public T Resolve<T>(string tag = null)
        {
            var key = (tag ,typeof(T));

            if (_resolutions.Contains(key))
            {
                throw new Exception($"Cyclic dependency: {key.Item1} and type {key.Item2.FullName} already registered.");
            }

            _resolutions.Add(key);
            try
            {
                if (_registrations.TryGetValue(key, out var registrations))
                {
                    if (registrations.IsSingleton)
                    {
                        if (registrations.Instance == null && registrations.Factory != null)
                        {
                            registrations.Instance = registrations.Factory(this);
                        }
                        return (T)registrations.Instance;
                    }
                
                    return (T)registrations.Factory(this);
                }


                if (_parentDIContainer != null)
                {
                    return _parentDIContainer.Resolve<T>(tag);
                }
            }
            finally
            {
                _resolutions.Remove(key);
            }
            
            
            throw new Exception($"DI: Couldn't resolve type {typeof(T).FullName}");
        }

        private void Register<T>((string, Type) key, Func<DIСontainer, T> factory, bool isSingleton = false)
        {
            if (_registrations.ContainsKey(key))
            {
                throw new Exception($"DI: Factory with tag {key.Item1} and type {key.Item2.FullName} already registered.");
            }

            _registrations[key] = new DIRegistrations
            {
                Factory = c => factory(c),
                IsSingleton = isSingleton
            };
        }
        
        
    }
}
