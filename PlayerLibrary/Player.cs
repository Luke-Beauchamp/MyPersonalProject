using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CharacterLibrary;
using MapLibrary;
using SkillLibrary;
using EquipmentLibrary;
using ItemLibrary;
using MonsterLibrary;

namespace PlayerLibrary
{
    public class Player : Humanoid
    {


        public int Prestige { get; set; } //A player will collect prestige as he completes quests, kills monsters, and completes floors of the dungeon
                                          //    -Prestige will be used as an access modifier for shops and quests along the dungeon
                                          //    -Example:   A shop on the 2nd floor of the dungeon has potions and a map for sale
                                          //            ~   The shop also offers an outfit to players who have 100 prestige
                                          //            ~   The player has to get a minimum of 75 prestige to get to the second floor (50 for completing
                                          //            ~   the first floor and 25 for defeating the boss) but could have gotten a maximum of 125 
                                          //            ~   prestige (if they killed 1 mini-boss for 25 points and completed a quest for 25 points
                                          //    -Note: Will also be a score factor updon defeat
        public int Gold { get; set; } //A player will collect gold as he completes quests, kills monsters, and completes floors of the dungeon
                                      //        -Gold will be used as a currency to buy items at shops throughout the dungeon
                                      //        -Similarly, some NPCs might ask for gold, in which case the player will have an option (if they have any)
                                      //            to give the NPC the gold
        public int zToggle { get; set; }
        public Skill[,] Skills { get; set; }
        public int Floor { get; set; }
        public int[] SelectedSkill { get; set; } //Will be used for calling PlayerSkill()
        public EquipSet Equipment { get; set; }
        public bool[] Emotional { get; set; } //Used in randomizing NPCs through a random sequence of emotions TODO: relate to map, when full false
        public int[] Itemable { get; set; } //Used in randomizing Items given and ensuring the same item-type isn't dropped too often
        public int DiceShards { get; set; }


        public Player(int floor, Attributes primaryAttribute) : base(floor, primaryAttribute)
        {
            Weapon weapon = new Weapon(4, 7, 0, StatusEffect.None, 0, Slot.MainHand, false, new int[7]
            { 0, 0, 0, 0, 0, 0, 0 }, 0, "", "", ItemType.Equip);

            switch (primaryAttribute)
            {
                case Attributes.Strength:
                    weapon.Name = "Sword of Starting";
                    weapon.HitChanceBonus += 5;
                    break;
                case Attributes.Defense:
                    weapon.Name = "Mace of Starting";
                    weapon.MinDamage += 2;
                    weapon.MaxDamage++; //(7-10)
                    weapon.HitChanceBonus += 5;
                    break;
                case Attributes.Intelligence:
                    weapon.Name = "Scepter of Starting";
                    weapon.MinDamage--;
                    weapon.MaxDamage--;//(4-8)
                    weapon.HitChanceBonus += 5;
                    break;
                case Attributes.Wisdom:
                    weapon.Name = "Wand of Starting";
                    weapon.MinDamage += 2; //(7-9)
                    weapon.HitChanceBonus += 5;
                    break;
                case Attributes.Dexterity:
                    weapon.Name = "Dagger of Starting";
                    weapon.MinDamage -= 2;
                    weapon.MaxDamage++; //(3-10)
                    weapon.HitChanceBonus += 5;
                    break;
                case Attributes.Agility:
                    weapon.Name = "Dirk of Starting";
                    weapon.MinDamage--;
                    weapon.MaxDamage += 2; //(4-11)
                    weapon.HitChanceBonus += 5;
                    break;
            }
            weapon.MinDamage += floor;
            weapon.MaxDamage += floor;
            weapon.HitChanceBonus += floor / 2;
            Skills = new Skill[4, 0];

            int x = 0;
            int y = 0;

            LifeMax = 50;
            SelectedSkill = new int[2] { -1, 0 };
            Equipment = new EquipSet(new Head(), new Outfit(), new Amulet(), new Accessory(),
                        weapon, new OffHand());
            Emotional = new bool[8] { true, true, true, true, true, true, true, true };
            DiceShards = 0;
            Itemable = new int[3] { 1, 1, 1 };

        }

