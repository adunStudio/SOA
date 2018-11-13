using System;
using System.IO;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.CodeAnalysis.CSharp.Scripting;


namespace KKK
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            string path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, "sample.csx");
            ScriptOptions option = ScriptOptions.Default.AddImports("System").AddImports("System.IO");
            CSharpScript.RunAsync(File.ReadAllText(path), option).Wait();

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            
            //var app = new App();
            //Application.Run(app); 
        }
    }
}
