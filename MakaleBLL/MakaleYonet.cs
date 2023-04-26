using MakaleDAL;
using MakaleEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakaleBLL
{
    public class MakaleYonet
    {
        Repository<Makale> rep_makale=new Repository<Makale>(); 
        public List<Makale> Listele()
        {
          return rep_makale.Liste();
        }
    }
}
