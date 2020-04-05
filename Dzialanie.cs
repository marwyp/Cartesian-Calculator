using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Globalization;

namespace Funkcje
{
	
	class Dzialanie
    {
        //zmienne i obikety
        private string ludzkie_dzialanie;
        private string komputerowe_dzialanie;
        private Pen pen;
		private List<string> ONP;
		private PointF[] points;
		private double precyzja;
		public Graphics graphics;
		private bool narysowane;
		private aktywne_dzialanie color;
		private bool dziedzina;


		//konstruktor
		public Dzialanie(Color kolor, int grubosc, aktywne_dzialanie k)
        {
            ludzkie_dzialanie = "";
            komputerowe_dzialanie = "";
			ONP = new List<string>();
            pen = new Pen(kolor, grubosc);
			points = new PointF[900];
			PointF p = new PointF();
			for(int i=-400; i<500; i++)
			{
				p.X = i;
				p.Y = 1;
				points[i+400] = p;
			}
			precyzja = 1;
			narysowane = false;
			color = k;
			//dziedzina = true;
        }

        //funkcje prywatne

        string czysc_spacje(string wyrazenie)   // czysci spacje stringa    
        {
            int licznik = 0;
            do
            {
                if (wyrazenie[licznik] == ' ')
                {
                    wyrazenie= wyrazenie.Remove(licznik,1);
                    licznik--;
                }
                licznik++;
            } while (licznik != wyrazenie.Length);
            return wyrazenie;
        }

		bool litera(char lit)
		{
			bool flaga;
			if (lit > 64 && lit < 91) flaga = true;
			else if (lit > 96 && lit < 123) flaga = true;
			else flaga = false;
			if (lit == 's' || lit == 'c' || lit == 't') flaga = false;

			return flaga;
		}

		int waga(string znak)
		{
			if (znak == "+" || znak == "-") return 1;
			else if (znak == "*" || znak == "/") return 2;
			else if (znak == "^") return 3;
			else if (znak == "s" || znak == "c" || znak == "t") return 4;
			else if (znak == "(") return 0;
			else return 0;
		}

		List<string> alg_expr(string dzialanie)
		{
			//zmienne i obiekty
			List<string> vec = new List<string>();
			string obecny, Poprzedni;
			Stack<string> stos_glowny = new Stack<string>();
			Stack<string> stos_pomocniczy = new Stack<string>();

			double pomoc;

			dzialanie = czysc_spacje(dzialanie);
			int size = dzialanie.Length;
			for (int i = 0; i < size; i++)
			{
				if (double.TryParse(dzialanie[i].ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out pomoc))
				{
					if (stos_glowny.Count == 0)
					{
						stos_glowny.Push(dzialanie[i].ToString());
					}
					else if (double.TryParse(obecny = stos_glowny.Peek(), NumberStyles.Any, CultureInfo.InvariantCulture, out pomoc))
					{
						stos_glowny.Pop();
						obecny += dzialanie[i].ToString();
						stos_glowny.Push(obecny);
					}
					else
					{
						if (stos_glowny.Count > 1)
						{
							obecny = stos_glowny.Peek();
							stos_glowny.Pop();
							Poprzedni = stos_glowny.Peek();
							stos_glowny.Push(obecny);
							if ((obecny == "+" || obecny == "-") && (double.TryParse(Poprzedni, NumberStyles.Any, CultureInfo.InvariantCulture, out pomoc) == false && Poprzedni != ")" && !litera(Poprzedni[0])))
							{
								stos_glowny.Pop();
								obecny += dzialanie[i].ToString();
								stos_glowny.Push(obecny);
							}
							else
							{
								stos_glowny.Push(dzialanie[i].ToString());
							}
						}
						else
						{
							obecny = stos_glowny.Peek();
							if (obecny == "+" || obecny == "-")
							{
								stos_glowny.Pop();
								obecny += dzialanie[i].ToString();
								stos_glowny.Push(obecny);
							}
							else
							{
								stos_glowny.Push(dzialanie[i].ToString());
							}
						}
					}
				}
				else if (dzialanie[i] == '.')
				{
					if (stos_glowny.Count==0)
					{
						stos_glowny.Push(dzialanie[i].ToString());
					}
					else
					{
						obecny = stos_glowny.Peek();
						stos_glowny.Pop();
						obecny += ".";
						stos_glowny.Push(obecny);
					}
				}
				else if (litera(dzialanie[i]))
				{
					stos_glowny.Push(dzialanie[i].ToString());
				}
				else
				{
					if (stos_glowny.Count == 0)
					{
						stos_glowny.Push(dzialanie[i].ToString());
					}
					else
					{
						obecny = dzialanie[i].ToString();
						stos_glowny.Push(obecny);
					}
				}
			}
			while (stos_glowny.Count != 0)
			{
				stos_pomocniczy.Push(stos_glowny.Peek());
				stos_glowny.Pop();
			}
			while (stos_pomocniczy.Count != 0)
			{
				vec.Add(stos_pomocniczy.Peek());
				stos_pomocniczy.Pop();
			}
			return vec;
		}

