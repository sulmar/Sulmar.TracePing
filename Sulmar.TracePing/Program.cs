using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using System.Threading;
using NLog;

namespace Sulmar.TracePing
{
    class Program
    { 
        private static Logger logger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            while (true)
            {
                Send("8.8.8.8");
            }

        }

        private static void Send(string address)
        {
            try
            {

                var ping = new Ping();

                var reply = ping.Send(address);


                if (reply.Status == IPStatus.Success)
                {
                    logger.Trace($"Reply from {reply.Address}: status={reply.Status} time={reply.RoundtripTime} TTL={ reply.Options.Ttl}");
                }
                else if (reply.Status == IPStatus.TimedOut)
                {
                    logger.Error("Request timed out.");
                }
                else
                {
                    logger.Error(reply.Status);
                }
            }

            catch (PingException e)
            {
                logger.Trace(e.Message);
            }

            Thread.Sleep(1000);
        }
    }
}
