using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace NGramTextPredition
{
    public partial class Form1 : Form
    {
        List<String> words = new List<string>();
        //List<Gram> grams = new List<Gram>();
        Gram2 start = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("");
            System.Diagnostics.Debug.WriteLine("Origin string");
            System.Diagnostics.Debug.Write(txtInput.Text);
            System.Diagnostics.Debug.WriteLine("");
            System.Diagnostics.Debug.WriteLine("Manipulated:");
            System.Diagnostics.Debug.Write(removeDirty(txtInput.Text));
            wordTokenizer(removeDirty(txtInput.Text));
            printWordsGot();
            getNGrams(3);

            string result = "";

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
        private void getNGrams(int nGram)
        {
            start = new Gram2("");           
            
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

        /*
        /*
        private void getNGrams(int nGram)
        {
            for (int i = 0; i < words.Count - (nGram); i++)
            {
                string s = words[i];
                if (s == "")
                {
                    i++;
                }
                else
                {
                    Gram temp = new Gram(s);
                    Gram n = isExist(temp);
                    if (n != null)
                    {
                        temp = n;
                        for (int j = 1; j < (nGram); j++)
                        {
                            if (words[i + j] != "")
                            {
                                s = words[i + j];
                                Gram k = new Gram(s);
                                n.addChildren(k);
                            }

                        }
                        int b = 0;
                        grams.Remove(temp);
                        grams.Add(n);
                    }
                    else
                    {\
                        for (int j = 1; j < (nGram); j++)
                        {
                            if (words[i + j] != "")
                            {
                                s = words[i + j];
                                Gram k = new Gram(s);
                                temp.addChildren(k);
                            }

                        }
                        grams.Add(temp);
                    }

                }
            }
        }
        */
        /*
        private Gram isExist(Gram n)
        {
            foreach (Gram g in grams)
            {
                if (g.getParent() == n.getParent())
                {
                    return g;
                    int a = 4;
                }
            }
            return null;
        }*/
        
    }
}
