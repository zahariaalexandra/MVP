using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVP_tema2_Hangman
{
    public class Player
    {
        internal string _name;
        internal int _gamesPlayed;
        internal int _gamesLost;
        internal int _gamesWon;
        
        internal string name { get { return _name; } set { _name = value; } }
        internal int gamesPlayed { get { return _gamesPlayed; } set { _gamesPlayed = value; } }
        internal int gamesWon { get { return _gamesWon; } set { _gamesWon = value; } }
        internal int gamesLost { get { return _gamesLost; } set { _gamesLost = value; } }

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
