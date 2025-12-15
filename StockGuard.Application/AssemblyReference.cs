using System.Reflection;

namespace StockGuard.Application;

public static class AssemblyReference
{
	public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}