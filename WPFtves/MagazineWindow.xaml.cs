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
using System.Windows.Shapes;
using System.Xml.Linq;
using WPFtves.Objects;

namespace WPFtves
{
    /// <summary>
    /// Interaction logic for MagazineWindow.xaml
    /// </summary>
    public partial class MagazineWindow : Window
    {
        private int IdPatient;
        public ObservableCollection<Measurement> Measurements { get; set; }
        public ObservableCollection<Patient> Patients { get; set; } 

        public MagazineWindow(int idPatient)
        {
            Measurements = Measurement.GetList(idPatient);
            InitializeComponent();
            DgMeasurements.ItemsSource = Measurements;

            DataContext = Measurements;
            IdPatient = idPatient;
        }

        public void LoadMeasurements()
        {
            Measurements = Measurement.GetList(IdPatient);
            DgMeasurements.ItemsSource = Measurements;
        }

        private void BtnOkay_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            MeasureWindow measureWindow = new MeasureWindow(IdPatient);
            measureWindow.ShowDialog();
            LoadMeasurements();
        }

        private void BtnDel_Click(object sender, RoutedEventArgs e)
        {
            if (DgMeasurements.SelectedItem is Measurement selectedMeasurement)
            {

                var result = MessageBox.Show("Вы действительно хотите удалить запись?", "Предупреждение!", MessageBoxButton.OKCancel, MessageBoxImage.Exclamation);

                if (result == MessageBoxResult.OK)
                {
                    selectedMeasurement.Delete();
                    LoadMeasurements();
                }
                
            }

        }

    }
}
                                                                          