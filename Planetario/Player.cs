using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planetario
{
    class Player
    {
        uint    Id;             // Ид игрока
        string  Nickname;       // Ник игрока
        ulong   Score;          // Счёт игрока
        ulong   Record;         // Рекорд игрока
    }
    class CurrentPlayer
    {
        Player ThisPlayer;    // Текущий игрок

        float MapScale;       // Текущий масштаб карты
        float Speed;          // Текущая скорость игрока

        bool BonusSpeed;       // Включено ли ускорение у игрока

        uint LostScoresOnStep; // Количество очков которые мы теряем, например каждые 5 

        Point Position;     // Позиция игрока
        Color Color;        // Цвет игрока
    }
}

