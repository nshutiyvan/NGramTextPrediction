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
        int numberOfChild;
        int counter;
        string parent;
        double probability;
        long myId=0;
        Random rand;
        public Gram2(string s,int nChild,long val,ref Random rt)
        {
            counter = 1;
            parent = s;
            childeren = new List<Gram2>();
            numberOfChild = nChild;
            probability = 0.0;
            myId = val;
            rand = rt;

        }
        public int getCounter()
        {
            return counter;
        }
        public long getId()
        {
            return myId;
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
        public string getParent()
        {
            return parent;
        }
        public void setID(long id)
        {
            myId = id;
        }
        public long getID()
        {
            return myId;
        }
        public void buildTree(List<string> words)
        {
            if(childeren.Count < numberOfChild)
            {
                if (words.Count > 0)
                {
                    string first = words.FirstOrDefault();
                    words.RemoveAt(0);
                    int random = rand.Next();
                    Gram2 child = new Gram2(first, numberOfChild,random,ref rand);
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

        }
        public void setStatistics()
        {
            foreach(Gram2 g in childeren)
            {
                g.setProbability(this);
            }
        }
        private void setProbability(Gram2 head)
        {
            this.probability = this.getCounter() / head.getCounter();
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
