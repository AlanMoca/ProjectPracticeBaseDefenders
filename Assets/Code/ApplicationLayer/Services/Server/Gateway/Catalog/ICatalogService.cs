using PlayFab.ClientModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using SystemUtilities;

namespace ApplicationLayer.Services.Server.Gateways.Catalog
{
    public interface ICatalogService                                                            //Ser� la interface que implementar�n las clases que se quieran conectar con el catalogo pero ya propio de playfab
    {
        Task<Optional<List<CatalogItem>>> GetITems( string catalogId );
    }
}

//El ICatalogService lo tendr�amos que renombrar a IGetCatalogService porque lo �nico que har� la clase que la implementa es obtener el catalogo.
//Esto porque si luego queremos actualizar el catalogo se deber�a de hacer en otra implentaci�n con otra interface por Single Responsability.
//Cuando decimos una responsabilidad es un motivo de cambio, es decir si podemos pensar cualquier cosa que sea distinta que pueda afectar a una clase es porque son distintos motivos de cambio, por ejemplo:
//Si en el catalog tengo get pero tambi�n hago para actualizar entonces son 2 motivos de cambio porque si ma�ana cambia como obtengo los datos lo tengo que modificar y si tambi�n cambia
//como modifico los datos tambi�n lo tendr�a que cambiar por lo que son 2 motivos de cambio.
