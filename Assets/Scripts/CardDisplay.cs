using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine.EventSystems;

public class CardDisplay : MonoBehaviour, IPointerClickHandler
{
    public Card card;

    public TMP_Text nameText, typeText, statText;
    public Image artworkImage;

    private Dictionary<string, Color> cardTypeColors = new Dictionary<string, Color>()
    {
        { "DAÑA", new Color(0.86f, 0.10f, 0.11f) }, // Rojo
        { "CUBRE", new Color(0f, 0.38f, 0.39f) },   // Azul
        { "CURA", new Color(0.41f, 0.62f, 0.22f) }  // Verde
    };

    private Vector3 originalPosition; // Guarda la posición original del objeto

    void Start()
    {
        originalPosition = transform.position; // Guarda la posición inicial al inicio
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
    {
        // Asignar color basado en el tipo de carta
        if (cardTypeColors.ContainsKey(card.type))
        {
            typeText.color = cardTypeColors[card.type];
        }
        else
        {
            Debug.LogWarning("Card type not found in color dictionary");
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Aquí puedes usar card para lo que necesites
        Debug.Log("Carta seleccionada: " + card.cardName);

        // Llama a la función en GameManager para agregar la carta jugada
        // Llama a la función en GameManager para agregar la carta jugada
        GameManager.instance.AddPlayedCard(card);

        // Desplazar el objeto hacia arriba 50 pixeles
        Vector3 newPosition = transform.position + Vector3.up * 50f;
        transform.position = newPosition;            
    }
}
