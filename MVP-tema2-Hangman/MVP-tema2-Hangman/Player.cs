using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVP_tema2_Hangman
{
    public class Player
    {
        internal string name;
        internal int gamesPlayed;
        internal int gamesLost;
        internal int gamesWon;
        
        internal string _name { get { return name; } set { name = value; } }

        public Player()
        {
            name = "";
            gamesPlayed = 0;
            gamesLost = 0;
            gamesWon = 0;
        }

        public Player(string name)
        {
            this.name = name;
            gamesPlayed = 0;
            gamesLost = 0;
            gamesWon = 0;
        }

        public void addGame()
        {
            gamesPlayed++;
        }

        public void addLostGame()
        {
            gamesLost++;
        }

        public void addWonGame()
        {
            gamesWon++;
        }
    }
}
