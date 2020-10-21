using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CharacterLibrary;
using ItemLibrary;
using SkillLibrary;

namespace MonsterLibrary
{
    public class Monster : Character
    {
        //In this class I will define the Monster object as a child of Character
        //Monsters will have an Item[] which will be the potential drops from that monster, the Item[] will contain
        // -Weapons     -Heads      -OffHands       -DiceShards
        //Monsters will have a minimum gold amount dropped that will be randomized upon player victory in a battle
        //Monsters will have a minimum damage and a maximum damamge
        //Monsters will have a Skill[] containing the skills the can use.  Might look something like:
        // Skill[] Skills = new Skill[] {fireball, fireball, fireball, slash};  This would be randomized to determine which skill is
        //  actually used, giving fireball a greater chance at being called.

        //Fields


        public List<Item> ItemDrop { get; set; }
        public int GoldDrop { get; set; }
        public int MaxDamage { get; set; }
        public int MinDamage { get; set; }
        public Skill[] Skills { get; set; }
        public int SkillFreq { get; set; } //Sets frequency of skill use: 0 - 0% || to || 10 - 100%



        public Monster(int floor)//This will create a random "flat" monster based on the floor.
        {
            Random rand = new Random();
            int randNbr = rand.Next(6);
            StatusDuration = new int[17] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            Shield = 0;
            Boosts = new int[14, 3];
            for (int a = 0; a < 14; a++)
            {
                for (int b = 0; b < 3; b++)
                {
                    Boosts[a, b] = 0;
                }
            }
            MaxDamage = (floor * 4) + 1;
            MinDamage = (floor * 2) + 1;
            SkillType skillType = (randNbr + 2) % 2 == 0 ? SkillType.Offense : SkillType.Support;
            Skills = new Skill[] { new Skill("", skillType, "") };
            //Skills Here
            //Skills Here
            //Get to Skills Here!!!!!!!!
            Name = "";
            Status = new bool[] { true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
            LifeMax = 10 + floor;
            //Chances
            HitChance = 50 + floor;
            ShieldBonus = 5 + (floor / 2);
            ResistChance = 5 + (floor / 2);
            DodgeChance = 5 + (floor / 2); ;
            //Attributes
            Strength = floor;
            Defense = floor;
            Intelligence = floor;
            Wisdom = floor;
            Dexterity = floor;
            Agility = floor;
            switch (randNbr)
            {
                case 0:
                    PrimaryAttribute = Attributes.Strength;
                    ResourceAttribute = Attributes.Stamina;
                    Strength += 2 * floor;
                    break;
                case 1:
                    PrimaryAttribute = Attributes.Defense;
                    ResourceAttribute = Attributes.Stamina;
                    Defense += 2 * floor;
                    break;
                case 2:
                    PrimaryAttribute = Attributes.Intelligence;
                    ResourceAttribute = Attributes.Knowledge;
                    Intelligence += 2 * floor; ;
                    break;
                case 3:
                    PrimaryAttribute = Attributes.Wisdom;
                    ResourceAttribute = Attributes.Knowledge;
                    Wisdom += 2 * floor; ;
                    break;
                case 4:
                    PrimaryAttribute = Attributes.Dexterity;
                    ResourceAttribute = Attributes.Intuition;
                    Dexterity += 2 * floor; ;
                    break;
                case 5:
                    PrimaryAttribute = Attributes.Agility;
                    ResourceAttribute = Attributes.Intuition;
                    Agility += 2 * floor; ;
                    break;
            }
            Name = PrimaryAttribute + " Type";
            //Try to imagine 6 skills, basic, scaled by floor
            //Try to imagine 6 skills, chance to nullify emotions
            int resource = ResourceAttribute == Attributes.Stamina ? 4 + floor : ResourceAttribute == Attributes.Knowledge ?
                floor : 2 + floor;
            ResourceMax = resource;
            Resource = ResourceAttribute != Attributes.Intuition ? resource : 0;
            Life = FindLife();
        }

        public Monster(string random) : base(random)
        {
            //This allows me to create a random monster!
            //Can also allow me to create a BOSS monster!
            //All I have to do is set up a switch for random and make whatever i want within the switch...
            StatusDuration = new int[17] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            Shield = 0;
            Boosts = new int[13, 3];
            for (int a = 0; a < 13; a++)
            {
                for (int b = 0; b < 3; b++)
                {
                    Boosts[a, b] = 0;
                }
            }
            string type = random.ToUpper();
            Random picker = new Random();
            int pick = picker.Next(1, 5);
            ItemDrop = new List<Item> { };


            switch (type)
            {
                case "RANDOM":
                    MaxDamage = 8 / pick;
                    MinDamage = 4 / pick;
                    //Skills Here
                    //Skills Here
                    //Get to Skills Here!!!!!!!!
                    Name = pick == 1 ? "Papa Bear" : pick == 2 ? "Mama Bear" : pick == 3 ? "Baby Bear" : "Teddy Bear";
                    Status = new bool[] { true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
                    LifeMax = pick == 1 ? 25 : pick == 2 ? 20 : pick == 3 ? 10 : 5;
                    //Chances
                    HitChance = pick == 1 ? 65 : pick == 2 ? 50 : pick == 3 ? 35 : 25;
                    ShieldBonus = pick == 1 ? 10 : pick == 2 ? 8 : pick == 3 ? 5 : 0;
                    ResistChance = pick == 1 ? 10 : pick == 2 ? 8 : pick == 3 ? 5 : 0;
                    DodgeChance = pick == 1 ? 10 : pick == 2 ? 8 : pick == 3 ? 5 : 0;
                    //Attributes
                    Strength = 12 / pick;
                    Defense = 12 / pick;
                    Intelligence = 4 / pick;
                    Wisdom = 4 / pick;
                    Dexterity = 8 / pick;
                    Agility = 8 / pick;
                    do //Add random number of dice shards upon defeat
                    {
                        ItemDrop.Add(new Item("Dice Shard", "It looks like a piece of a di", ItemType.Consumable));
                        pick++;
                    } while (pick <= 4);

                    pick = picker.Next(6);
                    switch (pick)
                    {
                        case 0:
                            PrimaryAttribute = Attributes.Strength;
                            Strength += 5;
                            break;
                        case 1:
                            PrimaryAttribute = Attributes.Defense;
                            Defense += 5;
                            break;
                        case 2:
                            PrimaryAttribute = Attributes.Intelligence;
                            Intelligence += 5;
                            break;
                        case 3:
                            PrimaryAttribute = Attributes.Wisdom;
                            Wisdom += 5;
                            break;
                        case 4:
                            PrimaryAttribute = Attributes.Dexterity;
                            Dexterity += 5;
                            break;
                        case 5:
                            PrimaryAttribute = Attributes.Agility;
                            Agility += 5;
                            break;
                    }
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
                    ResourceMax = 10;
                    Resource = ResourceAttribute != Attributes.Intuition ? 10 : 0;
                    Life = FindLife();
                    break;
                case "BOSS":
                    MaxDamage = 7;
                    MinDamage = 4;
                    //Skills Here
                    //Skills Here
                    //Get to Skills Here!!!!!!!!
                    Name = "Grizzly Bear";
                    Status = new bool[] { true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
                    LifeMax = 60;
                    //Chances
                    HitChance = 55;
                    ShieldBonus = 0;
                    ResistChance = 10;
                    DodgeChance = 10;
                    //Attributes
                    Strength = 10;
                    Defense = 8;
                    Intelligence = 4;
                    Wisdom = 4;
                    Dexterity = 8;
                    Agility = 8;
                    switch (picker.Next(6))
                    {
                        case 0:
                            PrimaryAttribute = Attributes.Strength;
                            Strength += 7;
                            break;
                        case 1:
                            PrimaryAttribute = Attributes.Defense;
                            Defense += 7;
                            break;
                        case 2:
                            PrimaryAttribute = Attributes.Intelligence;
                            Intelligence += 7;
                            break;
                        case 3:
                            PrimaryAttribute = Attributes.Wisdom;
                            Wisdom += 7;
                            break;
                        case 4:
                            PrimaryAttribute = Attributes.Dexterity;
                            Dexterity += 7;
                            break;
                        case 5:
                            PrimaryAttribute = Attributes.Agility;
                            Agility += 7;
                            break;
                    }
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
                    ResourceMax = 10;
                    Resource = ResourceAttribute != Attributes.Intuition ? 10 : 0;
                    Life = FindLife();
                    break;
            }
        }

        public void Define(int random, int floor)
        {
            Random rand = new Random();
            int str = Strength + rand.Next(random); int def = Defense + rand.Next(random);
            int intel = Intelligence + rand.Next(random); int wis = Wisdom + rand.Next(random);
            int dex = Dexterity + rand.Next(random); int agi = Agility + rand.Next(random);
            int life = LifeMax + rand.Next(random); int resource = ResourceMax;
            int minDmg = MinDamage; int maxDmg = MaxDamage;
            int mod = random == 1 ? 0 :
                        random == 2 ? 3 :
                        random == 3 ? 5 :
                        random == 4 ? 10 : 20;
            life += random == 1 ? 2 : random == 2 ? 2 + floor : random == 3 ? -2 - floor : random == 4 ? 2 + (floor * 2) : 20 + (floor * 7);
            minDmg += random == 1 ? 0 : random == 2 ? 1 : random == 2 ? 2 : random == 4 ? 2 : 5;
            maxDmg += random == 1 ? 1 : random == 2 ? 2 : random == 2 ? 3 : random == 4 ? 3 : 6;
            
            string name = random == 1 ? "Tiny " : random == 2 ? "" : random == 3 ? "Aggressive " : random == 4 ? "Large " : "Monstrous ";
            switch (PrimaryAttribute)
            {
                case Attributes.Strength:
                    name += floor < 3 ? "Boar" : floor < 6 ? "Crusher" : "Orc";
                    str += mod;
                    switch (Skills[0].Type)
                    {
                        case SkillType.Offense:
                            Skills[0].Name = random == 1 ? "Rush" : random == 2 ? "Rush" : random == 3 ? "Thrash" : random == 4 ? "Thrash" : "Crush";
                            Skills[0].DamageType = random <= 4 ? DamageType.Single : DamageType.Double;
                            Skills[0].Damage = random <= 2 ? 6 : random <= 4 ? 8 : 6;
                            Skills[0].Cost = random <= 2 ? 5 : random <= 4 ? 4 : 3;
                            Skills[0].Status = random <= 2 ? StatusEffect.Unsteady : random <= 4 ? StatusEffect.Stagger : StatusEffect.Tremble;
                            Skills[0].StatusChance = random == 1 ? 50 : random == 2 ? 55 : random == 3 ? 60 : random == 4 ? 65 : 70;
                            Skills[0].StatusDuration = random <= 2 ? 2 : random <= 4 ? 3 : 4;
                            break;
                        case SkillType.Support:
                            Skills[0].Name = random == 1 ? "Grunt" : random == 2 ? "Grunt" : random == 3 ? "Battle Cry" : random == 4 ? "Battle Cry" : "Harden";
                            Skills[0].DamageType = DamageType.Boost;
                            Skills[0].Damage = random <= 2 ? 10 : random <= 4 ? 12 : 16;
                            Skills[0].BoostStat = random <= 2 ? 4 : random <= 4 ? 0 : 1;
                            Skills[0].BoostDuration = random <= 3 ? 2 : random <= 4 ? 3 : 4;
                            Skills[0].Cost = random <= 2 ? 5 : random <= 4 ? 4 : 3;
                            Skills[0].Status = random <= 2 ? StatusEffect.Presence : random <= 4 ? StatusEffect.Certain : StatusEffect.Nimble;
                            Skills[0].StatusChance = random == 1 ? 50 : random == 2 ? 55 : random == 3 ? 60 : random == 4 ? 65 : 70;
                            Skills[0].StatusDuration = random <= 2 ? 2 : random <= 4 ? 3 : 4;
                            break;
                    }
                    //Add Skill 
                    break;
                case Attributes.Defense:
                    name += floor < 3 ? "Bear" : floor < 6 ? "Brute" : "Ogre";
                    def += mod;
                    switch (Skills[0].Type)
                    {
                        case SkillType.Offense:
                            Skills[0].Name = random <= 2 ? "Pin Down" : random <= 4 ? "Smash" : "Toss";
                            Skills[0].DamageType = DamageType.Single;
                            Skills[0].Damage = random == 1 ? 3 : random == 2 ? 4 : random == 3 ? 6 : random == 4 ? 8 : 10;
                            Skills[0].Cost = random <= 2 ? 5 : random <= 4 ? 4 : 3;
                            Skills[0].Status = random <= 2 ? StatusEffect.Fumble : random <= 4 ? StatusEffect.Sink : StatusEffect.Whimsy;
                            Skills[0].StatusChance = random == 1 ? 65 : random == 2 ? 70 : random <= 4 ? 50 : 70;
                            Skills[0].StatusDuration = random <= 3 ? 2 : 3;
                            break;
                        case SkillType.Support:
                            Skills[0].Name = random <= 2 ? "Thick Skin" : random <= 4 ? "Roar" : "Tough Hide";
                            Skills[0].DamageType = random <= 4 ? DamageType.Boost : DamageType.Shield;
                            Skills[0].Damage = random <= 2 ? 10 : random <= 4 ? 12 : 10;
                            Skills[0].BoostStat = random <= 2 ? 1 : random <= 4 ? 8 : 0;
                            Skills[0].BoostDuration = random == 1 ? 2 : random <= 4 ? 3 : 0;
                            Skills[0].Cost = random <= 2 ? 5 : random <= 4 ? 4 : 3;
                            Skills[0].Status = random <= 2 ? StatusEffect.Resilient : random <= 4 ? StatusEffect.Presence : StatusEffect.Resilient;
                            Skills[0].StatusChance = random == 1 ? 55 : random == 2 ? 60 : random == 3 ? 45 : random == 4 ? 55 : 70;
                            Skills[0].StatusDuration = random <= 3 ? 2 : 3;
                            break;
                    }
                    //Add Skill
                    break;
                case Attributes.Intelligence:
                    name += floor < 3 ? "Wisp" : floor < 6 ? "Fey" : "Elf";
                    intel += mod;
                    switch (Skills[0].Type)
                    {
                        case SkillType.Offense:
                            Skills[0].Name = random <= 2 ? "Chill" : random <= 4 ? "Spinal Shiver" : "Bolt";
                            Skills[0].DamageType = DamageType.Single;
                            Skills[0].Damage = random <= 2 ? 1 : random <= 4 ? 6 : 6;
                            Skills[0].Cost = random <= 2 ? 3 : random <= 4 ? 4 : 2;
                            Skills[0].Status = random <= 2 ? StatusEffect.Fade : random <= 4 ? StatusEffect.Tremble : StatusEffect.Conflict;
                            Skills[0].StatusChance = random <= 2 ? 70 : random <= 4 ? 65 : 60;
                            Skills[0].StatusDuration = random <= 2 ? 3 : random <= 4 ? 2 : 3;
                            break;
                        case SkillType.Support:
                            Skills[0].Name = random <= 2 ? "Vapor" : random <= 4 ? "Shimmer" : "Soothe";
                            Skills[0].DamageType = random <= 4 ? DamageType.Boost : DamageType.Heal;
                            Skills[0].Damage = random <= 4 ? 8 : 15;
                            Skills[0].BoostStat = random <= 2 ? 10 : random <= 4 ? 8 : 0;
                            Skills[0].BoostDuration = random <= 2 ? 2 : random <= 4 ? 3 : 0;
                            Skills[0].Cost = random <= 2 ? 4 : 3;
                            Skills[0].Status = random <= 2 ? StatusEffect.Nimble : random <= 4 ? StatusEffect.Presence : StatusEffect.Relish;
                            Skills[0].StatusChance = random <= 2 ? 65 : random <= 4 ? 55 : 70;
                            Skills[0].StatusDuration = random <= 4 ? 3 : 4;
                            break;
                    }
                    //Add Skill
                    break;
                case Attributes.Wisdom:
                    name += floor < 3 ? "Slime" : floor < 6 ? "Ooze" : "Siren";
                    wis += mod;
                    switch (Skills[0].Type)
                    {
                        case SkillType.Offense:
                            Skills[0].Name = random <= 2 ? "Muck" : random <= 4 ? "Fling" : "Whirlpool";
                            Skills[0].DamageType = DamageType.Single;
                            Skills[0].Damage = random == 1 ? 2 : random == 2 ? 3 : random == 3 ? 8 : random == 4 ? 6 : 9;
                            Skills[0].Cost = random <= 2 ? 4 : 3;
                            Skills[0].Status = random <= 2 ? StatusEffect.Whimsy : random <= 4 ? StatusEffect.Fumble : StatusEffect.Sink;
                            Skills[0].StatusChance = random <= 2 ? 60 : random == 3 ? 40 : random == 4 ? 55 : 75;
                            Skills[0].StatusDuration = random <= 2 ? 2 : 3;
                            break;
                        case SkillType.Support:
                            Skills[0].Name = random <= 2 ? "Amorphous" : random <= 4 ? "Bubble" : "Hidden Power";
                            Skills[0].DamageType = random <= 2 ? DamageType.Boost : random <= 4 ? DamageType.Shield : DamageType.Boost;
                            Skills[0].Damage = random <= 2 ? 6 : random <= 4 ? 7 : 15;
                            Skills[0].BoostStat = random <= 4 ? 1 : 6;
                            Skills[0].BoostDuration = random <= 2 ? 3 : random <= 4 ? 0 : 3;
                            Skills[0].Cost = random <= 2 ? 4 : 3; ;
                            Skills[0].Status = random <= 2 ? StatusEffect.Resilient : random <= 4 ? StatusEffect.Tower : StatusEffect.Rage;
                            Skills[0].StatusChance = random <= 2 ? 60 : random <= 4 ? 45 : 65;
                            Skills[0].StatusDuration = random <= 4 ? 2 : 3;
                            break;
                    }
                    //Add Skill
                    break;
                case Attributes.Dexterity:
                    name += floor < 3 ? "Spider" : floor < 6 ? "Serpent" : "Naga";
                    dex += mod;
                    switch (Skills[0].Type)
                    {
                        case SkillType.Offense:
                            Skills[0].Name = random <= 2 ? "Spit" : random <= 4 ? "Bind" : "Wrap";
                            Skills[0].DamageType = random <= 4 ? DamageType.Single : DamageType.Triple;
                            Skills[0].Damage = random <= 2 ? 3 : random == 3 ? 8 : random == 4 ? 6 : 4;
                            Skills[0].Cost = random <= 4 ? 5 : 3;
                            Skills[0].Status = random <= 2 ? StatusEffect.Unsteady : random <= 4 ? StatusEffect.Fumble : StatusEffect.Fade;
                            Skills[0].StatusChance = random <= 4 ? 50 : 75;
                            Skills[0].StatusDuration = random <= 3 ? 2 : 4;
                            break;
                        case SkillType.Support:
                            Skills[0].Name = random <= 2 ? "Spin" : random <= 4 ? "Slither" : "Grounded";
                            Skills[0].DamageType = DamageType.Boost;
                            Skills[0].Damage = random <= 4 ? 8 : 15;
                            Skills[0].BoostStat = random <= 2 ? 5 : random <= 4 ? 4 : 1;
                            Skills[0].BoostDuration = random == 1 || random == 3 ? 2 : 3;
                            Skills[0].Cost = random <= 4 ? 5 : 3;
                            Skills[0].Status = random <= 2 ? StatusEffect.Nimble : random <= 4 ? StatusEffect.Presence : StatusEffect.Certain;
                            Skills[0].StatusChance = random <= 3 ? 45 : random == 4 ? 55 : 70;
                            Skills[0].StatusDuration = random <= 3 ? 2 : 3;
                            break;
                    }
                    //Add Skill
                    break;
                case Attributes.Agility:
                    name += floor < 3 ? "Bat" : floor < 6 ? "Shade" : "Specter";
                    agi += mod;
                    switch (Skills[0].Type)
                    {
                        case SkillType.Offense:
                            Skills[0].Name = random <= 2 ? "Swoop" : random <= 4 ? "Flurry" : "Frighten";
                            Skills[0].DamageType = random <= 2 ? DamageType.Single : random <= 4 ? DamageType.Triple : DamageType.Single;
                            Skills[0].Damage = random <= 2 ? 4 : random == 3 ? 3 : random == 4 ? 2 : 6;
                            Skills[0].Cost = random <= 4 ? 5 : 3;
                            Skills[0].Status = random <= 2 ? StatusEffect.Whimsy : random <= 4 ? StatusEffect.Stagger : StatusEffect.Tremble;
                            Skills[0].StatusChance = random == 1 ? 55 : random == 2 ? 60 : random <= 4 ? 45 : 80;
                            Skills[0].StatusDuration = 3;
                            break;
                        case SkillType.Support:
                            Skills[0].Name = random <= 2 ? "Soar" : random <= 4 ? "Impervious" : "Anguished Cry";
                            Skills[0].DamageType = random <= 2 || random == 5 ? DamageType.Boost : DamageType.Cleanse;
                            Skills[0].Damage = random <= 4 ? 8 : 15;
                            Skills[0].BoostStat = random <= 4 ? 10 : 1;
                            Skills[0].BoostDuration = random <= 2 ? 2 : random <= 4 ? 0 : 3;
                            Skills[0].Cost = random <= 4 ? 5 : 3;
                            Skills[0].Status = random <= 2 ? StatusEffect.Nimble : random <= 4 ? StatusEffect.Certain : StatusEffect.Relish;
                            Skills[0].StatusChance = random <= 3 ? 50 : random == 4 ? 60 : 80;
                            Skills[0].StatusDuration = random <= 4 ? 2 : 4;
                            break;
                    }
                    //Add Skill
                    break;
            }
            Name = name; Strength = str; Defense = def; Intelligence = intel; Wisdom = wis; Dexterity = dex; Agility = agi;
            MinDamage = minDmg; MaxDamage = maxDmg; LifeMax = life; ResourceMax = resource;
            Life = FindLife();
            Resource = ResourceAttribute != Attributes.Intuition ? ResourceMax : 0;
        }

        public int MinDmg()
        {
            int minDmg = MinDamage + (Strength / 10);
            return minDmg;
        }

        public int MaxDmg()
        {
            int maxDmg = MaxDamage + (Strength / 5);
            return maxDmg;
        }

        public override int CalcDamage(Character player, Character monster) //This will happen after a CalcHit
        {
            Random rand = new Random();
            int damageDealt = rand.Next(MinDamage, MaxDamage + 1);
            damageDealt += (Strength / 5);
            return damageDealt;
        }

        public int FindLife()
        {
            int maxLife = LifeMax;
            maxLife += PrimaryAttribute == Attributes.Strength ? (Strength / 2) : PrimaryAttribute == Attributes.Defense ? (Defense / 2) : PrimaryAttribute == Attributes.Intelligence ?
                (Intelligence / 2) : PrimaryAttribute == Attributes.Wisdom ? (Wisdom / 2) : PrimaryAttribute == Attributes.Dexterity ? (Dexterity / 2) :
                PrimaryAttribute == Attributes.Agility ? (Agility / 2) : 0;
            return maxLife;
        }

        public void LifeColor()
        {
            if (Life >= ((FindLife() * 9) / 10)) { Console.ForegroundColor = ConsoleColor.Green; }
            else if (Life > (2 * (FindLife() / 3))) { Console.ForegroundColor = ConsoleColor.Yellow; }
            else if (Life > FindLife() / 3) { Console.ForegroundColor = ConsoleColor.DarkYellow; }
            else if (Life > FindLife() / 10) { Console.ForegroundColor = ConsoleColor.Red; }
            else { Console.ForegroundColor = ConsoleColor.DarkRed; }
        }

        public void ResourceColor()
        {
            if (Resource >= ((ResourceMax * 9) / 10)) { Console.ForegroundColor = ConsoleColor.Green; }
            else if (Resource > (2 * (ResourceMax / 3))) { Console.ForegroundColor = ConsoleColor.Yellow; }
            else if (Resource > ResourceMax / 3) { Console.ForegroundColor = ConsoleColor.DarkYellow; }
            else if (Resource > ResourceMax / 10) { Console.ForegroundColor = ConsoleColor.Red; }
            else { Console.ForegroundColor = ConsoleColor.DarkRed; }
        }

    }
}
