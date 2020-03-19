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
        public MainWindow()
        {
            InitializeComponent();
            SpecialSymbols();

        }

        public string WholeText
        {
            get { return wholeText; }
        }

        public bool End
        {
            get { return end; }
            set { end = value; }
        }

        public string Output
        {
            set { ot = value; }
        }

        //wszystkie te unicodowe symbole typu x^2 czy sqr(x)
        private void SpecialSymbols()
        {
            kwadrat.Content = "x" + "\u00b2";
            pierwiastek.Content = "\u221a" + "x";
            back.Content = "\u232b";
            odwrotnosc.Content = "\u00b9" + "\u2215" + "\u2093"; //"1"+ "\u2215" +"x";
            dzielenie.Content = "\u00F7";
            mnozenie.Content = "\u2715";
            minus.Content = "\u002D";
            plus.Content = "\u002B";
            znak.Content = "\u00b1";
        }


        private bool IsSpecial(Button button) //bez = i backa
        {
            string cont = Convert.ToString(button.Content);
            switch(cont)
            {
                case "x" + "\u00b2": return true; //kwadrat
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
            string cont = Convert.ToString(button.Content);
            if (cont == "=") return true;
            else return false;
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            TextBlock tb = tekst;


            if (IsEnding(button)) 
            {

                wholeText = tb.Text;
                //TODO 
                //TUTAJ ma posłać do Inputa w between lub Input between ma sam sobie pobrać wholeText.
            }
            else
            {
                //nie =
            }




            tb.Text+=Convert.ToString(button.Content);


        }


    }
}
