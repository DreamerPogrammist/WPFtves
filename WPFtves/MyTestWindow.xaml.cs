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
using System.Windows.Shapes;
using WPFtves.Objects;

namespace WPFtves
{
    /// <summary>
    /// Interaction logic for MyTestWindow.xaml
    /// </summary>
    public partial class MyTestWindow : Window
    {
        private Patient Patient { get; set; } = new();
        public MyTestWindow(Patient patient)
        {
            Patient = patient;
            InitializeComponent();
            DataContext = Patient;
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult= false;
        }
    }
}