		List<string> ONP_expr(List<string> dzialanie)
		{
			Stack<string> stos_onp = new Stack<string>();
			Stack<string> stos_znakow = new Stack<string>();
			List<string> vec = new List<string>();
			string obecny;
			double pomoc;
			///////////////////////////////////
			for (int i = 0; i < dzialanie.Count; i++)

			{
				if (double.TryParse(dzialanie[i], NumberStyles.Any, CultureInfo.InvariantCulture, out pomoc))
				{
					stos_onp.Push(dzialanie[i]);
				}
				else if (litera(dzialanie[i][0]))
				{
					stos_onp.Push(dzialanie[i]);
				}
				else
				{
					if (stos_znakow.Count==0)
					{
						stos_znakow.Push(dzialanie[i]);
					}
					else
					{
						if (dzialanie[i] == "(")
						{
							stos_znakow.Push(dzialanie[i]);
						}
						else if (dzialanie[i] == ")")
						{
							while (stos_znakow.Peek() != "(")
							{
								stos_onp.Push(stos_znakow.Peek());
								stos_znakow.Pop();
							}
							stos_znakow.Pop();
						}
						else
						{
							obecny = stos_znakow.Peek();
							if (waga(obecny) >= waga(dzialanie[i]))
							{
								while (stos_znakow.Count!=0 && stos_znakow.Peek() != "(")
								{
									stos_onp.Push(stos_znakow.Peek());
									stos_znakow.Pop();
								}
								stos_znakow.Push(dzialanie[i]);
							}
							else
							{
								stos_znakow.Push(dzialanie[i]);
							}
						}
					}
				}
			}
			while (stos_onp.Count!=0)
			{
				stos_znakow.Push(stos_onp.Peek());
				stos_onp.Pop();
			}
			while (stos_znakow.Count != 0)
			{
				vec.Add(stos_znakow.Peek());
				stos_znakow.Pop();
			}
			return vec;
		}

		// funkcje do kalkulatora ONP
		double dodawanie(double x, double y)
		{
			return x + y;
		}
		double odejmowanie(double x, double y)
		{
			return y - x;
		}
		double mnozenie(double x, double y)
		{
			return x * y;
		}
		double dzielenie(double x, double y)
		{
			if (x > -0.1 && x < 0.1)
			{
				dziedzina = false;
			} 
			return y / x;
		}
		double potegowanie(double x, double y)
		{
			return Math.Pow(y,x);
		}
		double Sinus(double x)
		{
			return Math.Sin(x);
		}
		double Cosinus(double x)
		{
			return Math.Cos(x);
		}
		double Tangens(double x)
		{
			if (Math.Cos(x) > -0.1 && Math.Cos(x) < 0.1) dziedzina=false;
			return Math.Tan(x);
		}
		//

		double kalk_ONP(List<string> dzialanie)
		{
			Stack<double> stos = new Stack<double>();
			double liczba;
			string pomoc;
			//MessageBox.Show("stos dzialanie: " + dzialanie[0]);

			//////
			for (int i = 0; i < dzialanie.Count; i++)
			{
				if(dziedzina)
				{
					pomoc = dzialanie[i];
					pomoc = pomoc.Replace(",", ".");
					if (double.TryParse(pomoc, NumberStyles.Any, CultureInfo.InvariantCulture, out liczba))
					{
						//MessageBox.Show("jest");
						stos.Push(liczba);
					}
					else
					{
						switch (dzialanie[i][0])
						{
							case '+':
								stos.Push(dodawanie(stos.Pop(), stos.Pop()));
								break;
							case '-':
								stos.Push(odejmowanie(stos.Pop(), stos.Pop()));
								break;
							case '*':
								stos.Push(mnozenie(stos.Pop(), stos.Pop()));
								break;
							case '/':
								stos.Push(dzielenie(stos.Pop(), stos.Pop()));
								break;
							case '^':
								stos.Push(potegowanie(stos.Pop(), stos.Pop()));
								break;
							case 's':
								stos.Push(Sinus(stos.Pop()));
								break;
							case 'c':
								stos.Push(Cosinus(stos.Pop()));
								break;
							case 't':
								stos.Push(Tangens(stos.Pop()));
								break;
						}
					}
				}
				else
				{
					//MessageBox.Show("nie");
				}
			}
			if (!dziedzina) stos.Push(50000);
			return stos.Peek();
		}

