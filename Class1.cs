﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heroes_and_NoobCo
{
    //Базовый класс оружия
    abstract class Weapon
    {
        public string damagetype = "";
        public string name = "";
        public int basedamage;
        public int bonus;
        public int FullDamage {get => (int)basedamage + (int)bonus; set => bonus = value;}
    }
    internal class Sword : Weapon
    {
        public Sword () : base ()
        {
            name = "sword";
            damagetype = "physical";
            basedamage = 5;
        }
    }
    internal class Dagger : Weapon
    {
        public Dagger () : base()
        {
            name = "dagger";
            damagetype = "physical";
            basedamage = 4;
        }
    }
    internal class Staff : Weapon
    {
        public Staff () : base()
        {
            name = "staff";
            damagetype = "physical";
            basedamage = 15;
        }
    }
    internal class Cast : Weapon
    {
        public Cast ()
        {
            name = "chain lightning";
            damagetype = "magic";
            basedamage = 10;
        }
    }
    //Базовый класс героя
    abstract class Hero
    {
        internal string name = "";
        internal int power;
        internal int agility;
        internal int health;
        internal int intellect;
        internal Weapon? weapon;
        internal Cast? cast;
        
        internal virtual int Power { get => power; set => power = value; }
        internal virtual int Health { get => health; set => health = value; }
        internal virtual string Name { get => name; set => name = value; }
        internal virtual int Agility { get => agility; set => agility = value; }
        internal virtual int Intellect { get => intellect; set => intellect = value; }

        internal virtual int HealthRate { get => (int)health * 4; set => health = value; }
        internal virtual int Mana { get => (int)intellect * 4;  set => intellect = value; }

        internal virtual int Armor { get => (int)agility / 2; set => agility = value; }
        internal virtual int MageArmor { get => (int)intellect / 2; set => agility = value; }

        internal virtual Cast Cast { get => cast; set => cast = value; }
        internal virtual Weapon Weapon { get => weapon; set => weapon = value; }
        internal int currenthealth;
        internal int currentmana;
        public Hero (int power, int agility, int health, int intellect, string name)
        {
            Power = power;
            Health = health;
            Name = name;
            Agility = agility;
            Intellect = intellect;
            HealthRate = health;
            Mana = intellect;
            Armor = agility;
            MageArmor = intellect;
            Weapon = weapon;
            Cast = cast;
            currenthealth = HealthRate;
            currentmana = Mana;
        }
    }
    class Knight : Hero
    {
        internal override int HealthRate { get => (int)health * 4 + 15; set => health = value; }
        internal override int Power  { get => (int)power + 2; set => power = value; }
        internal override int Armor { get => ((int)agility / 2) + 2; set => agility = value; }
        internal override Weapon Weapon { get => base.Weapon; set => base.Weapon = new Sword(); }

        public Knight(int power, int agility, int health, int intellect, string name) :
            base(power, agility, health, intellect, name)
        {
            Weapon.FullDamage = this.Power;
        }
    }
    class Thief : Hero
    {
        internal override int Agility { get => (int)agility + 3; set => agility = value; }
        internal override int Armor { get => ((int)agility + 3) / 2; set => agility = value; }
        internal override Weapon Weapon { get => base.Weapon; set => base.Weapon = new Dagger(); }
        public Thief(int power, int agility, int health, int intellect, string name) :
            base(power, agility, health, intellect, name)
        {           
            Weapon.FullDamage = this.Agility;
        }
    }
    class Mage : Hero
    {
        internal override int Intellect { get => (int)intellect + 5; set => intellect = value; }
        internal override int Mana { get => ((int)intellect + 5) * 4 + 25; set => intellect = value; }
        internal override int MageArmor { get => ((int)intellect + 5) / 2 + 2; set => intellect = value; }
        internal override Weapon Weapon { get => base.Weapon; set => base.Weapon = new Staff(); }
        internal override Cast Cast { get => base.Cast; set => base.Cast = new Cast(); }
        public Mage(int power, int agility, int health, int intellect, string name) :
            base(power, agility, health, intellect, name)
        {           
            Weapon.FullDamage = this.Power;
            Cast.FullDamage = this.Intellect;
        }
    }
}
