using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace DiasShared.Operations.JsonOperation
{
    public static class JsonOperations
    {
        /// <summary>        
        /// </summary>
        /// <param name="value"> will be serialized Json</param>
        /// <param name="stream">output stream</param>
        public static void SerializeJsonIntoStream(object value, Stream stream)
        {
            using var stremw = new StreamWriter(stream, new UTF8Encoding(false), 1024, true);
            using var jsonw = new JsonTextWriter(stremw) { Formatting = Formatting.None };
            var js = new JsonSerializer();
            js.Serialize(jsonw, value);
            jsonw.Flush();
        }
    }
}
