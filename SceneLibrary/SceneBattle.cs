using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MenuLibrary;
using CharacterLibrary;
using PlayerLibrary;
using MonsterLibrary;
using SkillLibrary;

namespace SceneLibrary
{
    public class SceneBattle
    {
        //public static void Initialize(Player player, Monster monster)
        //{
        //        DrawDisplay(player, monster); //static
        //}

        public static void BattleLoop(Player player, Monster monster)
        {
            //Private Variables
            //bools
            bool battleOn = true;
            bool playerTurn = true;
            //ints
            int selector = 1;
            //strings
            string[] turnCurrent = new string[3];
            //Start Loop
            Console.SetCursorPosition(110, 1);
            do
            {
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.A:
                    case ConsoleKey.NumPad4:
                        selector -= selector != 0 ? 1 : -2;
                        UpdateInput(player, monster, selector);
                        break;
                    case ConsoleKey.Tab:
                    case ConsoleKey.Add:
                    case ConsoleKey.D:
                    case ConsoleKey.NumPad6:
                        selector += selector != 2 ? 1 : -2;
                        UpdateInput(player, monster, selector);
                        break;
                    case ConsoleKey.F:
                    case ConsoleKey.NumPad5:
                        //Look at selector
                        switch (selector)
                        {
                            case 0: //Attack!
                                playerTurn = false;
                                turnCurrent = PlayerAttack(player, monster, turnCurrent);
                                battleOn = VictoryCheck(player, monster);
                                break;
                            case 1: //Focus!
                                UpdateInput(player, monster, 5);
                                switch (player.SelectedSkill[0])
                                {
                                    case -3:
                                        Focus(player, monster, player.Primary, turnCurrent);
                                        playerTurn = false;
                                        break;
                                    case -4:
                                        Focus(player, monster, player.Secondary, turnCurrent);
                                        playerTurn = false;
                                        break;
                                    case -5:
                                        Focus(player, monster, player.Tertiary, turnCurrent);
                                        playerTurn = false;
                                        break;
                                }
                                player.SelectedSkill[0] = -1;
                                UpdateInput(player, monster, selector);
                                break;
                            case 2: //Skill!
                                //Needs to select skill.
                                UpdateInput(player, monster, 4);
                                if (player.SelectedSkill[0] != -1)
                                {
                                    playerTurn = false;
                                    turnCurrent = PlayerSkill(player, monster, turnCurrent);
                                    battleOn = VictoryCheck(player, monster);
                                }
                                UpdateInput(player, monster, selector);
                                //Open Menu (UpdateInput(player, monster, 4)
                                break;
                        }
                        break;
                }
                if (playerTurn != true & battleOn == true)
                {
                    UpdateInput(player, monster, 3);
                    turnCurrent = MonsterTurn(monster, player, turnCurrent);
                    battleOn = VictoryCheck(player, monster);
                    playerTurn = true;
                    player.UpdateBoosts();
                    UpdateInput(player, monster, selector);
                    UpdateStatus(player);
                }
            } while (battleOn);
            Console.Clear();
            EndProcessing(player);
        }

        //Attack Methods
        public static string[] PlayerAttack(Player player, Monster monster, string[] turnCurrent)
        {
            //Bools
            bool doesHit;
            //Ints
            int damageDealt;
            //String to store info for UpdateSequence
            string[] previous = turnCurrent;
            string[] current = new string[3];
            current[0] = "P";
            current[1] = player.Name + " attacks " + monster.Name + "!";
            //Player info
            int[] boosts = { player.Boosts[0, 1], player.Boosts[1, 1], player.Boosts[2, 1], player.Boosts[3, 1],
            player.Boosts[4, 1], player.Boosts[5, 1], player.Boosts[6, 1], player.Boosts[7, 1], player.Boosts[8, 1],
            player.Boosts[9, 1], player.Boosts[10, 1] };
            int[] statusMods = player.StatusDoes();
            int[] playerAttrs = player.EqAttrMod();
            playerAttrs[0] += player.Strength + statusMods[0] + boosts[0];
            playerAttrs[1] += player.Defense + statusMods[1] + boosts[1];
            playerAttrs[2] += player.Intelligence + statusMods[2] + boosts[2];
            playerAttrs[3] += player.Wisdom + statusMods[3] + boosts[3];
            playerAttrs[4] += player.Dexterity + statusMods[4] + boosts[4];
            playerAttrs[5] += player.Agility + statusMods[5] + boosts[5];
            playerAttrs[6] += player.Resource + statusMods[6] + boosts[6];
            int[] playerChances = player.ChancesMod();
            playerChances[0] += player.HitChance + statusMods[8] + boosts[8] + (playerAttrs[4] / 3);
            playerChances[1] += player.ShieldBonus + statusMods[7] + boosts[7] + (playerAttrs[3] / 3);
            playerChances[2] += player.ResistChance + statusMods[9] + boosts[9] + (playerAttrs[3] / 3);
            playerChances[3] += player.DodgeChance + statusMods[10] + boosts[10] + (playerAttrs[5] / 3);
            //Monster info
            int[] monsterBoost = { monster.Boosts[0, 1], monster.Boosts[1, 1], monster.Boosts[2, 1], monster.Boosts[3, 1],
            monster.Boosts[4, 1], monster.Boosts[5, 1], monster.Boosts[6, 1], monster.Boosts[7, 1], monster.Boosts[8, 1],
            monster.Boosts[9, 1], monster.Boosts[10, 1] };
            int[] monsterMods = monster.StatusDoes();
            int[] monsterAttrs = new int[7]
            { monster.Strength + monsterMods[0] + monsterBoost[0],
                monster.Defense + monsterMods[1] + monsterBoost[1],
                monster.Intelligence + monsterMods[2] + monsterBoost[2],
                monster.Wisdom + monsterMods[3] + monsterBoost[3],
                monster.Dexterity + monsterMods[4] + monsterBoost[4],
                monster.Agility + monsterMods[5] + monsterBoost[5],
                monster.Resource };
            int[] monsterChances = new int[4];
            monsterChances[0] = (monster.HitChance + monsterMods[8] + monsterBoost[8] + (monsterAttrs[4] / 3));
            monsterChances[1] = (monster.ShieldBonus + monsterMods[7] + monsterBoost[7] + (monsterAttrs[3] / 3));
            monsterChances[2] = (monster.ResistChance + monsterMods[9] + monsterBoost[9] + (monsterAttrs[3] / 3));
            monsterChances[3] = (monster.DodgeChance + monsterMods[10] + monsterBoost[10] + (monsterAttrs[5] / 3));

            //Consider putting following into a method resuable by monster on it's turn
            //Display action 
            //DarkCyan
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.SetCursorPosition(7, 17); Console.Write(player.Name);
            //White
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(7 + player.Name.Length, 17); Console.Write(" attacks ");
            Console.SetCursorPosition(16 + player.Name.Length + monster.Name.Length, 17); Console.Write("!");
            //DarkRed
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(16 + player.Name.Length, 17); Console.Write(monster.Name);
            UpdateInput(player, monster, 3);
            PressEnter();

            //Needs to...
            //Determine if attack hits
            doesHit = player.CalcHit(playerChances[0], monsterChances[3]);

            //Determine damage dealt
            if (doesHit)
            {
                int mDef = monster.Defense / 5;
                mDef += mDef <= 0 ? 1 : 0;
                damageDealt = (player.CalcDamage(player, monster)) / mDef;
                current[2] = player.Name + " hits " + monster.Name + " for " + damageDealt + " damage.";
                //DarkCyan
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.SetCursorPosition(7, 18); Console.Write(player.Name);
                //White
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(7 + player.Name.Length, 18); Console.Write(" hits ");
                Console.SetCursorPosition(14 + player.Name.Length + monster.Name.Length, 18); Console.Write("for {0} damage.", damageDealt);
                //DarkRed
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(13 + player.Name.Length, 18); Console.Write(monster.Name);
                //Update Properties
                if (player.ResourceAttribute == Attributes.Intuition)
                {
                    player.Resource += 5 + (player.FindResource()/10);
                    player.Resource = player.Resource > player.FindResource() ? player.FindResource() : player.Resource;
                }
                monster.Shield -= damageDealt;
                if (monster.Shield < 0)
                {
                    monster.Life += monster.Shield;
                    monster.Shield = 0;
                }
            }
            else
            {
                current[2] = player.Name + " missed!";
                if (player.ResourceAttribute == Attributes.Intuition)
                {
                    player.Resource += 1;
                    player.Resource = player.Resource > player.FindResource() ? player.FindResource() : player.Resource;
                }
                //DarkCyan
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.SetCursorPosition(7, 18); Console.Write(player.Name);
                //White
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(7 + player.Name.Length, 18); Console.Write(" missed!");
            }
            //Update Stuff
            Update(player, monster);

            PressEnter();
            if (monster.Life != 0)
            {
                UpdateSequence(1, current, previous, player, monster);
                UpdateInput(player, monster, 3);
            }

            //Display "{player.Name} attacks {monster.Name}" on line 2 of zone 3
            //Press enter to continue
            //Display Result
            //Press enter to continue
            //UpdateInput(player, monster, 3) ====Gray'd out during monster's turn====
            //UpdateSequence

            return current;
        }



