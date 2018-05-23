using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _40217045_CW1
{
    class Incidents
    {
        private string incident;
        private int occurances;

        public string Incident
        {
            get { return incident; }
            set { incident = value; }
        }

        public int Occurances
        {
            get { return occurances; }
            set { occurances = value; }
        }

        
        public Incidents(string i, int occ)
        {
            Incident = i;
            Occurances = occ;
        }
    }
}
