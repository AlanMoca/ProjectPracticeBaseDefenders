namespace ApplicationLayer.Services.Server.DTOs.Server
{
    public class CatalogItemDTO : IDTO                                                          //Este es nuestro y aqu� es donde tendremos que a�adir los datos parseados que nos triaga playfab
    {
        public readonly string ID;
        public readonly string DisplayName;
        private readonly object _customData;

        public CatalogItemDTO( string id, string displayName, object customData )
        {
            ID = id;
            DisplayName = displayName;
            _customData = customData;
        }

        public T GetCustomData<T>()
        {
            return (T)_customData;
        }
    }
}

