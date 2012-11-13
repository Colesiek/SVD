using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {

            double[,] a1;
            double[] w;
            double[,] u;
            double[,] vt;
            double[,] przed = new double[9,9];
            double[] dlugosci = new double[9];
            double[,] odleglosci = new double[9,9];
            double[,] iloczyny = new double[9, 9];
            static double [,] a = new double[,] { {1, 0, 0, 1, 0, 0, 0, 0, 0 }, { 1, 0, 1, 0, 0, 0, 0, 0, 0 }, { 1, 1, 0, 0, 0, 0, 0, 0, 0 }, { 0, 1, 1, 0, 1, 0, 0, 0, 0 }, { 0, 1, 1, 2, 0, 0, 0, 0, 0 }, { 0, 1, 0, 0, 1, 0, 0, 0, 0 }, { 0, 1, 0, 0, 1, 0, 0, 0, 0 }, { 0, 0, 1, 1, 0, 0, 0, 0, 0 }, { 0, 1, 0, 0, 0, 0, 0, 0, 1  }, { 0, 0, 0, 0, 0, 1, 1, 1, 0 }, { 0, 0, 0, 0, 0, 0, 1, 1, 1  }, { 0, 0, 0, 0, 0, 0, 0, 1, 1 } };

        public Form1()
        {
            InitializeComponent();

        }

        private void norm()
        {
            a1 = a;
            for (int i = 0; i < 9; i++)
            {
                double suma = 0;
                for (int j = 0; j < 12; j++)
                {
                    suma += a1[j, i];
                }
                for (int j = 0; j < 12; j++)
                {
                    a1[j, i] = a1[j, i] - (suma / 9);
                }
            }
        }

        void mnozenie()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = i + 1; j < 9; j++)
                {
                    przed[i, j] = iloczyny[i, j] / dlugosci[i] * dlugosci[j];
                }
            }
        }

        void dlugosc()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    dlugosci[i] += Math.Pow(a1[j, i], 2);
                }
                dlugosci[i] += Math.Sqrt(dlugosci[i]);
            }
        }

        void iloczyn()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = i + 1; j < 9; j++)
                {
                    for (int k = 0; k < 12; k++)
                    {
                        iloczyny[i, j] += a1[k, i] * a1[k, j];
                    }
                }
            }
        }

        void odl()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    odleglosci[i, j] = dlugosci[i] - dlugosci[j];
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            alglib.rmatrixsvd(a, 12, 9, 2, 2, 0, out w, out u, out vt);


            richTextBox1.Text = "";
            richTextBox2.Text = "";
            richTextBox3.Text = "";
            richTextBox4.Text = "";


            //druk U
            for(int i=0; i<12; i++)
            {
                double t = 0;
                for(int j=0; j<9; j++)
                {
                    t = Math.Round(u[j, i], 2);
                    richTextBox1.Text += t.ToString();
                    richTextBox1.Text += " ";
                }
                richTextBox1.Text += "\n";
            }

            //druk VT
            for (int i = 0; i < 9; i++)
            {
                double t = 0;
                for (int j = 0; j < 9; j++)
                {
                    t = Math.Round(vt[i, j], 2);
                    richTextBox2.Text += t.ToString();
                    richTextBox2.Text += " ";
                }
                richTextBox2.Text += "\n";
            }

            //druk W
                for (int j = 0; j < 9; j++)
                {
                    double t = 0;
                    t = Math.Round(w[j], 2);
                    richTextBox3.Text += t.ToString();
                    richTextBox3.Text += " ";
                }


                norm();
                iloczyn();
                dlugosc();
                mnozenie();

                
                    for (int i = 0; i < 9; i++)
                    {
                        double t = 0;
                        for (int j = 0; j < 9; j++)
                        {
                            t = Math.Round(przed[j, i], 2);
                            if (t == 0)
                                richTextBox4.Text += "  ";
                            else
                            {
                                richTextBox4.Text += t.ToString();
                                richTextBox4.Text += " ";
                            }
                        }
                        richTextBox4.Text += "\n";
                    }
        }
    }
}
