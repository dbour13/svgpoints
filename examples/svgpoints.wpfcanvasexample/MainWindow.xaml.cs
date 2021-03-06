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

namespace svgpoints.wpfcanvasexample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                if (files.Count() > 0)
                {
                    // Get svg points
                    try
                    {
                        var svgPathDataList = svgpoints.SvgPoints.GetPointsFromSVG(files.FirstOrDefault());

                        // Draw lines
                        cvArea.Children.Clear();
                        foreach (var svgPathData in svgPathDataList)
                        {
                            if (svgPathData.IsClosed)
                            {
                                var myPoly = new Polygon();
                                myPoly.Stroke = System.Windows.Media.Brushes.Black;
                                myPoly.StrokeThickness = 2;
                                for (int i = 0; i < svgPathData.Points.Count() - 1; i++)
                                {
                                    myPoly.Points.Add(new System.Windows.Point(svgPathData.Points[i].X, svgPathData.Points[i].Y));
                                }
                                cvArea.Children.Add(myPoly);
                            }
                            else
                            {
                                for (int i = 0; i < svgPathData.Points.Count() - 1; i++)
                                {
                                    var myLine = new Line();
                                    myLine.Stroke = System.Windows.Media.Brushes.Black;
                                    myLine.X1 = svgPathData.Points[i].X;
                                    myLine.X2 = svgPathData.Points[i + 1].X;
                                    myLine.Y1 = svgPathData.Points[i].Y;
                                    myLine.Y2 = svgPathData.Points[i + 1].Y;
                                    myLine.StrokeThickness = 2;
                                    cvArea.Children.Add(myLine);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Exception occured
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
        }
    }
}
