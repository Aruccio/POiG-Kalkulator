using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalkulator
{
    class Between
    {
        /// <summary>
        /// Klasa, która mam nadzieję służy za tą klase pomiędzy kodem i grafiką. Mniej lub bardziej...
        /// </summary>
        MainWindow mw;
        string ot;
        string inp;
        public Between()
        {
            mw = new MainWindow();
        }

        public void Calc()
        {
            if (mw.End)
            {
                Input = mw.WholeText;
                Operating opera = new Operating(Input);
                Output = opera.Output;
                mw.End = false;
                mw.Output = Output;
            }
        }

        public string Output
        {
            set { ot = value; }
            get { return ot; }
        }

        public string Input
        {
            set { inp = value; }
            get { return inp; }
        }






    }
}


