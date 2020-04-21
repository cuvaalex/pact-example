using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace APIConsumer.Models
{
    public class User
    {
        [JsonPropertyName("userid")]
        public Guid UserId { get; set; }
        [JsonPropertyName("firstname")]
        public String FirstName { get; set; }
        [JsonPropertyName("lastname")]
        public String LastName { get; set; }
    }
}
