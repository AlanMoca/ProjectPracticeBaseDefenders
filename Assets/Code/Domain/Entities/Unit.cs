namespace Domain.Entities
{
    //En esta clase es donde se añade el ataque, el identificador, la vida, todos los datos mappeados del DTO pero convertidos en Unit porque aquí estarán los datos
    //ya transformados con funciones para modificarlos. Luego de esto tendremos UnitUser y otras pero aquí estarán los datos base.
    //NOTA: Una entidad no puede conocer un DTO.
    public class Unit
    {
        public readonly string Id;                                                              //Readonly es más optimo que usar getters y settersporque al final el set no deja de ser una función pero es lo mismo.
        public readonly string Name;
        public readonly int Attack;
        public readonly int Health;

        public Unit( string id, string name, int attack, int health )
        {
            Id = id;
            Name = name;
            Attack = attack;
            Health = health;
        }
    }
}

