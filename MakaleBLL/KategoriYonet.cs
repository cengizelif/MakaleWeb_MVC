using MakaleDAL;
using MakaleEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakaleBLL
{
    public class KategoriYonet
    {
        Repository<Kategori> rep_kat = new Repository<Kategori>();
        public List<Kategori> Listele()
        {
            return rep_kat.Liste();
        }
    }
}
