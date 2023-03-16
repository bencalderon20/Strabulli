using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformManager : MonoBehaviour
{
    public static platformManager Instance = null;

    [SerializeField] GameObject platformPrefab;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(platformPrefab, new Vector2(-3.5f,;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
