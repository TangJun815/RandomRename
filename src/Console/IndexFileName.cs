using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ConsoleApp
{
    public static class IndexFileName
    {
        private static int index = 0;
        public static string GetNexFileName(string sourceFileName)
        {
            return $"{++index}{Path.GetExtension(sourceFileName)}";
        }
    }
}
