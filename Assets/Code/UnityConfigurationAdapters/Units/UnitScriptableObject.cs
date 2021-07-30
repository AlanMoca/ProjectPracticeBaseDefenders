using ApplicationLayer.Services.Server.DTOs.Server;
using NaughtyAttributes;
using PlayFab;
using PlayFab.AdminModels;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UnityConfigurationAdapters.Units
{
    [CreateAssetMenu(menuName ="Base Defender/Units/New Unit", fileName ="UnitConfiguration", order =0)]
    public class UnitScriptableObject : ScriptableObject
    {
        [SerializeField] private string id;
        [SerializeField] private string name;
        [SerializeField] private int attack;
        [SerializeField] private int health;

        [Button]
        public void LoadFromServer()
        {
            var request = new GetCatalogItemsRequest
            {
                CatalogVersion = "Units",
            };
            PlayFabAdminAPI.GetCatalogItems( request,
                                             result =>
                                             {
                                                 var catalogItem = result.Catalog.First( item => item.ItemId.Equals( id ) );                    //Buscamos el item especifico que queremos
                                                 name = catalogItem.DisplayName;                                                                //Tenemos que asignar los valores al Scriptable object empezamos a llamar parametros.
                                                 var unitCustomData = JsonUtility.FromJson<UnitCustomDataDTO>( catalogItem.CustomData );        //Tenemos que pasar los datos de playfab a nuestro DTO
                                                 attack = unitCustomData.Attack;                                                                //Guardamos valores
                                                 health = unitCustomData.Health;

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
            var request = new UpdateCatalogItemsRequest
            {
                Catalog = new List<CatalogItem>
                {
                    new CatalogItem
                    {
                        ItemId = id,
                        DisplayName = name,
                        CustomData = JsonUtility.ToJson(new UnitCustomDataDTO(attack, health))
                    }
                },
                CatalogVersion = "Units",
            };
            PlayFabAdminAPI.UpdateCatalogItems( request, 
                                                result => { Debug.Log( "Saved" ); },
                                                error =>
                                                {
                                                    throw new System.Exception( error.ErrorMessage );
                                                } 
                                                );
        }

    }
}
//Una opción que se pensaría sería serializar el DTO, pero el DTO no debería de viajar por todo el código sólo estar en la capa de aplicación (datos).
//Usaremos los DTOs para no tener clases duplicadas con los mismos valores
//Como esto es una tool pues usaremos callbacks y lambdas pero sólo porque estarán en la parte del editor.