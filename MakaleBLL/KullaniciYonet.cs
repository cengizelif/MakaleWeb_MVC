using MakaleCommon;
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
        public MakaleBLLSonuc<Kullanici> KullaniciKaydet(RegisterModel model)
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
            else
            {
              int islemsonuc=rep_kul.Insert(new Kullanici()
                {
                      KullaniciAdi=model.KullaniciAdi,  
                      Email=model.Email,    
                      Sifre=model.Sifre ,
                      AktifGuid=Guid.NewGuid()
                });

                if(islemsonuc>0)
                {
                    sonuc.nesne= rep_kul.Find(x => x.KullaniciAdi == model.KullaniciAdi || x.Email == model.Email);

                    string siteUrl = ConfigHelper.Get<string>("SiteRootUri");

                    string aktivateUrl = $"{siteUrl}/Home/HesapAktiflestir/{sonuc.nesne.AktifGuid}";
                    //https:/localhost:44325/Home/HesapAktiflestir/dcaad31a-a109-4dca-beba-1bb2c88793de

                    string body = $"Merhaba {sonuc.nesne.Adi} {sonuc.nesne.Soyad} <br /> Hesabınızı aktifleştirmek için <a href='{aktivateUrl}' target='_blank'>tıklayınız</a>";

                    MailHelper.SendMail(body, sonuc.nesne.Email, "Hesap Aktifleştirme");

                }
            }

            return sonuc;   
        }

        public MakaleBLLSonuc<Kullanici> LoginKontrol(LoginModel model)
        {
            MakaleBLLSonuc<Kullanici> sonuc = new MakaleBLLSonuc<Kullanici>();

           sonuc.nesne=rep_kul.Find(x=>x.KullaniciAdi==model.KullaniciAdi && x.Sifre==model.Sifre);    

            if(sonuc.nesne==null)
            {
                sonuc.hatalar.Add("Kullanıcı adı yada şifre hatalı");
            }
            else
            {
                if(!sonuc.nesne.Aktif)
                {
                    sonuc.hatalar.Add("Kullanıcı aktifleştirilmemiştir.Lütfen e-posta adresinizi kontrol ediniz.");
                }             
            }

            return sonuc;
        }



    }
}
