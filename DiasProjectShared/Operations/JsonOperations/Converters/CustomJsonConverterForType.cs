using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DiasShared.Operations.JsonOperation.Converters
{
    /// <summary>
    /// Type tiplerinin serialize - deserialize edilebilmesi için
    /// Projenin konfigürasyonunda(Startup.cs gibi) kullanılmalıdır
    /// Bu Newtonsoft değildir System.Text.Json Converterdır
    /// </summary>
    public class CustomJsonConverterForType : JsonConverter<Type>
    {
        public override Type Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options
            )
        {
            string assemblyQualifiedName = reader.GetString();
            return Type.GetType(assemblyQualifiedName);
        }

        public override void Write(
            Utf8JsonWriter writer,
            Type value,
            JsonSerializerOptions options
            )
        {
            string assemblyQualifiedName = value.AssemblyQualifiedName;
            writer.WriteStringValue(assemblyQualifiedName);
        }
    }
}
