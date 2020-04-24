using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace MVP_tema2_Hangman
{
    public class Game
    {
        internal int _id;
        internal string _category;
        internal int _level;
        internal string _word;
        internal Player _player;
        internal List<string> _usedLetters;
        internal int _progress;

        public int id { get { return _id; } set { _id = value; } }
        public string category { get { return _category; } set { _category = value; } }
        public int level { get { return _level; } set { this._level = value; } }
        public string word { get { return _word; } set { _word = value; } }
        public int progress{ get { return _progress; } set { _progress = value; } }
        public List<string> usedLetters { get { return _usedLetters; } set { _usedLetters = value; } }
        public Player player { get { return _player; } set { _player = value; } }

        public Game()
        {
            id = 0;
            player = new Player();
            category = "";
            level = 0;
            word = "";
            progress = 0;
            usedLetters = new List<string>();
        }

        public Game(Player player)
        {
            id = 0;
            this.player = player;
            category = "";
            level = 0;
            word = "";
            progress = 0;
            usedLetters = new List<string>();
        }

        public void addUsedLetter(string letter)
        {
            usedLetters.Add(letter);
        }
    }
}
