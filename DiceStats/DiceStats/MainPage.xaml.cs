﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microcharts;
using SkiaSharp;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace DiceStats
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            var OczkaDefault = new int[11] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            var Kolory = new string[11] { "d44de1", "#da2ea2", "df0e62", "e63d4d", "fac70b", "c0b329", "869f46", "4c8b64", "127681", "1a4766", "21174a" };
            Dane.oczka = OczkaDefault;
            Dane.kolory = Kolory;

        }
        void DrawChart()
        {
            var suma = 0;
            for (int i = 0; i <= 10; i++)
            {
                suma += Dane.oczka[i];
            }
            List<ChartEntry> DataList = new List<ChartEntry>();
            List<ChartEntry> DataList2 = new List<ChartEntry>();
            for (int i = 0; i <= 10; i++)
            {
                var procent = 0f;
                if (Dane.oczka[i] != 0)
                {
                    procent = (float)Dane.oczka[i] / (float)suma * 100;
                }
                DataList.Add(new ChartEntry(Dane.oczka[i])
                {
                    Label = (i + 2).ToString(),
                    ValueLabel = Dane.oczka[i].ToString(),
                    Color = SKColor.Parse(Dane.kolory[i])
                });
                DataList2.Add(new ChartEntry(Dane.oczka[i])
                {
                    Label = (i + 2) + " - " + Math.Round(procent, 1) + "%",
                    ValueLabel = Dane.oczka[i].ToString(),
                    Color = SKColor.Parse(Dane.kolory[i])
                });

            }

            chartViewBar.Chart = new BarChart { Entries = DataList, Margin = 40, BarAreaAlpha = 80, ValueLabelOrientation = Orientation.Horizontal, LabelOrientation = Orientation.Horizontal, LabelTextSize = 60 };
            chartViewPie.Chart = new PieChart { Entries = DataList2, HoleRadius = 0.25f, LabelTextSize = 45 };
        }
        private void Button_Clicked(object sender, EventArgs e)
        {
            var rand = new Random();
            int dice1 = rand.Next(1, 7);
            int dice2 = rand.Next(1, 7);
            Dice.Text = (dice1 + dice2).ToString();
            Dane.oczka[(dice1 + dice2 - 2)] = Dane.oczka[(dice1 + dice2 - 2)] + 1;
            DrawChart();
        }
    }
}
