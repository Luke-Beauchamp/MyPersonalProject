using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CharacterLibrary;
using PlayerLibrary;
using MonsterLibrary;
using MapLibrary;
using MenuLibrary;

namespace SceneLibrary
{
    public class Scene
    {
        public static void InputClear()
        {
            //(2, 23) - Ending at(102, 27)
            for (int y = 23; y < 28; y++)
            {
                for (int x = 2; x < 102; x++)
                {
                    Console.SetCursorPosition(x, y); Console.Write(" ");
                }
                Console.SetCursorPosition(2, 28); Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.Write("\\__________________________________________________________________________________________________/");
            }
        }

        public static bool Room(Room room, Player player)
        {
            bool mainLoop = true;
            CharacterInfo.ClearArea();
            Scene.InputClear();
            MenuDoes.CommandClear();
            MenuDoes.SceneClear();
            MenuDoes.DrawMenu();
            CharacterInfo.ShortSheet(player);
            MenuDoes.CommandToggle(2, player.zToggle, player);
            Random rand = new Random();
            if (!room.HasBeenIn)
            {
                //In this branch, check the room's type and make it has been in
                if (room.Type != RoomType.Boss)
                {
                    if (rand.Next(player.Floor*2) >= player.Floor)
                    {
                        room.HasBeenIn = true;
                        room.Character = SceneSocial.CreateNPC(player);
                        SceneSocial.Socialize(player, room.Character);// Could transfer to a scenebattle
                    }
                    else
                    {
                        Monster monster = SceneSocial.GetMonster(player.Floor);
                        monster.Define(rand.Next(1, 5), player.Floor);
                        Console.Clear();
                        SceneBattle.DrawDisplay(player, monster);
                        SceneBattle.BattleLoop(player, monster);
                    }
                }
                else
                {

                    //Scene.Boss();
                    mainLoop = false;
                    //Scene.AfterBoss();
                    Console.Clear();
                    SceneSocial.DiceVendor(player);
                    Monster boss = SceneSocial.GetMonster(player.Floor);
                    boss.Define(5, player.Floor);
                    SceneBattle.DrawDisplay(player, boss);
                    SceneBattle.BattleLoop(player, boss);
                }
            }
            else
            {
                Monster monster = SceneSocial.GetMonster(player.Floor);
                monster.Define(rand.Next(1, 3), player.Floor);
                Console.Clear();
                SceneBattle.DrawDisplay(player, monster);
                SceneBattle.BattleLoop(player, monster);
            }
            return mainLoop;
        }

