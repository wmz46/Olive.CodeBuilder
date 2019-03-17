using System;
using System.CodeDom.Compiler;
using System.IO;
using System.Text;
using Microsoft.VisualStudio.TextTemplating;

namespace Olive.CodeBuilder.Core
{
    public class CodeBuilder
    {
        public string Process(TableHost host)
        {
           
            Engine engine = new Engine();

            //Transform the text template.
            //Read the text template.


            var content = File.ReadAllText(host.TemplateFileValue);
            

            
            string output = engine.ProcessTemplate(content, host);
            var sb = new StringBuilder();
            if (host.Errors.HasErrors)
            {
                foreach (CompilerError err in host.Errors)
                {
                    sb.AppendLine(err.ToString());
                }
                output = output + Environment.NewLine + sb.ToString();
            }
            return output;
        }
    }
}
