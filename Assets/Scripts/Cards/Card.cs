using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;
[System.Serializable]
public enum ETargetPlayer
    {
    None = 0,
    Self = 1,
    Neighbors = 2,
    SelftAndNeighbors = 3,
    AllOther = 4,
    All = 5,
}
[System.Serializable]
public abstract class Card : MonoBehaviour
{
    [SerializeField]
    private string nameValue;

    [SerializeField]
    private TextMeshProUGUI nameText;

    public virtual Color Color
    {
        get
        {
            switch (TargetPlayer)
            {
                case ETargetPlayer.Self:
                    return Color.green.Darker();
                    break;
                case ETargetPlayer.Neighbors:
                    return Color.Lerp()
                    break;
                case ETargetPlayer.SelftAndNeighbors:
                    apply(GameController.Instance.CurrentPlayer);
                    apply(GameController.Instance.CurrentPlayer.Neighbors);
                    break;
                case ETargetPlayer.AllOther:
                    apply(GameController.Instance.CurrentPlayer.Others);
                    break;
                case ETargetPlayer.All:
                    apply(GameController.Instance.CurrentPlayer);
                    apply(GameController.Instance.CurrentPlayer.Others);
                    break;
                case ETargetPlayer.None:
                default:
                    break;
            }
        }
    }

    [field: SerializeField]
    public string Name
    {
        get { return nameValue; }
        set
        {
            nameValue = value;
            if (nameText != null)
            {
                nameText.text = name;
            }
        }
    }

    [SerializeField]
    private string messageValue;

    [SerializeField]
    private TextMeshProUGUI message;

    [field: SerializeField]
    public string Message
    {
        get { return messageValue; }
        set
        {
            messageValue = value;
            if (message != null)
            {
                message.text = messageValue;
            }
        }
    }

    [SerializeField]
    private string titleValue;

    [SerializeField]
    private TextMeshProUGUI title;

    [field: SerializeField]
    public string Title
    {
        get { return titleValue; }
        set
        {
            titleValue = value;
            if (title != null)
            {
                title.text = titleValue;
            }
        }
    }

    [field: SerializeField]
    public uint ID { get; set; }
    
    [field: SerializeField]
    public Player Owner{ get; set; }
    
    [field: SerializeField]
    public ETargetPlayer TargetPlayer { get; set; }

    public virtual void Start()
    {
        Takeover(this);
    }
    public void Takeover(Card c)
    {
        Takeover(c.ID, c.Name, c.Title, c.Message);
    }
    public void Takeover(uint id=0, string name = "Card", string title = "Card Title", string message = "Hello World")
    {
        ID = id;
        Name = name;
        Title = title;
        Message = message;
    }
    protected bool apply(params IEnumerable<Player>[] players)
    {
        foreach (var playerGroup in players)
        {
            foreach (var player in playerGroup)
            {
                if (!apply(player))
                {
                    return false; // or handle the failure case
                }
            }
        }
        return true;
    }

    protected abstract bool apply(Player player);

    public void Apply()
    {
        switch (TargetPlayer)
        {
            case ETargetPlayer.Self:
                apply(GameController.Instance.CurrentPlayer);
                break;
            case ETargetPlayer.Neighbors:
                apply(GameController.Instance.CurrentPlayer.Neighbors);
                break;
            case ETargetPlayer.SelftAndNeighbors:
                apply(GameController.Instance.CurrentPlayer);
                apply(GameController.Instance.CurrentPlayer.Neighbors);
                break;
            case ETargetPlayer.AllOther:
                apply(GameController.Instance.CurrentPlayer.Others);
                break;
            case ETargetPlayer.All:
                apply(GameController.Instance.CurrentPlayer);
                apply(GameController.Instance.CurrentPlayer.Others);
                break;
            case ETargetPlayer.None:
            default:
                break;
        }
    }

}

