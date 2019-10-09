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

  private static int points;
  public static int Points
  {
    get
    {
      return points;
    }
    set
    {
      points = value;
      instance.pointText.text = string.Format("Points: {0}", value);
      if (points >= instance.winPoints)
      {
        instance.winPanel.SetActive(true);
      }
    }
  }

  public int winPoints = 2;

  public List<ResourceItem> itemList;
  public List<RecepyItem> recepyList;

  public ResultObject resultObject;
  public GameObject buttonOk;
  public List<DropObject> dropList;
  public TMPro.TMP_Text pointText;

  public GameObject startPanel;
  public GameObject resumeButton;
  public GameObject winPanel;

  internal readonly List<string> currentRecepy = new List<string>();

  private void Awake()
  {
    instance = this;
    resumeButton.SetActive(false);
    buttonOk.SetActive(false);
    startPanel.SetActive(true);
    winPanel.SetActive(false);
    Points = 0;
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

  public void AddItem(string item, int listId)
  {
    if (currentRecepy.Count <= listId)
      currentRecepy.Add(item);
    else
      currentRecepy[listId] = item;

    CheckRecepy();
  }

  public void RemoveItem(string item, int listId)
  {
    if (currentRecepy.Count >= listId)
      return;

    currentRecepy.RemoveAt(listId);

    //currentRecepy.Remove(item);
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

      List<string> checkList = new List<string>(recepy.resources);

      found = true;
      foreach (string itm in currentRecepy) {
        if (checkList.IndexOf(itm) < 0)
        {
          found = false;
          break;
        }
        checkList.Remove(itm);
       }

      if (found)
      {
        result = recepy;
        break;
      }
    }

    if (!found)
    {
      if (points > 0)
      {
        Points--;
      }

      return;
    }

    //TODO make new recepy item
    Debug.Log("Recepy found:" + result.result);

    ResourceItem rItem = itemList.Find((item) => item.name == result.result);
    if (rItem == null)
      return;

    resultObject.itemName = rItem.name;
    resultObject.picture.sprite = rItem.sprite;

    buttonOk.SetActive(true);
    Points++;

  }

  public void OnStartGame()
  {
    ClearRecepy();

    buttonOk.SetActive(false);
    Points = 0;

    winPanel.SetActive(false);
    startPanel.SetActive(false);
  }
  public void OnExitMainMenu()
  {
    resumeButton.SetActive(false);
    winPanel.SetActive(false);
    startPanel.SetActive(true);
  }
  public void OnExitMenu()
  {
    resumeButton.SetActive(true);

    startPanel.SetActive(true);
  }
  public void OnResumeGame()
  {
    startPanel.SetActive(false);
  }

  public void OnExitGame()
  {
    Application.Quit();
  }

}
