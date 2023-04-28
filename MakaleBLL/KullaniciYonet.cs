using MakaleDAL;
using MakaleEntities;
using MakaleEntities.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakaleBLL
{
    public class KullaniciYonet
    {
        Repository<Kullanici> rep_kul=new Repository<Kullanici>();  
        public MakaleBLLSonuc<Kullanici> KullaniciBul(RegisterModel model)
        {
            MakaleBLLSonuc<Kullanici> sonuc = new MakaleBLLSonuc<Kullanici>();

            sonuc.nesne=rep_kul.Find(x => x.KullaniciAdi == model.KullaniciAdi || x.Email == model.Email);

            if(sonuc.nesne!=null)
            {
                if(sonuc.nesne.KullaniciAdi==model.KullaniciAdi)
                {
                    sonuc.hatalar.Add("Bu kullanıcı adı sistemle kayıtlı");
                }

                if(sonuc.nesne.Email==model.Email)
                {
                    sonuc.hatalar.Add("Bu email sistemde kayıtlı");
                }
            }

            return sonuc;   
        }


    }
}
