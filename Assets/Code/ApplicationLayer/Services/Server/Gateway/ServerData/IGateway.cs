using Code.ApplicationLayer.Services.Server.DTOs;
using System.Threading.Tasks;

namespace Code.ApplicationLayer.Services.Server.Gateways
{                                                                                               //Es el patr�n para acceder a los datos.
    public interface IGateway                                                                   //Es el que se va a conectar a la API deplayfab para obtener los datos de usuario y los datos de configuraci�n del servidor
    {
        Task InitializeData();                                                                  //Obtiene los datos y los guarda en cache.
        Task<T> Get<T>() where T : IDTO;                                                        //Pregunta por datos concretos.
        Task<bool> Contains<T>() where T : IDTO;                                                //Preguntamos si existe esa clase o Dto
        void Set<T>( T data ) where T : IDTO;                                                   //Actualizamos los datos
        Task Save();                                                                            //Guardamos los datos que hemos hecho SET. No es optimo que el mismo Set guarde porque haremos varias peticiones. (Los guardas en los dirty).
    }
}