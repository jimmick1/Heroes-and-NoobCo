using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace Heroes_and_NoobCo
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                string? hero = Console.ReadLine();
                if (hero != "hero")
                    throw new Exception($"You must enter \"hero\" instead of {hero}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return;
            }
            List<Hero> heroes = new List<Hero>();
            while (true)
            {
                string? hero = Console.ReadLine();
                if (hero == "enemy")
                {
                    break;
                }
                else
                {
                    string [] heroesenter = hero.Split(' ', 6).ToArray();
                    try
                    {
                        switch (heroesenter[0])
                        {
                            case "knight":
                                heroes.Add(new Knight(Convert.ToInt32(heroesenter[1]), Convert.ToInt32(heroesenter[2]),
                                    Convert.ToInt32(heroesenter[3]), Convert.ToInt32(heroesenter[4]), heroesenter[5]));
                                break;
                            case "thief":
                                heroes.Add(new Thief(Convert.ToInt32(heroesenter[1]), Convert.ToInt32(heroesenter[2]),
                                    Convert.ToInt32(heroesenter[3]), Convert.ToInt32(heroesenter[4]), heroesenter[5]));
                                break;
                            case "mage":
                                heroes.Add(new Mage(Convert.ToInt32(heroesenter[1]), Convert.ToInt32(heroesenter[2]),
                                    Convert.ToInt32(heroesenter[3]), Convert.ToInt32(heroesenter[4]), heroesenter[5]));
                                break;
                            default: throw new Exception("Please check your enter");
                        }
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        return;
                    }
                }                
            }
            List<Hero> enemies = new List<Hero>();
            while (true)
            {
                string? hero = Console.ReadLine();
                if (hero == "end")
                {
                    break;
                }
                else
                {
                    string[] heroesenter = hero.Split(' ', 6).ToArray();
                    try
                    {
                        switch (heroesenter[0])
                        {
                            case "knight":
                                enemies.Add(new Knight(Convert.ToInt32(heroesenter[1]), Convert.ToInt32(heroesenter[2]),
                                    Convert.ToInt32(heroesenter[3]), Convert.ToInt32(heroesenter[4]), heroesenter[5]));
                                break;
                            case "thief":
                                enemies.Add(new Thief(Convert.ToInt32(heroesenter[1]), Convert.ToInt32(heroesenter[2]),
                                    Convert.ToInt32(heroesenter[3]), Convert.ToInt32(heroesenter[4]), heroesenter[5]));
                                break;
                            case "mage":
                                enemies.Add(new Mage(Convert.ToInt32(heroesenter[1]), Convert.ToInt32(heroesenter[2]),
                                    Convert.ToInt32(heroesenter[3]), Convert.ToInt32(heroesenter[4]), heroesenter[5]));
                                break;
                            default: throw new Exception("Please check your enter");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        return;
                    }
                }

            }
            PrintFight(heroes, enemies);
        }        
        private static string ReturnType (Hero hero)
        {
            try
            {
                switch (hero)
                {
                    case Knight: return "Knight";
                    case Thief: return "Thief";
                    case Mage: return "Mage";
                    default: throw new Exception ("Unknown hero");
                }
            }
            catch (Exception e) 
            {
                return e.Message;
            }
        }
        private static int ClearDamage(Hero atacker, Hero defender)
        {
            int damage = atacker.Weapon.FullDamage - (defender.Armor + defender.Agility);
            if (damage >= 0)
            {
                return damage;
            }
            else
            {
                return 0;
            }
        }
        private static int ClearMagicDamage(Hero mageatacker, Hero defender)
        {
            int damage = mageatacker.Cast.FullDamage - (defender.MageArmor + defender.Intellect);
            if (damage >= 0)
            {
                return damage;
            }
            else
            {
                return 0;
            }
        }
        private static void MagicCast (Hero atacker, List<Hero> defenders)
        {
            foreach (Hero defender in defenders)
            {
                defender.currenthealth -= ClearMagicDamage(atacker, defender);
                if (defender.currenthealth < 0)
                {
                    defender.currenthealth = 0;
                }
                Console.WriteLine("{0} {1} attacking {2} {3} with {4}.", ReturnType(atacker), atacker.Name, ReturnType(defender),
                defender.Name, atacker.Cast.name);
                Console.WriteLine("{0} {1} get hit for {2} hp and have {3} hp left!", ReturnType(defender), defender.Name,
                ClearMagicDamage(atacker, defender), defender.currenthealth);
                if (defender.currenthealth == 0)
                {
                    Console.WriteLine("{0} {1} is defeated!", ReturnType(defender), defender.Name);
                }
            }
            atacker.currentmana -= 40;
        }
        private static void Shot (Hero atacker, Hero defender, ref bool flag)
        {
            defender.currenthealth -= ClearDamage(atacker, defender);
            if (defender.currenthealth < 0)
            {
                defender.currenthealth = 0;
            }
            Console.WriteLine("{0} {1} attacking {2} {3} with {4}.", ReturnType(atacker), atacker.Name, ReturnType(defender), 
                defender.Name, atacker.Weapon.name);
            Console.WriteLine("{0} {1} get hit for {2} hp and have {3} hp left!", ReturnType(defender), defender.Name,
                ClearDamage(atacker, defender), defender.currenthealth);
            if (defender.currenthealth == 0)
            {
                Console.WriteLine("{0} {1} is defeated!", ReturnType(defender), defender.Name);
                flag = true;
            }
        }
        private static int ReturnTargetIndex (List<Hero> fighters)
        {
            List<int> stats = new List<int>();
            foreach (Hero hero in fighters)
            {
                stats.Add(hero.currenthealth + hero.Armor);
            }
            return stats.IndexOf(stats.Min());
        }
        private static void HeroesKick (ref List<Hero> heroes, ref List<Hero> enemies)
        {
            bool flag = false;
            foreach (Hero hero in heroes)
            {
                if (enemies.Count == 0)
                {
                    break;
                }
                var targetindex = ReturnTargetIndex(enemies);
                if (hero is Mage && hero.currentmana >= 40)
                {
                    MagicCast(hero, enemies);
                    for (int i = 0; i < enemies.Count; i++)
                    {
                        if (enemies[i].currenthealth == 0)
                        {
                            enemies.RemoveAt(i);
                            i--;
                        }
                    }
                }
                else
                {
                    Shot(hero, enemies[targetindex], ref flag);
                    if (flag)
                    {
                        enemies.RemoveAt(targetindex);
                    }                    
                }
            }
        }
        private static void PrintFight (List<Hero> heroes, List<Hero> enemies)
        {
            StringBuilder stringBuilder = new StringBuilder();
            string defeatcase = "";
            stringBuilder.Append("Stay a while and listen, and I will tell you a story. " +
                "A story of Dungeons and Dragons, of Orcs and Goblins, of Ghouls and Ghosts, " +
                "of Kings and Quests, but most importantly -- of Heroes and NoobCo -- " +
                "Well... A story of Heroes.").AppendLine();
            stringBuilder.Append("So here starts the journey of our");
            if (heroes.Count == 1)
            {
                stringBuilder.Append(" hero " + ReturnType(heroes[0]) + " " + heroes[0].Name + " ");
                defeatcase = "Unfortunately our hero was brave, yet not enough skilled, or just lack of luck.";
            }
            else
            {
                stringBuilder.Append(" heroes: "); 
                for (int i = 0; i < heroes.Count - 1; i++)
                {
                    stringBuilder.Append(ReturnType(heroes[i]) + " " + heroes[i].Name + ", ");
                }
                stringBuilder.Append(ReturnType(heroes[heroes.Count - 1]) + " " + heroes[heroes.Count - 1].Name + " ");
                defeatcase = "Unfortunately our heroes were brave, yet not enough skilled, or just lack of luck.";
            }
            stringBuilder.Append("got order to eliminate the ");
            if (enemies.Count == 1)
            {
                stringBuilder.Append("local bandit known as " + ReturnType(enemies[0]) + " " + enemies[0].Name + ".").AppendLine();
            }
            else
            {
                stringBuilder.Append("local gang consists of well known bandits: ");
                for (int i = 0; i < enemies.Count - 1; i++)
                {
                    stringBuilder.Append(ReturnType(enemies[i]) + " " + enemies[i].Name + ", ");
                }
                stringBuilder.Append(ReturnType(enemies[enemies.Count - 1]) + " " + enemies[enemies.Count - 1].Name + ".").AppendLine();
            } 
            if (heroes.Count > 1 || enemies.Count > 1)
            {
                if (heroes.Count == 1)
                {
                    stringBuilder.Append(ReturnType(heroes[0]) + " " + heroes[0].Name);
                }
                else
                {
                    for (int i = 0; i < heroes.Count - 1; i++)
                    {
                        stringBuilder.Append(ReturnType(heroes[i]) + " " + heroes[i].Name + ", ");
                    }
                    stringBuilder.Append(ReturnType(heroes[heroes.Count - 1]) + " " + heroes[heroes.Count - 1].Name + " ");
                }
                stringBuilder.Append("engaged the ");
                if (enemies.Count == 1)
                {
                    stringBuilder.Append(ReturnType(enemies[0]) + " " + enemies[0].Name + ".").AppendLine();
                }
                else
                {
                    for (int i = 0; i < enemies.Count - 1; i++)
                    {
                        stringBuilder.Append(ReturnType(enemies[i]) + " " + enemies[i].Name + ", ");
                    }
                    stringBuilder.Append(ReturnType(enemies[enemies.Count - 1]) + " " + enemies[enemies.Count - 1].Name + ".").AppendLine();
                }
            }
            Console.Write(stringBuilder.ToString());
            while (true)
            {
                HeroesKick(ref heroes, ref enemies);               
                if (enemies.Count == 0)
                {
                    Console.WriteLine("Congratulations!");
                    break;
                }
                HeroesKick(ref enemies, ref heroes);
                if (heroes.Count == 0)
                {
                    Console.WriteLine(defeatcase);
                    break;
                }
            }            
        }
    }
}

