namespace DeltaCare.Entity.Model
{
    public class UserModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string Role { get; set; }
        public string Token { get; set; } = string.Empty;
    }
}
