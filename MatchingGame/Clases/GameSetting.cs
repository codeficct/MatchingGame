using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace MatchingGame.Clases
{
    public class GameSetting : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private static int level = 0;

        public int Level
        {
            get => level;
            set
            {
                if (level == value)
                    return;
                level = value;
                onPropertyChanged(nameof(Level));
                onPropertyChanged(nameof(GetLevel));
            }
        }

        public int GetLevel => level;

        void onPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}
