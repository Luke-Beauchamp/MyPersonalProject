using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MapLibrary;

namespace MenuLibrary
{
    //Here I need to build a menu for the Map.  This will call the CreateMap() which means a map is needed for this menu to function.
    //It can check the rooms position on the map if each room contains within it those variables, or consts rather... as they shouldn't
    //change over the course of this game in particular.  (I'll leave them as variables in case I'm feeling spicey in later floors of the
    //dungeon)
    public class MapMenu
    {
        //TODO create the menu for the map
        //Check exits and create options based on exits
        //  int counter = 0;
        //  int picker = 0;
        //  Foreach (bool exit in map.IsExit){if(exit){picker++;Console.Write("({0}) {1}{2}", picker,
        //  counter == 0 ? "UP" : counter == 1 ? "RIGHT" : counter == 2 ? "DOWN" : counter == 3 ? "LEFT",
        //  picker == 0 ? "\t\t" : picker == 1 ? "\n" : picker == 2 ? "\t\t" : picker == 3 ? "\n")

        //public static void MenuMap(Map map)
        //{
        //    int counter;
        //    int picker;
        //    ConsoleKey userKey;
        //    bool menuOn = true;
        //    bool eachOn;
        //    Room switchRoom = new Room();
        //    do
        //    {
        //        eachOn = true;
        //        counter = 0;
        //        picker = 1;
        //        string[] pickers = { "", "", "", "" };
        //        map.CreateMap();
        //        Console.Write("Which direction will you move? (Press Tab to Toggle Menu)\n");
        //        foreach (bool exit in map.CurrentRoom.IsExit)
        //        {
        //            if (exit)
        //            {
        //                //picker++;
        //                //I need to swap out picker for pickers.
        //                pickers[picker-1] = counter == 0 ? "Up" : counter == 1 ? "Right" : counter == 2 ? "Down" : "Left";
        //                Console.Write("({1}) {0}\n",
        //                pickers[picker-1],
        //                picker++);
        //            }
        //            counter++;
        //        }
        //        userKey = Console.ReadKey(true).Key;
        //        switch (userKey)
        //        {
        //            case ConsoleKey.D1://Moves the player towards the direction stored in pickers[0] and updates current room
        //                UpdateRoom(map, pickers, 0);
        //                //Go To CurrentRoom
        //                break;
        //            case ConsoleKey.D2://Moves the player towards the direction stored in pickers[1] and updates current room
        //                UpdateRoom(map, pickers, 1);
        //                //Go To CurrentRoom
        //                break;
        //            case ConsoleKey.D3://Moves the player towards the direction stored in pickers[2] and updates current room
        //                UpdateRoom(map, pickers, 2);
        //                //Go To CurrentRoom
        //                break;
        //            case ConsoleKey.D4://Moves the player towards the direction stored in pickers[3] and updates current room
        //                UpdateRoom(map, pickers, 3);
        //                //Go To CurrentRoom
        //                break;
        //            case ConsoleKey.Tab:
        //                Console.WriteLine("Switching to Main Menu!"); //will need to call the main menu here
        //                //The main menu should have -Explore-Character-Skills-Inventory-Exit Game-
        //                break;
        //            default:
        //                Console.Clear();
        //                Console.WriteLine("Please Enter a Valid Choice.");
        //                break;
        //        }

        //    } while (menuOn);
        //}//end menu map

        //private static void UpdateRoom(Map map, string[] pickers, int pick)
        //{
        //    bool eachOn = true;
        //    foreach (Room room in map.Rooms)
        //    {
        //        switch (pickers[pick])
        //        {
        //            case "Up"://TODO also switch the Chars at the appropriate index to the appropriate values....
        //                if (room.MapX == map.CurrentRoom.MapX && room.MapY == map.CurrentRoom.MapY - 1 && eachOn)
        //                {
        //                    map.UpdateExits(true);
        //                    map.CurrentRoom = room;
        //                    Console.Clear();
        //                    eachOn = false;
        //                }
        //                break;
        //            case "Right":
        //                if (room.MapX == map.CurrentRoom.MapX + 1 && room.MapY == map.CurrentRoom.MapY && eachOn)
        //                {
        //                    map.UpdateExits(true);
        //                    map.CurrentRoom = room;
        //                    Console.Clear();
        //                    eachOn = false;
        //                }
        //                break;
        //            case "Down":
        //                if (room.MapX == map.CurrentRoom.MapX && room.MapY == map.CurrentRoom.MapY + 1 && eachOn)
        //                {
        //                    map.UpdateExits(true);
        //                    map.CurrentRoom = room;
        //                    Console.Clear();
        //                    eachOn = false;
        //                }
        //                break;
        //            case "Left":
        //                if (room.MapX == map.CurrentRoom.MapX - 1 && room.MapY == map.CurrentRoom.MapY && eachOn)
        //                {
        //                    map.UpdateExits(true);
        //                    map.CurrentRoom = room;
        //                    Console.Clear();
        //                    eachOn = false;
        //                }
        //                break;
        //            default:
        //                break;
        //        }
        //    }
        //}//end UpdateRoom()
    }
}
