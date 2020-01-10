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
            counter = 1;
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
                Gram  newG= n;
             
                this.increaseCounter(newG.getCounter());
                //this.childeren.Add(newG);
               
               
                foreach(Gram oldChild in childeren)
                {
                    foreach(Gram newChild in newG.GetChildren())
                    {                     
                        if (oldChild.getParent() == newChild.getParent())
                        {
                            //List<Gram> tempList = oldChild.GetChildren();
                            Gram temp = oldChild;
                            temp.increaseCounter(newChild.getCounter());
                            this.childeren.Remove(oldChild);
                            this.childeren.Add(temp);
                        }
                    }
                    
                }
               
                
                

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
