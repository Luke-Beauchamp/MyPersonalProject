using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MenuLibrary;
using NPCLibrary;
using CharacterLibrary;
using PlayerLibrary;
using MonsterLibrary;
using ItemLibrary;
using EquipmentLibrary;

namespace SceneLibrary
{
    public class SceneSocial
    {
        public static NPC CreateNPC(Player player)
        {
            //Initialize Private Variables
            Random rand = new Random();
            int roll;
            bool loop = false;
            int x = 0;
            int y = 0;
            int z = 0;
            //Randomize gender
            roll = rand.Next(0, 2);
            string gender = roll == 0 ? "m" : "f";
            string name = GetName(gender);
            //Initialize Variables
            Emotion primary = Emotion.Null;
            Emotion secondary = Emotion.Null;
            Emotion tertiary = Emotion.Null;

            //TEMPORARY VARIABLES
            //END

            //Fix player.Emotional if all are false
            foreach (bool emotionIs in player.Emotional)
            {
                x += emotionIs ? 0 : 1;
            }
            player.Emotional = x == 8 ? new bool[] { true, true, true, true, true, true, true, true } : player.Emotional;
            //Randomize until Primary selected
            loop = true;
            do
            {
                roll = rand.Next(0, 8);
                if (player.Emotional[roll])
                {
                    switch (roll)
                    {
                        case 0:
                            primary = Emotion.Anger;
                            break;
                        case 1:
                            primary = Emotion.Anticipation;
                            break;
                        case 2:
                            primary = Emotion.Disgust;
                            break;
                        case 3:
                            primary = Emotion.Fear;
                            break;
                        case 4:
                            primary = Emotion.Joy;
                            break;
                        case 5:
                            primary = Emotion.Sadness;
                            break;
                        case 6:
                            primary = Emotion.Surprise;
                            break;
                        case 7:
                            primary = Emotion.Trust;
                            break;
                    }
                    player.Emotional[roll] = false;
                    x = roll;
                    loop = false;
                }
            } while (loop);
            //Randomize until Secondary selected
            loop = true;
            do
            {
                roll = rand.Next(0, 8);
                if (roll != x)
                {
                    switch (roll)
                    {
                        case 0:
                            secondary = Emotion.Anger;
                            break;
                        case 1:
                            secondary = Emotion.Anticipation;
                            break;
                        case 2:
                            secondary = Emotion.Disgust;
                            break;
                        case 3:
                            secondary = Emotion.Fear;
                            break;
                        case 4:
                            secondary = Emotion.Joy;
                            break;
                        case 5:
                            secondary = Emotion.Sadness;
                            break;
                        case 6:
                            secondary = Emotion.Surprise;
                            break;
                        case 7:
                            secondary = Emotion.Trust;
                            break;
                    }
                    y = roll;
                    loop = false;
                }
            } while (loop);
            //Randomize until Tertiary selected
            loop = true;
            do
            {
                roll = rand.Next(0, 8);
                if (roll != x & roll != y)
                {
                    switch (roll)
                    {
                        case 0:
                            tertiary = primary != Emotion.Anger & secondary != Emotion.Anger ? Emotion.Anger : Emotion.Anticipation;
                            break;
                        case 1:
                            tertiary = primary != Emotion.Anticipation ? Emotion.Anticipation : Emotion.Disgust;
                            break;
                        case 2:
                            tertiary = primary != Emotion.Disgust ? Emotion.Disgust : Emotion.Fear;
                            break;
                        case 3:
                            tertiary = primary != Emotion.Fear ? Emotion.Fear : Emotion.Joy;
                            break;
                        case 4:
                            tertiary = primary != Emotion.Joy ? Emotion.Joy : Emotion.Sadness;
                            break;
                        case 5:
                            tertiary = primary != Emotion.Sadness ? Emotion.Sadness : Emotion.Surprise;
                            break;
                        case 6:
                            tertiary = primary != Emotion.Surprise ? Emotion.Surprise : Emotion.Trust;
                            break;
                        case 7:
                            tertiary = primary != Emotion.Trust ? Emotion.Trust : Emotion.Anger;
                            break;
                    }
                    z = roll;
                    loop = false;
                }
            } while (loop);

            string[,] dialogue = GetDialogue(player);

            NPC npc = new NPC(name, primary, secondary, tertiary, dialogue);
            //NPC npc = new NPC(primary, secondary, tertiary, dialogue, name, new bool[17], 0, 0, 0, 0, 0, Attributes.Agility, 0, 0, 0, 0, 0, 0, 0);
            npc.Monster = GetMonster(player.Floor);
            npc.Monster.Define(rand.Next(2, 4), player.Floor);


            return npc;
        }

