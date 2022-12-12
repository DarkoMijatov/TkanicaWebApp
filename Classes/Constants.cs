using System.Collections.ObjectModel;

namespace TkanicaWebApp.Classes
{
    public class Constants
    {
        public static Dictionary<int, string> Gender = new()
        {
            {1, "muški" },
            {2, "ženski" }
        };

        public static Dictionary<int, string> Age = new()
        {
            {1, "odrasli" },
            {2, "deca" }
        };

        public static Dictionary<int, string> Areas = new()
        {
            {1, "Vojvodina" },
            {2, "Centralna Srbija" },
            {3, "Zapadna Srbija" },
            {4, "Severoistočna Srbija" },
            {5, "Jugoistočna Srbija" }
        };

        public static Dictionary<int, string> UserTypes = new()
        {
            {1, "administrator" },
            {2, "upravnik" },
            {3, "član" },
            {4, "zaposleni" }
        };
    }

    public class DictionaryValues
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static List<DictionaryValues> GetDictionaryValues(Dictionary<int, string> dictionary)
        {
            var list = new List<DictionaryValues>();
            foreach(var keyValue in dictionary)
            {
                list.Add(new DictionaryValues { Id = keyValue.Key, Name = keyValue.Value });
            }
            return list;
        }
    }
}
