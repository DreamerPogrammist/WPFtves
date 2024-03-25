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
using WPFtves.Objects;

namespace WPFtves
{
    
    public partial class MyTestWindow : Window
    {
        public Patient Patient { get; set; } = new Patient();
        //public ObservableCollection<Patient> Patients { get; set; }

        public MyTestWindow()
        {
            //Patient = patient;
            InitializeComponent();
            DataContext = Patient;
            TbLastName.DataContext = Patient;
            TbName.DataContext = Patient;
            TbFatherName.DataContext = Patient;
            TbBirthday.DataContext = Patient;
            TbSex.DataContext = Patient;
            TbInsurance.DataContext = Patient;
            TbHeight.DataContext = Patient;
            TbWeight.DataContext = Patient;
        }

        
        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            Patient.Create();

            string message = "Пациент добавлен!";
            MessageBox.Show(message);
            Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            
            Close();
        }
    }
}