        public static void DiceVendor(Player player)
        {
            MenuDoes.DrawMenu();
            MenuDoes.CommandClear();
            MenuDoes.CommandToggle(0, player.zToggle, player);
            DrawScene(player);//Consider fixing this taking both....
            CharacterInfo.ClearArea();
            CharacterInfo.ShortAttributes(player);
            Scene.SceneClear();
            int x;
            int y;
            Console.SetCursorPosition(x = 7, y = 8);
            Console.Write("Your {0} dice shards have been turned into {1} dice.",
                player.DiceShards, player.DiceShards / 6);
            DrawInput(new string[] { "//Continue" }, 0);
            SceneBattle.PressEnter();
            bool loop = true;
            int selector = 0;
            string[] options = new string[6] { "Strength", "Defense", "Intelligence", "Wisdom", "Dexterity", "Agiliy" };
            int dice = player.DiceShards / 6;
            if (dice == 0)
            {
                Console.SetCursorPosition(7, 10); Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Insufficient dice! Good luck!");
            }
            else
            {
                player.DiceShards = player.DiceShards % 6;
                string selection = "";
                int amount = 0;
                string[] description = new string[] {
                    "STR increases aDMG             ",
                    "DEF reduces recieved aDMG      ",
                    "INT increases STA% and +Heal   ",
                    "WIS increases RES% and +Shield ",
                    "DEX increases HIT%             ",
                    "AGI increases DOD%             "};
                if (player.Floor <= 2)
                {
                    description = new string[] {
                        "Strength increases attack damage (aDMG)                                   ",
                        "Defense reduces recieved attack damage (aDMG)                             ",
                        "Intelligence increases status effect chances (STA%) and heals (+Heal)     ",
                        "Wisdom increases status effect resist chances (RES%) and shields (+Shield)",
                        "Dexterity increases attack hit chance (HIT%)                              ",
                        "Agility increases attack dodge chance (DOD%)                              " };
                }
                do
                {
                    Console.SetCursorPosition(x = 7, y = 10);
                    Console.ForegroundColor = ConsoleColor.Green; Console.Write("{0}", dice);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(" dice remaining.  Select which attribute to improve.                    ");
                    Console.SetCursorPosition(7, 11);
                    Console.Write(description[selector] + "                   ");
                    DrawInput(options, selector);
                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.Z:
                        case ConsoleKey.NumPad0:
                            CharacterInfo.ClearArea();
                            CharacterInfo.ShortSheet(player);
                            break;
                        case ConsoleKey.X:
                        case ConsoleKey.NumPad1:
                            CharacterInfo.ClearArea();
                            CharacterInfo.ShortAttributes(player);
                            break;
                        case ConsoleKey.C:
                        case ConsoleKey.NumPad3:
                            CharacterInfo.ClearArea();
                            CharacterInfo.ShortEquipment(player);
                            break;
                        case ConsoleKey.LeftArrow:
                        case ConsoleKey.A:
                        case ConsoleKey.NumPad4:
                            selector -= selector == 0 ? -5 : 1;
                            DrawInput(options, selector);
                            break;
                        case ConsoleKey.RightArrow:
                        case ConsoleKey.D:
                        case ConsoleKey.NumPad6:
                            selector += selector == 5 ? -5 : 1;
                            DrawInput(options, selector);
                            break;
                        case ConsoleKey.Enter:
                        case ConsoleKey.F:
                        case ConsoleKey.NumPad5:
                            Console.SetCursorPosition(7, 12);
                            Console.Write("                                                             ");
                            bool looped = true;
                            DrawInput(new string[] { "//Confirm " + options[selector] }, 0);
                            Random rand = new Random();
                            amount = rand.Next(1, 7);
                            do
                            {
                                switch (Console.ReadKey(true).Key)
                                {
                                    case ConsoleKey.Escape:
                                    case ConsoleKey.Subtract:
                                        looped = false;
                                        break;
                                    case ConsoleKey.F:
                                    case ConsoleKey.NumPad5:
                                        looped = false;
                                        switch (selector)
                                        {
                                            case 0:
                                                selection = "Strength";
                                                player.Strength += amount;
                                                break;
                                            case 1:
                                                selection = "Defense";
                                                player.Defense += amount;
                                                break;
                                            case 2:
                                                selection = "Intelligence";
                                                player.Intelligence += amount;
                                                break;
                                            case 3:
                                                selection = "Wisdom";
                                                player.Wisdom += amount;
                                                break;
                                            case 4:
                                                selection = "Dexterity";
                                                player.Dexterity += amount;
                                                break;
                                            case 5:
                                                selection = "Agility";
                                                player.Agility += amount;
                                                break;
                                        }
                                        CharacterInfo.ShortAttributes(player);
                                        dice--;
                                        Console.SetCursorPosition(7, 12);
                                        Console.Write("{0} increased by {1}!", selection, amount);
                                        if (dice == 0) { loop = false; }
                                        else { DrawInput(new string[] { "//Continue" }, 0); SceneBattle.PressEnter(); }
                                        break;
                                }
                            } while (looped);
                            break;
                    }
                } while (loop);
                Console.SetCursorPosition(7, 12); Console.Write("                                                        ");
                Console.SetCursorPosition(7, 11); Console.Write("{0} increased by {1}!                                           ", selection, amount);
                Console.SetCursorPosition(7, 10); Console.Write("No dice remaining                                       ");
            }
            DrawInput(new string[] { "//Continue" }, 0);
            SceneBattle.PressEnter();
            Console.Clear();
        }//IMprove

        public static string GetName(string gender)
        {
            int counter = 0;
            string preName = "";
            string midName = "";
            string endName = "";
            Random rand = new Random();
            int roll = 0;
            string names;

            //decide preName
            roll = rand.Next(1, 9);
            switch (roll)
            {
                case 1:
                    preName = gender == "m" ? "Alex " : "Alice ";
                    break;
                case 2:
                    preName = gender == "m" ? "Ander " : "Andrea ";
                    break;
                case 3:
                    preName = gender == "m" ? "Daryl " : "Darlene ";
                    break;
                case 4:
                    preName = gender == "m" ? "Justice " : "Justine ";
                    break;
                case 5:
                    preName = gender == "m" ? "Scott " : "Scarlet ";
                    break;
                case 6:
                    preName = gender == "m" ? "Seth " : "Selah ";
                    break;
                case 7:
                    preName = gender == "m" ? "Thomas " : "Tamara ";
                    break;
                case 8:
                    preName = gender == "m" ? "Frank " : "Filiberta ";
                    break;
            }
            //Decide midName
            roll = rand.Next(1, 9);
            switch (roll)
            {
                case 1:
                    midName = "Wilkon";
                    break;
                case 2:
                    midName = "Furten";
                    break;
                case 3:
                    midName = "Sapher";
                    break;
                case 4:
                    midName = "Bexton";
                    break;
                case 5:
                    midName = "Hayden";
                    break;
                case 6:
                    midName = "Zorion";
                    break;
                case 7:
                    midName = "Timmon";
                    break;
                case 8:
                    midName = "Broham";
                    break;
            }
            //Decide endName
            roll = rand.Next(1, 9);
            switch (roll)
            {
                case 1:
                    endName = "son";
                    break;
                case 2:
                    endName = "fer";
                    break;
                case 3:
                    endName = "kin";
                    break;
                case 4:
                    endName = "ent";
                    break;
                case 5:
                    endName = "lin";
                    break;
                case 6:
                    endName = "men";
                    break;
                case 7:
                    endName = "ski";
                    break;
                case 8:
                    endName = "wan";
                    break;
            }
            names = preName + midName + endName;
            counter++;


            return names;
        }

        public static Monster GetMonster(int floor)
        {
            Monster monster = new Monster(floor);
            int[] definitions = new int[] { 1, 1, 1, 1, 2, 2, 2, 3, 3, 4 };
            Random rand = new Random();
            int random = definitions[rand.Next(definitions.Length)];
            monster.Define(random, floor);
            return monster;
        }//TODO IMPROVE

