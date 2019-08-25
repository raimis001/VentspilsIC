using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ResourceItem
{
  public string name;
  public Sprite sprite;
}

[System.Serializable]
public class RecepyItem
{
  public List<string> resources;
  public string result;
}

public class GameManager : MonoBehaviour
{
  public static GameManager instance;

  public List<ResourceItem> itemList;
  public List<RecepyItem> recepyList;

  public ResultObject resultObject;
  public GameObject buttonOk;
  public List<DropObject> dropList;

  internal readonly List<string> currentRecepy = new List<string>();

  private void Awake()
  {
    instance = this;
    buttonOk.SetActive(false);
  }

  public void ClearRecepy()
  {
    currentRecepy.Clear();

    foreach (DropObject obj in dropList)
    {
      obj.picture.sprite = null;
    }

    resultObject.picture.sprite = null;
    resultObject.itemName = "";
  }

  public void AddItem(string item)
  {
    currentRecepy.Add(item);
    Debug.Log("Add: " + item);
    CheckRecepy();
  }

  public void RemoveItem(string item)
  {
    currentRecepy.Remove(item);
  }


  public void CheckRecepy()
  {
    if (currentRecepy.Count < 2)
      return;

    bool found = true;
    RecepyItem result = null;
    foreach (RecepyItem recepy in recepyList)
    {
      if (recepy.resources.Count != currentRecepy.Count)
        return;

      found = true;
      foreach (string itm in currentRecepy) {
        if (recepy.resources.IndexOf(itm) < 0)
        {
          found = false;
          break;
        }
       }

      if (found)
      {
        result = recepy;
        break;
      }
    }

    if (!found)
      return;

    //TODO make new recepy item
    Debug.Log("Recepy found:" + result.result);

    ResourceItem rItem = itemList.Find((item) => item.name == result.result);
    if (rItem == null)
      return;

    resultObject.itemName = rItem.name;
    resultObject.picture.sprite = rItem.sprite;

    buttonOk.SetActive(true);

  }

}
