using Newtonsoft.Json;

namespace Sample.Api.Models
{
    public class ApiResponse
    {
        public object Data { get; set; }

        public ApiResponseMeta Meta { get; set; }
    }

    public class ApiResponseMeta
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string DisplayMessage { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public object Error { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? ErrorCode { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Location { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string ErrorType { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string NextUrl { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? TotalCount { get; set; }
    }
}
