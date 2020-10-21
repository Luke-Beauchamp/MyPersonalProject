using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MapLibrary;
using RoomLibrary;
using CharacterLibrary;
using PlayerLibrary;

namespace SceneLibrary
{
    public class SceneMap
    {

        public static void DrawMap(Player player, Map map)
        {
            InputClear();
            DrawInput(player, map);
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.SetCursorPosition(2, 6); Console.Write(
                "\\             \\____________________________________________________________________________________/");
            Console.SetCursorPosition(23, 7); Console.Write("/");
            Console.SetCursorPosition(22, 8); Console.Write("/");
            Console.SetCursorPosition(4, 9); Console.Write("_________________/");
            Console.SetCursorPosition(4, 10); Console.Write("/\\/\\/\\/\\/\\/\\/\\/\\/\\");
            //Console.SetCursorPosition(4, 11); Console.Write("__________________\\_____");
            for (int y = 7; y < 22; y++)
            {
                Console.SetCursorPosition(28, y); Console.Write("{0}", y % 2 != 0 ? "\\" : "/");
                if (y < 11)
                {
                    Console.SetCursorPosition(23, y); Console.Write("{1}{0}{1}{0}{1}", y % 2 != 0 ? "\\" : "/", y % 2 == 0 ? "\\" : "/");
                }
                if (y == 9)
                {
                    Console.SetCursorPosition(22, y); Console.Write("\\/");
                }
                if (y == 10)
                {
                    Console.SetCursorPosition(22, y); Console.Write("/\\");
                }
                //else if (y == 11)
                //{
                //    Console.SetCursorPosition(23, y); Console.Write("\\\\\\\\\\");
                //}
            }
            Console.SetCursorPosition(4, 21); Console.Write(
                "\\_______________________\\________________________________________________________________________/");
            Console.SetCursorPosition(4, 22); Console.Write(
                "\\________/              \\________________________________/                \\______________________/");
            Console.SetCursorPosition(2, 23); Console.Write(
                "");
            for (int y = 7; y < 23; y++)
            {
                Console.SetCursorPosition(101, y); Console.Write(" "); Console.SetCursorPosition(2, y); Console.Write(" ");
                Console.SetCursorPosition(3, y); Console.Write("{0}", y % 2 != 0 ? "\\" : "/");
                Console.SetCursorPosition(100, y); Console.Write("{0}", y % 2 != 0 ? "/" : "\\");
            }
            Console.ForegroundColor = ConsoleColor.White; Console.SetCursorPosition(6, 7); Console.Write("//MAP");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(7, 8); Console.Write(map.Name);
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(7, 12); Console.Write("//Rapport");
            player.WriteRapport(Emotion.Anger, 12, 13);
            player.WriteRapport(Emotion.Anticipation, 5, 14);
            player.WriteRapport(Emotion.Disgust, 10, 15);
            player.WriteRapport(Emotion.Fear, 13, 16);
            player.WriteRapport(Emotion.Joy, 14, 17);
            player.WriteRapport(Emotion.Sadness, 10, 18);
            player.WriteRapport(Emotion.Surprise, 9, 19);
            player.WriteRapport(Emotion.Trust, 12, 20);
            //Fill Map
            FillMap(player, map);


        }

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
        public static void DrawInput(Player player, Map map)
        {
            //Need to decide what the input area looks like for Map Scene
            //Need to draw player input area... ( Starting at ( 2, 23) - Ending at (102, 27) )
            bool isLeft = map.CurrentRoom.IsExit[3];
            bool isUp = map.CurrentRoom.IsExit[0];
            bool isRight = map.CurrentRoom.IsExit[1];
            bool isDown = map.CurrentRoom.IsExit[2];
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            //Will basically just give the options of movement in the User Input area
            Console.SetCursorPosition(2, 23);
            Console.Write("/________/                 \\_______________________________________________________________________\\");
            for (int y = 24; y < 29; y++)
            {
                Console.SetCursorPosition(42, y); Console.Write("|");
                Console.SetCursorPosition(101, y); Console.Write("]");
            }
            Console.SetCursorPosition(3, 24); Console.Write("/                                    \\");
            Console.SetCursorPosition(2, 25); Console.Write("/"); Console.SetCursorPosition(41, 25); Console.Write("\\");
            Console.SetCursorPosition(2, 28); Console.Write("\\"); Console.SetCursorPosition(41, 28); Console.Write("/");
            Console.SetCursorPosition(43, 23); Console.Write("________________/                   \\___________________");
            Console.SetCursorPosition(44, 24); Console.Write("/"); Console.SetCursorPosition(99, 24); Console.Write("\\");
            Console.SetCursorPosition(43, 25); Console.Write("/"); Console.SetCursorPosition(100, 25); Console.Write("\\");
            Console.SetCursorPosition(43, 28); Console.Write("\\"); Console.SetCursorPosition(100, 28); Console.Write("/");
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(15, 23); Console.Write("Map Legend"); Console.SetCursorPosition(62, 23); Console.Write("Select Movement");//Optional header
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(5, 25); Console.Write("(C) =");
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(22, 25); Console.Write("{u} =");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.SetCursorPosition(5, 26); Console.Write("{S} =");
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(22, 26); Console.Write("{e} =");
            Console.SetCursorPosition(5, 27); Console.Write(""); Console.SetCursorPosition(24, 27); Console.Write("");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(11, 25); Console.Write("Current"); Console.SetCursorPosition(28, 25); Console.Write("Unexplored");
            Console.SetCursorPosition(11, 26); Console.Write("Start"); Console.SetCursorPosition(28, 26); Console.Write("Explored");
            Console.SetCursorPosition(11, 27); Console.Write(""); Console.SetCursorPosition(30, 27); Console.Write("");

            //Directional Movement
            
        }

