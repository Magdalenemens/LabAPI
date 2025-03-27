namespace DeltaCare.Entity
{
    public class CommonModel
    {
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool IsDeleted { get; set; }
        public CommonModel()
        {
            this.CreatedOn = DateTime.Now;
        }
    }
}
