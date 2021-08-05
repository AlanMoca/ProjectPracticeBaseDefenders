using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Code.ApplicationLayer.Services.Server.DTOs;
using Code.Domain.Services.Serializer;
using Code.Domain.Services.Server;
using UnityEngine.Assertions;

namespace Code.ApplicationLayer.Services.Server.Gateways
{
    public abstract class Gateway : IGateway, IDataPreLoaderService                             //IDataPreLoader podría meterla dentro de IGateway pero eso significa que todos lo que implementen esto tendría que precargar datos y eso no me interesa.
    {
        private Dictionary<string, string> rawData;                                             //En Playfab trabajamos con datos que son string-string. Tenemos la key-JSON.
        private Dictionary<Type, string> typeToKey;                                             //Supongo convierte el tipo del DTO a string para la key en JSON.
        private Dictionary<Type, IDTO> parsedData;                                              //Una vez parseados se guardarán aquí por su tipo y el DTO en su clase final.
        private readonly Dictionary<string, string> dirtyData;                                  ////Este es para cuando guardemos datos, marcamos lo que están sucios y cuando le digamos guarda, guardemos todos los que están sucios en vez de guardar todo sólo guardamos lo que esté sucio. (Los sets)

        private readonly ISerializerService serializerService;                                  //Parser de JSON
        private readonly IGetDataService getDataService;                                        //Obtener datos. (Servicio de playfab pero lo abstraemos para desacoplar playfab)
        private readonly ISetDataService setDataService;                                        //Set de datos

        private bool isInitialized;                                                             //Nos ayuda a saber si el actual DTO esta inicializado o no.

        protected Gateway( ISerializerService _serializerService, IGetDataService _getDataService, ISetDataService _setDataService )
        {
            serializerService = _serializerService;
            getDataService = _getDataService;
            setDataService = _setDataService;
            dirtyData = new Dictionary<string, string>();
        }

        protected abstract void InitializeTypeToKey( out Dictionary<Type, string> typeToKey );  //Template method -> Esqueleto que los hijos van a override (UserDataGateway y TitleDataGateway). Lo que hacen los hijos es el mappeador de qué tipo de DTO equivale a cada nombre.


        public async Task PreLoad()                                                             //Obtiene los datos y los guarda en cahce.
        {
            InitializeTypeToKey( out typeToKey );                                               //Obtenemos las Keys del hijo que las implementa.

            var optional = await getDataService.Get( new List<string>() );                      //Obtenemos el servicio que implementamos con playfab pidiendole todas las keys(porque si le pasamos una lista vacia nos devuelve todas). Sólo que con el parametro nosotros podemos defini cual en especifico queremos que nos regrese.

            optional.IfPresent( result =>                                                       //El optional puede o puede no estar. Si esta presente se hace algo sino se tira la exception.
                    {
                        rawData = result.Data;                                                  //Esto sería el diccionario que nos regresa playfab. result.data Supongo yo sale como si fuese la información que regreso playfab a la clase DataResult porque lo pide la interface IGetDataService.
                        parsedData = new Dictionary<Type, IDTO>( rawData.Count );               //Inicializamos este otro diccionario con el tamaño que tengan los datos.
                        isInitialized = true;                                                   //Lo marcamos como inicializado para que por ejemplo en el método Get no tengamos que volver a inicializarlo.
                    } )
                    .OrElseThrow( new Exception( "Error initializing gateway data" ) );
        }

        public T Get<T>() where T : IDTO
        {
            Assert.IsTrue( isInitialized, "Gateway is not initialized." );

            var type = typeof( T );                                                             //Almacenamos el tipo de clase de nuestro DTO.

            if( parsedData.TryGetValue( type, out var result ) )                                //Preguntamos si nuestro DTO ya está cacheado en los datos que ya están parseados y si ya lo está, entonces lo devolvemos.
            {
                return (T)result;
            }
                                                                                                //Si no está cacheado entonces:
            var key = typeToKey[type];                                                          //Buscamos por el tipo de nuestro DTO la key que le corresponde en string para luego buscarla en el diccionario. Hay que recordar que la variable typeToKey almaceno todas las keys con su tipo gracias a la clase abstracta que implementan los respectivos hijos.
            var data = rawData[key];                                                            //Este diccionario tendrá todas las keys-data que playfab nos haya regresado por lo que le metemos el DTO(key string) que queremos y nos regresará el JSON correspondiente.

            var dto = serializerService.Deserialize<T>( data );                                 //Obtenemos la información del DTO que andabamos buscando.
            parsedData.Add( type, dto );                                                        //Lo almacenamos en los datos que ya tenemos parseados o bien cacheados.

            return dto;
        }

        public bool Contains<T>() where T : IDTO                                    //Simplemente pregunta si este tipo existe.
        {
            Assert.IsTrue( isInitialized, "Gateway is not initialized." );

            var type = typeof( T );
            var key = typeToKey[type];

            return rawData.ContainsKey( key );
        }

        public void Set<T>( T data ) where T : IDTO                                             //Settea los datos pero en cache
        {
            Assert.IsTrue( isInitialized, "Gateway is not initialized." );

            var type = typeof( T );
            var key = typeToKey[type];

            if( dirtyData.ContainsKey( key ) )                                                  //Verificamos si actualmente este DTO ha sido modificado, actualizado. Si es así entonces:
            {
                throw new Exception( $"This key {type} is already dirty" );                     //Se lanza una exception porque significa que alguien ha escrito los datos pero no los guardo en servidor por lo tanto es un comportamiento que el programdor no hizo bien
            }
                                                                                                //Si no ha sido modificado o bien se guardo antes en servidor, antes de volver a ser modificada entonces:
            var serializedData = serializerService.Serialize( data );                          //El DTO modificado o actualizado se convierte en JSON
            SetRawData( key, serializedData );                                                  //Como se modifico la data de un DTO, guardamos en cache la nueva data cruda modficada.
            SetParsedData( data, type );                                                        //Y hacemos lo mismo con el cache de la data parseada. Pero sólo lo modificamos en ambos diccionario para que la info concuerde.
            dirtyData.Add( key, serializedData );                                               //Lo marcamos como sucio(cache de modificado) porque está modificado.

        }

        private void SetRawData( string key, string serializedData )
        {
            if( rawData.ContainsKey( key ) )
            {
                rawData[key] = serializedData;
            }
            else
            {
                rawData.Add( key, serializedData );
            }
        }

        private void SetParsedData<T>( T data, Type type ) where T : IDTO
        {
            if( !parsedData.ContainsKey( type ) )
            {
                parsedData.Add( type, data );
            }
            else
            {
                parsedData[type] = data;
            }
        }

        public Task Save()
        {
            Assert.IsTrue( isInitialized, "Gateway is not initialized" );

            var task = setDataService.Set( dirtyData );                                         //Settea todos los datos que tengamos sucios a servidor. Es decir subelos.
            dirtyData.Clear();                                                                  //Limpiamos los datos que habían sido modificados. Porque después de guardado ya no están sucios.
            return task;                                                                        //Regresamos la task de subirlos al servidor.
        }
    }
}