        public Player(Emotion primary, Emotion secondary, Emotion tertiary, string name, bool[] status, int lifeMax, int hitChance,
            int blockChance, int resistChance, int dodgeChance, Attributes primaryAttribute, int strength, int defense, int intelligence,
            int wisdom, int dexterity, int agility, int resourceStat) :
            base(primary, secondary, tertiary, name, status, lifeMax, hitChance, blockChance, resistChance,
                dodgeChance, primaryAttribute, strength, defense, intelligence, wisdom, dexterity, agility, resourceStat)
        {
            Shield = 0;
            Boosts = new int[13, 3];
            for (int a = 0; a < 13; a++)
            {
                for (int b = 0; b < 3; b++)
                {
                    Boosts[a, b] = 0;
                }
            }
            StatusDuration = new int[17] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            SelectedSkill = new int[2] { -1, 0 };
            DiceShards = 0;
            Emotional = new bool[] { true, true, true, true, true, true, true, true };
            Itemable = new int[3] { 1, 1, 1 };
            Prestige = 100;
            Gold = 100;
            //The player's initial rapport will be determined by primary/secondary/tertiary//quaternary//quinary emotions
            Rapport = new int[] { 2, 2, 2, 2, 2, 2, 2, 2 };
            //Primary
            int index = primary == Emotion.Anger ? 0 : primary == Emotion.Anticipation ? 1 : primary == Emotion.Disgust ? 2 :
                primary == Emotion.Fear ? 3 : primary == Emotion.Joy ? 4 : primary == Emotion.Sadness ? 5 : primary == Emotion.Surprise ? 6 : 7;
            Rapport[index] += 3;
            //Secondary
            index = secondary == Emotion.Anger ? 0 : secondary == Emotion.Anticipation ? 1 : secondary == Emotion.Disgust ? 2 :
                secondary == Emotion.Fear ? 3 : secondary == Emotion.Joy ? 4 : secondary == Emotion.Sadness ? 5 : secondary == Emotion.Surprise ? 6 : 7;
            Rapport[index] += 2;
            //Tertiary
            index = tertiary == Emotion.Anger ? 0 : tertiary == Emotion.Anticipation ? 1 : tertiary == Emotion.Disgust ? 2 :
                tertiary == Emotion.Fear ? 3 : tertiary == Emotion.Joy ? 4 : tertiary == Emotion.Sadness ? 5 : tertiary == Emotion.Surprise ? 6 : 7;
            Rapport[index] += 1;
            //Opposite
            index = Opposite == Emotion.Anger ? 0 : Opposite == Emotion.Anticipation ? 1 : Opposite == Emotion.Disgust ? 2 :
                Opposite == Emotion.Fear ? 3 : Opposite == Emotion.Joy ? 4 : Opposite == Emotion.Sadness ? 5 : Opposite == Emotion.Surprise ? 6 : 7;
            Rapport[index] -= 2;
            //Quaternary
            switch (Quaternary)
            {
                case Emotion.Terror:
                    index = 3;
                    break;
                case Emotion.Admiration:
                    index = 7;
                    break;
                case Emotion.Ecstasy:
                    index = 4;
                    break;
                case Emotion.Vigilance:
                    index = 1;
                    break;
                case Emotion.Rage:
                    index = 0;
                    break;
                case Emotion.Loathing:
                    index = 2;
                    break;
                case Emotion.Grief:
                    index = 5;
                    break;
                case Emotion.Amazement:
                    index = 6;
                    break;
                case Emotion.Submission:
                    index = primary == Emotion.Fear ? 7 : 3;
                    break;
                case Emotion.Love:
                    index = primary == Emotion.Trust ? 4 : 7;
                    break;
                case Emotion.Optimism:
                    index = primary == Emotion.Joy ? 1 : 4;
                    break;
                case Emotion.Aggressiveness:
                    index = primary == Emotion.Anger ? 1 : 0;
                    break;
                case Emotion.Contempt:
                    index = primary == Emotion.Anger ? 2 : 0;
                    break;
                case Emotion.Remorse:
                    index = primary == Emotion.Disgust ? 5 : 2;
                    break;
                case Emotion.Disapproval:
                    index = primary == Emotion.Sadness ? 6 : 5;
                    break;
                case Emotion.Awe:
                    index = primary == Emotion.Surprise ? 3 : 6;
                    break;
            }
            Rapport[index] += 1;
            switch (index)
            {
                case 0:
                    index = 3;
                    break;
                case 1:
                    index = 6;
                    break;
                case 2:
                    index = 7;
                    break;
                case 3:
                    index = 0;
                    break;
                case 4:
                    index = 5;
                    break;
                case 5:
                    index = 4;
                    break;
                case 6:
                    index = 1;
                    break;
                case 7:
                    index = 2;

                    break;
            }
            Rapport[index] -= 1;
            //Quinary
            switch (Quinary)
            {
                case Emotion.Apprehension:
                    index = 3;
                    break;
                case Emotion.Acceptance:
                    index = 7;
                    break;
                case Emotion.Serenity:
                    index = 4;
                    break;
                case Emotion.Interest:
                    index = 1;
                    break;
                case Emotion.Annoyance:
                    index = 0;
                    break;
                case Emotion.Boredom:
                    index = 2;
                    break;
                case Emotion.Pensiveness:
                    index = 5;
                    break;
                case Emotion.Distraction:
                    index = 6;
                    break;
                case Emotion.Submission:
                    index = primary == Emotion.Fear ? 7 : 3;
                    break;
                case Emotion.Love:
                    index = primary == Emotion.Trust ? 4 : 7;
                    break;
                case Emotion.Optimism:
                    index = primary == Emotion.Joy ? 1 : 4;
                    break;
                case Emotion.Aggressiveness:
                    index = primary == Emotion.Anger ? 1 : 0;
                    break;
                case Emotion.Contempt:
                    index = primary == Emotion.Anger ? 2 : 0;
                    break;
                case Emotion.Remorse:
                    index = primary == Emotion.Disgust ? 5 : 2;
                    break;
                case Emotion.Disapproval:
                    index = primary == Emotion.Sadness ? 6 : 5;
                    break;
                case Emotion.Awe:
                    index = primary == Emotion.Surprise ? 3 : 6;
                    break;
            }
            Rapport[index] += 1;
            switch (index)
            {
                case 0:
                    index = 3;
                    break;
                case 1:
                    index = 6;
                    break;
                case 2:
                    index = 7;
                    break;
                case 3:
                    index = 0;
                    break;
                case 4:
                    index = 5;
                    break;
                case 5:
                    index = 4;
                    break;
                case 6:
                    index = 1;
                    break;
                case 7:
                    index = 2;

                    break;
            }
            Rapport[index] -= 1;
            //The player's first equipment set will be determined at character creation.  Inherently determined by PrimaryAttribute
            Equipment = PrimaryAttribute == Attributes.Strength ? new EquipSet(new Head(), new Outfit(), new Amulet(), new Accessory(),
                        new Weapon(4, 8, 10, StatusEffect.None, 0, Slot.MainHand, false, new int[7], 0,
                        "Bronze Longsword", "A longsword made from bronze.", ItemType.Equip), new OffHand()) :
                     PrimaryAttribute == Attributes.Defense ? new EquipSet(new Head(), new Outfit(), new Amulet(), new Accessory(),
                     new Weapon(4, 5, 5, StatusEffect.None, 0, Slot.MainHand, false, new int[7], 0,
                     "Bronze Mace", "A mace with a head made from bronze.", ItemType.Equip), new OffHand()) :
                        PrimaryAttribute == Attributes.Intelligence ? new EquipSet(new Head(), new Outfit(), new Amulet(), new Accessory(),
                        new Weapon(3, 6, 5, StatusEffect.None, 0, Slot.MainHand, false, new int[7], 0,
                        "Greenwood Scepter", "A scepter made from greenwood.", ItemType.Equip), new OffHand()) :
                     PrimaryAttribute == Attributes.Wisdom ? new EquipSet(new Head(), new Outfit(), new Amulet(), new Accessory(),
                     new Weapon(2, 7, 10, StatusEffect.None, 0, Slot.MainHand, true, new int[7], 0,
                     "Greenwood Scepter", "A scepter made from greenwood", ItemType.Equip), new OffHand()) :
                        PrimaryAttribute == Attributes.Dexterity ? new EquipSet(new Head(), new Outfit(), new Amulet(), new Accessory(),
                        new Weapon(3, 7, 15, StatusEffect.None, 0, Slot.MainHand, false, new int[7], 0,
                        "Bronze Dagger", "A dagger made from bronze.", ItemType.Equip), new OffHand()) :
                     new EquipSet(new Head(), new Outfit(), new Amulet(), new Accessory(),
                     new Weapon(3, 8, 5, StatusEffect.None, 0, Slot.MainHand, true, new int[7], 0,
                     "Iron Dagger", "A dagger made from iron.", ItemType.Equip), new OffHand());
            //Initialize Skills
            Skills = new Skill[4, 10];
            int x = 0;
            int y = 0;
            do
            {
                Skills[y, x] = new Skill("", SkillType.Exploration, "");
                Skills[y, x].Emotion = Emotion.Null;
                x += x == 9 ? -9 : 1;
                y += x == 9 ? 1 : 0;
            } while (y != 4);
            Skill tempSkill1 = new Skill("", SkillType.Exploration, "");
            Skill tempSkill2 = new Skill("", SkillType.Exploration, "");
            Skill tempSkill3 = new Skill("", SkillType.Exploration, "");
            //Initialize First "Core" Skill
            switch (PrimaryAttribute) // Setting up first "Core" Skill
            {
                case Attributes.Strength:
                    Skills[0, 0] = new Skill("Bash", SkillType.Offense, SkillClass.Strength, Emotion.Null,
                        Emotion.Null, 5, 10, DamageType.Single, StatusEffect.None, 0,
                        "Deal minor damage.");//Damage
                    tempSkill1 = new Skill("Bash", SkillType.Offense, SkillClass.Strength, Emotion.Null,
                        Emotion.Null, 5, 10, DamageType.Single, StatusEffect.None, 0,
                        "Deal minor damage.");
                    tempSkill2 = new Skill("Bash", SkillType.Offense, SkillClass.Strength, Emotion.Null,
                        Emotion.Null, 5, 10, DamageType.Single, StatusEffect.None, 0,
                        "Deal minor damage.");
                    tempSkill3 = new Skill("Bash", SkillType.Offense, SkillClass.Strength, Emotion.Null,
                        Emotion.Null, 5, 10, DamageType.Single, StatusEffect.None, 0,
                        "Deal minor damage.");
                    break;
                case Attributes.Defense:
                    Skills[0, 0] = new Skill("Block", SkillType.Support, SkillClass.Defense, Emotion.Null,
                        Emotion.Null, 5, 10, DamageType.Shield, StatusEffect.None, 0,
                        "Shield minor damage.");//Shield
                    tempSkill1 = new Skill("Block", SkillType.Support, SkillClass.Defense, Emotion.Null,
                        Emotion.Null, 5, 10, DamageType.Shield, StatusEffect.None, 0,
                        "Shield minor damage.");
                    tempSkill2 = new Skill("Block", SkillType.Support, SkillClass.Defense, Emotion.Null,
                        Emotion.Null, 5, 10, DamageType.Shield, StatusEffect.None, 0,
                        "Shield minor damage.");
                    tempSkill3 = new Skill("Block", SkillType.Support, SkillClass.Defense, Emotion.Null,
                        Emotion.Null, 5, 10, DamageType.Shield, StatusEffect.None, 0,
                        "Shield minor damage.");
                    break;
                case Attributes.Intelligence:
                    Skills[0, 0] = new Skill("Burn", SkillType.Offense, SkillClass.Intelligence, Emotion.Null,
                        Emotion.Null, 1, 12, DamageType.Single, StatusEffect.None, 0,
                        "Deal minor damage.");//Damage
                    tempSkill1 = new Skill("Burn", SkillType.Offense, SkillClass.Intelligence, Emotion.Null,
                        Emotion.Null, 1, 12, DamageType.Single, StatusEffect.None, 0,
                        "Deal minor damage.");
                    tempSkill2 = new Skill("Burn", SkillType.Offense, SkillClass.Intelligence, Emotion.Null,
                        Emotion.Null, 1, 12, DamageType.Single, StatusEffect.None, 0,
                        "Deal minor damage.");
                    tempSkill3 = new Skill("Burn", SkillType.Offense, SkillClass.Intelligence, Emotion.Null,
                        Emotion.Null, 1, 12, DamageType.Single, StatusEffect.None, 0,
                        "Deal minor damage.");
                    break;
                case Attributes.Wisdom:
                    Skills[0, 0] = new Skill("Bright", SkillType.Support, SkillClass.Wisdom, Emotion.Null,
                        Emotion.Null, 1, 12, DamageType.Heal, StatusEffect.None, 0,
                        "Heal minor damage.");//Heal
                    tempSkill1 = new Skill("Bright", SkillType.Support, SkillClass.Wisdom, Emotion.Null,
                        Emotion.Null, 1, 12, DamageType.Heal, StatusEffect.None, 0,
                        "Heal minor damage.");
                    tempSkill2 = new Skill("Bright", SkillType.Support, SkillClass.Wisdom, Emotion.Null,
                        Emotion.Null, 1, 12, DamageType.Heal, StatusEffect.None, 0,
                        "Heal minor damage.");
                    tempSkill3 = new Skill("Bright", SkillType.Support, SkillClass.Wisdom, Emotion.Null,
                        Emotion.Null, 1, 12, DamageType.Heal, StatusEffect.None, 0,
                        "Heal minor damage.");
                    break;
                case Attributes.Dexterity:
                    Skills[0, 0] = new Skill("Break", SkillType.Offense, SkillClass.Dexterity, Emotion.Null,
                        Emotion.Null, 3, 5, DamageType.Double, StatusEffect.None, 0,
                        "Deal minor damage x2.");//Damage Double
                    tempSkill1 = new Skill("Break", SkillType.Offense, SkillClass.Dexterity, Emotion.Null,
                        Emotion.Null, 3, 5, DamageType.Double, StatusEffect.None, 0,
                        "Deal minor damage x2.");
                    tempSkill2 = new Skill("Break", SkillType.Offense, SkillClass.Dexterity, Emotion.Null,
                        Emotion.Null, 3, 5, DamageType.Double, StatusEffect.None, 0,
                        "Deal minor damage x2.");
                    tempSkill3 = new Skill("Break", SkillType.Offense, SkillClass.Dexterity, Emotion.Null,
                        Emotion.Null, 3, 5, DamageType.Double, StatusEffect.None, 0,
                        "Deal minor damage x2.");
                    break;
                case Attributes.Agility:
                    Skills[0, 0] = new Skill("Bend", SkillType.Support, SkillClass.Agility, Emotion.Null,
                        Emotion.Null, 3, 10, DamageType.Boost, StatusEffect.None, 0,
                        "Minor Agility boost, short duration.");//Increased Agility
                    Skills[0, 0].BoostDuration = 2;
                    Skills[0, 0].BoostStat = 20;
                    tempSkill1 = new Skill("Bend", SkillType.Support, SkillClass.Agility, Emotion.Null,
                        Emotion.Null, 3, 10, DamageType.Boost, StatusEffect.None, 0,
                        "Minor Agility boost, short duration.");//Increased Agility
                    tempSkill1.BoostDuration = 2;
                    tempSkill1.BoostStat = 10;
                    tempSkill2 = new Skill("Bend", SkillType.Support, SkillClass.Agility, Emotion.Null,
                        Emotion.Null, 3, 10, DamageType.Boost, StatusEffect.None, 0,
                        "Minor Agility boost, short duration.");//Increased Agility
                    tempSkill2.BoostDuration = 2;
                    tempSkill2.BoostStat = 10;
                    tempSkill3 = new Skill("Bend", SkillType.Support, SkillClass.Agility, Emotion.Null,
                        Emotion.Null, 3, 10, DamageType.Boost, StatusEffect.None, 0,
                        "Minor Agility boost, short duration.");//Increased Agility
                    tempSkill3.BoostDuration = 2;
                    tempSkill3.BoostStat = 10;
                    break;
                default:
                    break;
            }
            //Initialize First "Primary" Skill
            tempSkill1.Emotion = Primary;
            tempSkill1.EmotionSwitch = Primary;
            tempSkill1.GiveStatus(70, 2);
            Skills[1, 0] = tempSkill1;
            //Initialize First "Secondary" Skill
            tempSkill2.Emotion = Secondary;
            tempSkill2.EmotionSwitch = Secondary;
            tempSkill2.GiveStatus(70, 2);
            Skills[2, 0] = tempSkill2;
            //Initialize First "Tertiary" Skill
            tempSkill3.Emotion = Tertiary;
            tempSkill3.EmotionSwitch = Tertiary;
            tempSkill3.GiveStatus(70, 2);
            Skills[3, 0] = tempSkill3;
        }


        public int[,] PBattler()
        {
            int[,] pBattler = new int[2, 6];
            //[0, x] x = 0 : STR  1: DEF  2: INT  3: WIS  4: DEX  5: AGI
            //[1, x] x = 0 : Hit%  1: Dod%  2: SE%  3: SR%
            //[2, x] x = 0 : aDMG  1: sDMG  2: +Heal  3: +Shield

            //All of this has to be assumed that Statuses and Boosts have both been updated

            //Start by figuring out the actual attributes.  These will be able to determine the rest of the fields.
            int strength; int defense; int intelligence; int wisdom; int dexterity; int agility;
            //let's look at EqModAttrs()

            return pBattler;
        }