        public static string[] MonsterAttack(Monster monster, Player player, string[] turnCurrent)
        {
            //Bools
            bool doesHit;
            //Ints
            int damageDealt;
            //String to store info for UpdateSequence
            string[] previous = turnCurrent;
            string[] current = new string[3];
            current[0] = "M";
            current[1] = monster.Name + " attacks " + player.Name + "!";
            //Player info
            int[] boosts = { player.Boosts[0, 1], player.Boosts[1, 1], player.Boosts[2, 1], player.Boosts[3, 1],
            player.Boosts[4, 1], player.Boosts[5, 1], player.Boosts[6, 1], player.Boosts[7, 1], player.Boosts[8, 1],
            player.Boosts[9, 1], player.Boosts[10, 1], player.Boosts[11, 1] };
            int[] statusMods = player.StatusDoes();
            int[] playerAttrs = player.EqAttrMod();
            playerAttrs[0] += player.Strength + statusMods[0] + boosts[0];
            playerAttrs[1] += player.Defense + statusMods[1] + boosts[1];
            playerAttrs[2] += player.Intelligence + statusMods[2] + boosts[2];
            playerAttrs[3] += player.Wisdom + statusMods[3] + boosts[3];
            playerAttrs[4] += player.Dexterity + statusMods[4] + boosts[4];
            playerAttrs[5] += player.Agility + statusMods[5] + boosts[5];
            playerAttrs[6] += player.Resource + statusMods[6] + boosts[6];
            int[] playerChances = player.ChancesMod();
            playerChances[0] += player.HitChance + statusMods[8] + boosts[8] + (playerAttrs[4] / 3);
            playerChances[1] += player.ShieldBonus + statusMods[7] + boosts[7] + (playerAttrs[3] / 5);
            playerChances[2] += player.ResistChance + statusMods[9] + boosts[9] + (playerAttrs[3] / 5);
            playerChances[3] += player.DodgeChance + statusMods[10] + boosts[10] + (playerAttrs[5] / 3);
            //Monster info
            int[] monsterBoost = { monster.Boosts[0, 1], monster.Boosts[1, 1], monster.Boosts[2, 1], monster.Boosts[3, 1],
            monster.Boosts[4, 1], monster.Boosts[5, 1], monster.Boosts[6, 1], monster.Boosts[7, 1], monster.Boosts[8, 1],
            monster.Boosts[9, 1], monster.Boosts[10, 1] };
            int[] monsterMods = monster.StatusDoes();
            int[] monsterAttrs = new int[7]
            { monster.Strength + monsterMods[0] + monsterBoost[0],
                monster.Defense + monsterMods[1] + monsterBoost[1],
                monster.Intelligence + monsterMods[2] + monsterBoost[2],
                monster.Wisdom + monsterMods[3] + monsterBoost[3],
                monster.Dexterity + monsterMods[4] + monsterBoost[4],
                monster.Agility + monsterMods[5] + monsterBoost[5],
                monster.Resource };
            int[] monsterChances = new int[4]
            { monster.HitChance + monsterMods[8] + monsterBoost[8] + (monsterAttrs[4]/3),
                monster.ShieldBonus + monsterMods[7] + monsterBoost[7] + (monsterAttrs[3]/3),
                monster.ResistChance + monsterMods[9] + monsterBoost[9] + (monsterAttrs[3]/3),
                monster.DodgeChance + monsterMods[10] + monsterBoost[10] + (monsterAttrs[5]/3)};

            //Consider putting following into a method resuable by monster on it's turn
            //Display action 
            //DarkCyan
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(7, 17); Console.Write(monster.Name);
            //White
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(7 + monster.Name.Length, 17); Console.Write(" attacks ");
            Console.SetCursorPosition(16 + player.Name.Length + monster.Name.Length, 17); Console.Write("!");
            //DarkRed
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.SetCursorPosition(16 + monster.Name.Length, 17); Console.Write(player.Name);
            UpdateInput(player, monster, 3);
            PressEnter();

            //Needs to...
            //Determine if attack hits

            doesHit = monster.CalcHit(monsterChances[0], playerChances[3]);

            //Determine damage dealt
            if (doesHit)
            {
                int pDef = playerAttrs[1] / 20;
                pDef += pDef <= 0 ? 1 : 0;
                damageDealt = monster.CalcDamage(monster, player) / pDef;
                current[2] = monster.Name + " hits " + player.Name + " for " + damageDealt + " damage.";
                //DarkCyan
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(7, 18); Console.Write(monster.Name);
                //White
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(7 + monster.Name.Length, 18); Console.Write(" hits ");
                Console.SetCursorPosition(14 + player.Name.Length + monster.Name.Length, 18); Console.Write("for {0} damage.", damageDealt);
                //DarkRed
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.SetCursorPosition(13 + monster.Name.Length, 18); Console.Write(player.Name);
                //Update Properties
                if (monster.ResourceAttribute == Attributes.Intuition)
                {
                    monster.Resource += 5;
                    int max = monster.ResourceMax;
                    monster.Resource = monster.Resource >= max ? max : monster.Resource;
                }
                player.Shield -= damageDealt;
                if (player.Shield < 0)
                {
                    player.Life += player.Shield;
                    player.Shield = 0;
                }
            }
            else
            {
                if (monster.ResourceAttribute == Attributes.Intuition)
                {
                    monster.Resource += 1;
                    int max = monster.ResourceMax;
                    monster.Resource = monster.Resource >= max ? max : monster.Resource;
                }
                current[2] = monster.Name + " missed!";
                //DarkCyan
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(7, 18); Console.Write(monster.Name);
                //White
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(7 + monster.Name.Length, 18); Console.Write(" missed!");
            }
            //Update Stuff
            Update(player, monster);

            PressEnter();
            if (player.Life != 0)
            {
                UpdateSequence(1, current, previous, player, monster);
            }
            return current;
        }

