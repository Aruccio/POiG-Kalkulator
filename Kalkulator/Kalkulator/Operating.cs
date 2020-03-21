using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalkulator
{
    /// <summary>
    ///Klasa do liczenia
    /// </summary>
    class Operating
    {
        MainWindow mw;
        string ot;
        string inp;
        public Operating(MainWindow mw)
        {
            this.mw = mw;
        }

        public string Output
        {
            get { return ot; }
            set { ot = value; }
        }


        List<String> Divide(string tekst)
        {
            //podzieli string na liczby i znaki,
            //przykładowo dla 13+45/5 da {"13","+","45", "/","5"}
            byte[] ascii = Encoding.ASCII.GetBytes(tekst);
            int n = ascii.Length;
            List<string> divided = new List<string>();
            string x = "";
            int i = 0;

            while (i < n)
            {
                x = "";


                while ((ascii[i] < 58 && ascii[i] > 47) || ascii[i] == 46 || ascii[i]==251)
                {
                    //cyfra, w tym pierwiastek, np 2 pierwiastki z 3
                    x += Convert.ToString((char)ascii[i]);
                    i++;
                    if (i >= n) break;
                }
                divided.Add(x);
                if (i >= n) break;

                if (ascii[i] >= 58 || ascii[i] <= 47)
                {
                    //znak
                    x = Convert.ToString((char)ascii[i]);
                    divided.Add(x);
                    i++;
                }
            }
            return divided;
        }

        public string Calculate(string str)
        {
            List<String> ls = Divide(str);
            double x = 0;
            double temp = 0;
            double sec;
            int n = ls.Count - 1;
            if (str == "") return "";
            else
            {

            byte[] ascii = Encoding.ASCII.GetBytes(ls[0]);

            //żeby pierwszy i ostatni element nie był przypadkowo jakimś znakiem
            if ((ascii[0] >= 58 || ascii[0] <= 47) && ascii[0] != 46)
                ls.RemoveAt(0);

            n = ls.Count - 1;
            ascii = Encoding.ASCII.GetBytes(ls[n]);
            if (ascii[0] >= 58 || ascii[0] <= 47)
                ls.RemoveAt(n);

            n = ls.Count - 1;

            temp = Sd(ls[0]);


            int i = 1;
            while (i < n)
            {
                ascii = Encoding.ASCII.GetBytes(ls[i]);
                //      Console.WriteLine(ascii[0] + "||" + ls[i]);
                if (ascii[0] >= 58 || ascii[0] <= 47)
                {
                    sec = Sd(ls[i + 1]);
                    switch (ls[i])
                    {
                        case "+":
                            temp += sec;
                            break;
                        case "-":
                            temp -= sec;
                            break;
                        case "*":
                            temp *= sec;
                            break;
                        case "/":
                            if (sec == 0) return "ERROR: DZIELENIE PRZEZ 0";
                            else temp /= sec;
                            break;
                        case "^":
                            temp = Math.Pow(temp, sec);
                            break;
                        case "\u221a": //pierwiastek
                          //  temp = Math.Sqrt(temp);
                            break;
                    }
                    i += 2;
                    if (i >= n) break;
                }
                else
                {
                    if (ascii[0] == 46)
                        temp = Sd("0" + ls[i]);
                    else temp = Sd(ls[i]);
                    i++;
                    if (i >= n) break;
                }

                Console.WriteLine(temp);
            }

            return Convert.ToString(temp);

            }
        }

        double Sd(string s) //string->double
        {
            s = s.Replace('.', ',');
            double d = Convert.ToDouble(s);
            Console.WriteLine(s + " = " + d);
            return d;
        }
    }
}
