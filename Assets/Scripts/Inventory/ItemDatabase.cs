using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using System.IO;

public class ItemDatabase : MonoBehaviour 
{
    private List<Item> database = new List<Item>();
    private JsonData itemData;

	// Use this for initialization
    /*
	void Start () 
    {
        itemData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + 
                                       "/StreamingAssets/Items.json"));
        ConstructItemDatabase();

        //Debug.Log(FetchItemByID(0).Description);
	}
    */   

    void Awake()
    {
        itemData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + 
            "/StreamingAssets/Items.json"));
        ConstructItemDatabase();
    }
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public Item FetchItemByID(int id)
    {
        for (int i = 0; i < database.Count; i++)
        {
            if (database[i].ID == id)
            {
                return database[i];
            }
        }

        return null;
    }

    void ConstructItemDatabase()
    {
        for (int i = 0; i < itemData.Count; i++)
        {
            database.Add(new Item((int)itemData[i]["id"], 
                         itemData[i]["name"].ToString(), 
                         (int)itemData[i]["value"], 
                         (int)itemData[i]["stats"]["power"],
                         (int)itemData[i]["stats"]["defence"], 
                         (int)itemData[i]["stats"]["vitality"],
                         itemData[i]["description"].ToString(),
                         (bool)itemData[i]["stackable"],
                         (int)itemData[i]["rarity"]));
        }
    }
}
