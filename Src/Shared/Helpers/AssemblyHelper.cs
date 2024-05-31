using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Helpers;

public static class AssemblyHelper
{
	private static readonly ConcurrentDictionary<string, Assembly> Assemblies = new();

	public static Assembly GetAssembly(string assemblyName)
	{
		return Assemblies.GetOrAdd(assemblyName, Assembly.Load(assemblyName));
	}
}

public static class Assemblies
{
	public const string Application = "Application";

	public const string Domain = "Domain";

	public const string Infrastructure = "Infrastructure";
}
