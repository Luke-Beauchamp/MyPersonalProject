using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CharacterLibrary;

namespace SkillLibrary
{
    public enum SkillType
    {
        Offense, //Will Target Enemy
        Support, //Will Target Self
        Social, //Will affect social situations
        Exploration //Will affect exploration of the dungeon
    }

    public enum SkillClass
    {
        Strength,
        Defense,
        Intelligence,
        Wisdom,
        Dexterity,
        Agility
    }

    public enum DamageType
    {
        Single,
        Double,
        Triple,
        Quadruple,
        Heal,
        Shield,
        Boost,
        Cleanse,
        Custom
    }

    public class Skill
    {
        //In this class I will determine he qualities of (at least the parent) Skill
        public string Name { get; set; }
        public SkillType Type { get; set; }
        //public bool IsAvailable { get; set; }
        public SkillClass SkillClass { get; set; } //Determines what kind of resource skill will use, player object tracks name of resource
        public Emotion Emotion { get; set; } //Determines if skill is has a base emotion
        public Emotion EmotionSwitch { get; set; }
        public int Cost { get; set; } //Resource cost of each skill
        public int Damage { get; set; }
        public DamageType DamageType { get; set; }
        public int BoostDuration { get; set; }
        public int BoostStat { get; set; }
        //WRITE WHAT THE BOOST STATS MEAN.......
        // 0: Strength  1: Defense  2: Intelligence  3: Wisdom  4: Dexterity  5: Agility  
        // 6: Damage  7: Shield  8: Hit Chance  9: Resist Chance  10: Dodge Chance 
        // 11: Heal  12:
        public StatusEffect Status { get; set; }
        public int StatusChance { get; set; }
        public int StatusDuration { get; set; }
        public string Description { get; set; }

        public Skill(string name, SkillType type, SkillClass skillClass, Emotion emotion, Emotion emotionSwitch, int cost, int damage, 
            DamageType damageType, StatusEffect status, int statusChance, string description)
        {
            Name = name;
            Type = type;
            SkillClass = skillClass;
            Emotion = emotion;
            EmotionSwitch = emotionSwitch;
            Cost = cost;
            Damage = damage;
            DamageType = damageType;
            Status = status;
            StatusChance = statusChance;
            Description = description;
        }

        public Skill(string name, SkillType type, string description)
        {
            Name = name;
            Type = type;
            Description = description;
        }

        public void GiveStatus(int chance, int duration)
        {
            switch (Emotion)
            {
                case Emotion.Fear:
                    Status = Type == SkillType.Offense ? StatusEffect.Tremble : StatusEffect.Tower;
                    break;
                case Emotion.Trust:
                    Status = Type == SkillType.Offense ? StatusEffect.Conflict : StatusEffect.Certain;
                    break;
                case Emotion.Joy:
                    Status = Type == SkillType.Offense ? StatusEffect.Whimsy : StatusEffect.Presence;
                    break;
                case Emotion.Anticipation:
                    Status = Type == SkillType.Offense ? StatusEffect.Fumble : StatusEffect.Nimble;
                    break;
                case Emotion.Anger:
                    Status = Type == SkillType.Offense ? StatusEffect.Stagger : StatusEffect.Rage;
                    break;
                case Emotion.Disgust:
                    Status = Type == SkillType.Offense ? StatusEffect.Fade : StatusEffect.Resilient;
                    break;
                case Emotion.Sadness:
                    Status = Type == SkillType.Offense ? StatusEffect.Sink : StatusEffect.Relish;
                    break;
                case Emotion.Surprise:
                    Status = Type == SkillType.Offense ? StatusEffect.Unsteady : StatusEffect.Ready;
                    break;
            }
            StatusDuration = duration;
            StatusChance = chance;
            Description += Type == SkillType.Offense ? chance != 100 ? " Chance to inflict " : " Inflicts " : chance != 100 ? " Chance to gain " : " Gain ";
            Description += Status + " for " + duration + " turns.";
        }