        //EqAttrMod returns an int[]- stores "Combat-Ready" modifications for attributes[ 0 ,  1 ,  2 ,  3 ,  4 ,  5 ,  6 ]
        public int[] EqAttrMod() //Will follow same container rules as Equipment.Attributes (str, def, int, wis, dex, agi, res)
        {
            int[] attrMods = new int[7];
            int index = 0;
            foreach (int mod in attrMods)
            {
                attrMods[index] = Equipment.EquippedHead.Attributes[index] + Equipment.EquippedAmulet.Attributes[index] +
                Equipment.EquippedOutfit.Attributes[index] + Equipment.EquippedAccessory.Attributes[index] +
                Equipment.EquippedWeapon.Attributes[index] + Equipment.EquippedOffHand.Attributes[index];
                index++;
            }
            //Primary Stat Modification Below
            index = PrimaryAttribute == Attributes.Strength ? 0 :
                PrimaryAttribute == Attributes.Defense ? 1 :
                PrimaryAttribute == Attributes.Intelligence ? 2 :
                PrimaryAttribute == Attributes.Wisdom ? 3 :
                PrimaryAttribute == Attributes.Dexterity ? 4 : 5;
            attrMods[index] += Equipment.EquippedHead.PrimaryMod + Equipment.EquippedOffHand.OHPrimaryBonus;
            attrMods[6] += Equipment.EquippedAmulet.ResourceMod + Equipment.EquippedOffHand.OHResourceBonus;
            //Resource Stat Modification Below
            return attrMods;
        }


        public int[,] EquipmentMods()
        {
            int[,] equipmentMods = new int[3, 6];
            int index = 0;
            //Below [0, x] : The attributes 
            for (int i = 0; i < 6; i++)
            {
                equipmentMods[0, i] =
                    Equipment.EquippedHead.Attributes[i] + Equipment.EquippedAmulet.Attributes[i] +
                Equipment.EquippedOutfit.Attributes[i] + Equipment.EquippedAccessory.Attributes[i] +
                Equipment.EquippedWeapon.Attributes[i] + Equipment.EquippedOffHand.Attributes[i];
            }
            index = PrimaryAttribute == Attributes.Strength ? 0 :
                PrimaryAttribute == Attributes.Defense ? 1 :
                PrimaryAttribute == Attributes.Intelligence ? 2 :
                PrimaryAttribute == Attributes.Wisdom ? 3 :
                PrimaryAttribute == Attributes.Dexterity ? 4 : 5;
            equipmentMods[0, index] += Equipment.EquippedHead.PrimaryMod + Equipment.EquippedOffHand.OHPrimaryBonus;


            return equipmentMods;
        }

        //ChancesMod returns an int[]- stores "Combat-Ready" modifications for chances [ 0 ,   1  ,   2   ,   3  ]
        public int[] ChancesMod() //Will follow same container rules as Outfit.Chances (hit, block, resist, dodge)
        {
            int[] chancesMods = new int[4];
            int index = 0;
            foreach (int mod in chancesMods)
            {
                chancesMods[index] = Equipment.EquippedOutfit.Chances[index];
            }
            chancesMods[0] += Equipment.EquippedOffHand.OHType == OffHandType.Lasso ?
                Equipment.EquippedOffHand.OHBonus : 0;
            chancesMods[1] += Equipment.EquippedOffHand.OHType == OffHandType.ArmGuard ?
                Equipment.EquippedOffHand.OHBonus : 0;
            chancesMods[2] += Equipment.EquippedOffHand.OHType == OffHandType.Orb ?
                Equipment.EquippedOffHand.OHBonus : 0;
            chancesMods[3] += Equipment.EquippedOffHand.OHType == OffHandType.Powder ?
                Equipment.EquippedOffHand.OHBonus : 0;
            int[] attrMods = EqAttrMod();
            chancesMods[0] += chancesMods[0] + ((attrMods[4] + Dexterity) / 2);
            chancesMods[1] += chancesMods[1] + ((attrMods[1] + Defense) / 2);
            chancesMods[2] += chancesMods[2] + ((attrMods[3] + Wisdom) / 2);
            chancesMods[3] += chancesMods[3] + ((attrMods[5] + Agility) / 2);
            return chancesMods;
        }

        public void InitializeRapport()
        {
            Emotion primary = Primary;
            Emotion secondary = Secondary;
            Emotion tertiary = Tertiary;
            //Primary
            int index = primary == Emotion.Anger ? 0 : primary == Emotion.Anticipation ? 1 : primary == Emotion.Disgust ? 2 :
                primary == Emotion.Fear ? 3 : primary == Emotion.Joy ? 4 : primary == Emotion.Sadness ? 5 : primary == Emotion.Surprise ? 6 : 7;
            Rapport[index] += 3;
            //Secondary
            index = secondary == Emotion.Anger ? 0 : secondary == Emotion.Anticipation ? 1 : secondary == Emotion.Disgust ? 2 :
                secondary == Emotion.Fear ? 3 : secondary == Emotion.Joy ? 4 : secondary == Emotion.Sadness ? 5 : secondary == Emotion.Surprise ? 6 : 7;
            Rapport[index] += 2;
            //Tertiary
            index = tertiary == Emotion.Anger ? 0 : tertiary == Emotion.Anticipation ? 1 : tertiary == Emotion.Disgust ? 2 :
                tertiary == Emotion.Fear ? 3 : tertiary == Emotion.Joy ? 4 : tertiary == Emotion.Sadness ? 5 : tertiary == Emotion.Surprise ? 6 : 7;
            Rapport[index] += 1;
            //Opposite
            index = Opposite == Emotion.Anger ? 0 : Opposite == Emotion.Anticipation ? 1 : Opposite == Emotion.Disgust ? 2 :
                Opposite == Emotion.Fear ? 3 : Opposite == Emotion.Joy ? 4 : Opposite == Emotion.Sadness ? 5 : Opposite == Emotion.Surprise ? 6 : 7;
            Rapport[index] -= 2;
            //Quaternary
            switch (Quaternary)
            {
                case Emotion.Terror:
                    index = 3;
                    break;
                case Emotion.Admiration:
                    index = 7;
                    break;
                case Emotion.Ecstasy:
                    index = 4;
                    break;
                case Emotion.Vigilance:
                    index = 1;
                    break;
                case Emotion.Rage:
                    index = 0;
                    break;
                case Emotion.Loathing:
                    index = 2;
                    break;
                case Emotion.Grief:
                    index = 5;
                    break;
                case Emotion.Amazement:
                    index = 6;
                    break;
                case Emotion.Submission:
                    index = primary == Emotion.Fear ? 7 : 3;
                    break;
                case Emotion.Love:
                    index = primary == Emotion.Trust ? 4 : 7;
                    break;
                case Emotion.Optimism:
                    index = primary == Emotion.Joy ? 1 : 4;
                    break;
                case Emotion.Aggressiveness:
                    index = primary == Emotion.Anger ? 1 : 0;
                    break;
                case Emotion.Contempt:
                    index = primary == Emotion.Anger ? 2 : 0;
                    break;
                case Emotion.Remorse:
                    index = primary == Emotion.Disgust ? 5 : 2;
                    break;
                case Emotion.Disapproval:
                    index = primary == Emotion.Sadness ? 6 : 5;
                    break;
                case Emotion.Awe:
                    index = primary == Emotion.Surprise ? 3 : 6;
                    break;
            }
            Rapport[index] += 1;
            switch (index)
            {
                case 0:
                    index = 3;
                    break;
                case 1:
                    index = 6;
                    break;
                case 2:
                    index = 7;
                    break;
                case 3:
                    index = 0;
                    break;
                case 4:
                    index = 5;
                    break;
                case 5:
                    index = 4;
                    break;
                case 6:
                    index = 1;
                    break;
                case 7:
                    index = 2;

                    break;
            }
            Rapport[index] -= 1;
            //Quinary
            switch (Quinary)
            {
                case Emotion.Apprehension:
                    index = 3;
                    break;
                case Emotion.Acceptance:
                    index = 7;
                    break;
                case Emotion.Serenity:
                    index = 4;
                    break;
                case Emotion.Interest:
                    index = 1;
                    break;
                case Emotion.Annoyance:
                    index = 0;
                    break;
                case Emotion.Boredom:
                    index = 2;
                    break;
                case Emotion.Pensiveness:
                    index = 5;
                    break;
                case Emotion.Distraction:
                    index = 6;
                    break;
                case Emotion.Submission:
                    index = primary == Emotion.Fear ? 7 : 3;
                    break;
                case Emotion.Love:
                    index = primary == Emotion.Trust ? 4 : 7;
                    break;
                case Emotion.Optimism:
                    index = primary == Emotion.Joy ? 1 : 4;
                    break;
                case Emotion.Aggressiveness:
                    index = primary == Emotion.Anger ? 1 : 0;
                    break;
                case Emotion.Contempt:
                    index = primary == Emotion.Anger ? 2 : 0;
                    break;
                case Emotion.Remorse:
                    index = primary == Emotion.Disgust ? 5 : 2;
                    break;
                case Emotion.Disapproval:
                    index = primary == Emotion.Sadness ? 6 : 5;
                    break;
                case Emotion.Awe:
                    index = primary == Emotion.Surprise ? 3 : 6;
                    break;
            }
            Rapport[index] += 1;
            switch (index)
            {
                case 0:
                    index = 3;
                    break;
                case 1:
                    index = 6;
                    break;
                case 2:
                    index = 7;
                    break;
                case 3:
                    index = 0;
                    break;
                case 4:
                    index = 5;
                    break;
                case 5:
                    index = 4;
                    break;
                case 6:
                    index = 1;
                    break;
                case 7:
                    index = 2;

                    break;
            }
            Rapport[index] -= 1;
        }

