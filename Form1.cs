using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Globalization;
using System.IO;

namespace Funkcje
{
    public partial class Form1 : Form
    {
        // zmienne
        Graphics uklad, siatka;
        Pen czarny_pen = new Pen(Color.Black,2);
        Pen siatka_pen = new Pen(Color.Black, 1.5F);
        string dzialanie, dzialanie_tekst;
        double precyzja = 1;
        const int grubosc = 2;
        aktywne_dzialanie active = aktywne_dzialanie.red;
        bool ScaleNumbers = false;

        //dzialanie
        Dzialanie red = new Dzialanie(Color.Red, grubosc, aktywne_dzialanie.red);
        Dzialanie blue = new Dzialanie(Color.Blue, grubosc, aktywne_dzialanie.blue);
        Dzialanie green = new Dzialanie(Color.Green, grubosc, aktywne_dzialanie.green);
        Dzialanie yellow = new Dzialanie(Color.Gold, grubosc, aktywne_dzialanie.yellow);
        Dzialanie pink = new Dzialanie(Color.Pink, grubosc, aktywne_dzialanie.pink);
        Dzialanie brown = new Dzialanie(Color.Brown, grubosc, aktywne_dzialanie.brown);

        // form1

        public Form1()
        {
            InitializeComponent();
            CreateGraph();
            HideScaleNumbers();

        }
        //functions

        private void ShowScaleNumbers()
        {
            l1.Visible = true;
            l2.Visible = true;
            l3.Visible = true;
            l4.Visible = true;
            l5.Visible = true;
            l6.Visible = true;
            l7.Visible = true;
            l8.Visible = true;
            l9.Visible = true;
            l10.Visible = true;
            l11.Visible = true;
            l12.Visible = true;
            l13.Visible = true;
            l14.Visible = true;
            l15.Visible = true;
            l16.Visible = true;
            l17.Visible = true;
            l18.Visible = true;
            l19.Visible = true;
            l20.Visible = true;
            l21.Visible = true;
            l22.Visible = true;
            l23.Visible = true;
            l24.Visible = true;
            l25.Visible = true;
            l26.Visible = true;
            l27.Visible = true;
            l28.Visible = true;
            l1.Text = (-8.0 * precyzja).ToString();
            l2.Text = (-7.0 * precyzja).ToString();
            l3.Text = (-6.0 * precyzja).ToString();
            l4.Text = (-5.0 * precyzja).ToString();
            l5.Text = (-4.0 * precyzja).ToString();
            l6.Text = (-3.0 * precyzja).ToString();
            l7.Text = (-2.0 * precyzja).ToString();
            l8.Text = (-1.0 * precyzja).ToString();
            l9.Text = (1.0 * precyzja).ToString();
            l10.Text = (2.0 * precyzja).ToString();
            l11.Text = (3.0 * precyzja).ToString();
            l12.Text = (4.0 * precyzja).ToString();
            l13.Text = (5.0 * precyzja).ToString();
            l14.Text = (6.0 * precyzja).ToString();
            l15.Text = (7.0 * precyzja).ToString();
            l16.Text = (8.0 * precyzja).ToString();
            l17.Text = (-6.0 * precyzja).ToString();
            l18.Text = (-5.0 * precyzja).ToString();
            l19.Text = (-4.0 * precyzja).ToString();
            l20.Text = (-3.0 * precyzja).ToString();
            l21.Text = (-2.0 * precyzja).ToString();
            l22.Text = (-1.0 * precyzja).ToString();
            l23.Text = (1.0 * precyzja).ToString();
            l24.Text = (2.0 * precyzja).ToString();
            l25.Text = (3.0 * precyzja).ToString();
            l26.Text = (4.0 * precyzja).ToString();
            l27.Text = (5.0 * precyzja).ToString();
            l28.Text = (6.0 * precyzja).ToString();
        }                        // pokazuje liczby skali w ukladzie

