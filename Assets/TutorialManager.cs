using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject [] popUps;
    private int popUpIndex;

    private void Update()
    {
        for (int i = 0; i < popUps.Length; i++)
        {
            if (i == popUpIndex)
            {
                popUps[popUpIndex].SetActive(true);
            } else
            {
                popUps[i].SetActive(false);
            }
        }
        if ( popUpIndex == 0 )
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
            {
                popUpIndex++;
            }
            else if (popUpIndex == 1)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    popUpIndex++;
                }
            }   
        }
    }
}
