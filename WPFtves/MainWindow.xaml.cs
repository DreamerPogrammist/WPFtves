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
using WPFtves.AppData;
using System.DirectoryServices;

namespace WPFtves
{

    public partial class MainWindow : Window
    {
        public Patient Patient { get; set; }
        public ObservableCollection<Patient> Patients { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            
            Database.Connect(new Configuration());
            
            Patient = new Patient();
            Patients = new ObservableCollection<Patient>();


            LoadPatients();
            //DgPatients.DataContext = this;
            //DgPatients.ItemsSource = Patients;

        }

        private void LoadPatients()
        {
            Patients.Clear();

            var list = Patient.GetList();
            foreach ( var item in list )
            {
                Patients.Add(item);
            }
        }

        private void BtnD_Click(object sender, RoutedEventArgs e)
        {
            if (DgPatients.SelectedItem is Patient selected)
            {
                Patient.Delete(selected.Id);
            }        

            LoadPatients();
            string message = "Пациент удалён!";
            MessageBox.Show(message);
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {

            MyTestWindow myTestWindow = new MyTestWindow();
            myTestWindow.ShowDialog();
            LoadPatients();
        }

        private void DgPatients_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (DgPatients.SelectedItem is Patient selected)
            {
                MessageBox.Show($"Выбран пациент {selected.LastName}");
                Patient = selected;
            }
        }

    }
}
