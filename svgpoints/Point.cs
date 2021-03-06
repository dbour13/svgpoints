﻿using System;
using System.Collections.Generic;
using System.Text;

namespace svgpoints
{
    /// <summary>
    /// Represents an X/Y coordinate
    /// </summary>
    public struct Point
    {
        public double X;
        public double Y;
        public Point(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }
    }

    public class SVGPathData
    {
        public List<Point> Points { get; set; }
        public bool IsClosed { get; set; } = false;
    }
}
