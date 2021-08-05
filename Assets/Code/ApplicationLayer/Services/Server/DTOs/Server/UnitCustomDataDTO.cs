using Code.SharedTypes.Units;
using UnityEngine;
using System;

namespace Code.ApplicationLayer.Services.Server.DTOs.Server
{
    [Serializable]
    public class UnitCustomDataDTO                                                              //Tiene que contener lo que tengamos en playfab ya que añadiremos esos datos ya parseados. (Si es serializable no puedo usar readonly)
    {
        [SerializeField] private UnitAttributes unitAttributes;

        public UnitAttributes UnitAttributes => unitAttributes;
    }
}