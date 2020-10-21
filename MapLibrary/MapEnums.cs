using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapLibrary
{
    public enum RoomType
    {
        Start,
        Unexplored,
        Explored,
        Boss,
        Combat,
        Maze,
        Tunnel,
        Social,
        Vendor,
        Puzzle,
        Portal,
        End
    }

    public enum Direction
    {
        Up,
        Right,
        Down,
        Left
    }
}