using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warden.Core.Utils
{
    public static class Logger
    {
        private const string logFilePath = @"data\logs";

        public static void Add(string message)
        {
            using (var sr = new StreamWriter(string.Format(@"{0}\{1}", AppDomain.CurrentDomain.BaseDirectory, logFilePath), true))
            {
                sr.WriteLine("DateTime: {0} | Error: {1}", DateTime.Now, message);
            }
        }
    }
}
