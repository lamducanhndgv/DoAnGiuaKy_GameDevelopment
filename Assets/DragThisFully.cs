using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragThisFully : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	public static GameObject itemBeingDragged;
	Vector3 startPosition;
	Transform startParent;
	private RectTransform rectTransform;

    private void Awake()
    {
		Debug.Log("Begin drag");

		rectTransform = GetComponent<RectTransform>();
    }
    #region IBeginDragHandler implementation

    public void OnBeginDrag(PointerEventData eventData)
	{
		Debug.Log("Begin drag");
		itemBeingDragged = gameObject;
		startPosition = transform.position;
		startParent = transform.parent;
		GetComponent<CanvasGroup>().blocksRaycasts = false;
	}

	#endregion

	#region IDragHandler implementation

	public void OnDrag(PointerEventData eventData)
	{
		Debug.Log("On drag");

		transform.position = eventData.position;
	}

	#endregion

	#region IEndDragHandler implementation

	public void OnEndDrag(PointerEventData eventData)
	{
		Debug.Log("End drag");

		itemBeingDragged = null;
		GetComponent<CanvasGroup>().blocksRaycasts = true;
		if (transform.parent == startParent)
		{
			transform.position = startPosition;
		}
	}

	#endregion
}
