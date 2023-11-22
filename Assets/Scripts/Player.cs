using UnityEngine;
[System.Serializable]
public class Player : MonoBehaviour, IDBObject, IName
{
    [field: SerializeField]
    public uint ID { get; set; }
    [field: SerializeField]
    public string Name { get; set; }

    
}