using System;

namespace SystemUtilities
{
    //Es un patrón de diseño usado mucho en Java. Nos dice que puede estar el valor o puede no estar el valor. Y lo que nos permites es decirle sí está el valor has esto, sino está el valor has esta otra cosa. 
    //Usualmente alguien le tiene que pasar el opcional al consumidor ya que no tiene sentido preguntar si existe en el mismo consumidor. 
    //Para evitar o mejor dicho comprobar de otra forma si un valor es nulo o no es nulo nos apoyamos de esta clase, el consumidor hace uso de lambdas y de esta forma eliminamos el smellcode de "if nulo" sino intento recuperar para que no sea nulo
    //permitiendo trasladar esta responsabilidad a una clase (optional) y sea esa clase la que se encargue de preguntar eso. Ya que al consumidor no le importa.
    public class Optional<T>
    {
        private readonly T value;

        public T Get()
        {
            if( value == null )
                throw new Exception( "The element is null" );
            else
                return value;
        }
        //public T Get => value == null ? throw new Exception( "The element is null" ) : value;     //Es lo mismo que arriba.

        public Optional( T value )
        {
            this.value = value;
        }

        public Optional()
        {
        }

        public bool IsPresent()                                                                 //Podemos preguntar si está presente.
        {
            return value != null;
        }

        public Optional<T> IfPresent( Action<T> consumer )                                      //Si está presente ejecutamos la action que pasamor por parametro.
        {
            if( value == null )
                return this;

            consumer( value );
            return new NullOptional<T>( value );
        }

        public virtual void Else( Action action )
        {
            action();
        }

        public T OrElse( T elseValue )
        {
            if( value == null )
            {
                return elseValue;
            }

            return value;
        }

        public T OrElseThrow( Exception exceptionToThrow )
        {
            if( value == null )
            {
                throw exceptionToThrow;
            }

            return value;
        }
    }

    public class NullOptional<T> : Optional<T>                                                  //Sólo es para permitirnos hacer el else.
    {
        public NullOptional( T value ) : base( value )
        {
        }

        public override void Else( Action action )
        {
        }
    }
}