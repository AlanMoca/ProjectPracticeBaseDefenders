using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Code.ApplicationLayer.Services.Server.DTOs.Server
{
    [Serializable]
    public class InitialUnitsDTO : IDTO
    {
        [SerializeField] private string[] unitsId;

        public string[] UnitsId => unitsId;

    }
}