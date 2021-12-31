using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace OloAutomationChallengeProject.Models
{
    public class User
    {
        [JsonProperty("userId")]
        public int UserId { get; set; }
        
        [JsonProperty("id")]
        public int Id { get; set; }
        
        [JsonProperty("title")]
        public string Title { get; set; }
        
        [JsonProperty("body")]
        public string body { get; set; }
    }
}
