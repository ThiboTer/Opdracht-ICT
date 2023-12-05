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
			set 
			{
				if (value.Length <= 16)
				{
					tekst= value.PadRight(16,' ');
                    Error(false);
                }
				if (value.Length > 16) 
				{
					tekst = value;
					Error(true);
				}
				else
				{ tekst = value;}
			}
		}

        public bool lengte { get; set; }

        private void Error(bool status)
		{ lengte = status; }



	}
}
