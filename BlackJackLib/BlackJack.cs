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
            CheckHighest(contenders);
        }

        private void CheckHighest(List<Player> players)
        {
            Dictionary<Player, int> pairs = new Dictionary<Player, int>();
            int low = 2;
            bool HasWinner = false;
            while (!HasWinner)
            {
                if (players.Count > 1)
                {
                    foreach (var player in players)
                    {
                        if (players.Count > 1)
                        {
                            if (player.HandValue < low)
                            {

                                players.Remove(player);
                                break;
                            }
                        }
                    }
                }
                else
                {
                    HasWinner = true;
                }
                low++;
            }
        }
    }
}