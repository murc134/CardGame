using UnityEngine;
[System.Serializable]
public abstract class Card : MonoBehaviour
{
    [field: SerializeField]
    public uint ID { get; set; }

    [field: SerializeField]
    public string Name { get; set; }
    [field: SerializeField]
    public string Title { get; set; }
    [field: SerializeField]
    public string Message { get; set; }

    public Card() : this(0)
    {
    }

    public Card(uint id, string name = "Card", string title = "Card Title", string message = "Hello World")
    {
        ID = id;
        Name = name;
        Title = title;
        Message = message;
    }

    public abstract void Apply();

}

[System.Serializable]
public abstract class Card<TValue, TEffect> : Card
    where TValue : CardValue<TValue>
    where TEffect : CardEffect<TValue>
{
    [field: SerializeField]
    public uint ID { get; set; }
    
    [field: SerializeField]
    public string Name { get; set; }
    [field: SerializeField]
    public string Title { get; set; }
    [field: SerializeField]
    public string Message { get; set; }
    [field: SerializeField]
    public TValue Value { get; set; }
    [field: SerializeField]
    public TEffect Effect { get; set; }

    public Card() : this(0)
    {
        
    }

    public Card(uint id, string name="Card", string title="Card Title", string message="Hello World", TValue value = null, TEffect effect=null) : base(id, name, title, message)
    {
        Value = value;
        Effect = effect;
    }

    public override void Apply()
    {
        Effect.Apply();
    }

}