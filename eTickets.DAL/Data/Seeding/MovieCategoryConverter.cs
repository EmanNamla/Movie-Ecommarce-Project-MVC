using eTickets.DAL.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace eTickets.DAL.Data.DataSeed
{
    public class MovieCategoryConverter : JsonConverter<MovieCategory>
    {
        public override MovieCategory Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {

            if (reader.TokenType == JsonTokenType.String)
            {
                string value = reader.GetString();
                if (Enum.TryParse<MovieCategory>(value, true, out var result))
                    return result;
            }
            return default; 
        }

        public override void Write(Utf8JsonWriter writer, MovieCategory value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
