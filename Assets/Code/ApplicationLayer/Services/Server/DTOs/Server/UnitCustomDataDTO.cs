namespace ApplicationLayer.Services.Server.DTOs.Server
{
    [System.Serializable]
    public class UnitCustomDataDTO : IDTO                                                       //Tiene que contener lo que tengamos en playfab ya que añadiremos esos datos ya parseados. (Si es serializable no puedo usar readonly)
    {
        public int Health;
        public int Attack;

        public UnitCustomDataDTO( int health, int attack )
        {
            Health = health;
            Attack = attack;
        }
    }
}