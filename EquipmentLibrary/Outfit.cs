using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ItemLibrary;
using CharacterLibrary;

namespace EquipmentLibrary
{
    public class Outfit : Equipment
    {
        public int[] Chances { get; set; } //will have an array with the size of 4 = to the chance modifier (cumulatively to the player's current)
                                           //[0, 1, 2, 3] at these indexes
                                           //0: Hit Chance modifier
                                           //1: Block Chance Modifier
                                           //2: Resist Chance Modifier
                                           //3: Dodge Chance Modifier

        public Outfit(int[] chances, Slot slot, int[] attributes, int cost, string name, string description, ItemType type) : 
            base(slot, attributes, cost, name, description, type)
        {
            Chances = chances;
        }

        public Outfit(string name, int rapport, int floor) : base(name)
        {
            Random rand = new Random();
            int nbrRand;
            Slot = Slot.Outfit;
            string[] randName = { "Robe ", "Armor ", "Tunic " };
            string[] randName2 = { "of Wizardry", "of Blocking", "of Ninjary" };
            Name = randName[rand.Next(3)] + name;
            Attributes = new int[7] { 0, 0, 0, 0, 0, 0, 0 };
            Chances = new int[4] { 0, 0, 0, 0 };
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
            switch (rand.Next(1, 7)) // Randomize the stats.
            {
                case 1://Do Math On all of these to account for the floor of the dungeon and rapport of the character.
                    Chances[0] = (3 * floor) + rapport + rand.Next(1, 4);
                    break;
                case 2:
                    Chances[2] = (3 * floor) + rapport + rand.Next(1, 4);
                    break;
                case 3:
                    Chances[3] = (3 * floor) + rapport + rand.Next(1, 4);
                    break;
                case 4:
                    Chances[0] = floor + rapport + rand.Next(1, 4);
                    Attributes[nbrRand = rand.Next(7)] = nbrRand == 6 ? (floor*2) : (floor*2) + rapport + rand.Next(-1, 2);
                    break;
                case 5:
                    Chances[2] = floor + rapport + rand.Next(1, 4);
                    Attributes[nbrRand = rand.Next(7)] = nbrRand == 6 ? (floor * 2) : (floor*2) + rapport + rand.Next(-1, 2);
                    break;
                case 6:
                    Chances[3] = floor + rapport + rand.Next(1, 4);
                    Attributes[nbrRand = rand.Next(7)] = nbrRand == 6 ? (floor * 2) : (floor * 2) + rapport + rand.Next(-1, 2);
                    break;
            }
        }

        public Outfit() //Creates a "blank" Outfit Equipment
        {
            Chances = new int[] { 0, 0, 0, 0 };
            Slot = Slot.Outfit;
            Attributes = new int[] { 0, 0, 0, 0, 0, 0, 0 };
            Cost = 0;
            Name = "";
            Description = "";
            Type = ItemType.Equip;
        }
    }
}
