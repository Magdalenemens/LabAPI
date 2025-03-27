using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text.Json.Serialization;

namespace DeltaCare.Entity.Model
{
    [AttributeUsage(AttributeTargets.Property)]
    public class IgnoreParameterAttribute : Attribute
    {
        public static bool IsIgnored(PropertyInfo property)
        {
            return property.GetCustomAttribute(typeof(IgnoreParameterAttribute)) != null;
        }
    }
    public class SiteModel : RequestMode
    {
        [IgnoreParameter]
        public int? SNO { get; set; }
        public int? SITE_DTL_ID { get; set; }
        [MaxLength(3)]
        [Range(100, 999, ErrorMessage = "Please enter 3 digit integer Number")]
        public string SITE_NO { get; set; }
        public string CMPNY_NO { get; set; }
        [IgnoreParameter]
        public string? COMPANY { get; set; }
        [MaxLength(50)]
        public string SITE_NAME { get; set; }
        public string ABRV { get; set; }
        public string SHORTNAME { get; set; }
        public string AR_SITE_NAME { get; set; }
        public string SITE_TP { get; set; }
        public string REF_SITE { get; set; }
        public string REF_SITE_S { get; set; }
        [IgnoreParameter]
        public string? REF_SITE_NAME { get; set; }
        [IgnoreParameter]
        public string? REF_SITE_SECONDARY_NAME { get; set; }
        public byte? RCVD_COL { get; set; }        
        public string CITY { get; set; }
        public string ADDRESS { get; set; }
        public string TEL { get; set; }
        public string MOBILE { get; set; }
        public string LATITUDE { get; set; }
        public string LONGITUDE { get; set; }
        public string EMAIL { get; set; }
    }
}
