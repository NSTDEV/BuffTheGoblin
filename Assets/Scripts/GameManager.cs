using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class GameManager : MonoBehaviour, IDropHandler
{
    public static GameManager instance;
    public Target enemy, player;

    private List<Card> playedCards = new List<Card>(); // Lista de cartas jugadas en este turno

    void Awake()
    {
        instance = this;
    }

    // Método para añadir una carta jugada
    public void AddPlayedCard(Card card)
    {
        if (card != null)
        {
            playedCards.Add(card);
        }
        else
        {
            Debug.LogWarning("La carta pasada es nula");
        }
    }

    // Método para finalizar el turno y aplicar el daño acumulado al enemigo
    public void EndTurn()
    {
        float totalDamage = 0f;
        float totalHealing = 0f;

        foreach (Card card in playedCards)
        {
            switch (card.type)
            {
                case "DAÑA":
                    totalDamage += card.statN;
                    break;
                case "CURA":
                    totalHealing += card.statN;
                    break;
                case "CUBRE":
                    totalDamage -= card.statN * 1.5f;
                    break;
                default:
                    Debug.LogWarning("Tipo de carta no reconocido: " + card.type);
                    break;
            }
        }

        // Aplicar el daño total al enemigo y curar al jugador
        enemy.TakeDamage(totalDamage);
        player.Heal(totalHealing);

        // Limpiar la lista de cartas jugadas para el siguiente turno
        playedCards.Clear();
    }

    public void OnDrop(PointerEventData eventData)
    {
        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();

        if (d != null)
        {
            CardDisplay cardDisplay = d.GetComponent<CardDisplay>();
            if (cardDisplay != null && cardDisplay.card != null)
            {
                // Aquí deberías asegurarte de que cardDisplay.card sea del tipo correcto
                Debug.Log("Carta " + cardDisplay.card.name + " soltada en: " + gameObject.name);
                instance.AddPlayedCard(cardDisplay.card);
            }
            else
            {
                Debug.LogWarning("No se encontró CardDisplay o la carta no está asignada correctamente.");
            }
        }
    }
}