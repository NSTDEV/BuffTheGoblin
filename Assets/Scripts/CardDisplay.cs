using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class CardDisplay : MonoBehaviour
{
    public Card card;

    public TMP_Text nameText, typeText, statText;
    public Image artworkImage;

    private Dictionary<string, string> cardTypeColors = new Dictionary<string, string>()
    {
        { "DAÑA", "#dd191d" },
        { "CUBRE", "#006064" },
        { "CURA", "#689f38" }
    };

    void Start()
    {
        if (card != null)
        {
            PrintCard();
        }
        else
        {
            Debug.LogError("Card is not assigned in the inspector");
        }
    }

    public void PrintCard()
    {
        nameText.text = card.cardName.ToUpper();
        typeText.text = card.type.ToUpper();
        statText.text = card.stat;

        artworkImage.sprite = card.artwork;
        DyeColor();
    }

    public void DyeColor()
    { //Asignar color basado en el tipo de carta
        if (cardTypeColors.ContainsKey(card.type))
        {
            string hexColor = cardTypeColors[card.type];
            Color typeColor;

            if (ColorUtility.TryParseHtmlString(hexColor, out typeColor))
            {
                typeText.color = typeColor;
            }
        }
        else
        {
            Debug.LogWarning("Card type not found in color dictionary");
        }
    }
}