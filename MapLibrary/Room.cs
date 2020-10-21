using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CharacterLibrary;
using ItemLibrary;
using MonsterLibrary;
using NPCLibrary;

namespace MapLibrary
{
    //In this class I will establish the Room object.
    //It will be a parent type to other, more specific types of rooms.
    //Each room will need this information about it to be relevant to the dungeon.
    //  -Number of Exits-   Will !always be at least 1 if some rooms potentially "teleport" you to another room....
    //  -Description-       Will describe, in text, the room.  This could alternatively be an object Description
    //                      which could contain the Type of room (set up as an enum)
    //  -Is Starting Room-  The first room in the dungeon will be a little different than the rest
    //                      It will call a Menu that will allow the user to Update their Attributes/Skills/Equipment
    //                      with the experience from the previous dungeon level...
    //  -Is Ending Room-    This will check to see if this is the last room in the dungeon
    //  -Has Been In-       This will check to see if a player has been in the room before
    //  -TBD-
    //
    //I need to figure out the different "types" of rooms.
    //                               Room
    //       Tunnel--Maze\             |          /Combat--Boss
    //                    \____________|_________/                       
    //                    /                      \
    //     Portal--Puzzle/                        \Social--Vendor
    //                                                     --
    public class Room
    {
        //Fields
        private int _numberOfExits;

        //Properties
        public string Description { get; set; }
        public int FloorOn { get; set; }
        public RoomType Type { get; set; } //Will really determine how the room is created...
        public NPC Character { get; set; } //Simplified the below process by just making an array that I can fill w/ characters
        public Monster Enemy { get; set; }
        //public Character Character1 { get; set; } //These Characters can be used when creating the room
        //public Character Character2 { get; set; } //They can be filled based on the room type
        //public Character Character3 { get; set; } //For combat: Fill with potential monsters in that room...
        public bool HasBeenIn { get; set; } //A bool to see if the player has already been in the room
        public int MapX { get; set; } //Location on the dungeon floor (x-coordinate)
        public int MapY { get; set; } //Location on the dungeon floor (y-coordinate)
                                           //Will perhaps respawn a monster in revisted rooms.
                                           //This could be an effect mitigated by a skill or item
        public bool[] IsExit { get; set; } //A bool array for which directions an exit exists for this room. [0.up, 1.right, 2.down, 3.left]
        public int NumberOfExits
        {
            get { return _numberOfExits; }
            set { _numberOfExits = value > 4 ? 4 : value < 0 ? 0 : value; }
        }
        //public bool IsStartingRoom { get; set; } Commenting these out for now.  I think I am going to make the starting room and ending room
        //public bool IsEndingRoom { get; set; }   Children of Room instead

        //Constructors
        public Room(string description, RoomType type, int numberOfExits, bool[] isExit, int mapX, int mapY)
        {
            Description = description;
            Type = type;
            NumberOfExits = numberOfExits;
            IsExit = isExit; //This array MUST have a length of 4, and the bools represent TRUE if that direction is an exit, FALSE otherwise
            HasBeenIn = false; //When creating a new room, the player will not have been in it...
            MapX = mapX;
            MapY = mapY;
        }

        public Room(string custom, int exit, int mapX, int mapY) //Custom Random Room CTOR
        {
            Random rand = new Random();
            FloorOn = 0;
            Description = custom; //for reference
            switch (custom)
            {
                case "random":
                    Type = RoomType.Combat;
                    break;
                case "boss":
                    Type = RoomType.Boss;
                    break;
                default:
                    break;
            }
            //Figure Exits
            IsExit = new bool[4] { false, false, false, false };
            IsExit[exit] = true;
            MapX = mapX; MapY = mapY;
            int randomize = 3;
            int counter = 0;
            foreach (bool canExit in IsExit)
            {
                if (!canExit)
                {
                    
                    if (1 == rand.Next(1, randomize))
                    {
                        bool impossible = false;
                        switch (counter)
                        {
                            case 0: //up
                                impossible = mapY == 1 ? true : false; 
                                break;
                            case 1: //right
                                impossible = mapX == 20 ? true : false;
                                break;
                            case 2: //down
                                impossible = mapY == 13 ? true : false;
                                break;
                            case 3: //left
                                impossible = mapX == 1 ? true: false;
                                break;
                        }
                        if (impossible == false)
                        {
                            IsExit[counter] = true;
                            randomize++;
                        }
                    } else { randomize--; }
                }
                counter++;
            }
            //Figure NPC

            
        }

        public Room(List<string> descriptions, int picker, Direction direction)
        //This will create a random room with a random description out of the List<string> descriptions, with a random (or not) MapType
        {

            //This will insert a random description out of the descriptions[]
            Random rand = new Random();
            int index = rand.Next(0, descriptions.Count);
            Description = descriptions[index];
            descriptions.RemoveAt(index); //I believe this will remove the description from the List wherever this is being created at...

            // By setting picker to a random number (between 0 and 8 currently) you can randomly decide the RoomType.  You can decide 
            // whether you want to not include room types by setting the random number between 0 and:
            //
            // Boss-7 || "" && Portal-6 || "" && Tunnel-5 || "" && Vendor-4 || "" && Puzzle-3 || "" && Maze-2 || "" && Combat-1
            // I am leaving out RoomType.Start and RoomType.End because these will *NEVER* be randomly created
            
            NumberOfExits = rand.Next(1, 4);
            //Check direction, 
            switch (direction)
            {
                case Direction.Up:
                    IsExit[2] = true;
                    break;
                case Direction.Right:
                    IsExit[3] = true;
                    break;
                case Direction.Down:
                    IsExit[0] = true;
                    break;
                case Direction.Left:
                    IsExit[1] = true;
                    break;
            }

            switch (NumberOfExits)
            {
                case 1:
                    //If only 1 exit, it MUST be from the direction the player was facing, see direction enum
                    break;
                case 2:
                    //If 2 exits, 1 MUST be in the direction
                    break;
                case 3:
                    //If 3 exits 1 must be in the direction
                    break;
                default:
                    break;
            }
            //IsStartingRoom = false; //Will not use a random room generator for the starting level of any dungeon floor
            //IsEndingRoom = false; //Will not use a random room generator for the ending level of any dungeon floor
            HasBeenIn = false;
        }

        //Methods

    }
}
