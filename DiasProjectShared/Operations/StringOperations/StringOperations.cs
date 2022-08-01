using System;
using System.IO;
using System.Linq;

namespace DiasShared.Operations.StringOperation
{
    public static class StringOperations
    {
        /// <summary>
        /// use for sanitize quotes from string
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public static string[] SanitizeQuotedOnString(string row)
        {
            string[] valuesQuotedSeperated = row.Split("\"".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(sValue => sValue.Trim()).ToArray();

            if (valuesQuotedSeperated.Length > 1)
            {
                row = String.Empty;

                for (int i = 0; i < valuesQuotedSeperated.Length; i++)
                {
                    if ((valuesQuotedSeperated[i].Length > 1) && (i % 2 == 1))
                    {
                        //Replace comma so split will work
                        valuesQuotedSeperated[i] = valuesQuotedSeperated[i].Replace(",", ";");
                        valuesQuotedSeperated[i] = valuesQuotedSeperated[i].Replace("\"", "");
                    }

                    row += valuesQuotedSeperated[i];
                }
            }

            string[] values = row.Split(",".ToCharArray(), StringSplitOptions.None).Select(sValue => sValue.Trim()).ToArray();

            //Add comma again
            for (int i = 0; i < values.Length; i++)
            {
                if (values[i] != null && (values[i] != ""))
                {
                    values[i] = values[i].Replace(";", ",");
                }
            }

            return values;
        }

        public static string FormatTimeSpanString(string input)
        {
            if ((input != null) && (input != ""))
            {
                int freqDelimeter = input.Count(f => (f == ':'));

                if (freqDelimeter == 0)
                {
                    input = "0:0:" + input;
                }
                else if (freqDelimeter == 1)
                {
                    input = "0:" + input;
                }
            }

            return input;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns>output stream</returns>
        public static Stream GenerateStreamFromString(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}
