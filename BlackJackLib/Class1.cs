using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardGameFrameworkLibrary.Models;

namespace BlackJackLib
{
    public class BlackJack
    {
        List<Player> players;

        private void GetContenders()
        {
            List<Player> contenders = new List<Player>();

            foreach (var player in players)
            {
                if (!player.HasFolded)
                {
                    contenders.Add(player);
                }
            }
        }

        private void CheckHighest(List<Player> players)
        {
            foreach
        }
    }
}
