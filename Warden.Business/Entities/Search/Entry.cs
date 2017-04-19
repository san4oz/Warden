using System.Collections.Generic;

namespace Warden.Business.Entities.Search
{
    public class Entry
    {
        public string Id { get; set; }

        public float Score { get; set; }

        public Dictionary<string, object> Fields { get; set; }

        public object GetValueOrDefault(string fieldName)
        {
            object value = null;
            Fields.TryGetValue(fieldName, out value);
            return value;
        }

        public Entry()
        {
            this.Fields = new Dictionary<string, object>();
        }
    }
}
