using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Text;

namespace Core.Extensions {
    // Taken from https://github.com/engindemirog/NetCoreBackend/blob/master/Core/Extensions/ErrorDetails.cs
    public class ErrorDetails {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public override string ToString() {
            return JsonConvert.SerializeObject(this, new JsonSerializerSettings {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
        }
    }
}
