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

namespace ScrumProject.User_Controls
{
    /// <summary>
    /// Interaction logic for PlayerInfo.xaml
    /// </summary>
    public partial class PlayerInfo : UserControl
    {
        public PlayerInfo(string name)
        {
            InitializeComponent();
            lbName.Content = $"{name}";
        }
    }
}
