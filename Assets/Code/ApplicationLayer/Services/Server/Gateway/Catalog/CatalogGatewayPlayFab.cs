using Code.ApplicationLayer.Services.Server.DTOs.Server;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Code.Domain.Services.Serializer;
using PlayFab.ClientModels;
using Code.Domain.Services.Server;
using UnityEngine.Assertions;

namespace Code.ApplicationLayer.Services.Server.Gateways.Catalog
{                                                                                               //Esta clase nos dará nuestros catalogos ya parseados. Sólo que no nos conectaremos directamente con la api de playfab
    public class CatalogGatewayPlayFab : ICatalogGateway, ICatalogDataPreLoaderService          //IDataPre... Trabajará con el identificador
    {
        private readonly ICatalogService catalogService;
        private readonly ISerializerService serializerService;
        private readonly Dictionary<string, List<CatalogItemDTO>> items;

        public CatalogGatewayPlayFab( ICatalogService catalogService, ISerializerService serializerService )
        {
            this.catalogService = catalogService;
            this.serializerService = serializerService;
            this.items = new Dictionary<string, List<CatalogItemDTO>>();
        }

        public async Task PreLoad<T>( string catalogId )                                              //En el de gateway preguntabamos si ya lo teníamos cacheados sino cargabamos en el servidor, en este lo que haremos es cargarlos desde el servidor. Si hago un Get y no he hecho preload es porque no estoy usando esta clase como esta diseñada.
        {
            await GetItemsFromServer<T>( catalogId );
        }

        public IReadOnlyList<CatalogItemDTO> GetItems( string catalogId )        //Método que regresa nuestras unidades ya parseadas de CatalogItem en catalogItemDTO y almacena en un cache(diccionario) para sólo hacerlo una vez
        {
            var alreadyCached = items.ContainsKey( catalogId );
            Assert.IsTrue( alreadyCached, "You need to call preload first" );
            return GetCachedItems( catalogId );
        }

        private List<CatalogItemDTO> GetCachedItems( string catalogId )
        {
            return items[catalogId];
        }

        private async Task GetItemsFromServer<T>( string catalogId )
        {
            var optionalItems = await catalogService.GetITems( catalogId );                     //Como no existe lo obtenemos para parsearlo.

            optionalItems.IfPresent( items => { ParseCatalogItems<T>( catalogId, items ); } )
                         .OrElseThrow( new System.Exception() );
        }

        private void ParseCatalogItems<T>( string catalogId, List<CatalogItem> items )
        {                                                                                       //Lo que nos permite SELECT es construir un objeto a partir de los datos que contiene este array suele hacer ciclos for dentro de él.
            var result = new List<CatalogItemDTO>( items.Select( ParseCatalogItem<T> ) );       //Nos devuelve un item ya parseado por cada uno que tenga la lista (se recorren por el for)
            this.items.Add( catalogId, result );                                                //Lo añadimos a los items que ya tenemos cacheados para evitar este proceso y sólo entrar por el diccionario.
        }

        private CatalogItemDTO ParseCatalogItem<T>( CatalogItem item )
        {
            return new CatalogItemDTO( item.ItemId,                                             //Estoy creando un nuevo CatalogItemDTO por cada uno que exista en playfab y lo estos mappeando.
                                       item.DisplayName, 
                                       serializerService.Deserialize<T>( item.CustomData )      //CustomData ya parseado no en string para que no se tenga que hacer cada vez.
                                       );
        }
    }
}
//Una vez aplicado esto tenemos el gateway para obtener los datos del catalogo lo siguiente es hacer el repository que mappeara estos datos a entidades.
