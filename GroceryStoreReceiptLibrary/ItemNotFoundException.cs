using System;

public class ItemNotFound : Exception
{
    public ItemNotFound()
    {
    }

    public ItemNotFound(string message)
        : base(message)
    {
    }

}
