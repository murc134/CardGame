public interface IDBObject
{
    uint ID { get; set; }
}

public interface IName
{
    string Name { get; set; }
}

public interface IDisplay
{

    string Text { get; set; }

    string Message { get; set; }
}