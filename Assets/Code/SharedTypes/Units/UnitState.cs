using UnityEngine;

namespace Code.SharedTypes.Units
{
    [System.Serializable]
    public class UnitState : MonoBehaviour
    {
        [SerializeField] private int healt;
        [SerializeField] private int level;

        public UnitState( int healt, int level )
        {
            this.healt = healt;
            this.level = level;
        }
    }
}