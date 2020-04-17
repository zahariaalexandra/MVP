using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVP_tema2_Hangman
{
    class Player
    {
        private string name;
        private int gamesPlayed;
        private int gamesLost;
        private int gamesWon;

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
