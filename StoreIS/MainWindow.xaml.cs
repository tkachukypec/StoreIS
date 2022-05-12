using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StoreIS
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Auth_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(tb_Login.Text))
            {
                if(!String.IsNullOrEmpty(pb_Password.Password))
                {
                    IQueryable<Employee> employee_list = DataBase.GetContext().Employee.Where(p => p.Login == tb_Login.Text && p.Password == pb_Password.Password);
                    if (employee_list.Count() == 1)
                    {
                        Employee employee = employee_list.First();
                        MessageBox.Show("Добро пожаловать, " + employee.Name);
                        Window1 window = new Window1(employee);
                        window.Owner = this;
                        window.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Неверный логин или пароль");
                    }
                }
            }
        }
    }
}
