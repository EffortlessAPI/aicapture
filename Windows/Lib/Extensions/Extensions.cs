using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLibrary.Extensions
{
    public static class Extensions
    {
        public static string SafeToString(this object value)
        {
            if (ReferenceEquals(value, null)) return String.Empty;
            else return value.ToString();
        }
    }
}
