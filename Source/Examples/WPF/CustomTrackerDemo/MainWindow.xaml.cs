﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using OxyPlot;

namespace CustomTrackerDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var model = new PlotModel();
            model.Series.Add(new LineSeries("Series 1") { TrackerKey = "Tracker1", ItemsSource = new List<DataPoint> { new DataPoint(0, 0), new DataPoint(10, 20), new DataPoint(20, 18) } });
            model.Series.Add(new LineSeries("Series 2") { TrackerKey = "Tracker2", ItemsSource = new List<DataPoint> { new DataPoint(0, 10), new DataPoint(10, 10), new DataPoint(20, 16) } });
            this.Model = model;
            DataContext = this;
        }

        public PlotModel Model { get; set; }
    }
}
