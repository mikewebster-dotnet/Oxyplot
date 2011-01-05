﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;
using Caliburn.Micro;
using Microsoft.Win32;
using OxyPlot;
using OxyPlot.Pdf;
using OxyPlot.Reporting;
using OxyPlot.Wpf;
using DataSeries = OxyPlot.DataSeries;
using Plot = OxyPlot.Wpf.Plot;

namespace ExportDemo
{
    [Export(typeof (IShell))]
    public class ShellViewModel : PropertyChangedBase, IShell
    {
        public enum RenderMethods
        {
            Shapes,
            DrawingContext
        }

        private RenderMethods renderMethod;

        public RenderMethods RenderMethod
        {
            get { return renderMethod; }
            set
            {
                renderMethod = value;
                RenderAsShapes = renderMethod == RenderMethods.Shapes;
                NotifyOfPropertyChange(() => RenderMethod);
            }
        }

        private PlotModel model;

        public Plot Plot { get; private set; }
        public Window Owner { get; private set; }

        public void Attach(Window owner, Plot plot)
        {
            Owner = owner;
            Plot = plot;
        }

        private ModelType currentModel;

        public ModelType CurrentModel
        {
            get { return currentModel; }
            set
            {
                currentModel = value;
                Model = PlotModelFactory.Create(currentModel);
            }
        }

        public ShellViewModel()
        {
            RenderAsShapes = true;
            CurrentModel = ModelType.SineWave;
        }

        private bool renderAsShapes;

        public bool RenderAsShapes
        {
            get { return renderAsShapes; }
            set
            {
                renderAsShapes = value;
                NotifyOfPropertyChange(() => RenderAsShapes);
            }
        }

        public PlotModel Model
        {
            get { return model; }
            set
            {
                if (model != value)
                {
                    model = value;
                    NotifyOfPropertyChange(() => Model);
                    NotifyOfPropertyChange(() => TotalNumberOfPoints);
                }
            }
        }

        public int TotalNumberOfPoints
        {
            get
            {
                if (Model == null) return 0;
                return Model.Series.Sum(ls => ((DataSeries) ls).Points.Count);
            }
        }

        public void SaveReport(string fileName)
        {
            string ext = Path.GetExtension(fileName);
            if (ext == null)
                return;
            ext = ext.ToLower();

            var r = CreateReport(fileName);

            if (ext == ".html")
            {
                const string style =
                    @"body { font-family: Verdana,Arial; margin:20pt; }
table { border: solid 1px black; margin: 8pt; border-collapse:collapse; }
td { padding: 0 2pt 0 2pt; border-left: solid 1px black; border-right: solid 1px black;}
thead { border:solid 1px black; }
.content, .content td { border: none; }
.figure { margin: 8pt;}
.table { margin: 8pt;}
.table caption { margin: 4pt;}
.table thead td { padding: 2pt;}";

                using (var hw = new HtmlReportWriter(fileName, "OxyPlot example file", null, style))
                {
                    r.Write(hw);
                }
            }

            if (ext == ".pdf")
                using (var pw = new PdfReportWriter(fileName))
                {
                    r.Write(pw);
                }

            if (ext == ".rtf")
                using (var pw = new RtfReportWriter(fileName))
                {
                    r.Write(pw);
                }

            if (ext == ".tex")
                using (var pw = new LatexReportWriter(fileName, "Example report", "oxyplot"))
                {
                    r.Write(pw);
                }

            if (ext == ".txt")
                using (var tw = new TextReportWriter(Path.ChangeExtension(fileName, ".txt")))
                {
                    r.Write(tw);
                }
        }

