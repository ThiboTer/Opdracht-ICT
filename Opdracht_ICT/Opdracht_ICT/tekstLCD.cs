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

        private DateTime uur;
        public string Uur
        {
            get { return uur.ToString("HH:mm"); }
        }

        private DateTime datum;

        public string Datum 
        {
            
            get { return datum.ToString("dd-MM-yyyy"); }
        }


        public tekstLCD()
        {
            tekst = "voer tekst in.";
        }

        public void UpdateDateTime()
        {
            uur = DateTime.Now;
            datum = DateTime.Now;   
        } 

        public void VerwijderTekst()
        {
            tekst = string.Empty;
        }

    }
}
