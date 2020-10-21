using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ItemLibrary;
using CharacterLibrary;

namespace EquipmentLibrary
{
    public class Equipment : Item
    {
        public Slot Slot { get; set; } //Slot of item
        public int[] Attributes { get; set; } //Attribute bonuses of item, will be an array of 7 attributes
                                                    //    [0]       [1]         [2]       [3]      [4]       [5]        [6]
                                                    //{strength, defense, intelligence, wisdom, dexterity, agility, resource attribute}
                                                    //if value < 0 ? bonus added to attribute (resulting in a reduced attribute)
                                                    //if value == 0 ? no bonus is applied
                                                    //if value > 0 ? bonus added to attribute (resulting in an increased attribute)
        public int Cost { get; set; } //Sets cost of item

        public Equipment(Slot slot, int[] attributes, int cost, string name, string description, ItemType type) :
        base(name, description, type)
        {
            Slot = slot;
            Attributes = attributes;
            Cost = cost;
        }

        public Equipment(string name) : base(name)
        {
        }

        public Equipment() //Creates a "blank" equipment item
        {
            Slot = Slot.None;
            Attributes = new int[] {0,0,0,0,0,0,0};
            Cost = 0;
        }

        public virtual int GetBonus(string bonus)
        {
            return 0;
        }
    }


}
