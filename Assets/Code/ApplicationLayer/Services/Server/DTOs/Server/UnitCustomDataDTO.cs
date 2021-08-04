using Code.SharedTypes.Units;
using UnityEngine;

namespace Code.ApplicationLayer.Services.Server.DTOs.Server
{
    [System.Serializable]
    public class UnitCustomDataDTO : IDTO                                                       //Tiene que contener lo que tengamos en playfab ya que añadiremos esos datos ya parseados. (Si es serializable no puedo usar readonly)
    {
        [SerializeField] private UnitAttributes unitAttributes;

        public UnitAttributes UnitAttributes => unitAttributes;
    }
}