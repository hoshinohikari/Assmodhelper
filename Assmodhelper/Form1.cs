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

            if (openFileDialog1.ShowDialog() != DialogResult.OK) return;
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

            if (saveFileDialog1.ShowDialog() != DialogResult.OK) return;
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

        private static bool SaveAss(IList assList, string savename)
        {
            if (assList == null || assList.Count == 0)
            {
                MessageBox.Show("未选择需要合并的字幕文件。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrWhiteSpace(savename))
            {
                MessageBox.Show("输出文件路径不能为空。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            var info = new ArrayList();
            var styles = new ArrayList();
            var events = new ArrayList();

            var assIndex = 0;
            foreach (var i in assList)
            {
                var path = i?.ToString();
                if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
                {
                    MessageBox.Show($"字幕文件不存在: {path}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                using (var file1 = new StreamReader(path))
                {
                    if (file1.Peek() == -1)
                    {
                        MessageBox.Show($"字幕文件为空: {path}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    string line;
                    var foundStyles = false;
                    if (assIndex == 0)
                    {
                        while ((line = file1.ReadLine()) != null)
                        {
                            if (line == "[V4+ Styles]")
                            {
                                foundStyles = true;
                                break;
                            }
                            info.Add(line);
                        }
                    }
                    else
                    {
                        while ((line = file1.ReadLine()) != null)
                        {
                            if (line == "[V4+ Styles]")
                            {
                                foundStyles = true;
                                break;
                            }
                        }
                    }

                    if (!foundStyles)
                    {
                        MessageBox.Show($"缺少样式区段 [V4+ Styles]: {path}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    // 跳过样式格式行
                    if (file1.ReadLine() == null)
                    {
                        MessageBox.Show($"样式格式行缺失: {path}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    var foundEvents = false;
                    while ((line = file1.ReadLine()) != null)
                    {
                        if (line == "[Events]")
                        {
                            foundEvents = true;
                            break;
                        }
                        if (string.IsNullOrWhiteSpace(line))
                            continue;
                        var commaIndex = line.IndexOf(',');
                        styles.Add(commaIndex > 0
                            ? line.Insert(commaIndex, '-' + assIndex.ToString())
                            : line);
                    }

                    if (!foundEvents)
                    {
                        MessageBox.Show($"缺少事件区段 [Events]: {path}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    // 跳过事件格式行
                    if (file1.ReadLine() == null)
                    {
                        MessageBox.Show($"事件格式行缺失: {path}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    while ((line = file1.ReadLine()) != null)
                    {
                        if (string.IsNullOrWhiteSpace(line))
                            continue;
                        var commaIndex = line.IndexOf(',', 34);
                        events.Add(commaIndex > 0
                            ? line.Insert(commaIndex, '-' + assIndex.ToString())
                            : line);
                    }
                }

                assIndex++;
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
                foreach (var i in events) file2.WriteLine(i);
            }

            return true;
        }

        private void Save_Click(object sender, EventArgs e)
        {
            var assList = new ArrayList();
            foreach (var i in AssList.Items) assList.Add(i);
            try
            {
                if (SaveAss(assList, SaveFilename.Text))
                    MessageBox.Show(@"success!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"保存失败: {ex.Message}");
            }
        }

        private void AssList_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Copy : DragDropEffects.None;
        }

        private void AssList_DragDrop(object sender, DragEventArgs e)
        {
            var fileNames = (string[]) e.Data.GetData(DataFormats.FileDrop);
            foreach (var t in fileNames)
            {
                if (!string.Equals(Path.GetExtension(t), ".ass", StringComparison.OrdinalIgnoreCase))
                    continue;
                AssList.Items.Add(t);
            }
        }

        private void AssList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AssList.Items.Count == 1)
                return;
            if (AssList.SelectedIndex == 0)
            {
                button2.Enabled = false;
                button1.Enabled = true;
            }
            else if (AssList.SelectedIndex == AssList.Items.Count - 1)
            {
                button1.Enabled = false;
                button2.Enabled = true;
            }
            else
            {
                button1.Enabled = true;
                button2.Enabled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (AssList.SelectedIndex <= 0)
                return;
            var tem = AssList.SelectedItem;
            AssList.Items[AssList.SelectedIndex] = AssList.Items[AssList.SelectedIndex - 1];
            AssList.Items[AssList.SelectedIndex - 1] = tem;
            AssList.SelectedIndex--;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (AssList.SelectedIndex < 0 || AssList.SelectedIndex >= AssList.Items.Count - 1)
                return;
            var tem = AssList.SelectedItem;
            AssList.Items[AssList.SelectedIndex] = AssList.Items[AssList.SelectedIndex + 1];
            AssList.Items[AssList.SelectedIndex + 1] = tem;
            AssList.SelectedIndex++;
        }
    }
}