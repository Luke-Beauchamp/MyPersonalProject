using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CharacterLibrary;
using PlayerLibrary;
using SkillLibrary;
using EquipmentLibrary;
using ItemLibrary;

namespace MenuLibrary
{
    public class CharacterInfo
    {
        //Here I will hold public classes for creating menus that store character info
        //Character Short Sheet
        //Name,  Life/LifeMax,  Resource/ResourceMax,  MinDamage/MaxDamage
        //
        //Character Long Sheet

        public static void ClearArea()
        {
            for (int y = 1; y < 5; y++)
            {
                for (int x = 3; x < 118; x++)
                {
                    Console.SetCursorPosition(x, y);
                    Console.Write(" ");
                }
            }
        }

        public static void ShortSheet(Player player)
        {
            #region ShortSheet

            //strings
            string resource = player.PrimaryAttribute == Attributes.Strength || player.PrimaryAttribute == Attributes.Defense ? "  Stamina" :
                player.PrimaryAttribute == Attributes.Intelligence || player.PrimaryAttribute == Attributes.Wisdom ? "Knowledge" : "Intuition";
            //Creating a short character sheet to be displayed usually
            //Header below
            Console.SetCursorPosition(11, 1); Console.ForegroundColor = ConsoleColor.White; Console.Write("//Character Info");

            //Name Below
            //Name shouldn't be more than 15 characters long
            Console.SetCursorPosition(22, 2); Console.ForegroundColor = ConsoleColor.White; Console.Write("Name:");
            Console.SetCursorPosition(28, 2); Console.ForegroundColor = ConsoleColor.DarkCyan; Console.Write(player.Name);
            //Life Below
            //Life changes color based on how low compared to LifeMax
            Console.SetCursorPosition(53, 2); Console.ForegroundColor = ConsoleColor.White; Console.Write("Life:");
            Console.SetCursorPosition(59, 2); Console.ForegroundColor = ConsoleColor.Gray;
            player.LifeColor(); Console.Write("{0}/{1}", player.Life, player.FindLife());
            //Emotion
            Console.SetCursorPosition(69, 4); Console.ForegroundColor = ConsoleColor.White; Console.Write("Current Emotion:");
            Console.ForegroundColor = player.EmotionColor(player.Current);
            Console.SetCursorPosition(86, 4); Console.Write("                  ");
            Console.SetCursorPosition(86, 4); Console.Write("{0}", player.Current.ToString());

            //Damage Below
            Console.SetCursorPosition(20, 4); Console.ForegroundColor = ConsoleColor.White; Console.Write("Damage:");
            Console.SetCursorPosition(28, 4); Console.ForegroundColor = ConsoleColor.Green; Console.Write("{0}-{1}", player.MinDamage(), player.MaxDamage());
            //Resource Below
            Console.SetCursorPosition(57-player.ResourceAttribute.ToString().Length, 4); Console.ForegroundColor = ConsoleColor.White; Console.Write("{0}:", player.ResourceAttribute);
            Console.SetCursorPosition(59, 4); player.ResourceColor(); Console.Write("{0}/{1}", player.Resource, player.FindResource());
            //Status Below
            //Code for witing player statuses into a string...
            List<string> currentstatuses = new List<string> { }; int counter = 0;
            StatusEffect[] effects = {StatusEffect.None, StatusEffect.Tower, StatusEffect.Tremble, StatusEffect.Certain, StatusEffect.Conflict,
            StatusEffect.Presence, StatusEffect.Whimsy, StatusEffect.Nimble, StatusEffect.Fumble, StatusEffect.Rage, StatusEffect.Stagger,
            StatusEffect.Resilient, StatusEffect.Fade, StatusEffect.Relish, StatusEffect.Sink, StatusEffect.Ready, StatusEffect.Unsteady };
            int x = 84; int y = 2;
            int x2 = 84; int y2 = 3;
            foreach (bool status in player.Status)
            {
                if (status)
                {
                    currentstatuses.Add(effects[counter].ToString());
                    Console.ForegroundColor = ConsoleColor.White;

                    if (currentstatuses.Count <= 3)
                    {
                        Console.SetCursorPosition(x, y); Console.Write("{0}", currentstatuses.Count > 1 ? "," : ""); x += 2;
                        Console.ForegroundColor = counter == 0 ? ConsoleColor.Gray : counter % 2 == 0 ? ConsoleColor.Red : 
                            ConsoleColor.Green; Console.SetCursorPosition(x, y); Console.Write("{0}", effects[counter].ToString());
                        x += effects[counter].ToString().Length;
                    }
                    else if (currentstatuses.Count <= 6)
                    {
                        Console.SetCursorPosition(x2, y2); Console.Write("{0}", currentstatuses.Count > 4 ? "," : ""); x2 += 2;
                        Console.ForegroundColor = counter % 2 == 0 ? ConsoleColor.Red : ConsoleColor.Green;
                        Console.SetCursorPosition(x2, y2); Console.Write("{0}", effects[counter].ToString());
                        x2 += effects[counter].ToString().Length;
                    }
                    else { Console.SetCursorPosition(x2, y2); Console.Write("..."); }
                }
                counter++;
            }
            //End Code
            Console.SetCursorPosition(76, 2); Console.ForegroundColor = ConsoleColor.White; Console.Write("{0}:",
                currentstatuses.Count > 1 ? "Statuses" : "  Status");
            Console.SetCursorPosition(86, 4);

            #endregion

        }

        public static void ShortAttributes(Player player)
        {
            //Creating a short attribute sheet to be toggled to
            //Header below
            Console.SetCursorPosition(5, 1); Console.ForegroundColor = ConsoleColor.White; Console.Write("//Character Attributes");
            //Strength below
            int[] attrMods = player.EqAttrMod();
            Console.SetCursorPosition(18, 2); Console.ForegroundColor = ConsoleColor.White; Console.Write("Strength:", attrMods[0], attrMods[1], attrMods[2]);
            Console.SetCursorPosition(28, 2); Console.ForegroundColor = player.Strength + attrMods[0] >= player.Strength ? 
                ConsoleColor.Green : ConsoleColor.Red;
            Console.Write("{0}", player.Strength + attrMods[0]);
            Console.SetCursorPosition(29 + attrMods[0].ToString().Length, 2); Console.ForegroundColor = ConsoleColor.DarkGray; Console.Write(" ({0})", player.Strength);
            //Intelligence below
            Console.SetCursorPosition(45, 2); Console.ForegroundColor = ConsoleColor.White; Console.Write("Intelligence:");
            Console.SetCursorPosition(59, 2); Console.ForegroundColor = player.Intelligence + attrMods[2] >= player.Intelligence ?
                ConsoleColor.Green : ConsoleColor.Red;
            Console.Write("{0}", player.Intelligence + attrMods[2]);
            Console.SetCursorPosition(61 + attrMods[2].ToString().Length, 2); Console.ForegroundColor = ConsoleColor.DarkGray; Console.Write("({0})", player.Intelligence);
            //Dexterity below
            Console.SetCursorPosition(74, 2); Console.ForegroundColor = ConsoleColor.White; Console.Write("Dexterity:");
            Console.SetCursorPosition(85, 2); Console.ForegroundColor = player.Dexterity + attrMods[4] >= player.Dexterity ? 
                ConsoleColor.Green : ConsoleColor.Red;
            Console.Write("{0}", player.Dexterity + attrMods[4]);
            Console.SetCursorPosition(87 + attrMods[4].ToString().Length, 2); Console.ForegroundColor = ConsoleColor.DarkGray; Console.Write("({0})", player.Dexterity);

            //Defense below
            Console.SetCursorPosition(19, 4); Console.ForegroundColor = ConsoleColor.White; Console.Write("Defense:");
            Console.SetCursorPosition(28, 4); Console.ForegroundColor = player.Defense + attrMods[1] >= player.Defense ? 
                ConsoleColor.Green : ConsoleColor.Red;
            Console.Write("{0}", player.Defense + attrMods[1]);
            Console.SetCursorPosition(30 + attrMods[1].ToString().Length, 4); Console.ForegroundColor = ConsoleColor.DarkGray; Console.Write("({0})", player.Defense);
            //Wisdom below
            Console.SetCursorPosition(51, 4); Console.ForegroundColor = ConsoleColor.White; Console.Write("Wisdom:");
            Console.SetCursorPosition(59, 4); Console.ForegroundColor = player.Wisdom + attrMods[3] >= player.Wisdom ? 
                ConsoleColor.Green : ConsoleColor.Red;
            Console.Write("{0}", player.Wisdom + attrMods[3]);
            Console.SetCursorPosition(61 + attrMods[2].ToString().Length, 4); Console.ForegroundColor = ConsoleColor.DarkGray; Console.Write("({0})", player.Wisdom);
            //Agility below
            Console.SetCursorPosition(76, 4); Console.ForegroundColor = ConsoleColor.White; Console.Write("Agility:");
            Console.SetCursorPosition(85, 4); Console.ForegroundColor = player.Agility + attrMods[5] >= player.Agility ? 
                ConsoleColor.Green : ConsoleColor.Red;
            Console.Write("{0}", player.Agility + attrMods[5]);
            Console.SetCursorPosition(86 + attrMods[5].ToString().Length, 4); Console.ForegroundColor = ConsoleColor.DarkGray; Console.Write("({0})", player.Agility);
            Console.ForegroundColor = ConsoleColor.White;
            int x = player.PrimaryAttribute == Attributes.Strength ? 17 : player.PrimaryAttribute == Attributes.Defense ? 18 :
                player.PrimaryAttribute == Attributes.Intelligence ? 44 : player.PrimaryAttribute == Attributes.Wisdom ? 50 :
                player.PrimaryAttribute == Attributes.Dexterity ? 73 : 75;
            int y = player.PrimaryAttribute == Attributes.Strength || player.PrimaryAttribute == Attributes.Intelligence ||
                player.PrimaryAttribute == Attributes.Dexterity ? 2 : 4;
            Console.SetCursorPosition(x, y); Console.Write("*");
        }//Need to give negative values a Red color instead of Green

