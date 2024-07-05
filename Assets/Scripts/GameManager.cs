using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public List<CardDisplay> playedCards = new List<CardDisplay>(); // Lista de cartas jugadas en este turno

    public TargetDisplay enemyTarget;
    public TargetDisplay playerTarget;

    public CardGenerator cardGenerator; // Referencia al generador de cartas
    public Transform cardsParent; // Padre donde se instancian las cartas
    public GameObject cardDisplayPrefab; // Prefab de la carta a instanciar 
    private Card selectedCard;

    private List<CardDisplay> currentCards = new List<CardDisplay>(); // Lista de cartas actuales en la zona de drops

    private int selectedCardsCount = 0;
    private const int maxSelectableCards = 2;

    void Awake()
    {
        instance = this;

        if (cardGenerator == null)
        {
            Debug.LogError("CardGenerator no asignado en el GameManager.");
        }
        if (cardDisplayPrefab == null)
        {
            Debug.LogError("CardDisplayPrefab no asignado en el GameManager.");
        }
        if (cardsParent == null)
        {
            Debug.LogError("CardsParent no asignado en el GameManager.");
        }
    }

    public void SelectCard(Card card)
    {
        if (selectedCardsCount < maxSelectableCards)
        {
            selectedCard = card;
            selectedCardsCount++;
            Debug.Log("Carta seleccionada: " + card.cardName);
        }
        else
        {
            Debug.LogWarning("No se pueden seleccionar más de " + maxSelectableCards + " cartas por turno.");
        }
    }

    // Método para añadir una carta jugada
    public void AddPlayedCard(CardDisplay cardDisplay)
    {
        if (playedCards.Count < maxSelectableCards)
        {
            playedCards.Add(cardDisplay);
            Debug.Log("Carta añadida a la lista de jugadas: " + cardDisplay.card.cardName + " con estadística " + cardDisplay.card.statN);
        }
    }

    // Método para finalizar el turno y aplicar el daño acumulado al enemigo
    public void EndTurn()
    {
        Debug.Log("Finalizando turno...");
        float totalDamage = 0f;
        float totalHealing = 0f;

        foreach (CardDisplay cardD in playedCards)
        {
            float cardStat = cardD.card.statN;

            switch (cardD.card.type)
            {
                case "DAÑA":
                    totalDamage += cardStat;
                    Debug.Log("Añadido daño: " + cardStat + ". Daño total: " + totalDamage);
                    break;
                case "CURA":
                    totalHealing += cardStat;
                    Debug.Log("Añadida cura: " + cardStat + ". Cura total: " + totalHealing);
                    break;
                case "CUBRE":
                    totalDamage -= cardStat * 1.5f;
                    Debug.Log("Reducido daño: " + cardStat + ". Daño total: " + totalDamage);
                    break;
                default:
                    Debug.LogWarning("Tipo de carta no reconocido: " + cardD.card.type);
                    break;
            }
        }

        // Aplicar el daño total al enemigo y curar al jugador
        Debug.Log("Aplicando daño total al enemigo: " + totalDamage);
        enemyTarget.TakeDamage(totalDamage);

        Debug.Log("Aplicando daño total al jugador: " + enemyTarget.target.damage);
        playerTarget.TakeDamage(enemyTarget.target.damage);

        Debug.Log("Aplicando cura total al jugador: " + totalHealing);
        playerTarget.Heal(totalHealing);

        // Limpiar la lista de cartas jugadas
        playedCards.Clear();

        // Resetear la cuenta de cartas seleccionadas
        selectedCardsCount = 0;

        // Generar y mostrar 3 nuevas cartas en la zona de drops
        GenerateNewCards();
    }

    // Método para generar 3 nuevas cartas y mostrarlas en la zona de drops
    public void GenerateNewCards()
    {
        Debug.Log("Generando nuevas cartas...");
        // Eliminar las cartas actuales
        foreach (CardDisplay card in currentCards)
        {
            Destroy(card.gameObject);
        }
        currentCards.Clear();

        // Generar y mostrar 3 nuevas cartas
        for (int i = 0; i < 3; i++)
        {
            Card newCard = cardGenerator.GenerateRandomCard();
            if (newCard != null && cardDisplayPrefab != null)
            {
                GameObject newCardObj = Instantiate(cardDisplayPrefab.gameObject, cardsParent);
                CardDisplay newCardDisplay = newCardObj.GetComponent<CardDisplay>();
                newCardDisplay.card = newCard;
                newCardDisplay.PrintCard(); // Actualizar la interfaz con los datos de la nueva carta
                currentCards.Add(newCardDisplay);
            }
            else
            {
                Debug.LogWarning("No se pudo generar una nueva carta o cardDisplayPrefab no está asignado correctamente.");
            }
        }
    }
}
