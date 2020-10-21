using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlayerLibrary;

namespace MenuLibrary
{
    public class MenuDoes
    {
        public static void DrawMenu()
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            for (int b = 1; b < 30; b++)
            {
                Console.SetCursorPosition(0, b);
                if (b > 0 && b < 5) { Console.Write("]"); } else if (b % 2 != 0) { Console.Write("]|"); } else { Console.Write("|["); }
                Console.SetCursorPosition(118, b);
                if (b > 0 && b < 5) { Console.Write(" ["); } else if (b % 2 != 0) { Console.Write("|["); } else { Console.Write("]|"); }
            }
            
            Console.SetCursorPosition(0, 0);
            Console.Write("//````````````````````````````````````````````````````````````````````````````````````````````````````````````````````\\\\");
            
            Console.SetCursorPosition(1, 5);
            Console.Write("\\_____________________________________________________________________________________________________________________");
            Console.SetCursorPosition(2, 28);
            Console.Write("____________________________________________________________________________________________________________________");
            Console.SetCursorPosition(2, 29);
            Console.Write("][][][][][][][][][][][][][][][][][][][][][][][][][][][][][][][][][][][][][][][][][][][][][][][][][][][][][][][][][][");
        }

        public static void CommandClear()
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            for (int y = 6; y < 29; y++)
            {
                for (int x = 102; x < 103; x++)
                {
                    Console.SetCursorPosition(x, y);
                    Console.Write("|[");
                    Console.SetCursorPosition(x + 16, y);
                    Console.Write("]|");
                }
            }
            for (int y = 6; y < 28; y++)
            {
                for (int x = 104; x < 118; x++)
                {
                    Console.SetCursorPosition(x, y);
                    Console.Write(" ");
                }
            }
        }

        public static void SceneClear()
        {
            for (int y = 6; y < 23; y++)
            {
                for (int x = 2; x < 102; x++)
                {
                    Console.SetCursorPosition(x, y);
                    Console.Write(" ");
                }
            }
        }

        public static void CommandToggle(int x, int z, Player player)
        {
            // x checks which command menu to toggle to
            Console.ForegroundColor = ConsoleColor.White;
            if (x == 0)
            {
                string pref = z == 0 ? "F:        " : "Num 5:    ";
                string pref1 = z == 0 ? "Tab:      " : "Num +:    ";
                string pref2 = z == 0 ? "V:        " : "Backspace:";
                string pref3 = z == 0 ? "Escape:   " : "Num -:    ";
                Console.SetCursorPosition(105, 7); Console.Write("GENERAL");
                Console.SetCursorPosition(105, 8); Console.Write("COMMANDS");
                Console.SetCursorPosition(105, 10); Console.Write(pref); Console.ForegroundColor = ConsoleColor.Gray;                
                Console.SetCursorPosition(105, 11); Console.Write("confirm"); 
                Console.SetCursorPosition(105, 12); Console.Write("selection"); Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(105, 14); Console.Write(pref1); Console.ForegroundColor = ConsoleColor.Gray;
                Console.SetCursorPosition(105, 15); Console.Write("toggle"); 
                Console.SetCursorPosition(105, 16); Console.Write("response"); Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(105, 18); Console.Write(pref2); Console.ForegroundColor = ConsoleColor.Gray;
                Console.SetCursorPosition(105, 19); Console.Write("toggle");
                Console.SetCursorPosition(105, 20); Console.Write("commands"); Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(105, 22); Console.Write(pref3); Console.ForegroundColor = ConsoleColor.Gray;
                Console.SetCursorPosition(105, 23); Console.Write("cancel");
                Console.SetCursorPosition(105, 24); Console.Write("selection");
            }
            if (x == 1)
            {
                string pref = z == 0 ? "Z:        " : "Num 0:    ";
                string pref1 = z == 0 ? "X:        " : "Num 1:    ";
                string pref2 = z == 0 ? "C:        " : "Num 3:    ";
                string pref3 = z == 0 ? "G:        " : "Num -:    ";
                Console.SetCursorPosition(105, 7); Console.Write("GENERAL");
                Console.SetCursorPosition(105, 8); Console.Write("SHORTCUTS");
                Console.SetCursorPosition(105, 10); Console.Write(pref); Console.ForegroundColor = ConsoleColor.Gray;
                Console.SetCursorPosition(105, 12); Console.Write("character");
                Console.SetCursorPosition(105, 13); Console.Write("info menu"); Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(105, 15); Console.Write(pref1); Console.ForegroundColor = ConsoleColor.Gray;
                Console.SetCursorPosition(105, 17); Console.Write("attributes");
                Console.SetCursorPosition(105, 18); Console.Write("info menu"); Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(105, 20); Console.Write(pref2); Console.ForegroundColor = ConsoleColor.Gray;
                Console.SetCursorPosition(105, 22); Console.Write("equipment");
                Console.SetCursorPosition(105, 23); Console.Write("info menu"); Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(105, 25); Console.Write(pref3); Console.ForegroundColor = ConsoleColor.Gray;
                Console.SetCursorPosition(105, 27); Console.Write("open map"); Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(105, 24); Console.Write(""); Console.ForegroundColor = ConsoleColor.Gray;
                Console.SetCursorPosition(105, 23); Console.Write("");
            }
            if (x == 2)//
            {
                string pref = z == 0 ? "Q, E, R:   " : "Num 7, /, 9:";
                string pref1 = z == 0 ? "E:        " : "Num /:    ";
                string pref2 = z == 0 ? "R:        " : "Num 9:    ";
                string pref3 = z == 0 ? "F:        " : "Num 5:    ";
                string pref4 = z == 0 ? "Escape:   " : "Num -:    ";
                Console.SetCursorPosition(105, 7); Console.Write("SOCIAL");
                Console.SetCursorPosition(105, 8); Console.Write("COMMANDS");
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(105, 10); Console.Write(pref3); Console.ForegroundColor = ConsoleColor.Gray;
                Console.SetCursorPosition(105, 11); Console.Write("confirm");
                Console.SetCursorPosition(105, 12); Console.Write("selection"); Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(105, 14); Console.Write(pref); Console.ForegroundColor = ConsoleColor.Gray;
                Console.SetCursorPosition(105, 15); Console.Write("Respond in"); Console.ForegroundColor = player.EmotionColor(player.Primary);
                Console.SetCursorPosition(105, 16); Console.Write("{0},", player.Primary.ToString().ToUpper()); Console.ForegroundColor = ConsoleColor.Gray;;Console.ForegroundColor = player.EmotionColor(player.Secondary);
                Console.SetCursorPosition(105, 17); Console.Write("{0},", player.Secondary.ToString().ToUpper()); Console.ForegroundColor = ConsoleColor.Gray;Console.ForegroundColor = player.EmotionColor(player.Tertiary);
                Console.SetCursorPosition(105, 18); Console.Write("{0}", player.Tertiary.ToString().ToUpper());  Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(105, 20); Console.Write(pref4); Console.ForegroundColor = ConsoleColor.Gray;
                Console.SetCursorPosition(105, 21); Console.Write("cancel    ");
                Console.SetCursorPosition(105, 22); Console.Write("selection ");

            }
            if (x == 4)
            {
                Console.SetCursorPosition(105, 7); Console.Write("COMMANDS");
                Console.SetCursorPosition(105, 9); Console.Write("Directions:"); Console.ForegroundColor = ConsoleColor.Gray;
                Console.SetCursorPosition(105, 10); Console.Write("select");
                Console.SetCursorPosition(105, 11); Console.Write("attribute");
                Console.SetCursorPosition(105, 12); Console.Write(""); Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(105, 13); Console.Write("Enter:"); Console.ForegroundColor = ConsoleColor.Gray;
                Console.SetCursorPosition(105, 14); Console.Write("confirm");
                Console.SetCursorPosition(105, 15); Console.Write("selection");
                Console.SetCursorPosition(105, 17); Console.Write(""); Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(105, 19); Console.Write(""); Console.ForegroundColor = ConsoleColor.Gray;
                Console.SetCursorPosition(105, 20); Console.Write("");
                Console.SetCursorPosition(105, 21); Console.Write("");
                Console.SetCursorPosition(105, 22); Console.Write(""); Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(105, 24); Console.Write(""); Console.ForegroundColor = ConsoleColor.Gray;
                Console.SetCursorPosition(105, 25); Console.Write("");
                Console.SetCursorPosition(105, 26); Console.Write("");
                Console.SetCursorPosition(105, 27); Console.Write("");
            }
            if (x == 5)
            {
                Console.SetCursorPosition(105, 7); Console.Write("COMMANDS");
                Console.SetCursorPosition(105, 9); Console.Write("Directions:"); Console.ForegroundColor = ConsoleColor.Gray;
                Console.SetCursorPosition(105, 10); Console.Write("move");
                Console.SetCursorPosition(105, 11); Console.Write("across");
                Console.SetCursorPosition(105, 12); Console.Write("map"); Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(105, 14); Console.Write("Spacebar:"); Console.ForegroundColor = ConsoleColor.Gray;
                Console.SetCursorPosition(105, 15); Console.Write("Currently");
                Console.SetCursorPosition(105, 16); Console.Write("Not");
                Console.SetCursorPosition(105, 17); Console.Write("Available"); Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(105, 19); Console.Write("B: '#'"); Console.ForegroundColor = ConsoleColor.Gray;
                Console.SetCursorPosition(105, 20); Console.Write("use item");
                Console.SetCursorPosition(105, 21); Console.Write("in bag:");
                Console.SetCursorPosition(105, 22); Console.Write("('#')"); Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(105, 24); Console.Write("S: '#'"); Console.ForegroundColor = ConsoleColor.Gray;
                Console.SetCursorPosition(105, 25); Console.Write("open");
                Console.SetCursorPosition(105, 26); Console.Write("skills");
                Console.SetCursorPosition(105, 27); Console.Write("(n/a)");
            }
            if (x == 7)
            {
                Console.SetCursorPosition(105, 7); Console.Write("GENERAL");
                Console.SetCursorPosition(105, 8); Console.Write("COMMANDS");
                Console.SetCursorPosition(105, 10); Console.Write("F \\");
                Console.SetCursorPosition(105, 11); Console.Write("NP 5 :"); Console.ForegroundColor = ConsoleColor.Gray;
                Console.SetCursorPosition(105, 12); Console.Write("confirm");
                Console.SetCursorPosition(105, 13); Console.Write("selection"); Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(105, 15); Console.Write("{0}", z > 0 ? "Tab \\" : "");
                Console.SetCursorPosition(105, 16); Console.Write("{0}", z > 0 ? "NP + :" : ""); Console.ForegroundColor = ConsoleColor.Gray;
                Console.SetCursorPosition(105, 17); Console.Write("{0}", z > 0 ? "toggle" : "");
                Console.SetCursorPosition(105, 18); Console.Write("{0}", z > 0 ? "response" : ""); Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(105, 20); Console.Write("{0}", z > 1 ? "Escape \\" : "");
                Console.SetCursorPosition(105, 21); Console.Write("{0}", z > 1 ? "NP - :" : ""); Console.ForegroundColor = ConsoleColor.Gray;
                Console.SetCursorPosition(105, 22); Console.Write("{0}", z > 1 ? "cancel" : "");
                Console.SetCursorPosition(105, 23); Console.Write("{0}", z > 1 ? "selection" : ""); Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(105, 25); Console.Write("{0}", z > 4 ? "W, A, S, D \\" : ""); Console.ForegroundColor = ConsoleColor.Gray;
                Console.SetCursorPosition(105, 26); Console.Write("{0}", z > 4 ? "NP 8, 4, 2, 6 :" : "");
                Console.SetCursorPosition(105, 27); Console.Write("{0}", z > 4 ? "up, left, down, right" : "");
            }
        }
    }
}
