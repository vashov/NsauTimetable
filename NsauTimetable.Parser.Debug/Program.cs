using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NsauTimetable.Parser.Debug
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*** WORK START ***");

            var worker = new Worker();
            worker.Work();

            Console.WriteLine("*** WORK END ***");
        }
    }
}
