using UnityEngine;

namespace Code.SharedTypes.Units
{
    [System.Serializable]
    public class UnitState : MonoBehaviour
    {
        [SerializeField] private int level;

        public UnitState( int level )
        {
            this.level = level;
        }
    }
}