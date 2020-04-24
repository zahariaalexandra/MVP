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
        internal int id;
        internal string category;
        internal int level;
        internal string word;
        internal Player player;
        internal List<string> usedLetters;
        internal int progress;

        internal string _id { get { return category; } set { category = value; } }
        internal string _category { get { return category; } set { category = value; } }
        internal int _level { get { return level; } set { this.level = value; } }
        internal string _word { get { return word; } set { word = value; } }
        internal int _progress{ get { return progress; } set { progress = value; } }
        internal List<string> _usedLetters { get { return usedLetters; } set { usedLetters = value; } }
       
        public Game()
        {
            this.id = 0;
            this.player = new Player();
            this.category = "";
            this.level = 0;
            this.word = "";
            this.progress = 0;
            this.usedLetters = new List<string>();
        }

        public Game(Player player)
        {
            this.id = 0;
            this.player = player;
            this.category = "";
            this.level = 0;
            this.word = "";
            this.progress = 0;
            this.usedLetters = new List<string>();
        }

        public void addUsedLetter(string letter)
        {
            usedLetters.Add(letter);
        }
    }
}