        public static void ShortEquipment(Player player)
        {
            int x = 12; int y = 2;
            Console.SetCursorPosition(6, 1); Console.ForegroundColor = ConsoleColor.White; Console.Write("//Character Equipment");
            //Head:
            Console.SetCursorPosition(x, y); Console.Write("Head:");
            Console.SetCursorPosition(x + 6, y); player.Write(player.Equipment.EquippedHead, 0); x += 30;
            //Weapon:
            Console.SetCursorPosition(x, y); Console.Write("Weapon:");
            Console.SetCursorPosition(x + 8, y); player.Write(player.Equipment.EquippedWeapon, 0); x += 33;
            //Offhand:
            Console.SetCursorPosition(x, y); Console.Write("Off Hand:");
            Console.SetCursorPosition(x + 10, y); player.Write(player.Equipment.EquippedOffHand, 0); x = 10; y = 4;
            //Amulet:
            Console.SetCursorPosition(x, y); Console.Write("Amulet:");
            Console.SetCursorPosition(x + 8, y); player.Write(player.Equipment.EquippedAmulet, 0); x += 32;
            //Outfit:
            Console.SetCursorPosition(x, y); Console.Write("Outfit:");
            Console.SetCursorPosition(x + 8, y); player.Write(player.Equipment.EquippedOutfit, 0); x += 32;
            //Accessory:
            Console.SetCursorPosition(x, y); Console.Write("Accessory:");
            Console.SetCursorPosition(x + 11, y); player.Write(player.Equipment.EquippedAccessory, 0); x += 11;

        }