        public static Player Initialize()
        {
            Console.ForegroundColor = ConsoleColor.White;
            string[] welcome = { "" };
            int x = 5;
            int y = 2;
            Console.SetCursorPosition(x, y);
            Console.Write("|/\\\\  |     |  |  ||||  //``  |  |  ||||  |  //  /``\\");
            Console.SetCursorPosition(x, y + 1);
            Console.Write("| //  |     |  |    |   |     |__|    |   | //   \\__");
            Console.SetCursorPosition(x, y + 2);
            Console.Write("|/    |     |  |    |   |     |  |    |   |/\\\\      \\");
            Console.SetCursorPosition(x, y + 3);
            Console.Write("|     \\\\__  \\__/    |   \\___  |  |  __|_  /  \\\\  \\__/");
            Console.SetCursorPosition(x, y + 4);
            Console.Write("______________________________________________________");
            Console.SetCursorPosition(x, y + 5);
            Console.Write("______________________________________________________");
            y++;
            Console.SetCursorPosition(x + 12, y + 6);
            Console.Write("|/\\\\  |  |  |/\\\\  //`\\  //\\\\  ||||  /``\\  |/\\\\  \\  //");
            Console.SetCursorPosition(x + 12, y + 7);
            Console.Write("| //  |  |  | //  |     |__|    |   |  |  | //   \\//");
            Console.SetCursorPosition(x + 12, y + 8);
            Console.Write("|/    |  |  |\\\\   |  \\  |  |    |   |  |  |\\\\    ||");
            Console.SetCursorPosition(x + 12, y + 9);
            Console.Write("|     \\__/  |  \\  \\__/  |  |    |   \\__/  |  \\   ||");
            Console.SetCursorPosition(x + 12, y + 10);
            Console.Write("______________________________________________________");
            Console.SetCursorPosition(x + 12, y + 11);
            Console.Write("______________________________________________________");
            x = 5; y = y + 13;
            int selector = 0;
            bool selected = false;
            bool[] statuses = { true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
            Player player = new Player(Emotion.Anger, Emotion.Sadness, Emotion.Anticipation,
                "John Wilksonfer", statuses, 50, 50, 5, 5, 5, Attributes.Wisdom, 11, 7, 50, 4, 6, 3, 5);
            do
            {
                Console.SetCursorPosition(x, y); Console.ForegroundColor = selector == 0 ? ConsoleColor.Green : ConsoleColor.Gray;
                Console.Write("New Game");
                Console.SetCursorPosition(x, y + 1); Console.ForegroundColor = selector == 1 ? ConsoleColor.Green : ConsoleColor.Gray;
                Console.Write("Tutorial");
                Console.SetCursorPosition(x, y + 2); Console.ForegroundColor = selector == 2 ? ConsoleColor.Green : ConsoleColor.Gray;
                Console.Write("Exit");
                Console.SetCursorPosition(13, y);
                ConsoleKey userInput = Console.ReadKey(true).Key;
                switch (userInput)
                {
                    case ConsoleKey.DownArrow:
                        selector += selector != 2 ? 1 : -2;
                        break;
                    case ConsoleKey.UpArrow:
                        selector -= selector != 0 ? 1 : -2;
                        break;
                    case ConsoleKey.Enter:
                        switch (selector)
                        {
                            case 0:
                                Console.Clear();
                                selected = true;
                                //player = NewGame();
                                //player = CreatePlayer();
                                player = Tutorial(false);
                                break;
                            case 1:
                                Console.SetCursorPosition(5, 17);
                                Console.Write("Not yet...");
                                Console.ReadKey(true);
                                player = Tutorial(false);

                                selected = true;
                                break;
                            case 2:
                                Console.Clear();
                                Console.Write("Another day perhaps...");
                                Environment.Exit(0);
                                break;
                        }
                        break;
                }
            } while (!selected);
            Console.Clear();
            return player;
        }

        public static Player Tutorial(bool on)//TODO FINISH THIS
        {
            Console.CursorVisible = false;
            Attributes primaryAttribute = Attributes.Agility;
            Player player = new Player(0, primaryAttribute);
            player.Current = Emotion.Null;
            //CharacterInfo.ClearArea();
            //Scene.InputClear();
            //MenuDoes.CommandClear();
            //MenuDoes.SceneClear();
            string[] custom = new string[7] { "", "", "", "", "", "", "" };
            Console.Clear();
            MenuDoes.DrawMenu();
            SceneSocial.DrawScene(player);
            //Let's begin the tutorial.
            #region Get Player Name

            bool choosing = false;
            string playerName = "";
            do
            {
                CharacterInfo.ClearArea();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(11, 1); SlowWrite(new string[] { "W","h","a","t"," ","i","s"," ","y","o","u","r"," ",
                "n","a","m","e","?"," " });
                Console.ForegroundColor = ConsoleColor.Gray; SlowWrite(new string[] { "(","c","o","n","f","i","r","m"," ",
                "s","e","l","e","c","t","i","o","n"," ","a","f","t","e","r"," ","y","o","u"," ","t","y","p","e"," ","i","n"," ",
                "y","o","u","r"," ","n","a","m","e"," ","b","y"," ","p","r","e","s","s","i","n","g"," ","E","N","T","E","R",")" });
                MenuDoes.CommandClear();
                SceneSocial.DrawInput(new string[] { "//Type Your Name" }, 0);
                playerName = PlayerName();
                CharacterInfo.ClearArea();
                SceneSocial.DrawInput(new string[] { "//Confirm " + playerName }, 0);
                choosing = Confirm(playerName, 11, 1, player);
            } while (!choosing);
            CharacterInfo.ClearArea();
            #endregion
            #region First Tutorial \ Basic Selections
            if (on)
            {
                SceneSocial.DrawInput(new string[] { "" }, 0);
                CharacterInfo.ClearArea();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(11, 1); SlowWrite(new string[] { "H", "e", "l", "l", "o", ",", " ", "w", "a", "r", "y",
            " ", "s", "o", "u", "l", ".", ".", ".", " ", "Y", "o", "u", " ", "c", "a", "n", " ", "u", "s", "e", " ",
            "t", "h", "e", " ", "c", "o", "m", "m", "a", "n", "d", "s", " ", "l", "i", "s", "t", "e", "d", " ",
            "t", "o", " ", "t", "h", "e", " ", "r", "i", "g", "h", "t", " ", "o", "f", " ", "t", "h", "e", " ",
            "w", "i", "n", "d", "o", "w", " ", "t", "o", " ", "l", "e", "a", "r", "n", " ", "h", "o", "w", " ",
            "t", "o", " ", "s", "t", "a", "r", "t"});
                Console.SetCursorPosition(11, 2); SlowWrite(new string[] { "i", "n", "t", "e", "r", "a", "c", "t", "i", "n", "g",
            " ", "w", "i", "t", "h", " ", "t", "h", "e", " ", "g", "a", "m", "e", ".", ".", "."," ","\"","N","P","\""," ",
                "s","t","a","n","d","s"," ","f","o","r"," "}); Console.ForegroundColor = ConsoleColor.White;
                Console.Write("\"NumPad\" "); Console.ForegroundColor = ConsoleColor.Green;
                SlowWrite(new string[]{"a","n","d"," ","i","s"," ","r","e","f","e","r","e","n","c","i","n","g"," ","t","h","e"," ",
                    "n","u","m","b","e","r"," ","p","a","d"," ","o","n"," ","t","h","e"," ","r","i","g","h","t" });
                Console.SetCursorPosition(11, 3); SlowWrite(new string[] { "o","f"," ","t","h","e"," ","k","e","y","b","o","a","r",
                "d","."," ","A","l","s","o"," ","n","o","t","e"," ","t","h","a","t"," ","e","v","e","r","y"," ",
                "c","o","m","m","a","n","d"," ","h","a","s"," ","a","n"," ","a","l","t","e","r","n","a","t","i","v","e"," ",
                "k","e","y"," ","f","o","r"," ","y","o","u","r"," ","g","a","m","i","n","g"," ",
                    "p","r","e","f","e","r","e","n","c","e","."});
                Console.SetCursorPosition(11, 4); Console.ForegroundColor = ConsoleColor.Red; Console.Write("!Caution! ");
                Console.ForegroundColor = ConsoleColor.Gray; SlowWrite(new string[] { "T","o"," ","u","s","e"," ","t","h","e"," ",
                "n","u","m","b","e","r"," ","p","a","d"," ","m","a","k","e"," ","s","u","r","e"," "});
                Console.ForegroundColor = ConsoleColor.White; Console.Write("NUMLOCK "); Console.ForegroundColor = ConsoleColor.Gray;
                SlowWrite(new string[] { "i", "s", " ", "o", "n","." });
                MenuDoes.CommandToggle(7, 1, player);
                int selected = 1;
                bool looped = true;
                do
                {
                    System.Threading.Thread.Sleep(77);
                    SceneSocial.DrawInput(new string[] { "//Wow!", "//Okay", "//Huh?" }, selected);
                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.Tab:
                        case ConsoleKey.Add:
                            switch (selected)
                            {
                                case 0:
                                case 1:
                                    selected++;
                                    break;
                                case 2:
                                    selected = 0;
                                    break;
                            }
                            break;
                        case ConsoleKey.F:
                        case ConsoleKey.NumPad5:
                            looped = false;
                            break;
                    }
                } while (looped);
                CharacterInfo.ClearArea();
            }
            #endregion
            #region Second Tutorial \ Basics and Directional Selection
            if (on)
            {
                SceneSocial.DrawInput(new string[] { "" }, 0);
                Console.SetCursorPosition(11, 1); SlowWrite(new string[] { "Y", "o", "u", " ", "w", "i", "l", "l", " ",
            "b", "e", " ", "p","r","o","m","p","t","e","d"," ","t","h","r","o","u","g","h","o","u","t"," ","t","h","e"," ",
            "g","a","m","e"," ","t","o", " ", "c", "h", "o", "o", "s", "e", " ", "a", " ", "r", "e", "s", "p", "o","n","s","e",
            " ","f","r","o","m"," ","a"," ","l","i","s","t", " ", "o","f"," ","r","e","s","p","o","n","s","e","s",
            " ","i","n"," ","t","h","e"," "});
                custom[1] = "makeitblue";
                SceneSocial.DrawInput(custom, 0); Console.ForegroundColor = ConsoleColor.Blue;
                Console.SetCursorPosition(11, 2); SlowWrite(new string[] { "/", "/","S", "e", "l", "e", "c", "t",
            " ","R","e","s","p","o","n","s","e"}); Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(28, 2); SlowWrite(new string[] { " ","a","r","e","a"," ","b","e","l","o","w",".",
            ".","."," ","I","f"," ","m","u","l","t","i","p","l","e"," ","r","e","s","p","o","n","s","e","s"," ","a","r","e",
            " ","a","v","a","i","l","a","b","l","e",","," ","t","h","e"," " });
                custom[1] = "";
                SceneSocial.DrawInput(custom, 0); Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(84, 2); SlowWrite(new string[] { "a", "r", "r", "o", "w", "s" });
                Console.ForegroundColor = ConsoleColor.Green; Console.SetCursorPosition(90, 2); SlowWrite(new string[] { " ","t","o",
            " ","t","h","e"," ","l","e","f","t"," ","a","n","d"," "});
                Console.SetCursorPosition(11, 3); SlowWrite(new string[] { "r","i","g","h","t"," ","o","f"," ","t","h","e",
            " ","\"","s","e","l","e","c","t","i","o","n"," ","b","o","x","\""," ","w","i","l","l"," ","t","u","r","n",
            " "});
                custom[1] = "makeitgreen";
                SceneSocial.DrawInput(custom, 0); Console.SetCursorPosition(49, 3); SlowWrite(new string[] { " ","g","r","e","e","n",
            ".",".","."," ","O","t","h","e","r","w","i","s","e",","," ","y","o","u","r"," ","O","N","L","Y"," ",
            "o","p","t","i","o","n"," ","w","i","l","l"," ","b","e"," ","l","i","s","t","e","d"," ","i","n"," ","t","h","e"});
                Console.SetCursorPosition(10, 4); SlowWrite(new string[] { " ","\"","s","e","l","e","c","t","i","o","n"," ",
            "b","o","x","\""," ","a","n","d"," ","t","h","e"," ","a","r","r","o","w","s"," ","w","i","l","l"," ",
            "t","u","r","n"," "}); Console.ForegroundColor = ConsoleColor.DarkGray; Console.SetCursorPosition(52, 4);
                SlowWrite(new string[] { "d", "a", "r", "k", " ", "g", "r", "a", "y" }); Console.SetCursorPosition(61, 4);
                Console.ForegroundColor = ConsoleColor.Green; SlowWrite(new string[] { ".", ".", "." });
                SceneSocial.DrawInput(new string[] { "//Okay" }, 0); SceneBattle.PressEnter();
                SceneSocial.DrawInput(new string[] { "" }, 0); CharacterInfo.ClearArea();

                Console.SetCursorPosition(11, 1); SlowWrite(new string[] { "I","n"," ","a","d","d","i","t","i","o","n"," ",
                "t","o"," ","\"","t","o","g","g","l","i","n","g","\""," ","t","h","r","o","u","g","h"," ",
                "r","e","s","p","o","n","s","e","s"," ","y","o","u"," ","c","a","n"," ","a","l","s","o"," ","u","s","e"," ",
                "d","i","r","e","c","t","i","o","n","a","l"," ","c","o","m","m","a","n","d","s"," ","t","o"," ",
                "n","a","v","i","g","a","t","e"," ","t","h","e"});
                Console.SetCursorPosition(11, 2); SlowWrite(new string[] { "a","v","a","i","l","a","b","l","e"," ",
                "r","e","s","p","o","n","s","e","s","."," ","T","a","k","e"," ","a"," ","m","o","m","e","n","t"," ","t","o"," ",
                    "f","a","m","i","l","i","a","r","i","z","e"," ","y","o","u","r","s","e","l","f"," ","w","i","t","h"," ",
                    "t","h","e"," ","c","o","m","m","a","n","d","s"," ","b","e","l","o","w","." });
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(23, 3); Console.Write("W, A, S, D :");
                Console.SetCursorPosition(11, 4); Console.Write("NumPad (NP) 8, 4, 2, 6 :");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.SetCursorPosition(36, 3); Console.Write("Select Up, Left, Down, Right");
                Console.SetCursorPosition(36, 4); Console.Write("Select Up, Left, Down, Right");
                int selected = 0;
                bool looped = true;
                do
                {
                    SceneSocial.DrawInput(new string[] { "Zero", "One", "Two", "//Continue" }, selected);
                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.Tab:
                        case ConsoleKey.Add:
                        case ConsoleKey.D:
                        case ConsoleKey.NumPad6:
                            selected += selected != 3 ? 1 : -3;
                            break;
                        case ConsoleKey.A:
                        case ConsoleKey.NumPad4:
                            selected -= selected != 0 ? 1 : -3;
                            break;
                        case ConsoleKey.F:
                        case ConsoleKey.NumPad5:
                            looped = selected == 3 ? false : true;
                            break;
                    }
                } while (looped);
                SceneSocial.DrawInput(new string[] { "" }, 0);
                CharacterInfo.ClearArea();
            }
            #endregion
            #region Third Tutorial \ Command Esc\-
            if (on)
            {
                Console.SetCursorPosition(11, 1); SlowWrite(new string[] { "T","h","r","o","u","g","h","o","u","t"," ",
                "t","h","i","s"," ","p","u","r","g","a","t","o","r","y"," ","y","o","u"," ","w","i","l","l"," ","c","o","m","e",
                " ","a","c","r","o","s","s"," ","o","t","h","e","r"," ","w","a","n","d","e","r","i","n","g"," ",
                    "s","o","u","l","s"," ","t","r","y","i","n","g"," ","t","o"," ","f","i","n","d"," ","t","h","e","i","r",
                " ","w","a","y"," ","o","u","t",".",".","."});
                Console.SetCursorPosition(11, 2); SlowWrite(new string[] { "I","t"," ","i","s"," ","u","p"," ","t","o"," ",
                "y","o","u"," ","t","o"," ","d","e","c","i","d","e"," ","h","o","w"," ","t","o"," ","i","n","t","e","r","a","c","t",
                " ","w","i","t","h"," ","t","h","e","m",".",".","."," ","B","e","f","o","r","e"," ","y","o","u"," ",
                "d","e","c","i","d","e"," ","w","h","a","t"," ","e","m","o","t","i","o","n","s"," ","y","o","u"," ","w","i","l","l",
                " ", "u","s","e","," });
                Console.SetCursorPosition(11, 3); 
                SlowWrite(new string[] { "l","e","t","'","s"," ","d","i","s","c","u","s","s"," ","t","h","i","s"," ",
                    "n","e","w"," ","c","o","m","m","a","n","d","."}); Console.ForegroundColor = ConsoleColor.White;
                Console.Write("  !COMMANDS UPDATED!"); MenuDoes.CommandClear(); MenuDoes.CommandToggle(7, 3, player);
                SceneSocial.DrawInput(new string[] { "//Okay" }, 0); SceneBattle.PressEnter(); SceneSocial.DrawInput(new string[]
                    {"" }, 0); CharacterInfo.ClearArea();
                Console.SetCursorPosition(11, 1); SlowWrite(new string[] { "T","h","r","o","u","g","h","o","u","t"," ",
                "t","h","i","s"," ","p","u","r","g","a","t","o","r","y"," ","y","o","u"," ","w","i","l","l"," ",
                "s","o","m","e","t","i","m","e","s"," ","h","a","v","e"," ","o","p","p","o","r","t","u","n","i","t","i","e","s",
                " ","i","n"," ","t","h","e"," ","r","e","s","p","o","n","s","e"," ","a","r","e","a"," ","t","o"," ",
                "c","a","n","c","e","l"," ","y","o","u","r"});
                Console.SetCursorPosition(11, 2); SlowWrite(new string[] { "r","e","s","p","o","n","s","e"," ","o","r"," ","\"",
                "b","a","c","k"," ","o","u","t","\""," ","o","f"," ","a"," ","m","e","n","u","."," ","W","h","e","n",
                " ","a","p","p","r","o","p","r","i","a","t","e",","," ","u","s","e"," ","t","h","e"," "});
                Console.ForegroundColor = ConsoleColor.Gray; Console.Write("\"cancel selection\" ");
                Console.ForegroundColor = ConsoleColor.Green; SlowWrite(new string[] { "k", "e", "y", ","," ",
                "e","s","c","a","p","e"," ","o","r"," ","s","u","b","t","r","a","c","t"});
                Console.SetCursorPosition(11, 3); SlowWrite(new string[] { "(","o","n"," ","t","h","e"," ",
                "N","u","m","P","a","d",")","."," ","B","e","l","o","w",","," ","t","r","y"," ","i","t"," ","o","u","t"," ",
                "w","i","t","h"," ","t","h","e"," ","r","e","s","p","o","n","s","e","s"," ","a","n","d"," ","c","o","n","f","i","r","m",
                " ","t","h","e"," ","s","e","l","e","c","t","i","o","n"," ","\"","C","o","n","t","i","n","u","e","\""," ",
                "w","h","e","n"," ","r","e","a","d","y","."});
                bool looped = true;
                int selected = 0;
                do
                {
                    Console.SetCursorPosition(11, 8); Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write(player.Name); Console.ForegroundColor = ConsoleColor.White; Console.Write(": ");
                    Console.ForegroundColor = ConsoleColor.Gray; Console.Write("(How will I respond?)                             ");
                    SceneSocial.DrawInput(new string[] { "I am the son of a monkey's uncle.", "Well, let's check this out.",
                        "Okay", "//Continue"}, selected);
                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.Tab:
                        case ConsoleKey.Add:
                        case ConsoleKey.D:
                        case ConsoleKey.NumPad6:
                            selected += selected != 3 ? 1 : -3;
                            break;
                        case ConsoleKey.A:
                        case ConsoleKey.NumPad4:
                            selected -= selected != 0 ? 1 : -3;
                            break;
                        case ConsoleKey.F:
                        case ConsoleKey.NumPad5:
                            Console.SetCursorPosition(13 + player.Name.Length, 8); Console.ForegroundColor = ConsoleColor.White;
                            Console.Write("                                      ");
                            Console.SetCursorPosition(13 + player.Name.Length, 8);
                            bool go1 = true;
                            bool go2 = true;
                            bool go3 = true;
                            switch (selected)
                            {
                                case 0:
                                    Console.Write("I am the son of a monkey's uncle.");
                                    do
                                    {
                                        switch (Console.ReadKey(true).Key)
                                        {
                                            case ConsoleKey.Escape:
                                            case ConsoleKey.Subtract:
                                                go1 = false;
                                                break;
                                        }
                                    } while (go1);
                                    break;
                                case 1:
                                    Console.Write("Well, let's check this out.");
                                    do
                                    {
                                        switch (Console.ReadKey(true).Key)
                                        {
                                            case ConsoleKey.Escape:
                                            case ConsoleKey.Subtract:
                                                go2 = false;
                                                break;
                                        }
                                    } while (go2);
                                    break;
                                case 2:
                                    Console.Write("Okay...");
                                    do
                                    {
                                        switch (Console.ReadKey(true).Key)
                                        {
                                            case ConsoleKey.Escape:
                                            case ConsoleKey.Subtract:
                                                go3 = false;
                                                break;
                                        }
                                    } while (go3);
                                    break;
                                case 3:
                                    looped = false;
                                    SceneClear();
                                    break;
                            }
                            break;
                    }
                } while (looped);
                CharacterInfo.ClearArea();
            }
            #endregion
            #region Pick Emotions
            //need to toggle selection, confirm to get more details, confirm again to select
            MenuDoes.CommandToggle(7, 3, player);
            Console.SetCursorPosition(11, 1); Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Toggle through the emotions below and pick your "); Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Primary Emotion");
            Console.SetCursorPosition(11, 2);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("-Confirm selection once to get more info");
            Console.SetCursorPosition(11, 3);
            Console.Write("-Confirm selection again to select that emotion");
            int selector = 0;
            int[] emotions = new int[3] { -1, -1, -1 };
            bool loop = true;
            bool update = false;
            do
            {
                if (update == true)
                {
                    if (emotions[1] == -1)
                    {
                        Console.SetCursorPosition(11, 1); Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("Toggle through the emotions below and set your "); Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("Secondary Emotion");
                    }
                    else
                    {
                        Console.SetCursorPosition(11, 1); Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("Toggle through the emotions below and set your "); Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("Tertiary Emotion");
                    }
                }
                SceneSocial.DrawInput(new string[] 
                { "Anger", "Anticipation", "Joy", "Trust", "Fear", "Surprise", "Sadness", "Disgust" }, selector);
                DrawWheel(selector, emotions, 36, 8);
                switch (Console.ReadKey(true).Key)
                {
                    //Need to toggle through emotions, and as I do switch colors on the corresponding "wheel"
                    //While changing the emotion in the center of the "wheel" to the correct emotion
                    case ConsoleKey.Tab:
                    case ConsoleKey.Add:
                        selector += selector != 7 ? 1 : -7;
                        if (emotions[0] == selector | emotions[1] == selector) { selector += selector != 7 ? 1 : -7; }
                        if (emotions[0] == selector | emotions[1] == selector) { selector += selector != 7 ? 1 : -7; }
                        break;
                    case ConsoleKey.A:
                    case ConsoleKey.NumPad4:
                        selector -= selector != 0 ? 1 : -7;
                        if (emotions[0] == selector | emotions[1] == selector) { selector -= selector != 0 ? 1 : -7; }
                        if (emotions[0] == selector | emotions[1] == selector) { selector -= selector != 0 ? 1 : -7; }
                        break;
                    case ConsoleKey.D:
                    case ConsoleKey.NumPad6:
                        selector += selector != 7 ? 1 : -7;
                        if (emotions[0] == selector | emotions[1] == selector) { selector += selector != 7 ? 1 : -7; }
                        if (emotions[0] == selector | emotions[1] == selector) { selector += selector != 7 ? 1 : -7; }
                        break;
                    case ConsoleKey.F:
                    case ConsoleKey.NumPad5:
                        bool looped = true;
                        DrawWheel(selector, emotions, 36, 8);
                        string write = selector == 0 ? "Anger" :
                            selector == 1 ? "Anticipation" :
                            selector == 2 ? "Joy" :
                            selector == 3 ? "Trust" :
                            selector == 4 ? "Fear" :
                            selector == 5 ? "Surprise" :
                            selector == 6 ? "Sadness" :
                            selector == 7 ? "Disgust" : "Oopsie Poopsie";
                        SceneSocial.DrawInput(new string[] { "//Set " + write + "?"}, 0);
                        do
                        {
                            switch (Console.ReadKey(true).Key)
                            {
                                case ConsoleKey.F:
                                case ConsoleKey.NumPad5:
                                    if (emotions[0] == -1)
                                    {
                                        emotions[0] = selector;
                                        update = true;
                                    }
                                    else if (emotions[1] == -1)
                                    {
                                        emotions[1] = selector;
                                        update = true;
                                    }
                                    else { emotions[2] = selector; loop = false; }
                                    looped = false;
                                    selector += selector != 7 ? 1 : -7;
                                    if (emotions[0] == selector | emotions[1] == selector) { selector += selector != 7 ? 1 : -7; }
                                    if (emotions[0] == selector | emotions[1] == selector) { selector += selector != 7 ? 1 : -7; }
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
            SceneSocial.DrawInput(new string[] { "//Continue" }, 0); SceneBattle.PressEnter();
            CharacterInfo.ClearArea(); SceneSocial.DrawInput(new string[] { "" }, 0); SceneClear();
            #endregion
            #region Fourth Tutorial \ Social Scenes
            if (on)
            {
                Console.SetCursorPosition(11, 1); Console.ForegroundColor = ConsoleColor.Green;
                SlowWrite(new string[] { "G","r","e","a","t"," ","j","o","b"," ","p","i","c","k","i","n","g"," ","y","o","u","r"," ",
            "e","m","o","t","i","o","n","s","."," ","T","h","e","y"," ","w","i","l","l"," ","b","e"," ",
            "i","m","p","o","r","t","a","n","t"," ","t","h","r","o","u","g","h","o","u","t"," ","t","h","i","s"," ",
            "p","u","r","g","a","t","o","r","y"," ","a","n","d"," ","w","i","l","l"," ","t","a","k","e"," ","t","i","m","e"," " });
                Console.SetCursorPosition(11, 2); SlowWrite(new string[] { "t","o"," ","m","a","s","t","e","r","."," ","I","n"," ",
            "\"","s","o","c","i","a","l"," ","s","c","e","n","e","s","\""," ","y","o","u"," ","w","i","l","l"," ","b","e"," ",
            "a","b","l","e"," ","t","o"," ","u","s","e"," ","y","o","u","r"," ","e","m","o","t","i","o","n","s"," ","t","o"," ",
            "c","o","n","f","i","r","m"," ","y","o","u","r"," ","s","e","l","e","c","t","i","o","n","s","."," ","T","h","e"," " });
                Console.SetCursorPosition(11, 3); SlowWrite(new string[] { "c","o","m","m","a","n","d","s"," ","w","i","n","d","o","w",
            " ","w","i","l","l"," ","u","p","d","a","t","e"," ","d","u","r","i","n","g"," ","t","h","e","s","e"," ",
            "s","c","e","n","e","s"," ","t","o"," ","s","h","o","w"," ","y","o","u"," ","h","o","w"," ","t","o"," ",
            "r","e","s","p","o","n","d"," ","w","i","t","h"," ","y","o","u","r"," ","e","m","o","t","i","o","n","s","."});
                Console.SetCursorPosition(11, 4); Console.ForegroundColor = ConsoleColor.White; Console.Write("!Note! ");
                Console.ForegroundColor = ConsoleColor.Gray; Console.Write("Your previous commands are still available even when the" +
                    " command window changes"); MenuDoes.CommandClear(); MenuDoes.CommandToggle(2, 0, player);
                SceneSocial.DrawInput(new string[] { "//Continue" }, 0);
                SceneBattle.PressEnter();
                SceneSocial.DrawInput(new string[] { "" }, 0);
                CharacterInfo.ClearArea();
                Console.SetCursorPosition(11, 1); SlowWrite(new string[] { "J","u","s","t"," ","r","e","m","e","m","b","e","r"," ",
            "t","h","e"," ","c","o","m","m","a","n","d"," ","w","i","n","d","o","w"," ","i","s"," ","a"," ","h","e","l","p","f",
            "u","l"," ","r","e","f","e","r","e","n","c","e"," ","b","u","t"," ","i","n"," ","c","o","m","b","a","t"," ",
            "i","t"," ","w","i","l","l"," ","n","o","t"," ","b","e"," ","t","h","e","r","e"," ","t","o"});
                Console.SetCursorPosition(11, 2); SlowWrite(new string[] { "g", "u", "i", "d", "e", " ", "y", "o", "u", ".", " " });
                Console.SetCursorPosition(11, 3); SlowWrite(new string[] { "O","n"," ","t","h","e"," ","t","o","p","i","c"," ",
            "o","f"," ","c","o","m","b","a","t"," ","l","e","t","'","s"," ","g","o"," ","a","h","e","a","d"," ","a","n","d"," ",
            "p","i","c","k"," ","y","o","u","r"," ","C","o","r","e"," ","A","t","t","r","i","b","u","t","e","."});
                Console.SetCursorPosition(11, 4); SlowWrite(new string[] { "N","a","v","i","g","a","t","e"," ","t","h","e"," ",
            "m","e","n","u"," ","o","n"," ","t","h","e"," ","n","e","x","t"," ","s","c","r","e","e","n"," ","u","s","i","n","g",
            " ","w","h","a","t"," ","y","o","u"," ","h","a","v","e"," ","l","e","a","r","n","e","d"," ","s","o"," ","f","a","r","."});
                SceneSocial.DrawInput(new string[] { "//Continue" }, 0);
                SceneBattle.PressEnter(); CharacterInfo.ClearArea();
                MenuDoes.CommandClear();
                MenuDoes.CommandToggle(7, 3, player);
            }
            
            #endregion
            //Must Choose Core Attribute
            #region Pick Core Attribute
            Console.SetCursorPosition(11, 1); Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Toggle through the attributes below and pick your "); Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Core Attribute");
            Console.SetCursorPosition(11, 2);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("-Confirm selection once to get more info");
            Console.SetCursorPosition(11, 3);
            Console.Write("-Confirm selection again to select that attribute");
            Console.SetCursorPosition(11, 4); Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Your Core Attribute will increase your ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Life");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" and your Skill Damage (");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("sDMG");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(")");
            Attributes core = PickCore();
            player = new Player(1, core);
            player.Primary = emotions[0] == 0 ? Emotion.Anger : emotions[0] == 1 ? Emotion.Anticipation : emotions[0] == 2 ?
                Emotion.Joy : emotions[0] == 3 ? Emotion.Trust : emotions[0] == 4 ? Emotion.Fear : emotions[0] == 5 ?
                Emotion.Surprise : emotions[0] == 6 ? Emotion.Sadness : Emotion.Disgust;
            player.Secondary = emotions[1] == 0 ? Emotion.Anger : emotions[1] == 1 ? Emotion.Anticipation : emotions[1] == 2 ?
                Emotion.Joy : emotions[1] == 3 ? Emotion.Trust : emotions[1] == 4 ? Emotion.Fear : emotions[1] == 5 ?
                Emotion.Surprise : emotions[1] == 6 ? Emotion.Sadness : Emotion.Disgust;
            player.Tertiary = emotions[2] == 0 ? Emotion.Anger : emotions[2] == 1 ? Emotion.Anticipation : emotions[2] == 2 ?
                Emotion.Joy : emotions[2] == 3 ? Emotion.Trust : emotions[2] == 4 ? Emotion.Fear : emotions[2] == 5 ?
                Emotion.Surprise : emotions[2] == 6 ? Emotion.Sadness : Emotion.Disgust;
            player.Current = player.Primary;
            player.Name = playerName;
            player.ResourceMax = 10;
            player.Life = player.FindLife();
            SceneBattle.EndProcessing(player);
            #endregion
            #region Fifth Tutorial \ Not sure yet, dice?

            #endregion
            CharacterInfo.ClearArea();
            SceneClear();
            player.DiceShards = 30;
            CharacterInfo.ShortSheet(player);
            Console.SetCursorPosition(7, 8);
            Console.Write("Monsters throughout this purgatory will drop \"dice shards\".");
            Console.SetCursorPosition(7, 9);
            Console.Write("Use these shards to improve the attributes of your character.");
            Console.SetCursorPosition(7, 10); Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("(You recieved "); Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("{0} ", player.DiceShards); Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("dice shards)"); Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(7, 11); //Elaborate?
            SceneSocial.DrawInput(new string[] { "//Continue" }, 0);
            SceneBattle.PressEnter();
            SceneSocial.DiceVendor(player);
            player.InitializeRapport();
            CharacterInfo.ClearArea();
            CharacterInfo.ShortAttributes(player);
            player.Resource = player.ResourceAttribute != Attributes.Intuition ? player.FindResource() : 0;
            return player;
        }

        public static void DrawWheel(int selector, int[] selected, int x, int y)
        {
            bool eS = false;
            //Anger
            foreach (int emotion in selected) { if (emotion == 0) { eS = true; } }
            Console.ForegroundColor = selector == 0 || eS ? ConsoleColor.DarkRed : ConsoleColor.Gray;
            Console.SetCursorPosition(x + 8, y + 1); Console.Write("|\\");
            Console.SetCursorPosition(x + 8, y + 2); Console.Write("{0}", selector == 0 || eS ? "|\\\\" : "| \\");
            Console.SetCursorPosition(x + 8, y + 3); Console.Write("{0}", selector == 0 || eS ? "|\\\\\\" : "|  \\");
            Console.SetCursorPosition(x + 8, y + 4); Console.Write("{0}", selector == 0 || eS ? "|\\\\\\\\" : "|   \\");
            Console.SetCursorPosition(x + 9, y + 5); Console.Write("{0}", selector == 0 || eS ? "\\\\\\\\\\" : "\\   \\");
            Console.SetCursorPosition(x + 8, y + 6); Console.Write("{0}", selector == 0 ? "     ANGER     " : "               ");
            eS = false;
            //Anticipation
            foreach (int emotion in selected) { if (emotion == 1) { eS = true; } }
            Console.ForegroundColor = selector == 1 || eS ? ConsoleColor.DarkGreen : ConsoleColor.Gray;
            Console.SetCursorPosition(x + 11, y); Console.Write("|\\");
            Console.SetCursorPosition(x + 11, y + 1); Console.Write("{0}", selector == 1 || eS ? "|\\\\" : "| \\");
            Console.SetCursorPosition(x + 11, y + 2); Console.Write("{0}", selector == 1 || eS ? "|\\\\\\" : "|  \\");
            Console.SetCursorPosition(x + 12, y + 3); Console.Write("{0}", selector == 1 || eS ? "\\\\\\|" : "   |");
            Console.SetCursorPosition(x + 13, y + 4); Console.Write("{0}", selector == 1 || eS ? "\\\\|" : "  |");
            Console.SetCursorPosition(x + 14, y + 5); Console.Write("{0}", selector == 1 || eS ? "\\|" : " |");
            Console.SetCursorPosition(x + 8, y + 6); Console.Write("{0}", selector == 1 ? "  ANTICIPATION  " : "");
            eS = false;
            //Joy
            foreach (int emotion in selected) { if (emotion == 2) { eS = true; } }
            Console.ForegroundColor = selector == 2 || eS ? ConsoleColor.Yellow : ConsoleColor.Gray;
            Console.SetCursorPosition(x + 18, y); Console.Write("/|");
            Console.SetCursorPosition(x + 17, y + 1); Console.Write("{0}", selector == 2 || eS ? "//|" : "/ |");
            Console.SetCursorPosition(x + 16, y + 2); Console.Write("{0}", selector == 2 || eS ? "///|" : "/  |");
            Console.SetCursorPosition(x + 16, y + 3); Console.Write("{0}", selector == 2 || eS ? "///" : "   ");
            Console.SetCursorPosition(x + 16, y + 4); Console.Write("{0}", selector == 2 || eS ? "//" : "  ");
            Console.SetCursorPosition(x + 16, y + 5); Console.Write("{0}", selector == 2 || eS ? "/" : " ");
            Console.SetCursorPosition(x + 8, y + 6); Console.Write("{0}", selector == 2 ? "      JOY      " : "");
            eS = false;
            //Trust
            foreach (int emotion in selected) { if (emotion == 3) { eS = true; } }
            Console.ForegroundColor = selector == 3 || eS ? ConsoleColor.Blue : ConsoleColor.Gray;
            Console.SetCursorPosition(x + 21, y + 1); Console.Write("/|");
            Console.SetCursorPosition(x + 20, y + 2); Console.Write("{0}", selector == 3 || eS ? "//|" : "/ |");
            Console.SetCursorPosition(x + 19, y + 3); Console.Write("{0}", selector == 3 || eS ? "///|" : "/  |");
            Console.SetCursorPosition(x + 18, y + 4); Console.Write("{0}", selector == 3 || eS ? "////|" : "/   |");
            Console.SetCursorPosition(x + 17, y + 5); Console.Write("{0}", selector == 3 || eS ? "/////" : "/   /");
            Console.SetCursorPosition(x + 8, y + 6); Console.Write("{0}", selector == 3 ? "     TRUST     " : "");
            eS = false;
            //Fear
            foreach (int emotion in selected) { if (emotion == 4) { eS = true; } }
            Console.ForegroundColor = selector == 4 || eS ? ConsoleColor.DarkGray : ConsoleColor.Gray;
            Console.SetCursorPosition(x + 17, y + 7); Console.Write("{0}", selector == 4 || eS ? "\\\\\\\\\\" : "\\   \\");
            Console.SetCursorPosition(x + 18, y + 8); Console.Write("{0}", selector == 4 || eS ? "\\\\\\\\|" : "\\   |");
            Console.SetCursorPosition(x + 19, y + 9); Console.Write("{0}", selector == 4 || eS ? "\\\\\\|" : "\\  |");
            Console.SetCursorPosition(x + 20, y + 10); Console.Write("{0}", selector == 4 || eS ? "\\\\|" : "\\ |");
            Console.SetCursorPosition(x + 21, y + 11); Console.Write("\\|");
            Console.SetCursorPosition(x + 8, y + 6); Console.Write("{0}", selector == 4 ? "      FEAR     " : "");
            eS = false;
            //Surprise
            foreach (int emotion in selected) { if (emotion == 5) { eS = true; } }
            Console.ForegroundColor = selector == 5 || eS ? ConsoleColor.Cyan : ConsoleColor.Gray;
            Console.SetCursorPosition(x + 16, y + 7); Console.Write("{0}", selector == 5 || eS ? "\\" : " ");
            Console.SetCursorPosition(x + 16, y + 8); Console.Write("{0}", selector == 5 || eS ? "\\\\" : "  ");
            Console.SetCursorPosition(x + 16, y + 9); Console.Write("{0}", selector == 5 || eS ? "\\\\\\" : "   ");
            Console.SetCursorPosition(x + 16, y + 10); Console.Write("{0}", selector == 5 || eS ? "\\\\\\|" : "\\  |");
            Console.SetCursorPosition(x + 17, y + 11); Console.Write("{0}", selector == 5 || eS ? "\\\\|" : "\\ |");
            Console.SetCursorPosition(x + 18, y + 12); Console.Write("\\|");
            Console.SetCursorPosition(x + 8, y + 6); Console.Write("{0}", selector == 5 ? "    SURPRISE    " : "");
            eS = false;
            //Sadness
            foreach (int emotion in selected) { if (emotion == 6) { eS = true; } }
            Console.ForegroundColor = selector == 6 || eS ? ConsoleColor.DarkYellow : ConsoleColor.Gray;
            Console.SetCursorPosition(x + 14, y + 7); Console.Write("{0}", selector == 6 || eS ? "/|" : " |");
            Console.SetCursorPosition(x + 13, y + 8); Console.Write("{0}", selector == 6 || eS ? "//|" : "  |");
            Console.SetCursorPosition(x + 12, y + 9); Console.Write("{0}", selector == 6 || eS ? "///|" : "   |");
            Console.SetCursorPosition(x + 11, y + 10); Console.Write("{0}", selector == 6 || eS ? "|///" : "|  /");
            Console.SetCursorPosition(x + 11, y + 11); Console.Write("{0}", selector == 6 || eS ? "|//" : "| /");
            Console.SetCursorPosition(x + 11, y + 12); Console.Write("|/");
            Console.SetCursorPosition(x + 8, y + 6); Console.Write("{0}", selector == 6 ? "     SADNESS     " : "");
            eS = false;
            //Disgust
            foreach (int emotion in selected) { if (emotion == 7) { eS = true; } }
            Console.ForegroundColor = selector == 7 || eS ? ConsoleColor.Magenta : ConsoleColor.Gray;
            Console.SetCursorPosition(x + 9, y + 7); Console.Write("{0}", selector == 7 || eS ? "/////" : "/   /");
            Console.SetCursorPosition(x + 8, y + 8); Console.Write("{0}", selector == 7 || eS ? "|////" : "|   /");
            Console.SetCursorPosition(x + 8, y + 9); Console.Write("{0}", selector == 7 || eS ? "|///" : "|  /");
            Console.SetCursorPosition(x + 8, y + 10); Console.Write("{0}", selector == 7 || eS ? "|//" : "| /");
            Console.SetCursorPosition(x + 8, y + 11); Console.Write("|/");
            Console.SetCursorPosition(x + 8, y + 6); Console.Write("{0}", selector == 7 ? "     DISGUST     " : "");
            eS = false;
        }

        public static Attributes PickCore()
        {
            bool loop = true;
            int selector = 0;
            do
            {
                SceneSocial.DrawInput(new string[] { "//Strength", "//Defense", "//Intelligence", "//Wisdom", "//Dexterity", "//Agility" },
                    selector);
                Inform(selector);
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.Tab:
                    case ConsoleKey.Add:
                    case ConsoleKey.D:
                    case ConsoleKey.NumPad6:
                        selector += selector != 5 ? 1 : -5;
                        break;
                    case ConsoleKey.A:
                    case ConsoleKey.NumPad4:
                        selector -= selector != 0 ? 1 : -5;
                        break;
                    case ConsoleKey.F:
                    case ConsoleKey.NumPad5:
                        string input = selector == 0 ? "Strength" : selector == 1 ? "Defense" :
                            selector == 2 ? "Intelligence" : selector == 3 ? "Wisdom" : selector == 4 ? "Dexterity" : "Agility";
                        SceneSocial.DrawInput(new string[] {"//Confirm " + input}, 0 );
                        SceneClear();
                        Console.SetCursorPosition(7, 8); Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("Set "); Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(input);
                        Console.ForegroundColor = ConsoleColor.White; Console.Write(" as your Core Attribute?");
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
                        break;
                }
            } while (loop);
            Attributes core = selector == 0 ? Attributes.Strength : selector == 1 ? Attributes.Defense : selector == 2 ?
                Attributes.Intelligence : selector == 3 ? Attributes.Wisdom : selector == 4 ? Attributes.Dexterity : Attributes.Agility;
            return core;
        }

        public static void Inform(int selector)
        {
            SceneClear(); Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(7, 8); Console.Write("//");
            Console.ForegroundColor = ConsoleColor.Green;
            switch (selector)
            {
                case 0:
                    Console.Write("Strength"); Console.ForegroundColor = ConsoleColor.White; Console.Write(":");
                    Console.SetCursorPosition(7, 9);
                    Console.Write("Focuses on attacks");
                    Console.SetCursorPosition(7, 10);
                    Console.Write("Strength increases attack damage (");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("aDMG");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(")");
                    Console.SetCursorPosition(7, 11);
                    Console.Write("Skills use \"Stamina\", a resource that replenishes each combat.");
                    break;
                case 1:
                    Console.Write("Defense"); Console.ForegroundColor = ConsoleColor.White; Console.Write(":");
                    Console.SetCursorPosition(7, 9);
                    Console.Write("Focuses on reducing damage from attacks");
                    Console.SetCursorPosition(7, 10);
                    Console.Write("Defense reduces recieved attack damage (aDMG)");
                    Console.SetCursorPosition(7, 11);
                    Console.Write("Skills use \"Stamina\", a resource that replenishes each combat.");
                    break;
                case 2:
                    Console.Write("Intelligence"); Console.ForegroundColor = ConsoleColor.White; Console.Write(":");
                    Console.SetCursorPosition(7, 9);
                    Console.Write("Focuses on statuses and heals");
                    Console.SetCursorPosition(7, 10);
                    Console.Write("Intelligence increases status effect chances (");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("STA%");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(") and heals (");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("+Heal");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(")");
                    Console.SetCursorPosition(7, 11);
                    Console.Write("Skills use \"Knowledge\", a resource that replenishes each floor.");
                    break;
                case 3:
                    Console.Write("Wisdom"); Console.ForegroundColor = ConsoleColor.White; Console.Write(":");
                    Console.SetCursorPosition(7, 9);
                    Console.Write("Focuses on resisting and shields");
                    Console.SetCursorPosition(7, 10);
                    Console.Write("Wisdom increases resist chance (");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("RES%");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(") and shields (");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("+Shield");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(")");
                    Console.SetCursorPosition(7, 11);
                    Console.Write("Skills use \"Knowledge\", a resource that replenishes each floor.");
                    break;
                case 4:
                    Console.Write("Dexterity"); Console.ForegroundColor = ConsoleColor.White; Console.Write(":");
                    Console.SetCursorPosition(7, 9);
                    Console.Write("Focuses on hit chance");
                    Console.SetCursorPosition(7, 10);
                    Console.Write("Dexterity increases hit chance (");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("HIT%");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(")");
                    Console.SetCursorPosition(7, 11);
                    Console.Write("Skills use \"Intuition\", a resource that replenishes some each attack, but depletes");
                    Console.SetCursorPosition(7, 12);
                    Console.Write("between combats.");
                    break;
                case 5:
                    Console.Write("Agility"); Console.ForegroundColor = ConsoleColor.White; Console.Write(":");
                    Console.SetCursorPosition(7, 9);
                    Console.Write("Focuses on dodging");
                    Console.SetCursorPosition(7, 10);
                    Console.Write("Agility increases dodge chance (");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("DOD%");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(")");
                    Console.SetCursorPosition(7, 11);
                    Console.Write("Skills use \"Intuition\", a resource that replenishes some each attack, but depletes");
                    Console.SetCursorPosition(7, 12);
                    Console.Write("between combats.");
                    break;
            }
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

        public static string PlayerName()
        {
            string playerName = "";
            ConsoleKey userInput;
            bool naming = true;
            int capitalize = 0;

            string name = "";
            
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
                        if (capitalize == 0)
                        {
                            playerName += userInput.ToString(); capitalize++;
                        }
                        else
                        {
                            playerName += userInput.ToString().ToLower();
                        }
                        SceneSocial.DrawInput(new string[] { "//" + playerName }, 0);
                        break;
                    case ConsoleKey.Spacebar:
                        playerName += " ";
                        capitalize = 0;
                        SceneSocial.DrawInput(new string[] { "//" + playerName }, 0);
                        break;
                    default:
                        break;
                }//END SWITCH
                if (!naming) { naming = playerName.Length < 15 & playerName.Length > 0 ? false : true;
                    if (naming) { playerName = ""; SceneSocial.DrawInput(new string[] { "//Invalid Name Length" }, 0); }
                }
            } while (naming);//END LOOP
            
            return playerName;
        }

        public static Monster GetBoss()
        {
            Monster boss = new Monster("boss");
            return boss;
        }

        public static void SlowWrite(string[] writeThis)
        {
            int index = 0;
            int x;
            foreach (string letter in writeThis)
            {
                x = writeThis[index] == "." ? 197 : 7;//TEMP 7 (27)
                Console.Write(writeThis[index]); System.Threading.Thread.Sleep(x);
                index++;
            }
        }

        public static void CreatePlayer()
        {
//            Console.Clear();

//            Emotion primary = Emotion.Null;
//            Emotion secondary = Emotion.Null;
//            Emotion tertiary = Emotion.Null;
//            string name;
//            int lifeMax = 50;
//            int hitChance = 50;
//            int blockChance = 0;
//            int resistChance = 5;
//            int dodgeChance = 5;
//            Attributes primaryAttribute = Attributes.Strength;
//            int strength = 5;
//            int defense = 5;
//            int intelligence = 5;
//            int wisdom = 5;
//            int dexterity = 5;
//            int agility = 5;
//            int resourceStat = 0;
//            Console.WriteLine("Hello.");
//            Console.WriteLine("Will you press \"Enter\"?");
//            SceneBattle.PressEnter();
//            Console.WriteLine("Good job!\nNow... let's decide some things about your character.");
//            bool menuOn = true;
//            do
//            {
//                Console.WriteLine("Please choose a Primary Emotion from the list below\n(1) Anger\n(2) Anticipation\n" +
//                   "(3) Disgust\n(4) Fear\n(5) Joy\n(6) Sadness\n(7) Surprise\n(8) Trust\n");
//                switch (Console.ReadKey().Key)
//                {
//                    case ConsoleKey.D1:
//                        primary = Emotion.Anger;
//                        menuOn = Confirm("Anger");
//                        break;
//                    case ConsoleKey.D2:
//                        primary = Emotion.Anticipation;
//                        menuOn = Confirm("Anticipation");
//                        break;
//                    case ConsoleKey.D3:
//                        primary = Emotion.Disgust;
//                        menuOn = Confirm("Disgust");
//                        break;
//                    case ConsoleKey.D4:
//                        primary = Emotion.Fear;
//                        menuOn = Confirm("Fear");
//                        break;
//                    case ConsoleKey.D5:
//                        primary = Emotion.Joy;
//                        menuOn = Confirm("Joy");
//                        break;
//                    case ConsoleKey.D6:
//                        primary = Emotion.Sadness;
//                        menuOn = Confirm("Sadness");
//                        break;
//                    case ConsoleKey.D7:
//                        primary = Emotion.Surprise;
//                        menuOn = Confirm("Surprise");
//                        break;
//                    case ConsoleKey.D8:
//                        primary = Emotion.Trust;
//                        menuOn = Confirm("Trust");
//                        break;
//                }

//                Console.Clear();
//            } while (menuOn);
//            menuOn = true;
//            do
//            {
//                Console.WriteLine("Please choose a Secondary Emotion from the list below");
//                Console.Write("{0}{1}{2}{3}{4}{5}{6}{7}",
//                   primary != Emotion.Anger ? "(1) Anger\n" : "",
//                   primary != Emotion.Anticipation ? "(2) Anticipation\n" : "",
//                   primary != Emotion.Disgust ? "(3) Disgust\n" : "",
//                   primary != Emotion.Fear ? "(4) Fear\n" : "",
//                   primary != Emotion.Joy ? "(5) Joy\n" : "",
//                   primary != Emotion.Sadness ? "(6) Sadness\n" : "",
//                   primary != Emotion.Surprise ? "(7) Surprise\n" : "",
//                   primary != Emotion.Trust ? "(8) Trust\n" : "");

//                switch (Console.ReadKey().Key)
//                {
//                    case ConsoleKey.D1:
//                        if (primary != Emotion.Anger)
//                        {
//                            secondary = Emotion.Anger;
//                            menuOn = Confirm("Anger");
//                        }
//                        break;
//                    case ConsoleKey.D2:
//                        if (primary != Emotion.Anticipation)
//                        {
//                            secondary = Emotion.Anticipation;
//                            menuOn = Confirm("Anticipation");
//                        }
//                        break;
//                    case ConsoleKey.D3:
//                        if (primary != Emotion.Disgust)
//                        {
//                            secondary = Emotion.Disgust;
//                            menuOn = Confirm("Disgust");
//                        }
//                        break;
//                    case ConsoleKey.D4:
//                        if (primary != Emotion.Fear)
//                        {
//                            secondary = Emotion.Fear;
//                            menuOn = Confirm("Fear");
//                        }
//                        break;
//                    case ConsoleKey.D5:
//                        if (primary != Emotion.Joy)
//                        {
//                            secondary = Emotion.Joy;
//                            menuOn = Confirm("Joy");
//                        }
//                        break;
//                    case ConsoleKey.D6:
//                        if (primary != Emotion.Sadness)
//                        {
//                            secondary = Emotion.Sadness;
//                            menuOn = Confirm("Sadness");
//                        }
//                        break;
//                    case ConsoleKey.D7:
//                        if (primary != Emotion.Surprise)
//                        {
//                            secondary = Emotion.Surprise;
//                            menuOn = Confirm("Surprise");
//                        }
//                        break;
//                    case ConsoleKey.D8:
//                        if (primary != Emotion.Trust)
//                        {
//                            secondary = Emotion.Trust;
//                            menuOn = Confirm("Trust");
//                        }
//                        break;
//                }

//                Console.Clear();
//            } while (menuOn);
//            menuOn = true;
//            do
//            {
//                Console.WriteLine("Please choose a Tertiary Emotion from the list below");
//                Console.Write("{0}{1}{2}{3}{4}{5}{6}{7}",
//                   primary != Emotion.Anger & secondary != Emotion.Anger ? "(1) Anger\n" : "",
//                   primary != Emotion.Anticipation & secondary != Emotion.Anticipation ? "(2) Anticipation\n" : "",
//                   primary != Emotion.Disgust & secondary != Emotion.Disgust ? "(3) Disgust\n" : "",
//                   primary != Emotion.Fear & secondary != Emotion.Fear ? "(4) Fear\n" : "",
//                   primary != Emotion.Joy & secondary != Emotion.Joy ? "(5) Joy\n" : "",
//                   primary != Emotion.Sadness & secondary != Emotion.Sadness ? "(6) Sadness\n" : "",
//                   primary != Emotion.Surprise & secondary != Emotion.Surprise ? "(7) Surprise\n" : "",
//                   primary != Emotion.Trust & secondary != Emotion.Trust ? "(8) Trust\n" : "");

//                switch (Console.ReadKey().Key)
//                {
//                    case ConsoleKey.D1:
//                        if (primary != Emotion.Anger & secondary != Emotion.Anger)
//                        {
//                            tertiary = Emotion.Anger;
//                            menuOn = Confirm("Anger");
//                        }
//                        break;
//                    case ConsoleKey.D2:
//                        if (primary != Emotion.Anticipation & secondary != Emotion.Anticipation)
//                        {
//                            tertiary = Emotion.Anticipation;
//                            menuOn = Confirm("Anticipation");
//                        }
//                        break;
//                    case ConsoleKey.D3:
//                        if (primary != Emotion.Disgust & secondary != Emotion.Disgust)
//                        {
//                            tertiary = Emotion.Disgust;
//                            menuOn = Confirm("Disgust");
//                        }
//                        break;
//                    case ConsoleKey.D4:
//                        if (primary != Emotion.Fear & secondary != Emotion.Fear)
//                        {
//                            tertiary = Emotion.Fear;
//                            menuOn = Confirm("Fear");
//                        }
//                        break;
//                    case ConsoleKey.D5:
//                        if (primary != Emotion.Joy & secondary != Emotion.Joy)
//                        {
//                            tertiary = Emotion.Joy;
//                            menuOn = Confirm("Joy");
//                        }
//                        break;
//                    case ConsoleKey.D6:
//                        if (primary != Emotion.Sadness & secondary != Emotion.Sadness)
//                        {
//                            tertiary = Emotion.Sadness;
//                            menuOn = Confirm("Sadness");
//                        }
//                        break;
//                    case ConsoleKey.D7:
//                        if (primary != Emotion.Surprise & secondary != Emotion.Surprise)
//                        {
//                            tertiary = Emotion.Surprise;
//                            menuOn = Confirm("Surprise");
//                        }
//                        break;
//                    case ConsoleKey.D8:
//                        if (primary != Emotion.Trust & secondary != Emotion.Trust)
//                        {
//                            tertiary = Emotion.Trust;
//                            menuOn = Confirm("Trust");
//                        }
//                        break;
//                }

//                Console.Clear();
//            } while (menuOn);
//            Console.WriteLine("Congratulations, you're now an emotional wreck!\n");
//            System.Threading.Thread.Sleep(1500);
//            Console.WriteLine("Just kidding!\nGood job picking out your emotions!\nNow let's get on with it.");
//            Console.WriteLine("You need to now pick out a Core Attribute.");
//            menuOn = true;
//            int resourceMax = 0;
//            do
//            {
//                Console.WriteLine("Please choose a core attribute from the list below.\n(1) Strength\n(2) Defense\n" +
//                    "(3) Intelligence\n(4) Wisdom\n(5) Dexterity\n(6) Agility");
//                switch (Console.ReadKey().Key)
//                {
//                    case ConsoleKey.D1:
//                        primaryAttribute = Attributes.Strength;
//                        strength = 20;
//                        resourceStat = 10;
//                        resourceMax = 10;
//                        menuOn = Confirm("Strength");
//                        break;
//                    case ConsoleKey.D2:
//                        primaryAttribute = Attributes.Defense;
//                        defense = 20;
//                        resourceStat = 10;
//                        resourceMax = 10;
//                        menuOn = Confirm("Defense");
//                        break;
//                    case ConsoleKey.D3:
//                        primaryAttribute = Attributes.Intelligence;
//                        intelligence = 20;
//                        resourceStat = 7;
//                        resourceMax = 10;
//                        menuOn = Confirm("Intelligence");
//                        break;
//                    case ConsoleKey.D4:
//                        primaryAttribute = Attributes.Wisdom;
//                        wisdom = 20;
//                        resourceStat = 7;
//                        resourceMax = 10;
//                        menuOn = Confirm("Wisdom");
//                        break;
//                    case ConsoleKey.D5:
//                        primaryAttribute = Attributes.Dexterity;
//                        dexterity = 20;
//                        resourceMax = 10;
//                        menuOn = Confirm("Dexterity"); resourceStat = 5;
//                        break;
//                    case ConsoleKey.D6:
//                        primaryAttribute = Attributes.Agility;
//                        agility = 20;
//                        resourceMax = 10;
//                        menuOn = Confirm("Agility");
//                        resourceStat = 5;
//                        break;
//                }

//                Console.Clear();
//            } while (menuOn);
//            Console.WriteLine("Wow, you're really getting the hang of this.\nI feel like with your impeccable decision " +
//                "making skills you're gonna do just fine here.");
//            menuOn = true;
//            do
//            {
//                Console.WriteLine("Oh I almost forgot, let's figure out your name!  Please type it below");
//                name = Console.ReadLine();
//                if (name.Length > 15)
//                {
//                    Console.Clear();
//                    Console.WriteLine("Oo, I'm afraid that's too long, try to keep it shorter.");
//                }
//                else { menuOn = Confirm(name); }

//            } while (menuOn);
//            Console.WriteLine("All right, so, before you get started, I think you should have these...");
//            Console.WriteLine("(You recieved 6 dice)");
//            int dice = 6;
//            Random rand = new Random();
//            menuOn = true;
//            int temp;
//            do
//            {
//                Console.WriteLine("Now choose an attribute from below to roll for more stats! {0}\n" +
//                    "(1) Strength\n(2) Defense\n(3) Intelligence\n(4) Wisdom\n(5) Dexterity\n(6) Agility",
//                    dice == 6 ? "6 dice remain" :
//                    dice == 5 ? "5 dice remain" :
//                    dice == 4 ? "4 dice remain" :
//                    dice == 3 ? "3 dice remain" :
//                    dice == 2 ? "2 dice remain" : "1 di remains");
//                switch (Console.ReadKey().Key)
//                {
//                    case ConsoleKey.D1:
//                        temp = rand.Next(1, 7);
//                        strength += temp;
//                        Console.Clear();
//                        Console.WriteLine("\nStrength boosted by {0}!", temp);
//                        dice--;
//                        break;
//                    case ConsoleKey.D2:
//                        temp = rand.Next(1, 7);
//                        defense += temp;
//                        Console.Clear();
//                        Console.WriteLine("\nDefense boosted by {0}!", temp);
//                        dice--;
//                        break;
//                    case ConsoleKey.D3:
//                        temp = rand.Next(1, 7);
//                        intelligence += temp;
//                        Console.Clear();
//                        Console.WriteLine("\nIntelligence boosted by {0}!", temp);
//                        dice--;
//                        break;
//                    case ConsoleKey.D4:
//                        temp = rand.Next(1, 7);
//                        wisdom += temp;
//                        Console.Clear();
//                        Console.WriteLine("\nWisdom boosted by {0}!", temp);
//                        dice--;
//                        break;
//                    case ConsoleKey.D5:
//                        temp = rand.Next(1, 7);
//                        dexterity += temp;
//                        Console.Clear();
//                        Console.WriteLine("\nDexterity boosted by {0}!", temp);
//                        dice--;
//                        break;
//                    case ConsoleKey.D6:
//                        temp = rand.Next(1, 7);
//                        agility += temp;
//                        Console.Clear();
//                        Console.WriteLine("\nAgility boosted by {0}!", temp);
//                        dice--;
//                        break;
//                }
//                menuOn = dice == 0 ? false : true;
//            } while (menuOn);
//            Console.WriteLine("Press any key to continue...");
//            Console.ReadKey();
//            Console.Clear();
//            Console.WriteLine(@"Welcome Adventurer, to Plutchik's
//Purgatory!  How you ended up here...
//One cannot be certain, but nevertheless,
//here you are!

//So, let's get some things straight.
//This place is no joke.  It's full of
//other's who find themselves in the
//same purgatory.  You will interact
//with them plenty along the way.  Those
//interactions could go several different
//ways.  They will always be up to you!

//Don't get too discouraged if people
//don't warm up to you initially.  It's
//a pretty scary place.  Keep trying!
//Where there is a will, there is a way...

//...out of purgatory.");
//            Console.WriteLine("Press any key to continue...");
//            Console.ReadKey();
//            Console.Clear();
//            Player player = new Player(primary, secondary, tertiary, name, new bool[17]
//            { true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false},
//            lifeMax, hitChance, blockChance, resistChance, dodgeChance, primaryAttribute, strength, defense, intelligence, wisdom,
//            dexterity, agility, resourceStat);
//            player.Life = player.FindLife();
//            player.ResourceMax = resourceMax;
//            return player;
        }

        public static bool Confirm(string writeThis, int x, int y, Player player)
        {
            bool confirm = false;
            bool menuOn = true;
            Console.SetCursorPosition(x, y); Console.ForegroundColor = ConsoleColor.Green;
            SlowWrite(new string[] { "Y", "o", "u", " ", "c", "h", "o", "s", "e", " " });
            Console.ForegroundColor = ConsoleColor.White; Console.Write(writeThis);
            Console.ForegroundColor = ConsoleColor.Green; SlowWrite(new string[] { ".", ".", "." });
            Console.SetCursorPosition(x, y + 1);
            SlowWrite(new string[] { "I", "s", " ", "t", "h", "a", "t", " ", "c", "o", "r", "r", "e", "c", "t", "?" });
            Console.ForegroundColor = ConsoleColor.Gray; Console.SetCursorPosition(x, y + 2);
            SlowWrite(new string[] { "(", "\"","F","\""," ","o","r"," ","\"","N","u","m","P","a","d"," ","5","\"", " ",
                "t", "o", " ", "c", "o", "n","f","i","r","m", ")", " ", "(", "\"", "E","s","c","a","p","e","\""," ","o","r"," ",
                "\"","N","u","m","P","a","d"," ","S","u","b","t","r","a","c","t","\""," ","t","o"," ","c","a","n","c","e","l",")"});
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
                        player.zToggle = 0;
                        menuOn = false;
                        confirm = true;
                        break;
                    case ConsoleKey.NumPad5:
                        player.zToggle = 1;
                        menuOn = false;
                        confirm = true;
                        break;
                }
            } while (menuOn);
            return confirm;
        }
    }
}