        public string[] GetSkill() //Return a string array that holds written information about the skill at its various indexes.
        {
            string[] skillInfo = new string[5];
            //Need an array to store various information about skills in text.
            //0-Name:  Current Turn: "{0} uses {1}!", player.Name, skillInfo[0]
            skillInfo[0] = Name;
            
            //1-Description:  //Select Skill- "{0}: {1}", skillInfo[0], skillInfo[1] //Must find a shorthand for skill description.
            skillInfo[1] = "Cost: " + Cost + ", " + Description;


            //2-DamageType
            
            skillInfo[2] = DamageType.ToString();
            switch (skillInfo[2])
            {
                case "Single":
                    skillInfo[2] = "one";
                    break;
                case "Double":
                    skillInfo[2] = "2";
                    break;
                case "Triple":
                    skillInfo[2] = "3";
                    break;
                case "Quadruple":
                    skillInfo[2] = "4";
                    break;
                case "Heal":
                    skillInfo[2] = "heals for";
                    break;
                case "Shield":
                    skillInfo[2] = "shields the next";
                    break;
                case "Boost":
                    switch (BoostStat)
                    {
                        case 0:
                            skillInfo[2] = "boosted STR for " + BoostDuration + " turns."; 
                            break;
                        case 1:
                            skillInfo[2] = "boosted DEF for " + BoostDuration + " turns.";
                            break;
                        case 2:
                            skillInfo[2] = "boosted INT for " + BoostDuration + " turns.";
                            break;
                        case 3:
                            skillInfo[2] = "boosted WIS for " + BoostDuration + " turns.";
                            break;
                        case 4:
                            skillInfo[2] = "boosted DEX for " + BoostDuration + " turns.";
                            break;
                        case 5:
                            skillInfo[2] = "boosted AGI for " + BoostDuration + " turns.";
                            break;
                        case 6:
                            skillInfo[2] = "boosted DMG for " + BoostDuration + " turns.";
                            break;
                        case 7:
                            skillInfo[2] = "boosted Shield for " + BoostDuration + " turns.";
                            break;
                        case 8:
                            skillInfo[2] = "boosted HIT% for " + BoostDuration + " turns.";
                            break;
                        case 9:
                            skillInfo[2] = "boosted RES% for " + BoostDuration + " turns.";
                            break;
                        case 10:
                            skillInfo[2] = "boosted DOD% for " + BoostDuration + " turns.";
                            break;
                        case 11:
                            skillInfo[2] = "boosted Heals for " + BoostDuration + " turns.";
                            break;
                    }
                    break;
                case "Custom"://Write these in when ready...
                    skillInfo[2] = SkillClass == SkillClass.Strength ? Type == SkillType.Offense ? "" : "" :
                        SkillClass == SkillClass.Defense ? Type == SkillType.Offense ? "" : "" :
                        SkillClass == SkillClass.Intelligence ? Type == SkillType.Offense ? "" : "" :
                        SkillClass == SkillClass.Wisdom ? Type == SkillType.Offense ? "" : "" :
                        SkillClass == SkillClass.Dexterity ? Type == SkillType.Offense ? "" : "" : 
                        Type == SkillType.Offense ? "" : ""; //Agility here at the end... 
                    //Might refer to a boss skill, that does things.... differently.
                    //Can at a later time put in a check on skillInfo[3] references for "custom" and IF "custom" call a Custom() that super specifies the skill
                    break;
            }

            //3-Damage: Refined meaning in DamageType
            skillInfo[3] = Damage.ToString();

            //4
            if (Status != StatusEffect.None)
            {
                switch (Type)
                {
                    case SkillType.Offense:
                        skillInfo[4] = "inflicted with " + Status.ToString() + " for " + StatusDuration + " turns.";
                        break;
                    case SkillType.Support:
                        skillInfo[4] = "gained " + Status.ToString() + " for " + StatusDuration + " turns.";
                        break;
                }
            }
            else
            {
                skillInfo[4] = "";
            }


            //5

            return skillInfo;
        }


    }
}
