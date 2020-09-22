using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using IAS.Audit.Abstractions;
using Newtonsoft.Json;

namespace IAS.Audit.Sinks.UriSink
{
    public class UriSink : IAuditSink
    {
        private readonly string _uri;
        readonly HttpClient _httpClient;
        
        public UriSink(string uri)
        {
            _uri = uri;
            _httpClient= new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        }

        public async Task Dispatch(AuditEvent auditEvent)
        {
            var content = new StringContent(JsonConvert.SerializeObject(auditEvent), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_uri, content);
            if (!response.IsSuccessStatusCode)
                throw new Exception($"Failed to write audit event to uri - {_uri}");

        }
    }
}
