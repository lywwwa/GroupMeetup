using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupMeetup.Models
{
    class RetrieveResult
    {
        public RetrieveResult()
        {

        }

        //
        // Summary:
        //     Gets or sets the status.
        [JsonProperty("status")]
        public string Status { get; set; }
        //
        // Summary:
        //     Gets or sets the auto complete places.
        [JsonProperty("predictions")]
        public List<PlaceResult> PlacesResult { get; set; }
    }
}
