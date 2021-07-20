using Domain.Services.Server;
using System.Threading.Tasks;
using PlayFab;
using PlayFab.ClientModels;
using System;

namespace ApplicationLayer.Services.Server
{
    public abstract class PlayFabLogin : IServiceAuthenticator
    {
        public string UserID
        {
            get; private set;
        }

        public Task Authenticate()                                                              //Al devolver un task lo que hacemos es que nuestro consumidor no tiene que preocuparse por callbacks.
        {
            var t = new TaskCompletionSource<bool>();                                           //Crearemos una tarea que almacenar� informarci�n(source) y que cuando se ejecute esperar� hasta qie obtenga el resultado y lo guardar�.

            Login( t );

            return Task.Run( () => t.Task );                                                    //Nuestra tarea espera hasta que acabe y no se sale hasta que playfab nos regresa toda la informaci�n(source) y ejecuta el callback correspondiente.
        }

        protected abstract void Login( TaskCompletionSource<bool> taskCompletionSource );

        protected void OnSuccess( LoginResult result, TaskCompletionSource<bool> taskCompletionSource )
        {
            UserID = result.PlayFabId;
            taskCompletionSource.SetResult( true );
            UnityEngine.Debug.Log( "Todo fine" );
        }

        protected void OnError( PlayFabError error, TaskCompletionSource<bool> t )
        {
            t.SetResult( false );                                                               //No hago SetCanceled para que no se detenda mejor le dir� que la cosa ha ido mal con SetResult(false).
            throw new Exception( error.ErrorMessage );                                          //Hay que manejar estos errores porque puede que lo �nico que le suceda al usuario es que perdio el internet y s�lo tengamos que reintentarlo
        }
    }
}