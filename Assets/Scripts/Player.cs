using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Player : MonoBehaviour, IDBObject, IName
{
    public float money = 0;

    [field: SerializeField]
    public uint ID { get; set; }
    [SerializeField]
    private string name;
    public string Name { get { return name; } set { gameObject.name = name = value; } }
    [field: SerializeField]
    public CardPile CardPile { get; set; }

    public bool IsCurrentPlayer
    {
        get
        {
            return GameController.Instance.CurrentPlayer == this;
        }
    }

    // Property to get/set the player's index in the players list
    public int PlayerIndex
    {
        get { return GameController.Instance.Players.IndexOf(this); }
        set
        {
            // Ensure the player is in the list at the correct position
            if (GameController.Instance.Players.Contains(this))
            {
                GameController.Instance.Players.Remove(this);
            }
            GameController.Instance.Players.Insert(value, this);
        }
    }

    // Neighbors based on the current instance's position in the list
    public List<Player> Neighbors
    {
        get
        {
            int index = PlayerIndex;
            if (index == -1 || GameController.Instance.Players.Count < 3) return new List<Player>(); // Player not found or not enough players

            int count = GameController.Instance.Players.Count;
            int prevIndex = (index + count - 1) % count; // Wrap around for the previous neighbor
            int nextIndex = (index + 1) % count; // Wrap around for the next neighbor

            List < Player > neighbors = new List<Player>  ();
            neighbors.Add(GameController.Instance.Players[prevIndex]);
            neighbors.Add(GameController.Instance.Players[nextIndex]);

            return neighbors;
        }
    }

    public List<Player> Others
    {
        get
        {
            if (GameController.Instance.CurrentPlayer == null || GameController.Instance.Players.Count < 2) return new List<Player>(); // Return empty if Current is not set or not enough players

            List<Player> neighbors = new List<Player>(GameController.Instance.Players);
            neighbors.Remove(GameController.Instance.CurrentPlayer); // Remove the Current player from the list

            return neighbors; // Convert to array and return
        }
    }

    public void Awake()
    {
        CardPile.Owner = this;
        if(string.IsNullOrEmpty(Name) || Name == "Name")
        {
            Name = "Player" + PlayerIndex;
        }
    }

    private void OnEnable()
    {
        if (!GameController.Instance.Players.Contains(this))
        {
            GameController.Instance.Players.Add(this);
        }
    }
    private void OnDisable()
    {
        if (GameController.Instance.Players.Contains(this))
        {
            GameController.Instance.Players.Remove(this);
        }

    }
}