        private void HideScaleNumbers()
        {
            l1.Visible = false;
            l2.Visible = false;
            l3.Visible = false;
            l4.Visible = false;
            l5.Visible = false;
            l6.Visible = false;
            l7.Visible = false;
            l8.Visible = false;
            l9.Visible = false;
            l10.Visible = false;
            l11.Visible = false;
            l12.Visible = false;
            l13.Visible = false;
            l14.Visible = false;
            l15.Visible = false;
            l16.Visible = false;
            l17.Visible = false;
            l18.Visible = false;
            l19.Visible = false;
            l20.Visible = false;
            l21.Visible = false;
            l22.Visible = false;
            l23.Visible = false;
            l24.Visible = false;
            l25.Visible = false;
            l26.Visible = false;
            l27.Visible = false;
            l28.Visible = false;
        }                        // ukrywa liczby skali w ukladzie

        private void CreateGraph()
        {
            red.graphics = this.CreateGraphics();
            blue.graphics = this.CreateGraphics();
            green.graphics = this.CreateGraphics();
            yellow.graphics = this.CreateGraphics();
            pink.graphics = this.CreateGraphics();
            brown.graphics = this.CreateGraphics();
        }                             // CreateGraphics

        private void change_f_color(aktywne_dzialanie k)
        {
            if (k == aktywne_dzialanie.red) label5.BackColor = Color.Red;
            else if (k == aktywne_dzialanie.blue) label5.BackColor = Color.Blue;
            else if (k == aktywne_dzialanie.green) label5.BackColor = Color.Green;
            else if (k == aktywne_dzialanie.pink) label5.BackColor = Color.Pink;
            else if (k == aktywne_dzialanie.yellow) label5.BackColor = Color.Gold;
            else if (k == aktywne_dzialanie.brown) label5.BackColor = Color.Brown;
        }       // zmiana koloru label6

        private void rysuj_uklad()
        {
            uklad = this.CreateGraphics();
            uklad.TranslateTransform(700, 350);
            uklad.DrawLine(czarny_pen, 0, -500, 0, 500);
            uklad.DrawLine(czarny_pen, -400, 0, 500, 0);
            for (int i = -400; i < 500; i += 50)
            {
                uklad.DrawLine(czarny_pen, i, -3, i, 3);
                uklad.DrawLine(czarny_pen, -3, i, 3, i);
            }
        }                             // rysuje uklad wspolrzednych

        private void rysuj_siatke()
        {
            siatka = this.CreateGraphics();
            siatka.TranslateTransform(700, 350);
            for (int i = -400; i < 500; i += 50)
            {
                siatka.DrawLine(siatka_pen, i, -500, i, 500);
                siatka.DrawLine(siatka_pen, -400, i, 500, i);
            }
        }                            // rysuje siatke

        private void color(ref Dzialanie dzialanie)
        {
            active = dzialanie.GetColor();
            change_f_color(active);
            if (dzialanie.GetNarysowane() == true) label6.Text = dzialanie.GetLuzdzkie();
            else label6.Text = "";
        }            // zmiana koloru, button red,blue, green... onlick

        private void submit(Dzialanie d)
        {
            if(d.GetNarysowane()==false)
            {
                rysuj_uklad();
                d.SetCenter();
                d.SetEquotion(dzialanie_tekst, dzialanie);
                if (d.EquotionValid())
                {
                    d.SetPrecyzja(precyzja);
                    d.SetONP();
                    d.Draw2();
                    label6.Text = d.GetLuzdzkie();
                }
                else
                {
                    label3.Text = "Error";
                }
            } 
        }                       // submit onclick

        private void SetPrecyzja()
        {
            double p;
            if(double.TryParse(textBox1.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out p))
            {
                precyzja = p;
                if (ScaleNumbers == true)
                {
                    ShowScaleNumbers();
                }
            }
            else
            {
                precyzja = 1;
            }
        }                             // ustawia skale

        private void Redraw()                                      // maluje jeszcze raz funkcje
        {
            red.graphics.Clear(SystemColors.Control);
            CreateGraph();
            rysuj_uklad();
            if (red.GetNarysowane())
            {
                red.SetCenter();
                red.Draw2();
            }
            if (blue.GetNarysowane())
            {
                blue.SetCenter();
                blue.Draw2();
            }
            if (green.GetNarysowane())
            {
                green.SetCenter();
                green.Draw2();
            }
            if (pink.GetNarysowane())
            {
                pink.SetCenter();
                pink.Draw2();
            }
            if (yellow.GetNarysowane())
            {
                yellow.SetCenter();
                yellow.Draw2();
            }
            if (brown.GetNarysowane())
            {
                brown.SetCenter();
                brown.Draw2();
            }
        }




