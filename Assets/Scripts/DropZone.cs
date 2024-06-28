using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Draggable.Slot typeOfItem = Draggable.Slot.Inventory;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
            return;

        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
        if (d != null)
        {
            d.placeHolderParent = transform;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
            return;

        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
        if (d != null && d.placeHolderParent == transform)
        {
            d.placeHolderParent = d.parentToReturnTo;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();

        if (d != null)
        {
            d.parentToReturnTo = transform;

            // Obtener el componente CardDisplay de la carta
            CardDisplay cardDisplay = d.GetComponent<CardDisplay>();
            if (cardDisplay != null && cardDisplay.card != null)
            {
                // Aquí deberías asegurarte de que cardDisplay.card sea del tipo correcto
                Debug.Log("Carta " + cardDisplay.card.name + " soltada en: " + gameObject.name);
                GameManager.instance.AddPlayedCard(cardDisplay);
            }
            else
            {
                Debug.LogWarning("No se encontró CardDisplay o la carta no está asignada correctamente.");
            }
        }
    }
}