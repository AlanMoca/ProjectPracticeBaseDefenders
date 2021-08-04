using UnityEngine;

namespace Code.SharedTypes.Units
{
    public class UnitAttributes                                                                 //Esto nos permite ahorarnos el ir modificando otros c�digos, s�lo con editar esta clase.
    {
        [SerializeField] private int health;
        [SerializeField] private int healthIncrementPerLevel;
        [SerializeField] private int attack;
        [SerializeField] private int attackIncrementPerLevel;
        [SerializeField] private int movementSpeed;
        [SerializeField] private int secondsBetweenAttacks;
        [SerializeField] private int initialUpgradeCost;
        [SerializeField] private int invocationSecondsCooldown;

        public int Health => health;
        public int HealthIncrementPerLevel => healthIncrementPerLevel;
        public int Attack => attack;
        public int AttackIncrementPerLevel => attackIncrementPerLevel;
        public int MovementSpeed => movementSpeed;
        public int SecondsBetweenAttacks => secondsBetweenAttacks;
        public int InitialUpgradeCost => initialUpgradeCost;
        public int InvocationSecondsCooldown => invocationSecondsCooldown;


    }
}