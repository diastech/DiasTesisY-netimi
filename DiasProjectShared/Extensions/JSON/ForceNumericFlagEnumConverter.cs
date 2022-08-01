using Newtonsoft.Json;
using System;

namespace DiasShared.Extensions.JSON
{
    public class ForceNumericFlagEnumConverter : FixedUlongStringEnumConverter
	{
		public static bool HasFlagsAttribute(Type objectType)
		{
			return Attribute.IsDefined(Nullable.GetUnderlyingType(objectType) ?? objectType, typeof(System.FlagsAttribute));
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var enumType = value.GetType();
			if (HasFlagsAttribute(enumType))
			{
				var underlyingType = Enum.GetUnderlyingType(enumType);
				var underlyingValue = Convert.ChangeType(value, underlyingType);
				writer.WriteValue(underlyingValue);
			}
			else
			{
				base.WriteJson(writer, value, serializer);
			}
		}
	}
}
