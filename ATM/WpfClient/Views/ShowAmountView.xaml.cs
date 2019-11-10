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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfClient.Tools.Navigation;
using WpfClient.ViewModels;

namespace WpfClient.Views
{
    /// <summary>
    /// Логика взаимодействия для ShowAmountView.xaml
    /// </summary>
    public partial class ShowAmountView : UserControl, INavigatable
    {
        public ShowAmountView()
        {
            InitializeComponent();
            DataContext = new ShowAmountViewModel();
        }
    }
}
