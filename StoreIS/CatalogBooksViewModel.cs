using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using StoreIS.pages.mainWindowPages;
using System.Windows;
namespace StoreIS
{
    class CatalogBooksViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void PropertyChange(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        public ObservableCollection<Product> Products { get; set; }
        public ObservableCollection<GroupFilter> GroupFilters { get; private set; }
        public CatalogBooksViewModel()
        {
            Products = new ObservableCollection<Product>();
            GroupFilters = new ObservableCollection<GroupFilter>(DataBase.GetContext().Group.Select(p => new GroupFilter() { Group = p }));
            UpdateProductsList();
        }
        public string SearchString { get; set; } = "";
        public void UpdateProductsList()
        {
            try
            {
                Products.Clear();
                List<Group> groups = GroupFilters.Where(p => p.IsChecked).Select(p => p.Group).ToList();
                List<Product> products = DataBase.GetContext().Product.ToList();
                CountAllElements = products.Count();
                if(!String.IsNullOrWhiteSpace(SearchString))
                {
                    products = products.Where(p => p.Name.Contains(SearchString.Trim())).ToList();
                }
                if(groups.Count != 0)
                {
                    products = products.Where(p => groups.Contains(p.Group)).ToList();
                    ResetFilterActive = Visibility.Visible;
                }
                else
                {
                    ResetFilterActive = Visibility.Hidden;
                }
                switch (SelectedSort)
                {
                    case 0:
                        {
                            products = products.OrderBy(p => p.Name).ToList();
                            break;
                        }
                    case 1:
                        {
                            products = products.OrderByDescending(p => p.Name).ToList();
                            break;
                        }
                    case 2:
                        {
                            products = products.OrderBy(p => p.Price).ToList();
                            break;
                        }
                    case 3:
                        {
                            products = products.OrderByDescending(p => p.Price).ToList();
                            break;
                        }
                }
                CurrentCountElements = products.Count();
                if (products.Count != 0)
                {
                    foreach (Product product in products)
                        Products.Add(product);
                }
                else
                {
                    MessageBox.Show("Ничего не найдено", "Результат поиска");
                }

                //Products = new ObservableCollection<Product>(DataBase.GetContext().Product);
            }
            catch(Exception e)
            {
                MessageBox.Show("Ошибка: " + e.Message);
            }
        }
        public void ResetFilters()
        {
            ResetFilterActive = Visibility.Hidden;
            foreach(GroupFilter groupfilter in GroupFilters)
            {
                groupfilter.IsChecked = false;
            }

            UpdateProductsList();
        }
        public Visibility _resetFilterActive;
        public Visibility ResetFilterActive
        {
            get => _resetFilterActive;
            private set
            {
                if (value != _resetFilterActive)
                {
                    _resetFilterActive = value;
                    PropertyChange("ResetFilterActive");
                }
            }
        }
        public int SelectedSort { get; set; } = 0;

        private int _currentCountElements = 0;
        private int _countAllElements = 0;
        public int CurrentCountElements
        {
            get => _currentCountElements;
            private set
            {
                if(_currentCountElements != value)
                {
                    _currentCountElements = value;
                    PropertyChange("CurrentCountElements");
                }
            }
        }
        public int CountAllElements
        {
            get => _countAllElements;
            set
            {
                if(_countAllElements != value)
                {
                    _countAllElements = value;
                    PropertyChange("CountAllElements");
                }
            }
        }
    }
}