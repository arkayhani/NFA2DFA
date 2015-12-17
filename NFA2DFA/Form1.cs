using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NFA2DFA
{
    public partial class Form1 : Form
    {

        int alphanum = 10;
        List<link> nfa = new List<link>();
        List<link> newnfa = new List<link>();
        List<link> dfa = new List<link>();
        class link
        {
            public string start;
            public string end;
            public string aplha;
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            link temp = new link();
            temp.start = textBox1.Text;
            temp.aplha = textBox2.Text;
            temp.end = textBox3.Text;
            nfa.Add(temp);

            listBox1.Items.Add(textBox1.Text + " ===" + textBox2.Text + "===> " + textBox3.Text);
            textBox1.Text="";
            textBox2.Text="";
            textBox3.Text="";
            button2.Enabled = true;
          
        }
        string landaend(string start)
        {
            string resualt = "";
            foreach (var item in nfa)
            {
                if (item.start == start && item.aplha == "landa")
                {
                    resualt += item.end + landaend(item.end);
                }
            }
            return resualt;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            link tempn = new link();
            tempn.start = "s0"+landaend("s0");
            newnfa.Add(tempn);
           
            while (newnfa.Count > 0)
            {
                foreach (var item in newnfa)
                {
                    for (int i = 0; i < alphanum; i++)
                    {
                        string alphatemp = (Convert.ToChar('a' + i)).ToString();
                        link temp = new link();
                        string end = "";
                        for (int j = 0; j < item.start.Length; j+=2)
                        {
                            string st = item.start.Substring(j, 2);
                            foreach (var item2 in nfa)
                            {
                                if (st == item2.start && alphatemp == item2.aplha)
                                {
                                    end += item2.end + landaend(item2.end);
                                }
                            }
                        } if (end != "")
                        {
                            temp.start = item.start;
                            temp.aplha = alphatemp;
                            temp.end = end;
                            dfa.Add(temp);
                            Boolean flag = true;
                            foreach (var i3 in dfa)
                            {
                                if (i3.start == item.start)
                                    flag = false;
                            }
                            if (flag)
                            {
                                link tempnf = new link();
                                tempnf.start = item.start;
                                newnfa.Add(tempnf);
                            }
                            
                        }


                    }
                    newnfa.Remove(item);
                    break;
                }
            }
            foreach (var item in dfa)
            {
                listBox2.Items.Add(item.start + " ===" + item.aplha + "===> " + item.end);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            alphanum = Convert.ToInt32(textBox4.Text);
            button1.Enabled = true;
            button3.Enabled = false;
        }
    }
}
