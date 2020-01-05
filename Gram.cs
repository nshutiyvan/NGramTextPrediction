using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGramTextPredition
{ 
    class Gram
    {
        List<Gram> childeren;
        string parent;
        int counter;
        public Gram(string s)
        {
            childeren = new List<Gram>();
            parent = s;
            counter = 0;
        }
        public Gram getHead()
        {
            return childeren[0];
        }
        public List<Gram> GetChildren()
        {
           
            return childeren;
        }
        public void addChildren(Gram n)
        {
            if(this.getParent() == n.getParent())
            {
                this.counter++;

            }
            else
            {
                childeren.Add(n);
            }          
        }
        public  string getParent()
        {
            return parent;
        }
        public override string ToString()
        {
            string result = parent;
            foreach (Gram n in childeren)
                result +=" "+n.ToString();
            return result;
        }

    }
}