		List<string> zastap_zmienna(char z, string liczba)
		{
			List<string> lista = new List<string>();
			int licznik = 0;
			do
			{
				lista.Add(ONP[licznik]);
				licznik++;
			} while (ONP.Count != licznik);
			for (int j = 0; j < ONP.Count; j++)
			{
				if (z == ONP[j][0]) lista[j] = liczba;
			}
			return lista;
		}

		//funkcje publiczne

		public bool valid(ref string dzialanie)
		{
			bool flaga = true;
			if (dzialanie.Length > 1)
			{
				int licznik = 1, liczba_nawiasow_otwartych = 0, liczba_nawiasow_zamknietych = 0;
				double pomoc;
				do
				{
					if (litera(dzialanie[licznik]) || dzialanie[licznik] == 's' || dzialanie[licznik] == 'c' || dzialanie[licznik] == 't' || dzialanie[licznik] == '(')
					{
						if (double.TryParse(dzialanie[licznik - 1].ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out pomoc))
						{
							dzialanie = dzialanie.Insert(licznik, "*");
						}
					}
					licznik++;
				} while (licznik != dzialanie.Length);
				licznik = 1;
				do
				{
					if (dzialanie[licznik] == 's' || dzialanie[licznik] == 'c' || dzialanie[licznik] == 't')
					{
						if (dzialanie[licznik - 1] == 's' || dzialanie[licznik - 1] == 'c' || dzialanie[licznik - 1] == 't')
						{
							flaga = false;
						}
					}
					if (dzialanie[licznik] == '-' || dzialanie[licznik] == '+' || dzialanie[licznik] == '*' || dzialanie[licznik] == '/')
					{
						if (dzialanie[licznik - 1] == '-' || dzialanie[licznik - 1] == '+' || dzialanie[licznik - 1] == '*' || dzialanie[licznik - 1] == '/')
						{
							flaga = false;
						}
					}
					if (litera(dzialanie[licznik]))
					{
						if (dzialanie[licznik - 1] == dzialanie[licznik]) flaga = false;
					}

					licznik++;
				} while (licznik != dzialanie.Length);
				licznik = 0;
				do
				{
					if (dzialanie[licznik] == '(') liczba_nawiasow_otwartych++;
					if (dzialanie[licznik] == ')') liczba_nawiasow_zamknietych++;
					licznik++;
				} while (licznik != dzialanie.Length);
				if (liczba_nawiasow_otwartych != liczba_nawiasow_zamknietych) flaga = false;
				if (dzialanie[dzialanie.Length - 1] != ')' && !double.TryParse(dzialanie[dzialanie.Length - 1].ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out pomoc) && !litera(dzialanie[dzialanie.Length - 1])) flaga = false;

			}
			else
			{
				if (dzialanie[0] == 's') flaga = false;
				if (dzialanie[0] == 'c') flaga = false;
				if (dzialanie[0] == 't') flaga = false;
				if (dzialanie[0] == '+') flaga = false;
				if (dzialanie[0] == '-') flaga = false;
				if (dzialanie[0] == '/') flaga = false;
				if (dzialanie[0] == '*') flaga = false;
				if (dzialanie[0] == '^') flaga = false;
			}


			return flaga;
		}                       // sprawdza poprawnosc dzialania

		public void SetEquotion(string ludzkie, string komputerowe)
        {
            ludzkie_dzialanie = ludzkie;
            komputerowe_dzialanie = komputerowe;
        }   // nadaje dzialania

		public void SetONP()
		{
			string dzialanie = komputerowe_dzialanie;
			List<string> lista = new List<string>();
			lista = alg_expr(dzialanie);
			lista = ONP_expr(lista);
			ONP = lista;
		}                                          // nadaje ONP

