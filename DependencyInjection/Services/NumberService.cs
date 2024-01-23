namespace DotNetCore.Services
{
    public interface INumberServiceTransient
    {
        public int GetNumber();
    }
    public interface INumberServiceScoped
    {
        public int GetNumber();
    }
    public interface INumberServiceSingleton
    {
        public int GetNumber();
    }
    public class NumberServiceTransient : INumberServiceTransient
    {
        private int _number;
        public NumberServiceTransient()
        {
            Random random = new Random();
            _number = random.Next(1, 100);
        }
        public int GetNumber()
        {
            return _number;
        }
    }

    public class NumberServiceScoped : INumberServiceScoped
    {
        private int _number;
        public NumberServiceScoped()
        {
            Random random = new Random();
            _number = random.Next(1, 100);
        }
        public int GetNumber()
        {
            return _number;
        }
    }

    public class NumberServiceSingleton : INumberServiceSingleton
    {
        private int _number;
        public NumberServiceSingleton()
        {
            Random random = new Random();
            _number = random.Next(1, 100);
        }
        public int GetNumber()
        {
            return _number;
        }
    }
}
