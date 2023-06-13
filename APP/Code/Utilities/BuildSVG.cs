﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Code
{
    public class BuildSVG
    {
        public static string LineTo(PointF NextPoint)
        {
            return LineTo(NextPoint.X, NextPoint.Y);
        }

        public static string LineTo(float x, float y)
        {
            return " L " + x + " " + y;
        }

        public static string QuadTo(float x, float y, float cx, float cy)
        {
            return " Q " + x + " " + y + " " + cx + " " + cy;
        }

        public static string CurveTo(float x, float y, float cx1, float cy1, float cx2, float cy2)
        {
            return " C " + cx1 + " " + cy1 + cx2 + " " + cy2 + " " + x + " " + y;
        }
        public static string MoveTo(float x, float y)
        {
            return " M " + x + " " + y;
        }

        public static string BoxValue(PointF InnerP1, PointF Center, float WidthLimit, float HeightLimit)
        {
            return "M 0 0"
                    + " L 0 0 M "
                    + (WidthLimit) + " 0 "
                    + " L "
                    + (WidthLimit) + " 0 "
                    + " M "
                    + WidthLimit + " " + HeightLimit
                    + " L "
                    + WidthLimit + " " + HeightLimit
                    + " M "
                    + WidthLimit + " 0 "
                    + " L "
                    + WidthLimit + " 0 "
                    // code above this is used to make maui 
                    // align the different shapes
                    + " M "
                    + InnerP1.X + " " + InnerP1.Y;
        }

        public static string BoxArea(PointF Center, PointF First, float Radius)
        {
            return "M " + (Center.X - Radius) + " " + (Center.Y - Radius)
                    + " L " + (Center.X - Radius) + " " + (Center.Y - Radius)
                    + " M " + (Center.X + Radius) + " " + (Center.Y + Radius)
                    + " L " + (Center.X + Radius) + " " + (Center.Y + Radius)
                    + " M " + First.X + " " + First.Y;
        }
    }
}
