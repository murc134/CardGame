using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardPileVisualization : MonoBehaviour
{
    [SerializeField]
    private MaskableGraphic background;

    // Setter for the alpha value of a MaskableGraphic
    public float BackgroundAlpha
    {
        get
        {
            if (background is Graphic graphic)
            {
                return graphic.color.a;
            }
            return 1.0f; // Default alpha
        }
        set
        {
            if (background is Graphic graphic)
            {
                Color currentColor = graphic.color;
                graphic.color = new Color(currentColor.r, currentColor.g, currentColor.b, value);
            }
        }
    }

    public Player Owner
    {
        get
        {
            return card.Owner == null ? null : card.Owner;
        }
    }

    private float Money
    {
        get
        {
            return card.Owner == null ? 0 : card.Owner.money;
        }
    }

    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI money;
    [SerializeField] private TextMeshProUGUI amount;

    private const string defaultMoneyText = "0 $"; // Default value

    private Color textColor = Color.white; // Default text color

    private int lasthash = 0;

    public Color TextColor
    {
        get { return textColor; }
        set
        {
            textColor = value;

            if (title != null)
            {
                title.color = textColor;
            }

            if (money != null)
            {
                money.color = textColor;
            }

            if (amount != null)
            {
                amount.color = textColor;
            }
        }
    }
    // Setter for the color of a MaskableGraphic
    public Color BackgroundColor
    {
        get
        {
            if (background is Graphic graphic)
            {
                return graphic.color;
            }
            return Color.white; // Default color
        }
        set
        {
            if (background is Graphic graphic)
            {
                graphic.color = value;
            }
        }
    }
    public string TitleText
    {
        get { return title.text; }
        set { title.text = value; }
    }

    public string MoneyText
    {
        get { return money.text; }
        set
        {
            // Replace all recognized currency symbols with '¢'
            string pattern = "[" + string.Join("", CultureInfo.GetCultures(CultureTypes.SpecificCultures)
                        .Select(culture => new RegionInfo(culture.Name).ISOCurrencySymbol)
                        .Distinct().ToArray()) + "]";

            value = Regex.Replace(value, pattern, "¢");

            if (string.IsNullOrEmpty(value))
            {
                money.text = "0 ¢";
            }
            else
            {
                // Ensure there's only one currency symbol in the string
                if (value.Count(c => c == '¢') > 1)
                {
                    // Handle multiple currency symbols here, for example, by removing all but the first one
                    int firstCurrencyIndex = value.IndexOf("¢");
                    value = value.Remove(firstCurrencyIndex + 1, value.Count(c => c == '¢') - 1);
                }

                money.text = value;
            }

            // Add a $ sign if it's not already there
            if (!money.text.EndsWith("$"))
            {
                money.text += " $";
            }
        }
    }

    public string AmountText
    {
        get { return amount.text; }
        set
        {
            // If the value is null or empty, display "0"
            if (string.IsNullOrEmpty(value))
            {
                amount.text = "0";
            }
            else
            {
                // Remove any non-numeric characters and format the number with commas and periods
                string numericValue = Regex.Replace(value, @"[^\d]", "");
                decimal parsedValue;

                if (decimal.TryParse(numericValue, out parsedValue))
                {
                    amount.text = parsedValue.ToString("N2", CultureInfo.GetCultureInfo("de-DE"));
                }
                else
                {
                    // Handle invalid input here
                    amount.text = "0";
                }
            }
        }
    }

    public CardPile card;

    private int ColorHash = 0;

    void Start()
    {
        Player player = GetComponent<Player>();

        TitleText = player == null ? "Card Pile" : player.name;
        AmountText = "0";
        MoneyText = "0";

        card = GetComponent<CardPile>();
    }

    private void Update()
    {
        AmountText = card.Count.ToString();
        MoneyText = Money.ToString();
        if((BackgroundColor.GetHashCode() ^ TextColor.GetHashCode() ^ Owner.GetHashCode() ^ GameController.Instance.CurrentPlayer.GetHashCode()) != ColorHash  )
        {
            Debug.Log("SetColors");
            SetColors();
        }
        
    }

    public void SetColors()
    {
        Color bgc = Color.white;
        Color tc = Color.black;

        if (Owner != null)
        {
            bgc = Owner.IsCurrentPlayer ? Color.green.Darker() : Color.red.Darker();
            tc = Color.white;
        }
        else
        {
            bgc = Color.yellow.Orange();
            tc = Color.white;
        }

        BackgroundColor = bgc;
        TextColor = tc;

        ColorHash = bgc.GetHashCode() ^ tc.GetHashCode() ^ Owner.GetHashCode() ^ GameController.Instance.CurrentPlayer.GetHashCode();
    }

}
