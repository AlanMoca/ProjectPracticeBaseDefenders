using Code.Domain.Services.Serializer;
using UnityEngine;

namespace Code.ApplicationLayer.Services.Serializer
{
    public class UnityJsonSerializer : ISerializerService
    {
        public string Serialize<T>( T data )
        {
            return JsonUtility.ToJson( data );
        }

        public T Deserialize<T>( string data )
        {
            return JsonUtility.FromJson<T>( data );
        }
    }
}
