using System;
using System.Collections;
using System.IO;
using System.Security;
using System.Text;
using System.Windows.Forms;

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
            var openFileDialog1 = new OpenFileDialog
            {
                FileName = "Select a text file",
                Filter = @"Ass files (*.ass)|*.ass",
                Title = @"Open text file",
                Multiselect = true
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                try
                {
                    foreach (var i in openFileDialog1.FileNames) AssList.Items.Add(i);
                }
                catch (SecurityException ex)
                {
                    MessageBox.Show($"Security error.\n\nError message: {ex.Message}\n\n" +
                                    $"Details:\n\n{ex.StackTrace}");
                }
        }

        private void DelAss_Click(object sender, EventArgs e)
        {
            if (AssList.SelectedIndex != -1)
                AssList.Items.RemoveAt(AssList.SelectedIndex);
        }

        private void OutputFile_Click(object sender, EventArgs e)
        {
            var saveFileDialog1 = new SaveFileDialog
            {
                FileName = "Select a text file",
                Filter = @"Ass files (*.ass)|*.ass",
                Title = @"Open text file"
            };

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
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

        private void SaveAss(ArrayList assList, string savename)
        {
            string line;
            var info = new ArrayList();
            var styles = new ArrayList();
            var Event = new ArrayList();

            foreach (var i in assList)
            {
                var file1 = new StreamReader(i.ToString());
                if (assList.IndexOf(i) == 0)
                    while ((line = file1.ReadLine()) != "[V4+ Styles]")
                        info.Add(line);
                else
                    while (file1.ReadLine() != "[V4+ Styles]")
                    {
                    }

                file1.ReadLine();
                while ((line = file1.ReadLine()) != "[Events]")
                {
                    if (line == "")
                        continue;
                    if (line != null) styles.Add(line.Insert(line.IndexOf(','), '-' + assList.IndexOf(i).ToString()));
                }

                file1.ReadLine();
                while ((line = file1.ReadLine()) != null)
                    Event.Add(line.Insert(line.IndexOf(',', 34), '-' + assList.IndexOf(i).ToString()));
            }

            using (var file2 =
                new StreamWriter(savename, false, Encoding.UTF8))
            {
                foreach (var i in info) file2.WriteLine(i);
                file2.WriteLine("[V4+ Styles]");
                file2.WriteLine(
                    "Format: Name, Fontname, Fontsize, PrimaryColour, SecondaryColour, OutlineColour, BackColour, Bold, Italic, Underline, StrikeOut, ScaleX, ScaleY, Spacing, Angle, BorderStyle, Outline, Shadow, Alignment, MarginL, MarginR, MarginV, Encoding");
                foreach (var i in styles) file2.WriteLine(i);
                file2.WriteLine("");
                file2.WriteLine("[Events]");
                file2.WriteLine("Format: Layer, Start, End, Style, Name, MarginL, MarginR, MarginV, Effect, Text");
                foreach (var i in Event) file2.WriteLine(i);
            }
        }

        private void Save_Click(object sender, EventArgs e)
        {
            var assList = new ArrayList();
            foreach (var i in AssList.Items) assList.Add(i);
            SaveAss(assList, SaveFilename.Text);
            MessageBox.Show(@"success!");
        }
    }
}