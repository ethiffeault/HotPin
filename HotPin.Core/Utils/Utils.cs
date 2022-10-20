using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace HotPin
{
    public static class Utils
    {
        public static List<Type> GetTypeInAssemblies(Type baseType, bool includeAbstract)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            return GetTypeInAssemblies(baseType, includeAbstract, assemblies.ToArray());
        }

        public static List<Type> GetTypeInAssemblies(Type baseType, bool includeAbstract, params Assembly[] assemblies)
        {
            List<Type> result = new List<Type>();

            foreach (Assembly assembly in assemblies)
            {
                Type[] assemblyTypes = assembly.GetTypes();
                foreach (Type assemblyType in assemblyTypes)
                {
                    if (assemblyType.IsSubclassOf(baseType))
                    {
                        if (includeAbstract == false && assemblyType.IsAbstract)
                            continue;
                        result.Add(assemblyType);
                    }
                }
            }
            return result;
        }

        public static void StartProcess(string file)
        {
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = file,
                UseShellExecute = true
            };
            Process.Start(psi);
        }
    }
}
