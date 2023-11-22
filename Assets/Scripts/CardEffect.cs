using UnityEngine;

[System.Serializable]
public abstract class CardEffect<TValue> : IDBObject, IName, IDisplay where TValue : CardValue<TValue>
{
    [field: SerializeField]
    public uint ID { get; set; }
    [field: SerializeField]
    public string Name { get; set; }
    [field: SerializeField]
    public string Text { get; set; }
    [field: SerializeField]
    public string Message { get; set; }
    [field: SerializeField]
    public TValue Value { get; set; }

    public CardEffect() : this(0)
    {
    }

    public CardEffect(uint id, string name = "Card Effect", string text = "Card Text", string message = "Hello World", TValue value = null)
    {
        ID = id;
        Name = name;
        Text = text;
        Message = message;
        Value = value == null ? null : value;
    }

    public abstract void Apply();

}