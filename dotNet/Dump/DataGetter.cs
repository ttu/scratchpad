// -----------------------------------------------------------------------
// <copyright file="DataGetter.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Tests
{
    public class DataHolder
    {
        public int Multipler { get; set; }

        public string Id = "This is ID";
    }

    public delegate void IntArgs(int vl);

    public class DataGetter
    {
        //public event EventHandler<IntEventArgs> NewData;

        public event IntArgs NewData;
        public event EventHandler Ready;

        public DataHolder DataHolder { get; set; }

        public int Multiplier { get; set; }

        public DataGetter()
        {
            Multiplier = 1;
        }

        public void SendReady()
        {
            var handler = Ready;

            if (handler != null)
            {
                handler(this, null);
            }
        }

        /// <summary>
        /// This one is bad bad. It uses public Multipier property that can be changed at anytime.
        /// </summary>
        public void GetData()
        {
            var getDataTask = new Action(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    // Got data from somewhere
                    System.Threading.Thread.Sleep(100);

                    var handler = NewData;

                    if (handler != null)
                    {
                        handler(i * Multiplier);
                        //handler(this, new IntEventArgs() {Value = i * Multiplier});
                    }
                }
            });

            var getData = new Task(getDataTask);
            getData.ContinueWith(t => { SendReady(); });
            getData.Start();
        }

        /// <summary>
        /// Public Multiplier property is passed to task when its created, so changing it later won't affect task
        /// Because it's value type this works.
        /// </summary>
        public void GetData_With_ValueType()
        {
            var getDataTask = new Action<object>((mult) =>
            {
                for (int i = 0; i < 10; i++)
                {
                    // Got data from somewhere
                    System.Threading.Thread.Sleep(100);

                    var handler = NewData;

                    if (handler != null)
                    {
                        handler(i * (int)mult);
                    }
                }
            });

            var getData = new Task(getDataTask, Multiplier);
            getData.ContinueWith(t => { SendReady(); });
            getData.Start();
        }

        /// <summary>
        /// Public Multiplier property is passed to task when its created, so changing it later won't affect task
        /// Because it's reference type this doesn't work.
        /// </summary>
        public void GetData_With_ReferenceType()
        {
            var getDataTask = new Action<object>((data) =>
            {
                for (int i = 0; i < 10; i++)
                {
                    // Got data from somewhere
                    System.Threading.Thread.Sleep(100);

                    var handler = NewData;

                    if (handler != null)
                    {
                        handler(i * ((DataHolder)data).Multipler);
                    }
                }
            });

            var getData = new Task(getDataTask, DataHolder);
            getData.ContinueWith(t => { SendReady(); });
            getData.Start();
        }

        /// <summary>
        /// No local copy is created
        /// </summary>
        public void GetData_No_ThreadSafe(DataHolder data)
        {
            var getDataTask = new Action(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    // Got data from somewhere
                    System.Threading.Thread.Sleep(100);

                    var handler = NewData;

                    if (handler != null)
                    {
                        handler(i * data.Multipler);
                    }
                }
            });

            var getData = new Task(getDataTask);
            getData.ContinueWith(t => { SendReady(); });
            getData.Start();
        }

        /// <summary>
        /// No local copy is created
        /// </summary>
        public void GetData_No_ThreadSafe2(DataHolder data)
        {
            var getDataTask = new Action<object>((dataH) =>
            {
                for (int i = 0; i < 10; i++)
                {
                    // Got data from somewhere
                    System.Threading.Thread.Sleep(100);

                    var handler = NewData;

                    if (handler != null)
                    {
                        handler(i * ((DataHolder)dataH).Multipler);
                    }
                }
            });

            var getData = new Task(getDataTask, data);
            getData.ContinueWith(t => { SendReady(); });
            getData.Start();
        }

        /// <summary>
        /// Local copy of data is created, so changing data outside wont affect task
        /// </summary>
        /// <param name="data"></param>
        public void GetData_Good(DataHolder data)
        {
            var getDataTask = new Action<object>((dataH) =>
            {
                for (int i = 0; i < 10; i++)
                {
                    // Got data from somewhere
                    System.Threading.Thread.Sleep(100);

                    var handler = NewData;

                    if (handler != null)
                    {
                        handler(i * ((DataHolder)dataH).Multipler);
                    }
                }
            });

            // Create local copy of passed object
            var temp = new DataHolder() { Multipler = data.Multipler };

            var getData = new Task(getDataTask, temp);
            getData.ContinueWith(t => { SendReady(); });
            getData.Start();
        }

        /// <summary>
        /// Local copy of data is created, so changing data outside wont affect task
        /// </summary>
        /// <param name="data"></param>
        public void GetData_Good2(DataHolder data)
        {
            // Create local copy of passed object
            var temp = new DataHolder() { Multipler = data.Multipler };

            var getDataTask = new Action(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    // Got data from somewhere
                    System.Threading.Thread.Sleep(100);

                    var handler = NewData;

                    if (handler != null)
                    {
                        handler(i * temp.Multipler);
                    }
                }
            });

            var getData = new Task(getDataTask);
            getData.ContinueWith(t => { SendReady(); });
            getData.Start();
        }
    }

    public static class Extensions
    {
        public static Stopwatch sw = Stopwatch.StartNew();

        public static string TraceMessage(string info)
        {
            var traceMessage = string.Format("Thread ({0}), Id ({1}), Time ({2})\r\n{3}",
                 System.Threading.Thread.CurrentThread.ManagedThreadId,
                 0,
                 sw.ElapsedMilliseconds,
                 info);

            return traceMessage;
        }
    }
}