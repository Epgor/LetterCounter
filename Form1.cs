using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml.Linq;

namespace LetterCounter
{
    public partial class Form1 : Form
    {
        private Counter counter;
        private string file;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //LoadDefaultFile("default");
            LoadFile();
            label1.Text = counter.Count(true); //debug purposes
        }

        private void LoadDefaultFile(string name)
        {
            string sCurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string sFile = Path.Combine(sCurrentDirectory, @"..\..\defaultText.txt");
            string sFilePath = Path.GetFullPath(sFile);

            counter = new Counter(name);

            counter.SetFile(sFilePath);
            counter.LoadFile();

            counter.Count(true);
        }

        private void LoadFile()
        {
            counter = new Counter("loaded");

            counter.SetFile(file);
            counter.LoadFile();

            counter.Count(true);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var languageFrequency = counter.ReturnPlLanguageFrequerncy();
            var textFrequency = counter.ReturnTextFrequerncy();

            chart1.Series.Clear();

            chart1.ChartAreas["ChartArea1"].AxisX.Title = "Litery";
            chart1.ChartAreas["ChartArea1"].AxisY.Title = "%";

            chart1.Series.Add("PL_Language");
            chart1.Series["PL_Language"].ChartType = SeriesChartType.Line;
            chart1.Series["PL_Language"].Color = Color.Blue;

            chart1.Series.Add("Text_Frequency");
            chart1.Series["Text_Frequency"].ChartType = SeriesChartType.Line;
            chart1.Series["Text_Frequency"].Color = Color.Red;

            for(int i = 0; i < languageFrequency.Count; i++)
            {
                chart1.ChartAreas["ChartArea1"].AxisX.CustomLabels
                    .Add(i, i+1, languageFrequency.ElementAt(i).Key.ToString());
                chart1.Series["PL_Language"].Points
                    .AddXY(i, languageFrequency.ElementAt(i).Value);
                chart1.Series["Text_Frequency"].Points
                    .AddXY(i, textFrequency.ElementAt(i).Value);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Pliki tekstowe(*.txt) | *.txt";
            openFileDialog1.Multiselect = false;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                file = openFileDialog1.FileName;          
            }
        }
    }
}
