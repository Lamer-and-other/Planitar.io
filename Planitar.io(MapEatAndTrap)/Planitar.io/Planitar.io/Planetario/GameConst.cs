using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planetario
{
    static class GameConst
    {
        static public int MapWidth = 900;
        static public int MapHeight = 500;

        static public float PlayerMaxSpeed = 3.0f;     // Максимальная скорость игрока
        static public float PlayerMinSpeed = 1.0f;     // Минимальная скорость игрока
        static public float PlayerMinMapScale = 1.0f;  // Минимальный масштаб игрока
        static public ulong PlayerMinScore = 10;       // Начальное количество очков у игрока 

        static public int FoodMaxBonus = 2;    // Максимальный бонус за еду
        static public int FoodMinBonus = 0;    // Минимальный бонус за еду
        static public int FoodMaxOnMap = 150;   // Максимальное количество еды на карте

        static public int TrapMaxSize = 3;     // Максимальный размер ловушки на карте
        static public int TrapMinSize = 1;     // Минимальный размер ловушки на карте
        static public int TrapMaxOnMap = 15;   // Максимальное количество ловушек на карте
    }
}