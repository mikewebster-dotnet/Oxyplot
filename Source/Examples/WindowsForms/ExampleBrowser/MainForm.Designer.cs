// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainForm.Designer.cs" company="OxyPlot">
//   http://oxyplot.codeplex.com, license: MIT
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ExampleBrowser
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            OxyPlot.PlotModel plotModel1 = new OxyPlot.PlotModel();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.plot1 = new OxyPlot.WindowsForms.Plot();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.plot1);
            this.splitContainer1.Size = new System.Drawing.Size(943, 554);
            this.splitContainer1.SplitterDistance = 314;
            this.splitContainer1.TabIndex = 0;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(314, 554);
            this.treeView1.TabIndex = 1;
            // 
            // plot1
            // 
            this.plot1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plot1.KeyboardPanHorizontalStep = 0.1D;
            this.plot1.KeyboardPanVerticalStep = 0.1D;
            this.plot1.Location = new System.Drawing.Point(0, 0);
            plotModel1.Annotations = ((System.Collections.ObjectModel.Collection<OxyPlot.Annotations.Annotation>)(resources.GetObject("plotModel1.Annotations")));
            plotModel1.AutoAdjustPlotMargins = true;
            plotModel1.Axes = ((System.Collections.ObjectModel.Collection<OxyPlot.Axes.Axis>)(resources.GetObject("plotModel1.Axes")));
            plotModel1.AxisTierDistance = 4D;
            plotModel1.Background = null;
            plotModel1.Culture = null;
            plotModel1.DefaultColors = ((System.Collections.Generic.IList<OxyPlot.OxyColor>)(resources.GetObject("plotModel1.DefaultColors")));
            plotModel1.DefaultFont = "Segoe UI";
            plotModel1.DefaultFontSize = 12D;
            plotModel1.IsLegendVisible = true;
            plotModel1.LegendBackground = null;
            plotModel1.LegendBorder = null;
            plotModel1.LegendBorderThickness = 1D;
            plotModel1.LegendColumnSpacing = 0D;
            plotModel1.LegendFont = null;
            plotModel1.LegendFontSize = 12D;
            plotModel1.LegendFontWeight = 400D;
            plotModel1.LegendItemAlignment = OxyPlot.HorizontalTextAlign.Left;
            plotModel1.LegendItemOrder = OxyPlot.LegendItemOrder.Normal;
            plotModel1.LegendItemSpacing = 24D;
            plotModel1.LegendMargin = 8D;
            plotModel1.LegendOrientation = OxyPlot.LegendOrientation.Vertical;
            plotModel1.LegendPadding = 8D;
            plotModel1.LegendPlacement = OxyPlot.LegendPlacement.Inside;
            plotModel1.LegendPosition = OxyPlot.LegendPosition.RightTop;
            plotModel1.LegendSymbolLength = 16D;
            plotModel1.LegendSymbolMargin = 4D;
            plotModel1.LegendSymbolPlacement = OxyPlot.LegendSymbolPlacement.Left;
            plotModel1.LegendTextColor = null;
            plotModel1.LegendTitle = null;
            plotModel1.LegendTitleColor = null;
            plotModel1.LegendTitleFont = null;
            plotModel1.LegendTitleFontSize = 12D;
            plotModel1.LegendTitleFontWeight = 700D;
            plotModel1.PlotAreaBackground = null;
            plotModel1.PlotAreaBorderColor = ((OxyPlot.OxyColor)(resources.GetObject("plotModel1.PlotAreaBorderColor")));
            plotModel1.PlotAreaBorderThickness = 1D;
            plotModel1.PlotType = OxyPlot.PlotType.XY;
            plotModel1.Series = ((System.Collections.ObjectModel.Collection<OxyPlot.Series.Series>)(resources.GetObject("plotModel1.Series")));
            plotModel1.Subtitle = null;
            plotModel1.SubtitleColor = null;
            plotModel1.SubtitleFont = null;
            plotModel1.SubtitleFontSize = 14D;
            plotModel1.SubtitleFontWeight = 400D;
            plotModel1.TextColor = ((OxyPlot.OxyColor)(resources.GetObject("plotModel1.TextColor")));
            plotModel1.Title = null;
            plotModel1.TitleColor = null;
            plotModel1.TitleFont = null;
            plotModel1.TitleFontSize = 18D;
            plotModel1.TitleFontWeight = 700D;
            plotModel1.TitlePadding = 6D;
            this.plot1.Model = plotModel1;
            this.plot1.Name = "plot1";
            this.plot1.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plot1.Size = new System.Drawing.Size(625, 554);
            this.plot1.TabIndex = 0;
            this.plot1.Text = "plot1";
            this.plot1.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plot1.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plot1.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(943, 554);
            this.Controls.Add(this.splitContainer1);
            this.Name = "MainForm";
            this.Text = "OxyPlot.WindowsForms Example Browser";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeView1;
        private OxyPlot.WindowsForms.Plot plot1;
    }
}