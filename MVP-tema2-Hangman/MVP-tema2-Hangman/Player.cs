using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVP_tema2_Hangman
{
    public class Player
    {
        private string _name;
        private int _gamesPlayed;
        private int _gamesLost;
        private int _gamesWon;
        
        public string name { get { return _name; } set { _name = value; } }
        public int gamesPlayed { get { return _gamesPlayed; } set { _gamesPlayed = value; } }
        public int gamesWon { get { return _gamesWon; } set { _gamesWon = value; } }
        public int gamesLost { get { return _gamesLost; } set { _gamesLost = value; } }

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
    }
}
