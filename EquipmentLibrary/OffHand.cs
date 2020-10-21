using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ItemLibrary;
using CharacterLibrary;

namespace EquipmentLibrary
{
    public class OffHand : Equipment
    {
        public OffHandType OHType { get; set; } //Sticking with the offhand being exclusive to class
        public int OHPrimaryBonus { get; set; } //Will Modify Primary Attribute
        public int OHResourceBonus { get; set; } //Will Modify Resource Attribute
        public int OHBonus { get; set; } //Will Modify Based on OHType
        // PowerFist - min/max damage bonus // ArmGuard - Block Chance bonus // Tome - skill damage bonus
        // Orb - Resist Chance bonus // Lasso - Hit Chance bonus // Powder - Dodge Chance bonus

        public OffHand(OffHandType oHType, int oHPrimaryBonus, int oHResourceBonus, int oHBonus, Slot slot, int[] attributes, int cost, 
            string name, string description, ItemType type) :
            base(slot, attributes, cost, name, description, type)
        {
            OHType = oHType;
            OHPrimaryBonus = oHPrimaryBonus;
            OHResourceBonus = oHResourceBonus;
            OHBonus = oHBonus;
        }

        public OffHand(string name, int rapport, int floor) : base(name)
        {
            //rapport will be a randomized addition to stats chosen
            Random rand = new Random();
            //floor will be a constant increase to stats depending on how far in the dungeon the player is
            Slot = Slot.OffHand;
            OHType = OffHandType.None;
            Attributes = new int[7] { 0, 0, 0, 0, 0, 0, 0 };
            //New Improved Rapport Indicator Formatting for randomized OffHands
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
            switch (rand.Next(1, 7))
            {
                case 1: OHType = OffHandType.ArmGuard;
                    Name = "Arm Guard " + name;
                    break;
                case 2: OHType = OffHandType.Lasso;
                    Name = "Lasso " + name;
                    break;
                case 3: OHType = OffHandType.Orb;
                    Name = "Orb " + name;
                    break;
                case 4: OHType = OffHandType.Powder;
                    Name = "Powder " + name;
                    break;
                case 5: OHType = OffHandType.PowerFist;
                    Name = "Powerfist " + name;
                    break;
                case 6: OHType = OffHandType.Tome;
                    Name = "Tome " + name;
                    break;
            }
            switch (rand.Next(1, 7)) // Randomize the stats.
            {
                case 1://Do Math On all of these to account for the floor of the dungeon and rapport of the character.
                    OHPrimaryBonus = (3*floor) + rapport + rand.Next(1, 4);
                    break;
                case 2:
                    OHPrimaryBonus = (2*floor) + rapport + rand.Next(3);
                    OHResourceBonus = floor + rapport + rand.Next(3);
                    break;
                case 3:
                    OHPrimaryBonus = floor + rapport + rand.Next(3);
                    OHResourceBonus = floor + rapport + rand.Next(3);
                    OHBonus = floor + rapport + rand.Next(3);
                    break;
                case 4:
                    OHPrimaryBonus = floor + rapport + rand.Next(1, 4);
                    OHBonus = floor + rapport + rand.Next(3);
                    Attributes[rand.Next(6)] = floor + rapport + rand.Next(-1, 2);
                    break;
                case 5:
                    OHResourceBonus = floor + rapport + rand.Next(2);
                    OHBonus = floor + rapport + rand.Next(1, 4);
                    Attributes[rand.Next(6)] = floor + rapport + rand.Next(-1, 2);
                    break;
                case 6:
                    OHBonus = floor + rapport + rand.Next(1, 4);
                    Attributes[rand.Next(2)] = floor + rapport + rand.Next(-1, 2);
                    Attributes[rand.Next(2, 4)] = floor + rapport + rand.Next(-1, 2);
                    Attributes[rand.Next(4, 6)] = floor + rapport + rand.Next(-1, 2);
                    break;
            }
        }

        public OffHand() //Ceates a "blank" OffHand Equipment
        {
            OHType = OffHandType.None;
            OHPrimaryBonus = 0;
            OHResourceBonus = 0;
            OHBonus = 0;
            Slot = Slot.OffHand;
            Attributes = new int[] { 0, 0, 0, 0, 0, 0, 0 };
            Cost = 0;
            Name = "";
            Description = "";
            Type = ItemType.Equip;

        }
    }
}
