using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace Services.Mappers
{
    class ProfileAutoMapperHelper
    {
        public static IEnumerable<MapperModel> LoadStandardMappings(Assembly rootAssembly)
        {
            var types = rootAssembly.GetExportedTypes();

            var mapsFrom = (
                from type in types
                from instance in type.GetInterfaces()
                where
                    instance.IsGenericType && instance.GetGenericTypeDefinition() == typeof(IAutoMapperFrom) &&
                    !type.IsAbstract &&
                    !type.IsInterface
                select new MapperModel
                {
                    Source = type.GetInterfaces().First().GetGenericArguments().First(),
                    Destination = type
                }).ToList();

            return mapsFrom;
        }

        public static IEnumerable<ICustomAutoMapper> LoadCustomMappings(Assembly rootAssembly)
        {
            var types = rootAssembly.GetExportedTypes();

            var mapsFrom = (
                from type in types
                from instance in type.GetInterfaces()
                where
                    typeof(ICustomAutoMapper).IsAssignableFrom(type) &&
                    !type.IsAbstract &&
                    !type.IsInterface
                select (ICustomAutoMapper)Activator.CreateInstance(type)).ToList();

            return mapsFrom;
        }
    }
}
