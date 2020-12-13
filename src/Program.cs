using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace starter
{
    class Program
    {
        static void Main(string[] args)
        {
            var found = false;
            foreach (var p in Process.GetProcesses().OrderBy(p => p.ProcessName))
            {
                if (p.ProcessName.ToLower().Contains("trojan"))
                {
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                var current_path = Process.GetCurrentProcess().MainModule?.FileName;
                if (current_path != null)
                {
                    var psi = new ProcessStartInfo("trojan.exe")
                    {
                        WorkingDirectory = Path.GetDirectoryName(current_path) ?? ".",
                        CreateNoWindow = true,
                        WindowStyle = ProcessWindowStyle.Hidden,
                    };
                    Process.Start(psi);
                    Console.WriteLine("trojan started");
                }
                else
                {
                    Console.WriteLine("can not get cwd");
                    Console.ReadLine();
                }
            }
            else
            {
                Console.WriteLine("trojan already running");
            }
        }
    }
}