        public void CreateSkill(int floor)
        {
            int skillPoints = (floor*4) + 6;//Want to balance skill points to get this to 5 at level 1
            bool loop = true;
            int selector = 0;
            Skill[,] skills = new Skill[Skills.GetLength(0), Skills.GetLength(1) + 1];
            int x;
            int y;
            for (x = 0; x < Skills.GetLength(1); x++)
            {
                skills[0, x] = Skills[0, x];
                skills[1, x] = Skills[1, x];
                skills[2, x] = Skills[2, x];
                skills[3, x] = Skills[3, x];
            }
            Skills = skills;
            Skill thisSkill = new Skill("", SkillType.Exploration, "");
            Skill thisSkill1 = new Skill("", SkillType.Exploration, "");
            Skill thisSkill2 = new Skill("", SkillType.Exploration, "");
            Skill thisSkill3 = new Skill("", SkillType.Exploration, "");
            //CheckSkills()
            //Can offer an option to replace a skill, returning some of the skill points required to make that skill back
            //Consider adding a skillpointvalue property to skills to make tracking this easier
            //
            //ReplaceSkill()
            SceneClear();
            #region Type
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.SetCursorPosition(7, 8); Console.Write("//Skill Creator");
            Console.ForegroundColor = ConsoleColor.White; Console.Write(": ");
            Console.ForegroundColor = ConsoleColor.Green; Console.Write("{0}", skillPoints);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" skill points remaining\t\t{0} Max: ", ResourceAttribute);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("{0}", FindResource());
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(7, 9); Console.Write("Select the \"Type\" this skill will be");
            do
            {
                Console.SetCursorPosition(7, 10);
                Console.ForegroundColor = ConsoleColor.Green;
                switch (selector)
                {
                    case 0:
                        Console.Write("Offensive skills target the enemy");
                        break;
                    case 1:
                        Console.Write("Supportive skills target yourself");
                        break;
                }
                DrawInput(new string[] { "//Offense", "//Support" }, selector);
                switch (Console.ReadKey(true).Key)
                {

                    case ConsoleKey.D:
                    case ConsoleKey.NumPad6:
                    case ConsoleKey.Tab:
                    case ConsoleKey.Add:
                    case ConsoleKey.A:
                    case ConsoleKey.NumPad4:
                        selector = selector == 0 ? 1 : 0;
                        break;
                    case ConsoleKey.F:
                    case ConsoleKey.NumPad5:
                        bool looped = true;
                        switch (selector)
                        {
                            case 0:
                                DrawInput(new string[] { "//Confirm Offense" }, 0);
                                break;
                            case 1:
                                DrawInput(new string[] { "//Confirm Support" }, 0);
                                break;
                        }
                        do
                        {
                            switch (Console.ReadKey(true).Key)
                            {
                                case ConsoleKey.F:
                                case ConsoleKey.NumPad5:
                                    looped = false;
                                    loop = false;
                                    break;
                                case ConsoleKey.Escape:
                                case ConsoleKey.Subtract:
                                    looped = false;
                                    break;
                            }
                        } while (looped);
                        break;

                }
            } while (loop);
            thisSkill.Type = selector == 0 ? SkillType.Offense : SkillType.Support;
            thisSkill1.Type = selector == 0 ? SkillType.Offense : SkillType.Support;
            thisSkill2.Type = selector == 0 ? SkillType.Offense : SkillType.Support;
            thisSkill3.Type = selector == 0 ? SkillType.Offense : SkillType.Support;
            SceneClear();
            #endregion//Add an eventual "Passive" Skil

