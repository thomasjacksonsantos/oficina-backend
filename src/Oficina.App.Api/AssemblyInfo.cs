using System.Reflection;

namespace Oficina.App.Api;

public class AssemblyInfo
{
    public static Assembly GetApiAssembly() => typeof(AssemblyInfo).Assembly;
}