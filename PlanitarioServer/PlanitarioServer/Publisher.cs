using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanitarioServer
{
    class Publisher
    {
        public static Publisher publisher = new Publisher();
        
        // делегат, служит как "форма" для события отправки 
        public delegate void updata(byte[] data); 
        //  событиt отправки 
        event updata updataSenderEvent;
        // функция для добавление методов которые активируются после вызова события  
        public void addEventHandler(updata handler)
        {
            updataSenderEvent += handler;
        }
        // удаление методов из события 
        public void removeEventHandler(updata handler)
        {
            updataSenderEvent -= handler;
        }
        // запуск события 
        public void notify(byte[] message)
        {
            updataSenderEvent(message);
        }
    }
}
