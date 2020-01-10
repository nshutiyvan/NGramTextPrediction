using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGramTextPredition
{
    class Tree
    {
        Gram head =null;
        List<Gram> children = null;
        List<string> words = null;
        int index = 0;
        public Tree(List<String> val)
        {
            head = new Gram("");
            children = new List<Gram>();
            words = val;

        }

        private string[] getNlogy(int n)
        {
            string[] nLogy = new string[n];
            while (index < (words.Count - n)){
                string s = words[index];
                if (s == "")
                {
                    index++;
                    getNlogy(n);
                }
                else
                {               
                    for (int j = 1; j < (n); j++)
                    {

                        if (words[index + j] != "")
                        {
                            s = words[index + j];
                            nLogy[index] = s;
                            index++;
                        }

                    }
                }
                
            }
            return nLogy;
        }
        private Gram isExist(Gram n)
        {
            List<Gram> grams = head.GetChildren();
            foreach (Gram g in grams)
            {
                if (g.getParent() == n.getParent())
                {
                    return g;
                }
            }
            return null;
        }
        private void constructTree(int n)
        {
            addGram(getNlogy(n));
        }
        private void addGram(string[] nLogy)
        {
            Gram n = new Gram(nLogy[1]);
            Gram newG = isExist(n);
            if(newG == null)
            {
                children.Add(n);
            }
        }
    }
}
