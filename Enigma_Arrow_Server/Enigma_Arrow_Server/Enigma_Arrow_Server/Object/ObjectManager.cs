using Google.Protobuf.Protocol;
using System;
using System.Collections.Generic;
using System.Text;

public class ObjectManager
{

    object _lock = new object();

    // [UNUSED(1)][TYPE(7)][ID(24)]
    int _counter = 0;

    public T Add<T>() where T : GameObject, new()
    {
        T gameObject = new T();

        lock (_lock)
        {
            gameObject.Id = _counter++;
        }

        return gameObject;
    }
}
