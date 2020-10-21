using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapLibrary
{
    public class Map
    {
        //Here is where I want to establish the datatype of map to be used in creating maps in my game.
        //fields
        /*
         int width //the width of the map being created
         int height //the height of the map being created
         string[] _rows //the string array that will hold each line of the map being created
         int numberOfEndings //the number of desired possible endings for the floor of the dungeon, will be 1 for the tutorial
         int numberOfStarts //the number of desired possible starts for the floor of the dungeon, will be 1 for the tutorial
         int numberOfRooms //the number of desired possible rooms for the floor of the dungeon, will be 10 for the tutorial
                             this numberOfRooms will potentially determine the Length of a Room[numberOfRooms] 
         */
        private string[] _rows; //This string[] will be used to build each row/line of the maze, intending to be viewed in ONE console.writeline
                                //NOTE: Will Want A "menu" of choices that interacts with what the user can do on the map at this point....
                                //NOTE: This menu ought to include options such as...
                                //      -Move-  The player will be able to move based on their position relative to the Char adjacent in each
                                //              cardinal direction from the player, example:  H = checked Char      P = player's position
                                //                          .H.
                                //                         H.P.H
                                //                           H
                                //
                                //              H will need to be able to be changed, every time this map is built.  The map will need to be built
                                //              every time a play moves, upon leaving a room, upon running away.  Not every Char that makes up
                                //              The map will need to be a variable.  There will be some constants based on the instance of the map
                                //              being created.  See more on this in notes under methods.
                                //        
                                //      -Character Menu-    The Player can pull up the Character Menu from the Map Menu being created with this
                                //                          object, "Map"
                                //
                                //      -(save?)-   I don't know that I want to tackle this feature, yet
                                //
                                //      -Skills-    The Player will have interactivity between their skills and the map.
                                //                  These skills that interact with the map will have their own child type (SkillExplore)
                                //
                                //      -Inventory- The Player will also have interactivity between their items and the map.
                                //                  These items taht interact with the map will have their own child type (ItemExplore)
                                //
                                //      -Escape-    This will prompt the player if they are sure they wish to end their adventure, and upon
                                //                  "yes" will present an End Screen and End Program

        //properties
        public int Width { get; set; }
        public int StartX { get; set; }
        public int StartY { get; set; }
        public string Name { get; set; }
        public List<Room> Rooms { get; set; } //Rooms.Insert(index, room)
        public Room StartRoom { get; set; } //Create Start rooms for every floor (will look simliar to each other, could have 1 createstartroom(int floor)
        public Room EndRoom { get; set; } //Create End rooms for every floor (will look similar to each other, could have 1 createendroom(int floor)
        public Room CurrentRoom { get; set; }
        public int BossCounter { get; set; }
        public int PlayerX { get; set; }
        public int PlayerY { get; set; }
        public int NextX { get; set; }
        public int NextY { get; set; }
        public int Height { get; set; }
        public Char[,] MapRooms { get; set; } // a 2 dimensional array of chars correlating to existing rooms, will get filled in whenever a room is revealed, as in, when the current room
        public string[] Rows
        {
            get { return _rows; }
            set
            {
                if (value.Length == Height)
                {
                    _rows = value;
                }
                else
                {
                    int counter = 0;
                    _rows = new string[Height];
                    foreach (string rows in _rows)
                    {
                        _rows[counter] = "";
                        counter++;
                    }
                }
            }
        }

        //ctors
        //With the CTOR below, the intention is to insert the Width, Height, Starting X position, Starting Y position, and the Start and End Rooms
        //This information should be able to yeild the rest of the fields within this datatype.
        //Height = the length of Rows[]
        //Width * Height = the length of Chars[]
        //StartX = PlayerX
        //StartY = PlayerY
        public Map(string name, int width, int height, List<Room> rooms)
        {
            Name = name;
            Width = width >= 5 && width <= 20 ? width : 10;
            Height = height >= 5 && height <= 13 ? height : 10;
            Rooms = rooms;
            MapRooms = new Char[height + 1, width + 1];
            BossCounter = 10;
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    MapRooms[y, x] = '.';
                }
            }
            StartRoom = rooms[0];
            CurrentRoom = rooms[0];
            MapRooms[CurrentRoom.MapY, CurrentRoom.MapX] = 'C';
                        //CurrentRoom = rooms[0];
        }

        public void GetExits()
        {
            int index = 0;
            int x = CurrentRoom.MapX;
            int y = CurrentRoom.MapY;
            foreach (bool exit in CurrentRoom.IsExit)
            {
                if (exit)
                {
                    switch (index)
                    {
                        case 0://UP
                            MapRooms[y-1, x] = MapRooms[y - 1, x] == '.' ? 'u' : MapRooms[y - 1, x];
                            break;
                        case 1://RIGHT
                            MapRooms[y, x + 1] = MapRooms[y, x + 1] == '.' ? 'u' : MapRooms[y, x+1];
                            break;
                        case 2://DOWN
                            MapRooms[y+1, x] = MapRooms[y + 1, x] == '.' ? 'u' : MapRooms[y + 1, x];
                            break;
                        case 3://LEFT
                            MapRooms[y, x - 1] = MapRooms[y, x - 1] == '.' ? 'u' : MapRooms[y, x-1];
                            break;
                    }
                }
                index++;
            }
        }

        public void CreateRoom(int x, int y, int exit, int floor)
        {
            bool isBoss = Rooms.Count >= BossCounter ? true : false;
            Random rand = new Random();
            Room newRoom;
            if (isBoss && rand.Next(1, 2) == 1) { newRoom = new Room("boss", exit, x, y); }
            else { newRoom = new Room("random", exit, x, y); }
            CurrentRoom = newRoom;
            #region FixExits
            bool up = CurrentRoom.IsExit[0];
            bool right = CurrentRoom.IsExit[1];
            bool down = CurrentRoom.IsExit[2];
            bool left = CurrentRoom.IsExit[3];
            foreach (Room check in Rooms)//Consider's the structural integrity of the dungeon floor...
            {
                //Left Check
                if (check.MapX == x - 1 & check.MapY == y)//Left check
                {
                    if (check.IsExit[1] == true)
                    {
                        left = true;
                    }
                    else { left = false; }
                }
                //Right Check
                if (check.MapX == x + 1 & check.MapY == y)//Right check
                {
                    if (check.IsExit[3] == true)
                    {
                        right = true;
                    }
                    else { right = false; }
                }
                //Up Check
                if (check.MapX == x & check.MapY == y - 1)
                {
                    if (check.IsExit[2] == true)
                    {
                        up = true;
                    }
                    else { up = false; }
                }
                //Down Check
                if (check.MapX == x & check.MapY == y + 1)
                {
                    if (check.IsExit[0] == true)
                    {
                        down = true;
                    }
                    else { down = false; }
                }
            }
            #endregion
            CurrentRoom.IsExit = new bool[] { up, right, down, left };
            Rooms.Add(newRoom);
            MapRooms[y, x] = 'e';
            int aX = exit == 0 || exit == 2 ? x : exit == 1 ? x + 1 : x - 1;
            int bY = exit == 1 || exit == 3 ? y : exit == 0 ? y - 1 : y + 1;
            MapRooms[bY, aX] = 'e';
            
        }

        //public Map()

        //methods

        //public void Move() {}
        //This will move a character to the next room.  I can set up the options so that the player can ONLY choose to move in a direction that
        //I want them to... Possible ways of doing this:
        // (1) -Check player.Direction, where Direction is an enum containing North/East/South/West or UP/RIGHT/DOWN/LEFT
        //     -

        //public void CreateMap() {}
        //This will create the map based on the player's new position after the move command.
        //public void CreateMap() //Player.CurrentRoom
        ////Requiring the Map gets access to the map's Chars[], which allows for the map's current instance to be stored in the Char[]
        ////Requiring the Room gets access to room the player is currently in's .IsExit[0-3] which will indicate whether or not
        //// 0 - Up, 1- Right, 2- Down, 3- Left are exits, and if so will change the required Chars[index] to 'o' to indicate the player
        ////can move there next.

        ////Need to decide formatting for the map display.  This map display will provide a visual indicator (through console.writeline())
        ////Allowing the player to keep track of their location in the current floor of the dungeon they are on.  It will usually be nested
        ////within a menu that allows the player to -Move- -Character Information -Skills- -Inventory- -Exit Game-  So this will be the
        ////"Exploration Menu" as opposed to other possible menus such as "Social Menu" or "Combat Menu"

        ////To display the map in a console writeline I can store each row of the map into an index of Rows[] by piecing together the Chars[]
        ////To create each line.  If the width is 10, and the height is 10, then, Rows[] = new string[10] and Rows[] = new Char[100]
        ////Chars[0-9], Chars[10-19], Chars[20-29], Chars[30-39], Chars[40-49], Chars[50-59], Chars[60-69], Chars[70-79], Chars[80-89], Chars[90-99]
        ////The abve sets of Chars[] will need to be added to each Rows[] 

        ////What is the math to determine the proper indexing of the chars and rows tho.
        ////The Rows[] can be handled with a foreach, which means that I could set a charCounter to 0 to keep track of the chars

        //{
        //    ////Initialize variables needed
        //    //int[] charIndex = new int[4]; //declares the ints that will be used for Chars[indexing]
        //    int counter = 0;
        //    ////Determine where the player is
        //    Console.Write("/");
        //    do//draws top barrier for map
        //    {
        //        Console.Write("=");
        //        counter++;
        //    } while (counter != 2 * Width);
        //    Console.Write("=\\\n| {0}", Name);
        //    counter = 0;
        //    do//draws top barrier for map
        //    {
        //        Console.Write(" ");
        //        counter++;
        //    } while (counter != (2 * Width) - (1 + Name.Length));
        //    Console.Write(" |\n|");
        //    counter = 0;
        //    do//draws top barrier for map
        //    {
        //        Console.Write("=");
        //        counter++;
        //    } while (counter != 2 * Width);
        //    Console.Write("=|\n");
        //    counter = 0;
        //    Chars[((Width * (CurrentRoom.MapY - 1)) + CurrentRoom.MapX) - 1] = 'p';//setting the char at player's location equal to 'p'

        //    ////Figure out where the exits are in the room
        //    //foreach (bool exit in CurrentRoom.IsExit)
        //    //{
        //    //    if (exit == true) //Checks to see if an exit is needed to be created, then changes the appropriate Chars to 'o'
        //    //    {
        //    //        if (counter == 0) //If the exit is UP
        //    //        {
        //    //            charIndex[0] = ((Width * (CurrentRoom.MapY - 2)) + CurrentRoom.MapX) - 1;
        //    //            Chars[charIndex[0]] = 'o';

        //    //            //Must do the math to find the char directly above current position.  Current position = playerX/Y
        //    //        }
        //    //        else if (counter == 1) //If the exit is RIGHT
        //    //        {
        //    //            charIndex[1] = ((Width * (CurrentRoom.MapY - 1)) + (CurrentRoom.MapX + 1)) - 1;
        //    //            Chars[charIndex[1]] = 'o';
        //    //        }
        //    //        else if (counter == 2) //If the exit is DOWN
        //    //        {
        //    //            charIndex[2] = ((Width * CurrentRoom.MapY) + CurrentRoom.MapX) - 1;
        //    //            Chars[charIndex[2]] = 'o';
        //    //        }
        //    //        else //If the exit is LEFT
        //    //        {
        //    //            charIndex[3] = ((Width * (CurrentRoom.MapY - 1)) + (CurrentRoom.MapX - 1)) - 1;
        //    //            Chars[charIndex[3]] = 'o';
        //    //        }

        //    //    }
        //    //    counter++;
        //    //}//end Exit Chars assignment
        //    UpdateExits(false);
        //    counter = 0;
        //    int charCounter = 0;
        //    foreach (string row in Rows)
        //    {//This causes each index of map.Rows to fill it's respective string and console.write() it
        //        bool loopOn = true;
        //        Rows[counter] = "|";
        //        do //This creates the specific string for the Row[counter]
        //        {
        //            Rows[counter] += " " + Chars[charCounter].ToString();
        //            if ((charCounter - (Width * counter)) == (Width - 1))
        //            {
        //                Rows[counter] += " |\n";
        //                Console.Write(Rows[counter]);
        //                counter++;
        //                loopOn = false;
        //            }
        //            charCounter++;
        //        } while (loopOn);
        //    }//end Foreach string filling
        //    Console.Write("|");
        //    counter = 0;
        //    do//Draws Bottom Barrier for Map
        //    {
        //        Console.Write("=");
        //        counter++;
        //    } while (counter != 2 * Width);
        //    Console.Write("=|\n| P - Player");
        //    counter = 0;
        //    do//Draws Bottom Barrier for Map
        //    {
        //        Console.Write(" ");
        //        counter++;
        //    } while (counter != (2 * Width) - 11);
        //    Console.Write(" |\n| o - Exit");
        //    counter = 0;
        //    do//Draws Bottom Barrier for Map
        //    {
        //        Console.Write(" ");
        //        counter++;
        //    } while (counter != (2 * Width) - 9);
        //    Console.Write(" |\n| . - Revealed Room");
        //    counter = 0;
        //    do//Draws Bottom Barrier for Map
        //    {
        //        Console.Write(" ");
        //        counter++;
        //    } while (counter != (2 * Width) - 18);
        //    Console.Write(" |\n\\");
        //    counter = 0;
        //    do//Draws Bottom Barrier for Map
        //    {
        //        Console.Write("=");
        //        counter++;
        //    } while (counter != 2 * Width);
        //    Console.Write("=/\n");


        //}//end CreateMap()

        //public void UpdateExits(bool dot)
        //{
        //    //here i need to adjust the exits of the current room so that Chars at the appropriate indexes will = '.'
        //    int counter = 0;
        //    int[] charIndex = new int[4];
        //    Chars[((Width * (CurrentRoom.MapY - 1)) + CurrentRoom.MapX) - 1] = 'P';//Setting the char at Player's location equal to 'P'

        //    //Figure out where the exits are in the room
        //    foreach (bool exit in CurrentRoom.IsExit)
        //    {
        //        if (exit == true) //Checks to see if an exit is needed to be created, then changes the appropriate Chars to 'o'
        //        {
        //            if (counter == 0) //If the exit is UP
        //            {
        //                charIndex[0] = ((Width * (CurrentRoom.MapY - 2)) + CurrentRoom.MapX) - 1;
        //                Chars[charIndex[0]] = dot ? '.' : 'o';

        //                //Must do the math to find the char directly above current position.  Current position = playerX/Y
        //            }
        //            else if (counter == 1) //If the exit is RIGHT
        //            {
        //                charIndex[1] = ((Width * (CurrentRoom.MapY - 1)) + (CurrentRoom.MapX + 1)) - 1;
        //                Chars[charIndex[1]] = dot ? '.' : 'o';
        //            }
        //            else if (counter == 2) //If the exit is DOWN
        //            {
        //                charIndex[2] = ((Width * CurrentRoom.MapY) + CurrentRoom.MapX) - 1;
        //                Chars[charIndex[2]] = dot ? '.' : 'o';
        //            }
        //            else //If the exit is LEFT
        //            {
        //                charIndex[3] = ((Width * (CurrentRoom.MapY - 1)) + (CurrentRoom.MapX - 1)) - 1;
        //                Chars[charIndex[3]] = dot ? '.' : 'o';
        //            }

        //        }
        //        counter++;
        //    }//end Exit Chars assignment
        //}
    }
}
