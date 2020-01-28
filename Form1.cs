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
        Random rand,childRand;
        string resultString;
        Gram2 selectedG = null;
        
        public Form1()
        {
            InitializeComponent();
            rand = new Random();
            childRand = new Random();
            resultString = "";
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
           
            start.calcStatistics();
            Canvas cm = new Canvas(start);
            cm.generateTree();
            if(start != null)
            {
                selectedG = start;
                List<double[]> histogram=makeHistogram(nGram);
                printHistogram(histogram,nGram,nBranches);
            }
            

        }
        private void printHistogram(List<double[]> histogram,int depth,int branches)
        {
            double[] row = new double[histogram.Count];
            int index = 0;
            string s = "" + (index + 1) + "|";
            System.Diagnostics.Debug.WriteLine("");

            for (int i = 1; i <= branches; i++)
            {
                System.Diagnostics.Debug.Write("\t"+i);
            }
            System.Diagnostics.Debug.WriteLine("");
            for (double i = 0; i < histogram.Count / 2; i++)
            {
                System.Diagnostics.Debug.Write("---------");
            }
            
            while (index < depth)
            {
                foreach (double[] d in histogram)
                {
                    for (int i = 0; i < d.Length; i++)
                    {
                        if (i == index)
                        {
                            s += "\t" + d[i];
                            break;
                        }
                    }

                }
                s+="|";
                System.Diagnostics.Debug.WriteLine("");
                System.Diagnostics.Debug.WriteLine(s);
                for (double i = 0; i < histogram.Count / 2; i++)
                {
                    System.Diagnostics.Debug.Write("---------");
                }
                s = "";
                index++;
                s = ""+(index+1)+"|";
                
            }     

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

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            
            if(start != null)
            {
               
                getNRandChild(selectedG);

            }
            
        }
        private List<double[]> makeHistogram(int depth)
        {
            if(start != null)
            {
                /*
                int index = 1;
                System.Diagnostics.Debug.WriteLine("------------------------Histogram----------------------");
                System.Diagnostics.Debug.Write(" "+index+"|");
                */
                int index = 0;
                List<double[]> opa = new List<double[]>();
                foreach (Gram2 g in start.GetChildren())
                {
                    double[] data = new double[depth];
                    data[index] = g.getCounter();
                    
                    getNHistogram(g, index, depth-1,ref data);
                   
                    opa.Add(data);
                }
                return opa;
            }
            return null;
        }
        private void getNHistogram(Gram2 head,int index,int depth,ref double[] data)
        {
            /*
            while(index <= depth)
            {

                index++;
                int counter = 0;
                foreach (Gram2 g in head.GetChildren())
                {                   
                    getNHistogram(g, index, depth, ref data);
                    counter += g.getChildrenCount();
                }
                data[index] = counter;
            }
            */
            int counter = 0;
            if(index < depth)
            {
                data[index] = counter;             
            }
            else
            {
                index++;
                foreach (Gram2 g in head.GetChildren())
                {

                   counter +=g.getChildrenCount();
                    getNHistogram(g, index, depth, ref data);

                }
            }


        }
        private void getNRandChild(Gram2 g)
        {
            Gram2 randomG = generateRandomChild(g);
            if(randomG != null)
            {
                resultString += " " + randomG.getParent();
                resultTxt.Text = resultString;
                selectedG = randomG;
            }
            else
            {
                randomG = start.isExist(g);
                if(randomG != null)
                {
                    selectedG = randomG;
                }
                else
                {
                    selectedG = start;
                }
            }
            
        }
        private Gram2 generateRandomChild(Gram2 g)
        {
            return g.getRandomChild(ref childRand);
        }
       
    }
}
