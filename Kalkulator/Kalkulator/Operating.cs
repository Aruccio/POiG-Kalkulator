using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalkulator
{
    /// <summary>
    /// Liczenie na bazie przyjętego od between stringa
    /// </summary>
    class Operating
    {
        string tekst;
        string ot;
        public Operating(string tekst)
        {
            this.tekst = tekst;
        }

        public string Output
        {
            get { return ot; }
            set { ot = value; }
        }


        void Calculating()
        {

            byte[] ascii = Encoding.ASCII.GetBytes(tekst);
            int n = ascii.Length;
            for(int i=0; i<n; i++)
            {
                //if(ascii[i]<)
            }
        }
    }
}
