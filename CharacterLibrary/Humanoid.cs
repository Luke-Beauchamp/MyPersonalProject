using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CharacterLibrary;

namespace CharacterLibrary
{
    public class Humanoid : Character
    {
        //In this class I will define the object "Humanoid" as a child of Character, this humanoid class will be used as a parent
        //to Player and NPC

        //Humanoids will have emotions, and variables to track those emotions, 
        //Emotion Primary; Emotion Secondary; Emotion Tertiary; Emotion Quaternary; Emotion Quinary; Emotion Opposite;

        //The idea behind this emotional system is to allow a more in depth level of social interaction.  Based on the player's emotions
        //and the NPC they are interacting with, more options might open up if their emotions align, or options might become unavailable
        //if their emotions oppose.  This makes making interactions a bit more intensive, but will hopefully shine in actual playability
        //and create a level of depth to this aspect of the dungeon.

        private Emotion _quaternary;
        private Emotion _quinary;
        private Emotion _opposite;
        private Emotion _current;

        public Emotion Primary { get; set; }
        public Emotion Secondary { get; set; }
        public Emotion Tertiary { get; set; }
        public Emotion Quaternary
        {
            get
            {

                switch (Primary) //This checks the secondary emotion against the primary and determines either a combination emotion or
                                 //an intensified primary emotion
                {
                    case Emotion.Fear:
                        _quaternary = Secondary == Emotion.Trust ? Emotion.Submission : Secondary == Emotion.Surprise ? Emotion.Awe : Emotion.Terror;
                        break;
                    case Emotion.Trust:
                        _quaternary = Secondary == Emotion.Fear ? Emotion.Submission : Secondary == Emotion.Joy ? Emotion.Love : Emotion.Admiration;
                        break;
                    case Emotion.Joy:
                        _quaternary = Secondary == Emotion.Trust ? Emotion.Joy : Secondary == Emotion.Anticipation ? Emotion.Optimism : Emotion.Ecstasy;
                        break;
                    case Emotion.Anticipation:
                        _quaternary = Secondary == Emotion.Joy ? Emotion.Optimism : Secondary == Emotion.Anger ? Emotion.Aggressiveness : Emotion.Vigilance;
                        break;
                    case Emotion.Anger:
                        _quaternary = Secondary == Emotion.Anticipation ? Emotion.Aggressiveness : Secondary == Emotion.Disgust ? Emotion.Contempt : Emotion.Rage;
                        break;
                    case Emotion.Disgust:
                        _quaternary = Secondary == Emotion.Anger ? Emotion.Contempt : Secondary == Emotion.Sadness ? Emotion.Remorse : Emotion.Loathing;
                        break;
                    case Emotion.Sadness:
                        _quaternary = Secondary == Emotion.Disgust ? Emotion.Remorse : Secondary == Emotion.Surprise ? Emotion.Disapproval : Emotion.Grief;
                        break;
                    case Emotion.Surprise:
                        _quaternary = Secondary == Emotion.Sadness ? Emotion.Disapproval : Secondary == Emotion.Fear ? Emotion.Awe : Emotion.Amazement;
                        break;
                    default:
                        Quaternary = Emotion.Null; //Something went wrong if the default happens XD
                        break;
                }
                return _quaternary;
            }
            set { _quaternary = value; }
        }
        public Emotion Quinary {
            get
            {
                switch (Primary) 
                                 //This checks the tertiary emotion against the primary and determines either a combination emotion or
                                 //a less intense primary emotion
                {
                    case Emotion.Fear:
                        _quinary = Tertiary == Emotion.Trust ? Emotion.Submission : Tertiary == Emotion.Surprise ? Emotion.Awe : Emotion.Apprehension;
                        break;
                    case Emotion.Trust:
                        _quinary = Tertiary == Emotion.Fear ? Emotion.Submission : Tertiary == Emotion.Joy ? Emotion.Love : Emotion.Acceptance;
                        break;
                    case Emotion.Joy:
                        _quinary = Tertiary == Emotion.Trust ? Emotion.Joy : Tertiary == Emotion.Anticipation ? Emotion.Optimism : Emotion.Serenity;
                        break;
                    case Emotion.Anticipation:
                        _quinary = Tertiary == Emotion.Joy ? Emotion.Optimism : Tertiary == Emotion.Anger ? Emotion.Aggressiveness : Emotion.Interest;
                        break;
                    case Emotion.Anger:
                        _quinary = Tertiary == Emotion.Anticipation ? Emotion.Aggressiveness : Tertiary == Emotion.Disgust ? Emotion.Contempt : Emotion.Annoyance;
                        break;
                    case Emotion.Disgust:
                        _quinary = Tertiary == Emotion.Anger ? Emotion.Contempt : Tertiary == Emotion.Sadness ? Emotion.Remorse : Emotion.Boredom;
                        break;
                    case Emotion.Sadness:
                        _quinary = Tertiary == Emotion.Disgust ? Emotion.Remorse : Tertiary == Emotion.Surprise ? Emotion.Disapproval : Emotion.Pensiveness;
                        break;
                    case Emotion.Surprise:
                        _quinary = Tertiary == Emotion.Sadness ? Emotion.Disapproval : Tertiary == Emotion.Fear ? Emotion.Awe : Emotion.Distraction;
                        break;
                    default:
                        _quinary = Emotion.Null; //Something went wrong if the default happens XD
                        break;
                }
                return _quinary;
            }
            set { _quinary = value; }
        }
        public Emotion Opposite {
            get
            {
                switch (Primary) //This checks the secondary emotion against the primary and determines either a combination emotion or
                                 //an intensified primary emotion
                                 //This checks the tertiary emotion against the primary and determines either a combination emotion or
                                 //a less intense primary emotion
                                 //Checks primary emotion to determine opposite emotion
                {
                    case Emotion.Fear:
                        _opposite = Emotion.Anger;
                        break;
                    case Emotion.Trust:
                        _opposite = Emotion.Disgust;
                        break;
                    case Emotion.Joy:
                        _opposite = Emotion.Sadness;
                        break;
                    case Emotion.Anticipation:
                        _opposite = Emotion.Surprise;
                        break;
                    case Emotion.Anger:
                        _opposite = Emotion.Fear;
                        break;
                    case Emotion.Disgust:
                        _opposite = Emotion.Trust;
                        break;
                    case Emotion.Sadness:
                        _opposite = Emotion.Joy;
                        break;
                    case Emotion.Surprise:
                        _opposite = Emotion.Anticipation;
                        break;
                    default:
                        _opposite = Emotion.Null; //This should not happen.
                        break;
                }
                return _opposite;
            }
            set { _opposite = value; }
        }
        public Emotion Current {
            get { return _current; }
            set
            {
                _current = value == Primary || value == Secondary || value == Tertiary || value == Quaternary || value == Quinary ? value : Emotion.Null;
            }
        }
        public int[] Rapport { get; set; } //array tracking emotional rapport { 0: Anger, 1: Anticipation, 2: Disgust, 3: Fear, 4: Joy, 5: Sadness, 6: Surprise, 7: Trust

