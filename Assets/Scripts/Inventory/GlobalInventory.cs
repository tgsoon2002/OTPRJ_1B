using UnityEngine;
using System.Collections;

using System.Collections.Generic;
using LitJson;
using System.IO;

public class GlobalInventory : MonoBehaviour
{
    private List<GlobalItem> globalItemList = new List<GlobalItem>();
    // private List<GlobalItem> expeditionItemList = new List<GlobalItem>();
    private JsonData globalItemData;

    public List<GlobalItem> _GlobalItemList
    {
        get { return globalItemList; }

    }

    void Awake()
    {
        globalItemData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath +
            "/StreamingAssets/GlobalInventory.json"));

        ConstructGlobalInventory();
        //Debug.Log(globalItemList.Count);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public GlobalItem FetchGlobalItemByID(int id)
    {
        for (int i = 0; i < globalItemList.Count; i++)
        {
            if (globalItemList[i].ID == id)
            {
                return globalItemList[i];
            }
        }

        return null;
    }

    public void ConstructGlobalInventory()
    {
        for (int i = 0; i < globalItemData.Count; i++)
        {
            globalItemList.Add(new GlobalItem((int)globalItemData[i]["id"],
                (int)globalItemData[i]["quantity"]));
        }
    }
}


