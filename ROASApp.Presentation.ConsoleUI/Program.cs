using ROASApp.Domain;

namespace ROASApp.Presentaion.ConsoleUI
{
    public class Program
    {
        public static void Main()
        {
            Menu();
        }

        private static void Menu()
        {
            Console.Clear();
            Console.WriteLine("1. Yeni ROAS Kaydı\n2. Roas Listesi\n3. ROAS Filtrele\n4. ROAS Güncelle\n5. ROAS'ı Silme\n6. Çıkış");
            MenuSelection();
        } 
        private static void MenuSelection()
        {
            Console.Write("Seçiminiz : ");
            string choose = Console.ReadLine();
            switch (choose)
            {
                case "1":
                    NewROAS();
                    break;
                case "2":
                    ListOfROAS();
                    break;
                    case "3":
                    FilterROAS();
                    break;
                case "4":
                    UpdateROAS();
                    break;
                case "5":
                    RemoveROAS();
                    break;
                case "6":
                    Environment.Exit(0);
                    break;
                default:
                    MenuSelection();
                    break;
            }
            Again();
        }
        static void Again()
        {
            Console.WriteLine("Menüye dönmek için Enter");
            Console.ReadLine();
            Menu();
        } 
        private static void FilterROAS()
        {
            Console.WriteLine("Kanal adı içinde geçen kelimeyi yazın : ");
            string filterKeyword = Console.ReadLine();
            var data=ROASService
                .FilterByChannelName(filterKeyword);
            PrintList(data);
        } 
        private static void ListOfROAS()
        {
            var list = ROASService.GetAllROAS();
            PrintList(list);
        }


        private static void RemoveROAS()
        {
            ListOfROAS();

            Console.WriteLine("Silmek istediğiniz reklam kanalını seçin: ");           
            string kanalAdi = Console.ReadLine();
            bool varmi = ROASService.CheckROAS(kanalAdi);
            if (varmi)
            {
                var data = ROASService.RemoveROAS(kanalAdi);

            }
            else
            {
                Console.Clear();
                Console.WriteLine("Yanlış bir değer girdiniz");
                RemoveROAS();
            }

        }

        private static void UpdateROAS()
        {
            ListOfROAS();

            Console.WriteLine("Lütfen güncellemek istediğiniz index no'yu girin: ");
            int indexNo = Convert.ToInt32(Console.ReadLine());
            Console.Clear();
            var data = ROASService.UpdateROAS(indexNo-1);
            Console.WriteLine("Reklam kanalı güncellendi. Listeye gitmek ister misiniz? (y/n)");
            string answer = Console.ReadLine().ToLower();
            if (answer == "y")
                Console.Clear();
            ListOfROAS();
            if (answer == "n")
                Environment.Exit(0);



        }


        static void PrintList(IReadOnlyCollection<ROAS> list)
        {
             Console.WriteLine("----------- Liste Başlangıcı ----------"); 
            foreach (ROAS r in list)
            {
                Console.WriteLine(r.ROASInfo());
                Console.WriteLine("--------------------------------------");
            }
            Console.WriteLine("----------- Liste Sonu ----------");
            
        }
        private static void NewROAS()
        {
            Console.WriteLine("Reklam Kanalı Adı : ");
            string kanalAdi = Console.ReadLine();
            Console.WriteLine("Reklam Maliyeti : ");
            double maliyet =Convert.ToDouble( Console.ReadLine());
            Console.WriteLine("Satılan Ürünlerin Birim Fiyatı : ");
            double birimFiyat = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Satılan Ürün Adedi : ");
            int adet = Convert.ToInt32(Console.ReadLine());


           var data= ROASService.SaveROAS(kanalAdi, maliyet, birimFiyat, adet);

            Console.WriteLine($"Hesaplanan ROAS Değeri : %{data.ROASGetirisi()}");
            
        }
    }
}