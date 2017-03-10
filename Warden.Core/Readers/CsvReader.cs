using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;

namespace Warden.Core.Readers
{
    public class CsvReader
    {
        public List<T> ExtractEntities<T>(Stream stream, Func<string[], T> converter)
            where T : new()            
        {
            var result = new List<T>();

            using (TextFieldParser parser = new TextFieldParser(stream, Encoding.GetEncoding(1251)))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(";");
                parser.ReadFields();

                while(!parser.EndOfData)
                {
                    result.Add(converter(parser.ReadFields()));
                }
            }

            return result;
        }
    }
}
