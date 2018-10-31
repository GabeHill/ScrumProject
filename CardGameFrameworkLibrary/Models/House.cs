﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGameFrameworkLibrary.Models
{
    public class House : Player
    {
        public House()
        {
            Name = "CPU";
            Bank = 2000;
            Bet = 50;
        }
        public bool HasToDraw()
        {
            if (HandValue < 17)
            {
                return true;
            }
            return false;
        }
    }
}
