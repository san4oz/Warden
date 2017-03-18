using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warden.ExternalDataProvider.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class FieldIndexAttribute : Attribute
    {
        public int FieldIndex { get; set; }
        public FieldIndexAttribute(int index)
        {
            this.FieldIndex = index;
        }
    }
}
