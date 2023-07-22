using System.Reflection;

namespace CrackWatch.Infrastructure;

public class AssemblyMarker
{
    public static Assembly Marker => typeof(AssemblyMarker).Assembly;
}