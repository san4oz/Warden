using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warden.ExternalDataProvider.Attributes;
using Microsoft.VisualBasic.FileIO;

namespace Warden.ExternalDataProvider.Parsers
{
    public class BaseCsvParser<T>
        where T : class, new()
    {
        protected T ParseEntity(string[] fields)
        {
            var entity = new T();
            var properties = entity.GetType().GetProperties();
            foreach(var property in properties)
            {
                var indexAttribute = property.GetCustomAttributes(typeof(FieldIndexAttribute), true).FirstOrDefault() as FieldIndexAttribute;
                if (indexAttribute == null)
                    continue;

                entity.GetType().GetProperty(property.Name).SetValue(entity, fields[indexAttribute.FieldIndex]);
            }

            return entity;
        }

        public IList<T> ParseEntities(Stream stream, Encoding encoding = null)
        {
            var result = new List<T>();

            using (var textFieldParser = new TextFieldParser(, encoding ?? Encoding.GetEncoding(1251)))
            {
                textFieldParser.TextFieldType = FieldType.Delimited;
                textFieldParser.SetDelimiters(";");
                textFieldParser.ReadFields();

                while(!textFieldParser.EndOfData)
                {
                    result.Add(ParseEntity(textFieldParser.ReadFields()));
                }
            }

            return result;
        }
    }
}
