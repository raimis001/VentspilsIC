using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainCanvas : MonoBehaviour
{
  public RectTransform dragIcon;

  public Transform content;
  public DragObject dragPrefab;

  private DragObject currentDrag;
  public void OnBeginDrag(DragObject target)
  {
    currentDrag = target;

    Image icon = dragIcon.GetComponent<Image>();
    icon.sprite = currentDrag.picture.sprite;

    dragIcon.gameObject.SetActive(true);
  }
  public void OnEndDrag()
  {
    currentDrag = null;
    dragIcon.gameObject.SetActive(false);
  }
  public void OnDrag()
  {
    dragIcon.position = Input.mousePosition;
  }

  public void OnDrop(DropObject target, int listID)
  {
    target.itemName = currentDrag.itemName;
    target.sprite = currentDrag.picture.sprite;
    GameManager.instance.AddItem(currentDrag.itemName, listID);
  }

  public void OnOkButton()
  {
    GameManager.instance.buttonOk.SetActive(false);

    DragObject obj = Instantiate(dragPrefab, content);

    obj.picture.sprite = GameManager.instance.resultObject.picture.sprite;
    obj.itemName = GameManager.instance.resultObject.itemName;

    GameManager.instance.ClearRecepy();
  }
}
