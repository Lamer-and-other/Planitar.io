using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms; 

namespace Planitar.io
{
    class Map
    {
        Random Randomer;
        Rectangle MapRectangle;
        //Player CurrentPlayer;
        public static List<Player> Players;
        public List<Food> Foods;
        public List<Trap> Traps;
        int LastId = 1;
        int ScaleBy = 0;

        Point StartMapCoordinates;

        Font font = new Font("Comic Sans MS", 14, FontStyle.Bold, GraphicsUnit.Point);

        StringFormat stringFormat;

        public Player CurrentPlayer = null; 

        public Map()
        {
            // Метод для расчёта максимального объёма пикселей для перемещения игрока
            SetScaleBy();

            Players = new List<Player>();
            Foods = new List<Food>();
            Traps = new List<Trap>();

            Randomer = new Random();
            
            MapRectangle = new Rectangle(Form1.globalCenter.X - GameConst.MapSizeX / 2, 
                Form1.globalCenter.Y - GameConst.MapSizeY / 2, GameConst.MapSizeX, GameConst.MapSizeY);
            
            StartMapCoordinates = new Point(MapRectangle.X, MapRectangle.Y);

            stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;
        }
        
        public void CenterPlayer(Point newCenter)
        {
            // Предыдущая позиция мыши
            Point prevCenter = Form1.globalCenter;

            // Новая позиция мыши
            Form1.globalCenter = newCenter;

            // Меняем координаты карты
            MapRectangle.X += (Form1.globalCenter.X - prevCenter.X);
            MapRectangle.Y += (Form1.globalCenter.Y - prevCenter.Y);

            // Меняем координаты всех фигур
            ChangeCoordinates((Form1.globalCenter.X - prevCenter.X), true);
            ChangeCoordinates((Form1.globalCenter.Y - prevCenter.Y), false, true);

            // Ставим игрока в центр
            CurrentPlayer.Сollision.X = Form1.globalCenter.X - CurrentPlayer.Сollision.Width / 2;
            CurrentPlayer.Сollision.Y = Form1.globalCenter.Y - CurrentPlayer.Сollision.Height / 2;
        }
        public void SetScaleBy()
        {
            // Выбираем кратчайшую границу для рассчёта скорости и масштаба
            ScaleBy = Math.Min(Form1.globalCenter.X * 2, Form1.globalCenter.Y * 2);
        }
        // на клиенте 
        public void MoveThisPlayer()
        {
            // Метод для перемещения игрока, мне проще лично тебе объяснить это... 
            try
            {
                SetScaleBy();
                
                Point minus = new Point(IfPlusmax(Form1.globalCenter.X - Form1.myMouse.X), IfPlusmax(Form1.globalCenter.Y - Form1.myMouse.Y));

                int max = (int)(ScaleBy / GameConst.PlayerMaxSpeed);
                
                if (((MapRectangle.X + minus.X) <= CurrentPlayer.Сollision.X && ((MapRectangle.X + MapRectangle.Width) + minus.X) >= (CurrentPlayer.Сollision.X + CurrentPlayer.Сollision.Width)))
                {
                    // Тут переносим все
                    MapRectangle.X += minus.X;
                    ChangeCoordinates(minus.X, true);
                }
                else
                {
                    bool t1 = false, t2 = false;

                    while (!t1 || !t2)
                    {
                        if (MapRectangle.X >= CurrentPlayer.Сollision.X && !t1)
                        {
                            // Тут переносим все
                            MapRectangle.X -= 1;
                            ChangeCoordinates(-1, true);
                        }
                        else t1 = true;

                        if (MapRectangle.X + MapRectangle.Width <= CurrentPlayer.Сollision.X + CurrentPlayer.Сollision.Width && !t2)
                        {
                            MapRectangle.X += 1;
                            ChangeCoordinates(1, true);
                        }
                        else t2 = true;
                    }

                    while (minus.X != 0 || minus.X != max || minus.X != -max)
                    {
                        if (((MapRectangle.X + minus.X) <= CurrentPlayer.Сollision.X && ((MapRectangle.X + MapRectangle.Width) + minus.X) >= (CurrentPlayer.Сollision.X + CurrentPlayer.Сollision.Width)))
                        {
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

                if (((MapRectangle.Y + minus.Y) <= CurrentPlayer.Сollision.Y && (MapRectangle.Y + MapRectangle.Height + minus.Y) >= (CurrentPlayer.Сollision.Y + CurrentPlayer.Сollision.Height)))
                {
                    // Тут переносим все
                    MapRectangle.Y += minus.Y;
                    ChangeCoordinates(minus.Y, false, true);
                }
                else
                {
                    bool t1 = false, t2 = false;

                    while (!t1 || !t2)
                    {
                        if (MapRectangle.Y >= CurrentPlayer.Сollision.Y && !t1)
                        {
                            // Тут переносим все
                            MapRectangle.Y -= 1;
                            ChangeCoordinates(-1, false, true);
                        }
                        else t1 = true;

                        if (MapRectangle.Y + MapRectangle.Height <= CurrentPlayer.Сollision.Y + CurrentPlayer.Сollision.Height && !t2)
                        {
                            // Тут переносим все
                            MapRectangle.Y += 1;
                            ChangeCoordinates(1, false, true);
                        }
                        else t2 = true;
                    }

                    while (minus.Y != 0 || minus.Y != max || minus.Y != -max)
                    {
                        if (((MapRectangle.Y + minus.Y) <= CurrentPlayer.Сollision.Y && (MapRectangle.Y + MapRectangle.Height + minus.Y) >= (CurrentPlayer.Сollision.Y + CurrentPlayer.Сollision.Height)))
                        {
                            // Тут переносим все
                            MapRectangle.Y += minus.Y;
                            ChangeCoordinates(minus.Y, false, true);

                            break;
                        }
                        else
                        {
                            if (minus.Y > 0) minus.Y--;
                            else if (minus.Y < 0) minus.Y++;
                        }
                    }
                }

               // Eat();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }

        public Point GetCurrentPlayerPosition()
        {
            return new Point(StartMapCoordinates.X - MapRectangle.X, StartMapCoordinates.Y - MapRectangle.Y);
        }
        
        
        public void ChangeCoordinates(int plus, bool _X = false, bool _Y = false)
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
            foreach (Deceleration i in Traps)
            {
                if (_X) i.Сollision.X += plus;
                if (_Y) i.Сollision.Y += plus; 
            }

        }
        
        public int IfPlusmax(int x)
        {
            // Возвращаем количество пикселей для добавления (скорость) (переделываем сейчас)
            return (int)(x / (ScaleBy / GameConst.PlayerMaxSpeed)); ;
        }
        
        public void AddFood(Point location, int bonus)
        {
            Food f = new Food(location, Randomer, bonus);
            Foods.Add(f); 
        }
        
        public void AddTrap(Point location)
        {
            Deceleration t = new Deceleration(location, MapRectangle); 
            Traps.Add(t);
        }
        
        //public void AddPlayer(Player player)
        //{
        //    // Этот метод ещё стоит обсудить 
        //    Players.Add(player); 
        //    player.Subscribe(globalPublisher);
        //}

        //public void RemovePlayer(Player player)
        //{
        //    // Этот метод ещё стоит обсудить 
        //    Players.Remove(player);
        //}
        
        // сервер         
        public void Eat(Player player, Food food) 
        {
            //foreach (Food food in Foods)
            //{
            //    Point center = new Point(food.Сollision.X + food.Сollision.Width / 2, food.Сollision.Y + food.Сollision.Height / 2);
            //    if (food.Try_Eat(CurrentPlayer, center)) // еда возвращает очки и меняет своё местоположение если мы попадаем в условие
            //    {
            //        CurrentPlayer.ChangeSize((int)food.Bonus); 
            //        Form1.thisForm.setBonusLable(food.Bonus.ToString()); 
            //       // food.Destruction(Randomer, MapRectangle); 
            //       // return food; 
            //    }
            //}

            player.ChangeSize((int)food.Bonus);
            food.Destruction(food); 
            // тут будет отправлятся запрос проверки на съеденость кружка 
        }
      
        public void DrawIt()
        {
            // Метод для отрисвоки на форме 
            Form1.panelBuffer.Graphics.Clear(Color.White);
            
            Form1.panelBuffer.Graphics.FillRectangle(Brushes.WhiteSmoke, MapRectangle);
            
            foreach (Player i in Players) 
                Form1.panelBuffer.Graphics.FillEllipse(new SolidBrush(i.Color), i.Сollision);
            foreach (Food i in Foods)
                Form1.panelBuffer.Graphics.FillEllipse(new SolidBrush(i.color), i.Сollision);
            foreach (Deceleration i in Traps)
                Form1.panelBuffer.Graphics.FillEllipse(new SolidBrush(i.color), i.Сollision);

            Form1.panelBuffer.Graphics.FillEllipse(new SolidBrush(CurrentPlayer.Color), CurrentPlayer.Сollision);
            
            Form1.panelBuffer.Graphics.DrawString(CurrentPlayer.Score.ToString(), font, Brushes.Green, CurrentPlayer.Сollision, stringFormat);

            Form1.panelBuffer.Render();
        }
        public void ReSizer()
        {
            // Этот метод для масштабирования всех элементов относительно размера игрока, ещё в разработке
            float cof = ((Math.Min(Form1.globalCenter.X * 2, Form1.globalCenter.Y * 2) - 200) / (CurrentPlayer.Сollision.Width));
            MapRectangle = GetIt(cof, MapRectangle);
            CurrentPlayer.Сollision = GetIt(cof, CurrentPlayer.Сollision);
            
            foreach (Player i in Players)
            {
                i.Сollision = GetIt(cof, i.Сollision);
            }
            foreach (Food i in Foods)
            {
                i.Сollision = GetIt(cof, i.Сollision); 
            }
            foreach (Deceleration i in Traps)
            {
                i.Сollision = GetIt(cof, i.Сollision);
            }
        }
        
        public Rectangle GetIt(float cof, Rectangle rec)
        {
            // Этот метод для масштабирования всех элементов относительно размера игрока, ещё в разработке
            int newW, newH, newX, newY;
            
            newW = (int)(rec.Width * cof);
            newH = (int)(rec.Height * cof);
            newX = rec.X - (int)((newW) / 2) + Form1.globalCenter.X;
            newY = rec.Y - (int)((newH) / 2) + Form1.globalCenter.Y;

            rec.Width = newW;
            rec.Height = newH;
            rec.X = newX;
            rec.Y = newY;

            return rec;
        }

        public Player getPlayer(int id)
        {
            foreach (Player player in Map.Players)
            {
                if (player.id == id)
                {
                    return player;
                }
            }
            return null;
        }
        
    }
}