        public static bool VictoryCheck(Player player, Monster monster)
        {
            bool battleOn = true;
            //Check Player's Health
            if (player.Life == 0)
            {
                //Game Over
            }
            //Check Monster's Health
            if (monster.Life == 0)
            {
                Console.SetCursorPosition(1, 7); Console.ForegroundColor = ConsoleColor.Green; Console.Write("//VICTORY!                        ");
                for (int y = 8; y < 19; y++)
                {
                    Console.SetCursorPosition(7, y); Console.Write("                                                                                                               ");
                }
                Console.SetCursorPosition(7, 8); Console.ForegroundColor = ConsoleColor.White; Console.Write("Congratulations!");
                Console.SetCursorPosition(7, 9); Console.Write("You defeated"); Console.SetCursorPosition(21 + monster.Name.Length, 9); Console.Write("!");
                Console.SetCursorPosition(21, 9); Console.ForegroundColor = ConsoleColor.Red; Console.Write(monster.Name);
                PressEnter();
                Random rand = new Random();
                int dice = rand.Next(3, 6);
                player.DiceShards += dice;
                Console.SetCursorPosition(7, 12); Console.ForegroundColor = ConsoleColor.White; Console.Write("You found {0} dice shard{1}!", dice, dice != 1 ? "s" : "");
                PressEnter();
                Console.SetCursorPosition(7, 16); Console.Write("You now have");
                Console.SetCursorPosition(21 + player.DiceShards.ToString().Length, 16); Console.Write("{0}", player.DiceShards != 1 ? "dice shards" : "dice shard");
                Console.SetCursorPosition(20, 16); Console.ForegroundColor = ConsoleColor.Green; Console.Write("{0}", player.DiceShards);
                PressEnter();
                //Victory!
                //Will exit combat
                battleOn = false;
            }
            return battleOn;
        }

        public static string[] MonsterTurn(Monster monster, Player player, string[] turnCurrent)
        {
            string[] current = new string[3];
            monster.UpdateBoosts();
            UpdateStatus(monster);
            Update(player, monster);
            //Randomly decide monster's action (will currently just be attack)
            current = MonsterAttack(monster, player, turnCurrent);
            //Add monster skill turn.....
            //current = MonsterSkill(monster, player, turnCurrent);
            //current = MonstSkill(monster, player, turnCurrent); etc....
            if (player.Life == 0)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(@"
//==\\   //\\   |\\  //|  //===   //==\\  \\    //  //===  |==\\
|       |    |  | \\// |  |___    |    |   \\  //   |___   |__//
|   \\  |====|  |  \/  |  |	  |    |    \\//    |      |  \\
\\==//  |    |  |      |  \\===   \\==//     \/	    \\===  |   \\");
                Console.WriteLine("\n\n");
                PressEnter();
                Environment.Exit(-1);
            }
            return current;
        }

        public static string[] CharacterSkill(Player player, Monster monster, string[] turnCurrent)
        {
            return new string[3];
        }

