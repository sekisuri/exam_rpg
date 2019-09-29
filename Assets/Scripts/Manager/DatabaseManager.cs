using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{

    static public DatabaseManager instance;
    public string[] var_name;
    public float[] var_f;

    public string[] switch_name;
    public bool[] switches;

    public List<Item> itemList = new List<Item>();
    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
    }

    private void Start()
    {
        itemList.Add(new Item(10001, "빡간 포션", "체력을 50 채워주는 마법의 물약", Item.ItemType.Use));
    }

}
