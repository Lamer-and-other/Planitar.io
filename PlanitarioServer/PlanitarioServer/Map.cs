using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 

namespace PlanitarioServer
{
    class Map
    {
        static Random Randomer = new Random();
        static Rectangle MapRectangle;
        
        Size MapSize; 
        public static List<Player> Players = new List<Player>();
        public static List<Food> Foods = new List<Food>();
        public static List<Trap> Traps = new List<Trap>();
        // static потому что карта у нас будет одна и можно пока так оставить 
        public static Publisher globalPublisher = new Publisher();
        
        public static void Create()
        {
            MapRectangle = new Rectangle(0, 0, GameConst.MapSizeX, GameConst.MapSizeY);
            AddFood(); 
            AddTrap(); 
        }
        
        public static void AddPlayer(Player player)
        {
            player.Score = 10;
            player.Record = 10;
         
            
            Players.Add(player);
            player.Subscribe(globalPublisher);
        }
        public static void RemovePlayer(Player player)
        {
            Players.Remove(player);
        }
        
       // создаём абсолютно новую еду 
       static  void AddFood()
        {
            // Метод добавления еды
            for (int i = 0; i < GameConst.FoodMaxOnMap; i++)
            {
                Food f = new Food(Randomer, MapRectangle);
                Foods.Add(f);
            }
        }
        // создаём абсолютно вовую ловушка  
        static void AddTrap()
        {
            // Метод добавления ловушек
            for (int i = 0; i < GameConst.TrapMaxOnMap; i++)
            {
                bool flag = true;
                Deceleration t = new Deceleration(Randomer, MapRectangle);

                while (flag)
                {
                    flag = false;

                    foreach (Deceleration dec in Traps) 
                    {
                        if (t.Rect.IntersectsWith(dec.Rect)) 
                        {
                            t.ReRandom(Randomer, MapRectangle);
                            flag = true;
                        }
                    }
                }

                Traps.Add(t);
            }
        }

        public static Food Eat(Player player)
        {
            // Метод сравнения какую пищу мы съели
            foreach (Food food in Foods)
            {
                Point center = new Point(food.Сollision.X + food.Сollision.Width / 2, food.Сollision.Y + food.Сollision.Height / 2);
                if (food.Try_Eat(player, center)) // еда возвращает очки и меняет своё местоположение если мы попадаем в условие
                {
                    player.ChangeSize((int)food.Bonus);
                    food.Destruction(Randomer, MapRectangle);
                    return food;
                }
            }
            return null; 
        }































        /*
        // сортируем часть массива относительно опоры  
        public static int SortPart(ref int[] mass, int min, int max)
        {
            int pivot = min - 1;
            for (int i = min; i < max; i++)
            {
                if (mass[i] > mass[max])
                {
                    pivot++;
                    int temp = mass[pivot];
                    mass[pivot] = mass[i];
                    mass[i] = temp;
                }
            }
            pivot++;
            int temp2 = mass[pivot];
            mass[pivot] = mass[max];
            mass[max] = temp2;
            return pivot;
        }
        // быстрая сортировка 
        public static void QuickSort(ref int[] mass, int min, int max)
        {
            // проверка не пора ли заканчивать 
            if (min >= max)
            {
                return;
            }
            // узнаём опору             
            int pivot = SortPart(ref mass, min, max);
            // рекурсивно разделяем эту часть на две и сортируем их отдельно относительно опоры 
            QuickSort(ref mass, min, pivot - 1);
            QuickSort(ref mass, pivot + 1, max);
        }
        */
    }

}