        //Skill Methods
        public static string[] PlayerSkill(Player player, Monster monster, string[] turnCurrent)
        {

            Skill thisSkill = player.Skills[player.SelectedSkill[0], player.SelectedSkill[1]];
            //Temporary Skill Tweaks


            //End Temp
            #region GettingInfo
            //Player info
            int[] boosts = { player.Boosts[0, 1], player.Boosts[1, 1], player.Boosts[2, 1], player.Boosts[3, 1],
            player.Boosts[4, 1], player.Boosts[5, 1], player.Boosts[6, 1], player.Boosts[7, 1], player.Boosts[8, 1],
            player.Boosts[9, 1], player.Boosts[10, 1] };
            int[] statusMods = player.StatusDoes();
            int[] playerAttrs = player.EqAttrMod();
            playerAttrs[0] += player.Strength + statusMods[0] + boosts[0];
            playerAttrs[1] += player.Defense + statusMods[1] + boosts[1];
            playerAttrs[2] += player.Intelligence + statusMods[2] + boosts[2];
            playerAttrs[3] += player.Wisdom + statusMods[3] + boosts[3];
            playerAttrs[4] += player.Dexterity + statusMods[4] + boosts[4];
            playerAttrs[5] += player.Agility + statusMods[5] + boosts[5];
            playerAttrs[6] += player.Resource + statusMods[6] + boosts[6];
            int[] playerChances = player.ChancesMod();
            playerChances[0] += player.HitChance + statusMods[8] + boosts[8] + playerAttrs[4];
            playerChances[1] += player.ShieldBonus + statusMods[7] + boosts[7] + playerAttrs[3];
            playerChances[2] += player.ResistChance + statusMods[9] + boosts[9] + playerAttrs[3];
            playerChances[3] += player.DodgeChance + statusMods[10] + boosts[10] + playerAttrs[5];
            //Monster info
            int[] monsterBoost = { monster.Boosts[0, 1], monster.Boosts[1, 1], monster.Boosts[2, 1], monster.Boosts[3, 1],
            monster.Boosts[4, 1], monster.Boosts[5, 1], monster.Boosts[6, 1], monster.Boosts[7, 1], monster.Boosts[8, 1],
            monster.Boosts[9, 1], monster.Boosts[10, 1] };
            int[] monsterMods = monster.StatusDoes();
            int[] monsterAttrs = new int[7]
            { monster.Strength + monsterMods[0] + monsterBoost[0],
                monster.Defense + monsterMods[1] + monsterBoost[1],
                monster.Intelligence + monsterMods[2] + monsterBoost[2],
                monster.Wisdom + monsterMods[3] + monsterBoost[3],
                monster.Dexterity + monsterMods[4] + monsterBoost[4],
                monster.Agility + monsterMods[5] + monsterBoost[5],
                monster.Resource };
            int[] monsterChances = new int[4]
            { monster.HitChance + monsterMods[8] + monsterBoost[8] + monsterAttrs[4],
                monster.ShieldBonus + monsterMods[7] + monsterBoost[7] + monsterAttrs[3],
                monster.ResistChance + monsterMods[9] + monsterBoost[9] + monsterAttrs[3],
                monster.DodgeChance + monsterMods[10] + monsterBoost[10] + monsterAttrs[5]};
            #endregion
            //Finds Skill that is being used
            string[] skillInfo = thisSkill.GetSkill();
            //Useful local variables
            int x;
            int y;
            string temp;

            //Sets up battleSequence info
            string[] previous = turnCurrent;
            string[] current = new string[3];
            current[0] = "P";
            current[1] = player.Name + " uses " + skillInfo[0];
            current[2] = ""; //TODO REMOVE

            //Draw First Line (fairly Static)
            //DarkCyan
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.SetCursorPosition(7, 17); Console.Write(player.Name);
            //White
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(7 + player.Name.Length, 17); Console.Write(" uses ");
            Console.SetCursorPosition(13 + player.Name.Length + skillInfo[0].Length, 17); Console.Write("!");
            //Custom
            Console.ForegroundColor = player.EmotionColor(player.Current);
            Console.SetCursorPosition(13 + player.Name.Length, 17); Console.Write(skillInfo[0]);
            UpdateInput(player, monster, 3);
            PressEnter();

            //Draw Second Line (less Static)
            current[2] = player.Name + " ";
            //DarkCyan
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.SetCursorPosition(7, 18); Console.Write(player.Name);
            //Set proper cursor for following branch formatting
            x = 8 + player.Name.Length;
            Console.SetCursorPosition(x, 18);



            switch (thisSkill.Type) //Different formatting based on skill type
            {
                case SkillType.Offense: //Will be targetting the monster
                    int damageMod = 0;
                    Random rand = new Random();
                    switch (player.PrimaryAttribute)
                    {
                        case Attributes.Strength:
                            damageMod = playerAttrs[0] / 7;
                            break;
                        case Attributes.Defense:
                            damageMod = playerAttrs[1] / 7;
                            break;
                        case Attributes.Intelligence:
                            damageMod = playerAttrs[2] / 7;
                            break;
                        case Attributes.Wisdom:
                            damageMod = playerAttrs[3] / 7;
                            break;
                        case Attributes.Dexterity:
                            damageMod = playerAttrs[4] / 7;
                            break;
                        case Attributes.Agility:
                            damageMod = playerAttrs[5] / 7;
                            break;
                    }
                    damageMod = thisSkill.Damage + rand.Next(-2, 2) + (damageMod);
                    skillInfo[3] = damageMod.ToString();
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("hits"); x += 5; current[2] += "hits ";
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(x, 18);
                    Console.Write("{0}", monster.Name); x += 1 + monster.Name.Length; current[2] += monster.Name + " ";
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.SetCursorPosition(x, 18);
                    Console.Write("for {0} damage{1}.",
                        skillInfo[3],
                        skillInfo[2] == "one" ? temp = "" :
                        temp = " " + skillInfo[2] + " times");
                    current[2] += "for " + skillInfo[3] + " damage" + temp + ". ";
                    temp = "for " + skillInfo[3] + " damage" + temp + ". ";
                    x += temp.Length;
                    int damageTimes = thisSkill.DamageType == DamageType.Single ? 1 :
                        thisSkill.DamageType == DamageType.Double ? 2 :
                        thisSkill.DamageType == DamageType.Triple ? 3 :
                        thisSkill.DamageType == DamageType.Quadruple ? 4 : 1;
                    monster.Shield -= (damageMod * damageTimes);
                    if (monster.Shield < 0)
                    {
                        monster.Life += monster.Shield;
                        monster.Shield = 0;
                    }
                    Update(player, monster);
                    if (monster.Life > 0 & thisSkill.Status != StatusEffect.None)
                    {
                        PressEnter();
                        bool affects = player.DoesStatus(thisSkill, player, monster);
                        if (affects)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.SetCursorPosition(x, 18);
                            Console.Write(monster.Name); x += monster.Name.Length + 1;
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.SetCursorPosition(x, 18);
                            Console.Write(skillInfo[4]);
                            current[2] += " " + monster.Name + " " + skillInfo[4];

                            monster.Status[0] = false;
                            monster.StatusDuration[(int)thisSkill.Status] = thisSkill.StatusDuration;
                            monster.Status[(int)thisSkill.Status] = true;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.SetCursorPosition(x, 18);
                            string wording = thisSkill.Type == SkillType.Offense ? "resisted" : "failed";
                            Console.Write("Status effect {0}.", wording);
                            current[2] += " Status effect " + wording + ".";
                        }
                    }
                    else
                    {
                        //It will have a different effect
                    }
                    break;

                case SkillType.Support: //Will be affecting the player //Continue Here
                                        //Similar string formatting between heals and shields

                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(skillInfo[2]); current[2] += skillInfo[2];
                    if (thisSkill.DamageType == DamageType.Heal)
                    {
                        int maxLife = player.FindLife();
                        player.Life += thisSkill.Damage * (player.Intelligence / 10);
                        player.Life = player.Life > maxLife ? maxLife : player.Life;
                        skillInfo[3] = (thisSkill.Damage * (player.Intelligence / 10)).ToString();
                        Console.ForegroundColor = ConsoleColor.Green; Console.Write(" " + skillInfo[3]);
                        Console.ForegroundColor = ConsoleColor.White; Console.Write(" life.");
                        current[2] += " " + skillInfo[3] + " life.";
                    }
                    if (thisSkill.DamageType == DamageType.Shield)
                    {
                        player.GetShield(thisSkill.Damage + (player.Wisdom / 10));
                        skillInfo[3] = (player.Shield + (player.Wisdom/10)).ToString();
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write(" " + skillInfo[3]);
                        current[2] += " " + skillInfo[3] + " damage.";
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(" damage.");
                    }
                    if (thisSkill.DamageType == DamageType.Boost)
                    {
                        player.Boosts[thisSkill.BoostStat, 0] = 1;
                        player.Boosts[thisSkill.BoostStat, 1] = thisSkill.Damage;
                        player.Boosts[thisSkill.BoostStat, 2] = thisSkill.BoostDuration;
                    }
                    Update(player, monster);
                    if (thisSkill.Status != StatusEffect.None)
                    {
                        PressEnter();
                        bool effects = player.DoesStatus(thisSkill, player, monster);
                        if (effects)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            Console.SetCursorPosition(current[2].Length + 9, 18);
                            Console.Write(player.Name); x = current[2].Length + player.Name.Length + 9;
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.SetCursorPosition(x, 18);
                            Console.Write(" " + skillInfo[4]);
                            current[2] += " " + player.Name + " " + skillInfo[4];

                            player.Status[0] = false;
                            player.StatusDuration[(int)thisSkill.Status] = thisSkill.StatusDuration;
                            player.Status[(int)thisSkill.Status] = true;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.SetCursorPosition(current[2].Length + 9, 18);
                            Console.Write("Status effect failed.");
                            current[2] += " Status effect failed.";
                        }
                        Update(player, monster);
                    }
                    else
                    {
                        //It will have a different effect...
                        //Swapping emotions
                        //Emotion Specific Offensive/Support-
                    }

