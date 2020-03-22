using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testy
{
    class Program
    {
        static void Main(string[] args)
        {
           Console.WriteLine(Calculate("-2*3"));

            Console.ReadKey();
        }


        static List<String> Divide(string tekst)
        {
            //podzieli string na liczby i znaki,
            //przykładowo dla 13+45/5 da {"13","+","45", "/","5"}
            // dla 13+5.6 da {"13","+", "5.6"}
            byte[] ascii = Encoding.ASCII.GetBytes(tekst);
            int n = ascii.Length;
            List<string> divided = new List<string>();
            string x = "";
            int i = 0;

            while (i < n)
            {
                x = "";

                while ((ascii[i] < 58 && ascii[i] > 47) || ascii[i] == 46 || ascii[i]==44)
                //liczba                                    przecinek      lub kropka, bywa i tak
                {
                    if (ascii[i] == 46 || ascii[i]==44) x += "0"; //jeśli .5 zrób 0.5

                    x += Convert.ToString((char)ascii[i]);
                    i++;
                    if (i >= n) break;
                }
                if(x!="") divided.Add(x);

                if (i >= n) break;

                if (ascii[i] >= 58 || ascii[i] <= 47)
                {
                    //każdy znak do osobnego stringa
                    x = Convert.ToString((char)ascii[i]);
                    divided.Add(x);
                    i++;
                }
            }
            for (int j = 0; j < divided.Count; j++)
            {
                    Console.WriteLine($"Znak:{j} " + divided[j]);
            }
            return divided;
        }

        static string Calculate(string str)
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

                //pierwsze temp;


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

                        //jeśli pierwsza liczba jest z minusem
                        if(i==2)
                        {
                            if (ls[0] == "-") temp = -Sd(ls[1]);
                            else temp = Sd(ls[1]);
                        }
                        else temp = Sd(ls[i - 1]);

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

              //      Console.WriteLine(temp);
                }

                return Convert.ToString(temp);
            }
        }


        static double Sd(string s) //string->double
        {

            s = s.Replace('.', ',');
            double d = Convert.ToDouble(s);
    //        Console.WriteLine(s + " = " + d);
            return d;
        }

    }
}

