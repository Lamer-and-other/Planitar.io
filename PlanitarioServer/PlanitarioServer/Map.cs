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
        Size         MapSize;
        public static List<Player> Players = new List<Player>(); 
        List<Food>   Foods;
        List<Trap>   Traps;
        // static потому что карта у нас будет одна и можно пока так оставить 
        public static Publisher globalPublisher = new Publisher();  
        
        public static void AddPlayer(Player player)
        {
            Players.Add(player); 
        }
        public static void RemovePlayer(Player player)
        {
            Players.Remove(player);
        }      

        
        // сортируем часть массива относительно опоры  
        public static int SortPart(ref int[] mass, int min, int max)
        {
            int pivot = min - 1; 
            for(int i = min; i < max; i++)
            {
                if(mass[i] > mass[max])
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
            if(min >= max)
            {
                return; 
            }
            // узнаём опору             
            int pivot = SortPart(ref mass, min, max);
            // рекурсивно разделяем эту часть на две и сортируем их отдельно относительно опоры 
            QuickSort(ref mass, min, pivot - 1); 
            QuickSort(ref mass, pivot + 1, max); 
        }
        
    }
}
