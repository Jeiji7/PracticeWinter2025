using PracticeWinter2025.db;
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

namespace PracticeWinter2025.Pages
{
    /// <summary>
    /// Логика взаимодействия для EditService.xaml
    /// </summary>
    public partial class EditService : Page
    {
        private Service service = new Service();
        public EditService(Service _ser)
        {
            InitializeComponent();
            service = _ser;
            TitleTB.Text = service.Title.ToString();
            CostTB.Text = service.Cost.ToString();
            DurationTB.Text = service.DurationInSeconds.ToString();
            DiscountTB.Text = service.Discount.ToString();
        }


        private void Button_Click_Exit(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ServiceListPage());
        }

        private void Button_Click_SaveData(object sender, RoutedEventArgs e)
        {
            if (TitleTB.Text != null && CostTB.Text != null && DurationTB.Text != null && DiscountTB.Text != null)
            {
                service.Title = TitleTB.Text;
                service.Cost = Convert.ToDecimal(CostTB.Text);
                service.DurationInSeconds = Convert.ToInt32(DurationTB.Text);
                service.Discount = Convert.ToInt32(DiscountTB.Text);
                service.ActiveService = true;
                App.db.SaveChanges();
                MessageBox.Show("данные успешно добавлены!");
                NavigationService.Navigate(new ServiceListPage());
            }
            else
                MessageBox.Show("Провал!!");


        }
    }
}