                    break;
            }
            PressEnter();
            if (monster.Life != 0)
            {
                UpdateSequence(1, current, previous, player, monster);
            }
            return current;
        }

        //TODO public static string[] MonsterSkill(Monster monster, Player player, string[] turnCurrent)

        //Focus Method
        public static string[] Focus(Player player, Monster monster, Emotion emotion, string[] turnCurrent)
        {
            string[] previous = turnCurrent;
            string[] current = new string[3];
            UpdateInput(player, monster, 3);
            current[0] = "P";
            current[1] = player.Name + "focuses on " + emotion + ".";
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.SetCursorPosition(7, 17); Console.Write(player.Name);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" focuses on ");
            Console.ForegroundColor = player.EmotionColor(emotion);
            Console.Write(emotion);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(".");
            PressEnter();
            current[2] = player.PrimaryAttribute + " boosted for 2 turns!";
            Console.SetCursorPosition(7, 18);
            Console.Write(current[2]);
            PressEnter();
            switch (player.PrimaryAttribute)
            {
                case Attributes.Strength: //Str Boost
                    player.Boosts[0, 0] = 1;
                    player.Boosts[0, 1] = 10;
                    player.Boosts[0, 2] = 2;
                    break;
                case Attributes.Defense: //Def Boost
                    player.Boosts[1, 0] = 1;
                    player.Boosts[1, 1] = 10;
                    player.Boosts[1, 2] = 2;
                    break;
                case Attributes.Intelligence: //Int Boost
                    player.Boosts[2, 0] = 1;
                    player.Boosts[2, 1] = 10;
                    player.Boosts[2, 2] = 2;
                    break;
                case Attributes.Wisdom: //Wis Boost
                    player.Boosts[3, 0] = 1;
                    player.Boosts[3, 1] = 10;
                    player.Boosts[3, 2] = 2;
                    break;
                case Attributes.Dexterity: //Dex Boost
                    player.Boosts[4, 0] = 1;
                    player.Boosts[4, 1] = 10;
                    player.Boosts[4, 2] = 2;
                    break;
                case Attributes.Agility: //Agi Boost
                    player.Boosts[5, 0] = 1;
                    player.Boosts[5, 1] = 10;
                    player.Boosts[5, 2] = 2;
                    break;
            }
            UpdateSequence(1, current, previous, player, monster);
            return current;
        }
        //"Initialize"
        public static void DrawDisplay(Player player, Monster monster)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            for (int b = 1; b < 30; b++)
            {
                Console.SetCursorPosition(0, b);
                if (b > 0 && b < 6) { Console.Write("]"); } else if (b % 2 != 0) { Console.Write("]"); } else { Console.Write("|"); }
                Console.SetCursorPosition(1, b);
                if (b >= 22 & b <= 27) { Console.Write("["); }
                Console.SetCursorPosition(118, b);
                if (b > 0 && b < 6) { Console.Write(" ["); } else if (b % 2 != 0) { Console.Write(" ["); } else { Console.Write(" |"); }
                Console.SetCursorPosition(118, b);
                if (b >= 22 & b <= 27) { Console.Write("]"); }
            }

            Console.SetCursorPosition(0, 0);
            Console.Write("//````````````````````````````````````````````````````````````````````````````````````````````````````````````````````\\\\");

            Console.SetCursorPosition(1, 6);
            Console.Write("\\____________________________________________________________________________________________________________________/");
            Console.SetCursorPosition(1, 28);
            Console.Write("\\\\__________________________________________________________________________________________________________________//");
            Console.SetCursorPosition(0, 29);
            Console.Write("][][][][][][][][][][][][][][][][][][][][][][][][][][][][][][][][][][][][][][][][][][][][][][][][][][][][][][][][][][][]");
            for (int f = 0; f < 7; f++)
            {
                Console.SetCursorPosition(59, f); Console.Write("||");
            }
            Console.SetCursorPosition(1, 21); Console.Write("//``````````````````````````````````````````````````````````````````````````````````````````````````````````````````\\\\");
            Console.SetCursorPosition(1, 21); Console.Write("//``````````````````````````````````````````````````````````````````````````````````````````````````````````````````\\\\");

            //BufferConsole.SetCursorPosition(118, 7); Console.Write("\\");
            Console.SetCursorPosition(1, 20); Console.Write("\\____________________________________________________________________________________________________________________/");
            //Turn Zones
            //Zone 1
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.SetCursorPosition(1, 8); Console.Write("---[");
            Console.SetCursorPosition(1, 9); Console.Write("   [");
            Console.SetCursorPosition(1, 10); Console.Write("---[");
            //Zone 2
            Console.SetCursorPosition(1, 12); Console.Write("---[");
            Console.SetCursorPosition(1, 13); Console.Write("   [");
            Console.SetCursorPosition(1, 14); Console.Write("---[");
            //Zone 3
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(1, 16); Console.Write("---[");
            Console.SetCursorPosition(1, 17); Console.Write("   [");
            Console.SetCursorPosition(1, 18); Console.Write("---[");


            //White Zones
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(51, 22); Console.Write("//Select Action");
            Console.SetCursorPosition(13, 24); Console.Write(" //");
            Console.SetCursorPosition(13, 25); Console.Write("{<<<<");
            Console.SetCursorPosition(13, 26); Console.Write(" \\\\");

            Console.SetCursorPosition(103, 24); Console.Write("  \\\\");
            Console.SetCursorPosition(103, 25); Console.Write(">>>>}");
            Console.SetCursorPosition(103, 26); Console.Write("  //");

            //Turn Zone Declaration
            Console.SetCursorPosition(1, 7); Console.Write("//Battle Sequence:");

            //Selection Zone
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.SetCursorPosition(25, 23); Console.Write(" _____________________________________________________________________");
            Console.SetCursorPosition(25, 24); Console.Write("/");
            Console.SetCursorPosition(24, 25); Console.Write("||");
            Console.SetCursorPosition(25, 26); Console.Write("\\_____________________________________________________________________/");
            Console.SetCursorPosition(25, 27); Console.Write(" \\___________________________________________________________________/");

            Console.SetCursorPosition(94, 24); Console.Write(" \\");
            Console.SetCursorPosition(94, 25); Console.Write(" ||");






            Console.ForegroundColor = ConsoleColor.White;
            //Names
            Console.SetCursorPosition(5, 1); Console.Write("//"); Console.SetCursorPosition(65, 1); Console.Write("//");
            //Emotion
            Console.SetCursorPosition(35, 1); Console.Write("//Emotion:");

            //Lifes
            Console.SetCursorPosition(5, 2); Console.Write("Life:"); Console.SetCursorPosition(65, 2); Console.Write("Life:");
            //Resources
            Console.SetCursorPosition(35, 2); Console.Write("{0}:", player.ResourceAttribute);
            Console.SetCursorPosition(95, 2); Console.Write("{0}:", monster.ResourceAttribute);

            //Status
            Console.SetCursorPosition(5, 3); Console.Write("Statuses:"); Console.SetCursorPosition(65, 3); Console.Write("Statuses:");

            //Action Bars
            Console.SetCursorPosition(37, 25); Console.Write("//");
            Console.SetCursorPosition(55, 25); Console.Write("//");
            Console.SetCursorPosition(72, 25); Console.Write("//");

            //Names cont.
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.SetCursorPosition(7, 1); Console.Write(player.Name);

            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(67, 1); Console.Write(monster.Name);

            //UPDATES

            Update(player, monster);
            UpdateInput(player, monster, 1);
            //string[] fillBlank = new string[3] { "M", "Did This", "Caused This" }; // "M" for Monster's Name "'s previous turn
            //string[] fill = new string[3] {"P", "Did this", "Caused This"}; // "P" for Player's Name "'s previous turn"
            string[] fillBlank = new string[3] { "", "", "" }; // "M" for Monster's Name "'s previous turn
            string[] fill = new string[3] { "", "", "" }; // "P" for Player's Name "'s previous turn"
            UpdateSequence(0, fill, fillBlank, player, monster); //(turn, previous turn string array, previous previous turn string array)
                                                                 //Switch to DoLoop--- BattleLoop(player, monster);

        }

        //UPDATES
        public static void ClearInput()
        {
            for (int x = 3; x < 117; x++)
            {
                Console.SetCursorPosition(x, 22); Console.Write("  ");
                if (x > 26 & x < 95)
                {
                    Console.SetCursorPosition(x, 25); Console.Write(" ");
                }
            }
        }

        public static void UpdateSequence(int turn, string[] current, string[] previous, Player player, Monster monster)
        {
            //Private Variables
            string zone_1 = previous[0] == "M" ? monster.Name : previous[0] == "P" ? player.Name : "";
            string zone_2 = current[0] == "M" ? monster.Name : player.Name;
            string zone_3 = turn == 0 ? player.Name : current[0] == "M" ? player.Name : monster.Name;
            //Clear All 3 Zones
            for (int y = 8; y < 19; y++)
            {
                Console.SetCursorPosition(7, y); Console.Write("                                                                                                               ");
            }
            //Write Zone 1 w/ previous
            Console.ForegroundColor = turn == 0 ? ConsoleColor.White : previous[0] == "M" ? ConsoleColor.Red : ConsoleColor.DarkCyan;
            Console.SetCursorPosition(7, 8); Console.Write("{0}", turn != 0 ? zone_1 : "");
            //Write Zone 2 w/ current
            Console.ForegroundColor = current[0] == "M" ? ConsoleColor.Red : ConsoleColor.DarkCyan;
            Console.SetCursorPosition(7, 12); Console.Write("{0}", turn != 0 ? zone_2 : "");
            //Write Zone 3 based on current
            Console.ForegroundColor = turn == 0 || current[0] == "M" ? ConsoleColor.DarkCyan : ConsoleColor.Red;
            Console.SetCursorPosition(21, 16); Console.Write("{0}", zone_3);

            //Gray Areas
            Console.ForegroundColor = ConsoleColor.Gray;
            //Line 1
            //z1
            Console.SetCursorPosition(7 + zone_1.Length, 8); Console.Write("{0}", zone_1 == "" ? "" : turn != 0 ? "'s Previous Turn:" : "");
            //z2
            Console.SetCursorPosition(7 + zone_2.Length, 12); Console.Write("{0}", turn != 0 ? "'s Previous Turn:" : "");

            //DarkGray Areas
            Console.ForegroundColor = ConsoleColor.DarkGray;
            //Line 2 + 3
            //z1
            Console.SetCursorPosition(7, 9); Console.Write(previous[1]);
            Console.SetCursorPosition(7, 10); Console.Write(previous[2]);
            //z2
            Console.SetCursorPosition(7, 13); Console.Write(current[1]);
            Console.SetCursorPosition(7, 14); Console.Write(current[2]);

            //White Areas
            Console.ForegroundColor = ConsoleColor.White;
            //Zone 3
            //1
            Console.SetCursorPosition(7, 16); Console.Write("Current Turn:");
            //2
            //3

        }

        public static void Update(Player player, Monster monster)
        {
            //Will Move to UPDATE()
            //LIFE UPDATES
            Console.SetCursorPosition(11, 2); Console.Write("                 "); player.LifeColor();
            Console.SetCursorPosition(11, 2); Console.Write("{0}/{1}", player.Life, player.FindLife());
            Console.ForegroundColor = ConsoleColor.Cyan;
            if (player.Shield > 0) { Console.Write(" ({0})", player.Shield); }

            //EMOTION UPDATES
            Console.ForegroundColor = player.EmotionColor(player.Current);
            Console.SetCursorPosition(46, 1); Console.Write("             ");
            Console.SetCursorPosition(46, 1); Console.Write(player.Current);

            Console.SetCursorPosition(71, 2); Console.Write("         ");
            Console.SetCursorPosition(71, 2); monster.LifeColor(); Console.Write("{0}/{1}", monster.Life, monster.FindLife());
            //RESOURCE UPDATES
            Console.SetCursorPosition(37 + player.ResourceAttribute.ToString().Length, 2); Console.Write("           ");
            Console.SetCursorPosition(37 + player.ResourceAttribute.ToString().Length, 2); player.ResourceColor(); Console.Write("{0}/{1}", player.Resource, player.FindResource());
            Console.SetCursorPosition(97 + monster.ResourceAttribute.ToString().Length, 2); Console.Write("           ");
            Console.SetCursorPosition(97 + monster.ResourceAttribute.ToString().Length, 2); monster.ResourceColor(); Console.Write("{0}/{1}", monster.Resource, monster.ResourceMax);

            //STATUS UPDATES
            List<string> currentstatuses = new List<string> { }; int counter = 0;
            StatusEffect[] effects = {StatusEffect.None, StatusEffect.Tower, StatusEffect.Tremble, StatusEffect.Certain, StatusEffect.Conflict,
            StatusEffect.Presence, StatusEffect.Whimsy, StatusEffect.Nimble, StatusEffect.Fumble, StatusEffect.Rage, StatusEffect.Stagger,
            StatusEffect.Resilient, StatusEffect.Fade, StatusEffect.Relish, StatusEffect.Sink, StatusEffect.Ready, StatusEffect.Unsteady };
            int x = 5; int y = 4;
            int x2 = 5; int y2 = 5;
            Console.SetCursorPosition(x, y); Console.Write("                              ");
            Console.SetCursorPosition(x2, y2); Console.Write("                              ");
            foreach (bool status in player.Status)
            {
                if (status)
                {
                    currentstatuses.Add(effects[counter].ToString());
                    Console.ForegroundColor = ConsoleColor.White;

                    if (currentstatuses.Count <= 4)
                    {
                        Console.SetCursorPosition(x, y); Console.Write("{0}", currentstatuses.Count > 1 ? "," : ""); x += 2;
                        Console.ForegroundColor = counter == 0 ? ConsoleColor.DarkGray : counter % 2 == 0 ? ConsoleColor.Red : ConsoleColor.Green;
                        Console.SetCursorPosition(x, y); Console.Write("{0}", effects[counter].ToString());
                        x += effects[counter].ToString().Length;
                    }
                    else if (currentstatuses.Count <= 8)
                    {
                        Console.SetCursorPosition(x2, y2); Console.Write("{0}", currentstatuses.Count > 5 ? "," : ""); x2 += 2;
                        Console.ForegroundColor = counter % 2 == 0 ? ConsoleColor.Red : ConsoleColor.Green;
                        Console.SetCursorPosition(x2, y2); Console.Write("{0}", effects[counter].ToString());
                        x2 += effects[counter].ToString().Length;
                    }
                    else { Console.SetCursorPosition(x2, y2); Console.Write("..."); }
                }
                counter++;
            }
            x = 65; y = 4;
            x2 = 65; y2 = 5;
            Console.SetCursorPosition(x, y); Console.Write("                              ");
            Console.SetCursorPosition(x2, y2); Console.Write("                              ");
            currentstatuses = new List<string> { }; counter = 0;
            foreach (bool status in monster.Status)
            {
                if (status)
                {
                    currentstatuses.Add(effects[counter].ToString());
                    Console.ForegroundColor = ConsoleColor.White;

                    if (currentstatuses.Count <= 4)
                    {
                        Console.SetCursorPosition(x, y); Console.Write("{0}", currentstatuses.Count > 1 ? "," : ""); x += 2;
                        Console.ForegroundColor = counter == 0 ? ConsoleColor.DarkGray : counter % 2 == 0 ? ConsoleColor.Red : ConsoleColor.Green;
                        Console.SetCursorPosition(x, y); Console.Write("{0}", effects[counter].ToString());
                        x += effects[counter].ToString().Length;
                    }
                    else if (currentstatuses.Count <= 8)
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
        }

        public static void UpdateInput(Player player, Monster monster, int selector)
        {
            ClearInput();
            if (selector < 3)
            {
                Console.ForegroundColor = selector != -2 ? ConsoleColor.White : ConsoleColor.DarkGray;
                Console.SetCursorPosition(51, 22); Console.Write("{0}", selector != -2 ? "//Select Action" : "//Monster's Turn");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.SetCursorPosition(37, 25); Console.Write("\\\\Attack");
                Console.SetCursorPosition(55, 25); Console.Write("\\\\Focus");
                Console.SetCursorPosition(72, 25); Console.Write("\\\\Skill");
                switch (selector)
                {

                    case 0://Attack
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.SetCursorPosition(37, 25); Console.Write("//Attack");
                        break;

                    case 1://Focus-Default
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.SetCursorPosition(55, 25); Console.Write("//Focus");
                        break;

                    case 2://Skill
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.SetCursorPosition(72, 25); Console.Write("//Skill");
                        break;
                }
            }
            else
            {
                switch (selector)
                {
                    case 3:
                        //Monster's Turn/Static "Continue"
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.SetCursorPosition(51, 22); Console.Write("//Select Action");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.SetCursorPosition(53, 25); Console.Write("//Continue");
                        break;
                    case 4:
                        #region SkillMenu
                        //Skill Menu
                        player.SelectedSkill[0] = -1;
                        //Draws First skill in player's currently selected skill
                        Console.ForegroundColor = ConsoleColor.White;
                        //Get first index for Skills reference
                        int current = player.Current == player.Primary ? 1 : player.Current == player.Secondary ? 2 : player.Current == player.Tertiary ? 3 : 0;
                        //Get second index for Skills reference
                        int selectSkill = 0;
                        //Make private string[,] to hold available skills information pertinent to writing them on screen
                        //TODO string[,] skillInfo = player.GetSkill()
                        string[,] skillInfo = new string[player.Skills.GetLength(1), 5];
                        string[] skillTemp;
                        for (int a = 0; a < player.Skills.GetLength(1); a++)
                        {
                            skillTemp = player.Skills[current, a].GetSkill();
                            for (int b = 0; b < 5; b++)
                            {
                                skillInfo[a, b] = skillTemp[b];
                            }

                        }
                        int x = 3; int y = 22;
                        //REFERENCE TO OBTAINING SKILL INFO
                        //[ 0: Name, 1: Description, 2: DamageType, 3: Damage

                        bool refresh = true;
                        bool loopOn = true;
                        do
                        {
                            if (refresh)
                            {
                                ClearInput();
                                refresh = false;
                            }
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.SetCursorPosition(3, 22); Console.Write("//Select Skill:"); x += 16;
                            Console.SetCursorPosition(19, 22);
                            Console.ForegroundColor = player.EmotionColor(player.Current);
                            Console.Write("{0}- ", skillInfo[selectSkill, 0]);
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write("{0}", skillInfo[selectSkill, 1]);
                            //Draw "selected" skill
                            Console.ForegroundColor = ConsoleColor.Green;
                            x = 59;
                            x -= (skillInfo[selectSkill, 0].Length) / 2;
                            Console.SetCursorPosition(x, 25); Console.Write("//{0}", skillInfo[selectSkill, 0]);
                            //Draw "left next" skill
                            Console.ForegroundColor = ConsoleColor.Gray;
                            x = 29;
                            Console.SetCursorPosition(x, 25); Console.Write("{0}",
                                selectSkill != 0 ? "\\\\" + skillInfo[selectSkill - 1, 0] : "");
                            //Draw "right next" skill
                            if (selectSkill + 1 < player.Skills.GetLength(1))
                            {
                                x = skillInfo[selectSkill + 1, 0].Length > 0 ? 89 - skillInfo[selectSkill + 1, 0].Length : 83;
                                Console.SetCursorPosition(x, 25); Console.Write("{0}",
                                    skillInfo[selectSkill + 1, 0] != "" ? "\\\\" + skillInfo[selectSkill + 1, 0] : "");

                            }
                            switch (Console.ReadKey(true).Key)
                            {
                                case ConsoleKey.LeftArrow://Move "selected" to the right, to get the element that is at the left. 
                                case ConsoleKey.A:
                                case ConsoleKey.NumPad4:
                                    selectSkill -= selectSkill != 0 ? 1 : -(player.Skills.GetLength(1) - 1);
                                    refresh = true;
                                    break;
                                case ConsoleKey.RightArrow://Move "selected" to the left, to get the element that is at the right. 
                                case ConsoleKey.D:
                                case ConsoleKey.NumPad6:
                                case ConsoleKey.Tab:
                                case ConsoleKey.Add:
                                    selectSkill += selectSkill != player.Skills.GetLength(1) - 1 ? 1 : -(player.Skills.GetLength(1) - 1);
                                    refresh = true;
                                    break;
                                case ConsoleKey.Enter:
                                case ConsoleKey.F:
                                case ConsoleKey.NumPad5:
                                    //Select Current Skill... DoSkill(Xselector, Yselector)
                                    if (player.Resource >= player.Skills[current, selectSkill].Cost)
                                    {
                                        player.SelectedSkill[0] = current;
                                        player.SelectedSkill[1] = selectSkill;
                                        player.Resource -= player.Skills[current, selectSkill].Cost;
                                        Update(player, monster);
                                        loopOn = false;
                                    }
                                    else
                                    {
                                        Console.ForegroundColor = ConsoleColor.DarkGray;
                                        Console.SetCursorPosition(7, 17);
                                        Console.Write("                                                           ");
                                        Console.SetCursorPosition(7, 17);
                                        Console.Write("Insufficient {0}!", player.ResourceAttribute);
                                        loopOn = false;
                                    }
                                    break;
                                case ConsoleKey.Escape:
                                case ConsoleKey.Subtract:
                                    loopOn = false;
                                    player.SelectedSkill[0] = -1;
                                    break;
                            }
                        } while (loopOn);
                        #endregion
                        break;
                    case 5:
                        #region FocusEmotions
                        int picker = 0;
                        Emotion temp = Emotion.Null;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.SetCursorPosition(47, 22);
                        Console.Write("//Select Emotional Focus");
                        bool menuOn = true;
                        do
                        {
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.SetCursorPosition(37 - (player.Primary.ToString().Length / 2), 25);
                            Console.Write("\\\\{0}", player.Primary);
                            Console.SetCursorPosition(57 - (player.Secondary.ToString().Length / 2), 25);
                            Console.Write("\\\\{0}", player.Secondary);
                            Console.SetCursorPosition(77 - (player.Tertiary.ToString().Length / 2), 25);
                            Console.Write("\\\\{0}", player.Tertiary);
                            switch (picker)
                            {
                                case 0:
                                    Console.ForegroundColor = player.EmotionColor(player.Primary);
                                    Console.SetCursorPosition(37 - (player.Primary.ToString().Length / 2), 25);
                                    Console.Write("//{0}", player.Primary);
                                    break;
                                case 1:
                                    Console.ForegroundColor = player.EmotionColor(player.Secondary);
                                    Console.SetCursorPosition(57 - (player.Secondary.ToString().Length / 2), 25);
                                    Console.Write("//{0}", player.Secondary);
                                    break;
                                case 2:
                                    Console.ForegroundColor = player.EmotionColor(player.Tertiary);
                                    Console.SetCursorPosition(77 - (player.Tertiary.ToString().Length / 2), 25);
                                    Console.Write("//{0}", player.Tertiary);
                                    break;
                            }
                            switch (Console.ReadKey(true).Key)
                            {
                                case ConsoleKey.LeftArrow:
                                case ConsoleKey.A:
                                case ConsoleKey.NumPad4:
                                    if (picker != 0) { picker--; }
                                    break;
                                case ConsoleKey.RightArrow:
                                case ConsoleKey.D:
                                case ConsoleKey.NumPad6:
                                case ConsoleKey.Tab:
                                case ConsoleKey.Add:
                                    if (picker != 2) { picker++; }
                                    break;
                                case ConsoleKey.Enter:
                                case ConsoleKey.F:
                                case ConsoleKey.NumPad5:
                                    switch (picker)
                                    {
                                        case 0:
                                            temp = player.Primary;
                                            player.SelectedSkill[0] = -3;
                                            break;
                                        case 1:
                                            temp = player.Secondary;
                                            player.SelectedSkill[0] = -4;
                                            break;
                                        case 2:
                                            temp = player.Tertiary;
                                            player.SelectedSkill[0] = -5;
                                            break;
                                    }
                                    player.Current = temp;
                                    Update(player, monster);
                                    menuOn = false;
                                    break;
                                case ConsoleKey.Escape:
                                case ConsoleKey.Subtract:
                                    player.SelectedSkill[0] = -2;
                                    menuOn = false;
                                    break;
                            }

                        } while (menuOn);
                        #endregion
                        break;
                }

            }
        }

        public static void UpdateStatus(Character character)
        {
            //The idea here is to update the duration of status, and then flip the corresponding bool off, if 0
            //This method will be followed by an Update(player, monster) and used at the end of the previous turn...
            for (int i = 0; i < 17; i++)
            {
                character.StatusDuration[i] -= character.StatusDuration[i] > 0 ? 1 : 0;
                if (character.StatusDuration[i] == 0 & i != 0)
                {
                    character.Status[i] = false;
                }
            }
            int checker = 0;
            for (int i = 1; i < 17; i++)
            {
                checker += character.Status[i] ? 1 : 0;
            }
            character.Status[0] = checker == 0 ? true : false;
        }
        //Other functionality

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

        public static void EndProcessing(Player player)
        {
            player.Resource = player.ResourceAttribute == Attributes.Intuition ? 0 : player.ResourceAttribute == Attributes.Stamina ?
                player.FindResource() : player.Resource;

            player.Shield = 0;
            player.Boosts = new int[14, 3];
            for (int a = 0; a < 14; a++)
            {
                for (int b = 0; b < 3; b++)
                {
                    player.Boosts[a, b] = 0;
                }
            }
            player.StatusDuration = new int[17] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };//Might not do this
            int index = 0;
            foreach (bool status in player.Status)
            {
                player.Status[index] = false;
                index++;
            }
            player.Status[0] = true;
            player.SelectedSkill = new int[2] { -1, 0 };
        }

    }
}

