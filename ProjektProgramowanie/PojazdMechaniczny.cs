using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektProgramowanie
{
    public abstract class PojazdMechaniczny : Pojazd
    {
        public int RokProdukcji { get; set; }
        public int PojemnoscSilnika { get; set; }
        
    }
}
