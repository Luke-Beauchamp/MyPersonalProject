using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ItemLibrary
{
    public class Item
    {
        //Name
        //Description-could include 
        public string Name { get; set; } //Name of item
        public string Description { get; set; }
        public ItemType Type { get; set; }

        //CTOR
        public Item(string name, string description, ItemType type)
        {
            Name = name;
            Description = description;
            Type = type;
        }

        public Item(string name)
        {
            Name = name;
        }

        public Item() //Creates a "blank" item
        {
            Name = "";
            Description = "";
            Type = ItemType.None;
        }
    }
}
