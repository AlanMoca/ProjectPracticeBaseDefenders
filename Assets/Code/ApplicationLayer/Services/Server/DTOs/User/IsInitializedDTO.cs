using UnityEngine;

namespace ApplicationLayer.Services.Server.DTOs.User
{
    [System.Serializable]
    public class IsInitializedDTO : IDTO
    {
        [SerializeField]                                                                        //Al convertirlo en JSON la variable tiene que ser publica pero como no queremos que nadie la modifique la hacemos privada.
        private bool isInitialized;                                                             //Pero entonces tenemos que serializarla para que sea visible pero no editable.
                                                                                                //El contenido de cada JSON del servidor debe coincidir con el contenido de cada DTO respectivamente. (isInitialized = en key server y aquí).
        public bool IsInitialized => isInitialized;
    }
}