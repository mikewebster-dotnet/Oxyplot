// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ItemsSourceExamples.cs" company="OxyPlot">
//   http://oxyplot.codeplex.com, license: Ms-PL
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using OxyPlot;

namespace ExampleLibrary
{

    

    [Examples("ItemsSource")]
    public class ItemsSourceExamples : ExamplesBase
    {
        [Example("Using IDataPointProvider")]
        public static PlotModel UsingIDataPointProvider()
        {
            var measurements = new[] { new MeasurementType1(0, 10), new MeasurementType1(10, 8), new MeasurementType1(15, 12) };
            var model = new PlotModel("Using IDataPointProvider");
            model.Series.Add(new LineSeries { ItemsSource = measurements });
            return model;
        }

        [Example("Using IDataPoint")]
        public static PlotModel UsingIDataPoint()
        {
            var measurements = new[] { new MeasurementType2(0, 8), new MeasurementType2(10, 10), new MeasurementType2(15, 12) };
            var model = new PlotModel("Using IDataPoint");
            model.Series.Add(new LineSeries { ItemsSource = measurements });
            return model;
        }

        [Example("Using reflection")]
        public static PlotModel UsingReflection()
        {
            var measurements = new[] { new MeasurementType3(0, 12), new MeasurementType3(10, 8), new MeasurementType3(15, 10) };
            var model = new PlotModel("Using reflection");
            model.Series.Add(new LineSeries { ItemsSource = measurements, DataFieldX = "Abscissa", DataFieldY = "Ordinate" });
            return model;
        }

public class MeasurementType1 : IDataPointProvider
    {
        public double Abscissa { get; set; }
        public double Ordinate { get; set; }

        public MeasurementType1(double abscissa, double ordinate)
        {
            this.Abscissa = abscissa;
            this.Ordinate = ordinate;
        }

        public DataPoint GetDataPoint()
        {
            return new DataPoint(Abscissa, Ordinate);
        }
    }

    public class MeasurementType2 : IDataPoint
    {
        public double Abscissa { get; set; }
        public double Ordinate { get; set; }

        public MeasurementType2(double abscissa, double ordinate)
        {
            this.Abscissa = abscissa;
            this.Ordinate = ordinate;
        }

        public DataPoint GetDataPoint()
        {
            return new DataPoint(Abscissa, Ordinate);
        }

        public string ToCode()
        {
            return CodeGenerator.FormatConstructor(this.GetType(), "{0},{1}", this.Abscissa, this.Ordinate);
        }

        public double X
        {
            get
            {
                return Abscissa;
            }
        }

        public double Y
        {
            get
            {
                return Ordinate;
            }
        }
    }

    public class MeasurementType3
    {
        public double Abscissa { get; set; }

        public double Ordinate { get; set; }

        public MeasurementType3(double abscissa, double ordinate)
        {
            this.Abscissa = abscissa;
            this.Ordinate = ordinate;
        }
    }
    }
}