        public static void LongSheet(Player player, int menu, int selector)
        {
            int x = 90;
            int y = 7;
            int selected = selector;
            string resource = player.ResourceAttribute.ToString();
            Console.ForegroundColor = ConsoleColor.White;
            switch (menu)
            {
                case 0: //Create LongSheet for Character Info Details
                    #region Character Info
                    //Header
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    x = 87; y = 6; Console.SetCursorPosition(x, y); Console.Write("[|");
                    y = 7; Console.SetCursorPosition(x, y); Console.Write("[|"); 
                    y = 8; Console.SetCursorPosition(x, y); Console.Write("[|_____________");
                    x = 91; y = 7;  Console.ForegroundColor = ConsoleColor.White;
                    Console.SetCursorPosition(x, y); Console.Write("Info Menu");
                    x = 9; y = 7; Console.SetCursorPosition(x, y); Console.Write("//"); x = 11; Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.SetCursorPosition(x, y); Console.Write("{0}", player.Name); x += player.Name.Length; Console.ForegroundColor = ConsoleColor.White;
                    Console.SetCursorPosition(x, y); Console.Write("'s Character Details");
                    //Life
                    x = 7; y = 9; Console.SetCursorPosition(x, y); Console.WriteLine("//");
                    x = 9; Console.SetCursorPosition(x, y);
                    player.LifeColor(); Console.Write("Life"); Console.ForegroundColor = ConsoleColor.White;
                    x = 13; Console.SetCursorPosition(x, y); Console.Write(":"); Console.ForegroundColor = ConsoleColor.Gray;
                    x = 14; Console.SetCursorPosition(x, y);  Console.Write(" In combat \"hit points\"; modified by core attribute and equipment");
                    //Resource
                    x = 7; y = 11; Console.SetCursorPosition(x, y); Console.ForegroundColor = ConsoleColor.White; Console.Write("//{0}:",
                        player.ResourceAttribute.ToString()); x += 4 + player.ResourceAttribute.ToString().Length;
                    Console.SetCursorPosition(x, y); Console.ForegroundColor = ConsoleColor.Gray;
                    string resourceDescription = player.ResourceAttribute == Attributes.Stamina ? "Resource for skills; refills after combat; modified by equipment" :
                        player.ResourceAttribute == Attributes.Knowledge ? "Resource for skills; refills after resting; modified by equipment" : 
                        "Resource for skills; fills while attacking; modified by equipment"; Console.Write(resourceDescription);
                    //Damage
                    x = 7; y = 13; Console.SetCursorPosition(x, y); Console.ForegroundColor = ConsoleColor.White; Console.Write("//Damage:");
                    x = 17; Console.SetCursorPosition(x, y); Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write("Attack damage, set by weapon and modified by strength, equipment and statuses");
                    //Emotions
                    //Primary
                    x = 7; y = 15; Console.SetCursorPosition(x, y); Console.ForegroundColor = ConsoleColor.White; Console.Write("//Emotions:"); Console.ForegroundColor = player.EmotionColor(player.Primary); 
                    x = 19; Console.SetCursorPosition(x, y); Console.Write(player.Primary.ToString()); Console.ForegroundColor = ConsoleColor.Gray;
                    x += player.Primary.ToString().Length; Console.SetCursorPosition(x, y); Console.Write("- Primary");  x += 14;
                    //Secondary
                    Console.SetCursorPosition(x, y); Console.ForegroundColor = player.EmotionColor(player.Secondary); Console.Write(player.Secondary.ToString());
                    x += player.Secondary.ToString().Length; Console.SetCursorPosition(x, y); Console.ForegroundColor = ConsoleColor.Gray; Console.Write("- Secondary");
                    x += 16 ; Console.SetCursorPosition(x, y); Console.ForegroundColor = player.EmotionColor(player.Tertiary);
                    //Tertiary
                    Console.Write(player.Tertiary.ToString()); x += player.Tertiary.ToString().Length; Console.SetCursorPosition(x, y);
                    Console.ForegroundColor = ConsoleColor.Gray; Console.Write("- Tertiary"); Console.ForegroundColor = ConsoleColor.White;
                        //Emotional Rapport
                    x = 7; y = 17; Console.SetCursorPosition(x, y); Console.Write("\\\\Emotional Rapport:"); Console.ForegroundColor = ConsoleColor.Gray;
                    x = 28; Console.SetCursorPosition(x, y); Console.Write("Tracks rapport with characters of certain emotional qualities");

                    x = 9; y = 19; x = player.WriteRapport(Emotion.Anger, x, y); x += 5;
                    x = player.WriteRapport(Emotion.Anticipation, x, y); x += 5;
                    x = player.WriteRapport(Emotion.Disgust, x, y); x += 5;
                    x = player.WriteRapport(Emotion.Fear, x, y);

                    x = 9; y = 21; x = player.WriteRapport(Emotion.Joy, x, y); x += 5;
                    x = player.WriteRapport(Emotion.Sadness, x, y); x += 5;
                    x = player.WriteRapport(Emotion.Surprise, x, y); x += 5;
                    x = player.WriteRapport(Emotion.Trust, x, y);

                    //Statuses
                    x = 7; y = 23; Console.SetCursorPosition(x, y); Console.ForegroundColor = ConsoleColor.White; Console.Write("//Status:");
                    x = 17; Console.SetCursorPosition(x, y); Console.ForegroundColor = ConsoleColor.Green; Console.Write("Positively");
                    x = 28; Console.SetCursorPosition(x, y); Console.ForegroundColor = ConsoleColor.Gray; Console.Write("or");
                    x = 31; Console.SetCursorPosition(x, y); Console.ForegroundColor = ConsoleColor.Red; Console.Write("negatively");
                    x = 42; Console.SetCursorPosition(x, y); Console.ForegroundColor = ConsoleColor.Gray; Console.Write("affects a wide variety of the player's combat effectiveness");
                    break;
                #endregion
                case 1: //Create LongSheet for Attribute Details
                    #region Attributes Info
                    //Header
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    x = 81; y = 6; Console.SetCursorPosition(x, y);Console.Write("[|");
                    y++; Console.SetCursorPosition(x, y); Console.Write("[|");
                    y++; Console.SetCursorPosition(x, y); Console.Write("[|___________________");
                    x = 85; y = 7; Console.ForegroundColor = ConsoleColor.White;
                    Console.SetCursorPosition(x, y); Console.Write("Attributes Menu");
                    x = 9; y = 7; Console.SetCursorPosition(x, y); Console.Write("//"); x = 11; Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.SetCursorPosition(x, y); Console.Write("{0}", player.Name); x += player.Name.Length; Console.ForegroundColor = ConsoleColor.White;
                    Console.SetCursorPosition(x, y); Console.Write("'s Attributes Details");
                    //Core Attribute
                    x = 7; y = 9; Console.SetCursorPosition(x, y); Console.Write("//Core Attribute *{0}:", player.PrimaryAttribute);
                    Console.ForegroundColor = ConsoleColor.Gray; x = 27 + player.PrimaryAttribute.ToString().Length;
                    Console.SetCursorPosition(x, y); Console.Write("increases life (+Life) and skill damage (sDMG)");
                    //Strength
                    x = 7; y = 11; Console.SetCursorPosition(x, y); Console.ForegroundColor = ConsoleColor.White; Console.Write("//Strength:");
                    x = 19; Console.SetCursorPosition(x, y); Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write("(STR) increases attack damage (DMG)");
                    //Defense
                    x = 7; y = 13; Console.SetCursorPosition(x, y); Console.ForegroundColor = ConsoleColor.White; Console.Write("//Defense:");
                    x = 18; Console.SetCursorPosition(x, y); Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write("(DEF) reduces attack damage taken");
                    //Intelligence
                    x = 7; y = 15; Console.SetCursorPosition(x, y); Console.ForegroundColor = ConsoleColor.White; Console.Write("//Intelligence:");
                    x = 23; Console.SetCursorPosition(x, y); Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write("(INT) increases status effect chances (STA%) and heals (+Heal)");
                    //Wisdom
                    x = 7; y = 17; Console.SetCursorPosition(x, y); Console.ForegroundColor = ConsoleColor.White; Console.Write("//Wisdom:");
                    x = 17; Console.SetCursorPosition(x, y); Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write("(WIS) increases resist chance (RES%) and shields (+Shield)");
                    //Dexterity
                    x = 7; y = 19; Console.SetCursorPosition(x, y); Console.ForegroundColor = ConsoleColor.White; Console.Write("//Dexterity:");
                    x = 20; Console.SetCursorPosition(x, y); Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write("(DEX) increases hit chance (HIT%)");
                    //Agility
                    x = 7; y = 21; Console.SetCursorPosition(x, y); Console.ForegroundColor = ConsoleColor.White; Console.Write("//Agility:");
                    x = 18; Console.SetCursorPosition(x, y); Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write("(AGI) increases dodge chance (DOD%)");
                    break;
                #endregion
                case 2: //Create LongSheet for Equipment Details
                    #region Equipment Info
                    //Header
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    x = 82; y = 6; Console.SetCursorPosition(x, y); Console.Write("[|");
                    y++; Console.SetCursorPosition(x, y); Console.Write("[|");
                    y++; Console.SetCursorPosition(x, y); Console.Write("[|__________________");
                    Console.ForegroundColor = ConsoleColor.White;
                    x = 86; y = 7; Console.SetCursorPosition(x, y); Console.Write("Equipment Menu");
                    x = 9; y = 7; Console.SetCursorPosition(x, y); Console.Write("//"); x = 11; Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.SetCursorPosition(x, y); Console.Write("{0}", player.Name); x += player.Name.Length; Console.ForegroundColor = ConsoleColor.White;
                    Console.SetCursorPosition(x, y); Console.Write("'s Equipment Details");
                    //explain
                    x = 7; y = 9; Console.SetCursorPosition(x, y); Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write("Equipment can"); x += 14; Console.SetCursorPosition(x, y); Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("increase"); x += 9; Console.SetCursorPosition(x, y); Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write("or"); x += 3; Console.SetCursorPosition(x, y); Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("decrease"); x += 9; Console.SetCursorPosition(x, y); Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write("various aspects of combat effectiveness.");

                    x = 7; y = 11;
                    Console.SetCursorPosition(x, y);
                    player.Write(player.Equipment.EquippedHead, 1);
                    x = 7; y = 13;
                    Console.SetCursorPosition(x, y);
                    player.Write(player.Equipment.EquippedWeapon, 1);
                    x = 7; y = 15;
                    Console.SetCursorPosition(x, y);
                    player.Write(player.Equipment.EquippedOffHand, 1);
                    x = 7; y = 17;
                    Console.SetCursorPosition(x, y);
                    player.Write(player.Equipment.EquippedAmulet, 1);
                    x = 7; y = 19;
                    Console.SetCursorPosition(x, y);
                    player.Write(player.Equipment.EquippedOutfit, 1);
                    x = 7; y = 21;
                    Console.SetCursorPosition(x, y);
                    player.Write(player.Equipment.EquippedAccessory, 1);

                    //#region Head
                    //x = 7; y = 11;
                    //if (player.Equipment.EquippedHead.Name != "")
                    //{
                    //    Console.SetCursorPosition(x, y); Console.ForegroundColor = ConsoleColor.White; Console.Write("//"); x += 2;
                    //    Console.SetCursorPosition(x, y); Console.ForegroundColor = ConsoleColor.DarkCyan;
                    //    Console.Write(player.Equipment.EquippedHead.Name); x += player.Equipment.EquippedHead.Name.Length;
                    //    Console.ForegroundColor = ConsoleColor.White; Console.SetCursorPosition(x, y); Console.Write(":"); x += 2;
                    //    if (player.Equipment.EquippedHead.Attributes[0] != 0)
                    //    {
                    //        Console.ForegroundColor = ConsoleColor.Gray; Console.SetCursorPosition(x, y); Console.Write("STR:"); x += 5;
                    //        Console.ForegroundColor = player.Equipment.EquippedHead.Attributes[0] > 0 ? ConsoleColor.Green : ConsoleColor.Red;
                    //        Console.SetCursorPosition(x, y); Console.Write("{0}", player.PrimaryAttribute != Attributes.Strength ?
                    //            player.Equipment.EquippedHead.Attributes[0] : (player.Equipment.EquippedHead.Attributes[0] +
                    //            player.Equipment.EquippedHead.PrimaryMod));
                    //        x += player.PrimaryAttribute != Attributes.Strength ? player.Equipment.EquippedHead.Attributes[0].ToString().Length + 3 :
                    //            (player.Equipment.EquippedHead.Attributes[0] + player.Equipment.EquippedHead.PrimaryMod).ToString().Length + 3;
                    //    }
                    //    if (player.Equipment.EquippedHead.Attributes[1] != 0)
                    //    {
                    //        Console.ForegroundColor = ConsoleColor.Gray; Console.SetCursorPosition(x, y); Console.Write("DEF:"); x += 5;
                    //        Console.ForegroundColor = player.Equipment.EquippedHead.Attributes[1] > 0 ? ConsoleColor.Green : ConsoleColor.Red;
                    //        Console.SetCursorPosition(x, y); Console.Write("{0}", player.PrimaryAttribute != Attributes.Defense ?
                    //            player.Equipment.EquippedHead.Attributes[1] : (player.Equipment.EquippedHead.Attributes[1] +
                    //            player.Equipment.EquippedHead.PrimaryMod));
                    //        x += player.PrimaryAttribute != Attributes.Defense ? player.Equipment.EquippedHead.Attributes[1].ToString().Length + 3 :
                    //            (player.Equipment.EquippedHead.Attributes[1] + player.Equipment.EquippedHead.PrimaryMod).ToString().Length + 3;
                    //    }
                    //    if (player.Equipment.EquippedHead.Attributes[2] != 0)
                    //    {
                    //        Console.ForegroundColor = ConsoleColor.Gray; Console.SetCursorPosition(x, y); Console.Write("INT:"); x += 5;
                    //        Console.ForegroundColor = player.Equipment.EquippedHead.Attributes[2] > 0 ? ConsoleColor.Green : ConsoleColor.Red;
                    //        Console.SetCursorPosition(x, y); Console.Write("{0}", player.PrimaryAttribute != Attributes.Intelligence ?
                    //            player.Equipment.EquippedHead.Attributes[2] : (player.Equipment.EquippedHead.Attributes[2] +
                    //            player.Equipment.EquippedHead.PrimaryMod));
                    //        x += player.PrimaryAttribute != Attributes.Intelligence ? player.Equipment.EquippedHead.Attributes[2].ToString().Length + 3 :
                    //            (player.Equipment.EquippedHead.Attributes[2] + player.Equipment.EquippedHead.PrimaryMod).ToString().Length + 3;
                    //    }
                    //    if (player.Equipment.EquippedHead.Attributes[3] != 0)
                    //    {
                    //        Console.ForegroundColor = ConsoleColor.Gray; Console.SetCursorPosition(x, y); Console.Write("WIS:"); x += 5;
                    //        Console.ForegroundColor = player.Equipment.EquippedHead.Attributes[3] > 0 ? ConsoleColor.Green : ConsoleColor.Red;
                    //        Console.SetCursorPosition(x, y); Console.Write("{0}", player.PrimaryAttribute != Attributes.Wisdom ?
                    //            player.Equipment.EquippedHead.Attributes[3] : (player.Equipment.EquippedHead.Attributes[3] +
                    //            player.Equipment.EquippedHead.PrimaryMod));
                    //        x += player.PrimaryAttribute != Attributes.Wisdom ? player.Equipment.EquippedHead.Attributes[3].ToString().Length + 3 :
                    //            (player.Equipment.EquippedHead.Attributes[3] + player.Equipment.EquippedHead.PrimaryMod).ToString().Length + 3;
                    //    }
                    //    if (player.Equipment.EquippedHead.Attributes[4] != 0)
                    //    {
                    //        Console.ForegroundColor = ConsoleColor.Gray; Console.SetCursorPosition(x, y); Console.Write("DEX:"); x += 5;
                    //        Console.ForegroundColor = player.Equipment.EquippedHead.Attributes[4] > 0 ? ConsoleColor.Green : ConsoleColor.Red;
                    //        Console.SetCursorPosition(x, y); Console.Write("{0}", player.PrimaryAttribute != Attributes.Dexterity ?
                    //            player.Equipment.EquippedHead.Attributes[4] : (player.Equipment.EquippedHead.Attributes[4] +
                    //            player.Equipment.EquippedHead.PrimaryMod));
                    //        x += player.PrimaryAttribute != Attributes.Dexterity ? player.Equipment.EquippedHead.Attributes[4].ToString().Length + 3 :
                    //            (player.Equipment.EquippedHead.Attributes[4] + player.Equipment.EquippedHead.PrimaryMod).ToString().Length + 3;
                    //    }
                    //    if (player.Equipment.EquippedHead.Attributes[5] != 0)
                    //    {
                    //        Console.ForegroundColor = ConsoleColor.Gray; Console.SetCursorPosition(x, y); Console.Write("AGI:"); x += 5;
                    //        Console.ForegroundColor = player.Equipment.EquippedHead.Attributes[5] > 0 ? ConsoleColor.Green : ConsoleColor.Red;
                    //        Console.SetCursorPosition(x, y); Console.Write("{0}", player.PrimaryAttribute != Attributes.Agility ?
                    //            player.Equipment.EquippedHead.Attributes[5] : (player.Equipment.EquippedHead.Attributes[5] +
                    //            player.Equipment.EquippedHead.PrimaryMod));
                    //        x += player.PrimaryAttribute != Attributes.Agility ? player.Equipment.EquippedHead.Attributes[5].ToString().Length + 3 :
                    //            (player.Equipment.EquippedHead.Attributes[5] + player.Equipment.EquippedHead.PrimaryMod).ToString().Length + 3;
                    //    }
                    //    if (player.Equipment.EquippedHead.Attributes[6] != 0)
                    //    {
                    //        Console.ForegroundColor = ConsoleColor.Gray; Console.SetCursorPosition(x, y); Console.Write("{0}:",
                    //            player.ResourceAttribute); x += 2 + player.ResourceAttribute.ToString().Length;
                    //        Console.ForegroundColor = player.Equipment.EquippedHead.Attributes[6] > 0 ? ConsoleColor.Green : ConsoleColor.Red;
                    //        Console.SetCursorPosition(x, y); Console.Write("{0}", player.Equipment.EquippedHead.Attributes[6]);
                    //        x += player.Equipment.EquippedHead.Attributes[6].ToString().Length + 3;
                    //    }
                    //}
                    //else
                    //{
                    //    Console.SetCursorPosition(x, y); Console.ForegroundColor = ConsoleColor.White; Console.Write("//Head:"); x += 8;
                    //    Console.SetCursorPosition(x, y); Console.ForegroundColor = ConsoleColor.DarkGray; Console.Write("Not Equipped");
                    //}
                    //#endregion
                    //#region Amulet
                    //x = 7; y = 13; if (player.Equipment.EquippedAmulet.Name != "")
                    //{
                    //    Console.SetCursorPosition(x, y); Console.ForegroundColor = ConsoleColor.White; Console.Write("//"); x += 2;
                    //    Console.SetCursorPosition(x, y); Console.ForegroundColor = ConsoleColor.DarkCyan;
                    //    Console.Write(player.Equipment.EquippedAmulet.Name); x += player.Equipment.EquippedAmulet.Name.Length;
                    //    Console.ForegroundColor = ConsoleColor.White; Console.SetCursorPosition(x, y); Console.Write(":"); x += 2;
                    //    if (player.Equipment.EquippedAmulet.ResourceMod != 0)
                    //    {
                    //        Console.ForegroundColor = ConsoleColor.Gray; Console.SetCursorPosition(x, y); Console.Write("{0}:", resource); x += resource.Length + 2;
                    //        Console.ForegroundColor = player.Equipment.EquippedAmulet.ResourceMod > 0 ? ConsoleColor.Green : ConsoleColor.Red;
                    //        Console.SetCursorPosition(x, y); Console.Write(player.Equipment.EquippedAmulet.ResourceMod);
                    //        x += player.Equipment.EquippedAmulet.ResourceMod.ToString().Length + 3;
                    //    }
                    //    if (player.Equipment.EquippedAmulet.Attributes[0] != 0)
                    //    {
                    //        Console.ForegroundColor = ConsoleColor.Gray; Console.SetCursorPosition(x, y); Console.Write("STR:"); x += 5;
                    //        Console.ForegroundColor = player.Equipment.EquippedAmulet.Attributes[0] > 0 ? ConsoleColor.Green : ConsoleColor.Red;
                    //        Console.SetCursorPosition(x, y); Console.Write(player.Equipment.EquippedAmulet.Attributes[0]);
                    //        x += player.Equipment.EquippedAmulet.Attributes[0].ToString().Length + 3;
                    //    }
                    //    if (player.Equipment.EquippedAmulet.Attributes[1] != 0)
                    //    {
                    //        Console.ForegroundColor = ConsoleColor.Gray; Console.SetCursorPosition(x, y); Console.Write("DEF:"); x += 5;
                    //        Console.ForegroundColor = player.Equipment.EquippedAmulet.Attributes[1] > 0 ? ConsoleColor.Green : ConsoleColor.Red;
                    //        Console.SetCursorPosition(x, y); Console.Write(player.Equipment.EquippedAmulet.Attributes[1]);
                    //        x += player.Equipment.EquippedAmulet.Attributes[1].ToString().Length + 3;
                    //    }
                    //    if (player.Equipment.EquippedAmulet.Attributes[2] != 0)
                    //    {
                    //        Console.ForegroundColor = ConsoleColor.Gray; Console.SetCursorPosition(x, y); Console.Write("INT:"); x += 5;
                    //        Console.ForegroundColor = player.Equipment.EquippedAmulet.Attributes[2] > 0 ? ConsoleColor.Green : ConsoleColor.Red;
                    //        Console.SetCursorPosition(x, y); Console.Write(player.Equipment.EquippedAmulet.Attributes[2]);
                    //        x += player.Equipment.EquippedAmulet.Attributes[2].ToString().Length + 3;
                    //    }
                    //    if (player.Equipment.EquippedAmulet.Attributes[3] != 0)
                    //    {
                    //        Console.ForegroundColor = ConsoleColor.Gray; Console.SetCursorPosition(x, y); Console.Write("WIS:"); x += 5;
                    //        Console.ForegroundColor = player.Equipment.EquippedAmulet.Attributes[3] > 0 ? ConsoleColor.Green : ConsoleColor.Red;
                    //        Console.SetCursorPosition(x, y); Console.Write(player.Equipment.EquippedAmulet.Attributes[3]);
                    //        x += player.Equipment.EquippedAmulet.Attributes[3].ToString().Length + 3;
                    //    }
                    //    if (player.Equipment.EquippedAmulet.Attributes[4] != 0)
                    //    {
                    //        Console.ForegroundColor = ConsoleColor.Gray; Console.SetCursorPosition(x, y); Console.Write("DEX:"); x += 5;
                    //        Console.ForegroundColor = player.Equipment.EquippedAmulet.Attributes[4] > 0 ? ConsoleColor.Green : ConsoleColor.Red;
                    //        Console.SetCursorPosition(x, y); Console.Write(player.Equipment.EquippedAmulet.Attributes[4]);
                    //        x += player.Equipment.EquippedAmulet.Attributes[4].ToString().Length + 3;
                    //    }
                    //    if (player.Equipment.EquippedAmulet.Attributes[5] != 0)
                    //    {
                    //        Console.ForegroundColor = ConsoleColor.Gray; Console.SetCursorPosition(x, y); Console.Write("AGI:"); x += 5;
                    //        Console.ForegroundColor = player.Equipment.EquippedAmulet.Attributes[5] > 0 ? ConsoleColor.Green : ConsoleColor.Red;
                    //        Console.SetCursorPosition(x, y); Console.Write(player.Equipment.EquippedAmulet.Attributes[5]);
                    //        x += player.Equipment.EquippedAmulet.Attributes[5].ToString().Length + 3;
                    //    }
                    //    if (player.Equipment.EquippedHead.Attributes[6] != 0)
                    //    {
                    //        Console.ForegroundColor = ConsoleColor.Gray; Console.SetCursorPosition(x, y); Console.Write("{0}:",
                    //            player.ResourceAttribute); x += 2 + player.ResourceAttribute.ToString().Length;
                    //        Console.ForegroundColor = player.Equipment.EquippedHead.Attributes[6] > 0 ? ConsoleColor.Green : ConsoleColor.Red;
                    //        Console.SetCursorPosition(x, y); Console.Write("{0}", player.Equipment.EquippedHead.Attributes[6]);
                    //        x += player.Equipment.EquippedHead.Attributes[6].ToString().Length + 3;
                    //    }
                    //}
                    //else
                    //{
                    //    Console.SetCursorPosition(x, y); Console.ForegroundColor = ConsoleColor.White; Console.Write("//Amulet:"); x += 10;
                    //    Console.SetCursorPosition(x, y); Console.ForegroundColor = ConsoleColor.DarkGray; Console.Write("Not Equipped");
                    //}
                    //#endregion      
                    //#region Outfit
                    //x = 7; y = 15; if (player.Equipment.EquippedOutfit.Name != "")
                    //{
                    //    Console.SetCursorPosition(x, y); Console.ForegroundColor = ConsoleColor.White; Console.Write("//"); x += 2;
                    //    Console.SetCursorPosition(x, y); Console.ForegroundColor = ConsoleColor.DarkCyan;
                    //    Console.Write(player.Equipment.EquippedOutfit.Name); x += player.Equipment.EquippedOutfit.Name.Length;
                    //    Console.ForegroundColor = ConsoleColor.White; Console.SetCursorPosition(x, y); Console.Write(":"); x += 2;
                    //    if (player.Equipment.EquippedOutfit.Chances[0] != 0)
                    //    {
                    //        Console.ForegroundColor = ConsoleColor.Gray; Console.SetCursorPosition(x, y); Console.Write("HIT%:"); x += 6;
                    //        Console.ForegroundColor = player.Equipment.EquippedOutfit.Chances[0] > 0 ? ConsoleColor.Green : ConsoleColor.Red;
                    //        Console.SetCursorPosition(x, y); Console.Write(player.Equipment.EquippedOutfit.Chances[0]);
                    //        x += player.Equipment.EquippedOutfit.Chances[0].ToString().Length + 3;
                    //    }
                    //    if (player.Equipment.EquippedOutfit.Chances[1] != 0)
                    //    {
                    //        Console.ForegroundColor = ConsoleColor.Gray; Console.SetCursorPosition(x, y); Console.Write("BLO%:"); x += 6;
                    //        Console.ForegroundColor = player.Equipment.EquippedOutfit.Chances[1] > 0 ? ConsoleColor.Green : ConsoleColor.Red;
                    //        Console.SetCursorPosition(x, y); Console.Write(player.Equipment.EquippedOutfit.Chances[1]);
                    //        x += player.Equipment.EquippedOutfit.Chances[1].ToString().Length + 3;
                    //    }
                    //    if (player.Equipment.EquippedOutfit.Chances[2] != 0)
                    //    {
                    //        Console.ForegroundColor = ConsoleColor.Gray; Console.SetCursorPosition(x, y); Console.Write("RES%:"); x += 6;
                    //        Console.ForegroundColor = player.Equipment.EquippedOutfit.Chances[2] > 0 ? ConsoleColor.Green : ConsoleColor.Red;
                    //        Console.SetCursorPosition(x, y); Console.Write(player.Equipment.EquippedOutfit.Chances[2]);
                    //        x += player.Equipment.EquippedOutfit.Chances[2].ToString().Length + 3;
                    //    }
                    //    if (player.Equipment.EquippedOutfit.Chances[3] != 0)
                    //    {
                    //        Console.ForegroundColor = ConsoleColor.Gray; Console.SetCursorPosition(x, y); Console.Write("DOD%:"); x += 6;
                    //        Console.ForegroundColor = player.Equipment.EquippedOutfit.Chances[3] > 0 ? ConsoleColor.Green : ConsoleColor.Red;
                    //        Console.SetCursorPosition(x, y); Console.Write(player.Equipment.EquippedOutfit.Chances[3]);
                    //        x += player.Equipment.EquippedOutfit.Chances[3].ToString().Length + 3;
                    //    }
                    //    if (player.Equipment.EquippedOutfit.Attributes[0] != 0)
                    //    {
                    //        Console.ForegroundColor = ConsoleColor.Gray; Console.SetCursorPosition(x, y); Console.Write("STR:"); x += 5;
                    //        Console.ForegroundColor = player.Equipment.EquippedOutfit.Attributes[0] > 0 ? ConsoleColor.Green : ConsoleColor.Red;
                    //        Console.SetCursorPosition(x, y); Console.Write(player.Equipment.EquippedOutfit.Attributes[0]);
                    //        x += player.Equipment.EquippedOutfit.Attributes[0].ToString().Length + 3;
                    //    }
                    //    if (player.Equipment.EquippedOutfit.Attributes[1] != 0)
                    //    {
                    //        Console.ForegroundColor = ConsoleColor.Gray; Console.SetCursorPosition(x, y); Console.Write("DEF:"); x += 5;
                    //        Console.ForegroundColor = player.Equipment.EquippedOutfit.Attributes[1] > 0 ? ConsoleColor.Green : ConsoleColor.Red;
                    //        Console.SetCursorPosition(x, y); Console.Write(player.Equipment.EquippedOutfit.Attributes[1]);
                    //        x += player.Equipment.EquippedOutfit.Attributes[1].ToString().Length + 3;
                    //    }
                    //    if (player.Equipment.EquippedOutfit.Attributes[2] != 0)
                    //    {
                    //        Console.ForegroundColor = ConsoleColor.Gray; Console.SetCursorPosition(x, y); Console.Write("INT:"); x += 5;
                    //        Console.ForegroundColor = player.Equipment.EquippedOutfit.Attributes[2] > 0 ? ConsoleColor.Green : ConsoleColor.Red;
                    //        Console.SetCursorPosition(x, y); Console.Write(player.Equipment.EquippedOutfit.Attributes[2]);
                    //        x += player.Equipment.EquippedOutfit.Attributes[2].ToString().Length + 3;
                    //    }
                    //    if (player.Equipment.EquippedOutfit.Attributes[3] != 0)
                    //    {
                    //        Console.ForegroundColor = ConsoleColor.Gray; Console.SetCursorPosition(x, y); Console.Write("WIS:"); x += 5;
                    //        Console.ForegroundColor = player.Equipment.EquippedOutfit.Attributes[3] > 0 ? ConsoleColor.Green : ConsoleColor.Red;
                    //        Console.SetCursorPosition(x, y); Console.Write(player.Equipment.EquippedOutfit.Attributes[3]);
                    //        x += player.Equipment.EquippedOutfit.Attributes[3].ToString().Length + 3;
                    //    }
                    //    if (player.Equipment.EquippedOutfit.Attributes[4] != 0)
                    //    {
                    //        Console.ForegroundColor = ConsoleColor.Gray; Console.SetCursorPosition(x, y); Console.Write("DEX:"); x += 5;
                    //        Console.ForegroundColor = player.Equipment.EquippedOutfit.Attributes[4] > 0 ? ConsoleColor.Green : ConsoleColor.Red;
                    //        Console.SetCursorPosition(x, y); Console.Write(player.Equipment.EquippedOutfit.Attributes[4]);
                    //        x += player.Equipment.EquippedOutfit.Attributes[4].ToString().Length + 3;
                    //    }
                    //    if (player.Equipment.EquippedOutfit.Attributes[5] != 0)
                    //    {
                    //        Console.ForegroundColor = ConsoleColor.Gray; Console.SetCursorPosition(x, y); Console.Write("AGI:"); x += 5;
                    //        Console.ForegroundColor = player.Equipment.EquippedOutfit.Attributes[5] > 0 ? ConsoleColor.Green : ConsoleColor.Red;
                    //        Console.SetCursorPosition(x, y); Console.Write(player.Equipment.EquippedOutfit.Attributes[5]);
                    //        x += player.Equipment.EquippedOutfit.Attributes[5].ToString().Length + 3;
                    //    }
                    //    if (player.Equipment.EquippedHead.Attributes[6] != 0)
                    //    {
                    //        Console.ForegroundColor = ConsoleColor.Gray; Console.SetCursorPosition(x, y); Console.Write("{0}:",
                    //            player.ResourceAttribute); x += 2 + player.ResourceAttribute.ToString().Length;
                    //        Console.ForegroundColor = player.Equipment.EquippedHead.Attributes[6] > 0 ? ConsoleColor.Green : ConsoleColor.Red;
                    //        Console.SetCursorPosition(x, y); Console.Write("{0}", player.Equipment.EquippedHead.Attributes[6]);
                    //        x += player.Equipment.EquippedHead.Attributes[6].ToString().Length + 3;
                    //    }
                    //}
                    //else
                    //{
                    //    Console.SetCursorPosition(x, y); Console.ForegroundColor = ConsoleColor.White; Console.Write("//Outfit:"); x += 10;
                    //    Console.SetCursorPosition(x, y); Console.ForegroundColor = ConsoleColor.DarkGray; Console.Write("Not Equipped");
                    //}
                    //#endregion 
                    //#region Weapon
                    //x = 7; y = 17; if (player.Equipment.EquippedWeapon.Name != "")
                    //{
                    //    Console.SetCursorPosition(x, y); Console.ForegroundColor = ConsoleColor.White; Console.Write("//"); x += 2;
                    //    Console.SetCursorPosition(x, y); Console.ForegroundColor = ConsoleColor.DarkCyan;
                    //    Console.Write(player.Equipment.EquippedWeapon.Name); x += player.Equipment.EquippedWeapon.Name.Length;
                    //    Console.ForegroundColor = ConsoleColor.White; Console.SetCursorPosition(x, y); Console.Write(":"); x += 2;
                    //    Console.ForegroundColor = ConsoleColor.Gray; Console.SetCursorPosition(x, y); Console.Write("Damage:"); x += 8;
                    //    Console.ForegroundColor = ConsoleColor.Green; Console.SetCursorPosition(x, y);
                    //    Console.Write("{0}-{1}", player.Equipment.EquippedWeapon.MinDamage, player.Equipment.EquippedWeapon.MaxDamage);
                    //    x += 4 + player.Equipment.EquippedWeapon.MinDamage.ToString().Length + player.Equipment.EquippedWeapon.MaxDamage.ToString().Length;

                    //    if (player.Equipment.EquippedWeapon.HitChanceBonus != 0)
                    //    {
                    //        Console.ForegroundColor = ConsoleColor.Gray; Console.SetCursorPosition(x, y); Console.Write("HIT%:"); x += 6;
                    //        Console.ForegroundColor = player.Equipment.EquippedWeapon.HitChanceBonus > 0 ? ConsoleColor.Green : ConsoleColor.Red;
                    //        Console.SetCursorPosition(x, y); Console.Write(player.Equipment.EquippedWeapon.HitChanceBonus);
                    //        x += player.Equipment.EquippedWeapon.HitChanceBonus.ToString().Length + 3;
                    //    }
                    //    if (player.Equipment.EquippedWeapon.Attributes[0] != 0)
                    //    {
                    //        Console.ForegroundColor = ConsoleColor.Gray; Console.SetCursorPosition(x, y); Console.Write("STR:"); x += 5;
                    //        Console.ForegroundColor = player.Equipment.EquippedWeapon.Attributes[0] > 0 ? ConsoleColor.Green : ConsoleColor.Red;
                    //        Console.SetCursorPosition(x, y); Console.Write(player.Equipment.EquippedWeapon.Attributes[0]);
                    //        x += player.Equipment.EquippedWeapon.Attributes[0].ToString().Length + 3;
                    //    }
                    //    if (player.Equipment.EquippedWeapon.Attributes[1] != 0)
                    //    {
                    //        Console.ForegroundColor = ConsoleColor.Gray; Console.SetCursorPosition(x, y); Console.Write("DEF:"); x += 5;
                    //        Console.ForegroundColor = player.Equipment.EquippedWeapon.Attributes[1] > 0 ? ConsoleColor.Green : ConsoleColor.Red;
                    //        Console.SetCursorPosition(x, y); Console.Write(player.Equipment.EquippedWeapon.Attributes[1]);
                    //        x += player.Equipment.EquippedWeapon.Attributes[1].ToString().Length + 3;
                    //    }
                    //    if (player.Equipment.EquippedWeapon.Attributes[2] != 0)
                    //    {
                    //        Console.ForegroundColor = ConsoleColor.Gray; Console.SetCursorPosition(x, y); Console.Write("INT:"); x += 5;
                    //        Console.ForegroundColor = player.Equipment.EquippedWeapon.Attributes[2] > 0 ? ConsoleColor.Green : ConsoleColor.Red;
                    //        Console.SetCursorPosition(x, y); Console.Write(player.Equipment.EquippedWeapon.Attributes[2]);
                    //        x += player.Equipment.EquippedWeapon.Attributes[2].ToString().Length + 3;
                    //    }
                    //    if (player.Equipment.EquippedWeapon.Attributes[3] != 0)
                    //    {
                    //        Console.ForegroundColor = ConsoleColor.Gray; Console.SetCursorPosition(x, y); Console.Write("WIS:"); x += 5;
                    //        Console.ForegroundColor = player.Equipment.EquippedWeapon.Attributes[3] > 0 ? ConsoleColor.Green : ConsoleColor.Red;
                    //        Console.SetCursorPosition(x, y); Console.Write(player.Equipment.EquippedWeapon.Attributes[3]);
                    //        x += player.Equipment.EquippedWeapon.Attributes[3].ToString().Length + 3;
                    //    }
                    //    if (player.Equipment.EquippedWeapon.Attributes[4] != 0)
                    //    {
                    //        Console.ForegroundColor = ConsoleColor.Gray; Console.SetCursorPosition(x, y); Console.Write("DEX:"); x += 5;
                    //        Console.ForegroundColor = player.Equipment.EquippedWeapon.Attributes[4] > 0 ? ConsoleColor.Green : ConsoleColor.Red;
                    //        Console.SetCursorPosition(x, y); Console.Write(player.Equipment.EquippedWeapon.Attributes[4]);
                    //        x += player.Equipment.EquippedWeapon.Attributes[4].ToString().Length + 3;
                    //    }
                    //    if (player.Equipment.EquippedWeapon.Attributes[5] != 0)
                    //    {
                    //        Console.ForegroundColor = ConsoleColor.Gray; Console.SetCursorPosition(x, y); Console.Write("AGI:"); x += 5;
                    //        Console.ForegroundColor = player.Equipment.EquippedWeapon.Attributes[5] > 0 ? ConsoleColor.Green : ConsoleColor.Red;
                    //        Console.SetCursorPosition(x, y); Console.Write(player.Equipment.EquippedWeapon.Attributes[5]);
                    //        x += player.Equipment.EquippedWeapon.Attributes[5].ToString().Length + 3;
                    //    }
                    //    if (player.Equipment.EquippedHead.Attributes[6] != 0)
                    //    {
                    //        Console.ForegroundColor = ConsoleColor.Gray; Console.SetCursorPosition(x, y); Console.Write("{0}:",
                    //            player.ResourceAttribute); x += 2 + player.ResourceAttribute.ToString().Length;
                    //        Console.ForegroundColor = player.Equipment.EquippedHead.Attributes[6] > 0 ? ConsoleColor.Green : ConsoleColor.Red;
                    //        Console.SetCursorPosition(x, y); Console.Write("{0}", player.Equipment.EquippedHead.Attributes[6]);
                    //        x += player.Equipment.EquippedHead.Attributes[6].ToString().Length + 3;
                    //    }
                    //}
                    //else
                    //{
                    //    Console.SetCursorPosition(x, y); Console.ForegroundColor = ConsoleColor.White; Console.Write("//Weapon:"); x += 10;
                    //    Console.SetCursorPosition(x, y); Console.ForegroundColor = ConsoleColor.DarkGray; Console.Write("Not Equipped");
                    //}
                    //#endregion      
                    //#region Off Hand
                    //x = 7; y = 19; if (player.Equipment.EquippedOffHand.Name != "")
                    //{
                    //    Console.SetCursorPosition(x, y); Console.ForegroundColor = ConsoleColor.White; Console.Write("//"); x += 2;
                    //    Console.SetCursorPosition(x, y); Console.ForegroundColor = ConsoleColor.DarkCyan;
                    //    Console.Write(player.Equipment.EquippedOffHand.Name); x += player.Equipment.EquippedOffHand.Name.Length;
                    //    Console.ForegroundColor = ConsoleColor.White; Console.SetCursorPosition(x, y); Console.Write(":"); x += 2;
                    //    switch (player.Equipment.EquippedOffHand.OHType)
                    //    {
                    //        case OffHandType.PowerFist:
                    //            Console.ForegroundColor = ConsoleColor.Gray; Console.SetCursorPosition(x, y); Console.Write("Damage Bonus:"); x += 14;
                    //            Console.ForegroundColor = player.Equipment.EquippedOffHand.OHBonus > 0 ? ConsoleColor.Green : ConsoleColor.Red;
                    //            Console.SetCursorPosition(x, y); Console.Write(player.Equipment.EquippedOffHand.OHBonus);
                    //            x += player.Equipment.EquippedOffHand.OHBonus.ToString().Length + 3;
                    //            break;
                    //        case OffHandType.ArmGuard:
                    //            Console.ForegroundColor = ConsoleColor.Gray; Console.SetCursorPosition(x, y); Console.Write("BLO%:"); x += 6;
                    //            Console.ForegroundColor = player.Equipment.EquippedOffHand.OHBonus > 0 ? ConsoleColor.Green : ConsoleColor.Red;
                    //            Console.SetCursorPosition(x, y); Console.Write(player.Equipment.EquippedOffHand.OHBonus);
                    //            x += player.Equipment.EquippedOffHand.OHBonus.ToString().Length + 3;
                    //            break;
                    //        case OffHandType.Tome:
                    //            Console.ForegroundColor = ConsoleColor.Gray; Console.SetCursorPosition(x, y); Console.Write("Skill Damage:"); x += 14;
                    //            Console.ForegroundColor = player.Equipment.EquippedOffHand.OHBonus > 0 ? ConsoleColor.Green : ConsoleColor.Red;
                    //            Console.SetCursorPosition(x, y); Console.Write("{0}{1}", player.Equipment.EquippedOffHand.OHBonus > 0 ? "+" : "-",
                    //                player.Equipment.EquippedOffHand.OHBonus > 0 ? player.Equipment.EquippedOffHand.OHBonus : (player.Equipment.EquippedOffHand.OHBonus * -1));
                    //            x += player.Equipment.EquippedOffHand.OHBonus.ToString().Length + 4;
                    //            break;
                    //        case OffHandType.Orb:
                    //            Console.ForegroundColor = ConsoleColor.Gray; Console.SetCursorPosition(x, y); Console.Write("RES%:"); x += 6;
                    //            Console.ForegroundColor = player.Equipment.EquippedOffHand.OHBonus > 0 ? ConsoleColor.Green : ConsoleColor.Red;
                    //            Console.SetCursorPosition(x, y); Console.Write(player.Equipment.EquippedOffHand.OHBonus);
                    //            x += player.Equipment.EquippedOffHand.OHBonus.ToString().Length + 3;
                    //            break;
                    //        case OffHandType.Powder:
                    //            Console.ForegroundColor = ConsoleColor.Gray; Console.SetCursorPosition(x, y); Console.Write("DOD%:"); x += 6;
                    //            Console.ForegroundColor = player.Equipment.EquippedOffHand.OHBonus > 0 ? ConsoleColor.Green : ConsoleColor.Red;
                    //            Console.SetCursorPosition(x, y); Console.Write(player.Equipment.EquippedOffHand.OHBonus);
                    //            x += player.Equipment.EquippedOffHand.OHBonus.ToString().Length + 3;
                    //            break;
                    //        case OffHandType.Lasso:
                    //            Console.ForegroundColor = ConsoleColor.Gray; Console.SetCursorPosition(x, y); Console.Write("HIT%:"); x += 6;
                    //            Console.ForegroundColor = player.Equipment.EquippedOffHand.OHBonus > 0 ? ConsoleColor.Green : ConsoleColor.Red;
                    //            Console.SetCursorPosition(x, y); Console.Write(player.Equipment.EquippedOffHand.OHBonus);
                    //            x += player.Equipment.EquippedOffHand.OHBonus.ToString().Length + 3;
                    //            break;
                    //        case OffHandType.None:
                    //            break;
                    //    }
                    //    if (player.Equipment.EquippedOffHand.OHResourceBonus != 0)
                    //    {
                    //        Console.ForegroundColor = ConsoleColor.Gray; Console.SetCursorPosition(x, y); Console.Write("{0}:", resource); x += resource.Length + 2;
                    //        Console.ForegroundColor = player.Equipment.EquippedOffHand.OHResourceBonus > 0 ? ConsoleColor.Green : ConsoleColor.Red;
                    //        Console.SetCursorPosition(x, y); Console.Write("{0}", player.Equipment.EquippedOffHand.OHResourceBonus);
                    //        x += player.Equipment.EquippedOffHand.OHResourceBonus.ToString().Length + 3;
                    //    }
                    //    if (player.Equipment.EquippedOffHand.Attributes[0] != 0)
                    //    {
                    //        Console.ForegroundColor = ConsoleColor.Gray; Console.SetCursorPosition(x, y); Console.Write("STR:"); x += 5;
                    //        Console.ForegroundColor = player.Equipment.EquippedOffHand.Attributes[0] > 0 ? ConsoleColor.Green : ConsoleColor.Red;
                    //        Console.SetCursorPosition(x, y); Console.Write("{0}", player.PrimaryAttribute != Attributes.Strength ?
                    //            player.Equipment.EquippedOffHand.Attributes[0] : (player.Equipment.EquippedOffHand.Attributes[0] + 
                    //            player.Equipment.EquippedOffHand.OHPrimaryBonus));
                    //        x += player.PrimaryAttribute != Attributes.Strength ? player.Equipment.EquippedOffHand.Attributes[0].ToString().Length + 3 : 
                    //            (player.Equipment.EquippedOffHand.Attributes[0] + player.Equipment.EquippedOffHand.OHPrimaryBonus).ToString().Length + 3;
                    //    }
                    //    if (player.Equipment.EquippedOffHand.Attributes[1] != 0)
                    //    {
                    //        Console.ForegroundColor = ConsoleColor.Gray; Console.SetCursorPosition(x, y); Console.Write("DEF:"); x += 5;
                    //        Console.ForegroundColor = player.Equipment.EquippedOffHand.Attributes[1] > 0 ? ConsoleColor.Green : ConsoleColor.Red;
                    //        Console.SetCursorPosition(x, y); Console.Write("{0}", player.PrimaryAttribute != Attributes.Defense ?
                    //            player.Equipment.EquippedOffHand.Attributes[1] : (player.Equipment.EquippedOffHand.Attributes[1] + 
                    //            player.Equipment.EquippedOffHand.OHPrimaryBonus));
                    //        x += player.PrimaryAttribute != Attributes.Defense ? player.Equipment.EquippedOffHand.Attributes[1].ToString().Length + 3 :
                    //            (player.Equipment.EquippedOffHand.Attributes[1] + player.Equipment.EquippedOffHand.OHPrimaryBonus).ToString().Length + 3;
                    //    }
                    //    if (player.Equipment.EquippedOffHand.Attributes[2] != 0)
                    //    {
                    //        Console.ForegroundColor = ConsoleColor.Gray; Console.SetCursorPosition(x, y); Console.Write("INT:"); x += 5;
                    //        Console.ForegroundColor = player.Equipment.EquippedOffHand.Attributes[2] > 0 ? ConsoleColor.Green : ConsoleColor.Red;
                    //        Console.SetCursorPosition(x, y); Console.Write("{0}", player.PrimaryAttribute != Attributes.Intelligence ?
                    //            player.Equipment.EquippedOffHand.Attributes[2] : (player.Equipment.EquippedOffHand.Attributes[2] +
                    //            player.Equipment.EquippedOffHand.OHPrimaryBonus));
                    //        x += player.PrimaryAttribute != Attributes.Intelligence ? player.Equipment.EquippedOffHand.Attributes[2].ToString().Length + 3 :
                    //            (player.Equipment.EquippedOffHand.Attributes[2] + player.Equipment.EquippedOffHand.OHPrimaryBonus).ToString().Length + 3;
                    //    }
                    //    if (player.Equipment.EquippedOffHand.Attributes[3] != 0)
                    //    {
                    //        Console.ForegroundColor = ConsoleColor.Gray; Console.SetCursorPosition(x, y); Console.Write("WIS:"); x += 5;
                    //        Console.ForegroundColor = player.Equipment.EquippedOffHand.Attributes[3] > 0 ? ConsoleColor.Green : ConsoleColor.Red;
                    //        Console.SetCursorPosition(x, y); Console.Write("{0}", player.PrimaryAttribute != Attributes.Wisdom ?
                    //            player.Equipment.EquippedOffHand.Attributes[3] : (player.Equipment.EquippedOffHand.Attributes[3] +
                    //            player.Equipment.EquippedOffHand.OHPrimaryBonus));
                    //        x += player.PrimaryAttribute != Attributes.Wisdom ? player.Equipment.EquippedOffHand.Attributes[3].ToString().Length + 3 :
                    //            (player.Equipment.EquippedOffHand.Attributes[3] + player.Equipment.EquippedOffHand.OHPrimaryBonus).ToString().Length + 3;
                    //    }
                    //    if (player.Equipment.EquippedOffHand.Attributes[4] != 0)
                    //    {
                    //        Console.ForegroundColor = ConsoleColor.Gray; Console.SetCursorPosition(x, y); Console.Write("DEX:"); x += 5;
                    //        Console.ForegroundColor = player.Equipment.EquippedOffHand.Attributes[4] > 0 ? ConsoleColor.Green : ConsoleColor.Red;
                    //        Console.SetCursorPosition(x, y); Console.Write("{0}", player.PrimaryAttribute != Attributes.Dexterity ?
                    //            player.Equipment.EquippedOffHand.Attributes[4] : (player.Equipment.EquippedOffHand.Attributes[4] +
                    //            player.Equipment.EquippedOffHand.OHPrimaryBonus));
                    //        x += player.PrimaryAttribute != Attributes.Dexterity ? player.Equipment.EquippedOffHand.Attributes[4].ToString().Length + 3 :
                    //            (player.Equipment.EquippedOffHand.Attributes[4] + player.Equipment.EquippedOffHand.OHPrimaryBonus).ToString().Length + 3;
                    //    }
                    //    if (player.Equipment.EquippedOffHand.Attributes[5] != 0)
                    //    {
                    //        Console.ForegroundColor = ConsoleColor.Gray; Console.SetCursorPosition(x, y); Console.Write("AGI:"); x += 5;
                    //        Console.ForegroundColor = player.Equipment.EquippedOffHand.Attributes[5] > 0 ? ConsoleColor.Green : ConsoleColor.Red;
                    //        Console.SetCursorPosition(x, y); Console.Write("{0}", player.PrimaryAttribute != Attributes.Agility ?
                    //            player.Equipment.EquippedOffHand.Attributes[5] : (player.Equipment.EquippedOffHand.Attributes[5] +
                    //            player.Equipment.EquippedOffHand.OHPrimaryBonus));
                    //        x += player.PrimaryAttribute != Attributes.Agility ? player.Equipment.EquippedOffHand.Attributes[5].ToString().Length + 3 :
                    //            (player.Equipment.EquippedOffHand.Attributes[5] + player.Equipment.EquippedOffHand.OHPrimaryBonus).ToString().Length + 3;
                    //    }
                    //    if (player.Equipment.EquippedHead.Attributes[6] != 0)
                    //    {
                    //        Console.ForegroundColor = ConsoleColor.Gray; Console.SetCursorPosition(x, y); Console.Write("{0}:",
                    //            player.ResourceAttribute); x += 2 + player.ResourceAttribute.ToString().Length;
                    //        Console.ForegroundColor = player.Equipment.EquippedHead.Attributes[6] > 0 ? ConsoleColor.Green : ConsoleColor.Red;
                    //        Console.SetCursorPosition(x, y); Console.Write("{0}", player.Equipment.EquippedHead.Attributes[6]);
                    //        x += player.Equipment.EquippedHead.Attributes[6].ToString().Length + 3;
                    //    }
                    //}
                    //else
                    //{
                    //    Console.SetCursorPosition(x, y); Console.ForegroundColor = ConsoleColor.White; Console.Write("//Off Hand:"); x += 12;
                    //    Console.SetCursorPosition(x, y); Console.ForegroundColor = ConsoleColor.DarkGray; Console.Write("Not Equipped");
                    //}
                    //#endregion      
                    //#region Accessory
                    //x = 7; y = 21; if (player.Equipment.EquippedAccessory.Name != "")
                    //{
                    //    Console.SetCursorPosition(x, y); Console.ForegroundColor = ConsoleColor.White; Console.Write("//"); x += 2;
                    //    Console.SetCursorPosition(x, y); Console.ForegroundColor = ConsoleColor.DarkCyan;
                    //    Console.Write(player.Equipment.EquippedAccessory.Name); x += player.Equipment.EquippedAccessory.Name.Length;
                    //    Console.ForegroundColor = ConsoleColor.White; Console.SetCursorPosition(x, y); Console.Write(":"); x += 2;
                    //    if (player.Equipment.EquippedAccessory.MaxLifeMod != 0)
                    //    {
                    //        Console.ForegroundColor = ConsoleColor.Gray; Console.SetCursorPosition(x, y); Console.Write("Life:"); x += 6;
                    //        Console.ForegroundColor = player.Equipment.EquippedAccessory.MaxLifeMod > 0 ? ConsoleColor.Green : ConsoleColor.Red;
                    //        Console.SetCursorPosition(x, y); Console.Write(player.Equipment.EquippedAccessory.MaxLifeMod);
                    //        x += player.Equipment.EquippedAccessory.MaxLifeMod.ToString().Length + 3;
                    //    }
                    //    if (player.Equipment.EquippedAccessory.MaxResourceMod != 0)
                    //    {
                    //        Console.ForegroundColor = ConsoleColor.Gray; Console.SetCursorPosition(x, y); Console.Write("{0}:", resource); x += resource.Length + 2;
                    //        Console.ForegroundColor = player.Equipment.EquippedAccessory.MaxResourceMod > 0 ? ConsoleColor.Green : ConsoleColor.Red;
                    //        Console.SetCursorPosition(x, y); Console.Write(player.Equipment.EquippedAccessory.MaxResourceMod);
                    //        x += player.Equipment.EquippedAccessory.MaxResourceMod.ToString().Length + 3;
                    //    }
                    //    if (player.Equipment.EquippedAccessory.Attributes[0] != 0)
                    //    {
                    //        Console.ForegroundColor = ConsoleColor.Gray; Console.SetCursorPosition(x, y); Console.Write("STR:"); x += 5;
                    //        Console.ForegroundColor = player.Equipment.EquippedAccessory.Attributes[0] > 0 ? ConsoleColor.Green : ConsoleColor.Red;
                    //        Console.SetCursorPosition(x, y); Console.Write(player.Equipment.EquippedAccessory.Attributes[0]);
                    //        x += player.Equipment.EquippedAccessory.Attributes[0].ToString().Length + 3;
                    //    }
                    //    if (player.Equipment.EquippedAccessory.Attributes[1] != 0)
                    //    {
                    //        Console.ForegroundColor = ConsoleColor.Gray; Console.SetCursorPosition(x, y); Console.Write("DEF:"); x += 5;
                    //        Console.ForegroundColor = player.Equipment.EquippedAccessory.Attributes[1] > 0 ? ConsoleColor.Green : ConsoleColor.Red;
                    //        Console.SetCursorPosition(x, y); Console.Write(player.Equipment.EquippedAccessory.Attributes[1]);
                    //        x += player.Equipment.EquippedAccessory.Attributes[1].ToString().Length + 3;
                    //    }
                    //    if (player.Equipment.EquippedAccessory.Attributes[2] != 0)
                    //    {
                    //        Console.ForegroundColor = ConsoleColor.Gray; Console.SetCursorPosition(x, y); Console.Write("INT:"); x += 5;
                    //        Console.ForegroundColor = player.Equipment.EquippedAccessory.Attributes[2] > 0 ? ConsoleColor.Green : ConsoleColor.Red;
                    //        Console.SetCursorPosition(x, y); Console.Write(player.Equipment.EquippedAccessory.Attributes[2]);
                    //        x += player.Equipment.EquippedAccessory.Attributes[2].ToString().Length + 3;
                    //    }
                    //    if (player.Equipment.EquippedAccessory.Attributes[3] != 0)
                    //    {
                    //        Console.ForegroundColor = ConsoleColor.Gray; Console.SetCursorPosition(x, y); Console.Write("WIS:"); x += 5;
                    //        Console.ForegroundColor = player.Equipment.EquippedAccessory.Attributes[3] > 0 ? ConsoleColor.Green : ConsoleColor.Red;
                    //        Console.SetCursorPosition(x, y); Console.Write(player.Equipment.EquippedAccessory.Attributes[3]);
                    //        x += player.Equipment.EquippedAccessory.Attributes[3].ToString().Length + 3;
                    //    }
                    //    if (player.Equipment.EquippedAccessory.Attributes[4] != 0)
                    //    {
                    //        Console.ForegroundColor = ConsoleColor.Gray; Console.SetCursorPosition(x, y); Console.Write("DEX:"); x += 5;
                    //        Console.ForegroundColor = player.Equipment.EquippedAccessory.Attributes[4] > 0 ? ConsoleColor.Green : ConsoleColor.Red;
                    //        Console.SetCursorPosition(x, y); Console.Write(player.Equipment.EquippedAccessory.Attributes[4]);
                    //        x += player.Equipment.EquippedAccessory.Attributes[4].ToString().Length + 3;
                    //    }
                    //    if (player.Equipment.EquippedAccessory.Attributes[5] != 0)
                    //    {
                    //        Console.ForegroundColor = ConsoleColor.Gray; Console.SetCursorPosition(x, y); Console.Write("AGI:"); x += 5;
                    //        Console.ForegroundColor = player.Equipment.EquippedAccessory.Attributes[5] > 0 ? ConsoleColor.Green : ConsoleColor.Red;
                    //        Console.SetCursorPosition(x, y); Console.Write(player.Equipment.EquippedAccessory.Attributes[5]);
                    //        x += player.Equipment.EquippedAccessory.Attributes[5].ToString().Length + 3;
                    //    }
                    //    if (player.Equipment.EquippedHead.Attributes[6] != 0)
                    //    {
                    //        Console.ForegroundColor = ConsoleColor.Gray; Console.SetCursorPosition(x, y); Console.Write("{0}:",
                    //            player.ResourceAttribute); x += 2 + player.ResourceAttribute.ToString().Length;
                    //        Console.ForegroundColor = player.Equipment.EquippedHead.Attributes[6] > 0 ? ConsoleColor.Green : ConsoleColor.Red;
                    //        Console.SetCursorPosition(x, y); Console.Write("{0}", player.Equipment.EquippedHead.Attributes[6]);
                    //        x += player.Equipment.EquippedHead.Attributes[6].ToString().Length + 3;
                    //    }
                    //}
                    //else
                    //{
                    //    Console.SetCursorPosition(x, y); Console.ForegroundColor = ConsoleColor.White; Console.Write("//Accessory:"); x += 13;
                    //    Console.SetCursorPosition(x, y); Console.ForegroundColor = ConsoleColor.DarkGray; Console.Write("Not Equipped");
                    //}
                    //#endregion
                    #endregion
                    break;
                case 3:
                    Console.SetCursorPosition(x - 2, y);
                    Console.Write("Skills Menu\\\\");
                    break;
                case 4:
                    Console.SetCursorPosition(x - 5, y);
                    Console.Write("Inventory Menu\\\\");
                    break;
            }
        }

    }


}
