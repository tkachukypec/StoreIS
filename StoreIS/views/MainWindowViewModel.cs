using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using StoreIS.pages.mainWindowPages;

namespace StoreIS.views
{
    class MainWindowViewModel:INotifyPropertyChanged
    {
        private void PropertyChange(string property) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        public event PropertyChangedEventHandler PropertyChanged;
        public Employee employee { get; private set; }
        public ObservableCollection<Page> PageCollection { get; private set; }
        private Page _currentPage;
        public Page CurrentPage
        {
            get => _currentPage;
            set
            {
                if(_currentPage != value)
                {
                    _currentPage = value;
                    PropertyChange("CurrentPage");
                }
            }
        }
        public MainWindowViewModel(Employee _employee)
        {
            employee = _employee;
            PageCollection = new ObservableCollection<Page>();
            PageCollection.Add(new Page1());
            /*if(employee.PostId != 1)
            {
                PageCollection.Add(new Page2());
            }
            if(employee.PostId == 3)
            {
                PageCollection.Add(new Page3());
            }*/

            CurrentPage = PageCollection[0];
        }
    }

}
