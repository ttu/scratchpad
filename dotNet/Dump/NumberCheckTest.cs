using System;
using System.Linq;

namespace ConsoleApplication1
{
    internal class NumberCheckTest
    {
        private static void Main(string[] args)
        {
            //var ints = new NumberService(typeof(int));
            var ints = new IntService();

            var laskeva = ints.IsLaskeva(new int[] { 5, 4, 3 });
            var nouseva = ints.IsNouseva(new double[] { 5.2, 6, 4, 3 });
        }
    }

    public class IntService : NumberService
    {
        public IntService()
            : base(typeof(int))
        {
        }
    }

    public class NumberService
    {
        private Action<Type> _typeCheck;

        public NumberService(Type allowed)
        {
            _typeCheck = new Action<Type>(t =>
            {
                if (t != allowed) throw new Exception("Not allowed type");
            });
        }

        public bool IsLaskeva<T>(T[] numbers)
        {
            return Check(numbers, (num, i) => { return num[i] <= num[i + 1]; });
        }

        public bool IsNouseva<T>(T[] numbers)
        {
            return Check(numbers, (num, i) => { return num[i] >= num[i++]; });
        }

        public bool IsMonotLask<T>(T[] numbers)
        {
            return Check(numbers, (num, i) => { return num[i] < num[i++]; });
        }

        public bool IsMonotNouseva<T>(T[] numbers)
        {
            return Check(numbers, (num, i) => { return num[i] > num[i++]; });
        }

        private bool Check<T>(T[] numbers, Func<dynamic[], int, bool> check)
        {
            _typeCheck(numbers.First().GetType());

            dynamic[] d = numbers.Cast<dynamic>().ToList().ToArray();

            for (int i = 0; i < numbers.Length; i++)
            {
                if (i == numbers.Length - 1)
                    return true;

                if (check(d, i))
                    return false;
            }

            return true;
        }
    }
}