        // buttons

        private void button9_Click(object sender, EventArgs e)
        {
            dzialanie = dzialanie + button9.Text;
            dzialanie_tekst = dzialanie_tekst + button9.Text;
            
            label2.Text = dzialanie_tekst;
        }         // 1

        private void button8_Click(object sender, EventArgs e)
        {
            dzialanie = dzialanie + button8.Text;
            dzialanie_tekst = dzialanie_tekst + button8.Text;
            
            label2.Text = dzialanie_tekst;
        }         // 2

        private void button7_Click(object sender, EventArgs e)
        {
            dzialanie = dzialanie + button7.Text;
            dzialanie_tekst = dzialanie_tekst + button7.Text;
            
            label2.Text = dzialanie_tekst;
        }         // 3
          
        private void button6_Click(object sender, EventArgs e)
        {
            dzialanie = dzialanie + button6.Text;
            dzialanie_tekst = dzialanie_tekst + button6.Text;
            
            label2.Text = dzialanie_tekst;
        }         // 4

        private void button5_Click(object sender, EventArgs e)
        {
            dzialanie = dzialanie + button5.Text;
            dzialanie_tekst = dzialanie_tekst + button5.Text;
            
            label2.Text = dzialanie_tekst;
        }         // 5

        private void button4_Click(object sender, EventArgs e)
        {
            dzialanie = dzialanie + button4.Text;
            dzialanie_tekst = dzialanie_tekst + button4.Text;
            
            label2.Text = dzialanie_tekst;
        }         // 6

        private void button1_Click(object sender, EventArgs e)
        {
            dzialanie = dzialanie + button1.Text;
            dzialanie_tekst = dzialanie_tekst + button1.Text;
            
            label2.Text = dzialanie_tekst;
        }         // 7

        private void button2_Click(object sender, EventArgs e)
        {
            dzialanie = dzialanie + button2.Text;
            dzialanie_tekst = dzialanie_tekst + button2.Text;
            
            label2.Text = dzialanie_tekst;
        }         // 8

        private void button3_Click(object sender, EventArgs e)
        {
            dzialanie = dzialanie + button3.Text;
            dzialanie_tekst = dzialanie_tekst + button3.Text;
            
            label2.Text = dzialanie_tekst;
        }         // 9

        private void button12_Click(object sender, EventArgs e)
        {
            dzialanie = dzialanie + button12.Text;
            dzialanie_tekst = dzialanie_tekst + button12.Text;
            
            label2.Text = dzialanie_tekst;
        }        // +

        private void button13_Click(object sender, EventArgs e)
        {
            dzialanie = dzialanie + button13.Text;
            dzialanie_tekst = dzialanie_tekst + button13.Text;
            
            label2.Text = dzialanie_tekst;
        }        // -

        private void button11_Click(object sender, EventArgs e)
        {
            dzialanie = dzialanie + "*";
            dzialanie_tekst = dzialanie_tekst + "·";
            
            label2.Text = dzialanie_tekst;
        }        // *

        private void button14_Click(object sender, EventArgs e)
        {
            dzialanie = dzialanie + "/";
            dzialanie_tekst = dzialanie_tekst + ":";
            
            label2.Text = dzialanie_tekst;
        }        // :

        private void button20_Click(object sender, EventArgs e)
        {
            dzialanie = dzialanie + "s";
            dzialanie_tekst = dzialanie_tekst + "sin";
            
            label2.Text = dzialanie_tekst;
        }        // sin

        private void button19_Click(object sender, EventArgs e)
        {
            dzialanie = dzialanie + "c";
            dzialanie_tekst = dzialanie_tekst + "cos";
            
            label2.Text = dzialanie_tekst;
        }        // cos

        private void button18_Click(object sender, EventArgs e)
        {
            dzialanie = dzialanie + "t";
            dzialanie_tekst = dzialanie_tekst + "tg";
            
            label2.Text = dzialanie_tekst;
        }        // tg

        private void button24_Click(object sender, EventArgs e)
        {
            dzialanie = dzialanie + button24.Text;
            dzialanie_tekst = dzialanie_tekst + button24.Text;
            
            label2.Text = dzialanie_tekst;
        }        // ctg

