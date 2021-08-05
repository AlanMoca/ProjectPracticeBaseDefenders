using UnityEngine;

namespace Code.SharedTypes.Units
{
    [System.Serializable]
    public class UnitAttributes                                                                 //Esto nos permite ahorarnos el ir modificando otros códigos, sólo con editar esta clase.
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

        public UnitAttributes( int health, int healthIncrementPerLevel, int attack, int attackIncrementPerLevel,
            int movementSpeed, int secondsBetweenAttacks, int initialUpgradeCost, int invocationSecondsCooldown )
        {
            this.health = health;
            this.healthIncrementPerLevel = healthIncrementPerLevel;
            this.attack = attack;
            this.attackIncrementPerLevel = attackIncrementPerLevel;
            this.movementSpeed = movementSpeed;
            this.secondsBetweenAttacks = secondsBetweenAttacks;
            this.initialUpgradeCost = initialUpgradeCost;
            this.invocationSecondsCooldown = invocationSecondsCooldown;
        }

    }
}