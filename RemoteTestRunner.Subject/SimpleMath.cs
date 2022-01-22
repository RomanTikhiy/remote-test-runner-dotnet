namespace RemoteTestRunner.Subject
{
    public class SimpleMath
    {
        public int Pow(int number, int degree)
        {
            var result = 1;

            for (int i = 0; i < degree; i++)
            {
                result *= number;
            }

            return result;        
        }
    }
}
