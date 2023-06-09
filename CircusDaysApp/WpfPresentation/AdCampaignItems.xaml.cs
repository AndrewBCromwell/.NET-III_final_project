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
using DataObjects;

namespace WpfPresentation
{
    /// <summary>
    /// Interaction logic for AdCampaignItems.xaml
    /// </summary>
    public partial class AdCampaignItems : Window
    {
        private AdCampaignVM _adCampaign = null;

        public AdCampaignItems(AdCampaignVM adCampaign)
        {
            _adCampaign = adCampaign;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            lblAdCampaign.Content = "Ad Campaign " + _adCampaign.AdCampaignId;
            lstAdItems.ItemsSource = _adCampaign.AdItems;
        }
    }
}