		public Pen GetPen()
		{
			return pen;
		}                                           // zwraca pen

		public void SetCenter()
		{
			graphics.TranslateTransform(700, 350);
		}                                       // ustawia srodek na (700, 350)

		public bool EquotionValid() 
		{
			if(!string.IsNullOrEmpty(komputerowe_dzialanie))
			{
				return valid(ref komputerowe_dzialanie);
			}
			else
			{
				return false;
			}
		}                                  // sprawdza czy nadane dzialanie jest poprawne

		public void Draw2()
		{
			int y2, y1;
			string pomocniczy;
			double liczba;
			List<string> dzialanie = new List<string>();
			do
			{
				dziedzina = true;
				dzialanie = zastap_zmienna('x', (-400/(50/precyzja)).ToString());
				liczba = kalk_ONP(dzialanie);
				liczba *= (double)(50 / precyzja);
				liczba = Math.Round(liczba);
				pomocniczy = liczba.ToString();
				y2 = int.Parse(pomocniczy);
				//MessageBox.Show(dziedzina.ToString());
				//MessageBox.Show(y2.ToString());
			} while (!dziedzina);
			
			
			for (int i = -399; i < 500; i++)
			{
				dziedzina = true;
				y1 = y2;
				dzialanie = zastap_zmienna('x', ((double)i / (double)(50 / precyzja)).ToString());
				liczba = kalk_ONP(dzialanie);

				liczba *= (double)(50 / precyzja);
				liczba = Math.Round(liczba);
				pomocniczy = liczba.ToString();
				y2 = int.Parse(pomocniczy);
				try
				{
					if (dziedzina)
					{
						graphics.DrawLine(pen, (i - 1), (-1) * y1, i, (-1) * y2);
					}
					else
					{
						do
						{
							dziedzina = true;
							i++;
							dzialanie = zastap_zmienna('x', ((double)i / (double)(50 / precyzja)).ToString());
							liczba = kalk_ONP(dzialanie);
							liczba *= (double)(50 / precyzja);
							liczba = Math.Round(liczba);
							pomocniczy = liczba.ToString();
							y2 = int.Parse(pomocniczy);
							if (i == 500) break;
						} while (!dziedzina);
					}
				}
				catch(System.ArithmeticException)
				{
					break;
				}
				
			}

			narysowane = true;
		}                                           // rysuje zadana funkcje, dziedzina nie musi byc rzeczywista

		public void Draw() 
		{
			//liczba *= (double) (50 / precyzja);
			PointF p = new PointF();
			List<string> dzialanie = new List<string>();
			double wynik;
			string w;
			float y;
			for(int i=-400; i<500; i++)
			{
				dzialanie = zastap_zmienna('x',(points[i+400].X/(50.0/precyzja)).ToString(CultureInfo.InvariantCulture));
				p = points[i + 400];
				//MessageBox.Show(points[i + 400].ToString());

				wynik = kalk_ONP(dzialanie);
				//MessageBox.Show(dzialanie[0].ToString());
					w = wynik.ToString(CultureInfo.InvariantCulture);
				float.TryParse(w, NumberStyles.Any, CultureInfo.InvariantCulture, out y);
				//MessageBox.Show(w);

				y *= (float)(50 / precyzja);
				//MessageBox.Show(y.ToString());

				p.Y = -y;
				points[i + 400] = p;
				//MessageBox.Show(points[i + 400].ToString());
				/*if (i > 0 && i < 10)
				{
					//MessageBox.Show(points[i+400].ToString());
				}*/
			}
			graphics.DrawCurve(pen, points);
			narysowane = true;
		}                                           // rysuje zadana funkcje, dziedzina rzeczywista

		public void Clear()
		{
			graphics.Clear(SystemColors.Control);
			narysowane = false;
		}                                           // usuwa namalowana funkcje

		public bool GetNarysowane()
		{
			return narysowane;
		}                                   // zwraca true jesli dzialanie jest narysowane

		public aktywne_dzialanie GetColor()
		{
			return color;
		}                           // zwraca kolor dzialania

		public string GetLuzdzkie()
		{
			return ludzkie_dzialanie;
		}                                   // zwraca ludzkie dzialanie

		public void SetPrecyzja(double p)
		{
			precyzja = p;
		}                             // ustawia precyzje

	}
}
