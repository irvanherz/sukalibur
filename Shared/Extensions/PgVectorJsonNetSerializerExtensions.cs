using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using NodaTime;
using NodaTime.Utility;
using Pgvector;
using static Dapper.SqlMapper;

namespace Sukalibur.Shared.Extensions
{
    public static class PgVectorJsonNetSerializerExtensions
    {
        public static JsonSerializerSettings AddPgVectorJsonConverter(this JsonSerializerSettings settings)
        {
            Preconditions.CheckNotNull(settings, nameof(settings));
            settings.Converters.Add(new PgVectorJsonConverter());
            return settings;
        }

        public static JsonSerializer AddPgVectorJsonConverter(this JsonSerializer serializer)
        {
            Preconditions.CheckNotNull(serializer, nameof(serializer));
            serializer.Converters.Add(new PgVectorJsonConverter());
            return serializer;
        }

        internal static class Preconditions
        {
            internal static T CheckNotNull<T>(T argument, string paramName) where T : class
                => argument ?? throw new ArgumentNullException(paramName);

            internal static void CheckArgument(bool expression, string parameter, string message)
            {
                if (!expression)
                {
                    throw new ArgumentException(message, parameter);
                }
            }

            internal static void CheckData<T>(bool expression, string messageFormat, T messageArg)
            {
                if (!expression)
                {
                    string message = string.Format(messageFormat, messageArg);
                    throw new InvalidNodaDataException(message);
                }
            }
        }
    }
    public class PgVectorJsonConverter : JsonConverter<Vector>
    {
        public override Vector? ReadJson(JsonReader reader, Type objectType, Vector? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return null;
            }

            if (reader.TokenType == JsonToken.StartArray)
            {
                var values = serializer.Deserialize<List<float>>(reader);
                if (values != null)
                {
                    return new Vector(values.ToArray());
                }
            }

            throw new JsonSerializationException("Invalid JSON for Vector.");
        }

        public override void WriteJson(JsonWriter writer, Vector? value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
            }
            else
            {
                writer.WriteStartArray();
                foreach (var item in value.ToArray())
                {
                    serializer.Serialize(writer, item);
                }
                writer.WriteEndArray();
            }
        }
    }
}
