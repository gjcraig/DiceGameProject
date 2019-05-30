using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dice_Game
{
    class Player
    {
        private static Random rnd;
        private string name;
        private int score;
        private int bonus;

        public Player(string name, int score)
        {
            this.name = name;
            this.score = score;
            rnd = new Random();
        }

        public string Name
        {
            get
            {
                return this.name;
            }
        }

        public int Score
        {
            get
            {
                return this.score;
            }
        }

        public void setScore(int x)
        {
            this.score = x;
        }

        public void addScore(int x)
        {
            this.score += x;
        }

        public void checkRules(int a, int b)
        {
            // if the total is an even number, an additional 10 points are added to their score.
            if ((a + b) % 2 == 0)
            {
                this.score += 10;
                Console.WriteLine("{0} has an even total, they get 10 bonus points", this.name);
            }
            // if the total is an odd number, 5 points are subtracted from their score.
            if ((a + b) % 2 == 1)
            {
                this.score -= 5;
                Console.WriteLine("{0} has an odd total, they lose 5 points", this.name);
            }
            // If they roll a double, they get to roll one extra die and get the number of points rolled
            // added to their score
            if (a == b)
            {
                Console.WriteLine("{0} rolled a double! They get to roll the bonus die!", this.Name);
                bonus = rnd.Next(1, 7);
                this.addScore(bonus);
                Console.WriteLine("{0} got {1} bonus points!", this.Name, bonus);
            }

        }
    }
}
