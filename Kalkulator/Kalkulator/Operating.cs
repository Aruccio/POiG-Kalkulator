﻿using System;
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
            // dla 13+5.6 da {"13","+", "5.6"}
            if(tekst.Contains("."))
                tekst = tekst.Replace('.', ',');
            byte[] ascii = Encoding.ASCII.GetBytes(tekst);
            int n = ascii.Length;
            List<string> divided = new List<string>();
            string x = "";
            int i = 0;

            while (i < n)
            {
                x = "";

                while ((ascii[i] < 58 && ascii[i] > 47) || ascii[i] == 46 || ascii[i] == 44)
                //liczba                                    przecinek      lub kropka, bywa i tak
                {
                    //wszyscy lubimy pisać .5 zamiast 0.5
                    if ( ascii[i]==44 && i>0 && (ascii[i-1] >= 58 || ascii[i-1] <= 47)) x += "0"; 
                    if (ascii[i] == 46 && i > 0 && (ascii[i - 1] >= 58|| ascii[i - 1] <= 47)) x += "0";
                    if(i==0 && (ascii[i] == 44 || ascii[i] == 46)) x += "0";

                    x += Convert.ToString((char)ascii[i]);
                    i++;
                    if (i >= n) break;
                }
                if (x != "") divided.Add(x);

                if (i >= n) break;

                if (ascii[i] >= 58 || ascii[i] <= 47)
                {
                    //każdy znak do osobnego stringa
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
            if (str == "") return ""; //nigdzie nie ma nic
            else
            {

                byte[] ascii = Encoding.ASCII.GetBytes(ls[n]);
                //żeby pierwszy element nie był przypadkowo jakimś znakiem
                if (ascii[0] >= 58 || ascii[0] <= 47)
                    ls.RemoveAt(n);


                ascii = Encoding.ASCII.GetBytes(ls[0]);
                // żeby pierwszy element nie był przypadkowo jakimś znakiem(poza -albo.)
                if ((ascii[0] >= 58 || ascii[0] <= 47) && ascii[0] != 46 && ascii[0] != 45)
                    ls.RemoveAt(0);
                n = ls.Count - 1;
                int i = 1;

                if (ls[0] == "-") temp = -Sd(ls[1]);

                n = ls.Count - 1;

                //jeśli pierwsza liczba jest z minusem
                if (ls[0] == "-")
                {
                    temp = -Sd(ls[1]);
                    i += 1;
                }
                else temp = Sd(ls[0]);



                bool znak = false;
                while (i < n)
                {
                    ascii = Encoding.ASCII.GetBytes(ls[i]);

                    if (ascii[0] >= 58 || ascii[0] <= 47)
                    {
                        //jeśli niepierwsza liczba jest z minusem
                        if (ls[i + 1] == "-")
                        {
                            sec = -Sd(ls[i + 2]);
                            znak = true;
                        }
                        else sec = Sd(ls[i + 1]);




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
                        }
                        i += 2;
                        if (znak) i++;
                        znak = false;
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

                }


                int len = 42;
                string st = "";
                string tempS = Convert.ToString(temp);
                if (tempS.Length >= len)
                    st = tempS.Substring(0, len);
                else st = tempS;

                temp = Convert.ToDouble(st);


                return Convert.ToString(temp);
            }
        }


        double Sd(string s) //string->double
            {
                
                s = s.Replace('.', ',');
                double d = Convert.ToDouble(s);
            //    Console.WriteLine(s + " = " + d);
                return d;
            }
    }
}
