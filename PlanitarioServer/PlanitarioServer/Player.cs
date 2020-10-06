using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Windows.Shapes;

namespace PlanitarioServer 
{
    // я изменил твой класс потому что текуего игрока мы будем определять по его id который мы отправим 
    // я не вижсмысла создавать для этого отдельный класс   
    class Player
    {
        public static List<Player> playerList = new List<Player>(); 
        
        uint Id;             // Ид игрока
        string Nickname;       // Ник игрока
        ulong Score;          // Счёт игрока
        ulong Record;         // Рекорд игрока

        float MapScale;       // Текущий масштаб карты
        float Speed;          // Текущая скорость игрока

        bool BonusSpeed;       // Включено ли ускорение у игрока

        uint LostScoresOnStep; // Количество очков которые мы теряем, например каждые 5 

        Point Position;     // Позиция игрока
        Color Color;        // Цвет игрока
        
        public MyService service = new MyService();
        //Ellipse Collise;

        public Player(string Nickname)
        {
            this.Nickname = Nickname;
        }

    }

}


