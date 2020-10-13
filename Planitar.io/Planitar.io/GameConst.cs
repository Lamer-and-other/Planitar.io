using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planitar.io
{
    class GameConst
    {
        public static float PlayerMaxSpeed = 10f;     // Максимальная скорость игрока
        public static float PlayerMinSpeed = 1.0f;     // Минимальная скорость игрока
        public static float PlayerMinMapScale = 1.0f;  // Минимальный масштаб игрока
        public static ulong PlayerMinScore = 10;       // Начальное количество очков у игрока 

        public static int FoodMaxBonus = 2;    // Максимальный бонус за еду
        public static int FoodMinBonus = 0;    // Минимальный бонус за еду
        public static int FoodMaxOnMap = 200;   // Максимальное количество еды на карте

        public static int TrapMaxSize = 3;     // Максимальный размер ловушки на карте
        public static int TrapMinSize = 1;     // Минимальный размер ловушки на карте
        public static int TrapMaxOnMap = 15;   // Максимальное количество ловушек на карте

        public static int MapSizeX = 3000;      // Размер карты по X
        public static int MapSizeY = 3000;      // Размер карты по Y

    }
}
