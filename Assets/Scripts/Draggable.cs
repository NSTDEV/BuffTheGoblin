using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject placeHolder = null;

    public Transform parentToReturnTo = null;
    public Transform placeHolderParent = null;
    public enum Slot { Inventory, Chest, Merchant };

    public void OnBeginDrag(PointerEventData eventData)
    {
        placeHolder = new GameObject();
        placeHolder.transform.SetParent(transform.parent);

        RectTransform originalRect = GetComponent<RectTransform>();
        RectTransform placeholderRect = placeHolder.AddComponent<RectTransform>();
        placeholderRect.sizeDelta = originalRect.sizeDelta;

        placeHolder.transform.SetSiblingIndex(transform.GetSiblingIndex());

        parentToReturnTo = transform.parent;
        placeHolderParent = parentToReturnTo;
        transform.SetParent(transform.parent.parent);

        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;

        if (placeHolder.transform.parent != placeHolderParent)
            placeHolder.transform.SetParent(placeHolderParent);

        int newSiblingIndex = placeHolderParent.childCount;

        for (int i = 0; i < placeHolderParent.childCount; i++)
        {
            if (transform.position.x < placeHolderParent.GetChild(i).position.x)
            {
                newSiblingIndex = i;

                if (placeHolder.transform.GetSiblingIndex() < newSiblingIndex)
                    newSiblingIndex--;
                break;
            }
        }

        placeHolder.transform.SetSiblingIndex(newSiblingIndex);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentToReturnTo);
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        transform.SetSiblingIndex(placeHolder.transform.GetSiblingIndex());
        Destroy(placeHolder);
    }
}