using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace FirstMyProj
{
    public partial class _1st_step : Form
    {
        static public double[] b0;
        static public double[] average;
        static public double[,] x1;
        static public double[] y1;
        static int variables;
        static int watches;
        static string[] lines; //= System.IO.File.ReadAllLines(@"C:\games\input.txt")
        static public double[,] ftable = new double[47, 11];
        public _1st_step()
        {
            InitializeComponent();
            openFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";

        }
        private void Enter_v_w_Click(object sender, EventArgs e)
        {
            //label1.Text = "sdjfhskdjhvsdf;lbvnszfkl;bha;lb";

            

            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = openFileDialog1.FileName;
            lines = System.IO.File.ReadAllLines(filename);

            Linear.Series.Clear();
            Linear.Series.Add("y fact");
            Linear.Series.Add("y esmitated");
            Linear.Series["y fact"].ChartType = SeriesChartType.Line;
            Linear.Series["y esmitated"].ChartType = SeriesChartType.Line;

            Exp.Series.Clear();
            Exp.Series.Add("y fact");
            Exp.Series.Add("y esmitated");
            Exp.Series["y fact"].ChartType = SeriesChartType.Line;
            Exp.Series["y esmitated"].ChartType = SeriesChartType.Line;

            Power.Series.Clear();
            Power.Series.Add("y fact");
            Power.Series.Add("y esmitated");
            Power.Series["y fact"].ChartType = SeriesChartType.Line;
            Power.Series["y esmitated"].ChartType = SeriesChartType.Line;

            Repres.Series.Clear();
            Repres.Series.Add("y fact");
            Repres.Series.Add("y esmitated");
            Repres.Series["y fact"].ChartType = SeriesChartType.Line;
            Repres.Series["y esmitated"].ChartType = SeriesChartType.Line;

            

            variables = Convert.ToInt32(VarField.Text);
            watches = Convert.ToInt32(WatchField.Text);

            double fs = 0;
            double af = 0;
            double afm = 0;
            int mod = 0;

            y1 = new double[watches];
            x1 = new double[watches, variables + 1];

            double[] y = new double[watches];
            double[,] x = new double[watches, variables + 1];

            _1st_step.b0 = new double[variables + 1];
            _1st_step.average = new double[variables + 1];

            for (int h = 0; h < watches; h++)
                x[h, 0] = 1;

            for (int i = 0; i < watches; i++)
            {
                string[] line = lines[i].Split(' ', '	');
                y[i] = Convert.ToDouble(line[0]);
                for (int j = 0; j < variables; j++)
                {
                    x[i, j + 1] = Convert.ToDouble(line[1 + j]);
                }
            }

            Regession sle = new Regession();

            double[] y_es = new double[watches];
            double[] y_es1 = new double[watches];
            double[] y_es2 = new double[watches];
            double[] y_es3 = new double[watches];

            y_es = sle.linear(x, y, variables, watches);
            double rr = sle.rr(y, y_es, average);
            fs = sle.Fstat(rr, variables, watches);
            af = sle.Afalse(y, y_es, watches);
            afm = af;
            if (afm >= af) 
                { 
                    afm = af;
                    mod = 1;
                }

            //if (variables == 1)
            //{
                
                /*double aaa = sle.sb(watches,variables);
                double sss = sle.sa(watches, variables);*/
                AnLinar.Text="коеф. детерминации: " + rr +  Environment.NewLine + "F статистика: " + fs + Environment.NewLine + "Ошибка аппроксимации: " + af;
            //}

            for (int j = 0; j < watches; j++)
            {
                Linear.Series["y esmitated"].Points.Add(y_es[j], x[j, 1]);
                Linear.Series["y fact"].Points.Add(y[j], x[j, 1]);
            }

            if (variables == 1)
            {
                y_es1 = sle.exp(x, y, variables, watches);

                rr = sle.rr(y, y_es1, average);
                fs = sle.Fstat(rr, variables, watches);
                af = sle.Afalse(y, y_es1, watches);
                if (afm >= af)
                {
                    afm = af;
                    mod = 2;
                }
                AnExp.Text = "коеф. детерминации: " + rr + Environment.NewLine + "F статистика: " + fs + Environment.NewLine + "Ошибка аппроксимации: " + af;

                for (int j = 0; j < watches; j++)
                {
                    Exp.Series["y esmitated"].Points.Add(y_es1[j], x[j, 1]);
                    Exp.Series["y fact"].Points.Add(y[j], x[j, 1]);
                }

                y_es2 = sle.power(x, y, variables, watches);

                rr = sle.rr(y, y_es2, average);
                fs = sle.Fstat(rr, variables, watches);
                af = sle.Afalse(y, y_es2, watches);
                if (afm >= af)
                {
                    afm = af;
                    mod = 3;
                }
                AnPower.Text = "коеф. детерминации: " + rr + Environment.NewLine + "F статистика: " + fs + Environment.NewLine + "Ошибка аппроксимации: " + af;

                for (int j = 0; j < watches; j++)
                {
                    Power.Series["y esmitated"].Points.Add(y_es2[j], x[j, 1]);
                    Power.Series["y fact"].Points.Add(y[j], x[j, 1]);
                }

                y_es3 = sle.repres(x, y, variables, watches);

                rr = sle.rr(y, y_es3, average);
                fs = sle.Fstat(rr, variables, watches);
                af = sle.Afalse(y, y_es3, watches);
                if (afm >= af)
                {
                    afm = af;
                    mod = 4;
                }
                AnRepres.Text = "коеф. детерминации: " + rr + Environment.NewLine + "F статистика: " + fs + Environment.NewLine + "Ошибка аппроксимации: " + af;

                for (int j = 0; j < watches; j++)
                {
                    Repres.Series["y esmitated"].Points.Add(y_es3[j], x[j, 1]);
                    Repres.Series["y fact"].Points.Add(y[j], x[j, 1]);
                }

                if (mod==1)
                    Linear.BackColor = Color.FromArgb (125, 235, 110);
                else
                    if(mod==2)
                        Exp.BackColor = Color.FromArgb(125, 235, 110);
                else 
                    if(mod==3)
                        Power.BackColor = Color.FromArgb(125, 235, 110);
                else
                    Repres.BackColor = Color.FromArgb(125, 235, 110);
            }

        }
    }

    class Regession
    {
        public double[] SLE(double[,] x, double[] y, int variables, int watches)
        {
            double[] b = new double[variables + 1];
            double a = 0;
            double c = 0;
            double[,] sl = new double[variables + 1, variables + 1];

            for (int i = 0; i <= variables; i++)
            {
                for (int j = i; j <= variables; j++)
                {
                    for (int g = 0; g < watches; g++)
                    {
                        a += x[g, i] * x[g, j];
                        if (i == j) c += x[g, i] * y[g];
                    }
                    sl[i, j] = sl[j, i] = a / watches;
                    if (i == j) b[i] = c / watches;
                    a = 0;
                    c = 0;
                }

            }

            _1st_step.average[0] = b[0];
            for (int i = 1; i <= variables; i++)
                _1st_step.average[i] = sl[0, i];

            for (int i = 0; i <= variables; i++)
            {
                double d = sl[i, i];

                for (int j = 0; j <= variables; j++)
                {
                    sl[i, j] /= d;
                }
                b[i] /= d;

                for (int j = 0; j <= variables; j++)
                {
                    if (j == i) continue;

                    b[j] -= b[i] * sl[j, i];

                    double p = sl[j, i];
                    for (int k = 0; k <= variables; k++)
                    {
                        sl[j, k] -= sl[i, k] * p;
                    }

                }
            }

            return b;
        }
        public double[] linear(double[,] x, double[] y, int variables, int watches)
        {
            double[] p = new double[watches];

            Regession sle = new Regession();
            double[] b = sle.SLE(x, y, variables, watches);

            for (int i = 0; i < variables + 1; i++)
                _1st_step.b0[i] = b[i];

            for (int i = 0; i < watches; i++)
            {
                _1st_step.y1[i] = y[i];
            }
            for (int i = 0; i < watches; i++)
            {
                _1st_step.x1[i, 1] = x[i, 1];
            }


            for (int i = 0; i < watches; i++)
            {
                p[i] = 0;
                for (int j = 0; j < b.Length; j++)
                {
                    p[i] += x[i, j] * b[j];
                }
            }
            return p;
        }
        public double[] exp(double[,] x, double[] y, int variables, int watches)
        {
            double[] p = new double[watches];
            for (int i = 0; i < watches; i++)
            {
                _1st_step.y1[i] = Math.Log(y[i]);
            }

            for (int i = 0; i < watches; i++)
            {
                _1st_step.x1[i, 1] = x[i, 1];
            }

            Regession sle = new Regession();
            double[] b = sle.SLE(x, _1st_step.y1, variables, watches);

            for (int i = 0; i < variables + 1; i++)
                _1st_step.b0[i] = b[i];

            b[0] = Math.Exp(b[0]);

            for (int i = 0; i < watches; i++)
            {
                p[i] = b[0] * Math.Exp(b[1] * x[i, 1]);
            }
            return p;
        }
        public double[] power(double[,] x, double[] y, int variables, int watches)
        {
            double[] p = new double[watches];
            for (int i = 0; i < watches; i++)
            {
                _1st_step.y1[i] = Math.Log(y[i]);
            }

            for (int i = 0; i < watches; i++)
            {
                _1st_step.x1[i, 1] = x[i, 1];
            }

            Regession sle = new Regession();
            double[] b = sle.SLE(x, _1st_step.y1, variables, watches);

            for (int i = 0; i < variables + 1; i++)
                _1st_step.b0[i] = b[i];

            for (int i = 0; i <= variables; i++)
                b[i] = Math.Exp(b[i]);

            for (int i = 0; i < watches; i++)
            {
                p[i] = b[0] * Math.Pow(b[1], x[i, 1]);
            }
            return p;
        }
        public double[] repres(double[,] x, double[] y, int variables, int watches)
        {
            double[] p = new double[watches];
            double[,] o = new double[watches, variables + 1];
            for (int i = 0; i < watches; i++)
            {
                _1st_step.y1[i] = Math.Log(y[i]);
            }
            for (int i = 0; i < watches; i++)
            {
                for (int j = 0; j <= variables; j++)
                    _1st_step.x1[i, j] = Math.Log(x[i, j]);
                _1st_step.x1[i, 0] = 1;
            }
            Regession sle = new Regession();
            double[] b = sle.SLE(_1st_step.x1, _1st_step.y1, variables, watches);

            for (int i = 0; i < variables + 1; i++)
                _1st_step.b0[i] = b[i];

            b[0] = Math.Exp(b[0]);

            for (int i = 0; i < watches; i++)
            {
                p[i] = b[0] * Math.Pow(x[i, 1], b[1]);
            }
            return p;
        }

        public double sum(double[] y1_, double[] averages)
        {
            double s = 0;
            for (int i = 0; i < y1_.Length; i++)
                s += (y1_[i] - averages[0]) * (y1_[i] - averages[0]);
            return s;
        }
        public double sum(double[,] x1_)
        {
            double s = 0;
            for (int i = 0; i < 21; i++)
                s += x1_[i, 1] * x1_[i, 1];
            return s;
        }

        /*public double sa(int watches,int variables)
        {
            double s = 0;
            Regession sle = new Regession();

            s = Math.Pow(sle.sum(_1st_step.y1, _1st_step.average) / (watches - variables - 1), (1 / 2)) * sle.sum(_1st_step.x1) / (Math.Pow(sle.sum(_1st_step.x1) / watches, (1 / 2))*watches);
            return s;
        }

        public double sb(int watches, int variables)
        {
            double s = 0;
            Regession sle = new Regession();

            s = Math.Pow(sle.sum(_1st_step.y1, _1st_step.average) / (watches - variables - 1), (1 / 2)) / (Math.Pow(watches, (1 / 2)) * Math.Pow(sle.sum(_1st_step.x1)/watches, (1 / 2)));
            return s;
        }*/

        public double rr(double[] y, double[] y1, double[] average)
        {
            double s = 0;
            double s1 = 0;
            for (int i = 0; i < 21; i++)
            {
                s1 += (y1[i] - average[0]) * (y1[i] - average[0]);
                s += (y[i] - average[0]) * (y[i] - average[0]);
            }
            s = s1 / s;
            return s;
        }

        public double Fstat(double rr, int variables, int watches)
        {
            double s = 0;
            variables += 1;
            s = rr * (watches - variables) / (1 - rr) / ((variables - 1));
            return s;
        }

        public double Afalse(double[] y, double[] y1, int watches)
        {
            double s = 0;
            
            for (int i = 0; i < watches; i++)
            {
                s += (y[i] - y1[i]) * (y[i] - y1[i]) / y[i];
            }

            s /= watches;

            return s;
        }
    }
}      
   