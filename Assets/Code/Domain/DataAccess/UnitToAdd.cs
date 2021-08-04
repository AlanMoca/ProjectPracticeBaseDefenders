using Code.SharedTypes.Units;

namespace Code.Domain.DataAccess
{
    public class UnitToAdd
    {
        public readonly string Id;
        public readonly UnitState UnitState;

        public UnitToAdd( string id, UnitState unitState )
        {
            Id = id;
            UnitState = unitState;
        }
    }
}