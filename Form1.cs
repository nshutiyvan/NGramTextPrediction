using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace NGramTextPredition
{
    
    public partial class Form1 : Form
    {
        List<String> words = new List<string>();
        Gram2 start = null;
        public long globalId = 0;
        Random rand;
        public Form1()
        {
            InitializeComponent();
            rand = new Random();
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("");
            System.Diagnostics.Debug.WriteLine("Origin string");
            System.Diagnostics.Debug.Write(txtInput.Text);
            System.Diagnostics.Debug.WriteLine("");
            System.Diagnostics.Debug.WriteLine("Manipulated:");
            System.Diagnostics.Debug.Write(removeDirty(txtInput.Text));

            int nGram = int.Parse(numDepth.Text);
            int nBranches = int.Parse(numChild.Text);

            wordTokenizer(removeDirty(txtInput.Text));
            printWordsGot();
            getNGrams(nGram, nBranches);
            string result = "";
            printHistogram(start);
            start.calcStatistics();
            Canvas cm = new Canvas(start);
            cm.generateTree();
            

        }
        private string removeDirty(string toCleanArray)
        {
            string result = "";
            var charArray = toCleanArray.ToCharArray();
            foreach (char c in charArray)
            {
                if (Char.IsLetter(c))
                {
                    result += Char.ToLower(c);
                }
                else
                {
                    result +=" ";
                }              

            }
            return result;

        }
        private void wordTokenizer(string passage)
        {

            string[] tokens = passage.Split(' ');
            foreach(string s in tokens)
            {
                words.Add(s);
            }
        }
        private void printWordsGot()
        {
            System.Diagnostics.Debug.WriteLine("Words available");
            foreach (string s in words)
            {
                System.Diagnostics.Debug.WriteLine("");
                System.Diagnostics.Debug.Write(s);
            }          
        }
        private void buildHistTree(Gram2 head)
        {
            int count = 0;
            foreach(Gram2 child in head.GetChildren())
            {
                System.Diagnostics.Debug.WriteLine("1: \t");
                System.Diagnostics.Debug.Write(child.getCounter());
                buildHistTree(child);
            }
        }
        private void printHistogram(Gram2 head)
        {
            System.Diagnostics.Debug.WriteLine("------------------------Histogram----------------------");
            buildHistTree(head);
            System.Diagnostics.Debug.WriteLine("------------------------Close histogram----------------------");
           
            
        }
        private void getNGrams(int nGram,int nBranches)
        {
            start = new Gram2("", nBranches,0,ref rand);           
            
            for (int i = 0; i < words.Count - (nGram); i++)
            {
                List<string> block = new List<string>();
                string s = words[i];
                if (s == "")
                {
                    i++;
                }
                else
                {
                    block.Add(s);
                    for (int j = 1; j < (nGram); j++)
                    {
                        if (words[i + j] != "")
                        {
                            s = words[i + j];
                            block.Add(s);
                        }

                    }
                    start.buildTree(block);
                    
                }
            }

        }       
        
    }
}
