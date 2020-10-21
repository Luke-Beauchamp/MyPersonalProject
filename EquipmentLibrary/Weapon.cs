using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsumableLibrary;
using ItemLibrary;
using SkillLibrary;
using CharacterLibrary;

namespace EquipmentLibrary
{
    public class Weapon : Equipment
    {
        public int MinDamage { get; set; }//minDamage
        public int MaxDamage { get; set; }//maxDamage
        public int HitChanceBonus { get; set; }//hitChanceBonus
        public StatusEffect Status { get; set; }//status
        public int StatusChance { get; set; }//chance to inflict status
        public bool IsTwoHand { get; set; }//true = 2 handed weapon || false = 1 handed weapon
        //bonuseffect

        public Weapon(int minDamage, int maxDamage, int hitChanceBonus, StatusEffect status, int statusChance, Slot slot, 
            bool isTwoHand, int[] attributes, int cost, string name, string description, ItemType type) : 
            base(slot, attributes, cost, name, description, type)
        {
            MinDamage = minDamage;
            MaxDamage = maxDamage;
            HitChanceBonus = hitChanceBonus;
            Status = status;
            StatusChance = statusChance;
            IsTwoHand = isTwoHand;
        }

        public Weapon(string name, int rapport, int floor) : base(name)
        {
            Random rand = new Random();
            Slot = Slot.MainHand;
            string[] randName = { "Blade ", "Dagger ", "Scepter " };
            Name = randName[rand.Next(3)] + name;
            Attributes = new int[7] { 0, 0, 0, 0, 0, 0, 0 };
            //New Improved Rapport Indicator Formatting for randomized Weapon
            //Increase Damage per floor
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
            //For now minimal damage randomization below
            int bonus = rapport < 0 ? 0 : rapport < 3 ? 2 : 3;
            bonus = bonus * rand.Next(1, 3);
            MinDamage = rand.Next(floor, floor * 2) + bonus;
            MaxDamage = rand.Next(floor * 2, floor * 4) + bonus;
            HitChanceBonus = floor + rapport + rand.Next(5);
            Attributes[rand.Next(3)] = floor + rapport + rand.Next(5);
            Attributes[rand.Next(3, 6)] = floor + rapport + rand.Next(5);
            //Can put in a switch later if I want
        }

        public Weapon() //Creates a "blank" Weapon Equipment
        {
            MinDamage = 0;
            MaxDamage = 0;
            HitChanceBonus = 0;
            Status = StatusEffect.None;
            StatusChance = 0;
            IsTwoHand = false;
            Slot = Slot.MainHand;
            Attributes = new int[] { 0, 0, 0, 0, 0, 0, 0 };
            Cost = 0;
            Name = "";
            Description = "";
            Type = ItemType.Equip;
        }
    }
}
