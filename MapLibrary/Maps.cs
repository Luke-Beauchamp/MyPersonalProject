using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapLibrary
{
    //Here will be a storage place for the maps of each floor of the dungeon, including the tutorial which will be [0]
    public class Maps
    {
        public Map[] DungeonFloors { get; set; } //An array of the maps for each floor of the dungeon.

        public Maps()
        {
            DungeonFloors = new Map[10];
            
        }
    }
}
