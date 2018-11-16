using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using System.Windows.Forms;

namespace KKK
{
    class Program
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
                typeof(KKK.KKKApp).GetTypeInfo().Assembly
            };

            List<string> namespaces = new List<string>
            {
                "System",
                "System.IO",
                "System.Collections.Generic",
                "System.Windows.Forms",
                "System.Drawing",
                "System.Drawing.Imaging",
                "KKK",
                "KKK.Extension"
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
            string path = "C:/Users/adunstudio/Desktop/kakaoKungKuotta/KKK/sample.csx";
            var app = new KKKApp();

            CSharpScript.RunAsync(File.ReadAllText(path), option, app).Wait();

            app.Run();
        }
    }
}
