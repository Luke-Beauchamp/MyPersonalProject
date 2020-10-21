using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CharacterLibrary;
using PlayerLibrary;
using MapLibrary;
using MenuLibrary;
using SceneLibrary;

namespace DungeonForever
{
    public class Mane
    {

        public static Map CreateMap(int floor)
        {
            Random rand = new Random();
            int x = rand.Next(5, 16);
            int y = rand.Next(3, 9);
            List<Room> rooms = new List<Room> { new Room("", RoomType.Start, 1, new bool[4] { false, true, false, false }, x, y) };
            rooms.ElementAt(0).HasBeenIn = true;
            rooms.ElementAt(0).FloorOn = floor;
            Map map = new Map("Floor: " + floor, 20, 13, rooms);
            map.BossCounter = floor + 7;
            return map;
        }

        public static void StartRoom(Player player, int floor)
        {
            Console.Clear();
            MenuDoes.DrawMenu();
            CharacterInfo.ShortAttributes(player);
            MenuDoes.CommandToggle(0, player.zToggle, player);
            SceneSocial.DrawScene(player);
            SceneSocial.DrawInput(new string[] { "//Continue" }, 0);
            int x = 5;
            int y = 8;
            string[] dialogue = SRString(floor);
            player.CreateSkill(floor);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(7, 8); Console.Write("Plutchik's Soul");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(": It's time to continue your journey.");
            SceneSocial.DrawInput(new string[] { "//Continue" }, 0);
            SceneBattle.PressEnter();
            Console.SetCursorPosition(7, 9); Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("!You have been fully restored!");
            SceneSocial.DrawInput(new string[] { "//Leave Room" }, 0);
            player.Life = player.FindLife();
            player.Resource = player.ResourceAttribute != Attributes.Intuition ? player.FindResource() : 0;
            SceneBattle.PressEnter();
        }

        public static string[] SRString(int floor)
        {
            string[] srString = new string[] { "" };
            string[,] floorIntros = new string[3, 16];
            floorIntros[0, 0] = "";
            floorIntros[1, 0] = "";
            floorIntros[2, 0] = "";
            floorIntros[0, 0] = "";
            floorIntros[1, 0] = "";
            floorIntros[2, 0] = "";
            floorIntros[0, 0] = "";
            floorIntros[1, 0] = "";
            floorIntros[2, 0] = "";
            floorIntros[0, 0] = "";
            floorIntros[1, 0] = "";
            floorIntros[2, 0] = "";
            floorIntros[0, 0] = "";
            floorIntros[1, 0] = "";
            floorIntros[2, 0] = "";
            floorIntros[0, 0] = "";
            floorIntros[1, 0] = "";
            floorIntros[2, 0] = "";
            floorIntros[0, 0] = "";
            floorIntros[1, 0] = "";
            floorIntros[2, 0] = "";
            floorIntros[0, 0] = "";
            floorIntros[1, 0] = "";
            floorIntros[2, 0] = "";
            return srString;
        }

        public static void Initialize(Player player, Map map)
        {
            Console.Clear();
            MenuDoes.DrawMenu();
            CharacterInfo.ShortSheet(player);
            MenuDoes.CommandClear();
            MenuDoes.CommandToggle(0, player.zToggle, player);
            SceneMap.DrawMap(player, map);
        }

