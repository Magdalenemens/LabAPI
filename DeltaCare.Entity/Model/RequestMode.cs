using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace DeltaCare.Entity.Model
{
    public class RequestMode
    {
        [IgnoreDataMember]
        [JsonIgnore]
        public int QueryType { get; set; }
    }
    public class CommonField
    {
        [JsonIgnore]
        public int QueryType { get; set; }

        [JsonIgnore]
        public bool IsDeleted { get; set; }

        [JsonIgnore]
        public string CreatedBy { get; set; }

        [JsonIgnore]
        public DateTime CreatedDate { get; set; }

        [JsonIgnore]
        public string UpdatedBy { get; set; }

        [JsonIgnore]
        public DateTime UpdatedDate { get; set; }
    }
}
