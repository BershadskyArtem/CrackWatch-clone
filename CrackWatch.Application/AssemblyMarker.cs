using System.Reflection;

namespace CrackWatch.Application;

public class AssemblyMarker
{
    public static Assembly Marker => typeof(AssemblyMarker).Assembly;
}