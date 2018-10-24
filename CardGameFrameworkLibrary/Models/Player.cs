using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGameFrameworkLibrary.Models
{
    public class Player
    {
        public Hand PlayerHand { get; set; }
        public int Bank { get; set; }
        public string Name { get; set; }

    }
}
