namespace TkanicaWebApp.Classes
{
    public class Constants
    {
        public static Dictionary<int, string> Gender = new Dictionary<int, string>
        {
            {1, "muški" },
            {2, "ženski" }
        };

        public static Dictionary<int, string> Age = new Dictionary<int, string>
        {
            {1, "odrasli" },
            {2, "deca" }
        };

        public static Dictionary<int, string> Areas = new Dictionary<int, string>
        {
            {1, "Vojvodina" },
            {2, "Centralna Srbija" },
            {3, "Zapadna Srbija" },
            {4, "Severoistočna Srbija" },
            {5, "Jugoistočna Srbija" }
        };
    }
}
