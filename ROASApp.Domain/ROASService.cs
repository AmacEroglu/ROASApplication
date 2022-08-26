using ROASApp.Data.IO;
using System.Text.Json;

namespace ROASApp.Domain
{
    public class ROASService
    {
        private static List<ROAS> liste = new List<ROAS>();
        public static ROAS SaveROAS(string reklamKanali, double reklamMaliyeti, double birimFiyat, int satisAdedi)
        {
            ROAS roas = new ROAS();
            roas.reklamKanali = reklamKanali;
            roas.reklamMaliyeti = reklamMaliyeti;
            roas.birimFiyat = birimFiyat;
            roas.satisAdedi = satisAdedi;
           

            liste.Add(roas);


            string json = JsonSerializer.Serialize(liste,
                new JsonSerializerOptions { IncludeFields = true });

            FileOperation.Write(json);

            return roas;
        }

        public static List<ROAS> RemoveROAS(string reklamKanali)

        {
            LoadListFromFile();

            foreach (ROAS x in liste)
            {
                if (x.reklamKanali.ToLower() == reklamKanali.ToLower())
                {
                    liste.Remove(x);
                    Console.WriteLine("Liste silindi. Başka bir işlem yapmak ister misiniz? (y/n)");

                    break;

                }
                else
                {
                   

                }

            }

            string json = JsonSerializer.Serialize(liste,
            new JsonSerializerOptions { IncludeFields = true });

            FileOperation.Write(json);
            return liste;
        }

        public static List<ROAS> UpdateROAS(int indexNo)

        {
            LoadListFromFile();
            
                Console.WriteLine("Lütfen yeni reklam kanalını girin: ");
            liste[indexNo].reklamKanali = Console.ReadLine();
                Console.WriteLine("Lütfen yeni reklam maliyeti girin: ");
            liste[indexNo].reklamMaliyeti = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Lütfen yeni birim fiyat girin: ");
            liste[indexNo].birimFiyat = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Lütfen yeni reklam kanalını girin: ");
            liste[indexNo].satisAdedi = Convert.ToInt32(Console.ReadLine());

               
            

            string json = JsonSerializer.Serialize(liste,
            new JsonSerializerOptions { IncludeFields = true });

            FileOperation.Write(json);
            return liste;
        }

        public static bool CheckROAS(string kanalAdi)
        {
            foreach (var j in liste)
            {
                if (j.reklamKanali==kanalAdi)
                {
                    return true;                    
                }
                
            }
            return false;
        }

        public static IReadOnlyCollection<ROAS> GetAllROAS()
        {
            LoadListFromFile();
            return liste.AsReadOnly();
        }

        public static IReadOnlyCollection<ROAS> FilterByChannelName(string channelName)
        {
            LoadListFromFile();
            List<ROAS> filteredROAS = new List<ROAS>();
            foreach (ROAS r in liste)
            {
                if (r.reklamKanali.ToLower().Contains(channelName.ToLower()))
                    filteredROAS.Add(r);
            }
            return filteredROAS.AsReadOnly();
        }
        public static void LoadListFromFile()
        {
            string json = FileOperation.Read();
            liste = JsonSerializer.Deserialize<List<ROAS>>(json,
                new JsonSerializerOptions { IncludeFields = true });
        }

    }
}
