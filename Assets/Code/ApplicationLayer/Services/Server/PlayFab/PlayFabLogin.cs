using Domain.Services.Server;
using System.Threading.Tasks;
using PlayFab;
using PlayFab.ClientModels;
using System;

namespace ApplicationLayer.Services.Server
{
    public class PlayFabLogin : IAuthenticateService
    {
        private string userID;

        public PlayFabLogin()
        {

        }

        public Task Authenticate()
        {
            var t = new TaskCompletionSource<bool>();                                           //Crearemos una tarea que almacenará informarción y esperará a que termine o obtenga el resultado

            var request = new LoginWithCustomIDRequest                                          //Es la manera por default que accederemos por el editor
                          {
                              CreateAccount = true,
                              CustomId = "1"
                          };

            PlayFabClientAPI.LoginWithCustomID( request,                                        //Le pasamos la informacción para acceder y por callback nos regresará si fue exitosa o no
                                                result => OnSuccess( result, t ),               //Como no queríamos ensuciar el código y evitar que nuestro consumidor tenga que preocuparse por callbacks
                                                error => OnError( error, t )                    //Pasamos el parametro que nos pide el metodo y en otro el callback o tarea que nos regresará lo guardamos para
                                                );                                              //saber que nos lanzará y con el task run esperaremos hasta que termine y en tiempo de ejecucción no haga cosas hasta que termine.

            return Task.Run( () => t.Task );                                                    //Nuestra tarea con source se ejecuta y no se sale hasta que playfab nos regresa toda la información y ejecuta el callback correspondiente.
        }

        private void OnSuccess( LoginResult result, TaskCompletionSource<bool> taskCompletionSource )
        {
            userID = result.PlayFabId;
            taskCompletionSource.SetResult( true );
            UnityEngine.Debug.Log( "Todo fine" );
        }

        private void OnError( PlayFabError error, TaskCompletionSource<bool> t )
        {
            t.SetResult( false );                                                               //No hago SetCanceled para que no se detenda mejor le diré que la cosa ha ido mal con SetResult(false).
            throw new Exception( error.ErrorMessage );                                          //Hay que manejar estos errores porque puede que lo único que le suceda al usuario es que perdio el internet y sólo tengamos que reintentarlo
        }
    }
}


//Intentamos no trabajar con callbacks en el mismo método porque ensucian bastante el flujo y la lectura del código ya que un callback llama a otro y otro y mñe... 
//Lo que se hace es adaptar las llamadas de playfab para que funcionen de manera async con las task async no paralelas.

//TaskCompletionSource es una task que vamos a crear y vamos a ejecutar después donde en el source asignaremos el resultado. SetResult si ha ido bien o SetCanceled si ha ido mal.

//Y devolvemos una task de manera que nuestro consumidor no tiene que preocuparse por callbacks.