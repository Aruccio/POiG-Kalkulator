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
        public Between()
        {
            mw = new MainWindow();
        }

        public void Calc()
        {
            if (mw.End)
            {
                Operating opera = new Operating(mw.WholeText);
                Output = opera.Output;
                mw.End = false;
                mw.Output = Output;
            }
        }

        public string Output
        {
            get { return ot; }
            set { ot = value; }
        }








    }
}


