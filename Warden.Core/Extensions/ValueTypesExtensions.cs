using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warden.Core.Extensions
{
    public static class ValueTypesExtensions
    {
        public static bool IsEmpty(this Guid value) => value == Guid.Empty;

        public static bool IsEmpty(this string value) => string.IsNullOrEmpty(value);
    }
}
