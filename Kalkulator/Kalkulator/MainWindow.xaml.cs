using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Kalkulator
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string wholeText;
        bool end;
        string ot;
        Operating opera;
        public MainWindow()
        {
            //NOTE TO ktokolwiek będzie to czytał:
            //przycisk "kwadrat" to nie ^2, tylko ^n i dalej się wpisuje n
            //ale nie zmieniałam już wszędzie jego nazwy
            InitializeComponent();
            SpecialSymbols();
            opera = new Operating(this);

        }


        //wszystkie te unicodowe symbole typu x^n czy sqr(x)
        private void SpecialSymbols()
        {
            kwadrat.Content = "x"+ "\u207f";
            //pierwiastek.Content = "\u221a" + "x";
            back.Content = "\u232b";
            odwrotnosc.Content = "\u00b9" + "\u2215" + "\u2093"; //"1"+ "\u2215" +"x";
            dzielenie.Content = "\u00F7";
            mnozenie.Content = "\u2715";
            minus.Content = "\u002D";
            plus.Content = "\u002B";
            znak.Content = "\u00b1";
        }


        private bool IsSpecial(Button button) //bez = i wszystkich deletów
        {
            string cont = Convert.ToString(button.Content);
            switch(cont)
            {
                case "x"+"\u207f": return true; //kwadrat (potęga ^n)
                case "\u221a" + "x": return true;//pierwiastek
                case "\u00b9" + "\u2215" + "\u2093": return true;//odwrotość
                case "\u00F7": return true; //dzielenie
                case "\u2715": return true; //mnozenie
                case "\u002D": return true; //minus
                case "\u002B": return true;//plus
                case "\u00b1": return true;//zmiana znaku
                default: return false;
            }
        }

        private bool IsSpecialChar(char c)
        {
            switch (c)
            {
                case '^': return true; //kwadrat (potęga ^n)
              //  case '\u221a': return true;//pierwiastek
                case '/': return true; //dzielenie
                case '*': return true; //mnozenie
                case '-': return true; //minus
                case '+': return true;//plus
                default: return false;
            }
        }

        private bool IsSqr(char c)
        {
            if (c == '\u221a') return true;
            else return false;
        }


        private bool IsDeleting(Button button) //te trzy usuwające
        {
            string cont = Convert.ToString(button.Content);
            switch(cont)
            {
                case "\u232b": return true;
                case "C": return true;
                case "CE": return true;
                default: return false;
            }
        }
        private bool IsEnding(Button button)
        {
            //czy kliknięto =
            string cont = Convert.ToString(button.Content);
            if (cont == "=") return true;
            else return false;
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            TextBlock tb = tekst;
            TextBlock saved = savedT;


            if (IsEnding(button))
            {
                // znak =
                saved.Text += tb.Text;
                tb.Text = "";
                tb.Text = opera.Calculate(saved.Text);
                end = true;
            }
            else
            {
                if(end==true)
                {
                    tb.Text = "";
                    saved.Text = "";
                    end = false;
                }

                if (IsDeleting(button))
                {
                    //któreś usuwanie
                    switch (button.Name)
                    {
                        case "c":
                            tekst.Text = "";
                            savedT.Text = "";
                            break;
                        case "ce":
                            tekst.Text = "";
                            break;
                        case "back":
                            if (tekst.Text.Length > 0) tekst.Text = tekst.Text.Remove(tekst.Text.Length - 1);
                            break;
                    }
                }


                else if (IsSpecial(button))//&& !IsSpecialChar(saved.Text.Last()))
                {
                    bool canbe = true;
                    if (saved.Text == "")
                    {
                        if (tb.Text == "") canbe = false;
                    }
                    else
                    {
                        if (IsSpecialChar(saved.Text.Last()) && tb.Text == "") canbe = false;
                        if (IsSqr(saved.Text.Last()) && tb.Text=="") canbe = false;
                    }


                    if (canbe)
                    {
                        double x;
                        decimal d;

                        switch (button.Name)
                        {
                            //dwa instanty
                            case "znak":
                                tekst.Text = tekst.Text.Replace('.', ',');
                                x = Convert.ToDouble(tekst.Text) * (-1);
                                tekst.Text = Convert.ToString(x);
                                break;
                            case "odwrotnosc":
                                tekst.Text = tekst.Text.Replace('.', ',');
                                d = Convert.ToDecimal(tekst.Text);
                                x = 1 / Convert.ToDouble(d);
                                tekst.Text = Convert.ToString(x);
                                break;
                            //dodawane do stringa
                            case "kwadrat":
                                saved.Text += tekst.Text;
                                saved.Text += "^"; //94
                                tekst.Text = "";
                                break;
                            case "dzielenie":
                                saved.Text += tekst.Text;
                                saved.Text += "/"; //44
                                tekst.Text = "";
                                break;
                            case "mnozenie":
                                saved.Text += tekst.Text;
                                saved.Text += "*"; //42
                                tekst.Text = "";
                                break;
                            case "minus":
                                saved.Text += tekst.Text;
                                saved.Text += "-"; //45
                                tekst.Text = "";
                                break;
                            case "plus":
                                saved.Text += tekst.Text;
                                saved.Text += "+"; //43
                                tekst.Text = "";
                                break;
                        }
                    }
                }
                else if(!IsSpecial(button))
                {
                    //cyfra lub przecinek
                    if (button == przecinek)
                    { if (!tb.Text.Contains(',')) tb.Text += ","; }
                    else
                        tb.Text += Convert.ToString(button.Content);
                    // saved.Text += Convert.ToString(button.Content);
                }

            }

        }


    }
}
