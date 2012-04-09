// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HtmlReportWriter.cs" company="OxyPlot">
//   http://oxyplot.codeplex.com, license: Ms-PL
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OxyPlot.Reporting
{
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    /// <summary>
    /// Specifies the html element type to use when writing plots.
    /// </summary>
    public enum HtmlPlotElementType
    {
        /// <summary>
        /// Use the embed tag and reference an external svg file.
        /// </summary>
        Embed, 

        /// <summary>
        /// Use the object tag and reference an external svg file.
        /// </summary>
        Object, 

        /// <summary>
        /// Use the svg tag and include the plot inline.
        /// </summary>
        Svg
    }

    /// <summary>
    /// HTML5 report writer.
    /// </summary>
    public class HtmlReportWriter : XmlWriterBase, IReportWriter
    {

        #region Constants and Fields

        /// <summary>
        ///   The figure counter.
        /// </summary>
        private int figureCounter;

        /// <summary>
        ///   The style.
        /// </summary>
        private ReportStyle style;

        /// <summary>
        /// The directory of the output file.
        /// </summary>
        private string directory;

        /// <summary>
        /// The path of the output file.
        /// </summary>
        private string outputFile;
        #endregion

        #region Constructors and Destructors

#if !METRO

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlReportWriter"/> class.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        public HtmlReportWriter(string path)
            : base(path)
        {
            this.directory = Path.GetDirectoryName(path);
            this.outputFile = path;
            this.PlotElementType = HtmlPlotElementType.Embed;
            this.WriteHtmlElement();
        }

#endif

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlReportWriter"/> class.
        /// </summary>
        /// <param name="stream">
        /// The stream.
        /// </param>
        public HtmlReportWriter(Stream stream)
            : base(stream)
        {
            this.WriteHtmlElement();
            this.PlotElementType = HtmlPlotElementType.Svg;
        }

        #endregion

        public HtmlPlotElementType PlotElementType { get; set; }

        #region Public Methods

        /// <summary>
        /// Closes this instance.
        /// </summary>
        public override void Close()
        {
            this.WriteEndElement();
            this.WriteEndElement();
            base.Close();
        }

        /// <summary>
        /// Writes the class ID.
        /// </summary>
        /// <param name="className">
        /// The class.
        /// </param>
        /// <param name="id">
        /// The id.
        /// </param>
        public void WriteClassId(string className, string id = null)
        {
            if (className != null)
            {
                this.WriteAttributeString("class", className);
            }

            if (id != null)
            {
                this.WriteAttributeString("id", id);
            }
        }

        /// <summary>
        /// Writes the drawing.
        /// </summary>
        /// <param name="d">
        /// The drawing.
        /// </param>
        public void WriteDrawing(DrawingFigure d)
        {
            this.WriteStartFigure(d);
            this.WriteRaw(d.Content);
            this.WriteEndFigure(d.FigureText);
        }

        /// <summary>
        /// Writes the equation.
        /// </summary>
        /// <param name="equation">
        /// The equation.
        /// </param>
        public void WriteEquation(Equation equation)
        {
            // todo: MathML?
        }

        /// <summary>
        /// Writes the header.
        /// </summary>
        /// <param name="h">
        /// The header.
        /// </param>
        public void WriteHeader(Header h)
        {
            if (h.Text == null)
            {
                return;
            }

            this.WriteStartElement("h" + h.Level);
            this.WriteString(h.ToString());
            this.WriteEndElement();
        }

        /// <summary>
        /// Writes the image.
        /// </summary>
        /// <param name="i">
        /// The image.
        /// </param>
        public void WriteImage(Image i)
        {
            // this requires the image to be located in the same folder as the html
            string localFileName = Path.GetFileName(i.Source);
            this.WriteStartFigure(i);
            this.WriteStartElement("img");
            this.WriteAttributeString("src", localFileName);
            this.WriteAttributeString("alt", i.FigureText);
            this.WriteEndElement();
            this.WriteEndFigure(i.FigureText);
        }

        /// <summary>
        /// Writes the paragraph.
        /// </summary>
        /// <param name="p">
        /// The paragraph.
        /// </param>
        public void WriteParagraph(Paragraph p)
        {
            this.WriteElementString("p", p.Text);
        }

        /// <summary>
        /// Writes the plot.
        /// </summary>
        /// <param name="plot">
        /// The plot.
        /// </param>
        public void WritePlot(PlotFigure plot)
        {
            this.WriteStartFigure(plot);
            switch (this.PlotElementType)
            {
                case HtmlPlotElementType.Embed:
                case HtmlPlotElementType.Object:
                    string source = string.Format("{0}_Plot{1}.svg", Path.GetFileNameWithoutExtension(this.outputFile), plot.FigureNumber);
                    plot.PlotModel.SaveSvg(this.GetFullFileName(source), plot.Width, plot.Height);
                    this.WriteStartElement(this.PlotElementType == HtmlPlotElementType.Embed ? "embed" : "object");
                    this.WriteAttributeString("src", source);
                    this.WriteAttributeString("type", "image/svg+xml");
                    this.WriteEndElement();
                    break;
                case HtmlPlotElementType.Svg:
                    this.WriteRaw(plot.PlotModel.ToSvg(plot.Width, plot.Height));
                    break;
            }

            this.WriteEndFigure(plot.FigureText);
        }

        /// <summary>
        /// The write report.
        /// </summary>
        /// <param name="report">
        /// The report.
        /// </param>
        /// <param name="style">
        /// The style.
        /// </param>
        public void WriteReport(Report report, ReportStyle style)
        {
            this.style = style;
            this.WriteHtmlHeader(report.Title, null, CreateCss(style));
            report.Write(this);
        }

        /// <summary>
        /// Writes the items.
        /// </summary>
        /// <param name="t">
        /// The t.
        /// </param>
        public void WriteRows(Table t)
        {
            IList<TableColumn> columns = t.Columns;

            foreach (var c in columns)
            {
                this.WriteStartElement("col");
                this.WriteAttributeString("align", GetAlignmentString(c.Alignment));
                if (double.IsNaN(c.Width))
                {
                    this.WriteAttributeString("width", c.Width + "pt");
                }

                this.WriteEndElement();
            }

            foreach (var row in t.Rows)
            {
                if (row.IsHeader)
                {
                    this.WriteStartElement("thead");
                }

                this.WriteStartElement("tr");
                int j = 0;
                foreach (var c in row.Cells)
                {
                    bool isHeader = row.IsHeader || t.Columns[j++].IsHeader;

                    this.WriteStartElement("td");
                    if (isHeader)
                    {
                        this.WriteAttributeString("class", "header");
                    }

                    this.WriteString(c.Content);
                    this.WriteEndElement();
                }

                this.WriteEndElement(); // tr
                if (row.IsHeader)
                {
                    this.WriteEndElement(); // thead
                }
            }
        }

        /// <summary>
        /// Writes the table.
        /// </summary>
        /// <param name="t">
        /// The t.
        /// </param>
        public void WriteTable(Table t)
        {
            if (t.Rows == null || t.Columns == null)
            {
                return;
            }

            this.WriteStartElement("table");

            // WriteAttributeString("border", "1");
            // WriteAttributeString("width", "60%");
            if (t.Caption != null)
            {
                this.WriteStartElement("caption");
                this.WriteString(t.GetFullCaption(this.style));
                this.WriteEndElement();
            }

            this.WriteRows(t);

            this.WriteEndElement(); // table
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the full name of the specified file.
        /// </summary>
        /// <param name="filename">The filename (relative to the output file).</param>
        /// <returns>A full path to the file.</returns>
        private string GetFullFileName(string filename)
        {
            return Path.GetFullPath(Path.Combine(this.directory, filename));
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        private void WriteHtmlElement()
        {
            this.WriteStartElement("html", "http://www.w3.org/1999/xhtml");
        }

        /// <summary>
        /// Creates the css section.
        /// </summary>
        /// <param name="style">
        /// The style.
        /// </param>
        /// <returns>
        /// The css.
        /// </returns>
        private static string CreateCss(ReportStyle style)
        {
            var css = new StringBuilder();
            css.AppendLine("body { " + ParagraphStyleToCss(style.BodyTextStyle) + " }");
            for (int i = 0; i < style.HeaderStyles.Length; i++)
            {
                css.AppendLine("h" + (i + 1) + " {" + ParagraphStyleToCss(style.HeaderStyles[i]) + " }");
            }

            css.AppendLine("table caption { " + ParagraphStyleToCss(style.TableCaptionStyle) + " }");
            css.AppendLine("thead { " + ParagraphStyleToCss(style.TableHeaderStyle) + " }");
            css.AppendLine("td { " + ParagraphStyleToCss(style.TableTextStyle) + " }");
            css.AppendLine("td.header { " + ParagraphStyleToCss(style.TableHeaderStyle) + " }");
            css.AppendLine("figuretext { " + ParagraphStyleToCss(style.FigureTextStyle) + " }");

            css.Append(
                @"body { margin:20pt; }
            table { border: solid 1px black; margin: 8pt; border-collapse:collapse; }
            td { padding: 0 2pt 0 2pt; border-left: solid 1px black; border-right: solid 1px black;}
            thead { border:solid 1px black; }
            .content, .content td { border: none; }
            .figure { margin: 8pt;}
            .table { margin: 8pt;}
            .table caption { margin: 4pt;}
            .table thead td { padding: 2pt;}");
            return css.ToString();
        }

        /// <summary>
        /// Gets the alignment string.
        /// </summary>
        /// <param name="a">The alignment type.</param>
        /// <returns>An alignment string.</returns>
        private static string GetAlignmentString(Alignment a)
        {
            return a.ToString().ToLower();
        }

        /// <summary>
        /// Converts a paragraphes style to css.
        /// </summary>
        /// <param name="s">The style.</param>
        /// <returns>A css string.</returns>
        private static string ParagraphStyleToCss(ParagraphStyle s)
        {
            var css = new StringBuilder();
            if (s.FontFamily != null)
            {
                css.Append(string.Format("font-family:{0};", s.FontFamily));
            }

            css.Append(string.Format("font-size:{0}pt;", s.FontSize));
            if (s.Bold)
            {
                css.Append(string.Format("font-weight:bold;"));
            }

            return css.ToString();
        }

        /// <summary>
        /// Writes the div.
        /// </summary>
        /// <param name="style">The style.</param>
        /// <param name="content">The content.</param>
        private void WriteDiv(string style, string content)
        {
            this.WriteStartElement("div");
            this.WriteAttributeString("class", style);
            this.WriteString(content);
            this.WriteEndElement();
        }

        /// <summary>
        /// Writes the end figure.
        /// </summary>
        /// <param name="text">The figure text.</param>
        private void WriteEndFigure(string text)
        {
            this.WriteDiv("figuretext", string.Format("Fig {0}. {1}", this.figureCounter, text));
            this.WriteEndElement();
        }

        /// <summary>
        /// Writes the HTML header.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="cssPath">The CSS path.</param>
        /// <param name="style">The style.</param>
        private void WriteHtmlHeader(string title, string cssPath, string style)
        {
            this.WriteStartElement("head");

            if (title != null)
            {
                this.WriteElementString("title", title);
            }

            if (cssPath != null)
            {
                this.WriteStartElement("link");
                this.WriteAttributeString("href", cssPath);
                this.WriteAttributeString("rel", "stylesheet");
                this.WriteAttributeString("type", "text/css");
                this.WriteEndElement(); // link
            }

            if (style != null)
            {
                this.WriteStartElement("style");
                this.WriteAttributeString("type", "text/css");
                this.WriteRaw(style);
                this.WriteEndElement();
            }

            this.WriteEndElement(); // head
            this.WriteStartElement("body");
        }

        /// <summary>
        /// Writes the start figure element.
        /// </summary>
        /// <param name="f">The figure.</param>
        private void WriteStartFigure(Figure f)
        {
            this.figureCounter++;
            this.WriteStartElement("p");
            this.WriteClassId("figure");
        }

        #endregion
    }
}