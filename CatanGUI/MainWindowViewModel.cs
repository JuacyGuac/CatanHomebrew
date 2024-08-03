using CatanLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatanGUI
{
    // INotifyPropertyChanged needs to be actually implemented according to its specs!
    public class MainWindowViewModel<PointImpl> : INotifyPropertyChanged where PointImpl : IPoint<PointImpl>
    {
        private IBoard<PointImpl> _board;

        public IBoard<PointImpl> Board
        {
            get { return _board; }
            set
            {
                if (_board != value)
                {
                    _board = value;
                    OnPropertyChanged(nameof(Board));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
