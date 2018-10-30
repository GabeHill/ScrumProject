using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Poker.Controller;
//using Poker

namespace ScrumTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            PokerLogic pl = new PokerLogic();
            pl.Setup();
        }
    }
}
