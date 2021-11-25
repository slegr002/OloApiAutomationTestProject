using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace OloAutomationChallengeProject.Helpers
{
    public static class ContentConversionHandler
    {
        public static object SerializeJson(object content)
        {
            return JsonConvert.SerializeObject(content);
        }

        public static T DeserializeJson<T>(string content)
        {
            return JsonConvert.DeserializeObject<T>(content);
        }
    }
}
