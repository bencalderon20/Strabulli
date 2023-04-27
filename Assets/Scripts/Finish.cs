using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    [SerializeField] private AudioSource finishSound;

    private GameObject[] music;
    // Start is called before the first frame update
    private void Start()
    {
        finishSound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            StartCoroutine(ExecuteAfterPlay());      
        }
    }
    IEnumerator ExecuteAfterPlay()
    {
        finishSound.Play();
        yield return new WaitWhile (()=> finishSound.isPlaying);
        CompleteLevel();
    }

    private void CompleteLevel()
    {
        music = GameObject.FindGameObjectsWithTag("GameMusic");
        Destroy(music[0]);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
