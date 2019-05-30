using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dice_Game
{
    class Dice
    {
        private static Random rnd;
        private int value;

        public Dice()
        {
            rnd = new Random();
            rollDice();

        }
        public Dice(int value)
        {
            this.value = value;
            rnd = new Random();
        }

        public int Value
        {
            get
            {
                return this.value;
            }
            
            
        }

        public void rollDice()
        {
            this.value = rnd.Next(1, 7);
        }
    }
}
