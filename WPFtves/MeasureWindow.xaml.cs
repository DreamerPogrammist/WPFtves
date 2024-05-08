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
    /// Interaction logic for MeasureWindow.xaml
    /// </summary>
    public partial class MeasureWindow : Window
    {
        private Measurement Measurements { get; set; } = new Measurement();

        public MeasureWindow(int idPatient)
        {
            Measurements.IdPatient = idPatient;
            InitializeComponent();
            TbWeight.Focus();
            DataContext = Measurements;           
            TbWeight.DataContext = Measurements;
            TbHeight.DataContext = Measurements;
        }

        private void TbWeight_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }
        private void TbHeight_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void OnOk()
        {
            //TbHeight.EndChange();
            Measurements.Create();
            Close();
        }

        private void OnCancel()
        {
            Close();
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            OnOk();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            OnCancel();
        }

        private void Window_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            //if (e.Key == Key.Enter)
            //{
            //    OnOk();
            //}
            //else if (e.Key == Key.Escape)
            //{
            //    OnCancel();
            //}
            UpdateImt();
        }

        private void UpdateImt()
        {
            try
            {
                decimal w = decimal.Parse(TbWeight.Text);
                decimal h = decimal.Parse(TbHeight.Text);
                decimal a = w / ((h / 100) * (h / 100));
                double ac = Math.Round((double)a, 1);

                if (ac <= 5 || ac >= 80)
                {
                    TbImt.Text = "-";
                } else
                {
                TbImt.Text = $"{ac}";

                }

            }
            catch (Exception ex)
            {
                TbImt.Text = "-";
            }
        }
    }
}
