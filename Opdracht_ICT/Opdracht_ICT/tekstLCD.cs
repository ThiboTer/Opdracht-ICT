using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCD
{
    class tekstLCD
    {
		

		private string tekst;

		public string Tekst
		{
			get { return tekst; }
			set {tekst = value;}
		}

        public tekstLCD()
        {
            tekst = "voer tekst in.";
        }

        public void VerwijderTekst()
        {
            tekst = string.Empty;
        }

    }
}
