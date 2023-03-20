using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heroes_and_NoobCo
{
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
            name = "Sword";
            damagetype = "physical";
            basedamage = 5;
        }
    }
    internal class Dagger : Weapon
    {
        public Dagger () : base()
        {
            name = "Dagger";
            damagetype = "physical";
            basedamage = 4;
        }
    }
    internal class Staff : Weapon
    {
        public Staff () : base()
        {
            name = "Staff";
            damagetype = "physical";
            basedamage = 15;
        }
    }
    internal class Cast : Weapon
    {
        public Cast () : base()
        {
            name = "Cast";
            damagetype = "magic";
            basedamage = 0;
        }
    }
    abstract class Hero
    {
        internal string name = "";
        internal int power;
        internal int agility;
        internal int health;
        internal int intellect;
        internal Weapon? weapon;
        internal int cast;
        
        internal virtual int Power { get => power; set => power = value; }
        internal virtual int Health { get => health; set => health = value; }
        internal virtual string Name { get => name; set => name = value; }
        internal virtual int Agility { get => agility; set => agility = value; }
        internal virtual int Intellect { get => intellect; set => intellect = value; }

        internal virtual int HealthRate { get => (int)Health * 4; set => Health = value; }
        internal virtual int Mana { get => (int)Intellect * 4;  set => Intellect = value; }

        internal virtual int Armor { get => (int)Agility / 2; set => Agility = value; }
        internal virtual int MageArmor { get => (int)Intellect / 2; set => Intellect = value; }
        
        internal virtual Weapon Weapon { get => weapon; set => weapon = value; }
        internal int Cast { get => cast + Intellect; set => cast = value; }
        internal int currenthealth;
        internal int currentmana;
        public Hero (int power, int agility, int health, int intellect, string name)
        {
            this.Power = power;
            this.Health = health;
            this.Name = name;
            this.Agility = agility;
            this.Intellect = intellect;
            this.HealthRate = this.Health;
            this.Mana = this.Intellect;
            this.Armor = this.Agility;
            this.MageArmor = this.Intellect;
            this.Cast = 10;
            this.currenthealth = this.HealthRate;
            this.currentmana = this.Mana;
        }
    }
    class Knight : Hero
    {
        internal override int HealthRate { get => base.HealthRate + 15;}
        internal override int Power  { get => base.Power + 2;}
        internal override int Armor { get => base.Armor + 2;}
        internal override Weapon Weapon { get => base.Weapon; set => base.Weapon = new Sword(); }

        public Knight(int power, int agility, int health, int intellect, string name) :
            base(power, agility, health, intellect, name)
        {
            Weapon.FullDamage = Power;
        }
    }
    class Thief : Hero
    {
        internal override int Agility { get => base.Agility + 2;}
        internal override Weapon Weapon { get => base.Weapon; set => base.Weapon = new Dagger(); }
        public Thief(int power, int agility, int health, int intellect, string name) :
            base(power, agility, health, intellect, name)
        {           
            Weapon.FullDamage = Agility;
        }
    }
    class Mage : Hero
    {
        internal override int Intellect { get => base.Intellect + 5;}
        internal override int Mana { get => base.Mana + 25;}
        internal override int MageArmor { get => base.MageArmor + 2;}
        internal override Weapon Weapon { get => base.Weapon; set => base.Weapon = new Staff(); }
        public Mage(int power, int agility, int health, int intellect, string name) :
            base(power, agility, health, intellect, name)
        {
            Weapon.FullDamage = Intellect;
        }
    }
}