        public static void DrawRoom(int mapX, int mapY, char room)
        {
            int x = 32 + (mapX * 3);
            int y = 7 + mapY;
            Console.ForegroundColor = room == 'C' ? ConsoleColor.Green : room == 'S' ? ConsoleColor.Blue : ConsoleColor.Gray;
            string drawnRoom = room != 'C' ? "{" + room + "}" : "(C)";
            Console.SetCursorPosition(x, y); Console.Write(drawnRoom);
        }

        public static void FillMap(Player player, Map map)
        {
            UpdateInput(player, map, -1);
            int xCounter = 1;
            int yCounter = 1;
            Console.ForegroundColor = ConsoleColor.White;
            map.GetExits();

            //Draws what's known
            do
            {
                xCounter = 1;
                do
                {
                    if (map.MapRooms[yCounter, xCounter] == 'S' || map.MapRooms[yCounter, xCounter] == 'B' ||
                        map.MapRooms[yCounter, xCounter] == 'e' || map.MapRooms[yCounter, xCounter] == 'u')
                    {

                        DrawRoom(xCounter, yCounter, map.MapRooms[yCounter, xCounter]);
                    }
                    xCounter++;
                } while (xCounter <= map.Width);
                yCounter++;
            } while (yCounter <= map.Height);
            //Update Current Location
            DrawRoom(map.StartRoom.MapX, map.StartRoom.MapY, 'S');
            DrawRoom(map.CurrentRoom.MapX, map.CurrentRoom.MapY, 'C');
            //Draw Exits

            
            //Something like ... width = 10 + ((93 - (width*3))/2)

        }

        public static void UpdateInput(Player player, Map map, int selector)
        {
            int selected = selector; //Checking which direction to update, -1 is default
            bool isLeft = map.CurrentRoom.IsExit[3];
            bool isUp = map.CurrentRoom.IsExit[0];
            bool isRight = map.CurrentRoom.IsExit[1];
            bool isDown = map.CurrentRoom.IsExit[2];
            //UP
            Console.ForegroundColor = selected == 0 ? ConsoleColor.Green : isUp == true ? ConsoleColor.White : ConsoleColor.DarkGray;
            Console.SetCursorPosition(62, 25); Console.Write("/\\"); 
            Console.SetCursorPosition(61, 26); Console.Write("/||\\");
            Console.SetCursorPosition(62, 27); Console.Write("!!");

            //RIGHT
            Console.ForegroundColor = selected == 1 ? ConsoleColor.Green : isRight == true ? ConsoleColor.White : ConsoleColor.DarkGray;
            Console.SetCursorPosition(89, 25); Console.Write("  \\\\");
            Console.SetCursorPosition(89, 26); Console.Write(">>>>}");
            Console.SetCursorPosition(89, 27); Console.Write("  //");

            //DOWN
            Console.ForegroundColor = selected == 2 ? ConsoleColor.Green : isDown == true ? ConsoleColor.White : ConsoleColor.DarkGray;
            Console.SetCursorPosition(76, 25); Console.Write("||");
            Console.SetCursorPosition(75, 26); Console.Write("\\||/");
            Console.SetCursorPosition(76, 27); Console.Write("\\/");

            //LEFT
            Console.ForegroundColor = selected == 3 ? ConsoleColor.Green : isLeft == true ? ConsoleColor.White : ConsoleColor.DarkGray;
            Console.SetCursorPosition(46, 25); Console.Write(" //");
            Console.SetCursorPosition(46, 26); Console.Write("{<<<<");
            Console.SetCursorPosition(46, 27); Console.Write(" \\\\");

            //Console.ForegroundColor = isUp == true ? ConsoleColor.White : ConsoleColor.DarkGray;
            //Console.SetCursorPosition(60, 25); Console.Write("Up Arrow: Move Up");
            //Console.ForegroundColor = isDown == true ? ConsoleColor.White : ConsoleColor.DarkGray;
            //Console.SetCursorPosition(60, 27); Console.Write("Down Arrow: Move Down");
            //Console.ForegroundColor = isLeft == true ? ConsoleColor.White : ConsoleColor.DarkGray;
            //Console.SetCursorPosition(48, 26); Console.Write("Left Arrow: Move Left");
            //Console.ForegroundColor = isRight == true ? ConsoleColor.White : ConsoleColor.DarkGray;
            //Console.SetCursorPosition(76, 26); Console.Write("Right Arrow: Move Right");
        }

        public static void PreviewMove(int xRef, int yRef)
        {
            int x = 32 + (xRef * 3);
            int y = 7 + yRef;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(x, y); Console.Write("(C)");
        }
    }
}
