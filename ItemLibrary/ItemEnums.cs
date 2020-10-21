using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemLibrary
{
    public enum ItemType
    {
        Equip, //Equipment
        Combat, //Combat Only Item
        Exploration, //Exploration Only Item
        Consumable, //Could be either Combat or Exploration
        Quest, //Will be static items, useful for completing quests
        None

    }
    public enum Slot
    {
        Head, //Emotional Adjustment?
        Outfit, //Added Flexibility-Might give more accessory slots-Might have more stat combinations-Might give other bonuses
        Amulet, //Will adjust primary stat
        Accessory, //Could modify a variety of things
        MainHand, //Weapons, will have damage
        OffHand, //Could modify a variety of things, but could keep it simple with shields increasing block chance/defense or tomes increasing
                //Knowledge/Wisdom
        TwoHand,//Weapons, will have damage
        None
    }

    public enum OffHandType //OffHand Equipment will not have a damage modifier, instead it might offer some other bonus along with its stats
    {
        PowerFist, //Strength- Enhances strength / Stamina || Increases Min/Max Damage of Weapon by fixed amount
        ArmGuard, //Defense- Enhances defense / Stamina || Increases block chance by % / Can be used with a 2 handed weapon
        Tome, //Intelligence- Enhances Intelligence / Knowledge || Increases Skill Damage by %
        Orb, //Wisdom- Enhances Wisdom / Knowledge || Increases Hit/Block/Resist/Dodge Chance cumulatively as a %
        Powder, //Dexterity-  Enhances Dexterity / Intuition 
        Lasso, //Agility- Enhances Agility / Intuition
        None


    }
}
