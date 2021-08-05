using Code.ApplicationLayer.Services.Server.DTOs.Server;
using NaughtyAttributes;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Code.UnityConfigurationAdapters.Units
{
    [CreateAssetMenu(menuName ="Base Defender/Units/New Unit", fileName ="UnitConfiguration", order =0)]
    public class UnitScriptableObject : ScriptableObject
    {
        [SerializeField] private string id;
        [SerializeField] private string displayName;
        [SerializeField] private UnitCustomDataDTO unitCustomDataDTO;

#if UNITY_EDITOR && ENABLE_PLAYFABADMIN_API
        [Button]
        public void LoadFromServer()
        {
            var request = new PlayFab.AdminModels.GetCatalogItemsRequest
            {
                CatalogVersion = "Units",
            };
            PlayFab.PlayFabAdminAPI.GetCatalogItems( request,
                                                     result =>
                                                     {
                                                         var catalogItem = result.Catalog.First( item => item.ItemId.Equals( id ) );                    //Buscamos el item especifico que queremos
                                                         displayName = catalogItem.DisplayName;                                                         //Tenemos que asignar los valores al Scriptable object empezamos a llamar parametros.
                                                         var unitCustomData = JsonUtility.FromJson<UnitCustomDataDTO>( catalogItem.CustomData );        //Tenemos que pasar los datos de playfab a nuestro DTO
                                                         unitCustomDataDTO = unitCustomData;                                                            //Guardamos valores
                                                         
                                                         //Ahora forzamos el serializado del Scriptable object.
                                                         UnityEditor.EditorUtility.SetDirty( this );
                                                         UnityEditor.AssetDatabase.SaveAssets();
                                                         UnityEditor.AssetDatabase.Refresh();
                                                     },
                                                     error => throw new System.Exception( error.ErrorMessage )
                                                     );
        }

        [Button()]
        public void SaveOnServer()
        {
            var request = new PlayFab.AdminModels.UpdateCatalogItemsRequest
            {
                Catalog = new List<PlayFab.AdminModels.CatalogItem>
                {
                    new PlayFab.AdminModels.CatalogItem
                    {
                        ItemId = id,
                        DisplayName = displayName,
                        CustomData = JsonUtility.ToJson(unitCustomDataDTO)
                    }
                },
                CatalogVersion = "Units",
            };
            PlayFab.PlayFabAdminAPI.
                UpdateCatalogItems( request,
                                    result => { Debug.Log( "Saved" ); },
                                    error => throw new System.Exception( error.ErrorMessage )
                                    );
        }
#endif
    }
}
//Una opción que se pensaría sería serializar el DTO, pero el DTO no debería de viajar por todo el código sólo estar en la capa de aplicación (datos).
//Usaremos los DTOs para no tener clases duplicadas con los mismos valores
//Como esto es una tool pues usaremos callbacks y lambdas pero sólo porque estarán en la parte del editor.