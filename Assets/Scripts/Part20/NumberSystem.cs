using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberSystem : MonoBehaviour
{
    private AudioManager theAudio;
    private string key_sound;
    private string enter_sound;
    private string cancel_sound;
    private string correct_sound;

    private int count;
    private int selectedTextBox;

    private int result;
    private int correntNumber;

    private GameObject superObject;
    public GameObject[] panel;
    public Text[] Number_Text;

    public Animator anim;
    public bool activated;
    private bool keyInput;
    private bool correctFlag;

    private void Start()
    {
        theAudio = FindObjectOfType<AudioManager>();
    }

    public void ShowNumber(int _correctNumber)
    {
        correntNumber = _correctNumber;
        activated = true;
        correctFlag = false;

        string temp = _correctNumber.ToString();
        for(int i = 0; i < temp.Length; i++)
        {
            count = i;
            panel[i].SetActive(true);
            Number_Text[i].text = "0";
        }

        superObject.transform.position = new Vector3(superObject.transform.position.x + 50 * count,
            superObject.transform.position.y,
            superObject.transform.position.z);
        selectedTextBox = 0;
        result = 0;
        anim.SetBool("Appear", true);
        keyInput = true;
    }


    public void SetNumber(string _arrow)
    {
        int temp = int.Parse(Number_Text[selectedTextBox].text);

        if(_arrow == "DOWN")
        {
            if(temp == 0)
            {
                temp = 9;
            }
            else
            {
                temp--;
            }
        }
        else if(_arrow == "UP")
        {
            if(temp == 9)
            {
                temp = 0;
            }
            else
            {
                temp++;
            }
            Number_Text[selectedTextBox].text = temp.ToString();
        }
    }
    private void Update()
    {
        if (keyInput)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                theAudio.Play(key_sound);
                SetNumber("DOWN");
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                theAudio.Play(key_sound);
                SetNumber("UP");
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                theAudio.Play(key_sound);

                if(selectedTextBox < count)
                {
                    selectedTextBox++;
                }
                else
                {
                    selectedTextBox = 0;
                }
                SetColor();
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                theAudio.Play(key_sound);
                if (selectedTextBox > count)
                {
                    selectedTextBox--;
                }
                else
                {
                    selectedTextBox = count;
                }
                SetColor();
            }
            else if (Input.GetKeyDown(KeyCode.Z))
            {
                theAudio.Play(key_sound);
                keyInput = false;
                StartCoroutine(OXCoroutine());
            }
            else if (Input.GetKeyDown(KeyCode.X))
            {
                theAudio.Play(key_sound);
                keyInput = false;
                StartCoroutine(ExitCoroutine());
            }
        }
    }

    IEnumerator OXCoroutine()
    {
        Color color = Number_Text[0].color;
        color.a = 1f;
        for(int i = count; i >= 0; i--)
        {
            Number_Text[i].color = color;
            tempNumber += Number_Text[i].text;
            
        }
    }
    IEnumerator ExitCoroutine()
    {

    }
    public void SetColor()
    {
        Color color = Number_Text[0].color;
        color.a = 0.3f;
        for(int i = 0; i <= count; i++)
        {
            Number_Text[i].color = color;
        }
        color.a = 1f;
        Number_Text[selectedTextBox].color = color;
    }

}
