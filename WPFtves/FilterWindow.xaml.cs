﻿using System;
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

namespace WPFtves
{
    /// <summary>
    /// Interaction logic for FilterWindow.xaml
    /// </summary>
    public partial class FilterWindow : Window
    {
        public FilterWindow()
        {
            InitializeComponent();
        }

        private void OnOk()
        {
            //Measurements.Create();
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
    }
}
