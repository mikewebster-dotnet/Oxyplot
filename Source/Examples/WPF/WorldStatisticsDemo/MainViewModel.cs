//-----------------------------------------------------------------------
// <copyright file="MainViewModel.cs" company="OxyPlot">
//     http://oxyplot.codeplex.com, license: Ms-PL
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using OxyPlot;

namespace WorldStatisticsDemo
{
    /// <summary>
    /// www.gapminder.org
    /// </summary>
    public class MainViewModel : Observable
    {
        private Country selectedCountry;

        public Country SelectedCountry
        {
            get
            {
                return this.selectedCountry;
            }
            set
            {
                this.selectedCountry = value; RaisePropertyChanged(() => SelectedCountry);
                this.UpdatePlot();
            }
        }

        public Dictionary<string, Country> Countries { get; set; }

        private int year;
        public int Year
        {
            get { return year; }
            set
            {
                year = value;
                RaisePropertyChanged(() => Year);
                UpdatePlot();
            }
        }

        private void UpdatePlot()
        {
            var pm = new PlotModel(this.year.ToString(), "data from gapminder.org") { LegendPosition = LegendPosition.RightBottom };
            var ss = new ScatterSeries
                {
                    MarkerType = MarkerType.Circle,
                    MarkerFill = OxyColors.Transparent,
                    MarkerStroke = OxyColors.Blue,
                    MarkerStrokeThickness = 1
                };

            var piX = typeof(Statistics).GetProperty("GdpPerCapitaPpp");
            var piY = typeof(Statistics).GetProperty("LifeExpectancyAtBirth");
            var piSize = typeof(Statistics).GetProperty("Population");
            var piColor = typeof(Statistics).GetProperty("GeographicRegion");

            foreach (var kvp in Countries)
            {
                Country country = kvp.Value;
                double x = country.FindValue(year, piX);
                double y = country.FindValue(year, piY);
                double size = country.FindValue(year, piSize);

                if (double.IsNaN(x) || double.IsNaN(y))
                    continue;
                ss.Points.Add(new ScatterPoint(x, y, double.NaN, double.NaN, country.Name));

                //double radius = 4;
                //if (!double.IsNaN(size))
                //    radius = Math.Sqrt(size)*0.1;
                //if (radius < 4) radius = 4;
                //if (radius > 40) radius = 40;
                //ss.MarkerSizes.Add(radius);
                //   Debug.WriteLine(countryName+": "+stats.Population);
            }
            pm.Series.Add(ss);
            if (SelectedCountry != null)
            {
                var ls = new LineSeries(SelectedCountry.Name);
                ls.LineJoin = OxyPenLineJoin.Bevel;
                foreach (var p in SelectedCountry.Statistics)
                {
                    if (double.IsNaN(p.GdpPerCapitaPpp) || double.IsNaN(p.LifeExpectancyAtBirth)) continue;
                    ls.Points.Add(new DataPoint(p.GdpPerCapitaPpp, p.LifeExpectancyAtBirth));
                }
                pm.Series.Add(ls);
                var ss2 = new ScatterSeries();
                double x = SelectedCountry.FindValue(year, piX);
                double y = SelectedCountry.FindValue(year, piY);
                ss2.Points.Add(new ScatterPoint(x, y, 10));
                ss2.MarkerFill = OxyColor.FromAColor(120, OxyColors.Red);
                ss2.MarkerType = MarkerType.Circle;
                pm.Series.Add(ss2);
            }
            pm.Axes.Add(new LinearAxis(AxisPosition.Left, 19, 87, "Life expectancy (years)"));
            pm.Axes.Add(new LogarithmicAxis(AxisPosition.Bottom, 200, 90000, "Income per person (GDP/capita, PPP$ inflation-adjusted)"));
            PlotModel = pm;
        }

        private PlotModel plotModel;
        public PlotModel PlotModel
        {
            get { return plotModel; }
            set
            {
                plotModel = value;
                RaisePropertyChanged(() => PlotModel);
            }
        }

        public MainViewModel()
        {
            this.Countries = new Dictionary<string, Country>();

            // Load CSV files
            Load(@"Data\gdp_per_capita_ppp.csv", "GdpPerCapitaPpp");
            Load(@"Data\life_expectancy_at_birth.csv", "LifeExpectancyAtBirth");
            Load(@"Data\population.csv", "Population");

            foreach (var country in Countries.Values)
                country.SortStatistics();

            Year = 2009;
            SelectedCountry = Countries["Norway"];
        }

        private void Load(string path, string property)
        {
            var pi = typeof(Statistics).GetProperty(property);
            var doc = new CsvDocument();
            doc.Load(path);
            foreach (var item in doc.Items)
            {
                var countryName = item[0];
                if (!Countries.ContainsKey(countryName))
                    Countries[countryName] = new Country(countryName);
                var country = Countries[countryName];
                for (int i = 1; i < doc.Headers.Length; i++)
                {
                    int year = int.Parse(doc.Headers[i]);
                    if (!country.StatisticsByYear.ContainsKey(year))
                        country.StatisticsByYear[year] = new Statistics(year);
                    double value;
                    if (double.TryParse(item[i], out value))
                    {
                        var statistics = country.StatisticsByYear[year];
                        pi.SetValue(statistics, value, null);
                    }
                }
            }
        }
    }
}
