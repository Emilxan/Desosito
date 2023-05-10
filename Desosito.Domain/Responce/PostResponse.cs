using Desosito.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desosito.Domain.Responce
{
    public class PostResponse<T> : IPostResponse<T>
    {
        // Свойство ловит название ошибки
        public string Description { get; set; }

        // Свойство ловит код ошибки
        public StatusCode StatusCode { get; set; }

        // Свойство - результат запроса
        public T Post { get; set; }

        public T User { get; set; }
    }

    public interface IPostResponse<T>
    {
        string Description { get; }

        T Post { get; }
        public T User { get; set; }

        StatusCode StatusCode { get; }
    }
}
