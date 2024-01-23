namespace DotNetCore.Services
{
    public interface IDataService
    {
        public string GetData();
    }
    public class DataService1 : IDataService
    {
        public string GetData()
        {
            return "Data from Service 1";
        }
    }
    public class DataService2 : IDataService
    {
        public string GetData()
        {
            return "Data from Service 2";
        }
    }
}
