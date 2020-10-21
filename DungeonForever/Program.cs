using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SceneLibrary;
using CharacterLibrary;
using PlayerLibrary;
using MapLibrary;
using DungeonForever;

namespace DungeonForever
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            
            bool gameOver = true;
            int floor = 1;
            Player player = Scene.Initialize();
            do
            {
                player.Floor = floor;
                Map map = Mane.CreateMap(floor);
                Mane.StartRoom(player, floor);
                player = Mane.Loop(player, map);
                gameOver = player.Life != 0 ? true : false;
                floor++;
            } while (gameOver);
        }
    }
}



