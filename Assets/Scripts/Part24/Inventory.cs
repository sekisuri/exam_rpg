using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Inventory : MonoBehaviour
{
    private AudioManager theAudio;
    public string key_sound;
    public string enter_sound;
    public string cancel_sound;
    public string open_sound;
    public string beep_sound;

    private InventorySlot[] slots;

    private List<Item> inventoryItemList;
    private List<Item> inventoryTabList;

    public Text Description_Text;
    public string[] tabDescription;

    public Transform tf;

    public GameObject go;
    public GameObject[] selectedTabImages;

    private int selectedItem;
    private int selectedTab;

    private bool activated;
    private bool tabActivated;
    private bool itemActivated;
    private bool stopKeyInput;
    private bool preventExec;

    private WaitForSeconds waitTime = new WaitForSeconds(0.01f);

    private void Start()
    {
        theAudio = FindObjectOfType<AudioManager>();
        inventoryItemList = new List<Item>();
        inventoryTabList = new List<Item>();
        slots = tf.GetComponentsInChildren<InventorySlot>();
    }

    public void ShowTab()
    {
        RemoveSlot();
        SelectedTab();
    }

    public void RemoveSlot()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            slots[i].RemoveItem();
            slots[i].gameObject.SetActive(false);
        }
    }
    public void SelectedTab()
    {
        StopAllCoroutines();
        Color color = selectedTabImages[selectedTab].GetComponent<Image>().color;
        color.a = 0f;
        for(int i = 0; i < selectedTabImages.Length; i++)
        {
            selectedTabImages[i].GetComponent<Image>().color = color;
        }
        Description_Text.text = tabDescription[selectedTab];
        StartCoroutine(SelectedTabEffectCoroutine());
    }

    IEnumerator SelectedTabEffectCoroutine()
    {
        while (tabActivated)
        {
            Color color = selectedTabImages[selectedTab].GetComponent<Image>().color;
            while (color.a < 0.5f)
            {
                color.a += 0.03f;
                selectedTabImages[selectedTab].GetComponent<Image>().color = color;
                yield return waitTime;
            }
            while (color.a > 0f)
            {
                color.a -= 0.03f;
                selectedTabImages[selectedTab].GetComponent<Image>().color = color;
                yield return waitTime;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
    private void Update()
    {
        if (!stopKeyInput)
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                activated = !activated;
                if (activated)
                {
                    theAudio.Play(open_sound);
                    // theOrder.NotMove();
                    go.SetActive(true);
                    selectedTab = 0;
                    tabActivated = true;
                    itemActivated = false;
                    ShowTab();
                   // StartCoroutine(Sele)
                }
                else
                {
                    theAudio.Play(cancel_sound);
                    StopAllCoroutines();
                    go.SetActive(false);
                    tabActivated = false;
                    itemActivated = false;
                    // theOrder.Move();
                }
            }
        }

        if (activated)
        {
            if (tabActivated)
            {
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    if(selectedTab < selectedTabImages.Length - 1)
                    {
                        selectedTab++;

                    }
                    else
                    {
                        selectedTab = 0;
                    }
                    theAudio.Play(key_sound);
                    SelectedTab();
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    if(selectedTab > 0)
                    {
                        selectedTab--;
                    }
                    else
                    {
                        selectedTab = selectedTabImages.Length - 1;
                    }
                    theAudio.Play(key_sound);
                    SelectedTab();
                }
                else if (Input.GetKeyDown(KeyCode.Z))
                {
                    theAudio.Play(enter_sound);
                    Color color = selectedTabImages[selectedTab].GetComponent<Image>().color;
                    color.a = 0.25f;
                    selectedTabImages[selectedTab].GetComponent<Image>().color = color;
                    itemActivated = false;
                    tabActivated = false;
                    preventExec = true;
                    ShowItem();
                }

            }
        }
    }

    public void ShowItem()
    {
        inventoryTabList.Clear();
        RemoveSlot();
        selectedItem = 0;

        switch (selectedTab)
        {
            case 0:
                for(int i = 0;i < inventoryTabList.Count; i++)
                {
                    if(Item.ItemType.Use == inventoryTabList[i].itemType)
                    {
                        inventoryTabList.Add(inventoryTabList[i]);
                    }
                    
                }
                break;
            case 1:
                for(int i = 0; i < inventoryTabList.Count; i++)
                {
                    if(Item.ItemType.Equip == inventoryTabList[i].itemType)
                    {
                        inventoryTabList.Add(inventoryTabList[i]);
                    }
                }
                break;
            case 2:
                for(int i = 0;i < inventoryTabList.Count; i++)
                {
                    if(Item.ItemType.Quest == inventoryTabList[i].itemType)
                    {
                        inventoryTabList.Add(inventoryTabList[i]);
                    }
                }
                break;
            case 3:
                for (int i = 0; i < inventoryTabList.Count; i++)
                {
                    if (Item.ItemType.Quest == inventoryTabList[i].itemType)
                    {
                        inventoryTabList.Add(inventoryTabList[i]);
                    }
                }
                break;
        } // 탭에 따른 아이템 분류. 그것을 인벤토리 탭 리스트에 추가

        for(int i = 0; i < inventoryTabList.Count; i++)
        {
            slots[i].gameObject.SetActive(true);
            slots[i].Additem(inventoryTabList[i]);
        } //인벤토리 탭 리스트의 내용을 인벤토리에 추가

        SelectedItem();
    }

    public void SelectedItem()
    {
        StopAllCoroutines();
        if(inventoryTabList.Count > 0)
        {
            Color color = slots[0].selected_Item.GetComponent<Image>().color;
            Description_Text.text = inventoryTabList[selectedItem].itemDescription;
            color.a = 0f;
            for(int i = 0; i < inventoryItemList.Count; i++)
            {
                slots[i].selected_Item.GetComponent<Image>().color = color;
            }
            Description_Text.text = inventoryItemList[selectedItem].itemDescription;
            StartCoroutine(SelectedTabEffectCoroutine());
        }
        else
        {
            Description_Text.text = "해당 타입의 아이템을 소유하고 있지 않습니다.";
        }
    }

}
