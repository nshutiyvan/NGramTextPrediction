

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Collections;

namespace NGramTextPredition
{

    class Canvas
    {
        
       
        Gram2 t;
        public Canvas(Gram2 g)
        {
            t = g;
        }
        public void generateTree()
        {

            createGraphFile("grams.dot");
            string directory = Directory.GetCurrentDirectory();
            Process dot = new Process();
            dot.StartInfo.FileName = "dot.exe";
            dot.StartInfo.Arguments = String.Format("-Tpng {0}\\grams.dot -o {1}\\grams.png", directory, directory);
            dot.Start();
            dot.WaitForExit();
        }
        public void createGraphFile(string fileName)
        {
            string path = fileName;
            try
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    Gram2 head = t;
                    string writeString = "graph calculus {\n node [fontname = \"Arial\" ]\n";
                    sw.WriteLine(writeString);
                    
                    writeString = String.Format("node{0} [ label = \" {1}\" ]\n", head.getID(),head.getParent());
                    sw.WriteLine(writeString);
                    addNodeToGraph(sw, head,head.getID());
                    writeString = "}\n";
                    sw.WriteLine(writeString);
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }  
        public void addNodeToGraph(StreamWriter w,Gram2 p,long parentInt)
        {
            if (p.ToString() != null)
            {
                if (p.GetChildren() != null)
                {
                    foreach(Gram2 g in p.GetChildren())
                    {
                        string writeString = String.Format("node{0} -- node{1}\n",p.getID(),g.getID());
                        w.WriteLine(writeString);
                        writeString = String.Format("node{0} [ label = \"{1}\" ]\n", g.getID(), g.getParent() + "\n" + g.getCounter());
                        w.WriteLine(writeString);
                        addNodeToGraph(w, g,g.getID());
                        //parentInt = childInt;
                        /*
                        
                        if(g.GetChildren() != null)
                        {
                            addNodeToGraph(w, g, childInt);
                        }
                        else
                        {
                            childInt++;
                        }
                        */

                    }
                    
                }
            }

        }

    }
}

