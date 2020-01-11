using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGramTextPredition
{
    class Gram2
    {
        List<Gram2> childeren;
        int counter;
        string parent;
        public Gram2(string s)
        {
            counter = 1;
            parent = s;
            childeren = new List<Gram2>();
        }
        public int getCounter()
        {
            return counter;
        }
        public void increaseCounter()
        {
            this.counter++;
        }
        public void increaseCounter(int n)
        {
            this.counter += n;
        }
        public Gram2 getHead()
        {
            return this;
        }
        public List<Gram2> GetChildren()
        {

            return childeren;
        }
        private string getParent()
        {
            return parent;
        }
        public void buildTree(List<string> words)
        {
            if (words.Count > 0)
            {
                string first = words.FirstOrDefault();
                //words.RemoveAt(0);
                Gram2 child = new Gram2(first);
                Gram2 start = isExist(child);
                if (start == null)
                {
                    
                    child.buildTree(words);
                    childeren.Add(child);

                }
                else
                {

                    start.increaseCounter();
                    if (words.Count > 0)
                    {
                        start.buildTree(words);
                    }
                }
            }


        }      
       private Gram2 isExist(Gram2 newParent)
       {
           foreach (Gram2 g in childeren)
           {
               if (g.getParent() == newParent.getParent())
               {
                   return g;
               }
           }
           return null;
       }
        public override string ToString()
        {
            string s = parent;
            foreach (Gram2 g in childeren)
                s += " " + g.ToString();
            return s;
        }

    }
}
