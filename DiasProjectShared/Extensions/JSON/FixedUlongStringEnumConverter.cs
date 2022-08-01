using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Numerics;

namespace DiasShared.Extensions.JSON
{
    //Ulong enumları hatasız deserialize edilebilsin diye
    public class FixedUlongStringEnumConverter : StringEnumConverter
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.MoveToContentAndAssert().TokenType == JsonToken.Integer && reader.ValueType == typeof(System.Numerics.BigInteger))
            {
                // Todo: eğer !this.AllowIntegerValues ise exception at
                // https://www.newtonsoft.com/json/help/html/P_Newtonsoft_Json_Converters_StringEnumConverter_AllowIntegerValues.htm

                Type enumType = Nullable.GetUnderlyingType(objectType) ?? objectType;

                if (Enum.GetUnderlyingType(enumType) == typeof(ulong))
                {
                    BigInteger bigInteger = (BigInteger)reader.Value;

                    if (bigInteger >= ulong.MinValue && bigInteger <= ulong.MaxValue)
                    {
                        return Enum.ToObject(enumType, checked((ulong)bigInteger));
                    }
                }
            }

            return base.ReadJson(reader, objectType, existingValue, serializer);
        }
    }

    public static partial class JsonExtensions
    {
        public static JsonReader MoveToContentAndAssert(this JsonReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException();

            if (reader.TokenType == JsonToken.None)       //streamin başını skip et
                reader.ReadAndAssert();

            while (reader.TokenType == JsonToken.Comment) //eski commentleri geç
                reader.ReadAndAssert();

            return reader;
        }

        public static JsonReader ReadAndAssert(this JsonReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException();

            if (!reader.Read())
                throw new JsonReaderException("Unexpected end of JSON stream.");

            return reader;
        }
    }
}