        //Ctors
        public Humanoid(int floor, Attributes primaryAttribute) : base(floor, primaryAttribute)
        {
            Rapport = new int[8] { 2, 2, 2, 2, 2, 2, 2, 2 };
            Primary = Emotion.Null;
            Secondary = Emotion.Null;
            Tertiary = Emotion.Null;
            Quaternary = Emotion.Null;
            Quinary = Emotion.Null;
            Opposite = Emotion.Null;
        }

        public Humanoid(Emotion primary, Emotion secondary, Emotion tertiary, 
            string name, bool[] status, int lifeMax,  int hitChance, int blockChance, int resistChance, int dodgeChance,
            Attributes primaryAttribute, int strength, int defense, int intelligence, int wisdom, int dexterity, int agility, int resourceStat) :
            base(name, status, lifeMax, hitChance, blockChance, resistChance, dodgeChance, primaryAttribute, strength, defense, intelligence, 
                wisdom, dexterity, agility, resourceStat)
        {
            Primary = primary;
            Current = primary;
            Secondary = secondary;
            Tertiary = tertiary;
        }

        public Humanoid(string name, Emotion primary, Emotion secondary, Emotion tertiary) : base(name)
        {
            Primary = primary;
            Secondary = secondary;
            Tertiary = tertiary;
        }


        public static int RapportString(int rapport)
        {
            string rapportString = rapport <= -15 ? "Terrible" : rapport < -7 ? "Bad" : rapport < -2 ? "Poor" : rapport < 3 ? 
                "Moderate" : rapport < 8 ? "Fair" : rapport < 15 ? "Good" : "Great";
            switch (rapportString)
            {
                case "Terrible": Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    break;
                case "Bad": Console.ForegroundColor = ConsoleColor.DarkRed;
                    break;
                case "Poor": Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case "Moderate": Console.ForegroundColor = ConsoleColor.DarkYellow;
                    break;
                case "Fair": Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case "Good": Console.ForegroundColor = ConsoleColor.Green; 
                    break;
                case "Great": Console.ForegroundColor = ConsoleColor.DarkGreen;
                    break;
            }
            Console.Write(rapportString);
            return rapportString.Length;
        }

        public int WriteRapport(Emotion emotion, int x, int y)
        {
            int rapport = 0;
            Console.SetCursorPosition(x, y);
            switch (emotion)
            {
                case Emotion.Fear:
                    rapport = Rapport[3]; Console.ForegroundColor = ConsoleColor.DarkGray; Console.Write("Fear"); x += 4;
                    break;
                case Emotion.Trust:
                    rapport = Rapport[7]; Console.ForegroundColor = ConsoleColor.Blue; Console.Write("Trust"); x += 5;
                    break;
                case Emotion.Joy:
                    rapport = Rapport[4]; Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("Joy"); x += 3;
                    break;
                case Emotion.Anticipation:
                    rapport = Rapport[1]; Console.ForegroundColor = ConsoleColor.DarkGreen; Console.Write("Anticipation"); x += 12;
                    break;
                case Emotion.Anger:
                    rapport = Rapport[0]; Console.ForegroundColor = ConsoleColor.DarkRed; Console.Write("Anger"); x += 5;
                    break;
                case Emotion.Disgust:
                    rapport = Rapport[2]; Console.ForegroundColor = ConsoleColor.Magenta; Console.Write("Disgust"); x += 7;
                    break;
                case Emotion.Sadness:
                    rapport = Rapport[5]; Console.ForegroundColor = ConsoleColor.DarkYellow; Console.Write("Sadness"); x += 7;
                    break;
                case Emotion.Surprise:
                    rapport = Rapport[6]; Console.ForegroundColor = ConsoleColor.Cyan; Console.Write("Surprise"); x += 8;
                    break;
            }

            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = ConsoleColor.Gray; Console.Write(":"); x += 2;
            Console.SetCursorPosition(x, y);
            x += RapportString(rapport);
            return x;
        }

    }//end class
}
