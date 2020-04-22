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
        internal string category;
        internal int level;
        internal string word;
        internal Player player;
        internal Image drawing;
        internal List<string> usedLetters;

        internal string _category { get { return category; } set { category = value; } }
        internal int _level { get { return level; } set { this.level = value; } }
        internal string _word { get { return word; } set { word = value; } }
        internal Image _drawing{ get { return drawing; } set { drawing = value; } }
        internal List<string> _usedLetters { get { return usedLetters; } set { usedLetters = value; } }
       
        public Game()
        {
            this.player = new Player();
            this.category = "";
            this.level = 0;
            this.word = "";
            this.drawing = new Image();
            this.usedLetters = new List<string>();
        }

        public Game(Player player)
        {
            this.player = player;
            this.category = "";
            this.level = 0;
            this.word = "";
            this.drawing = new Image();
            this.usedLetters = new List<string>();
        }

        public void addUsedLetter(string letter)
        {
            usedLetters.Add(letter);
        }
    }
}
