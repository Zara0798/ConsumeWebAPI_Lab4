using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ConsumeWebAPI.Models
{
    public class Location
    {
        [JsonPropertyName("post code")]
        public string PostCode { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("country abbreviation")]
        public string CountryAbbreviation { get; set; }

        [JsonPropertyName("places")]
        public List<Place> Places { get; set; }
    }

    public class Place
    {
        [JsonPropertyName("place name")]
        public string PlaceName { get; set; }

        [JsonPropertyName("longitude")]
        public string Longitude { get; set; }

        [JsonPropertyName("latitude")]
        public string Latitude { get; set; }

        [JsonPropertyName("state")]
        public string State { get; set; }

        [JsonPropertyName("state abbreviation")]
        public string StateAbbreviation { get; set; }
    }
}
