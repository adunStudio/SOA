using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using System.Windows.Forms;

namespace SOA
{
    class StartMain
    {
        [STAThread]
        static void Main(string[] args)
        {
            ScriptOptions option = ScriptOptions.Default;

            List<Assembly> assemblys = new List<Assembly>
            {
                typeof(object).GetTypeInfo().Assembly,
                typeof(System.Linq.Enumerable).GetTypeInfo().Assembly,
                typeof(System.Windows.Forms.Application).GetTypeInfo().Assembly,
                typeof(SOA.SOAApp).GetTypeInfo().Assembly
            };

            List<string> namespaces = new List<string>
            {
                "System",
                "System.IO",
                "System.Collections.Generic",
                "System.Windows.Forms",
                "System.Drawing",
                "System.Drawing.Imaging",
                "SOA",
                "SOA.Extension"
            };

            foreach (Assembly assemble in assemblys)
            {
                option = option.WithReferences(assemble);
            }

            foreach (string name in namespaces)
            {
                option = option.AddImports(name);
            }

            //string path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, "sample.csx");
            //string path = "C:/Users/adunstudio/Desktop/Spear_of_Adun/SOA/Sample/1_sample_keyboard.csx"; // keyboad 
            string path = "C:/Users/adunstudio/Desktop/Spear_of_Adun/SOA/Sample/1_sample_mouse.csx";    // mouse

            var app = new SOAApp();

            Console.WriteLine("Program Start...");
            CSharpScript.RunAsync(File.ReadAllText(path), option, app).Wait();

            app.Run();
        }
    }
}
