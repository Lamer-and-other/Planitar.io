using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Planetario
{
    public class Map
    {
        Random        Randomer;
        Rectangle     MapRectangle;
        CurrentPlayer CurrentPlayer;
        List<Player>  Players;
        List<Food>    Foods;
        List<Trap>    Traps;
        int           LastId = 1;
        int           ScaleBy = 0;

        Point StartMapCoordinates;

        Font font = new Font("Comic Sans MS", 14, FontStyle.Bold, GraphicsUnit.Point);

        StringFormat stringFormat;

        public Map()
        {
            // Метод для расчёта максимального объёма пикселей для перемещения игрока
            SetScaleBy();

            Players =   new List<Player>();
            Foods =     new List<Food>();
            Traps =     new List<Trap>();

            Randomer = new Random();

            MapRectangle = new Rectangle(MainForm.globalCenter.X - GameConst.MapSizeX / 2, MainForm.globalCenter.Y - GameConst.MapSizeY / 2, GameConst.MapSizeX, GameConst.MapSizeY);

            StartMapCoordinates = new Point(MapRectangle.X, MapRectangle.Y);

            CurrentPlayer = new CurrentPlayer((new Player(0, "Player01", 10, 10)), Color.FromArgb(0, 255, 0));

            AddFood(MapRectangle);
            AddTrap(MapRectangle);

            stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;

           

        }
        public void CenterPlayer(Point newCenter)
        {
            // Размещение игрока онсотельно нового центра

            // Предыдущая позиция мыши
            Point prevCenter = MainForm.globalCenter;

            // Новая позиция мыши
            MainForm.globalCenter = newCenter;

            // Меняем координаты карты
            MapRectangle.X += (MainForm.globalCenter.X - prevCenter.X);
            MapRectangle.Y += (MainForm.globalCenter.Y - prevCenter.Y);

            // Меняем координаты всех фигур
            ChangeCoordinates((MainForm.globalCenter.X - prevCenter.X), true);
            ChangeCoordinates((MainForm.globalCenter.Y - prevCenter.Y), false,true);

            // Ставим игрока в центр
            CurrentPlayer.ThisPlayer.Сollision.X = MainForm.globalCenter.X - CurrentPlayer.ThisPlayer.Сollision.Width / 2;
            CurrentPlayer.ThisPlayer.Сollision.Y = MainForm.globalCenter.Y - CurrentPlayer.ThisPlayer.Сollision.Height / 2;
        }
        public void SetScaleBy()
        {
            // Выбираем кратчайшую границу для рассчёта скорости и масштаба
            ScaleBy = Math.Min(MainForm.globalCenter.X * 2, MainForm.globalCenter.Y * 2);
        }
        public void MoveThisPlayer()
        {
            // Метод для перемещения игрока, мне проще лично тебе объяснить это...
            try
            {
                SetScaleBy();

                Point minus = new Point(IfPlusmax(MainForm.globalCenter.X - MainForm.myMouse.X), IfPlusmax(MainForm.globalCenter.Y - MainForm.myMouse.Y));

                int max = (int)(ScaleBy / GameConst.PlayerMaxSpeed);

                if (((MapRectangle.X + minus.X) <= CurrentPlayer.ThisPlayer.Сollision.X && ((MapRectangle.X + MapRectangle.Width) + minus.X) >= (CurrentPlayer.ThisPlayer.Сollision.X + CurrentPlayer.ThisPlayer.Сollision.Width)))
                {
                    // замедление от ловушки
                    minus.X = InDeceleration(minus.X);
                    // Тут переносим все
                    MapRectangle.X += minus.X;
                    ChangeCoordinates(minus.X, true);
                }
                else
                {
                    bool t1 = false, t2 = false;

                    while (!t1 || !t2)
                    {
                        if (MapRectangle.X >= CurrentPlayer.ThisPlayer.Сollision.X && !t1)
                        {
                            // Тут переносим все
                            MapRectangle.X -= 1;
                            ChangeCoordinates(-1, true);
                        }
                        else t1 = true;

                        if (MapRectangle.X + MapRectangle.Width <= CurrentPlayer.ThisPlayer.Сollision.X + CurrentPlayer.ThisPlayer.Сollision.Width && !t2)
                        {
                            MapRectangle.X += 1;
                            ChangeCoordinates(1, true);
                        }
                        else t2 = true;
                    }

                    while (minus.X != 0 || minus.X != max || minus.X != -max)
                    {
                        if (((MapRectangle.X + minus.X) <= CurrentPlayer.ThisPlayer.Сollision.X && ((MapRectangle.X + MapRectangle.Width) + minus.X) >= (CurrentPlayer.ThisPlayer.Сollision.X + CurrentPlayer.ThisPlayer.Сollision.Width)))
                        {
                            minus.X = InDeceleration(minus.X);
                            // Тут переносим все
                            MapRectangle.X += minus.X;
                            ChangeCoordinates(minus.X, true);
                            break;
                        }
                        else
                        {
                            if (minus.X > 0) minus.X--;
                            else if (minus.X < 0) minus.X++;
                        }

                    }
                }

                if (((MapRectangle.Y + minus.Y) <= CurrentPlayer.ThisPlayer.Сollision.Y && (MapRectangle.Y + MapRectangle.Height + minus.Y) >= (CurrentPlayer.ThisPlayer.Сollision.Y + CurrentPlayer.ThisPlayer.Сollision.Height)))
                {
                    minus.Y = InDeceleration(minus.Y);
                    // Тут переносим все
                    MapRectangle.Y += minus.Y;
                    ChangeCoordinates(minus.Y, false, true);
                }
                else
                {
                    bool t1 = false, t2 = false;

                    while (!t1 || !t2)
                    {
                        if (MapRectangle.Y >= CurrentPlayer.ThisPlayer.Сollision.Y && !t1)
                        {
                            // Тут переносим все
                            MapRectangle.Y -= 1;
                            ChangeCoordinates(-1, false, true);
                        }
                        else t1 = true;

                        if (MapRectangle.Y + MapRectangle.Height <= CurrentPlayer.ThisPlayer.Сollision.Y + CurrentPlayer.ThisPlayer.Сollision.Height && !t2)
                        {
                            // Тут переносим все
                            MapRectangle.Y += 1;
                            ChangeCoordinates(1, false, true);
                        }
                        else t2 = true;
                    }

                    while (minus.Y != 0 || minus.Y != max || minus.Y != -max)
                    {
                        if (((MapRectangle.Y + minus.Y) <= CurrentPlayer.ThisPlayer.Сollision.Y && (MapRectangle.Y + MapRectangle.Height + minus.Y) >= (CurrentPlayer.ThisPlayer.Сollision.Y + CurrentPlayer.ThisPlayer.Сollision.Height)))
                        {
                            minus.Y = InDeceleration(minus.Y);
                            // Тут переносим все
                            MapRectangle.Y += minus.Y;
                            ChangeCoordinates(minus.Y,false,true);

                            break;
                        }
                        else
                        {
                            if (minus.Y > 0) minus.Y--;
                            else if (minus.Y < 0) minus.Y++;
                        }
                    }
                }

                Eat();
                InTrap();
            }
            catch (Exception e){
                MessageBox.Show(e.Message);
            }
           
        }
        public Point GetCurrentPlayerPosition()
        {
            return new Point(StartMapCoordinates.X - MapRectangle.X, StartMapCoordinates.Y - MapRectangle.Y);
        }
        public void ChangeCoordinates(int plus, bool _X = false, bool _Y = false )
        {
            // В этом методе перемещаем все фигуры
            foreach (Player i in Players)
            {
                if (_X) i.Сollision.X += plus;
                if (_Y) i.Сollision.Y += plus;
            }
            foreach (Food i in Foods)
            {
                if (_X) i.Сollision.X += plus;
                if (_Y) i.Сollision.Y += plus;
            }
            foreach (Trap i in Traps)
            {
                if (_X) i.Collise.X += plus;
                if (_Y) i.Collise.Y += plus;
            }
           
        }
        public int IfPlusmax(int x)
        {
            // Возвращаем количество пикселей для добавления (скорость) (переделываем сейчас)
            return (int)(x / (ScaleBy / GameConst.PlayerMaxSpeed)); ;
        }
        void AddFood(Rectangle MapRectangle)
        {
            // Метод добавления еды
            for (int i = 0; i < GameConst.FoodMaxOnMap; i++)
            {
                Food f = new Food(Randomer, MapRectangle);
                Foods.Add(f);
            }
        }

        public void AddPlayer(Player player)
        {
            // Этот метод ещё стоит обсудить
            Players.Add(player);
        }

        public void RemovePlayer(Player player)
        {
            // Этот /*метод*/ ещё стоит обсудить
            Players.Remove(player);
        }
        public void Eat()
        {
            // Метод сравнения какую пищу мы съели
            foreach (Food food in Foods)
            {
                Point center = new Point(food.Сollision.X + food.Сollision.Width / 2, food.Сollision.Y + food.Сollision.Height / 2);
                if (food.Try_Eat(CurrentPlayer.ThisPlayer, center)) // еда возвращает очки и меняет своё местоположение если мы попадаем в условие
                {
                    CurrentPlayer.ThisPlayer.ChangeSize((int)food.Bonus);
                    //ReSizer();
                    food.Destruction(Randomer, MapRectangle);

                    break;
                }
            }
        }

        /// <summary>
        /// попадание в ловушку шипы
        /// </summary>
        public void InTrap()
        {
            foreach(Trap trap in Traps)
            {
                if(trap is Thorns)
                {
                    Thorns thorns_tmp = trap as Thorns;
                    Point center = new Point(thorns_tmp.Collise.X + thorns_tmp.Collise.Width / 2, thorns_tmp.Collise.Y + thorns_tmp.Collise.Height / 2);
                    if(thorns_tmp.InThorns(CurrentPlayer.ThisPlayer, center))
                    {
                        CurrentPlayer.Death();
                        break;
                    }
                    else if(thorns_tmp.UnderThorns(CurrentPlayer.ThisPlayer, center))   // игрок прячется в ловушке
                    {
                        CurrentPlayer.Color = Color.FromArgb(100, 0, 255, 0);
                        break;
                    }
                    else CurrentPlayer.Color = Color.FromArgb(0, 255, 0);
                }
            }
        }
        /// <summary>
        /// попадание в зону замедления
        /// </summary>
        public int InDeceleration(int speed)
        {
            foreach(Trap trap in Traps)
            {
                if (trap is Deceleration)
                {
                    Deceleration dec_tmp = trap as Deceleration;
                    if (dec_tmp.Try_Action(CurrentPlayer))
                    {
                        speed = dec_tmp.Action(CurrentPlayer, speed);
                        break;
                    }
                }
            }
            return speed;
        }

        void AddTrap(Rectangle MapRectangle)
        {
            // добавления ловушек замедления
            for (int i = 0; i < GameConst.TrapMaxOnMap - 10; i++)
            {
                bool flag = true;
                Deceleration t = new Deceleration(Randomer, MapRectangle);

                while (flag)
                {
                    flag = false;

                    foreach (Trap trap in Traps)
                    {
                        Deceleration dec = trap as Deceleration;
                        if (t.Rect.IntersectsWith(dec.Rect))
                        {
                            t.ReRandom(Randomer, MapRectangle);
                            flag = true;
                        }
                    }
                }

                Traps.Add(t);
            }

            // добавление "шипов"
            for (int i = 0; i < GameConst.TrapMaxOnMap - 5; i++)
            {
                bool flag = true;
                Thorns t = new Thorns(Randomer, MapRectangle);

                while (flag)
                {
                    flag = false;

                    foreach (Trap trap in Traps)
                    {
                        Thorns th = trap as Thorns;
                        Rectangle thorns = t.Rect;
                        Rectangle traps_tmp = trap.Rect;
                        if (thorns.IntersectsWith(traps_tmp))
                        {
                            t.ReRandom(Randomer, MapRectangle);
                            flag = true;
                        }
                    }
                }

                Traps.Add(t);
            }
        }
        public void DrawIt()
        {
            // Метод для отрисвоки на форме

            MainForm.panelBuffer.Graphics.Clear(Color.White);

            MainForm.panelBuffer.Graphics.FillRectangle(Brushes.WhiteSmoke, MapRectangle);

            foreach (Player i in Players)       MainForm.panelBuffer.Graphics.FillEllipse(new SolidBrush(Color.DarkGreen), i.Сollision);
            foreach (Food i in Foods)           MainForm.panelBuffer.Graphics.FillEllipse(new SolidBrush(i.color),i.Сollision);
            foreach (Trap i in Traps)           MainForm.panelBuffer.Graphics.FillEllipse(new SolidBrush(i.Color), i.Collise);

            MainForm.panelBuffer.Graphics.FillEllipse(new SolidBrush(CurrentPlayer.Color), CurrentPlayer.ThisPlayer.Сollision);

            MainForm.panelBuffer.Graphics.DrawString(CurrentPlayer.ThisPlayer.Score.ToString(), font, Brushes.Green, CurrentPlayer.ThisPlayer.Сollision, stringFormat);

            MainForm.panelBuffer.Render();
        }
        public void ReSizer()
        {
            // Этот метод для масштабирования всех элементов относительно размера игрока, ещё в разработке
            float cof = ((Math.Min(MainForm.globalCenter.X * 2, MainForm.globalCenter.Y * 2) - 200) / (CurrentPlayer.ThisPlayer.Сollision.Width));
            MapRectangle = GetIt(cof,MapRectangle);
            CurrentPlayer.ThisPlayer.Сollision = GetIt(cof, CurrentPlayer.ThisPlayer.Сollision);

            foreach (Player i in Players)
            {
                i.Сollision = GetIt(cof, i.Сollision);
            }
            foreach (Food i in Foods)
            {
                i.Сollision = GetIt(cof, i.Сollision);
            }
            foreach (Trap i in Traps)
            {
                i.Collise = GetIt(cof, i.Collise);
            }
        }
        public Rectangle GetIt(float cof,Rectangle rec)
        {
            // Этот метод для масштабирования всех элементов относительно размера игрока, ещё в разработке

            int newW, newH, newX, newY;

            newW = (int)(rec.Width * cof);
            newH = (int)(rec.Height * cof);
            newX = rec.X - (int)((newW ) / 2) + MainForm.globalCenter.X;
            newY = rec.Y - (int)((newH ) / 2) + MainForm.globalCenter.Y;

            rec.Width = newW;
            rec.Height = newH;
            rec.X = newX;
            rec.Y = newY;

            return rec;
        }

    }
}
