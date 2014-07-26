using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Reactive.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class DataGetterTest
    {
        private static System.Threading.AutoResetEvent stopFlag = new System.Threading.AutoResetEvent(false);

        [TestMethod]
        public void GetData_Use_PublicVariable_Bad()
        {
            var getter = new DataGetter();

            getter.NewData += (e) =>
            {
                // This simulates situatin where something else changes public property
                if (e > 2)
                    getter.Multiplier = 4;

                Trace.WriteLine(Extensions.TraceMessage(e.ToString()));
            };

            getter.Ready += (s, e) =>
            {
                stopFlag.Set();
            };

            getter.GetData();

            stopFlag.WaitOne();
        }

        [TestMethod]
        public void GetData_Pass_ValueType_Good()
        {
            var getter = new DataGetter();

            getter.NewData += (e) =>
            {
                // This simulates situation where something else changes public property
                if (e > 2)
                    getter.Multiplier = 4;

                Trace.WriteLine(Extensions.TraceMessage(e.ToString()));
            };

            getter.Ready += (s, e) =>
            {
                stopFlag.Set();
            };

            getter.GetData_With_ValueType();

            stopFlag.WaitOne();
        }

        [TestMethod]
        public void GetData_Pass_ReferenceType_Bad()
        {
            var getter = new DataGetter();
            getter.DataHolder = new DataHolder() { Multipler = 1 };
            getter.NewData += (e) =>
            {
                // This simulates situation where something else changes public property
                if (e > 2)
                    getter.DataHolder.Multipler = 4;

                Trace.WriteLine(Extensions.TraceMessage(e.ToString()));
            };

            getter.Ready += (s, e) =>
            {
                stopFlag.Set();
            };

            getter.GetData_With_ReferenceType();

            stopFlag.WaitOne();
        }

        [TestMethod]
        public void GetData_Use_Object_Bad()
        {
            var getter = new DataGetter();
            var data = new DataHolder() { Multipler = 1 };

            getter.NewData += (e) =>
            {
                // This simulates situation where something else changes public property
                if (e > 2)
                    data.Multipler = 4;

                Trace.WriteLine(Extensions.TraceMessage(e.ToString()));
            };

            getter.Ready += (s, e) =>
            {
                stopFlag.Set();
            };

            getter.GetData_No_ThreadSafe2(data);

            stopFlag.WaitOne();
        }

        [TestMethod]
        public void GetData_Pass_Object_Good()
        {
            var getter = new DataGetter();
            var data = new DataHolder() { Multipler = 1 };

            getter.NewData += (e) =>
            {
                // This simulates situation where something else changes public property
                if (e > 2)
                    data.Multipler = 4;

                Trace.WriteLine(Extensions.TraceMessage(e.ToString()));
            };

            getter.Ready += (s, e) =>
            {
                stopFlag.Set();
            };

            getter.GetData_Good(data);

            stopFlag.WaitOne();
        }

        [TestMethod]
        public void Observable_Test()
        {
            var names = new List<string>
	      {
		     "George Washington",
		     "John Adams",
		     "Thomas Jefferson",
		     "James Madison",
		     "James Monroe",
		     "John Quincy Adams"
	      };

            foreach (string pName in names)
            {
                // pName.Dump();
            }

            //names.ForEach(name => name.Dump());

            IObservable<string> observable = names.ToObservable();

            observable.Subscribe<string>
            (
                pName =>
                {
                    //pName.Dump();
                }
            );

            var maxValue = 60;
            List<int> values = new List<int>(maxValue);

            for (int i = 0; i < maxValue; i++)
            {
                values.Add(i);
            }

            var results = values.ToObservable()
            .SelectMany(x => AsyncCall(x))
            .ToList().First();
        }

        public IObservable<int> AsyncCall(int a)
        {
            return Observable.Start(() =>
            {
                Trace.WriteLine(Extensions.TraceMessage(a.ToString()));
                return a;
            });
        }

        [TestMethod]
        public void CompressTest()
        {
            var s = "StartTime:13.4.201213:15:26;RunTime:00:01:24";

            var ms = new MemoryStream();
            var ds = new DeflateStream(ms, CompressionMode.Compress);

            var encoding = System.Text.Encoding.UTF8;
            var byteData = encoding.GetBytes(s);

            Trace.WriteLine("original    : {0}", s);
            ds.Write(byteData, 0, byteData.Length);
            ds.Close();
            byte[] compressed = ms.ToArray();

            Trace.WriteLine("compressed  : {0}", System.Convert.ToBase64String(compressed));

            ms = new MemoryStream(compressed);
            ds = new DeflateStream(ms, CompressionMode.Decompress);
            compressed = new byte[compressed.Length + 100];
            var offset = 0;

            while (true)
            {
                int bytesRead = ds.Read(compressed, offset, 1);
                if (bytesRead == 0) { break; }
                offset += bytesRead;
            }

            ds.Close();

            string uncompressed = encoding.GetString(compressed);

            Trace.WriteLine("uncompressed: {0}\n", uncompressed);
        }
    }
}