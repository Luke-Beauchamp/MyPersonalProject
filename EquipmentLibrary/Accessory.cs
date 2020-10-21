using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ItemLibrary;
using CharacterLibrary;

namespace EquipmentLibrary
{
    public class Accessory : Equipment
    {
        public int MaxLifeMod { get; set; } //Increases MaxLife by a fixed amount
        public int MaxResourceMod { get; set; } //Increases MaxResource by a fixed amount
        public bool[] StatusImmunity { get; set; } //Immunity from certain statuses, each index representing false for no immunity and true for
                                                   //immunity
                                                   //{ need to finish status effects enum }

        public Accessory(int maxLifeMod, int maxResourceMod, bool[] statusImmunity, Slot slot, int[] attributes, int cost, string name, 
            string description, ItemType type) :
            base(slot, attributes, cost, name, description, type)
        {
            MaxLifeMod = maxLifeMod;
            MaxResourceMod = maxResourceMod;
            StatusImmunity = statusImmunity;
        }

        public Accessory(string name, int rapport, int floor) : base(name)
        {
            Random rand = new Random();
            Slot = Slot.Accessory;
            string[] randName = { "Yo-yo ", "Banner ", "Fanny Pack " };
            Name = randName[rand.Next(3)] + name;
            Attributes = new int[7] { 0, 0, 0, 0, 0, 0, 0 };
            //New Improved Rapport Indicator Formatting for randomized Accessory
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
                    MaxLifeMod = (3 * floor) + rapport + rand.Next(1, 4);
                    break;
                case 2:
                    MaxLifeMod = (2 * floor) + rapport + rand.Next(2);
                    MaxResourceMod = floor + rapport + rand.Next(1, 4);
                    break;
                case 3:
                    MaxLifeMod = floor + rapport + rand.Next(3);
                    MaxResourceMod = floor + rapport + rand.Next(3);
                    Attributes[rand.Next(7)] = floor + rapport + rand.Next(-1, 2);
                    break;
                case 4:
                    MaxLifeMod = floor + rapport + rand.Next(1, 4);
                    Attributes[rand.Next(3)] = floor + rapport + rand.Next(-1, 2);
                    Attributes[rand.Next(3, 6)] = floor + rapport + rand.Next(-1, 2);
                    break;
                case 5:
                    MaxResourceMod = floor + rapport + rand.Next(1, 4);
                    Attributes[rand.Next(3)] = floor + rapport + rand.Next(-1, 2);
                    Attributes[rand.Next(3, 6)] = floor + rapport + rand.Next(-1, 2);
                    break;
                case 6:
                    MaxLifeMod = floor + rand.Next(3);
                    MaxResourceMod = floor + rand.Next(3);
                    Attributes[rand.Next(2)] = floor + rapport + rand.Next(-1, 2);
                    Attributes[rand.Next(2, 4)] = floor + rapport + rand.Next(-1, 2);
                    Attributes[rand.Next(4, 6)] = floor + rapport + rand.Next(-1, 2);
                    break;
            }
        }

        public Accessory() //Creates a "blank" Accessory Equipment
        {
            MaxLifeMod = 0;
            MaxResourceMod = 0;
            StatusImmunity = new bool[] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
            Slot = Slot.Accessory;
            Attributes = new int[] { 0, 0, 0, 0, 0, 0, 0 };
            Cost = 0;
            Name = "";
            Description = "";
            Type = ItemType.Equip;
        }
    }
}
