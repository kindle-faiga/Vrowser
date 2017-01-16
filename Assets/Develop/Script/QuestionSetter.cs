using UnityEngine;
using System.Collections;

public class QuestionSetter : MonoBehaviour
{
    [SerializeField]
    private GameObject[] pages;
    private int count = 0;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            pages[count].SetActive(false);

            if (count < 5)
            {
                ++count;
            }
            else
            {
                count = 0;
            }

            pages[count].SetActive(true);
        }
    }
}
