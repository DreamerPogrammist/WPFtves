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

        public MyTestWindow()
        {
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

        private void TbHeight_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }
        private void TbWeight_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            Patient.Create();

            double height100 = Patient.Height/100;
            double imt = Math.Round(Patient.Weight/(height100*height100), 2);

            string message = $"Пациент {Patient.LastName} добавлен! \n \n ИМТ пациента = {imt}";
            MessageBox.Show(message);
            Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            
            Close();
        }

        private void TbBirthday_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime? selectedDate = TbBirthday.SelectedDate;
            //MessageBox.Show(selectedDate.Value.Date.ToShortDateString());
        }
    }
}
