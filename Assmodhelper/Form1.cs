using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Security;

namespace Assmodhelper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SaveFilename.ReadOnly = true;
        }

        private void AddAss_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog()
            {
                FileName = "Select a text file",
                Filter = "Ass files (*.ass)|*.ass",
                Title = "Open text file",
                Multiselect = true
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    foreach (var i in openFileDialog1.FileNames)
                    {
                        AssList.Items.Add(i);
                    }
                }
                catch (SecurityException ex)
                {
                    MessageBox.Show($"Security error.\n\nError message: {ex.Message}\n\n" +
                                    $"Details:\n\n{ex.StackTrace}");
                }
            }
        }

        private void DelAss_Click(object sender, EventArgs e)
        {
            if (AssList.SelectedIndex != -1)
                AssList.Items.RemoveAt(AssList.SelectedIndex);
        }

        private void OutputFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog()
            {
                FileName = "Select a text file",
                Filter = "Ass files (*.ass)|*.ass",
                Title = "Open text file"
            };

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var filePath = saveFileDialog1.FileName;
                    SaveFilename.Text = filePath;
                }
                catch (SecurityException ex)
                {
                    MessageBox.Show($"Security error.\n\nError message: {ex.Message}\n\n" +
                                    $"Details:\n\n{ex.StackTrace}");
                }
            }
        }

        private void SaveAss(ArrayList AssList, string Savename)
        {
            string line;
            bool Infobool = true, Stylesbool = true, Eventsbool = true;
            ArrayList Info = new ArrayList();
            ArrayList Styles = new ArrayList();
            ArrayList Event = new ArrayList();

            foreach (var i in AssList)
            {
                System.IO.StreamReader file1 = new System.IO.StreamReader(i.ToString());
                if (AssList.IndexOf(i) == 0)
                {
                    while ((line = file1.ReadLine()) != "[V4+ Styles]")
                    {
                        Info.Add(line);
                    }
                }
                else
                {
                    while ((line = file1.ReadLine()) != "[V4+ Styles]")
                    {
                        continue;
                    }
                }

                file1.ReadLine();
                while ((line = file1.ReadLine()) != "[Events]")
                {
                    if (line == "")
                        continue;
                    Styles.Add(line.Insert(line.IndexOf(','), '-' + AssList.IndexOf(i).ToString()));
                }

                file1.ReadLine();
                while ((line = file1.ReadLine()) != null)
                {
                    Event.Add(line.Insert(line.IndexOf(',', 34), '-' + AssList.IndexOf(i).ToString()));
                }
            }

            using (StreamWriter file2 =
                new StreamWriter(Savename, false, Encoding.UTF8))
            {
                foreach (var i in Info)
                {
                    file2.WriteLine(i);
                }
                file2.WriteLine("[V4+ Styles]");
                file2.WriteLine("Format: Name, Fontname, Fontsize, PrimaryColour, SecondaryColour, OutlineColour, BackColour, Bold, Italic, Underline, StrikeOut, ScaleX, ScaleY, Spacing, Angle, BorderStyle, Outline, Shadow, Alignment, MarginL, MarginR, MarginV, Encoding");
                foreach (var i in Styles)
                {
                    file2.WriteLine(i);
                }
                file2.WriteLine("");
                file2.WriteLine("[Events]");
                file2.WriteLine("Format: Layer, Start, End, Style, Name, MarginL, MarginR, MarginV, Effect, Text");
                foreach (var i in Event)
                {
                    file2.WriteLine(i);
                }
            }
        }

        private void Save_Click(object sender, EventArgs e)
        {
            ArrayList AssList = new ArrayList();
            foreach (var i in this.AssList.Items)
            {
                AssList.Add(i);
            }
            SaveAss(AssList, SaveFilename.Text);
            MessageBox.Show("success!");
        }
    }
}
