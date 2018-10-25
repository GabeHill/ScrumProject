using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGameFrameworkLibrary.Models
{
    public class House : Player
    {
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
