using System.Text.Json;
using System.Text.Json.Serialization;
using Avalonia;

namespace CIImage.Shared.Converters;

public class MatrixJsonConverter : JsonConverter<Matrix>
{
    public override Matrix Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartObject)
            throw new JsonException("Expected start of object.");

        double m11 = 0, m12 = 0, m13 = 0;
        double m21 = 0, m22 = 0, m23 = 0;
        double m31 = 0, m32 = 0, m33 = 0;

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndObject)
                break;

            if (reader.TokenType == JsonTokenType.PropertyName)
            {
                var propertyName = reader.GetString();
                reader.Read();
                
                switch (propertyName)
                {
                    case "M11": m11 = reader.GetDouble(); break;
                    case "M12": m12 = reader.GetDouble(); break;
                    case "M13": m13 = reader.GetDouble(); break;
                    case "M21": m21 = reader.GetDouble(); break;
                    case "M22": m22 = reader.GetDouble(); break;
                    case "M23": m23 = reader.GetDouble(); break;
                    case "M31": m31 = reader.GetDouble(); break;
                    case "M32": m32 = reader.GetDouble(); break;
                    case "M33": m33 = reader.GetDouble(); break;
                }
            }
        }

        return new Matrix(m11, m12, m13, m21, m22, m23, m31, m32, m33);
    }

    public override void Write(Utf8JsonWriter writer, Matrix value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteNumber("M11", value.M11);
        writer.WriteNumber("M12", value.M12);
        writer.WriteNumber("M13", value.M13);
        writer.WriteNumber("M21", value.M21);
        writer.WriteNumber("M22", value.M22);
        writer.WriteNumber("M23", value.M23);
        writer.WriteNumber("M31", value.M31);
        writer.WriteNumber("M32", value.M32);
        writer.WriteNumber("M33", value.M33);
        writer.WriteEndObject();
    }
}