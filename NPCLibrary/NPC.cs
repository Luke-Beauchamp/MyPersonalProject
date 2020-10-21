using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CharacterLibrary;
using MonsterLibrary;

namespace NPCLibrary
{
    public class NPC : Humanoid
    {
        public string[,] Dialogue { get; set; }
        public Monster Monster { get; set; }

        //In this class I will build out the NPC object, child to Character
        public NPC(Emotion primary, Emotion secondary, Emotion tertiary, string[,] dialogue, string name, bool[] status, int lifeMax, int hitChance, int blockChance,
            int resistChance, int dodgeChance, Attributes primaryAttribute, int strength, int defense, int intelligence, int wisdom, int dexterity,
            int agility, int resourceStat) :
            base(primary, secondary, tertiary, name, status, lifeMax, hitChance, blockChance, resistChance, dodgeChance,
            primaryAttribute, strength, defense, intelligence, wisdom, dexterity, agility, resourceStat)
        {
            Dialogue = dialogue;
        }

        public NPC(string name, Emotion primary, Emotion secondary, Emotion tertiary, string[,] dialogue) : base(name, primary, secondary, tertiary)
        {
            Dialogue = dialogue;
        }
    }
}