            #region Cost
            loop = true;
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.SetCursorPosition(7, 8); Console.Write("//Skill Creator");
            Console.ForegroundColor = ConsoleColor.White; Console.Write(": ");
            Console.ForegroundColor = ConsoleColor.Green; Console.Write("{0}", skillPoints);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" skill points remaining\t\t{0} Max: ", ResourceAttribute);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("{0}", FindResource());
            Console.SetCursorPosition(7, 9); Console.Write("//"); Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("{0}", selector == 0 ? "Offensive" : "Supportive"); Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" Skill Being Created");
            selector = 0;
            Console.SetCursorPosition(7, 10); Console.Write("Select the cost of this skill");
            string[] costs = ResourceAttribute == Attributes.Stamina ? new string[] { "2", "4", "7", "10", "15", "30" } :
                ResourceAttribute == Attributes.Knowledge ? new string[] { "0", "1", "3", "5", "10", "20" } :
                new string[] { "1", "2", "3", "5", "8", "13" };
            int[] skillCost = new int[] { 17, 13, 9, 7, 5, 1 };
            do
            {
                DrawInput(costs, selector);
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(7, 11); Console.Write("                                              ");
                Console.SetCursorPosition(7, 11); Console.Write("A cost of "); Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(costs[selector]); Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" requires "); Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("{0}", skillCost[selector]); Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" skill points");
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.D:
                    case ConsoleKey.NumPad6:
                    case ConsoleKey.Tab:
                    case ConsoleKey.Add:
                        selector += selector != 5 ? 1 : -5;
                        break;
                    case ConsoleKey.A:
                    case ConsoleKey.NumPad4:
                        selector -= selector != 0 ? 1 : -5;
                        break;
                    case ConsoleKey.F:
                    case ConsoleKey.NumPad5:
                        bool looped = true;
                        if (FindResource() < Convert.ToInt32(costs[selector]))
                        {
                            DrawInput(new string[] { "//Insuficient " + ResourceAttribute, "red" }, 0);
                            Console.ReadKey(true);
                        }
                        else
                        {
                            if (skillPoints < skillCost[selector])
                            {
                                DrawInput(new string[] { "//Insuficient Skill Points", "red" }, 0);
                                Console.ReadKey(true);
                            }
                            else
                            {
                                DrawInput(new string[] { "//Confirm Cost of Skill: " + costs[selector] }, 0);
                                do
                                {
                                    switch (Console.ReadKey(true).Key)
                                    {
                                        case ConsoleKey.F:
                                        case ConsoleKey.NumPad5:
                                            looped = false;
                                            loop = false;
                                            break;
                                        case ConsoleKey.Escape:
                                        case ConsoleKey.Subtract:
                                            looped = false;
                                            break;
                                    }
                                } while (looped);
                            }
                        }
                        break;
                }
            } while (loop);
            skillPoints -= skillCost[selector];
            thisSkill.Cost = Convert.ToInt32(costs[selector]);
            thisSkill1.Cost = Convert.ToInt32(costs[selector]);
            thisSkill2.Cost = Convert.ToInt32(costs[selector]);
            thisSkill3.Cost = Convert.ToInt32(costs[selector]);
            SceneClear();
            #endregion

            #region Primary Effect / (DamageType)
            selector = 0;
            loop = true;
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.SetCursorPosition(7, 8); Console.Write("//Skill Creator");
            Console.ForegroundColor = ConsoleColor.White; Console.Write(": ");
            Console.ForegroundColor = ConsoleColor.Green; Console.Write("{0}", skillPoints);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" skill points remaining\t\t{0} Max: ", ResourceAttribute);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("{0}", FindResource());
            Console.SetCursorPosition(7, 9); Console.Write("//"); Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("{0}", selector == 0 ? "Offensive" : "Supportive"); Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" Skill Being Created with a cost of "); Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(thisSkill.Cost); Console.ForegroundColor = ConsoleColor.White; Console.Write(" {0}", ResourceAttribute);
            Console.SetCursorPosition(7, 10); Console.Write("Select the primary effect of this skill");
            string[] dmgType = thisSkill.Type == SkillType.Offense ?
                new string[] { "//Single Hit", "//Double Hit", "//Triple Hit", "//Quadruple Hit", "//Custom " + PrimaryAttribute } :
                        new string[] { "//Boost", "//Heal", "//Shield", "//Cleanse", "//Custom " + PrimaryAttribute };
            string cm = "//Confirm ";
            string[] confirm = thisSkill.Type == SkillType.Offense ?
                new string[] { cm + "Single Hit", cm + "Double Hit", cm + "Triple Hit", cm + "Quadruple Hit", cm + "Custom " + PrimaryAttribute } :
                new string[] { cm + "Boost", cm + "Heal", cm + "Shield", cm + "Cleanse", cm + "Custom " + PrimaryAttribute };
            do
            {
                DrawInput(dmgType, selector);
                Console.SetCursorPosition(7, 11);
                Console.Write("                                                                                           ");
                Console.ForegroundColor = ConsoleColor.White; Console.SetCursorPosition(7, 11);
                Console.Write("{0}: costs ",
                    selector == 0 ? thisSkill.Type == SkillType.Offense ? "Hits enemy one time for damage" : "Boosts combat effectiveness" :
                    selector == 1 ? thisSkill.Type == SkillType.Offense ? "Hits enemy two times for damage" : "Heals missing life" :
                    selector == 2 ? thisSkill.Type == SkillType.Offense ? "Hits enemy three times for damage" : "Shields incoming damage" :
                    selector == 3 ? thisSkill.Type == SkillType.Offense ? "Hits enemy four times for damage" : "Cleanses negative statuses" :
                    WriteCustom(0, thisSkill.Type));
                Console.ForegroundColor = selector == 0 ? ConsoleColor.Green :
                    selector == 1 ? skillPoints >= 3 ? ConsoleColor.Green : ConsoleColor.Red :
                    selector == 2 ? skillPoints >= 6 ? ConsoleColor.Green : ConsoleColor.Red :
                    selector == 3 ? skillPoints >= 10 ? ConsoleColor.Green : ConsoleColor.Red :
                    skillPoints >= 15 ? ConsoleColor.Green : ConsoleColor.Red; Console.Write("{0}",
                    selector == 0 ? "1" :
                    selector == 1 ? "3" :
                    selector == 2 ? "6" :
                    selector == 3 ? "9" : "500");
                Console.ForegroundColor = ConsoleColor.White; Console.Write(" skill points");
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.D:
                    case ConsoleKey.NumPad6:
                    case ConsoleKey.Tab:
                    case ConsoleKey.Add:
                        selector += selector != 4 ? 1 : -4;
                        break;
                    case ConsoleKey.A:
                    case ConsoleKey.NumPad4:
                        selector -= selector != 0 ? 1 : -4;
                        break;
                    case ConsoleKey.F:
                    case ConsoleKey.NumPad5:
                        bool sufSP = selector == 0 ? true : //Error in player's favor
                            selector == 1 ? skillPoints >= 3 ? true : false :
                            selector == 2 ? skillPoints >= 6 ? true : false :
                            selector == 3 ? skillPoints >= 9 ? true : false :
                            skillPoints >= 500 ? true : false;
                        if (sufSP)
                        {
                            DrawInput(new string[] { confirm[selector] }, 0);
                            bool looped = true;
                            do
                            {
                                switch (Console.ReadKey(true).Key)
                                {
                                    case ConsoleKey.F:
                                    case ConsoleKey.NumPad5:
                                        looped = false;
                                        loop = false;
                                        break;
                                    case ConsoleKey.Escape:
                                    case ConsoleKey.Subtract:
                                        looped = false;
                                        break;
                                }
                            } while (looped);
                        }
                        else
                        {
                            DrawInput(new string[] { "//Insufficient Skill Points", "red" }, 0);
                            Console.ReadKey(true);
                        }
                        break;
                }
            } while (loop);
            string description = selector == 0 ? thisSkill.Type == SkillType.Offense ? "Deals damage once." : "Boosts " :
                selector == 1 ? thisSkill.Type == SkillType.Offense ? "Deals damage two times." : "Heals missing life." :
                selector == 2 ? thisSkill.Type == SkillType.Offense ? "Deals damage three times." : "Shields incoming damage." :
                selector == 3 ? thisSkill.Type == SkillType.Offense ? "Deals damage four times." : "Cleanses negative statuses." :
                thisSkill.Type == SkillType.Offense ? "" : ""; //put a string variable in here instead, set that = custom skill descrip
            DamageType damageType = selector == 0 ? thisSkill.Type == SkillType.Offense ? DamageType.Single : DamageType.Boost :
                selector == 1 ? thisSkill.Type == SkillType.Offense ? DamageType.Double : DamageType.Heal :
                selector == 2 ? thisSkill.Type == SkillType.Offense ? DamageType.Triple : DamageType.Shield :
                selector == 3 ? thisSkill.Type == SkillType.Offense ? DamageType.Quadruple : DamageType.Cleanse : DamageType.Custom;
            thisSkill.DamageType = damageType;
            thisSkill1.DamageType = damageType;
            thisSkill2.DamageType = damageType;
            thisSkill3.DamageType = damageType;
            int cost = selector == 0 ? 1 : selector == 1 ? 3 : selector == 2 ? 6 : selector == 3 ? 10 : selector == 4 ? 2 : 15;
            skillPoints -= cost;
            string boostString = "";
            if (thisSkill.DamageType == DamageType.Boost)
            {
                selector = 0;
                loop = true;
                int boostStat = 0;
                //Will need to select the type of boost here.
                //Attributes - Free
                //HIT%/RES%/DOD% - 3 SP
                //Damage/Shield/Heal - 5 SP
                Console.SetCursorPosition(7, 12); Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Select what you want to boost with this skill.  Skill Point Cost: ");
                do
                {
                    Console.SetCursorPosition(73, 12); Console.ForegroundColor =
                        selector > 5 & selector < 9 ? skillPoints >= 3 ? ConsoleColor.Green : ConsoleColor.Red :
                        selector > 8 ? skillPoints >= 5 ? ConsoleColor.Green : ConsoleColor.Red : ConsoleColor.Green;
                    Console.Write("{0}",
                        selector < 6 ? "Free" : selector < 9 ? "3   " : "5   ");
                    string[] boosts = new string[] { "//Strength", "//Defense", "//Intelligence", "//Wisdom", "//Dexterity", "//Agility",
                    "//Hit Chance", "//Resist Chance", "//Dodge Chance", "//Damage", "//Shield", "//Heal"};
                    DrawInput(boosts, selector);
                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.A:
                        case ConsoleKey.NumPad4:
                            selector -= selector != 0 ? 1 : -11;
                            break;
                        case ConsoleKey.D:
                        case ConsoleKey.NumPad6:
                        case ConsoleKey.Tab:
                        case ConsoleKey.Add:
                            selector += selector != 11 ? 1 : -11;
                            break;
                        case ConsoleKey.F:
                        case ConsoleKey.NumPad5:
                            loop = false;
                            switch (selector)
                            {
                                case 0:
                                    //str
                                    boostStat = 0;
                                    boostString = " (STR)";
                                    description = "STR for ";
                                    break;
                                case 1:
                                    //def
                                    boostStat = 1;
                                    boostString = " (DEF)";
                                    description = "DEF for ";
                                    break;
                                case 2:
                                    //int
                                    boostStat = 2;
                                    boostString = " (INT)";
                                    description = "INT for ";
                                    break;
                                case 3:
                                    //wis
                                    boostStat = 3;
                                    boostString = " (WIS)";
                                    description = "WIS for ";
                                    break;
                                case 4:
                                    //dex
                                    boostStat = 4;
                                    boostString = " (DEX)";
                                    description = "DEX for ";
                                    break;
                                case 5:
                                    //agi
                                    boostStat = 5;
                                    boostString = " (AGI)";
                                    description = "AGI for ";
                                    break;
                                case 6:
                                    //hit%
                                    if (skillPoints >= 3)
                                    {
                                        boostStat = 8;
                                        skillPoints -= 3;
                                        boostString = " (HIT%)";
                                        description = "HIT% for ";
                                    }
                                    else
                                    {
                                        DrawInput(new string[] { "//Insufficient Skill Points", "red" }, 0);
                                        loop = true;
                                        PressEnter();
                                    }
                                    break;
                                case 7:
                                    //res%
                                    if (skillPoints >= 3)
                                    {
                                        boostStat = 9;
                                        skillPoints -= 3;
                                        boostString = " (RES%)";
                                        description = "RES% for ";
                                    }
                                    else
                                    {
                                        DrawInput(new string[] { "//Insufficient Skill Points", "red" }, 0);
                                        loop = true;
                                        PressEnter();
                                    }
                                    break;
                                case 8:
                                    //dod%
                                    if (skillPoints >= 3)
                                    {
                                        boostStat = 10;
                                        skillPoints -= 3;
                                        boostString = " (DOD%)";
                                        description = "DOD% for ";
                                    }
                                    else
                                    {
                                        DrawInput(new string[] { "//Insufficient Skill Points", "red" }, 0);
                                        loop = true;
                                        PressEnter();
                                    }
                                    break;
                                case 9:
                                    //dmg
                                    if (skillPoints >= 5)
                                    {
                                        boostStat = 6;
                                        skillPoints -= 5;
                                        boostString = " (DMG)";
                                        description = "DMG for ";
                                    }
                                    else
                                    {
                                        DrawInput(new string[] { "//Insufficient Skill Points", "red" }, 0);
                                        loop = true;
                                        PressEnter();
                                    }
                                    break;
                                case 10:
                                    //shield
                                    if (skillPoints >= 5)
                                    {
                                        boostStat = 7;
                                        skillPoints -= 5;
                                        boostString = " (+Shield)";
                                        description = "+Shield for ";
                                    }
                                    else
                                    {
                                        DrawInput(new string[] { "//Insufficient Skill Points", "red" }, 0);
                                        loop = true;
                                        PressEnter();
                                    }
                                    break;
                                case 11:
                                    //heal
                                    if (skillPoints >= 5)
                                    {
                                        boostStat = 11;
                                        skillPoints -= 5;
                                        boostString = " (+Heal)";
                                        description = "+Heal for "; //Make this "+Heal for " and add the # of turns later
                                    }
                                    else
                                    {
                                        DrawInput(new string[] { "//Insufficient Skill Points", "red" }, 0);
                                        loop = true;
                                        PressEnter();
                                    }
                                    break;
                            }
                            DrawInput(new string[] { boosts[selector] }, 0);
                            bool looped = true;
                            do
                            {
                                switch (Console.ReadKey(true).Key)
                                {
                                    case ConsoleKey.F:
                                    case ConsoleKey.NumPad5:
                                        looped = false;
                                        break;
                                    case ConsoleKey.Escape:
                                    case ConsoleKey.Subtract:
                                        loop = true;
                                        looped = false;
                                        break;
                                }
                            } while (looped);
                            break;
                    }

                } while (loop);
                loop = true;
                thisSkill.BoostDuration = 2;//Instead look at remaining skill points and calculate a boost to duration based on
                thisSkill1.BoostDuration = 2;
                thisSkill2.BoostDuration = 2;
                thisSkill3.BoostDuration = 2;
                int curve = 2;
                do
                {
                    if (skillPoints > curve)
                    {
                        skillPoints -= 3;
                        thisSkill.BoostDuration++;
                        thisSkill1.BoostDuration++;
                        thisSkill2.BoostDuration++;
                        thisSkill3.BoostDuration++;
                        curve += 2;
                    }
                    else
                    {
                        loop = false;
                    }
                } while (loop);
                description += thisSkill.BoostDuration + " turns.";
                thisSkill.BoostStat = boostStat;
                thisSkill1.BoostStat = boostStat;
                thisSkill2.BoostStat = boostStat;
                thisSkill3.BoostStat = boostStat;
            }
            thisSkill.Description = description;
            thisSkill1.Description = description;
            thisSkill2.Description = description;
            thisSkill3.Description = description;
            SceneClear();
            #endregion

            #region Finalize Core Skill
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.SetCursorPosition(7, 8); Console.Write("//Skill Creator");
            Console.ForegroundColor = ConsoleColor.White; Console.Write(": ");
            Console.ForegroundColor = ConsoleColor.Green; Console.Write("{0}", skillPoints);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" skill points remaining\t\t{0} Max: ", ResourceAttribute);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("{0}", FindResource());
            DrawInput(new string[] { "//Finalize Core Skill" }, 0);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(7, 9); Console.Write("//Enhancing skill automatically with any remaining skill points");
            cost = 0;
            loop = true;
            PressEnter();
            int damage = 0;
            string typeString = "";
            switch (thisSkill.DamageType)
            {
                case DamageType.Single:
                    typeString = "Single Hit";
                    damage = 5 + (floor * 2);
                    break;
                case DamageType.Double:
                    typeString = "Double Hit";
                    damage = 3 + (floor);
                    break;
                case DamageType.Triple:
                    typeString = "Triple Hit";
                    damage = 1 + ((floor * 2) / 3);
                    break;
                case DamageType.Quadruple:
                    typeString = "Quadruple Hit";
                    damage = 1 + (floor/2);
                    break;
                case DamageType.Heal:
                    typeString = "Heal";
                    damage = 12 + (floor * 3);
                    break;
                case DamageType.Shield:
                    typeString = "Shield";
                    damage = 12 + (floor * 3);
                    break;
                case DamageType.Boost:
                    typeString = "Boost" + boostString;
                    damage = 10 + (floor * 2);
                    break;
                case DamageType.Cleanse:
                    typeString = "Cleanse";
                    break;
                case DamageType.Custom:
                    typeString = "Custom " + PrimaryAttribute;
                    switch (thisSkill.Type)
                    {
                        case SkillType.Offense:

                            break;
                        case SkillType.Support:

                            break;
                    }
                    break;
            }
            damage += skillPoints;
            damage = thisSkill.Cost == 0 ? damage / 3 : damage;
            skillPoints = 0;
            thisSkill.Damage = damage;
            thisSkill1.Damage = damage;
            thisSkill2.Damage = damage;
            thisSkill3.Damage = damage;
            SceneClear();
            //TEMPORARY FINALIZE SKILL INPUTS
            //WILL ELABORATE INTO AN EMOTION CUSTOMIZATION
            #region Name Skill

            selector = 0;
            bool choosing = false;
            string skillName = "";
            do
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.SetCursorPosition(7, 8); Console.Write("//Skill Creator");
                Console.ForegroundColor = ConsoleColor.White; Console.Write(": ");
                Console.ForegroundColor = ConsoleColor.Green; Console.Write("{0}", skillPoints);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" skill points remaining\t\t{0} Max: ", ResourceAttribute);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("{0}", FindResource());
                Console.SetCursorPosition(7, 9); Console.Write("//"); Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("{0}", selector == 0 ? "Offensive" : "Supportive"); Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" Skill Being Created with a cost of "); Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(thisSkill.Cost); Console.ForegroundColor = ConsoleColor.White; Console.Write(" {0} and", ResourceAttribute);
                Console.SetCursorPosition(7, 10); Console.Write("a primary effect of ");
                Console.ForegroundColor = ConsoleColor.Green; Console.Write(typeString);
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(7, 11); Console.Write("Name your new skill and press ENTER: ");
                Console.SetCursorPosition(7, 12); Console.Write("                           ");
                Console.SetCursorPosition(7, 13); Console.Write("                           ");
                DrawInput(new string[] { "//Type Name of Skill" }, 0);
                skillName = SkillName();
                choosing = Confirm(skillName, 7, 12);
            } while (!choosing);
            #endregion
            thisSkill.Name = skillName;
            thisSkill1.Name = skillName;
            thisSkill2.Name = skillName;
            thisSkill3.Name = skillName;
            SkillClass skillClass = PrimaryAttribute == Attributes.Strength ? SkillClass.Strength :
                PrimaryAttribute == Attributes.Defense ? SkillClass.Defense :
                PrimaryAttribute == Attributes.Intelligence ? SkillClass.Intelligence :
                PrimaryAttribute == Attributes.Wisdom ? SkillClass.Wisdom :
                PrimaryAttribute == Attributes.Dexterity ? SkillClass.Dexterity : SkillClass.Agility;
            thisSkill.SkillClass = skillClass;
            thisSkill1.SkillClass = skillClass;
            thisSkill2.SkillClass = skillClass;
            thisSkill3.SkillClass = skillClass;
            //
            Emotion emotion = Primary;
            int where = emotion == Emotion.Anger ? 0 : emotion == Emotion.Anticipation ? 1 : emotion == Emotion.Disgust ? 2 :
                emotion == Emotion.Fear ? 3 : emotion == Emotion.Joy ? 4 : emotion == Emotion.Sadness ? 5 :
                emotion == Emotion.Surprise ? 6 : 7;
            int duration = Rapport[where] <= 2 ? 2 : Rapport[where] <= 4 ? 3 : Rapport[where] <= 9 ? 4 : 5;
            skills[0, skills.GetLength(1) - 1] = thisSkill;
            thisSkill1.Emotion = Primary;
            thisSkill1.GiveStatus(70 + (duration * 4), duration);
            skills[1, skills.GetLength(1) - 1] = thisSkill1;
            //
            emotion = Secondary;
            where = emotion == Emotion.Anger ? 0 : emotion == Emotion.Anticipation ? 1 : emotion == Emotion.Disgust ? 2 :
                emotion == Emotion.Fear ? 3 : emotion == Emotion.Joy ? 4 : emotion == Emotion.Sadness ? 5 :
                emotion == Emotion.Surprise ? 6 : 7;
            duration = Rapport[where] <= 2 ? 2 : Rapport[where] <= 4 ? 3 : Rapport[where] <= 9 ? 4 : 5;
            thisSkill2.Emotion = Secondary;
            thisSkill2.GiveStatus(70 + (duration * 4), duration);
            skills[2, skills.GetLength(1) - 1] = thisSkill2;
            //
            emotion = Tertiary;
            where = emotion == Emotion.Anger ? 0 : emotion == Emotion.Anticipation ? 1 : emotion == Emotion.Disgust ? 2 :
                emotion == Emotion.Fear ? 3 : emotion == Emotion.Joy ? 4 : emotion == Emotion.Sadness ? 5 :
                emotion == Emotion.Surprise ? 6 : 7;
            duration = Rapport[where] <= 2 ? 2 : Rapport[where] <= 4 ? 3 : Rapport[where] <= 9 ? 4 : 5;
            thisSkill3.Emotion = Tertiary;
            thisSkill3.GiveStatus(70 + (duration * 4), duration);
            skills[3, skills.GetLength(1) - 1] = thisSkill3;
            Skills = skills;
            #endregion
            SceneClear();
            //This needs to lead a player through a skill creation method.
        }

        public static string SkillName()
        {
            string skillName = "";
            ConsoleKey userInput;
            bool naming = true;
            int capitalize = 0;
            do
            {
                switch (userInput = Console.ReadKey(true).Key)
                {
                    case ConsoleKey.Enter:
                        naming = false;
                        capitalize = 0;
                        break;
                    case ConsoleKey.A:
                    case ConsoleKey.B:
                    case ConsoleKey.C:
                    case ConsoleKey.D:
                    case ConsoleKey.E:
                    case ConsoleKey.F:
                    case ConsoleKey.G:
                    case ConsoleKey.H:
                    case ConsoleKey.I:
                    case ConsoleKey.J:
                    case ConsoleKey.K:
                    case ConsoleKey.L:
                    case ConsoleKey.M:
                    case ConsoleKey.N:
                    case ConsoleKey.O:
                    case ConsoleKey.P:
                    case ConsoleKey.Q:
                    case ConsoleKey.R:
                    case ConsoleKey.S:
                    case ConsoleKey.T:
                    case ConsoleKey.U:
                    case ConsoleKey.V:
                    case ConsoleKey.W:
                    case ConsoleKey.X:
                    case ConsoleKey.Y:
                    case ConsoleKey.Z:
                    case ConsoleKey.D1:
                    case ConsoleKey.D2:
                    case ConsoleKey.D3:
                    case ConsoleKey.D4:
                    case ConsoleKey.D5:
                    case ConsoleKey.D6:
                    case ConsoleKey.D7:
                    case ConsoleKey.D8:
                    case ConsoleKey.D9:
                    case ConsoleKey.D0:
                        skillName += skillName.Length != 15 ? capitalize == 0 ? userInput.ToString() : userInput.ToString().ToLower() : "";
                        capitalize += capitalize == 0 ? 1 : 0;
                        DrawInput(new string[] { "//" + skillName, "white" }, 0);
                        break;
                    case ConsoleKey.Spacebar:
                        skillName += skillName.Length != 15 ? " " : "";
                        capitalize = 0;
                        DrawInput(new string[] { "//" + skillName, "white" }, 0);
                        break;
                    default:
                        break;
                }
                if (!naming)
                {
                    naming = skillName.Length < 16 & skillName.Length > 0 ? false : true;
                    if (naming) { skillName = ""; DrawInput(new string[] { "//Invalid Skill Name Length" }, 0);
                        PressEnter(); DrawInput(new string[] { "//..." }, 0);
                    }
                }
            } while (naming);

            return skillName;
        }

        public static bool Confirm(string writeThis, int x, int y)
        {
            bool confirm = false;
            bool menuOn = true;
            Console.SetCursorPosition(x, y); Console.ForegroundColor = ConsoleColor.White;
            Console.Write("You chose: ");
            Console.ForegroundColor = ConsoleColor.Green; Console.Write(writeThis);
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(x, y + 1);
            Console.Write("Is that correct?");
            DrawInput(new string[] { "//Confirm Skill Name" }, 0);
            do
            {
                Console.SetCursorPosition(108, 1);
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.Escape:
                    case ConsoleKey.Subtract:
                        menuOn = false;
                        confirm = false;
                        break;
                    case ConsoleKey.F:
                    case ConsoleKey.NumPad5:
                        menuOn = false;
                        confirm = true;
                        break;
                }
            } while (menuOn);
            return confirm;
        }

        public string WriteCustom(int selector, SkillType skillType)
        {
            string custom = "";
            switch (PrimaryAttribute)
            {
                case Attributes.Strength:
                    custom = skillType == SkillType.Offense ? "\"Power Attack\" gets an increased Strength modifier to damage" :
                        "\"Power Stance\" boosts Strength and Hit Chance";
                    break;
                case Attributes.Defense:
                    custom = skillType == SkillType.Offense ? "\"Stun\" has a chance to stun on hit" :
                        "\"Defend\" reduces incoming damage";
                    break;
                case Attributes.Intelligence:
                    custom = skillType == SkillType.Offense ? "\"Arcane\" gets an increased Intelligence modifier to damage" :
                        "\"Psychic Strikes\" adds an Intelligence-based damage to attacks";
                    break;
                case Attributes.Wisdom:
                    custom = skillType == SkillType.Offense ? "\"Meditation\" removes statuses on hit" :
                        "\"Aura\" battle duration boosts";
                    break;
                case Attributes.Dexterity:
                    custom = skillType == SkillType.Offense ? "\"Finisher\" deals more damage to enemies with less health" :
                        "\"Preperation\" adds a Dexterity-based damage over time to attacks";
                    break;
                case Attributes.Agility:
                    custom = skillType == SkillType.Offense ? "\"Multi Attack\" does multiple attacks" :
                        "\"Reflex\" cleanses negative statuses and boosts agility";
                    break;
            }
            return custom;
        }

        public static void SceneClear()
        {
            for (int y = 7; y < 21; y++)
            {
                for (int x = 3; x < 101; x++)
                {
                    Console.SetCursorPosition(x, y);
                    Console.Write(" ");
                }
            }
        }

        public static void DrawInput(string[] responses, int selector)
        {
            Console.ForegroundColor = responses.Length > 1 ? ConsoleColor.Green : ConsoleColor.DarkGray;
            Console.SetCursorPosition(5, 24); Console.Write(" //");
            Console.SetCursorPosition(5, 25); Console.Write("{<<<<");
            Console.SetCursorPosition(5, 26); Console.Write(" \\\\");

            Console.SetCursorPosition(95, 24); Console.Write("  \\\\");
            Console.SetCursorPosition(95, 25); Console.Write(">>>>}");
            Console.SetCursorPosition(95, 26); Console.Write("  //");
            Console.ForegroundColor = responses[selector] == "" ? ConsoleColor.DarkGray : ConsoleColor.White;
            if (responses.Length == 7)
            {
                Console.ForegroundColor = responses[1] == "makeitblue" ? ConsoleColor.Blue : ConsoleColor.DarkGray;
            }
            Console.SetCursorPosition(43, 22); Console.Write("//Select Response");
            int x = 13 + ((79 - responses[selector].Length) / 2);
            Console.ForegroundColor = ConsoleColor.Green;
            if (responses.Length != 1)
            {
                Console.ForegroundColor = responses[1] == "red" ? ConsoleColor.Red :
                    responses[1] == "white" ? ConsoleColor.White : ConsoleColor.Green;
            }
            Console.SetCursorPosition(13, 25); Console.Write("                                                                               ");
            Console.SetCursorPosition(x, 25); Console.Write(responses[selector]);

        }

        public void InitializeSkills()
        {
            Skill tempSkill1 = new Skill("", SkillType.Exploration, "");
            Skill tempSkill2 = new Skill("", SkillType.Exploration, "");
            Skill tempSkill3 = new Skill("", SkillType.Exploration, "");
            switch (PrimaryAttribute) // Setting up first "Core" Skill
            {
                case Attributes.Strength:
                    Skills[0, 0] = new Skill("Bash", SkillType.Offense, SkillClass.Strength, Emotion.Null,
                        Emotion.Null, 5, 10, DamageType.Single, StatusEffect.None, 0,
                        "Deal minor damage.");//Damage
                    tempSkill1 = new Skill("Bash", SkillType.Offense, SkillClass.Strength, Emotion.Null,
                        Emotion.Null, 5, 10, DamageType.Single, StatusEffect.None, 0,
                        "Deal minor damage.");
                    tempSkill2 = new Skill("Bash", SkillType.Offense, SkillClass.Strength, Emotion.Null,
                        Emotion.Null, 5, 10, DamageType.Single, StatusEffect.None, 0,
                        "Deal minor damage.");
                    tempSkill3 = new Skill("Bash", SkillType.Offense, SkillClass.Strength, Emotion.Null,
                        Emotion.Null, 5, 10, DamageType.Single, StatusEffect.None, 0,
                        "Deal minor damage.");
                    break;
                case Attributes.Defense:
                    Skills[0, 0] = new Skill("Block", SkillType.Support, SkillClass.Defense, Emotion.Null,
                        Emotion.Null, 5, 10, DamageType.Shield, StatusEffect.None, 0,
                        "Shield minor damage.");//Shield
                    tempSkill1 = new Skill("Block", SkillType.Support, SkillClass.Defense, Emotion.Null,
                        Emotion.Null, 5, 10, DamageType.Shield, StatusEffect.None, 0,
                        "Shield minor damage.");
                    tempSkill2 = new Skill("Block", SkillType.Support, SkillClass.Defense, Emotion.Null,
                        Emotion.Null, 5, 10, DamageType.Shield, StatusEffect.None, 0,
                        "Shield minor damage.");
                    tempSkill3 = new Skill("Block", SkillType.Support, SkillClass.Defense, Emotion.Null,
                        Emotion.Null, 5, 10, DamageType.Shield, StatusEffect.None, 0,
                        "Shield minor damage.");
                    break;
                case Attributes.Intelligence:
                    Skills[0, 0] = new Skill("Burn", SkillType.Offense, SkillClass.Intelligence, Emotion.Null,
                        Emotion.Null, 1, 12, DamageType.Single, StatusEffect.None, 0,
                        "Deal minor damage.");//Damage
                    tempSkill1 = new Skill("Burn", SkillType.Offense, SkillClass.Intelligence, Emotion.Null,
                        Emotion.Null, 1, 12, DamageType.Single, StatusEffect.None, 0,
                        "Deal minor damage.");
                    tempSkill2 = new Skill("Burn", SkillType.Offense, SkillClass.Intelligence, Emotion.Null,
                        Emotion.Null, 1, 12, DamageType.Single, StatusEffect.None, 0,
                        "Deal minor damage.");
                    tempSkill3 = new Skill("Burn", SkillType.Offense, SkillClass.Intelligence, Emotion.Null,
                        Emotion.Null, 1, 12, DamageType.Single, StatusEffect.None, 0,
                        "Deal minor damage.");
                    break;
                case Attributes.Wisdom:
                    Skills[0, 0] = new Skill("Bright", SkillType.Support, SkillClass.Wisdom, Emotion.Null,
                        Emotion.Null, 1, 12, DamageType.Heal, StatusEffect.None, 0,
                        "Heal minor damage.");//Heal
                    tempSkill1 = new Skill("Bright", SkillType.Support, SkillClass.Wisdom, Emotion.Null,
                        Emotion.Null, 1, 12, DamageType.Heal, StatusEffect.None, 0,
                        "Heal minor damage.");
                    tempSkill2 = new Skill("Bright", SkillType.Support, SkillClass.Wisdom, Emotion.Null,
                        Emotion.Null, 1, 12, DamageType.Heal, StatusEffect.None, 0,
                        "Heal minor damage.");
                    tempSkill3 = new Skill("Bright", SkillType.Support, SkillClass.Wisdom, Emotion.Null,
                        Emotion.Null, 1, 12, DamageType.Heal, StatusEffect.None, 0,
                        "Heal minor damage.");
                    break;
                case Attributes.Dexterity:
                    Skills[0, 0] = new Skill("Break", SkillType.Offense, SkillClass.Dexterity, Emotion.Null,
                        Emotion.Null, 3, 5, DamageType.Double, StatusEffect.None, 0,
                        "Deal minor damage x2.");//Damage Double
                    tempSkill1 = new Skill("Break", SkillType.Offense, SkillClass.Dexterity, Emotion.Null,
                        Emotion.Null, 3, 5, DamageType.Double, StatusEffect.None, 0,
                        "Deal minor damage x2.");
                    tempSkill2 = new Skill("Break", SkillType.Offense, SkillClass.Dexterity, Emotion.Null,
                        Emotion.Null, 3, 5, DamageType.Double, StatusEffect.None, 0,
                        "Deal minor damage x2.");
                    tempSkill3 = new Skill("Break", SkillType.Offense, SkillClass.Dexterity, Emotion.Null,
                        Emotion.Null, 3, 5, DamageType.Double, StatusEffect.None, 0,
                        "Deal minor damage x2.");
                    break;
                case Attributes.Agility:
                    Skills[0, 0] = new Skill("Bend", SkillType.Support, SkillClass.Agility, Emotion.Null,
                        Emotion.Null, 3, 10, DamageType.Boost, StatusEffect.None, 0,
                        "Minor Agility boost, short duration.");//Increased Agility
                    Skills[0, 0].BoostDuration = 2;
                    Skills[0, 0].BoostStat = 20;
                    tempSkill1 = new Skill("Bend", SkillType.Support, SkillClass.Agility, Emotion.Null,
                        Emotion.Null, 3, 10, DamageType.Boost, StatusEffect.None, 0,
                        "Minor Agility boost, short duration.");//Increased Agility
                    tempSkill1.BoostDuration = 2;
                    tempSkill1.BoostStat = 10;
                    tempSkill2 = new Skill("Bend", SkillType.Support, SkillClass.Agility, Emotion.Null,
                        Emotion.Null, 3, 10, DamageType.Boost, StatusEffect.None, 0,
                        "Minor Agility boost, short duration.");//Increased Agility
                    tempSkill2.BoostDuration = 2;
                    tempSkill2.BoostStat = 10;
                    tempSkill3 = new Skill("Bend", SkillType.Support, SkillClass.Agility, Emotion.Null,
                        Emotion.Null, 3, 10, DamageType.Boost, StatusEffect.None, 0,
                        "Minor Agility boost, short duration.");//Increased Agility
                    tempSkill3.BoostDuration = 2;
                    tempSkill3.BoostStat = 10;
                    break;
                default:
                    break;
            }
            //Initialize First "Primary" Skill
            tempSkill1.Emotion = Primary;
            tempSkill1.EmotionSwitch = Primary;
            tempSkill1.GiveStatus(70, 2);
            Skills[1, 0] = tempSkill1;
            //Initialize First "Secondary" Skill
            tempSkill2.Emotion = Secondary;
            tempSkill2.EmotionSwitch = Secondary;
            tempSkill2.GiveStatus(70, 2);
            Skills[2, 0] = tempSkill2;
            //Initialize First "Tertiary" Skill
            tempSkill3.Emotion = Tertiary;
            tempSkill3.EmotionSwitch = Tertiary;
            tempSkill3.GiveStatus(70, 2);
            Skills[3, 0] = tempSkill3;
        }

        public override void GetShield(int amount)
        {
            int[] mods = EqAttrMod();
            int wis = Wisdom + mods[3];

            amount += Equipment.EquippedOffHand.OHType == OffHandType.ArmGuard ? Equipment.EquippedOffHand.OHBonus + (wis / 10) :
                wis / 10;
            amount += Boosts[7, 2] != 0 ? Boosts[7, 1] : 0;
            Shield = amount;
        }

        public int MinDamage()
        {
            //Look at equipped weapon min damage and modify it by strength
            int minDamage = Equipment.EquippedWeapon.MinDamage;
            if (Equipment.EquippedOffHand.OHType == OffHandType.PowerFist)
            {
                minDamage += Equipment.EquippedOffHand.OHBonus;
            }
            int[] attrMods = EqAttrMod();
            int[] statusMod = StatusDoes();
            statusMod[0] += PrimaryAttribute == Attributes.Strength ? statusMod[6] : 0;
            minDamage += Boosts[0, 2] != 0 ? (Strength + attrMods[0] + Boosts[0, 1] + statusMod[0]) / 5 : (Strength + attrMods[0] + statusMod[0]) / 5;
            return minDamage;
        }

        public int MaxDamage()
        {
            int maxDamage = Equipment.EquippedWeapon.MaxDamage;
            if (Equipment.EquippedOffHand.OHType == OffHandType.PowerFist)
            {
                maxDamage += Equipment.EquippedOffHand.OHBonus;
            }
            int[] attrMods = EqAttrMod();
            int[] statusMods = StatusDoes();
            statusMods[0] += PrimaryAttribute == Attributes.Strength ? statusMods[6] : 0;
            maxDamage += Boosts[0, 2] != 0 ? (Strength + attrMods[0] + Boosts[0, 1] + statusMods[0]) / 3 :
                (Strength + attrMods[0] + statusMods[0]) / 3;
            return maxDamage;
        }

        public override int CalcDamage(Character player, Character monster) //This will happen after a CalcHit
        {
            Random rand = new Random();
            int damageDealt = rand.Next(MinDamage(), MaxDamage() + 1);
            return damageDealt;
        }

        public int FindLife()
        {
            int maxLife = LifeMax;
            int[,] mods = EquipmentMods();
            maxLife += PrimaryAttribute == Attributes.Strength ? ((Strength + mods[0,0]) / 2) : 
                PrimaryAttribute == Attributes.Defense ? ((Defense + mods[0,1]) / 2) : 
                PrimaryAttribute == Attributes.Intelligence ? ((Intelligence + mods[0,2]) / 2) : 
                PrimaryAttribute == Attributes.Wisdom ? ((Wisdom + mods[0,3]) / 2) : 
                PrimaryAttribute == Attributes.Dexterity ? ((Dexterity + mods[0,4]) / 2) :
                PrimaryAttribute == Attributes.Agility ? ((Agility + mods[0,5]) / 2) : 0;
            maxLife += Equipment.EquippedAccessory.MaxLifeMod;
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

        public int FindResource()
        {
            int maxResource = ResourceMax + Equipment.EquippedHead.Attributes[6] + 
                Equipment.EquippedAmulet.Attributes[6] + Equipment.EquippedOutfit.Attributes[6] + 
                Equipment.EquippedAccessory.Attributes[6] + Equipment.EquippedWeapon.Attributes[6] + 
                Equipment.EquippedOffHand.Attributes[6] + Equipment.EquippedAmulet.ResourceMod + 
                Equipment.EquippedOffHand.OHResourceBonus;
            return maxResource;

        }

        public void ResourceColor()
        {
            if (Resource >= ((FindResource() * 9) / 10)) { Console.ForegroundColor = ConsoleColor.Green; }
            else if (Resource > (2 * (FindResource() / 3))) { Console.ForegroundColor = ConsoleColor.Yellow; }
            else if (Resource > FindResource() / 3) { Console.ForegroundColor = ConsoleColor.DarkYellow; }
            else if (Resource > FindResource() / 10) { Console.ForegroundColor = ConsoleColor.Red; }
            else { Console.ForegroundColor = ConsoleColor.DarkRed; }
        }

        public ConsoleColor EmotionColor(Emotion current)
        {
            ConsoleColor color = current == Emotion.Fear ? ConsoleColor.DarkGray : current == Emotion.Trust ? ConsoleColor.Blue :
                current == Emotion.Joy ? ConsoleColor.Yellow : current == Emotion.Anticipation ? ConsoleColor.DarkGreen :
                current == Emotion.Anger ? ConsoleColor.DarkRed : current == Emotion.Disgust ? ConsoleColor.Magenta :
                current == Emotion.Sadness ? ConsoleColor.DarkYellow : current == Emotion.Surprise ? ConsoleColor.Cyan : ConsoleColor.Gray;
            return color;
        }

        public int GetIndex(string what)
        {
            int index = -1;
            what = what.ToUpper();
            if (what == "CORE")
            {
                switch (PrimaryAttribute)
                {
                    case Attributes.Strength:
                        index = 0;
                        break;
                    case Attributes.Defense:
                        index = 1;
                        break;
                    case Attributes.Intelligence:
                        index = 2;
                        break;
                    case Attributes.Wisdom:
                        index = 3;
                        break;
                    case Attributes.Dexterity:
                        index = 4;
                        break;
                    case Attributes.Agility:
                        index = 5;
                        break;
                }
            }
            return index;
        }

        public string GetShort(int index)
        {
            string shortAttr =
                index == 0 ? "STR" :
                index == 1 ? "DEF" :
                index == 2 ? "INT" :
                index == 3 ? "WIS" :
                index == 4 ? "DEX" :
                index == 5 ? "AGI" :
                PrimaryAttribute == Attributes.Strength || PrimaryAttribute == Attributes.Defense ? "STAM" :
                PrimaryAttribute == Attributes.Intelligence || PrimaryAttribute == Attributes.Wisdom ? "KNOW" : "INTU";

            return shortAttr;
        }

        public void Write(Equipment what, int how)
        {
            string write = "";
            List<string> longWriteA = new List<string> { };//longWriteA in white
            List<string> longWriteB = new List<string> { };//longWriteB in red or green + "   "
            Equipment equipment = new Equipment();
            int core = GetIndex("core");
            int index = 0;
            if (what.Name != "")
            {
                switch (what.Slot)
                {
                    case Slot.Head:
                        #region HEAD
                        Head tempHead = Equipment.EquippedHead;
                        equipment = tempHead;
                        write = tempHead.Name;
                        if (tempHead.PrimaryMod != 0)
                        {
                            longWriteA.Add(GetShort(core) + ": ");
                            longWriteB.Add((tempHead.PrimaryMod + tempHead.Attributes[core]).ToString());
                        }
                        foreach (int attr in tempHead.Attributes)
                        {
                            if (attr != 0)
                            {
                                if (index != core)
                                {
                                    longWriteA.Add(GetShort(index) + ": ");
                                    longWriteB.Add(tempHead.Attributes[index].ToString());
                                }
                                else if (tempHead.PrimaryMod == 0)
                                {
                                    longWriteA.Add(GetShort(index) + ": ");
                                    longWriteB.Add(tempHead.Attributes[index].ToString());
                                }
                            }
                            index++;
                        }
                        #endregion
                        break;
                    case Slot.MainHand:
                        #region WEAPON
                        Weapon tempWeapon = Equipment.EquippedWeapon;
                        equipment = tempWeapon;
                        write = tempWeapon.Name;
                        longWriteA.Add("DMG: ");
                        longWriteB.Add(tempWeapon.MinDamage.ToString());
                        longWriteA.Add("-");
                        longWriteB.Add(tempWeapon.MaxDamage.ToString());
                        if (tempWeapon.HitChanceBonus != 0)
                        {
                            longWriteA.Add("HIT%: ");
                            longWriteB.Add(tempWeapon.HitChanceBonus.ToString());
                        }
                        foreach (int attr in tempWeapon.Attributes)
                        {
                            if (attr != 0)
                            {
                                longWriteA.Add(GetShort(index) + ": ");
                                longWriteB.Add(tempWeapon.Attributes[index].ToString());
                            }
                            index++;
                        }
                        #endregion
                        break;
                    case Slot.OffHand:
                        #region OFFHAND
                        OffHand tempOffHand = Equipment.EquippedOffHand;
                        equipment = tempOffHand;
                        write = tempOffHand.Name;
                        if (tempOffHand.OHBonus != 0)
                        {
                            string ohBonus = tempOffHand.OHType == OffHandType.ArmGuard ? "+Shield: " :
                                tempOffHand.OHType == OffHandType.Lasso ? "HIT%: " :
                                tempOffHand.OHType == OffHandType.Orb ? "RES%: " :
                                tempOffHand.OHType == OffHandType.Powder ? "DOD%: " :
                                tempOffHand.OHType == OffHandType.PowerFist ? "+DMG: " :
                                tempOffHand.OHType == OffHandType.Tome ? "+sDMG: " : "Oopsie Poopsie";
                            longWriteA.Add(ohBonus);
                            longWriteB.Add(tempOffHand.OHBonus.ToString());
                        }
                        if (tempOffHand.OHPrimaryBonus != 0)
                        {
                            longWriteA.Add(GetShort(core) + ": ");
                            longWriteB.Add((tempOffHand.OHPrimaryBonus + tempOffHand.Attributes[core]).ToString());
                        }
                        if (tempOffHand.OHResourceBonus != 0)
                        {
                            longWriteA.Add(ResourceAttribute + ": ");
                            longWriteB.Add((tempOffHand.OHResourceBonus + tempOffHand.Attributes[6]).ToString());
                        }
                        foreach (int attr in tempOffHand.Attributes)
                        {
                            if (attr != 0)
                            {
                                if (core != index & index != 6)
                                {
                                    longWriteA.Add(GetShort(index) + ": ");
                                    longWriteB.Add(tempOffHand.Attributes[index].ToString());
                                }
                                else if (core == index & tempOffHand.OHPrimaryBonus == 0)
                                {
                                    longWriteA.Add(GetShort(index) + ": ");
                                    longWriteB.Add((tempOffHand.Attributes[index] + tempOffHand.OHPrimaryBonus).ToString());
                                }
                                else if (tempOffHand.OHResourceBonus == 0)
                                {
                                    longWriteA.Add(GetShort(index) + ": ");
                                    longWriteB.Add((tempOffHand.Attributes[index] + tempOffHand.OHResourceBonus).ToString());
                                }

                            }
                            index++;
                        }
                        #endregion
                        break;
                    case Slot.Amulet:
                        #region AMULET
                        Amulet tempAmulet = Equipment.EquippedAmulet;
                        equipment = tempAmulet;
                        write = tempAmulet.Name;
                        if (tempAmulet.ResourceMod != 0)
                        {
                            longWriteA.Add(ResourceAttribute + ": ");
                            longWriteB.Add((tempAmulet.ResourceMod + tempAmulet.Attributes[6]).ToString());
                        }
                        foreach (int attr in tempAmulet.Attributes)
                        {
                            if (attr != 0)
                            {
                                longWriteA.Add(GetShort(index) + ": ");
                                if (index != 6)
                                {
                                    longWriteB.Add(tempAmulet.Attributes[index].ToString());
                                }
                                else if (tempAmulet.ResourceMod == 0)
                                {
                                    longWriteB.Add((tempAmulet.Attributes[index] + tempAmulet.ResourceMod).ToString());
                                }

                            }
                            index++;
                        }
                        #endregion
                        break;
                    case Slot.Outfit:
                        #region OUTFIT
                        Outfit tempOutfit = Equipment.EquippedOutfit;
                        equipment = tempOutfit;
                        write = tempOutfit.Name;
                        foreach (int chance in tempOutfit.Chances)
                        {
                            if (chance != 0)
                            {
                                string chances = index == 0 ? "HIT%: " : index == 2 ? "RES%: " : index == 3 ? "DOD%: " : "Oopsie Poopsie";
                                longWriteA.Add(chances);
                                longWriteB.Add(tempOutfit.Chances[index].ToString());
                            }
                            index++;
                        }
                        index = 0;
                        foreach (int attr in tempOutfit.Attributes)
                        {
                            if (attr != 0)
                            {
                                longWriteA.Add(GetShort(index) + ": ");
                                longWriteB.Add(tempOutfit.Attributes[index].ToString());
                            }
                            index++;
                        }
                        #endregion
                        break;
                    case Slot.Accessory:
                        #region ACCESSORY
                        Accessory tempAccessory = Equipment.EquippedAccessory;
                        equipment = tempAccessory;
                        write = tempAccessory.Name;
                        if (tempAccessory.MaxLifeMod != 0)
                        {
                            longWriteA.Add("+Life: ");
                            longWriteB.Add(tempAccessory.MaxLifeMod.ToString());
                        }
                        foreach (int attr in tempAccessory.Attributes)
                        {
                            if (attr != 0)
                            {
                                longWriteA.Add(GetShort(index) + ": ");
                                if (index != 6)
                                {
                                    longWriteB.Add(tempAccessory.Attributes[index] + "   ");
                                }
                                else
                                {
                                    longWriteB.Add((tempAccessory.Attributes[index] + tempAccessory.MaxResourceMod).ToString());
                                }

                            }
                            index++;
                        }
                        #endregion
                        break;
                    default:
                        write = "Oopsie poosie!";
                        break;
                }
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("{0}", how == 0 ? "" : "//");
            Console.ForegroundColor = write != "" ? ConsoleColor.DarkCyan : ConsoleColor.DarkGray;
            write = write != "" ? write : "Not Equipped";
            Console.Write("{0}", how == 0 ? write : equipment.Name != "" ? equipment.Name + ": " : what.Slot == Slot.OffHand ? "Off Hand: Not Equipped" :
                what.Slot + ": Not Equipped");
            Console.ForegroundColor = ConsoleColor.White;
            if (how == 1)
            {
                index = 0;
                for (int count = 0; count < longWriteA.Count; count++)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(longWriteA.ElementAt(index));
                    Console.ForegroundColor = Convert.ToInt32(longWriteB.ElementAt(index)) > 0 ? ConsoleColor.Green : ConsoleColor.Red;
                    Console.Write(longWriteB.ElementAt(index) + "{0}", longWriteA.ElementAt(index) == "DMG: " ? "" : "   ");
                    index++;
                }
            }
        }
        //Combat Methods below
        public bool DoesStatus(Skill thisSkill, Player player, Monster monster)
        {
            if (thisSkill.Status != StatusEffect.None)
            {
                //Check StatusChance
                //Check Monster Resist Chance
                int chance = thisSkill.StatusChance;
                chance += (player.Intelligence + player.Boosts[2, 1] / 7);//player.Boosts[8, 1] + 
                int resist = monster.ResistChance;
                resist += monster.Boosts[9, 1] + ((monster.Wisdom + monster.Boosts[3, 1]) / 7);
                Random rand = new Random();
                if (chance - resist >= rand.Next(1, 201))
                {
                    return true;
                }
                else { return false; }
            }
            else
            {
                return false;
            }
        }
        
        public int MakeDice(Player player)
        {
            int dice = 0;
            dice = player.DiceShards / 6;
            int takeAway = (player.DiceShards % 6);
            player.DiceShards = takeAway;
            return dice;
        }
    }


}
