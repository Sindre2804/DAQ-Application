///////////////////////////////////////////////////////////////////////////////////////////
///
/// PingAppConsile: application testing the ping() connection to a node
///
/// Based on code form Microsoft MSDN about the ping() command
///
/// Version: 1.0: 8-JAN-17: NOS
///
///////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime;

//
namespace PingAppConsole
{

    class Program
    {

        static void Main(string[] args)
        {
            var periodicTimer = new PeriodicTimer(TimeSpan.FromSeconds(1));
            while (await periodicTimer.WaitForNextTickAsync())
            {
                string host, data;
                byte[] buffer;
                int timeout;
                Ping pingSender = new Ping();
                PingOptions options = new PingOptions();
                // Use the default Ttl value which is 128,
                // but change the fragmentation behavior.
                options.DontFragment = true;
                // Create a buffer of 32 bytes of data to be transmitted.
                data = "This is a Data String";
                buffer = Encoding.ASCII.GetBytes(data);
                timeout = 120;
                // Name or address of node to access
                host = "www.vg.no";
                PingReply reply = pingSender.Send(host, timeout, buffer, options);
                if (reply.Status == IPStatus.Success)
                {
                    Console.WriteLine(" Ping communication status for {0}:", host);
                    Console.WriteLine(" ------------------------------------------");
                    Console.WriteLine(" Address: {0}", reply.Address.ToString());
                    Console.WriteLine(" RoundTrip time (mSec): {0}", reply.RoundtripTime);
                    Console.WriteLine(" Time to live: {0}", reply.Options.Ttl);
                    Console.WriteLine(" Donít fragment: {0}", reply.Options.DontFragment);
                    Console.WriteLine(" Buffer size: {0}", reply.Buffer.Length);
                    Console.WriteLine(" ------------------------------------------");
                }
                else
                {
                    Console.WriteLine(" Error connecting to network address/name {0}", host);
                }
                Console.WriteLine(" Press CR or Enter to Quit the application");
                Console.ReadLine();
            }
            
        }
    }
}
///
////////////////////////////////////////////////////////////////////////////////////////