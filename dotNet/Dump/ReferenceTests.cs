using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeneralTests
{
    [TestClass]
    public class ReferenceTests
    {
        [TestMethod]
        public void ReferenceExample()
        {
            int initialValue = 0;
            string initialString = "Initial_String";
            int newInt = 123456;
            string newString = "New_String";

            int i = initialValue;
            string st = initialString;

            // #1

            SetInt(i, newInt);
            Assert.AreEqual(i, initialValue);

            SetInt(ref i, newInt);
            Assert.AreEqual(i, newInt);

            SetString(st, newString);
            Assert.AreEqual(st, initialString);

            SetString(ref st, newString);
            Assert.AreEqual(st, newString);

            // #2 Ref is needed when changing values from

            var h1 = new RefHelper();

            h1.Index = initialValue;
            h1.Value = initialString;

            SetInt(h1.Index, newInt);
            Assert.AreEqual(h1.Index, initialValue);

            SetString(h1.Value, newString);
            Assert.AreEqual(h1.Value, initialString);

            SetInt(ref h1.Index, newInt);
            Assert.AreEqual(h1.Index, newInt);

            SetString(ref h1.Value, newString);
            Assert.AreEqual(h1.Value, newString);

            // #3  Ref is not needed when changing values from class that is passed

            h1.Index = initialValue;
            h1.Value = initialString;

            SetValuesToClass(h1, newInt, newString);
            Assert.AreEqual(h1.Index, newInt);
            Assert.AreEqual(h1.Value, newString);

            // #4 Ref is needed when replacing whole class

            h1.Index = initialValue;
            h1.Value = initialString;

            var newHelper = new RefHelper { Index = 666, Value = "This is new!" };

            ReplaceClass(h1, newHelper);
            Assert.IsFalse(h1 == newHelper);

            ReplaceClass(ref h1, newHelper);
            Assert.IsTrue(h1 == newHelper);

            // #5 Items in list can be replaced without ref

            h1 = new RefHelper();

            var list = new List<RefHelper> { h1 };
            Assert.IsTrue(list[0] == h1);
            Assert.IsFalse(h1 == newHelper);

            ReplaceItemsInList(list, newHelper);
            Assert.IsTrue(list[0] == newHelper);
            Assert.IsFalse(h1 == newHelper);

            // #6 Struct works same way as a class

            var h2 = new RefHelperStruct();

            h2.Index = initialValue;
            h2.Value = initialString;

            SetInt(h2.Index, newInt);
            SetString(h2.Value, newString);

            SetInt(ref h2.Index, newInt);
            SetString(ref h2.Value, newString);

            h2.Index = initialValue;
            h2.Value = initialString;

            SetValuesToStruct(h2, newInt, newString);
            SetValuesToStruct(ref h2, newInt, newString);
        }

        [TestMethod]
        public void ReferenceTest2()
        {
            var keeper = new ValueKeeper();
            var h1 = new RefHelper { Index = 1, Value = "test" };
            keeper.MyRef = h1;

            Assert.AreEqual(keeper.MyRef.Index, h1.Index);

            h1.Index = 2;

            Assert.AreEqual(keeper.MyRef.Index, h1.Index);

            var h2 = new RefHelper { Index = 6, Value = "test" };

            h1 = h2;

            Assert.AreNotEqual(keeper.MyRef.Index, h1.Index);

            h1 = GetHelper();

            Assert.AreNotEqual(keeper.MyRef.Index, h1.Index);
        }

        private RefHelper _helper;

        [TestMethod]
        public async Task ReferenceTest3()
        {
            var keeper = new ValueKeeper();
            _helper = new RefHelper { Index = 1, Value = "test" };
            keeper.MyRef = _helper;

            Assert.AreEqual(keeper.MyRef.Index, _helper.Index);

            _helper.Index = 2;

            Assert.AreEqual(keeper.MyRef.Index, _helper.Index);

            var h2 = new RefHelper { Index = 6, Value = "test" };

            _helper = h2;

            Assert.AreNotEqual(keeper.MyRef.Index, _helper.Index);

            _helper = await GetHelperAsync();

            Assert.AreNotEqual(keeper.MyRef.Index, _helper.Index);
        }

        public RefHelper GetHelper()
        {
            return new RefHelper { Index = 3, Value = "test" };
        }

        public Task<RefHelper> GetHelperAsync()
        {
            return Task.Factory.StartNew<RefHelper>(() =>
            {
                return new RefHelper { Index = 3, Value = "test" };
            });
        }

        public void SetInt(int i, int newInt)
        {
            i = i + newInt;
        }

        public void SetInt(ref int i, int newInt)
        {
            i = i + newInt;
        }

        public void SetString(string st, string newString)
        {
            st = newString;
        }

        public void SetString(ref string st, string newString)
        {
            st = newString;
        }

        public void SetValuesToClass(RefHelper refHelper, int newInt, string newString)
        {
            refHelper.Index = newInt;
            refHelper.Value = newString;
        }

        public void ReplaceClass(RefHelper refHelper, RefHelper newHelper)
        {
            refHelper = newHelper;
        }

        public void ReplaceClass(ref RefHelper refHelper, RefHelper newHelper)
        {
            refHelper = newHelper;
        }

        public void ReplaceItemsInList(List<RefHelper> refHelpers, RefHelper newHelper)
        {
            refHelpers[0] = newHelper;
        }

        public void SetValuesToStruct(RefHelperStruct refHelper, int newInt, string newString)
        {
            refHelper.Index = newInt;
            refHelper.Value = newString;
        }

        public void SetValuesToStruct(ref RefHelperStruct refHelper, int newInt, string newString)
        {
            refHelper.Index = newInt;
            refHelper.Value = newString;
        }
    }

    public struct RefHelperStruct
    {
        public int Index;
        public string Value;
    }

    public class RefHelper
    {
        public int Index;
        public string Value;
    }

    public class ValueKeeper
    {
        public RefHelper MyRef { get; set; }
    }
}