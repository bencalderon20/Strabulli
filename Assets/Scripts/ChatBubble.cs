using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChatBubble : MonoBehaviour
{
    private SpriteRenderer backgroundSpriteRenderer;
    private SpriteRenderer iconSpriteRenderer;
    private TextMeshPro textMeshPro;

    private void Awake() {
        backgroundSpriteRenderer = transform.Find("Background").GetComponent<SpriteRenderer>();
        iconSpriteRenderer = transform.Find("Icon").GetComponent<SpriteRenderer>();
        textMeshPro = transform.Find("Text").GetComponent<TextMeshPro>();

    }

    void Start() {
        
        Setup("Press S To Read The Story");
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.S))
        {
            StartCoroutine(UpdateText());
        }
    }

    IEnumerator UpdateText()
    {  
        Setup("Long, Long Ago");
        yield return new WaitForSeconds(2f);
        Setup("There was once a man named steve");
        yield return new WaitForSeconds(2f);
        Setup("He enjoyed pizza pie");
        yield return new WaitForSeconds(2f);
    }

    private void Setup(string text) {
        textMeshPro.SetText(text);
        textMeshPro.ForceMeshUpdate();
        Vector2 textSize = textMeshPro.GetRenderedValues(false);
        
        Vector2 padding = new Vector2(4f, 2f);
        backgroundSpriteRenderer.size = textSize + padding;
    }
}
