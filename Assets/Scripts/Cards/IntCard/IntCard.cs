using TMPro;
using UnityEngine;
[System.Serializable]
public class IntCard : Card
{

    [SerializeField]
    private int valueValue;

    [SerializeField]
    private TextMeshProUGUI valueTmPro;

    [field: SerializeField]
    public int Value
    {
        get { return valueValue; }
        set
        {
            valueValue = value;
            if (valueTmPro != null)
            {
                valueTmPro.text = value.ToString();
            }
        }
    }

    public override void Start()
    {
        //Logger.Log(Application.persistentDataPath + "/test.txt");
        Randomize();
    }

    protected override bool apply(Player player)
    {
        if(Owner == null)
        {
            Logger.Log($"Int Card [{ID}] Value = {Value} Subtract to {player.Name}", player.gameObject);
            player.money -= Value;
        }
        else if(player.ID != Owner.ID)
        {
            Logger.Log($"Int Card [{ID}] Value = {Value} Owned by {Owner.Name} Subtract to {player.Name}", player.gameObject);
            player.money -= Value;
        }
        else
        {
            Logger.Log($"Int Card [{ID}] Value = {Value} Added to {player.Name} ({player.Name == Owner.Name})", player.gameObject);
            player.money += Value;
        }
        
        return true;
    }

    public void Randomize()
    {
        Name = Randomizer.Name();
        Title = Randomizer.Sentence(1, 4);
        Message = Randomizer.Sentence(6, 12);
        Value = Random.Range(1, 100);
    }


}