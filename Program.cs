using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dice_Game
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Player One, please enter your name");
            var name1 = Console.ReadLine();
            Login.CheckUser(name1);
            Console.WriteLine("Player Two, please enter your name");
            var name2 = Console.ReadLine();
            Login.CheckUser(name2);
            Player playerOne = new Player(name1, 0);
            Player playerTwo = new Player(name2, 0);
            Dice dice1 = new Dice();
            Dice dice2 = new Dice();
            Dice dice3 = new Dice();
            Dice dice4 = new Dice();
            Console.WriteLine("Press enter to start the game");
            Console.ReadLine();
            Console.WriteLine("######################################################################");
            Console.WriteLine("######################################################################");

            /* Each game is 5 rounds. Highest score is winner.
               If the total is an even number, an additional 10 points are added to their score.
               If the total is an odd number, 5 points are subtracted from their score.
               If they roll a double, they get to roll one extra die and get the number of points rolled
               added to their score.
            */
            for (int i = 1; i < 6; i++)
            {
                Console.WriteLine("Round " + i);
                dice1.rollDice();
                dice2.rollDice();
                playerOne.addScore(dice1.Value + dice2.Value);
                Console.WriteLine("{0} got {1} and {2}!!", playerOne.Name, dice1.Value, dice2.Value);
                playerOne.checkRules(dice1.Value, dice2.Value);
                dice3.rollDice();
                dice4.rollDice();
                playerTwo.addScore(dice3.Value + dice4.Value);
                Console.WriteLine("{0} got {1} and {2}!!", playerTwo.Name, dice3.Value, dice4.Value);
                playerTwo.checkRules(dice3.Value, dice4.Value);
                Console.WriteLine("The scores after round {0} are...", i);
                if (playerOne.Score < 0)
                {
                    playerOne.setScore(0);
                }
                if (playerTwo.Score < 0)
                {
                    playerTwo.setScore(0);
                }
                Console.WriteLine("{0} has {1} points!", playerOne.Name, playerOne.Score);
                Console.WriteLine("{0} has {1} points!", playerTwo.Name, playerTwo.Score);
                Console.WriteLine("######################################################################");
                Console.WriteLine("Press Enter to start next round");
                Console.ReadLine();
            }

            // who is the winner ?
            if (playerOne.Score == playerTwo.Score)
            {
                Console.WriteLine("It's a Draw");

                // TODO go to sudden death one die each
            }

            if (playerOne.Score > playerTwo.Score)
            {
                Console.WriteLine("{0} wins!!!!!", playerOne.Name);
            }

            if (playerOne.Score < playerTwo.Score)
            {
                Console.WriteLine("{0} wins!!!!!", playerTwo.Name);
            }

            // add the scores to the database 
            DB.WriteScore(playerOne.Name, playerOne.Score);
            DB.WriteScore(playerTwo.Name, playerTwo.Score);

            // view high scores
            Console.WriteLine();
            Console.WriteLine("######################################################################");
            DB.ReadScore();

            // keep window open
            Console.WriteLine("Press enter to close ...");
            Console.ReadLine();

        }
    }
}
