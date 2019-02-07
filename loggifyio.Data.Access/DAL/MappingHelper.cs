using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace loggifyio.Data.Access.DAL
{
    public partial class MainDbContext
    {
        public static class MappingHelper
        {
            public static IEnumerable<IMap> GetMainMappings()
            {
                var assemblyTypes = typeof(UserMap).GetTypeInfo().Assembly.DefinedTypes;
                var mappings = assemblyTypes
                    .Where(t => t.Namespace != null && t.Namespace.Contains(typeof(UserMap).Namespace))
                    .Where(t => typeof(IMap).GetTypeInfo().IsAssignableFrom(t));
                mappings = mappings.Where(x => !x.IsAbstract);
                return mappings.Select(m => (IMap) Activator.CreateInstance(m.AsType())).ToArray();
            }
        }
    }
}