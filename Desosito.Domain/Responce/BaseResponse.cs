using Desosito.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desosito.Domain.Responce
{
    public class BaseResponse<T> : IBaseResponse<T>
    {
        // Свойство ловит название ошибки
        public string Description { get; set; }

        // Свойство ловит код ошибки
        public StatusCode StatusCode { get; set; }

        // Свойство - результат запроса
        public T Data { get; set; }
    }

    public interface IBaseResponse<T>
    {
        string Description { get; }

        T Data { get; }

        StatusCode StatusCode { get; }
    }
}