        public static Player Loop(Player player, Map map)
        {
            //insert finished switch here.  being able to call this will allow for cutscenes.  -__o don't count on it anytime soon
            //bools
            Console.CursorVisible = false;
            bool menuOn = false;
            bool floorOn = true;
            bool refresh = true;
            //ints  //Refresh(int info, int command, int scene)
            int xRef = -1;
            int yRef = -1;
            int zRef = -1;
            int toggleSelector = -1;
            int toggleInfo = 0;
            int toggleCommand = 0;
            int toggleScene = 0;
            //NPC npc = map.CurrentRoom.Character;

            //Main Loop
            do
            {
                if (refresh)
                {
                    Console.CursorVisible = false;
                    toggleSelector = -1;
                    Initialize(player, map);
                    refresh = false;
                }
                Console.SetCursorPosition(109, 2);
                switch (Console.ReadKey(true).Key)
                {
                    //Opens Map (if menu is on) when Subtract (NumPad) or G is pressed
                    case ConsoleKey.Subtract:
                    case ConsoleKey.G:
                        #region Open Map
                        if (toggleScene == 0)
                        {
                            if (menuOn == true)
                            {
                                menuOn = false;
                                MenuDoes.SceneClear();
                                SceneMap.DrawMap(player, map);
                            }
                        }
                        #endregion
                        break;
                    //Current location set to up (if possible) when NumPad8 or W is pressed
                    case ConsoleKey.W:
                    case ConsoleKey.NumPad8:
                        #region Move Up
                        if (toggleScene == 0)//Map is scene
                        {
                            if (menuOn == false)
                            {
                                if (map.CurrentRoom.IsExit[0])
                                {
                                    toggleSelector = 0;
                                    xRef = map.CurrentRoom.MapX;
                                    yRef = map.CurrentRoom.MapY - 1;
                                    zRef = 2;
                                    SceneMap.FillMap(player, map);
                                    SceneMap.DrawRoom(map.CurrentRoom.MapX, map.CurrentRoom.MapY, 'o');
                                    SceneMap.PreviewMove(xRef, yRef);
                                    SceneMap.UpdateInput(player, map, 0);
                                }
                            }

                        }
                        #endregion
                        break;
                    //Current location set to right (if possible) when NumPad6 or D is pressed
                    case ConsoleKey.D:
                    case ConsoleKey.NumPad6:
                        #region Move Right
                        if (toggleScene == 0)//Map is scene
                        {
                            if (menuOn == false)
                            {
                                if (map.CurrentRoom.IsExit[1])
                                {
                                    toggleSelector = 0;
                                    xRef = map.CurrentRoom.MapX + 1;
                                    yRef = map.CurrentRoom.MapY;
                                    zRef = 3;
                                    SceneMap.FillMap(player, map);
                                    SceneMap.DrawRoom(map.CurrentRoom.MapX, map.CurrentRoom.MapY, 'o');
                                    SceneMap.PreviewMove(xRef, yRef);
                                    SceneMap.UpdateInput(player, map, 1);
                                }
                            }
                        }
                        #endregion
                        break;
                    //Current location set to down (if possible) when NumPad2 or S is pressed
                    case ConsoleKey.S:
                    case ConsoleKey.NumPad2:
                        #region Move Down
                        if (toggleScene == 0)//Map is scene
                        {
                            if (menuOn == false)
                            {
                                if (map.CurrentRoom.IsExit[2])
                                {
                                    toggleSelector = 0;
                                    xRef = map.CurrentRoom.MapX;
                                    yRef = map.CurrentRoom.MapY + 1;
                                    zRef = 0;
                                    SceneMap.FillMap(player, map);
                                    SceneMap.DrawRoom(map.CurrentRoom.MapX, map.CurrentRoom.MapY, 'o');
                                    SceneMap.PreviewMove(xRef, yRef);
                                    SceneMap.UpdateInput(player, map, 2);
                                }
                            }
                        }
                        #endregion
                        break;
                    //Current location set to left (if possible) when NumPad4 or A is pressed
                    case ConsoleKey.A:
                    case ConsoleKey.NumPad4:
                        #region Move Left
                        if (toggleScene == 0)//Map is scene
                        {
                            if (menuOn == false)
                            {
                                if (map.CurrentRoom.IsExit[3])
                                {
                                    toggleSelector = 0;
                                    xRef = map.CurrentRoom.MapX - 1;
                                    yRef = map.CurrentRoom.MapY;
                                    zRef = 1;
                                    SceneMap.FillMap(player, map);
                                    SceneMap.DrawRoom(map.CurrentRoom.MapX, map.CurrentRoom.MapY, 'o');
                                    SceneMap.PreviewMove(xRef, yRef);
                                    SceneMap.UpdateInput(player, map, 3);
                                }
                            }
                        }
                        #endregion
                        break;
                    //Open Character Info Menu when NumPad0 or Z is pressed
                    case ConsoleKey.Z:
                    case ConsoleKey.NumPad0:
                        #region Open Character Info
                        Scene.InputClear();
                        MenuDoes.SceneClear();
                        if (toggleInfo != 0)
                        {
                            toggleInfo = 0;
                            CharacterInfo.ClearArea();
                            MenuDoes.CommandClear();
                            CharacterInfo.ShortSheet(player);
                            MenuDoes.CommandToggle(toggleCommand, player.zToggle, player);
                        }
                        CharacterInfo.LongSheet(player, toggleInfo, -1);
                        menuOn = true;
                        #endregion
                        break;
                    //Open Attributes Menu when NumPad1 or X is pressed
                    case ConsoleKey.X:
                    case ConsoleKey.NumPad1:
                        #region Open Attributes Info
                        Scene.InputClear();
                        MenuDoes.SceneClear();
                        if (toggleInfo != 1)
                        {
                            toggleInfo = 1;
                            CharacterInfo.ClearArea();
                            MenuDoes.CommandClear();
                            CharacterInfo.ShortAttributes(player);
                            MenuDoes.CommandToggle(toggleCommand, player.zToggle, player);
                        }
                        CharacterInfo.LongSheet(player, toggleInfo, -1);
                        menuOn = true;
                        #endregion
                        break;
                    //Open Equipment Menu when NumPad3 or C is pressed
                    case ConsoleKey.C:
                    case ConsoleKey.NumPad3:
                        #region Open Equipment Info
                        Scene.InputClear();
                        MenuDoes.SceneClear();
                        if (toggleInfo != 2)
                        {
                            toggleInfo = 2;
                            CharacterInfo.ClearArea();
                            MenuDoes.CommandClear();
                            CharacterInfo.ShortEquipment(player);
                            MenuDoes.CommandToggle(toggleCommand, toggleInfo, player);
                        }
                        CharacterInfo.LongSheet(player, toggleInfo, -1);
                        menuOn = true;
                        #endregion  
                        break;
                    //Toggle Top/Menu view
                    case ConsoleKey.Tab:
                    case ConsoleKey.Add:
                        #region Toggle Info at Top of Screen
                        //Check InfoToggled
                        //Switch between shortsheet, shortattributes, short equipment
                        switch (toggleInfo)
                        {
                            case 0:
                                CharacterInfo.ClearArea();
                                CharacterInfo.ShortAttributes(player);
                                toggleInfo++;
                                if (toggleCommand == 0) { MenuDoes.CommandClear(); MenuDoes.CommandToggle(toggleCommand, toggleInfo, player); }
                                if (menuOn) { MenuDoes.SceneClear(); CharacterInfo.LongSheet(player, toggleInfo, -1); }
                                break;
                            case 1:
                                CharacterInfo.ClearArea();
                                CharacterInfo.ShortEquipment(player);
                                toggleInfo++;
                                if (toggleCommand == 0) { MenuDoes.CommandClear(); MenuDoes.CommandToggle(toggleCommand, toggleInfo, player); }
                                if (menuOn) { MenuDoes.SceneClear(); CharacterInfo.LongSheet(player, toggleInfo, -1); }
                                break;
                            case 2:
                                CharacterInfo.ClearArea();
                                CharacterInfo.ShortSheet(player);
                                toggleInfo = 0;
                                if (toggleCommand == 0) { MenuDoes.CommandClear(); MenuDoes.CommandToggle(toggleCommand, toggleInfo, player); }
                                if (menuOn) { MenuDoes.SceneClear(); CharacterInfo.LongSheet(player, toggleInfo, -1); }
                                break;
                        }
                        #endregion
                        break;
                    case ConsoleKey.Delete:
                        player.zToggle = player.zToggle == 0 ? 1 : 0;
                        MenuDoes.CommandClear();
                        MenuDoes.CommandToggle(toggleCommand, player.zToggle, player);
                        break;
                    //Confirm selection when NumPad5 or F (or enter) is pressed
                    case ConsoleKey.Enter:
                    case ConsoleKey.F:
                    case ConsoleKey.NumPad5:
                        #region Confirm
                        if (!menuOn)
                        {
                            if (toggleSelector != -1)
                            {
                                //Check Scene
                                switch (toggleScene)
                                {
                                    case 0:
                                        if (map.MapRooms[yRef, xRef] == 'u') //Make new room if "unexplored"
                                        {
                                            map.CreateRoom(xRef, yRef, zRef, map.StartRoom.FloorOn);
                                        }
                                        else //Find old room if "explored"
                                        {
                                            foreach (Room room in map.Rooms)
                                            {
                                                if (room.MapX == xRef & room.MapY == yRef)
                                                {
                                                    map.CurrentRoom = room;
                                                    map.CurrentRoom.HasBeenIn = true;
                                                }
                                            }
                                        }
                                        //SCENE SCENE SCENE!!!!!
                                        //Scene.Room, might need to toggleScene before?
                                        floorOn = Scene.Room(map.CurrentRoom, player); toggleScene = 0; toggleInfo = 0; toggleCommand = 0;
                                        if (floorOn == true)
                                        {
                                            map.CurrentRoom.HasBeenIn = true;
                                            toggleSelector = -1;
                                            refresh = true;
                                        }
                                        break;
                                    case 1:

                                        break;
                                    case 2:

                                        break;
                                }
                            }
                        }
                       
                        #endregion
                        break;
                    //Toggle Command Menu when Backspace of V is pressed
                    case ConsoleKey.Backspace:
                    case ConsoleKey.V:
                        #region Toggle Commands
                        switch (toggleCommand)
                        {
                            case 0:
                                MenuDoes.CommandClear();
                                MenuDoes.CommandToggle(1, player.zToggle, player);
                                toggleCommand++;
                                break;
                            case 1:
                                MenuDoes.CommandClear();
                                MenuDoes.CommandToggle(0, player.zToggle, player);
                                toggleCommand--;
                                break;
                                //case 2: //Antiquated
                                //    MenuDoes.CommandClear();
                                //    MenuDoes.CommandToggle(0, toggleInfo, player);
                                //    toggleCommand = 0;
                                //    break;
                        }
                        #endregion
                        break;
                    //Refresh (debug)
                    case ConsoleKey.Escape:
                        #region Exit Menu
                        menuOn = false;
                        refresh = true;
                        #endregion
                        break;
                }
            } while (floorOn);
            return player;
        }
    }
}
