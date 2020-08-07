using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats
{
    public int EQDmg { get { return Dmg + 110; } }
    public int QDmg { get { return Dmg + 80; } }
    public int EDmg { get { return (int)(AP*0.5f) + 40; } }
    public int NormalDmg { get { return Dmg + 15; } }
    public int UltDmg { get { return Dmg + 200; } }
    public int Blood
    {
        get; set;
    }
    public float Speed
    {
        get; set;
    }
    public int Dmg
    {
        get; set;
    }
    public int AP
    {
        get; set;
    }
    public int Armor
    {
        get; set;
    }
    public float Critical
    {
        get; set;
    }
    public float AttackSpeed
    {
        get; set;
    }
    public float ArmorPenetration
    {
        get; set;
    }

    public PlayerStats(int bl, int speed, int dmg, int armor, float critical, float attackspeed, float ap)
    {
        Blood = bl;
        Speed = speed;
        Dmg = dmg;
        Armor = armor;
        Critical = critical;
        AttackSpeed = attackspeed;
        ArmorPenetration = ap;
    }
}
