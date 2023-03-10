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
        }
    }
}

