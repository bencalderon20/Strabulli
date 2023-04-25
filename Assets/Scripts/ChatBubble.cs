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
        Setup("Once upon a time, in a land far away from here.");
        yield return new WaitForSeconds(2f);
        Setup("There was a small jelly bean named Strabulli");
        yield return new WaitForSeconds(2f);
        Setup("He was enjoying life and drinking from a lemonade stand");
        yield return new WaitForSeconds(2.5f);
        Setup("All of a sudden, he was attacked by an evil tomato named Tometi who was promoting his brand of tomato juice");
        yield return new WaitForSeconds(5f);
        Setup("In his darkest moment, Strabulli met a wise mentor in the form of a rat");
        yield return new WaitForSeconds(4f);
        Setup("He was encouraged to get revenge, and thus began this epic adventure");
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
