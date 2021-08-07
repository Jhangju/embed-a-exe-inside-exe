using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace extract
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileneedtoextractandexecute = "prac1.exe";
            exec(fileneedtoextractandexecute);
            Console.ReadLine();
        }
        public static void exec(string file)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            Console.WriteLine(path);
            string path2 = path + file;
            Console.WriteLine("Extracting exe from Assembly");
            Thread.Sleep(2000);
            extractResource(file, path2);
            Console.WriteLine("Extracted.");
            Thread.Sleep(1000);
            Console.WriteLine("Running.");
            Thread.Sleep(1000);
            Process.Start(path2);
            Console.WriteLine("Executed.");
        }
        public static void extractResource(String embeddedFileName, String destinationPath)
        {
            var currentAssembly = Assembly.GetExecutingAssembly();
            var arrResources = currentAssembly.GetManifestResourceNames();
            foreach (var resourceName in arrResources)
            {
                if (resourceName.ToUpper().EndsWith(embeddedFileName.ToUpper()))
                {
                    using (var resourceToSave = currentAssembly.GetManifestResourceStream(resourceName))
                    {
                        using (var output = File.OpenWrite(destinationPath))
                            resourceToSave.CopyTo(output);
                        resourceToSave.Close();
                    }
                }
            }
        }
    }
}
