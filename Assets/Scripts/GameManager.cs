using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public TargetDisplay enemy, player;

    private List<CardDisplay> playedCards = new List<CardDisplay>(); // Lista de cartas jugadas en este turno

    void Awake()
    {
        instance = this;

        if (enemy == null)
        {
            Debug.LogError("Enemy target display is not assigned.");
        }
        if (instance.player == null)
        {
            Debug.LogError("Player target display is not assigned.");
        }
    }

    //Método para añadir una carta jugada
    public void AddPlayedCard(CardDisplay cardD)
    {
        if (cardD != null)
        {
            playedCards.Add(cardD);
        }
        else
        {
            Debug.LogWarning("La carta pasada es nula");
        }
    }

    //Método para finalizar el turno y aplicar el daño acumulado al enemigo
    public void EndTurn()
    {
        float totalDamage = 0f;
        float totalHealing = 0f;

        foreach (CardDisplay cardD in playedCards)
        {
            switch (cardD.card.type)
            {
                case "DAÑA":
                    totalDamage += cardD.card.statN;
                    break;
                case "CURA":
                    totalHealing += cardD.card.statN;
                    break;
                case "CUBRE":
                    totalDamage -= cardD.card.statN * 1.5f;
                    break;
                default:
                    Debug.LogWarning("Tipo de carta no reconocido: " + cardD.card.type);
                    break;
            }
        }

        //Aplicar el daño total al enemigo y curar al jugador
        enemy.TakeDamage(totalDamage);
        player.Heal(totalHealing);

        foreach (CardDisplay cardD in playedCards)
        {
            Destroy(cardD.gameObject);
        }

        playedCards.Clear();
    }
}