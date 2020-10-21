using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterLibrary
{
    public class Character
    {
        //In this class I will define the parent class Character
        //Children of this class will include Player/Monster/NPC.  Note: Player and NPC might have another parent under Character called "Humanoid"
        //If I give NPCs stats too, I can potentially make Allies a possibility
        //Potential Fields for all 3:
        /*
         string Name;   int Strength;    int Defense;   int Stamina;    int Intelligence;   int Wisdom;     int Knowledge;
         int Dexterity;     int Agility;    int Intuition;
         int HitChance;     int BlockChance;        int DodgeChance;        int Life;       int MaxLife;        int Mana;       int MaxMana;
         */

        private int _life;

        public string Name { get; set; }
        public bool[] Status { get; set; }
        public int[] StatusDuration { get; set; }
        public int[,] Boosts { get; set; }
        public int LifeMax { get; set; }
        public int Life
        {
            get { return _life; }
            set { _life = value >= 0 ? value : 0; }
        }
        public int Shield { get; set; }
        public string[] Image { get; set; }
        public Attributes PrimaryAttribute { get; set; }
        public Attributes ResourceAttribute { get; set; }
        public int ResourceStat { get; set; }
        public int ResourceMax { get; set; }
        public int Resource { get; set; }
        public int Strength { get; set; }
        public int Defense { get; set; }
        public int ShieldBonus { get; set; }
        public int Intelligence { get; set; }
        public int Wisdom { get; set; }
        public int ResistChance { get; set; }
        public int Dexterity { get; set; }
        public int HitChance { get; set; }
        public int Agility { get; set; }
        public int DodgeChance { get; set; }

        public Character(int floor, Attributes primaryAttribute)
        {
            Shield = 0;
            Boosts = new int[14, 3];
            for (int a = 0; a < 13; a++)
            {
                for (int b = 0; b < 3; b++)
                {
                    Boosts[a, b] = 0;
                }
            }
            Status = new bool[17] { true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
            StatusDuration = new int[17] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            LifeMax = (floor * 5) + 50;
            Life = (floor * 5) + 50;
            Strength = (floor*5) + 5;
            Defense = (floor * 5) + 5;
            ShieldBonus = 5;
            Intelligence = (floor*5) + 5;
            Wisdom = (floor *5)+ 5;
            ResistChance = 5;
            Dexterity = (floor * 5) + 5;
            Agility = (floor * 5) + 5;
            DodgeChance = 5;
            HitChance = 55;
            PrimaryAttribute = primaryAttribute;
            switch (primaryAttribute)
            {
                case Attributes.Strength:
                    Strength += floor + 11;
                    ResourceAttribute = Attributes.Stamina;
                    break;
                case Attributes.Defense:
                    Defense += floor + 11;
                    ResourceAttribute = Attributes.Stamina;
                    break;
                case Attributes.Intelligence:
                    Intelligence += floor + 11;
                    ResourceAttribute = Attributes.Knowledge;
                    break;
                case Attributes.Wisdom:
                    Wisdom += floor + 11;
                    ResourceAttribute = Attributes.Knowledge;
                    break;
                case Attributes.Dexterity:
                    Dexterity += floor + 11;
                    ResourceAttribute = Attributes.Intuition;
                    break;
                case Attributes.Agility:
                    Agility += floor + 11;
                    ResourceAttribute = Attributes.Intuition;
                    break;
            }

        }

        public Character(string name, bool[] status, int lifeMax, int hitChance, int blockChance, int resistChance, int dodgeChance,
            Attributes primaryAttribute, int strength, int defense, int intelligence, int wisdom, int dexterity, int agility, int resourceStat)
        {
            Name = name;
            Status = status;
            LifeMax = lifeMax;
            Life = lifeMax;
            HitChance = hitChance;
            ShieldBonus = blockChance;
            ResistChance = resistChance;
            DodgeChance = dodgeChance;
            PrimaryAttribute = primaryAttribute;
            Strength = strength;
            Defense = defense;
            Intelligence = intelligence;
            Wisdom = wisdom;
            Dexterity = dexterity;
            Agility = agility;
            ResourceStat = resourceStat;
            switch (PrimaryAttribute)//Sets Resource when the object is created
            {
                case Attributes.Strength:
                case Attributes.Defense:
                    ResourceAttribute = Attributes.Stamina;
                    break;
                case Attributes.Intelligence:
                case Attributes.Wisdom:
                    ResourceAttribute = Attributes.Knowledge;
                    break;
                case Attributes.Dexterity:
                case Attributes.Agility:
                    ResourceAttribute = Attributes.Intuition;
                    break;
            }
            switch (ResourceAttribute)
            {
                case Attributes.Stamina:
                    ResourceMax = 10;
                    Resource = 10;
                    break;
                case Attributes.Knowledge:
                    ResourceMax = 5;
                    Resource = 5;
                    break;
                case Attributes.Intuition:
                    ResourceMax = 10;
                    Resource = 0;
                    break;
            }
        }

        public Character(string name)
        {
            Name = name;
        }
        //Combat Methods
        public Character()
        {

        }

        public virtual void GetShield(int amount)
        {
            amount += Wisdom / 10;
            Shield = amount;
        }

        public virtual bool CalcHit(int attackerHit, int defenderDodge)
        {
            bool doesHit;
            //Compare attacker hitchance to defender block chance
            Random rand = new Random();
            int diceRoll = rand.Next(1, 101);
            int chance = attackerHit - defenderDodge;
            diceRoll += chance;
            doesHit = diceRoll > 50 ? true : false;
            return doesHit;
        }

        public virtual int CalcDamage(Character attacker, Character defender)
        {
            return 0;
        }

        public int[] DamageMod()
        {
            int[] mods = new int[2];
            mods[0] = Strength / 4; //MIN DAMAGE
            mods[1] = Strength / 2; //MAX DAMAGE
            return mods;
        }

        public int[] StatusDoes() //Returning an array that holds modifications to character (used SceneBattle)
        {
            int[] statsAffected = new int[11] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            //[0: Strength, 1: Defense, 2: Intelligence, 3: Wisdom, 4: Dexterity, 5: Agility, 6: Core Attribute, 
            //7: Shield, 8: HitChance, 9: ResistChance, 10: DodgeChance]
            if (Status[0])
            {

            }
            else
            {
                if (Status[1]) //Tower: Will Increase the Character's Defense by 20%
                {
                    statsAffected[1] += Defense/5;
                }
                if (Status[2])//Tremble: Will Decrease the Character's Defense by 20%
                {
                    statsAffected[1] -= Defense/5;
                }
                if (Status[3])//Certain: Will increase the Character's Hit/Resist/Dodge by 10%
                {
                    statsAffected[8] += 10;
                    statsAffected[9] += 10;
                    statsAffected[10] += 10;
                }
                if (Status[4])//Conflict: Will decrease the Character's Hit/Resist/Dodge by 10%
                {
                    statsAffected[8] -= 10;
                    statsAffected[9] -= 10;
                    statsAffected[10] -= 10;
                }
                if (Status[5])//Presence: Will increase the Character's Hit by 20%
                {
                    statsAffected[8] += 20;
                }
                if (Status[6])//Whimsy: Will decrease the Character's Hit by 20%
                {
                    statsAffected[8] -= 20;
                }
                if (Status[7])//Nimble: Will increase the Character's Dodge by 20%;
                {
                    statsAffected[10] += 20;
                }
                if (Status[8])//Fumble: Will decrease the Character's Dodge by 20%;
                {
                    statsAffected[10] += 20;
                }
                if (Status[9])//Rage: Will increase the Character's Strength by 20%
                {
                    statsAffected[0] += Strength / 5;
                }
                if (Status[10])//Stagger: Will decrease the Character's Strength by 20%
                {
                    statsAffected[0] -= Strength / 5;
                }
                if (Status[11])//Resilient: Will increase the Character's Resist by 20%;
                {
                    statsAffected[9] += 20;
                }
                if (Status[12])//Fade: Will decrease the Character's Resist by 20%;
                {
                    statsAffected[9] -= 20;
                }
                if (Status[13])//Relish: Will increase the Character's Core by 20%
                {
                    switch (PrimaryAttribute)
                    {
                        case Attributes.Strength:
                            statsAffected[0] += Strength / 5;
                            break;
                        case Attributes.Defense:
                            statsAffected[1] += Defense / 5;
                            break;
                        case Attributes.Intelligence:
                            statsAffected[2] += Intelligence / 5;
                            break;
                        case Attributes.Wisdom:
                            statsAffected[3] += Wisdom / 5;
                            break;
                        case Attributes.Dexterity:
                            statsAffected[4] += Dexterity / 5;
                            break;
                        case Attributes.Agility:
                            statsAffected[5] += Agility / 5;
                            break;
                    }
                }
                if (Status[14])//Sink: Will decrease the Character's Core by 20%
                {
                    switch (PrimaryAttribute)
                    {
                        case Attributes.Strength:
                            statsAffected[0] -= Strength / 5;
                            break;
                        case Attributes.Defense:
                            statsAffected[1] -= Defense / 5;
                            break;
                        case Attributes.Intelligence:
                            statsAffected[2] -= Intelligence / 5;
                            break;
                        case Attributes.Wisdom:
                            statsAffected[3] -= Wisdom / 5;
                            break;
                        case Attributes.Dexterity:
                            statsAffected[4] -= Dexterity / 5;
                            break;
                        case Attributes.Agility:
                            statsAffected[5] -= Agility / 5;
                            break;
                    }
                }
                if (Status[15])//Ready: Will increase the Character's Dexterity by 20%
                {
                    statsAffected[5] += Agility/5;
                }
                if (Status[16])//Unsteady: Will decrease the Character's Dexterity by 20%
                {
                    statsAffected[5] -= Agility/5;
                }
            }
            
            return statsAffected;
        }

        public int[] UpdateBoosts()
        {
            int[] boosts = new int[14];
            for (int a = 0; a < 14; a++)
            {
                if (Boosts[a, 0] != 0) //Means active
                {
                    boosts[a] = Boosts[a, 1];
                    Boosts[a, 2] -= 1;
                    if (Boosts[a, 2] == 0)
                    {
                        Boosts[a, 0] = 0;
                        Boosts[a, 1] = 0;
                        Console.SetCursorPosition(7, 17);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write("{0} boost wore off",
                            a == 0 ? "STR" :
                            a == 1 ? "DEF" :
                            a == 2 ? "INT" :
                            a == 3 ? "WIS" :
                            a == 4 ? "DEX" :
                            a == 5 ? "AGI" :
                            a == 6 ? "Hit%" :
                            a == 7 ? "Dod%" :
                            a == 8 ? "SE%" :
                            a == 9 ? "SR%" :
                            a == 10 ? "+aDMG" :
                            a == 11 ? "+sDMG" :
                            a == 12 ? "+Heal" : "+Shield");
                        //Need scenebattle input
                        PressEnter();
                        Console.SetCursorPosition(7, 17);
                        Console.Write("                                                        ");
                    }
                    
                }
            }
            return boosts;
        }
        
        public static void PressEnter()
        {
            bool enterPressed = false;
            do
            {
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.Enter:
                    case ConsoleKey.F:
                    case ConsoleKey.NumPad5:
                        enterPressed = true;
                        break;
                }
            } while (!enterPressed);
        }
    }
}
