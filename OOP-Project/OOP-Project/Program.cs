using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace KarakterSimulasyonu
{
    class Karakter
    {
        private int karakterKimlik;
        private string karakterHareket;
        private int karakterPara;
        private int karakterAclik;
        private int karakterSusuzluk;
        private bool karakterUyuma;
        private int karakterGucSeviyesi;
        private int karakterSaglik;
        private List<string> envanter;

        public Karakter(int kimlik, int para, int aclik, int susuzluk, int gucSeviyesi, int saglik)
        {
            karakterKimlik = kimlik;
            karakterPara = para;
            karakterAclik = aclik;
            karakterSusuzluk = susuzluk;
            karakterGucSeviyesi = gucSeviyesi;
            karakterSaglik = saglik;
            karakterUyuma = false;
            envanter = new List<string>();
        }

        public void KarakterHareketIslemleri()
        {
            Console.WriteLine("Hangi Yöne Gitmek Istiyorsunuz?");
            Console.WriteLine("w-Ileri\ns-Geri\na-Sol\nd-Sağ");
            Console.WriteLine("x tuşuna basıldığında hareketler sonlanır.");
            while (true)
            {
                char yon = Console.ReadKey().KeyChar;
                Console.WriteLine();  // Satır başı

                switch (yon)
                {
                    case 'a':
                        Console.WriteLine("Sola Dönüldü");
                        break;
                    case 'd':
                        Console.WriteLine("Sağa Dönüldü");
                        break;
                    case 's':
                        Console.WriteLine("Geri gidildi");
                        break;
                    case 'w':
                        Console.WriteLine("İleri gidildi");
                        break;
                    case 'x':
                        Console.WriteLine("Hareket sonlandırıldı");
                        return;
                    default:
                        Console.WriteLine("Geçersiz yön bilgisi girildi!");
                        break;
                }
            }
        }

        public void ParaGondermeIslemleri(Karakter aliciKarakter, int gonderilecekPara)
        {
            if (karakterPara == 0)
            {
                Console.WriteLine("Bakiye yok!");
            }
            else if (gonderilecekPara > karakterPara)
            {
                Console.WriteLine("Bakiye yetersiz!");
            }
            else
            {
                karakterPara -= gonderilecekPara;
                aliciKarakter.karakterPara += gonderilecekPara;
                Console.WriteLine($"{karakterKimlik} kimlik nolu karakterden {aliciKarakter.karakterKimlik} nolu karaktere {gonderilecekPara} para gönderildi.");
            }
        }

        public void KarakterBesle()
        {
            karakterUyuma = false;
            Console.WriteLine("Miktar giriniz: ");
            int miktar = int.Parse(Console.ReadLine());
            karakterAclik += miktar;
            Console.WriteLine($"{karakterKimlik} nolu karaktere {miktar} Besin Verildi");
            Console.WriteLine($"Yeni Açlık Değeri: {karakterAclik}");
        }

        public void KarakterSuVer()
        {
            karakterUyuma = false;
            Console.WriteLine("Miktar giriniz: ");
            int miktar = int.Parse(Console.ReadLine());
            karakterSusuzluk += miktar;
            Console.WriteLine($"{karakterKimlik} nolu karaktere {miktar} Su Verildi");
            Console.WriteLine($"Yeni Susuzluk Değeri: {karakterSusuzluk}");
        }

        public async Task KarakterUykuKontrol()
        {
            while (true)
            {
                await Task.Delay(180000); // 180 saniye (3 dakika)
                if (!karakterUyuma)
                {
                    karakterUyuma = true;
                    Console.WriteLine($"{karakterKimlik} karakteri uyuyor...");
                }
            }
        }

        public async Task KarakterAclikSusuzlukAzalt()
        {
            while (true)
            {
                await Task.Delay(60000); // 60 saniye
                if (karakterAclik > 0)
                {
                    karakterAclik--;
                }
                if (karakterSusuzluk > 0)
                {
                    karakterSusuzluk--;
                }
            }
        }

        public void Init()
        {
            Task.Run(() => KarakterAclikSusuzlukAzalt());
            Task.Run(() => KarakterUykuKontrol());
        }

        public void KarekterGucKarsilastirma(Karakter digerKarakter)
        {
            if (karakterGucSeviyesi == digerKarakter.karakterGucSeviyesi)
            {
                Console.WriteLine("Güçler eşit");
            }
            else if (karakterGucSeviyesi > digerKarakter.karakterGucSeviyesi)
            {
                Console.WriteLine($"{karakterKimlik} kimlik numaralı karakter daha güçlü");
            }
            else
            {
                Console.WriteLine($"{digerKarakter.karakterKimlik} kimlik numaralı karakter daha güçlü");
            }
        }

        public void KarakterSaglikDurumu()
        {
            Console.WriteLine($"Karakterin Sağlık Puanı: {karakterSaglik}");
        }

        public void EnvantereEkle(string item)
        {
            envanter.Add(item);
            Console.WriteLine($"{item} envantere eklendi.");
        }

        public void EnvanteriGoster()
        {
            Console.WriteLine("Envanter: " + string.Join(", ", envanter));
        }

        public void Arayuz(Karakter digerKarakter)
        {
            while (true)
            {
                Console.WriteLine("\nİşlem Seçiniz:\n");
                Console.WriteLine("1 - Hareket İşlemleri");
                Console.WriteLine("2 - Para Gönderme İşlemleri");
                Console.WriteLine("3 - Karakter Besle");
                Console.WriteLine("4 - Karakter Su Ver");
                Console.WriteLine("5 - Karakter Güç Karşılaştırma");
                Console.WriteLine("6 - Sağlık Durumunu Göster");
                Console.WriteLine("7 - Envantere Eşya Ekle");
                Console.WriteLine("8 - Envanteri Göster");
                Console.WriteLine("9 - Çıkış");

                int secim = int.Parse(Console.ReadLine());

                switch (secim)
                {
                    case 1:
                        KarakterHareketIslemleri();
                        break;
                    case 2:
                        Console.WriteLine("Gönderilecek para miktarını giriniz: ");
                        int miktar = int.Parse(Console.ReadLine());
                        ParaGondermeIslemleri(digerKarakter, miktar);
                        break;
                    case 3:
                        KarakterBesle();
                        break;
                    case 4:
                        KarakterSuVer();
                        break;
                    case 5:
                        KarekterGucKarsilastirma(digerKarakter);
                        break;
                    case 6:
                        KarakterSaglikDurumu();
                        break;
                    case 7:
                        Console.WriteLine("Envantere eklenecek eşya: ");
                        string item = Console.ReadLine();
                        EnvantereEkle(item);
                        break;
                    case 8:
                        EnvanteriGoster();
                        break;
                    case 9:
                        Console.WriteLine("Çıkış yapılıyor...");
                        return;
                    default:
                        Console.WriteLine("Geçersiz seçim!");
                        break;
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Karakter k1 = new Karakter(1, 20, 10, 15, 5, 100);
            Karakter k2 = new Karakter(2, 10, 15, 10, 4, 100);

            k1.Init();
            k2.Init();

            k1.Arayuz(k2);
        }
    }
}
