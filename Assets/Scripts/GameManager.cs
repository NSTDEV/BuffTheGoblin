using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public List<Card> playedCards = new List<Card>(); // Lista de cartas jugadas en este turno

    public TargetDisplay enemyTarget;
    public TargetDisplay playerTarget;

    public CardGenerator cardGenerator; // Referencia al generador de cartas
    public Transform cardsParent; // Padre donde se instancian las cartas
    public GameObject cardDisplayPrefab; // Prefab de la carta a instanciar 

    private List<CardDisplay> currentCards = new List<CardDisplay>(); // Lista de cartas actuales en la zona de drops

    private int selectedCardsCount;
    private const int maxSelectableCards = 2;

    void Awake()
    {
        selectedCardsCount = 0;
        instance = this;
        GenerateNewCards();

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

    // Método para añadir una carta jugada
    public void AddPlayedCard(Card card)
    {
        if (selectedCardsCount < maxSelectableCards)
        {
                selectedCardsCount++;
                playedCards.Add(card);
                Debug.LogWarning("Carta añadida a la lista de jugadas: " + card.cardName + " con estadística " + card.statN);
        }
        else
        {
            Debug.LogWarning("No se pueden seleccionar más de " + maxSelectableCards + " cartas por turno.");
        }
    }

    // Método para finalizar el turno y aplicar el daño acumulado al enemigo
    public void EndTurn()
    {
        Debug.Log("Finalizando turno...");
        float totalDamage = 0f;
        float totalHealing = 0f;

        foreach (Card cardD in playedCards)
        {
            float cardStat = cardD.statN;

            switch (cardD.type)
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
                    enemyTarget.target.damage = enemyTarget.target.damage * 0.5f;
                    Debug.Log("Reducido daño: " + cardStat + ". Daño total: " + totalDamage);
                    break;
                default:
                    Debug.LogWarning("Tipo de carta no reconocido: " + cardD.type);
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
