using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using StreetLibrary;

namespace Server
{
    internal class DB_StreetsCollection
    {
        public static List<Street> LoadStreetsCollection()
        {
            List<Street> streetList = new List<Street>();
            streetList.Add(new Street(10009, "Ukraine", "Zhytomyr region", "Zhytomyr", "Korolev district", "Kibalchicha street", "street desc"));
            streetList.Add(new Street(10009, "Ukraine", "Zhytomyr region", "Zhytomyr", "Korolev district", "Kosmonoth street", "street desc"));
            streetList.Add(new Street(10009, "Ukraine", "Zhytomyr region", "Zhytomyr", "Korolev district", "Ogienka Ivana street", "street desc"));
            streetList.Add(new Street(10009, "France", "Provence region", "Marselha", "1st arrondissement of Marseille", "Independence Street", "street desc"));
            streetList.Add(new Street(10001, "Ukraine", "Zhytomyr region", "Zhytomyr", "Bogun district", "Pryvozalna street", "street desc"));
            streetList.Add(new Street(10001, "Ukraine", "Zhytomyr region", "Zhytomyr", "Bogun district", "Orlika Pilipa street", "street desc"));
            streetList.Add(new Street(10001, "Ukraine", "Zhytomyr region", "Zhytomyr", "Korolev district", "Grushevskogo Mihaila street", "street desc"));
            return streetList;
        }

        public static void SaveDbToJsonFile()
        {
            // Блок, що заміняє try - finally
            using (StreamWriter writer = new StreamWriter("streets.json", false, Encoding.Default))
            {
                // Серіалізація колекції - отримання рядка (бітової послідовності)
                string data = JsonSerializer.Serialize<List<Street>>(LoadStreetsCollection());
                // Запис рядка у файл
                writer.WriteLine(data);
            }
        }

        public static List<Street> LoadDbFromFile()
        {
            List<Street> streetList = null;
            using (StreamReader reader = new StreamReader("streets.json", Encoding.Default))
            {
                // Зчитування даних з файлу
                string data = reader.ReadToEnd();
                // Десеріалізація - створення колекції об'єктів з байтової послідовності
                streetList = JsonSerializer.Deserialize<List<Street>>(data);
            }
            return streetList == null ? new List<Street>() : streetList;
        }

    }
}
