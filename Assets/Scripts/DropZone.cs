using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject clickedObject = eventData.pointerPress.gameObject;
        CardDisplay cardDisplay = clickedObject.GetComponentInChildren<CardDisplay>();

        if (cardDisplay != null && cardDisplay.card != null)
        {
            Debug.Log("Tipo: " + cardDisplay.card.type);
            Debug.Log("Estadística: " + cardDisplay.card.stat);

            GameManager.instance.SelectCard(cardDisplay.card);
            GameManager.instance.AddPlayedCard(cardDisplay);
        }
    }
}
