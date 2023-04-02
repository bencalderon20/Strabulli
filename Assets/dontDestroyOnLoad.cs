using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dontDestroyOnLoad : MonoBehaviour
{
    private GameObject[] music;

    // Start is called before the first frame update
    void Start()
    {
        music = GameObject.FindGameObjectsWithTag("GameMusic");
        Destroy(music[1]);
    }

    // Update is called once per frame
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}
