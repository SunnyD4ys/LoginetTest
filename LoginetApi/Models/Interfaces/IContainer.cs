using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginetApi.Models.Interfaces
{
    //инверсия зависимостей. верхние уровени приложения  не должны зависеть от нижних, зависят от абстракций
    public interface IContainer
    {
        //источник данных. в данном примере это будет jsonplaceholder,  в иных случаях источником данных может быть, например,  база данных
        IDataSource DataSource
        {
            get;
            set;
        }
        //другие модули нашего сервиса тоже живут тут, в контейнере, ради примера описан лог
        ILogger Logger
        {
            get;
            set;
        }
        
    }
}
