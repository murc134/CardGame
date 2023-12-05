using System.Collections.Generic;
using UnityEngine;
public class GameController : MonoBehaviour
{
    private static GameController instance;
    public static GameController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindFirstObjectByType<GameController>();
            }
            return instance;
        }
    }
    [SerializeField]
    private List<Player> players = new List<Player>();

    public List<Player> Players
    {
        get
        {
            return players;
        }
    }

    public Player CurrentPlayer
    {
        get
        {
            return players[0];
        }
        set
        {
            if (players.Contains(value))
            {
                players.Remove(value);
                
            }
            else
            {
                players.Insert(0, value);
            }
        }
    }

    public Player NextPlayer
    {
        get
        {
            if (players.Count <= 0)
            {
                return null;
            }
            else if (players.Count == 1)
            {
                return players[0];
            }
            else
            {
                return players[1];
            }

        }
    }

    public Player PreviousPlayer
    {
        get
        {
            if(players.Count > 1)
            {
                return players[players.Count - 1];
            }
            else if(players.Count == 1)
            {
                return players[0];
            }
            else
            {
                return null;
            }
        }
    }

    [SerializeField]
    private IntCard CardPrefab;

    [field:SerializeField]
    public CardPile TableCards
    {
        get;
        set;
    }

    public void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        AddTestCards();
    }

    public void AddTestCards()
    {
        Card c = Instantiate<Card>(CardPrefab);
    }
    
    public void Next()
    {
        if (GameController.Instance.Players == null || GameController.Instance.Players.Count <= 0)
        {
            return;
        }
        // Remove the first element and add it to the end of the list
        Player firstCard = GameController.Instance.Players[0];
        GameController.Instance.Players.RemoveAt(0);
        GameController.Instance.Players.Add(firstCard);
    }
    public void Previous()
    {
        if (GameController.Instance.Players == null || GameController.Instance.Players.Count <= 0)
        {
            return;
        }
        // Remove the last element and add it to the beginning of the list
        Player lastCard = GameController.Instance.Players[GameController.Instance.Players.Count - 1];
        GameController.Instance.Players.RemoveAt(GameController.Instance.Players.Count - 1);
        GameController.Instance.Players.Insert(0, lastCard);
    }

    private bool vertDown = false;
    private bool horiDown = false;

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        //float verticalInput = Input.GetAxis("Vertical");

        if (horizontalInput > 0)
        {
            if(horiDown == false)
            {
                horiDown = true;
                Next();
            }
            
        }
        else if (horizontalInput < 0)
        {
            if (horiDown == false)
            {
                horiDown = true;
                Previous();
            }
        }
        else
        {
            vertDown = horiDown = false;
        }
    }
}