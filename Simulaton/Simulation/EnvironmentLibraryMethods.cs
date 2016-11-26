using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulaton.Simulation
{

    public partial class Property
    {
        public static Dictionary<int, string> Name = new Dictionary<int, string>();
        public static Dictionary<string, int> Id = new Dictionary<string, int>();

        public static void AddToEnvironment(int id, string name)
        {
            Name.Add(id, name);
            Id.Add(name, id);
        }
    }

    public partial class Ability
    {
        public static Dictionary<int, string> Name = new Dictionary<int, string>();
        public static Dictionary<string, int> Id = new Dictionary<string, int>();

        public static void AddToEnvironment(int id, string name)
        {
            Name.Add(id, name);
            Id.Add(name, id);
        }
    }

    public partial class Item
    {
        public static Dictionary<int, string> Name = new Dictionary<int, string>();
        public static Dictionary<string, int> Id = new Dictionary<string, int>();

        public static void AddToEnvironment(int id, string name)
        {
            Name.Add(id, name);
            Id.Add(name, id);
        }
    }
}
