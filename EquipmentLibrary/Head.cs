using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ItemLibrary;
using CharacterLibrary;

namespace EquipmentLibrary
{
    public class Head : Equipment
    {
        public int PrimaryMod { get; set; } //affects primary stat as a % (will start low and scale higher later into the dungeon)

        public Head(int primaryMod, Slot slot, int[] attributes, int cost, string name, string description, ItemType type) : 
            base(slot, attributes, cost, name, description, type)
        {
            PrimaryMod = primaryMod;
        }

        public Head(string name, int rapport, int floor) : base(name)
        {
            Random rand = new Random();
            int nbrRand;
            Slot = Slot.Head;
            string[] randName = { "Helmet ", "Cap ", "Hood " };
            Name = randName[rand.Next(3)] + name;
            Attributes = new int[7] { 0, 0, 0, 0, 0, 0, 0 };
            //New Improved Rapport Indicator Formatting for randomized Head
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
            switch (rand.Next(1, 6)) // Randomize the stats.
            {
                case 1://Do Math On all of these to account for the floor of the dungeon and rapport of the character.
                    PrimaryMod = (3 * floor) + rapport + rand.Next(1, 4);
                    break;
                case 2:
                    PrimaryMod = (2 * floor) + rapport + rand.Next(1, 4);
                    Attributes[nbrRand = rand.Next(7)] = nbrRand == 6 ? floor : floor + rapport + rand.Next(-1, 2);
                    break;
                case 3:
                    PrimaryMod = floor + rapport + rand.Next(1, 4);
                    Attributes[nbrRand = rand.Next(7)] = nbrRand == 6 ? (2 * floor) : (2 * floor) + rapport + rand.Next(-1, 2);
                    break;
                case 4:
                    PrimaryMod = floor + rapport + rand.Next(1, 4);
                    Attributes[rand.Next(3)] = floor + rapport + rand.Next(-1, 2);
                    Attributes[rand.Next(3, 6)] = floor + rapport + rand.Next(-1, 2);
                    break;
                case 5:
                    PrimaryMod = floor + rapport + rand.Next(1, 4);
                    Attributes[rand.Next(2)] = floor + rand.Next(2);
                    Attributes[rand.Next(2, 4)] = floor + rand.Next(-1, 2);
                    Attributes[rand.Next(4, 6)] = floor + rand.Next(-1, 2);
                    break;
                case 6:
                    PrimaryMod = floor + rapport + rand.Next(1, 4);
                    Attributes[rand.Next(6)] = floor + rapport + rand.Next(-1, 2);
                    break;
            }
        }

        public Head() //Creates a "blank" Head Equipment
        {
            PrimaryMod = 0;
            Slot = Slot.Head;
            Attributes = new int[] { 0, 0, 0, 0, 0, 0, 0 };
            Cost = 0;
            Name = "";
            Description = "";
            Type = ItemType.Equip;
        }

        public override int GetBonus(string selector)
        {
            int bonus = 0;
            selector = selector.ToLower();
            switch (selector)
            {
                case "str":
                    bonus = Attributes[0];
                    break;


                default:
                    bonus = 9001; //should not ever be over 9000
                    break;
            }
            return bonus;
        }
    }
}
