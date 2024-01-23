using DotNetCore.Services;

namespace DotNetCore.Helpers
{
    public interface INumberHelper
    {
        public int GetNumber();
    }
    public class NumberService : INumberHelper
    {
        private int _number;
        public NumberService()
        {
            Random random = new Random();
            _number = random.Next(1, 100);
        }
        public int GetNumber()
        {
            return _number;
        }
    }
    public class NumberHelper
    {
        private readonly INumberHelper _numberHelper;
        public NumberHelper(INumberHelper numberHelper)
        {
            _numberHelper = numberHelper;
        }

        public int GetNumber()
        {
            return _numberHelper.GetNumber();
        }
    }
}
