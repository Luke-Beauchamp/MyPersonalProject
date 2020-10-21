using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ItemLibrary;
using CharacterLibrary;

namespace EquipmentLibrary
{
    public class Amulet : Equipment
    {
        public int ResourceMod { get; set; } //Will affect resource attribute as a % (which will in effect affect max resource

        public Amulet(int resourceMod, Slot slot, int[] attributes, int cost, string name, string description, ItemType type) :
            base(slot, attributes, cost, name, description, type)
        {
            ResourceMod = resourceMod;
        }

        public Amulet(string name, int rapport, int floor) : base(name)
        {
            Random rand = new Random();
            Slot = Slot.Amulet;
            Name = "Amulet " + name;
            Attributes = new int[7] { 0, 0, 0, 0, 0, 0, 0 };
            //New Improved Rapport Indicator Formatting for randomized Amulet
            if (rapport < -14)//Terrible
            {

            }
            else if (rapport < -8)//Bad
            {

            }
            else if (rapport < -2)//Poor
            {

            }
            else if (rapport < 3)//Moderate
            {

            }
            else if (rapport < 8)//Fair
            {

            }
            else if (rapport < 15)//Good
            {

            }
            else//Terrific
            {

            }
            //Take out old below once above is complete
            switch (rand.Next(1, 7)) // Randomize the stats.
            {
                case 1://Do Math On all of these to account for the floor of the dungeon and rapport of the character.
                    ResourceMod = (3 * floor) + rapport + rand.Next(1, 4);
                    break;
                case 2:
                    ResourceMod = (2 * floor) + rapport + rand.Next(1, 4);
                    Attributes[rand.Next(6)] = floor + rapport + rand.Next(-1, 2);
                    break;
                case 3:
                    ResourceMod = floor + rapport + rand.Next(1, 4);
                    Attributes[rand.Next(3)] = floor + rapport + rand.Next(-1, 2);
                    Attributes[rand.Next(3, 6)] = floor + rapport + rand.Next(-1, 2);
                    break;
                case 4:
                    ResourceMod = floor + rapport + rand.Next(1, 4);
                    Attributes[rand.Next(6)] = (floor*2) + rapport + rand.Next(-1, 2);
                    break;
                case 5:
                    ResourceMod = floor + rapport + rand.Next(1, 4);
                    Attributes[rand.Next(2)] = floor + rand.Next(2);
                    Attributes[rand.Next(2, 4)] = floor + rand.Next(-1, 2);
                    Attributes[rand.Next(4, 6)] = floor + rand.Next(-1, 2);
                    break;
                case 6:
                    ResourceMod = floor + rapport + rand.Next(1, 4);
                    Attributes[rand.Next(2)] = floor + rapport + rand.Next(2);
                    Attributes[rand.Next(2, 4)] = floor + rapport + rand.Next(-1, 2);
                    Attributes[rand.Next(4, 6)] = floor + rapport + rand.Next(-1, 2);
                    break;
            }
        }

        public Amulet() //Creates a "blank" Amulet Equipment
        {
            ResourceMod = 0;
            Slot = Slot.Amulet;
            Attributes = new int[] { 0, 0, 0, 0, 0, 0, 0 };
            Cost = 0;
            Name = "";
            Description = "";
            Type = ItemType.Equip;
        }
    }
}
