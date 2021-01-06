using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class MainFrm : Form
    {
        public MainFrm()
        {
            InitializeComponent();
        }

        private void setPreview(string text)
        {
            int maxlen = text.Length < 100 ? text.Length : 100;
            textBox1.Text = text.Substring(0, maxlen).Replace("\n", Environment.NewLine)
                + Environment.NewLine 
                + "...";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            string path = Directory.GetCurrentDirectory();
            string inputFile = path + @"\input.txt";

            string text = Clipboard.GetText();
            if (text == "")
            {
                MessageBox.Show("Empty data");
                button1.Enabled = true;
                return;
            }
            this.setPreview(text);

            StreamWriter sw = new StreamWriter(inputFile);
            sw.Write(text);
            sw.Flush();
            sw.Close();
            try
            {
                Process process = new Process();

                process.StartInfo.FileName = path + @"\php8\php.exe";
                process.StartInfo.Arguments = path + @"\serialize.php " + inputFile;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.Start();
                //* Read the output (or the error)
                text = process.StandardOutput.ReadToEnd();

                process.WaitForExit();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Lỗi xảy ra");
                return;
            }
            this.setPreview(text);
            Clipboard.SetText(text);
            MessageBox.Show("Copied to clipboard");
            button1.Enabled = true;
        }
        
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                button1_Click(null, null);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
