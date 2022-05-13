using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
//using StoreIS.pages.mainWindowPages;
using System.Windows;
/*using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;*/

namespace StoreIS
{
    class CatalogBooksViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
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
                if(!String.IsNullOrWhiteSpace(SearchString))
                {
                    products = products.Where(p => p.Name.Contains(SearchString.Trim())).ToList();
                }
                if(groups.Count != 0)
                {
                    products = products.Where(p => groups.Contains(p.Group)).ToList();
                }
                if(products.Count != 0)
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
    }
}
