namespace DiasShared.Operations.GenericOperation
{
    public class GenericProperty<T>
    {
            private T _value;

            public T Value
            {
                get
                {
                    // insert desired logic here
                    return _value;
                }
                set
                {
                    // insert desired logic here
                    _value = value;
                }
            }

            public static implicit operator T(GenericProperty<T> value)
            {
                return value.Value;
            }

            public static implicit operator GenericProperty<T>(T value)
            {
                return new GenericProperty<T> { Value = value };
            }
        }
 }
