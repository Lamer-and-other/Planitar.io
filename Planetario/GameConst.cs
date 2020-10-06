using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planetario
{
    static class GameConst
    {
        
        static float PlayerMaxSpeed = 3.0f;     // Максимальная скорость игрока
        static float PlayerMinSpeed = 1.0f;     // Минимальная скорость игрока
        static float PlayerMinMapScale = 1.0f;  // Минимальный масштаб игрока
        static ulong PlayerMinScore = 10;       // Начальное количество очков у игрока 

        static int FoodMaxBonus = 2;    // Максимальный бонус за еду
        static int FoodMinBonus = 1;    // Минимальный бонус за еду
        static int FoodMaxOnMap = 15;   // Максимальное количество еды на карте

        static int TrapMaxSize = 3;     // Максимальный размер ловушки на карте
        static int TrapMinSize = 1;     // Минимальный размер ловушки на карте
        static int TrapMaxOnMap = 15;   // Максимальное количество ловушек на карте
    }
}