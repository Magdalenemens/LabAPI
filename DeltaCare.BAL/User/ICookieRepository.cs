namespace DeltaCare.BAL.User
{
    public interface ICookieRepository
    {
        public void Set(string token);
        public string Get();
        public void Delete();
    }
}
