using System;
using System.Linq;
using System.Reflection;

namespace DynamicLoading
{
    class DynamicLoading
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Assembly asm = Assembly.LoadFrom(@"D:\STUDY\workspace_dotnet\Dotnet\DynamicLoadingLibrary\bin\Debug\DynamicLoadingLibrary.dll");
            Type[] types = asm.GetExportedTypes();
            string classFullName = string.Empty;
            foreach(Type type in types)
            {
                Console.WriteLine(type.FullName);
                classFullName = type.FullName;
            }

            //Type type = asm.GetType(classFullName);
            dynamic obj = Activator.CreateInstance(types[0]);

            int result = obj.plus(1, 2);
            Console.WriteLine(result);

            result = obj.minus(2, 3);
            Console.WriteLine(result);

        }
    }
}
