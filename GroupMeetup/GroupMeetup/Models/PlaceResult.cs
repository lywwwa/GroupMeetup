using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupMeetup.Models
{

    //
    // Summary:
    //     Auto complete prediction.
    public class PlaceResult
    {
        public PlaceResult()
        {

        }

        //
        // Summary:
        //     Gets or sets the description.
        [JsonProperty("description")]
        public string Description { get; set; }
        //
        // Summary:
        //     Gets or sets the identifier.
        [JsonProperty("id")]
        public string ID { get; set; }
        //
        // Summary:
        //     Gets or sets the place identifier.
        [JsonProperty("place_id")]
        public string Place_ID { get; set; }
        //
        // Summary:
        //     Gets or sets the reference.
        [JsonProperty("reference")]
        public string Reference { get; set; }
    }
}
