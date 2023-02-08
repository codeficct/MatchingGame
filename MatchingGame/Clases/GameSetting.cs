using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace MatchingGame.Clases
{
    public class GameSetting : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private static int score = 0;

        public int Score
        {
            get => score;
            set
            {
                if (score == value)
                    return;
                score = value;
                onPropertyChanged(nameof(Score));
                onPropertyChanged(nameof(GetScore));
            }
        }

        public int GetScore => score;

        void onPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}
