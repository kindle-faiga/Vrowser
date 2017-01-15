using UnityEngine;
using System.Collections;

public class TutorialControl : MonoBehaviour
{
    [SerializeField]
    private GameObject[] pages;
    private int count = 0;
    private bool isStart = false;

    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isStart)
            {
                isStart = true;
                StartCoroutine(WaitForChange());
            }
        }
    }

    IEnumerator WaitForChange()
    {
        pages[count].SetActive(false);

        if (count < 6)
        {
            ++count;
        }
        else
        {
            count = 1;
        }

        pages[count].SetActive(true);

        yield return new WaitForSeconds(30.0f);

        StartCoroutine(WaitForChange());
    }
}
