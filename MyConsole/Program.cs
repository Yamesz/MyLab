using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using z01_LINQ;

namespace MyConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var section = (Hashtable)ConfigurationManager.GetSection("PowershellSnapIns");
            var a = (string)section["SnapIn1"];
        }
    }
}