        private void button25_Click(object sender, EventArgs e)
        {
            dzialanie = dzialanie + button25.Text;
            dzialanie_tekst = dzialanie_tekst + button25.Text;
            
            label2.Text = dzialanie_tekst;
        }        // )
        
        private void button23_Click(object sender, EventArgs e)
        {
            label3.Text = "";
            label2.Text = "Edit equation";
            dzialanie = "";
            dzialanie_tekst = "";
        }        // C
         
        private void button29_Click(object sender, EventArgs e)
        {
            color(ref green);
        }        // green

        private void button30_Click(object sender, EventArgs e)
        {
            color(ref yellow);
        }        // yellow

        private void button31_Click(object sender, EventArgs e)
        {
            color(ref pink);
        }        // pink

        private void button32_Click(object sender, EventArgs e)
        {
            color(ref brown);
        }        // brown

        private void button22_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(dzialanie))
            {
                if (dzialanie[dzialanie.Length - 1] == 's' || dzialanie[dzialanie.Length - 1] == 'c')
                {
                    dzialanie_tekst = dzialanie_tekst.Remove(dzialanie_tekst.Length - 3);
                }
                else if (dzialanie[dzialanie.Length - 1] == 't')
                {
                    dzialanie_tekst = dzialanie_tekst.Remove(dzialanie_tekst.Length - 2);
                }
                else
                {
                    dzialanie_tekst = dzialanie_tekst.Remove(dzialanie_tekst.Length - 1);
                }
                dzialanie = dzialanie.Remove(dzialanie.Length - 1);
                if (!string.IsNullOrEmpty(dzialanie))
                    label2.Text = dzialanie_tekst;
                else label2.Text = "Edit equation";
            }
        }        // DEL

        private void button16_Click(object sender, EventArgs e)
        {
            dzialanie = dzialanie + button16.Text;
            dzialanie_tekst = dzialanie_tekst + button16.Text;
            
            label2.Text = dzialanie_tekst;
        }        // ^

        private void button17_Click(object sender, EventArgs e)
        {
            dzialanie = dzialanie + button17.Text;
            dzialanie_tekst = dzialanie_tekst + button17.Text;
            
            label2.Text = dzialanie_tekst;
        }        // 0

        private void button21_Click(object sender, EventArgs e)
        {
            dzialanie = dzialanie + button21.Text;
            dzialanie_tekst = dzialanie_tekst + button21.Text;

            label2.Text = dzialanie_tekst;
        }        // x

        private void button10_Click(object sender, EventArgs e)
        {
            SetPrecyzja();
            CreateGraph();
            label3.Text = "";
            if (active == aktywne_dzialanie.red) submit(red);
            else if (active == aktywne_dzialanie.blue) submit(blue);
            else if (active == aktywne_dzialanie.green) submit(green);
            else if (active == aktywne_dzialanie.yellow) submit(yellow);
            else if (active == aktywne_dzialanie.pink) submit(pink);
            else if (active == aktywne_dzialanie.brown) submit(brown);
            textBox1.Enabled = false;
        }        // submit
       
        private void button27_Click(object sender, EventArgs e)
        {
            color(ref red);

        }        // red

        private void button28_Click(object sender, EventArgs e)
        {
            color(ref blue);
        }        // blue

        private void button26_Click(object sender, EventArgs e)
        {
            red.Clear();
            blue.Clear();
            green.Clear();
            yellow.Clear();
            pink.Clear();
            brown.Clear();
            label6.Text = "";
            textBox1.Enabled = true;
            label3.Text = "";
            if (ScaleNumbers == true)
            {
                rysuj_siatke();
            }
            rysuj_uklad();
        }        // clear screen

        private void button34_Click(object sender, EventArgs e)
        {

        }

        private void button33_Click(object sender, EventArgs e)
        {
            if(ScaleNumbers==false)
            {
                ShowScaleNumbers();
                ScaleNumbers = true;
                button33.Text = "Hide";
                rysuj_uklad();
                rysuj_siatke();
            }
            else
            {
                HideScaleNumbers();
                ScaleNumbers = false;
                button33.Text = "Show";
                Redraw();
            }
        }        // show

        private void button15_Click(object sender, EventArgs e)
        {
            dzialanie = dzialanie + button15.Text;
            dzialanie_tekst = dzialanie_tekst + button15.Text;
            
            label2.Text = dzialanie_tekst;
        }        // .
    }
}
