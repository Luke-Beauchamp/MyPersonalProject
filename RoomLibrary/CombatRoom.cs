using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MapLibrary;

namespace RoomLibrary
{
    public class CombatRoom
    {
        //Here I will define the child of Room, CombatRoom
        //Will need an array of Monsters to store the potential monsters for the room
        //Will need an array of Items that could potentially drop from the monsters, or I could put this functionality into the Monsters
        //Could define objects in the room that boost or nullify emotions, enhancing or negating the skills that are based on emotions
        //In a combat room I want the player to engage in combat with 1 (or multiple) enemy
        //I should probably preface this combat and could give monsters a base emotion that causes skills of that emotion type to have less
        //of an effect, and skills of the emotion opposite of that emotion to have more of an effect... but thats not regarding this object...
        //Oh wait; it would matter in the room actually, but maybe not in the object.  As in, when the room is happening...
        //Kinda realizing I don't know that I actually need to make this child of Room, as room has an array of characters already that can
        //store the monsters, and monsters can store the items, so really what's different about this room than any other?
        //By having the type as a field of Room, I can adjust whatever menu is used during a room interaction by checking the type.  So, for
        //now I will move back to the menus to get the functionality down, and might revisit this in the process...
    }
}
