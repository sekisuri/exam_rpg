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
                }

            }
        }
    }

}
