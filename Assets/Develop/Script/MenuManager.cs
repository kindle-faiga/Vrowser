using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuManager : MonoBehaviour 
{
    [SerializeField]
    private Transform cameraPos;
    [SerializeField]
    private Transform defaultPos;
    [SerializeField]
    private Transform[] buttons;
    [SerializeField]
    private Transform[] movePoses;
    private Vector3[] defaultSize;
    private bool isDisplay = false;

    void Start ()
    {
        //defaultPos = GetComponent<RectTransform>();

        //movePoses = new Transform[buttons.Length];

        defaultSize = new Vector3[buttons.Length];

        for (int i = 0; i < buttons.Length; ++i)
        {
            //movePoses[i] = buttons[i].position;

            buttons[i].position = defaultPos.position;

            defaultSize[i] = buttons[i].localScale;

            buttons[i].localScale = defaultSize[i] * 0.01f;
        }
    }

    public void Display()
    {
        isDisplay = true;

        for (int i = 0; i < buttons.Length; ++i)
        {
            iTween.MoveTo(buttons[i].gameObject, movePoses[i].position, 0.25f);
            iTween.ScaleTo(buttons[i].gameObject, defaultSize[i], 0.5f);
        }
    }

    public void InDisplay()
    {
        isDisplay = false;

        for (int i = 0; i < buttons.Length; ++i)
        {
            iTween.MoveTo(buttons[i].gameObject, defaultPos.position, 0.25f);
            iTween.ScaleTo(buttons[i].gameObject, defaultSize[i] * 0.01f, 0.5f);
        }
    }

    void Update ()
    {
	    if(Input.GetKeyDown("space"))
        {
            if(isDisplay)
            {
                InDisplay();
            }
            else
            {
                Display();
            }
        }
	}

    void FixedUpdate()
    {
        float dis = Vector3.Distance(cameraPos.position, transform.position);
        Debug.Log(dis);
        if(dis < 0.5f)
        {
            if(!isDisplay)
            {
                Display();
            }
        }
        else
        {
            if (isDisplay)
            {
                InDisplay();
            }
        }
    }
}
