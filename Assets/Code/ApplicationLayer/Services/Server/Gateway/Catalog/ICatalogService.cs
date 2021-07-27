using PlayFab.ClientModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using SystemUtilities;

namespace ApplicationLayer.Services.Server.Gateways.Catalog
{
    public interface ICatalogService                                                            //Será la interface que implementarán las clases que se quieran conectar con el catalogo pero ya propio de playfab
    {
        Task<Optional<List<CatalogItem>>> GetITems( string catalogId );
    }
}

//El ICatalogService lo tendríamos que renombrar a IGetCatalogService porque lo único que hará la clase que la implementa es obtener el catalogo.
//Esto porque si luego queremos actualizar el catalogo se debería de hacer en otra implentación con otra interface por Single Responsability.
//Cuando decimos una responsabilidad es un motivo de cambio, es decir si podemos pensar cualquier cosa que sea distinta que pueda afectar a una clase es porque son distintos motivos de cambio, por ejemplo:
//Si en el catalog tengo get pero también hago para actualizar entonces son 2 motivos de cambio porque si mañana cambia como obtengo los datos lo tengo que modificar y si también cambia
//como modifico los datos también lo tendría que cambiar por lo que son 2 motivos de cambio.
