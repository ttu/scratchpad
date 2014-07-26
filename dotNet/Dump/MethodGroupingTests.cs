using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DrinkTracker.Test
{
    [TestClass]
    public class MethodGroupingTests
    {
        [TestMethod]
        public void TestGroup()
        {
            var mm = new MyInteger(3);

            var restult = mm.Double().Multiply(3).Add(4).Calc((a, b) => { return 2 + b + a; }, 3).Value;
        }

        public class MyInteger
        {
            public int Value { get; private set; }

            public MyInteger(int val)
            {
                Value = val;
            }

            public MyInteger Calc(Func<int, int, int> calc, int a)
            {
                int val = calc(a, Value);
                return new MyInteger(val);
            }

            public MyInteger Double()
            {
                return new MyInteger(Value * 2);
            }

            public MyInteger Add(int a)
            {
                return new MyInteger(Value + a);
            }

            public MyInteger Multiply(int a)
            {
                return new MyInteger(Value * a);
            }
        }
    }
}