using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace OloAutomationChallengeProject.Helpers
{
    public static class ContentConversionHandler
    {
        /// <summary>
        /// Converts content from a C# object to a JSon string
        /// </summary>
        /// <param name="content"></param>
        /// <returns>string</returns>
        public static string SerializeJson(object content)
        {
            return JsonConvert.SerializeObject(content);
        }

        /// <summary>
        /// Converts a Json string to a C# object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="content"></param>
        /// <returns></returns>
        public static T DeserializeJson<T>(string content)
        {
            return JsonConvert.DeserializeObject<T>(content);
        }
    }
}
