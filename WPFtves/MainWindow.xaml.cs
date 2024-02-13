using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using WPFtves.Objects;

namespace WPFtves
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Patient CurrentPatient { get; set; } = new();
        public ObservableCollection<Patient> Patients = new();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = CurrentPatient;
            DgPatients.DataContext = this;
            DgPatients.ItemsSource = Patients;
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void BtnOpenWindow_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show($"Фамилия: {CurrentPatient.LastName}");
            var selected = DgPatients.SelectedItem as Patient;

            if(selected != null)
            {
                MyTestWindow w = new(selected);
                var r = w.ShowDialog();
                if (r == true)
                {
                    MessageBox.Show("Нажат OK");
                }
                else
                {
                    MessageBox.Show("Нажат Cancel");
                }
            }
        }

        private void DgPatients_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (DgPatients.SelectedItem is Patient selected)
            {
                //MessageBox.Show($"Выбран пациент {selected.LastName}");
                //CurrentPatient = selected;
            }
        }

        private void BtnAddPatient_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show($"Пациент {selected.LastName}{}{} добавлен")
        }
    }
}
