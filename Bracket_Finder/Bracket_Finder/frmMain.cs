#region Headers
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
#endregion 

namespace Bracket_Finder
{
    #region frmMain Class
    public partial class frmMain : Form
    {

        #region Varibales 
        
        #region Private

        private const string logpath = "logs";

        #endregion

        #region Public 
        #endregion

        #endregion

        #region Public Functions
        public frmMain()
        {
            InitializeComponent();
        }

        #endregion

        #region Private Functions
        private void btnPaste_Click(object sender, EventArgs e)
        {
            txtCode.Paste();
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtCode.Text.Equals(string.Empty))
            {
                MessageBox.Show("Please paste or open your pawn File!");
            }
            else 
            {
                if (!(txtBrackets.Text.Equals(String.Empty)))
                {
                    txtBrackets.Text = "";
                }
                SearchBraces(txtCode.Text);
            }
        }
        private void SearchBraces(string code) 
        {
            string line;
            int counter = 0;
            int opbrace = 0;
            int clbrace = 0;
            /*int c = 0;
            int o = 0;*/
            using (StringReader sr = new StringReader(code)) 
            {
                while((line = sr.ReadLine()) != null)
                {
                    counter++;
                    int index = line.IndexOf("{");
                    int secindex = line.IndexOf("}");
                    if (index != -1) 
                    {
                        opbrace++;
                        AT("Opening Curly Barce found at line: " + counter);
                        /*o = 2;
                        if (!(c == 2)) 
                        {
                            AT("Error closing brace not found at line: " + counter);
                        }*/
                    }
                    if (secindex != -1) 
                    {
                        clbrace++;
                        AT("Closing Curly Barce found at line: " + counter);
                        //c = 2;
                    }
                }
            }
            AT("\n\n\n\nTotal of " + opbrace.ToString() + " opening braces found");
            AT("Total of " + clbrace.ToString() + " closing braces found");
        }
        private void SaveLog(string filename) 
        {
            string path = Path.Combine(Application.StartupPath, logpath);
            if (!(Directory.Exists(path)))
            {
                Directory.CreateDirectory(path);
            }
            else 
            {
                string content = txtBrackets.Text;
                string secpath = Path.Combine(path, filename + ".txt");
                if (File.Exists(secpath))
                {
                    throw new ArgumentException("Error: Log file " + filename + " already exist on the logs directory. Please remove it", "File Existance Error");
                }
                else 
                {
                    FileStream fs;
                    fs = File.Create(secpath);
                    fs.Close();
                    File.WriteAllText(secpath, content);
                }
            }
        }
        private void AT(string text) 
        {
            txtBrackets.Text += Environment.NewLine + text;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            string pathfile = IB("Please input the filename", "File Name", "Please input the filename...");
            SaveLog(pathfile);
        }
        private static string IB(string prompt, string title, string defaultValue)
        {
            InputBox ib = new InputBox();
            ib.FormPrompt = prompt;
            ib.FormCaption = title;
            ib.DefaultValue = defaultValue;
            ib.ShowDialog();
            string s = ib.InputResponse;
            ib.Close();
            return s;
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtCode.Clear();
        }
        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            DialogResult res;
            ofd.Title = "Open Script";
            ofd.Filter = "All Files|*.*";
            res = ofd.ShowDialog();
            if (res == DialogResult.OK) 
            {
                string cotent = File.ReadAllText(ofd.FileName);
                txtCode.Clear();
                txtCode.Text = cotent;
            }
        }
        #endregion
    }
    #endregion
}
