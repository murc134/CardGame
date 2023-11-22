using UnityEngine;
[System.Serializable]
public abstract class CardValue<T> where T : class
{
    [SerializeField]
    private T value;
    public T Value
    {
        get { return value == null ? DefaultValue : value; }
        set { this.value = value == null ? null : value; }
    }
    
    public abstract T DefaultValue { get; }

    public bool IsNull
    {
        get
        {
            return value == null;
        }
    }
}