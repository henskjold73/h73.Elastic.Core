using System.Diagnostics;
using System.Reflection;

namespace h73.Elastic.Core
{
    public static class Logging
    {
        // TODO: Logging 
        public static void WriteDebug(string msg, bool debug)
        {
            if (!debug)
            {
                return;
            }
            
            var assembly = Assembly.GetExecutingAssembly();
            var fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            var version = fvi.FileVersion;
            Debug.Print($"{assembly.FullName} {version}: {msg}");
        }
    }
}
