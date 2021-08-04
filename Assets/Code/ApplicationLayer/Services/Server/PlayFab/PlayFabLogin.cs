using Code.Domain.Services.Server;
using System.Threading.Tasks;
using PlayFab;
using PlayFab.ClientModels;
using System;

//NOTA: Daniel maneja esta parte de la capa de Application como la capa más externa.
namespace Code.ApplicationLayer.Services.Server.PlayFab
{
    public abstract class PlayFabLogin : IServiceAuthenticator
    {
        public string UserId { get; private set; }

        public Task Authenticate()                                                              //Al devolver un task lo que hacemos es que nuestro consumidor no tiene que preocuparse por callbacks.
        {
            var t = new TaskCompletionSource<bool>();                                           //Crearemos una tarea que almacenará informarción(source) y que cuando se ejecute esperará hasta qie obtenga el resultado y lo guardará.

            Login( t );

            return Task.Run( () => t.Task );                                                    //Nuestra tarea espera hasta que acabe y no se sale hasta que playfab nos regresa toda la información(source) y ejecuta el callback correspondiente.
        }

        protected abstract void Login( TaskCompletionSource<bool> taskCompletionSource );

        protected void OnSuccess( LoginResult result, TaskCompletionSource<bool> taskCompletionSource )
        {
            UserId = result.PlayFabId;
            taskCompletionSource.SetResult( true );
            UnityEngine.Debug.Log( "Todo fine" );
        }

        protected void OnError( PlayFabError error, TaskCompletionSource<bool> t )
        {
            t.SetResult( false );                                                               //No hago SetCanceled para que no se detenda mejor le diré que la cosa ha ido mal con SetResult(false).
            throw new Exception( error.ErrorMessage );                                          //Hay que manejar estos errores porque puede que lo único que le suceda al usuario es que perdio el internet y sólo tengamos que reintentarlo
        }
    }
}