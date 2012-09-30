using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace ConsoleStarter
{
    internal class Program
    {
        private static System.Threading.AutoResetEvent stopFlag = new System.Threading.AutoResetEvent(false);

        private static void Main(string[] args)
        {
            Console.Title = "WCFService";

            Console.WriteLine("Service Started");

            ServiceHost svh = new ServiceHost(typeof(MyWcfService.MyService));

            //svh.AddServiceEndpoint(typeof(MyWcfService.IMyService),
            //    new WSHttpBinding(),
            //    "http://localhost:8666");

            svh.Open();

            Console.WriteLine("Channel open");

            stopFlag.WaitOne();

            svh.Close();
        }

        internal static void Stop()
        {
            stopFlag.Set();
        }
    }
}