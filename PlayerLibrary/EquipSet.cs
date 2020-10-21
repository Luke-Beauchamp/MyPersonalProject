using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EquipmentLibrary;

namespace PlayerLibrary
{
    public class EquipSet//FIX RANDOM ITEM CTORS
    {
        public Head EquippedHead { get; set; } //Primary Mod as % .PrimaryMod
        public Outfit EquippedOutfit { get; set; } //Chances Mods as %s .Chances [0: hit, 1: block, 2: resist, 3:dodge] // Limited either chances or attrs by 2
        public Amulet EquippedAmulet { get; set; } //Resource Mod as % .ResourceMod
        public Accessory EquippedAccessory { get; set; } //MaxLife Mod fixed, MaxResource Mod fixed, StatusImmunity, .MaxLifeMod .MaxResourceMod .StatusImmunity[index]
        public Weapon EquippedWeapon { get; set; } // .MinDamage .MaxDamage .HitChanceBonus .Status .StatusChance
        public OffHand EquippedOffHand { get; set; } // .OHPrimaryBonus .OHResourceBonus .OHBonus (powerfist: min max damage, armguard: block, tome: skill damage, orb: resist, lasso: hit,
                                                    //powder: dodge)

        public EquipSet(Head equippedHead, Outfit equippedOutfit, Amulet equippedAmulet, Accessory equippedAccessory, Weapon equippedWeapon, OffHand equippedOffHand)
        {
            EquippedHead = equippedHead;
            EquippedOutfit = equippedOutfit;
            EquippedAmulet = equippedAmulet;
            EquippedAccessory = equippedAccessory;
            EquippedWeapon = equippedWeapon;
            EquippedOffHand = equippedOffHand;
        }

        public EquipSet() //Creates a "blank" equipment set
        {
            EquippedHead = new Head();
            EquippedOutfit = new Outfit();
            EquippedAmulet = new Amulet();
            EquippedAccessory = new Accessory();
            EquippedWeapon = new Weapon();
            EquippedOffHand = new OffHand();
        }

    }
}
