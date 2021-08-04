using System.Threading.Tasks;
using PlayFab;
using PlayFab.ClientModels;

namespace Code.ApplicationLayer.Services.Server.PlayFab
{
    public class PlayFabLoginEditor : PlayFabLogin
    {
        protected override void Login( TaskCompletionSource<bool> taskCompletionSource )
        {
            var request = new LoginWithCustomIDRequest                                                              //Es la manera por default que accederemos por el editor
            {
                CreateAccount = true,
                CustomId = "1"
            };

            PlayFabClientAPI.LoginWithCustomID( request,                                                            //Le pasamos la informacción para acceder y por callback nos regresará si fue exitosa o no
                                                result => OnSuccess( result, taskCompletionSource ),                //Como no queríamos ensuciar el código y evitar que nuestro consumidor tenga que preocuparse por callbacks
                                                error => OnError( error, taskCompletionSource )                     //Pasamos el parametro que nos pide el metodo y en otro el callback o tarea que nos regresará lo guardamos para
                                                );                                                                  //saber que nos lanzará y con el task run esperaremos hasta que termine y en tiempo de ejecucción no haga cosas hasta que termine.
        }
    }
}

//Intentamos no trabajar con callbacks en el mismo método porque ensucian bastante el flujo y la lectura del código ya que un callback llama a otro y otro y mñe... 
//Lo que se hace es adaptar las llamadas de playfab para que funcionen de manera async con las task async no paralelas.