using UnityEngine;
using System.Collections.Generic;

public class CardGenerator : MonoBehaviour
{
    public List<Card> availableCards; // Lista de cartas disponibles para ser generadas

    // Método para generar una carta aleatoria
    public Card GenerateRandomCard()
    {
        if (availableCards == null || availableCards.Count == 0)
        {
            Debug.LogError("No hay cartas disponibles para generar.");
            return null;
        }

        int randomIndex = Random.Range(0, availableCards.Count);
        return availableCards[randomIndex];
    }
}
