﻿using System;
using System.Collections.Generic;
using System.Drawing;
using ESRI.ArcGIS.Display;

namespace Yutai.ArcGIS.Common.Helpers
{
    public class ColorHelper
    {
        // Fields  
        private static Random m_random = new Random();

        // Methods  
        public static Color CreateColor(IColor esriColor)
        {
            Color black = Color.Black;
            if (esriColor != null)
            {
                if (esriColor is IRgbColor)
                {
                    IRgbColor color2 = esriColor as IRgbColor;
                    black = Color.FromArgb(color2.Red, color2.Green, color2.Blue);
                }
                else
                {
                    int red = esriColor.RGB%256;
                    int green = (esriColor.RGB/256)%256;
                    int blue = ((esriColor.RGB/256)/256)%256;
                    black = Color.FromArgb(red, green, blue);
                }
            }
            return black;
        }

        public static IColor CreateColor(Color msColor)
        {
            return CreateColor(msColor.R, msColor.G, msColor.B);
        }

        public static IColor CreateColor(int red, int green, int blue)
        {
            RgbColor class2 = new RgbColor();
            class2.Red = red;
            class2.Green = green;
            class2.Blue = blue;
            return class2;
        }

        public static IColor CreateColor(byte alpha, int red, int green, int blue)
        {
            RgbColor class2 = new RgbColor();
            class2.Red = red;
            class2.Green = green;
            class2.Blue = blue;
            class2.Transparency = alpha;
            return class2;
        }

        public static IColor CreateRandomColor()
        {
            return CreateRandomColor(100);
        }

        public static IColor CreateRandomColor(byte alpha)
        {
            int red = m_random.Next(255);
            int green = m_random.Next(255);
            int blue = m_random.Next(255);
            return CreateColor(alpha, red, green, blue);
        }

        public static List<IColor> CreateRandomColorList(int pCount)
        {
            List<Color> list = new List<Color>();
            List<IColor> list2 = new List<IColor>();
            while (list.Count < pCount)
            {
                Color item = CreateRandomMSColor();
                if (!list.Contains(item))
                {
                    list.Add(item);
                }
            }
            foreach (Color color2 in list)
            {
                list2.Add(CreateColor(color2));
            }
            return list2;
        }

        public static Color CreateRandomMSColor()
        {
            int red = m_random.Next(255);
            int green = m_random.Next(255);
            int blue = m_random.Next(255);
            return Color.FromArgb(red, green, blue);
        }
    }
}