        private Report CreateReport(string fileName)
        {
            string ext = Path.GetExtension(fileName);
            ext = ext.ToLower();

            var r = new Report();
            var main = new ReportSection();

            r.AddHeader(1, "Example report from OxyPlot");
            r.AddHeader(2, "Content");
            r.AddTableOfContents(main);
            r.Add(main);

            main.AddHeader(2, "Introduction");
            main.AddParagraph("The content in this file was generated by OxyPlot.");
            main.AddParagraph("See http://oxyplot.codeplex.com for more information.");

            main.AddHeader(2, "Plot (vector)");
            var dir = Path.GetDirectoryName(fileName);
            var name = Path.GetFileNameWithoutExtension(fileName);

            string fileNameWithoutExtension = Path.Combine(dir, name);

            switch (ext)
            {
                case ".html":
                    {
                        main.AddParagraph("This plot was rendered to SVG and embedded in the HTML5 file.");
                        var svg = Model.ToSvg(800, 500);
                        main.AddDrawing(svg, "SVG plot");
                        break;
                    }
                case ".pdf":
                case ".tex":
                    {
                        var pdfPlotFileName = fileNameWithoutExtension + "_plot.pdf";
                        PdfExporter.Export(Model, pdfPlotFileName, 800, 500);
                        main.AddParagraph("This plot was rendered to PDF and embedded in the report.");
                        main.AddImage(Path.GetFileName(pdfPlotFileName), "PDF plot");
                        break;
                    }
            }

            main.AddHeader(2, "Plot (bitmap)");
            main.AddParagraph(
                "The plot is rendered to PNG and embedded in the HTML5 file.");

            string pngPlotFileName = fileNameWithoutExtension + "_plot.png";
            PngExporter.Export(Model, pngPlotFileName, 800, 500);
            main.AddImage(Path.GetFileName(pngPlotFileName), "PNG plot");

            main.AddHeader(2, "Data");
            int i = 1;
            foreach (DataSeries s in Model.Series)
            {
                main.AddHeader(3, "Data series " + (i++));
                main.AddPropertyTable("Properties of the " + s.GetType().Name, new[] {s});
                var fields = new List<TableColumn>
                                 {
                                     new TableColumn("X", "X"),
                                     new TableColumn("Y", "Y")
                                 };
                main.AddTable("Data", s.Points, fields);
            }
            main.AddHeader(3, "Equations");
            main.AddEquation(@"E = m \cdot c^2");
            main.AddEquation(@"\oint \vec{B} \cdot d\vec{S} = 0");
            return r;
        }

        public void SaveSvg()
        {
            var path = GetFilename(".svg files|*.svg", ".svg");
            if (path != null)
            {
                Model.SaveSvg(path, Plot.ActualWidth, Plot.ActualHeight);
                OpenContainingFolder(path);
            }
        }

        public void SavePng()
        {
            var path = GetFilename(".png files|*.png", ".png");
            if (path != null)
            {
                Plot.SaveBitmap(path);
                OpenContainingFolder(path);
            }
        }

        private static void OpenContainingFolder(string fileName)
        {
            // var folder = Path.GetDirectoryName(fileName);
            var psi = new ProcessStartInfo("Explorer.exe", "/select," + fileName);
            Process.Start(psi);
        }

        public void SavePdf()
        {
            var path = GetFilename(".pdf files|*.pdf", ".pdf");
            if (path != null)
            {
                PdfExporter.Export(Model, path, Plot.ActualWidth, Plot.ActualHeight);
                OpenContainingFolder(path);
            }
        }

        public void SaveRtfReport()
        {
            var path = GetFilename(".rtf files|*.rtf", ".rtf");
            if (path != null)
            {
                SaveReport(path);
                OpenContainingFolder(path);
            }
        }

        public void SaveHtmlReport()
        {
            var path = GetFilename(".html files|*.html", ".html");
            if (path != null)
            {
                SaveReport(path);
                OpenContainingFolder(path);
            }
        }

        public void SaveLatexReport()
        {
            var path = GetFilename(".tex files|*.tex", ".tex");
            if (path != null)
            {
                SaveReport(path);
                OpenContainingFolder(path);
            }
        }

        public void SaveXaml()
        {
            var path = GetFilename(".xaml files|*.xaml", ".xaml");
            if (path != null)
            {
                Plot.SaveXaml(path);
                OpenContainingFolder(path);
            }
        }

        public void CopySvg()
        {
            Clipboard.SetText(Model.ToSvg(Plot.ActualWidth, Plot.ActualHeight, true));
        }

        public void CopyBitmap()
        {
            Clipboard.SetImage(Plot.ToBitmap());
        }

        public void CopyXaml()
        {
            Clipboard.SetText(Plot.ToXaml());
        }

        public void SavePdfReport()
        {
            var path = GetFilename(".pdf files|*.pdf", ".pdf");
            if (path != null)
            {
                SaveReport(path);
                OpenContainingFolder(path);
            }
        }

        private string GetFilename(string filter, string defaultExt)
        {
            // todo: this should probably move out of the viewmodel
            var dlg = new SaveFileDialog {Filter = filter, DefaultExt = defaultExt};
            return dlg.ShowDialog(Owner).Value ? dlg.FileName : null;
        }

        public void Exit()
        {
            Owner.Close();
        }

        public void HelpHome()
        {
            Process.Start("http://oxyplot.codeplex.com");
        }

        public void HelpDocumentation()
        {
            Process.Start("http://oxyplot.codeplex.com/documentation");
        }
        public void HelpAbout()
        {
            var dlg = new PropertyEditorLibrary.AboutDialog(Owner);
            dlg.Title = "About OxyPlot ExportDemo";
            dlg.Image = new BitmapImage(new Uri(@"pack://application:,,,/ExportDemo;component/Images/oxyplot.png"));
            dlg.Show();
        }
    }
}