using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Helper
{
    public static class JsonExtensions
    {
        public static JObject FromJson(this object json)
        {
            if(json == null)
            {
                return null;
            }

            return JsonConvert.DeserializeObject<JObject>(json.ToString());
        }
    }
}
