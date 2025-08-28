using System.Collections.Generic;
using System.ComponentModel;

namespace assignment_2425
{
    public class Recipe : INotifyPropertyChanged
    {
        public string Name { get; set; }
        public string Ingredients { get; set; }
        public string Category { get; set; }
        public string City { get; set; }

        private bool isSaved;
        public bool IsSaved
        {
            get => isSaved;
            set
            {
                if (isSaved != value)
                {
                    isSaved = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsSaved)));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

