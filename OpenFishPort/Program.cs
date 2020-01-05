using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenFishPort
{
    class Program
    {
        static void Main(string[] args)
        {
            //Example that gets the most optimal Proxy Server
            OpenFish fish = new OpenFish();
            fish.Get();
            Tuple<string, int, string, long>[] Proxies = fish.GetResponseTimesMultiThreaded();
            Tuple<string, int, string, long> BestProxy = null;
            try
            {
                BestProxy = Proxies.Where((x) => x.Item4 != -1).OrderBy((x) => x.Item4).First();
            }
            catch (Exception)
            {
                return;
            }
            Console.WriteLine("Optimal Proxy is {0}:{1} with {2}ms response time located in {3}", BestProxy.Item1, BestProxy.Item2, BestProxy.Item4, BestProxy.Item3);
            Console.ReadLine();
            //return;
        }
    }
}
