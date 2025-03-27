namespace DeltaCare.Entity.Model
{
    public class PageTrackRecordModel : RequestMode
    {
        public int FK_PAGE_DETAIL_ID { get; set; }
        public string SESSION_ID { get; set; }
        public string USER_ID { get; set; }
        public DateTime START_TIME { get; set; }
        public DateTime END_TIME { get; set; }
    }
    public class GetPageTrackRecordModel : RequestMode
    {
        public int FK_PAGE_DETAIL_ID { get; set; }
        public string SESSION_ID { get; set; }
        public string FULL_NAME { get; set; }
        public string USER_ID { get; set; }
        public string PAGE_NAME { get; set; }
        public string MODULE_NAME { get; set; }
        public string URL { get; set; }
        public string PAGE_DESCRIPTION { get; set; }
        public DateTime SESSION_START_TIME { get; set; }
        public DateTime SESSION_END_TIME { get; set; }
        public int SESSION_DURATION_SECONDS { get; set; }
    }
    public class PageDetail : RequestMode
    {
        public int PAGE_DETAIL_ID { get; set; }
        public string PAGE_NAME { get; set; }
        public string MODULE_NAME { get; set; }
        public string URl { get; set; }
        public string PAGE_DESCRIPTION { get; set; }
    }
}
