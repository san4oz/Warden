using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;
using Warden.Core.Utils;

namespace Warden.Core.TextParsers
{
    public class CsvParser<T>
        where T : class, new()
    {
        protected T ParseEntity(string[] fields)
        {
            var entity = new T();
            var properties = entity.GetType().GetProperties();
            foreach (var property in properties)
            {
                var indexAttribute = property.GetCustomAttributes(typeof(FieldIndexAttribute), true).FirstOrDefault() as FieldIndexAttribute;
                if (indexAttribute == null)
                    continue;

                var value = fields[indexAttribute.FieldIndex];
                if (property.PropertyType == typeof(decimal))
                {
                    decimal outDecimal;
                    if (decimal.TryParse(value, out outDecimal))
                    {
                        property.SetValue(entity, outDecimal);
                    }
                }
                else if (property.PropertyType == typeof(DateTime))
                {
                    DateTime outDate;
                    if (DateTime.TryParse(value, out outDate))
                    {
                        property.SetValue(entity, outDate);
                    }
                }
                else
                {
                    property.SetValue(entity, value);
                }
            }

            return entity;
        }

        public IList<T> ParseEntities(Stream stream, Encoding encoding = null)
        {

            var result = new List<T>();

            using (var textFieldParser = new TextFieldParser(stream, encoding ?? Encoding.GetEncoding(1251)))
            {
                textFieldParser.TextFieldType = FieldType.Delimited;
                textFieldParser.SetDelimiters(";");
                textFieldParser.ReadFields();

                while (!textFieldParser.EndOfData)
                {
                    try
                    {
                        result.Add(ParseEntity(textFieldParser.ReadFields()));
                    }
                    catch (Exception ex)
                    {
#warning do something with exceptions
                    }
                }
            }

            return result;
        }

    }
}

