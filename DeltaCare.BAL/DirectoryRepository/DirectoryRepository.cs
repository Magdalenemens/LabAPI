using DeltaCare.DAL;

namespace DeltaCare.BAL
{
    public class DirectoryRepository : IDirectoryRepository
    {
        private readonly IDataRepository _datarepository;
        public DirectoryRepository(IDataRepository dataRepository)
        {
            _datarepository = dataRepository;
        }

        public void GetData()
        {
            var getData = _datarepository.ExecuteQuery("getAllData", null, null);
        }

    }
}
