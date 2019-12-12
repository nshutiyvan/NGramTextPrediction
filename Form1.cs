using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NGramTextPredition
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("");
            System.Diagnostics.Debug.Write("Print Integral value:");
            System.Diagnostics.Debug.Write(removeDirty(txtInput.Text));
        }
        private string removeDirty(string toCleanArray)
        {
            string result = "";
            var charArray = toCleanArray.ToCharArray();
            var charsToRemove = new string[] { "(", ",", ")" };
            foreach (char c in charArray)
            {
                if (Char.IsLetter(c))
                {
                    result += Char.ToLower(c);
                }
                result += " ";

            }
            return result;


        }
    }
}
