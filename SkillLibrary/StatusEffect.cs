using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillLibrary
{
    public enum StatusEffect
    {
        None, //Index 0 in bool[] Status on characters

        //These are PrimaryEmotion specific status effects available only to skills with PrimaryEmotion of X -
        //See Character.StatusDoes() for combat numbers
        //Also if I add more statuses, increase the Status (bool array) on players and monsters to fit...
        //And update StatusDoes()
        Tower, //1: Fear- Increased Defense 20%
        Tremble, //2: Fear- Decreased Defense
        Certain, //3: Trust-  Increased Chances
        Conflict, //4: Trust- Decreased Chances
        Presence, //5: Joy- Increased HitChance
        Whimsy, //6: Joy- Decreased HitChance
        Nimble, //7: Anticipation- Increased DodgeChance
        Fumble, //8: Anticipation- Decreased DodgeChance
        Rage, //9: Anger- Increased strength
        Stagger, //10: Anger- Decreased strength  
        Resilient, //11: Disgust- Increased ResistChance
        Fade,//12: Disgust- Decreased ResistChance
        Relish, //13: Sadness- Increased PrimaryAttribute 
        Sink, //14: Sadness- Decreased PrimaryAttribute
        Ready, //15: Surprise- Increased Agility
        Unsteady //16: Surprise- Decreased Agility

        
    }
}
