using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInjection
{
    public class Container
    {
        private Dictionary<Type, Type> _containerData = new Dictionary<Type, Type>();

        public void Register<TType, TImplementation>()
        {
            _containerData.Add(typeof(TType), typeof(TImplementation));
        }

        public TType GetInstance<TType>()
        {
            var type = typeof(TType);
            if (type.GetConstructors().Length > 1)
                throw new Exception($"The type '{type.FullName}' should implement only one constructor");

            ConstructorInfo ctor = type.GetConstructors()[0];
            ParameterInfo[] parameters = ctor.GetParameters();
            object[] constructorArgs = new object[parameters.Length];

            for (int i = 0; i < parameters.Length; i++)
            {
                var parameterType = parameters[i].ParameterType;

                if (!_containerData.ContainsKey(parameterType))
                    throw new Exception($"Can't find implementation for the type '{parameterType.FullName}'");

                var obj = Activator.CreateInstance(_containerData[parameterType]);
                if (obj != null)
                    constructorArgs[i] = obj;
            }

            return (TType)ctor.Invoke(constructorArgs);
        }
    }
}