        public static void Socialize(Player player, NPC npc)
        {
            int currentRapport = 0;
            int rapportMod = 0;
            int selector = 0;
            int rapportIndex = 0;
            bool combatOn = false;
            switch (npc.Primary)
            {
                case Emotion.Fear:
                    rapportIndex = 3;
                    currentRapport += player.Rapport[3];
                    selector = 3;
                    break;
                case Emotion.Trust:
                    rapportIndex = 7;
                    currentRapport = player.Rapport[7];
                    selector = 7;
                    break;
                case Emotion.Joy:
                    rapportIndex = 4;
                    currentRapport = player.Rapport[4];
                    selector = 4;
                    break;
                case Emotion.Anticipation:
                    rapportIndex = 1;
                    currentRapport = player.Rapport[1];
                    selector = 1;
                    break;
                case Emotion.Anger:
                    rapportIndex = 0;
                    currentRapport = player.Rapport[0];
                    selector = 0;
                    break;
                case Emotion.Disgust:
                    rapportIndex = 2;
                    currentRapport = player.Rapport[2];
                    selector = 2;
                    break;
                case Emotion.Sadness:
                    rapportIndex = 5;
                    currentRapport = player.Rapport[5];
                    selector = 5;
                    break;
                case Emotion.Surprise:
                    rapportIndex = 6;
                    currentRapport = player.Rapport[6];
                    selector = 6;
                    break;
            }//Modifies currentRapport to player's current rapport with npc.Primary
            DrawScene(player);
            int x = 5;
            int y = 8;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(x, y); Console.Write(npc.Name); x += npc.Name.Length;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(": " + npc.Dialogue[selector, 0]); //Show's first line for NPC

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.SetCursorPosition(5, 10); Console.Write(player.Name);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(5 + player.Name.Length, 10); Console.Write(": (how will I respond?)"); x = 7 + player.Name.Length; y = 10;

            //Need to show player responses to NPC... in DrawInput(NPC npc)
            //I need to randomize npc.Dialogue[selector, 1-3] to get the order of...
            Random rand = new Random();
            bool loop = true;
            List<string> playerLines = new List<string> { npc.Dialogue[selector, 1], npc.Dialogue[selector, 2], npc.Dialogue[selector, 3] };
            int[] impacts = new int[3];
            string[] playerChoice = new string[3];

            impacts[0] = rand.Next(1, 4);
            playerChoice[0] = npc.Dialogue[selector, impacts[0]];

            do
            {
                impacts[1] = rand.Next(1, 4);
                if (impacts[1] != impacts[0])
                {
                    playerChoice[1] = npc.Dialogue[selector, impacts[1]];
                    loop = false;
                }

            } while (loop);
            loop = true;
            do
            {
                impacts[2] = rand.Next(1, 4);
                if (impacts[2] != impacts[0] & impacts[2] != impacts[1])
                {
                    playerChoice[2] = npc.Dialogue[selector, impacts[2]];
                    loop = false;
                }
            } while (loop);


            DrawInput(playerChoice, 0);
            //END TEMP FILLER

            int selected = 0;
            loop = true;
            bool confirmed = false;
            do
            {
                DrawInput(playerChoice, selected);
                Console.SetCursorPosition(100, 2);
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.LeftArrow:
                    case ConsoleKey.A:
                    case ConsoleKey.NumPad4:
                        selected -= selected != 0 ? 1 : -(playerChoice.Length - 1);
                        break;
                    case ConsoleKey.RightArrow:
                    case ConsoleKey.D:
                    case ConsoleKey.NumPad6:
                        selected += selected != playerChoice.Length - 1 ? 1 : -(playerChoice.Length - 1);
                        break;
                    case ConsoleKey.Q://TODO: Will input "unknown" (to player) emotional response & Confirm
                    case ConsoleKey.NumPad7:
                        confirmed = Confirm(playerChoice[selected], player.EmotionColor(player.Primary));
                        if (confirmed)
                        {
                            Emotion temp = player.Primary;
                            rapportMod += player.Primary == npc.Primary ? 1 : player.Primary == npc.Opposite ? -1 : 0;
                            Console.ForegroundColor = player.EmotionColor(player.Primary);
                            Console.SetCursorPosition(x, y); Console.Write("                                          ");
                            Console.SetCursorPosition(x, y); Console.Write(playerChoice[selected]);
                            player.Current = temp;
                            //Check that choice against npc.Dialogue
                            loop = false;
                        }
                        break;
                    case ConsoleKey.E://TODO: Will input "unknown" (to player) emotional response & Confirm
                    case ConsoleKey.Divide:
                        confirmed = Confirm(playerChoice[selected], player.EmotionColor(player.Secondary));
                        if (confirmed)
                        {
                            rapportMod += player.Secondary == npc.Primary ? 1 : player.Secondary == npc.Opposite ? -1 : 0;
                            Console.ForegroundColor = player.EmotionColor(player.Secondary);
                            Console.SetCursorPosition(x, y); Console.Write("                                          ");
                            Console.SetCursorPosition(x, y); Console.Write(playerChoice[selected]);
                            Emotion tamp = player.Secondary;
                            player.Current = tamp;
                            //Check that choice against npc.Dialogue
                            loop = false;
                        }
                        break;
                    case ConsoleKey.R://TODO: Will input "unknown" (to player) emotional response & Confirm
                    case ConsoleKey.NumPad9:
                        confirmed = Confirm(playerChoice[selected], player.EmotionColor(player.Tertiary));
                        if (confirmed)
                        {
                            Console.ForegroundColor = player.EmotionColor(player.Tertiary);
                            Console.SetCursorPosition(x, y); Console.Write("                                          ");
                            Console.SetCursorPosition(x, y); Console.Write(playerChoice[selected]);
                            rapportMod += player.Tertiary == npc.Primary ? 1 : player.Tertiary == npc.Opposite ? -1 : 0;
                            Emotion tomp = player.Tertiary;
                            player.Current = tomp;
                            loop = false;
                        }
                        break;
                    case ConsoleKey.Enter://Will Confirm Selection
                    case ConsoleKey.F:
                    case ConsoleKey.NumPad5:
                        confirmed = Confirm(playerChoice[selected], ConsoleColor.White);
                        if (confirmed)
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.SetCursorPosition(x, y); Console.Write("                                          ");
                            Console.SetCursorPosition(x, y); Console.Write(playerChoice[selected]);
                            //Check that choice against npc.Dialogue
                            loop = false;
                        }
                        break;
                }

            } while (loop);
            DrawInput(new string[] { "//Select Continue" }, 0);
            SceneBattle.PressEnter();
            x = 5; y += 2;
            Console.SetCursorPosition(x, y); Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(npc.Name); x += npc.Name.Length;
            Console.SetCursorPosition(x, y); Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(":"); x += 2; Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(x, y); Console.Write("{0}", npc.Dialogue[selector, 3 + impacts[selected]]);
            SceneBattle.PressEnter();
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(x, y + 1); Console.Write("!");
            currentRapport += impacts[selected] == 1 ? 1 : impacts[selected] == 2 ? 0 : -1;
            rapportMod += impacts[selected] == 1 ? 1 : impacts[selected] == 2 ? 0 : -1;
            Console.ForegroundColor = rapportMod >= 1 ? ConsoleColor.Green : rapportMod == 0 ? ConsoleColor.Gray : ConsoleColor.Red;
            player.Rapport[rapportIndex] += rapportMod;
            Console.SetCursorPosition(x + 1, y + 1); Console.Write("Rapport {0}", rapportMod == 2 ? "increased+" : rapportMod == 1 ? "increased" : rapportMod == 0 ? "unaffected" :
                rapportMod == -1 ? "decreased" : "decreased-");
            SceneBattle.PressEnter();
            Console.SetCursorPosition(5, y + 3);
            switch (impacts[selected])
            {
                case 1://Put Equipment Sheet Equipment WriteLines into Methods to be able to write Equipment info here. And give choice
                    Console.ForegroundColor = ConsoleColor.Gray;//GetItem(int rapport)
                    Console.Write("{0} hands you a piece of equipment.", npc.Name);//Here I need to give the player a new item!
                    //Console.ForegroundColor = ConsoleColor.DarkCyan; Console.Write(equipment.Name + "!");
                    SceneBattle.PressEnter();
                    CharacterInfo.ClearArea();
                    CharacterInfo.ShortAttributes(player);
                    GetItem(player.Rapport[rapportIndex], player, 8, y + 4);
                    //Attempting to make a selection loop for picking New: or Old:

                    break;
                case 2:
                    Console.ForegroundColor = ConsoleColor.Gray; Console.Write("*{0} averts their attention elsewhere*", npc.Name);
                    break;
                case 3:
                    Console.ForegroundColor = ConsoleColor.Gray; Console.Write("*{0} hastily leaves the room*", npc.Name);
                    SceneBattle.PressEnter();
                    Console.SetCursorPosition(5, y + 3); Console.Write("                                                                 ");
                    Console.SetCursorPosition(5, y + 3); Console.ForegroundColor = ConsoleColor.Gray; Console.Write("*A");
                    Console.SetCursorPosition(8, y + 3); Console.ForegroundColor = ConsoleColor.Red; Console.Write("{0}", npc.Monster.Name);
                    Console.SetCursorPosition(9 + npc.Monster.Name.Length, y + 3); Console.ForegroundColor = ConsoleColor.Gray; Console.Write("enters the room*");
                    SceneBattle.PressEnter();
                    Console.SetCursorPosition(4, y + 4); Console.ForegroundColor = ConsoleColor.White; Console.Write("!");
                    Console.SetCursorPosition(26, y + 4); Console.Write("!");
                    Console.SetCursorPosition(5, y + 4); Console.ForegroundColor = ConsoleColor.Red; Console.Write("You're being attacked");
                    DrawInput(new string[] { "//Enter Combat" }, 0); combatOn = true;
                    break;
            }

            loop = true;
            if (!combatOn)
            {
                DrawInput(new string[] { "//Leave Room" }, 0);
                //SceneCombat.Battle(npc.Monster);
            }

            do
            {
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.Enter:
                    case ConsoleKey.F:
                    case ConsoleKey.NumPad5:
                        loop = false;
                        Console.Clear();
                        if (combatOn)
                        {
                            SceneBattle.DrawDisplay(player, npc.Monster);
                            SceneBattle.BattleLoop(player, npc.Monster);
                        }
                        break;
                }
            } while (loop);

        }

        public static void GetItem(int rapport, Player player, int xCursor, int yCursor)
        {//0: Accessory, 1: Amulet, 2: Head, 3: OffHand, 4: Outfit, 5: Weapon
            Random rand = new Random();
            int random;
            bool loopOn = true;
            int not0 = player.Itemable[0];
            int not1 = player.Itemable[1];
            MenuDoes.CommandClear();
            MenuDoes.CommandToggle(0, player.zToggle, player);
            do
            {
                random = rand.Next(6);
                if (random != not0 & random != not1 & random != player.Itemable[2])
                {
                    loopOn = false;
                    player.Itemable[2] = not1;
                    player.Itemable[1] = not0;
                    player.Itemable[0] = random;
                }
            } while (loopOn);
            string name = ""; //Randomize this to use in switches to give items 'unique' names // Give "of BLANK" names to an array and randomly pick... for now
            int tempResource = player.Resource;
            int tempMax = player.FindResource();
            int tempLife = player.Life;
            int tempMaxLife = player.FindLife();
            Equipment equipment = new Equipment();
            Console.SetCursorPosition(xCursor, yCursor + 1);
            string[] randNames = {"of Towers", "of Trembling", "of Certainty", "of Conflict", "of Presence", "of Whimsy", "of Nimbleness", "of Fumbling", "of Rage",
            "of Staggering", "of Resilience", "of Fading", "of Relishing", "of Sinking", "of Readiness", "of Unsteadiness"};
            name = randNames[rand.Next(16)];
            Head headOld = new Head();
            Weapon weaponOld = new Weapon();
            OffHand offHandOld = new OffHand();
            Amulet amuletOld = new Amulet();
            Outfit outfitOld = new Outfit();
            Accessory accessoryOld = new Accessory();
            switch (random)
            {
                case 0://Head
                    headOld = player.Equipment.EquippedHead;
                    equipment = headOld;
                    player.Write(equipment, 1);
                    Head head = new Head(name, rapport, player.Floor);
                    player.Equipment.EquippedHead = head;
                    Console.SetCursorPosition(xCursor, yCursor);
                    player.Write(head, 1);
                    break;
                case 1://Weapon
                    weaponOld = player.Equipment.EquippedWeapon;
                    equipment = weaponOld;
                    player.Write(equipment, 1);
                    Weapon weapon = new Weapon(name, rapport, player.Floor);
                    player.Equipment.EquippedWeapon = weapon;
                    Console.SetCursorPosition(xCursor, yCursor);
                    player.Write(weapon, 1);
                    break;
                case 2://OffHand
                    offHandOld = player.Equipment.EquippedOffHand;
                    equipment = offHandOld;
                    player.Write(equipment, 1);
                    OffHand offHand = new OffHand(name, rapport, player.Floor);
                    player.Equipment.EquippedOffHand = offHand;
                    Console.SetCursorPosition(xCursor, yCursor);
                    player.Write(offHand, 1);
                    break;
                case 3://Amulet
                    amuletOld = player.Equipment.EquippedAmulet;
                    equipment = amuletOld;
                    player.Write(equipment, 1);
                    Amulet amulet = new Amulet(name, rapport, player.Floor);
                    player.Equipment.EquippedAmulet = amulet;
                    Console.SetCursorPosition(xCursor, yCursor);
                    player.Write(amulet, 1);
                    break;
                case 4://Outfit
                    outfitOld = player.Equipment.EquippedOutfit;
                    equipment = outfitOld;
                    player.Write(equipment, 1);
                    Outfit outfit = new Outfit(name, rapport, player.Floor);
                    player.Equipment.EquippedOutfit = outfit;
                    Console.SetCursorPosition(xCursor, yCursor);
                    player.Write(outfit, 1);
                    break;
                case 5://Accessory
                    accessoryOld = player.Equipment.EquippedAccessory;
                    equipment = accessoryOld;
                    player.Write(equipment, 1);
                    Accessory accessory = new Accessory(name, rapport, player.Floor);
                    player.Equipment.EquippedAccessory = accessory;
                    Console.SetCursorPosition(xCursor, yCursor);
                    player.Write(accessory, 1);
                    break;
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(xCursor - 3, yCursor); Console.Write("NEW: ");
            Console.SetCursorPosition(xCursor - 3, yCursor + 1); Console.Write("OLD: ");
            bool loop = true;
            int selected = 0;
            string[] selectEq = new string[2] { "//Select \"NEW\"", "//Select \"OLD\"" };
            DrawInput(selectEq, selected);
            do
            {
                Console.ForegroundColor = selected == 0 ? ConsoleColor.Green : ConsoleColor.White;
                Console.SetCursorPosition(xCursor - 3, yCursor); Console.Write("NEW");
                Console.ForegroundColor = selected == 1 ? ConsoleColor.Green : ConsoleColor.White;
                Console.SetCursorPosition(xCursor - 3, yCursor + 1); Console.Write("OLD");
                DrawInput(selectEq, selected);
                Console.SetCursorPosition(100, 2);
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.Z:
                    case ConsoleKey.NumPad0:
                        CharacterInfo.ClearArea();
                        CharacterInfo.ShortSheet(player);
                        break;
                    case ConsoleKey.X:
                    case ConsoleKey.NumPad1:
                        CharacterInfo.ClearArea();
                        CharacterInfo.ShortAttributes(player);
                        break;
                    case ConsoleKey.C:
                    case ConsoleKey.NumPad3:
                        CharacterInfo.ClearArea();
                        CharacterInfo.ShortEquipment(player);
                        break;
                    case ConsoleKey.Tab:
                    case ConsoleKey.Add:
                    case ConsoleKey.A:
                    case ConsoleKey.NumPad4:
                    case ConsoleKey.D:
                    case ConsoleKey.NumPad6:
                        selected = selected == 0 ? 1 : 0;
                        break;
                    case ConsoleKey.Enter:
                    case ConsoleKey.F:
                    case ConsoleKey.NumPad5:
                        if (selected == 1)
                        {
                            do
                            {
                                DrawInput(new string[] { "//Confirm \"Old\"" }, 0);
                                switch (Console.ReadKey(true).Key)
                                {
                                    case ConsoleKey.F:
                                    case ConsoleKey.NumPad5:
                                        switch (equipment.Slot)
                                        {
                                            case Slot.Head:
                                                player.Equipment.EquippedHead = headOld;
                                                break;
                                            case Slot.Outfit:
                                                player.Equipment.EquippedOutfit = outfitOld;
                                                break;
                                            case Slot.Amulet:
                                                player.Equipment.EquippedAmulet = amuletOld;
                                                break;
                                            case Slot.Accessory:
                                                player.Equipment.EquippedAccessory = accessoryOld;
                                                break;
                                            case Slot.MainHand:
                                                player.Equipment.EquippedWeapon = weaponOld;
                                                break;
                                            case Slot.OffHand:
                                                player.Equipment.EquippedOffHand = offHandOld;
                                                break;
                                        }
                                        loop = false;
                                        DrawInput(new string[] { "//Leave Room" }, 0);
                                        selected = 2;
                                        break;
                                    case ConsoleKey.Escape:
                                    case ConsoleKey.Subtract:
                                        selected = 2;
                                        break;
                                }
                            } while (selected == 1);
                            selected = 1;
                        }
                        else
                        {
                            do
                            {
                                DrawInput(new string[] { "//Confirm \"New\"" }, 0);
                                switch (Console.ReadKey(true).Key)
                                {
                                    case ConsoleKey.F:
                                    case ConsoleKey.NumPad5:
                                        if (player.ResourceAttribute != Attributes.Intuition)
                                        {
                                            player.Resource = tempMax != player.FindResource() ?
                                            tempResource + (player.FindResource() - tempMax) : tempResource;
                                        }
                                        player.Life = tempMaxLife != player.FindLife() ?
                                            tempLife + (player.FindLife() - tempMaxLife) : tempLife;
                                        loop = false;
                                        DrawInput(new string[] { "//Leave Room" }, 0);
                                        selected = 2;
                                        break;
                                    case ConsoleKey.Escape:
                                    case ConsoleKey.Subtract:
                                        selected = 2;
                                        break;
                                }
                            } while (selected == 0);
                            selected = 0;
                        }
                        break;
                }
            } while (loop);
            CharacterInfo.ClearArea();
            CharacterInfo.ShortAttributes(player);
            #region old code
            //rapport += 3;
            //switch (selector)
            //{
            //    case 1:
            //        string[] acName = { "Charm bracelet", "Handbag", "Fannypack" };
            //        Accessory newItem1 = new Accessory(rand.Next(5) + (rapport / 2), 0, new bool[] { }, Slot.Accessory, new int[7]
            //            {rand.Next(3), rand.Next(3), rand.Next(3), rand.Next(3), rand.Next(3), rand.Next(3), rand.Next(3) },
            //            0, acName[rand.Next(3)], "A shiny thing.", ItemType.Equip);
            //        player.Equipment.EquippedAccessory = newItem1;
            //        sendBack = newItem1.Name;
            //        break;
            //    case 2:
            //        string[] amName = { "Star Amulet", "Sun Amulet", "Moon Amulet" };
            //        Amulet newItem2 = new Amulet(0, Slot.Amulet, new int[7] { rand.Next(3), rand.Next(3), rand.Next(3), rand.Next(3),
            //            rand.Next(3), rand.Next(3), rand.Next(3) }, 0, amName[rand.Next(3)], "", ItemType.Equip);
            //        player.Equipment.EquippedAmulet = newItem2;
            //        sendBack = newItem2.Name;
            //        break;
            //    case 3:
            //        string[] heName = { "Wizard Hat", "Camelskin Cap", "Helmet" };
            //            Head newItem3 = new Head(rand.Next(rapport) * 2, Slot.Head, new int[7] { rand.Next(3), rand.Next(3), rand.Next(3),
            //                rand.Next(3), rand.Next(3), rand.Next(3), rand.Next(3) }, 0, heName[rand.Next(3)], "", ItemType.Equip);
            //        player.Equipment.EquippedHead = newItem3;
            //        sendBack = newItem3.Name;
            //        break;
            //    case 4:
            //        OffHandType newOHT = OffHandType.None;
            //        string[] ohName = new string[3];
            //        switch (rand.Next(1, 7))
            //        {
            //            case 1:
            //                newOHT = OffHandType.ArmGuard;
            //                ohName = new string[3] { "Mini shield", "Arm Guard", "Crappy Shield" };
            //                break;
            //            case 2:
            //                newOHT = OffHandType.Lasso;
            //                ohName = new string[3] { "Wonder Whip", "Lasso", "String of Ears" };
            //                break;
            //            case 3:
            //                newOHT = OffHandType.Orb;
            //                ohName = new string[3] { "Orb of Scrutiny", "Orb of Sorrows", "Orb of Steak" };
            //                break;
            //            case 4:
            //                newOHT = OffHandType.Powder;
            //                ohName = new string[3] { "Blast Powder", "Smoke Powder", "Flash Powder" };
            //                break;
            //            case 5:
            //                newOHT = OffHandType.PowerFist;
            //                ohName = new string[3] { "Fist of Fury", "Power Fist", "Midas Touch" };
            //                break;
            //            case 6:
            //                newOHT = OffHandType.Tome;
            //                ohName = new string[3] { "Dark Tome", "Light Tome", "Rainbow Tome" };
            //                break;
            //        }
            //        OffHand newItem4 = new OffHand(newOHT, rand.Next(rapport), rand.Next(rapport), rand.Next(rapport), Slot.OffHand,
            //            new int[7] { 0, 0, 0, 0, 0, 0, 0 }, 0, ohName[rand.Next(3)], "", ItemType.Equip);
            //        player.Equipment.EquippedOffHand = newItem4;
            //        sendBack = newItem4.Name;
            //        break;
            //    case 5:
            //        string[] weName = { "Longersword", "Wanda's Wand", "Steak Knife", "Butterfly Net", "Exaliber", "Rebil Axe" };
            //        Weapon newItem5 = new Weapon(rand.Next(rapport) + 2, rand.Next(3, rapport + 3) + 3, 5 + rand.Next(rapport), SkillLibrary.StatusEffect.None,
            //            0, Slot.MainHand, false, new int[7] { rand.Next(5), rand.Next(5), rand.Next(5), rand.Next(5), rand.Next(5), rand.Next(5),
            //            rand.Next(5)}, 0, weName[rand.Next(6)], "", ItemType.None);
            //        player.Equipment.EquippedWeapon = newItem5;
            //        sendBack = newItem5.Name;
            //        break;
            //    case 6:
            //        string[] ouName = { "Jockey's Getup", "Wizard Robe", "Priest's Garb", "Dad's T-Shirt", "Cinderella's Gown"};
            //        Outfit newItem6 = new Outfit(new int[] { rand.Next(rapport), 0, rand.Next(rapport), rand.Next(rapport) },
            //            Slot.Outfit, new int[] { 0, 0, 0, 0, 0, 0, 0 }, 0, ouName[rand.Next(5)], "", ItemType.Equip);
            //        player.Equipment.EquippedOutfit = newItem6;
            //        sendBack = newItem6.Name;
            //        break;
            //}
            #endregion
        }//TODO IMPROVE-CTORS

        public static void DrawScene(Player player)
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta; Console.SetCursorPosition(102, 28); Console.Write("|[");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.SetCursorPosition(2, 6); Console.Write("\\__________________________________________________________________________________________________/");
            Console.SetCursorPosition(2, 21); Console.Write("[__________________________________________________________________________________________________]");
            Console.SetCursorPosition(2, 28); Console.Write("[__________________________________________________________________________________________________]");

            for (int y = 7; y < 28; y++)
            {
                Console.SetCursorPosition(2, y); Console.Write("[");
                Console.SetCursorPosition(101, y); Console.Write("]");
            }


            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.SetCursorPosition(12, 23); Console.Write(" _______________________________________________________________________________");
            Console.SetCursorPosition(12, 24); Console.Write("/");
            Console.SetCursorPosition(11, 25); Console.Write("||");
            Console.SetCursorPosition(12, 26); Console.Write("\\_______________________________________________________________________________/");
            Console.SetCursorPosition(12, 27); Console.Write(" \\_____________________________________________________________________________/");

            Console.SetCursorPosition(89, 23); Console.Write("");
            Console.SetCursorPosition(91, 24); Console.Write(" \\");
            Console.SetCursorPosition(91, 25); Console.Write(" ||");
        }

        public static bool Confirm(string response, ConsoleColor color)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;

            Console.SetCursorPosition(5, 24); Console.Write(" //");
            Console.SetCursorPosition(5, 25); Console.Write("{<<<<");
            Console.SetCursorPosition(5, 26); Console.Write(" \\\\");

            Console.SetCursorPosition(95, 24); Console.Write("  \\\\");
            Console.SetCursorPosition(95, 25); Console.Write(">>>>}");
            Console.SetCursorPosition(95, 26); Console.Write("  //");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(43, 22); Console.Write("//Confirm Response");

            int x = 13 + ((79 - response.Length) / 2);
            Console.SetCursorPosition(13, 25); Console.Write("                                                                               ");
            Console.ForegroundColor = color; Console.SetCursorPosition(x, 25); Console.Write(response);
            bool confirmed = false;
            x = 0;
            do
            {
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.F:
                    case ConsoleKey.NumPad5:
                        confirmed = true;
                        x++;
                        break;
                    case ConsoleKey.Escape:
                    case ConsoleKey.Subtract:
                        x++;
                        break;
                }
            } while (x == 0);
            return confirmed;
        }

        public static void DrawInput(string[] responses, int selector)
        {
            Console.ForegroundColor = responses.Length > 1 ? ConsoleColor.Green : ConsoleColor.DarkGray;
            if (responses.Length == 7)
            {
                Console.ForegroundColor = responses[selector + 1] == "makeitgreen" ? ConsoleColor.Green : responses[selector + 1] != "makeitblue" ? ConsoleColor.White :
                    ConsoleColor.DarkGray;
            }
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
            Console.SetCursorPosition(43, 22); Console.Write("//Select Response   ");
            int x = 13 + ((79 - responses[selector].Length) / 2);
            Console.SetCursorPosition(13, 25); Console.Write("                                                                               ");
            Console.ForegroundColor = responses.Length > 1 ? ConsoleColor.Gray : ConsoleColor.Green;
            Console.SetCursorPosition(x, 25); Console.Write(responses[selector]);
        }

        public static string[,] GetDialogue(Player player)
        {
            string[,] dialogue = new string[8, 7]; // [0, (3-11)]  npcEmotionLines [1, (3-11)] playerEmotion responses
                                                   //Need to put some text into place.... let's start with the 8 emotions each having a unique line.
            #region Dialogue 1
            string[,] dialogue1 = new string[8, 7];
            dialogue1[0, 0] = "I've had enough of these monsters!"; //NPCLine Anger
            dialogue1[0, 1] = "I'll smash the next one I see!"; //PlayerLine Rapport++;
            dialogue1[0, 2] = "They are so scary."; //PlayerLine Rapport;
            dialogue1[0, 3] = "Aw, keep your chin up!"; //PlayerLine Rapport--;
            dialogue1[0, 4] = "Here! Be sure to mess it up real good for me."; //NPCLine good rapport
            dialogue1[0, 5] = "Only if you're a coward..."; //NPCLine neutral rapport
            dialogue1[0, 6] = "I've had enough of you too!"; //NPCLine negative rapport

            dialogue1[1, 0] = "I've been waiting for you!"; //Anticipation
            dialogue1[1, 1] = "I've been trying to get here!"; //++
            dialogue1[1, 2] = "Okay..."; //
            dialogue1[1, 3] = "Ah! Get away!"; //--
            dialogue1[1, 4] = "Finally I can give you this!"; //npc+
            dialogue1[1, 5] = "Maybe you're not who I was looking for..."; //npc
            dialogue1[1, 6] = "You get away!"; //npc-

            dialogue1[2, 0] = "You look more lost than I am."; //Disgust
            dialogue1[2, 1] = "Its a rotten situation we're in."; //++
            dialogue1[2, 2] = "I'm not lost..."; //
            dialogue1[2, 3] = "You'd better get lost!"; //--
            dialogue1[2, 4] = "Maybe this will help."; //npc+
            dialogue1[2, 5] = "Well I'm not either..?"; //npc
            dialogue1[2, 6] = "I already am lost!"; //npc-

            dialogue1[3, 0] = "Ah! I thought you were another monster!"; //Fear
            dialogue1[3, 1] = "Eek! A monster? Where!?"; //++
            dialogue1[3, 2] = "Nope, I'm not a monster."; //
            dialogue1[3, 3] = "How could you mistake me for one of them!?"; //--
            dialogue1[3, 4] = "You'll stand a better chance against them with this."; //npc+
            dialogue1[3, 5] = "Well... good."; //npc
            dialogue1[3, 6] = "Why are you so mad?"; //npc-

            dialogue1[4, 0] = "I'm so glad you're here!"; //Joy
            dialogue1[4, 1] = "It's wonderful being here with you."; //++
            dialogue1[4, 2] = "What are you glad about?"; //
            dialogue1[4, 3] = "I'm not glad I'm here..."; //--
            dialogue1[4, 4] = "I think you should have this! *wink*"; //npc+
            dialogue1[4, 5] = "You being here silly!"; //npc
            dialogue1[4, 6] = "Oh, well... that's unfortunate."; //npc-

            dialogue1[5, 0] = "It's so hard to be stuck in here..."; //Sadness
            dialogue1[5, 1] = "It is really getting to me..."; //++
            dialogue1[5, 2] = "Meh, it doesn't bother me much."; //
            dialogue1[5, 3] = "I love new adventures!"; //--
            dialogue1[5, 4] = "I think this'll help."; //npc+
            dialogue1[5, 5] = "Must be nice..."; //npc
            dialogue1[5, 6] = "I don't."; //npc-

            dialogue1[6, 0] = "Oh my!  Who knew I'd be getting a visitor?"; //Surprise
            dialogue1[6, 1] = "Oh wow!  Who knew you'd be here?"; //++
            dialogue1[6, 2] = "Yup, it's me."; //
            dialogue1[6, 3] = "Well it's either me or a monster."; //--
            dialogue1[6, 4] = "I was saving this for someone super!"; //npc+
            dialogue1[6, 5] = "Hmm.  Not so exciting."; //npc
            dialogue1[6, 6] = "Yikes, that's one way to look at it."; //npc-

            dialogue1[7, 0] = "Come over here!  I need your help!"; //Trust
            dialogue1[7, 1] = "Sure, whatchya need?"; //++
            dialogue1[7, 2] = "Do I know you?"; //
            dialogue1[7, 3] = "No way."; //--
            dialogue1[7, 4] = "I thought you'd know what to do with this!"; //npc+
            dialogue1[7, 5] = "No, but you could..."; //npc
            dialogue1[7, 6] = "Aw, shucks..."; //npc-
            #endregion

            #region Dialogue 2
            string[,] dialogue2 = new string[8, 7];
            dialogue2[0, 0] = "What do you think you're doing here?!"; //NPCLine Anger
            dialogue2[0, 1] = "I'm slaying these damned monsters!"; //PlayerLine Rapport++;
            dialogue2[0, 2] = "I thought we could be friends."; //PlayerLine Rapport;
            dialogue2[0, 3] = "This room looked safe."; //PlayerLine Rapport--;
            dialogue2[0, 4] = "Use this and rampage on!"; //NPCLine good rapport
            dialogue2[0, 5] = "Maybe in another life."; //NPCLine neutral rapport
            dialogue2[0, 6] = "You'll find no safety here!"; //NPCLine negative rapport

            dialogue2[1, 0] = "Hello there. *Continues examining the corpse of a monster*"; //Anticipation
            dialogue2[1, 1] = "Interesting specimen.  *Examine the corpse*"; //++
            dialogue2[1, 2] = "*Silently observe*"; //
            dialogue2[1, 3] = "Careful! Get away from that!"; //--
            dialogue2[1, 4] = "I think this can help against them."; //npc+
            dialogue2[1, 5] = "Do you mind?"; //npc
            dialogue2[1, 6] = "How about I get away from you instead."; //npc-

            dialogue2[2, 0] = "Can't a person find a little peace and quiet around here?"; //Disgust
            dialogue2[2, 1] = "I don't want to stick around this dump anyway."; //++
            dialogue2[2, 2] = "I don't think that's possible."; //
            dialogue2[2, 3] = "Let's find that peace and quiet together."; //--
            dialogue2[2, 4] = "Well, before you go, take this."; //npc+
            dialogue2[2, 5] = "Not with you around anyway..."; //npc
            dialogue2[2, 6] = "Absolutely not."; //npc-

            dialogue2[3, 0] = "Quick! Step into the light!"; //Fear
            dialogue2[3, 1] = "Oh thank goodness, everything's so dark around here."; //++
            dialogue2[3, 2] = "What's the rush?"; //
            dialogue2[3, 3] = "Don't tell me what to do!"; //--
            dialogue2[3, 4] = "I hope this brings you more light on your journey."; //npc+
            dialogue2[3, 5] = "There are monsters out there."; //npc
            dialogue2[3, 6] = "Suit yourself!"; //npc-

            dialogue2[4, 0] = "Hail, friend! It's good to see you!"; //Joy
            dialogue2[4, 1] = "Salutations!  What a wonderful day to be alive!"; //++
            dialogue2[4, 2] = "Whoa! You came out of no where."; //
            dialogue2[4, 3] = "What's so good about it?"; //--
            dialogue2[4, 4] = "Here's something to make sure you see the next day!"; //npc+
            dialogue2[4, 5] = "I was standing still..."; //npc
            dialogue2[4, 6] = "I guess I'll be going then."; //npc-

            dialogue2[5, 0] = "*sigh* Another damned soul..."; //Sadness
            dialogue2[5, 1] = "It all feels so hopeless."; //++
            dialogue2[5, 2] = "What's your problem?"; //
            dialogue2[5, 3] = "Damned?  Try blessed!"; //--
            dialogue2[5, 4] = "Agreed.  Maybe this will change your luck."; //npc+
            dialogue2[5, 5] = "What isn't my problem?"; //npc
            dialogue2[5, 6] = "How could anything down here be blessed..."; //npc-

            dialogue2[6, 0] = "I had no idea I'd be getting a vistor."; //Surprise
            dialogue2[6, 1] = "You never know what's around the corner!"; //++
            dialogue2[6, 2] = "I'm just passing through."; //
            dialogue2[6, 3] = "You might if you paid attention."; //--
            dialogue2[6, 4] = "Be careful around the next one!"; //npc+
            dialogue2[6, 5] = "Oh, of course..."; //npc
            dialogue2[6, 6] = "But everything happens so fast here."; //npc-

            dialogue2[7, 0] = "Hello.  You look friendly!"; //Trust
            dialogue2[7, 1] = "Are we not all friends here?"; //++
            dialogue2[7, 2] = "That's just the way my face is."; //
            dialogue2[7, 3] = "And you look like a monkey's uncle."; //--
            dialogue2[7, 4] = "My sentiments exactly!  Here, friend!"; //npc+
            dialogue2[7, 5] = "Oh, my mistake."; //npc
            dialogue2[7, 6] = "Point taken..."; //npc-
            #endregion

            #region Dialogue 3
            string[,] dialogue3 = new string[8, 7];
            dialogue3[0, 0] = "Arrrrrgggghh!"; //NPCLine Anger
            dialogue3[0, 1] = "Rrraaaahhhr"; //PlayerLine Rapport++;
            dialogue3[0, 2] = "Weeeeeeee!"; //PlayerLine Rapport;
            dialogue3[0, 3] = "Aaaahhhhhh!"; //PlayerLine Rapport--;
            dialogue3[0, 4] = "You speak my language!  Have this."; //NPCLine good rapport
            dialogue3[0, 5] = "You're on the wrong ride."; //NPCLine neutral rapport
            dialogue3[0, 6] = "Scaredy cat."; //NPCLine negative rapport

            dialogue3[1, 0] = "I thought I'd be getting a visitor soon."; //Anticipation
            dialogue3[1, 1] = "What brilliant deductive skills you have."; //++
            dialogue3[1, 2] = "Who me?"; //
            dialogue3[1, 3] = "How could you possibly know that?"; //--
            dialogue3[1, 4] = "A little observation goes a long way.  So does stuff."; //npc+
            dialogue3[1, 5] = "You seem… confused."; //npc
            dialogue3[1, 6] = "My secrets are my own."; //npc-

            dialogue3[2, 0] = "Narrow hallways. Dark rooms. Filthy monsters."; //Disgust
            dialogue3[2, 1] = "Horrible dialogue."; //++
            dialogue3[2, 2] = "Blue skies."; //
            dialogue3[2, 3] = "Friendly faces."; //--
            dialogue3[2, 4] = "Extra stuff."; //npc+
            dialogue3[2, 5] = "I've forgotten what the sky looks like."; //npc
            dialogue3[2, 6] = "Not here."; //npc-
        
            dialogue3[3, 0] = "I wish everything wasn't so frightening."; //Fear
            dialogue3[3, 1] = "Even the tiny one's are scary."; //++
            dialogue3[3, 2] = "Spoken like a true coward."; //
            dialogue3[3, 3] = "You gotta man up!"; //--
            dialogue3[3, 4] = "Maybe a little less scary with this."; //npc+
            dialogue3[3, 5] = "At least I know who I am..."; //npc
            dialogue3[3, 6] = "I gotta get out of here!"; //npc-
        
            dialogue3[4, 0] = "*continues singing* ...and when the sun comes out..."; //Joy
            dialogue3[4, 1] = "...the frown turns upside down!"; //++
            dialogue3[4, 2] = "...the moon goes to sleep."; //
            dialogue3[4, 3] = "...we'll never see it happen."; //--
            dialogue3[4, 4] = "Oh, good one!  Hopefully this is good too!"; //npc+
            dialogue3[4, 5] = "This was supposed to be more upbeat."; //npc
            dialogue3[4, 6] = "No... that's not how it goes."; //npc-

            dialogue3[5, 0] = "*moaning* My dog... my dog..."; //Sadness
            dialogue3[5, 1] = "*cries* your dog... your dog..."; //++
            dialogue3[5, 2] = "What do I do with my hands..."; //
            dialogue3[5, 3] = "*sings* is a lovely pet!"; //--
            dialogue3[5, 4] = "Finally, someone understands!  This was his favorite toy."; //npc+
            dialogue3[5, 5] = "I'm... perplexed."; //npc
            dialogue3[5, 6] = "*sobs uncontrollably*"; //npc-
        
            dialogue3[6, 0] = "Woah!  You just burst straight into here!"; //Surprise
            dialogue3[6, 1] = "I should've been more careful."; //++
            dialogue3[6, 2] = "Ta da!"; //
            dialogue3[6, 3] = "Ready for action!"; //--
            dialogue3[6, 4] = "I appreciate the sentiment.  I don't want this anymore."; //npc+
            dialogue3[6, 5] = "Curiouser and curiouser..."; //npc
            dialogue3[6, 6] = "Too much for me."; //npc-
        
            dialogue3[7, 0] = "Thank goodness you're here! Can you watch my back while I pee?"; //Trust
            dialogue3[7, 1] = "Have no fear, I am here!"; //++
            dialogue3[7, 2] = "Do I have to watch?"; //
            dialogue3[7, 3] = "Gross, no."; //--
            dialogue3[7, 4] = "Ahhhh... thanks so much!"; //npc+
            dialogue3[7, 5] = "Er... I suppose you don't."; //npc
            dialogue3[7, 6] = "I gotta go now!"; //npc-
            #endregion

            #region Dialogue 4
            string[,] dialogue4 = new string[8, 7];
            dialogue4[0, 0] = "Dang it, you found my hiding spot!"; //NPCLine Anger
            dialogue4[0, 1] = "You clearly aren't hiding well enough!"; //PlayerLine Rapport++;
            dialogue4[0, 2] = "Oh sorry, didn't see you there."; //PlayerLine Rapport;
            dialogue4[0, 3] = "Oh no! Now they'll find us both!"; //PlayerLine Rapport--;
            dialogue4[0, 4] = "Fair point, I'll find a better spot, you take this and be on your way."; //NPCLine good rapport
            dialogue4[0, 5] = "Well hurry up already…"; //NPCLine neutral rapport
            dialogue4[0, 6] = "I'm leaving you behind!"; //NPCLine negative rapport

            dialogue4[1, 0] = "Hey, what do you think this is? *points to dark spot on floor*"; //Anticipation
            dialogue4[1, 1] = "Let me take a closer look."; //++
            dialogue4[1, 2] = "Probably nothing useful."; //
            dialogue4[1, 3] = "I'm not going near that!"; //--
            dialogue4[1, 4] = "*picks up object* Oh, this actually might be useful to you."; //npc+
            dialogue4[1, 5] = "Your loss…"; //npc
            dialogue4[1, 6] = "It's mine anyway!"; //npc-

            dialogue4[2, 0] = "Every one of these monsters is uglier than the last."; //Disgust
            dialogue4[2, 1] = "They could really use a makeover."; //++
            dialogue4[2, 2] = "Than the last what?"; //
            dialogue4[2, 3] = "Beauty is in the eye of the beholder."; //--
            dialogue4[2, 4] = "Give them a new face with this!"; //npc+
            dialogue4[2, 5] = "The last… monster?"; //npc
            dialogue4[2, 6] = "Ugly is in the space of your face."; //npc-

            dialogue4[3, 0] = "Shh! Get down!"; //Fear
            dialogue4[3, 1] = "Oh crap, what's there?"; //++
            dialogue4[3, 2] = "Speak up I can't hear you."; //
            dialogue4[3, 3] = "I'll be as loud as I want!"; //--
            dialogue4[3, 4] = "Just hiding from those scary monsters! Here take this."; //npc+
            dialogue4[3, 5] = "Oh you're helpless!"; //npc
            dialogue4[3, 6] = "Suit yourself, I'm outta here!"; //npc-

            dialogue4[4, 0] = "I'm happy you've made it this far."; //Joy
            dialogue4[4, 1] = "It's a pleasure to arrive."; //++
            dialogue4[4, 2] = "I'm not sure how I feel about it."; //
            dialogue4[4, 3] = "I wish I wasn't in this place."; //--
            dialogue4[4, 4] = "And now it'll be even more pleasurable."; //npc+
            dialogue4[4, 5] = "You take your time figuring that out."; //npc
            dialogue4[4, 6] = "Onward ho!"; //npc-

            dialogue4[5, 0] = "Our futures look so bleak."; //Sadness
            dialogue4[5, 1] = "Let's relish in the misery of the present."; //++
            dialogue4[5, 2] = "I try not to think about it."; //
            dialogue4[5, 3] = "Bleak, like… cool?"; //--
            dialogue4[5, 4] = "A present, for the present."; //npc+
            dialogue4[5, 5] = "Well, it'll come anyway."; //npc
            dialogue4[5, 6] = "You're too cool for school."; //npc-

            dialogue4[6, 0] = "You can't sneak up on someone like that!"; //Surprise
            dialogue4[6, 1] = "I totally understand, who knows what's lurking out there?"; //++
            dialogue4[6, 2] = "I can and I did."; //
            dialogue4[6, 3] = "You should just be more observant."; //--
            dialogue4[6, 4] = "You'll stand a better chance against whatever it is with this."; //npc+
            dialogue4[6, 5] = "Well, maybe knock next time."; //npc
            dialogue4[6, 6] = "*scoffs* As if that's even an option for me."; //npc-

            dialogue4[7, 0] = "I started a fire, come get warm!"; //Trust
            dialogue4[7, 1] = "Awesome! I brought marshmellows!"; //++
            dialogue4[7, 2] = "Not a huge fire fan."; //
            dialogue4[7, 3] = "I'm pretty sure you'll just burn me."; //--
            dialogue4[7, 4] = "I'll remember this forever.  Here's something to remember me by!"; //npc+
            dialogue4[7, 5] = "Oh, my mistake."; //npc
            dialogue4[7, 6] = "Oh, well I'll leave it going in case you change your mind."; //npc-
            #endregion

            Random rand = new Random();
            int random = rand.Next(200);
            dialogue = (random + 4) % 4 == 1 ? dialogue1 : (random + 4) % 4 == 2 ? dialogue2 : (random + 4) % 4 == 3 ?
                dialogue3 : dialogue4;
            return dialogue;
        }//I can make more 2D Arrays to randomize emotional content
    }
}
