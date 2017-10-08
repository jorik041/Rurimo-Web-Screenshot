using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace RurimoWebController
{
    public partial class MainForm : Form
    {
        String FolderPath;

        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FolderPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

            // set default settings
            textBox1.Text = FolderPath;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            radioButton1.Checked = true;
        }

        // ScreenShot
        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                int rand = new Random().Next();

                if (textBox4.Text.Contains("http://") || textBox4.Text.Contains("https://"))
                {
                    if (radioButton1.Checked)
                    {
                        Bitmap thumbnail = GenerateScreenshot(textBox4.Text);
                        thumbnail.Save(FolderPath + "\\" + rand + ".png", ImageFormat.Png);
                        MessageBox.Show("successfully captured!", "notification");
                    }
                    else if (radioButton2.Checked)
                    {
                        if (textBox2.Text != "" && textBox3.Text != "")
                        {
                            Bitmap thumbnail = GenerateScreenshot(textBox4.Text, Int32.Parse(textBox2.Text), Int32.Parse(textBox3.Text));
                            thumbnail.Save(FolderPath + "\\" + rand + ".png", ImageFormat.Png);
                            MessageBox.Show("successfully captured!", "notification");
                        }
                        else
                            MessageBox.Show("please type integer on width and height", "notification");
                    }
                }
                else
                    MessageBox.Show("type correct web address!", "notification");
            }
            catch
            {
                MessageBox.Show("occured error!", "notification");
            }
        }

        // Not Setting Size
        public Bitmap GenerateScreenshot(string url)
        {
            // This method gets a screenshot of the webpage
            // rendered at its full size (height and width)
            return GenerateScreenshot(url, -1, -1);
        }

        // Set Size
        public Bitmap GenerateScreenshot(string url, int width, int height)
        {
            // Load the webpage into a WebBrowser control
            WebBrowser wb = new WebBrowser();
            wb.ScrollBarsEnabled = false;
            wb.ScriptErrorsSuppressed = true;
            wb.Navigate(url);
            while (wb.ReadyState != WebBrowserReadyState.Complete) { Application.DoEvents(); }

            // Set the size of the WebBrowser control
            wb.Width = width;
            wb.Height = height;

            if (width == -1)
            {
                // Take Screenshot of the web pages full width
                wb.Width = wb.Document.Body.ScrollRectangle.Width;
            }

            if (height == -1)
            {
                // Take Screenshot of the web pages full height
                wb.Height = wb.Document.Body.ScrollRectangle.Height;
            }

            // Get a Bitmap representation of the webpage as it's rendered in the WebBrowser control
            Bitmap bitmap = new Bitmap(wb.Width, wb.Height);
            wb.DrawToBitmap(bitmap, new Rectangle(0, 0, wb.Width, wb.Height));
            wb.Dispose();

            return bitmap;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                textBox2.Enabled = false;
                textBox3.Enabled = false;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                textBox2.Enabled = true;
                textBox3.Enabled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)) && e.KeyChar != 8 && e.KeyChar != 45 && e.KeyChar != 46)
            {
                e.Handled = true;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)) && e.KeyChar != 8 && e.KeyChar != 45 && e.KeyChar != 46)
            {
                e.Handled = true;
            }
        }

        // Get Path
        private void button2_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() != DialogResult.Cancel)
            {
                textBox1.Text = folderBrowserDialog1.SelectedPath;
            }
        }
    }
}
