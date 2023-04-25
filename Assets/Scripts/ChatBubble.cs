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
        
        Setup("Press S To Speak To Squeakiams");
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.S))
        {
            StartCoroutine(UpdateText());
        }
    }

    IEnumerator UpdateText()
    {  
        Setup("Are you ok?");
        yield return new WaitForSeconds(2f);
        Setup("That clown Tometi really did a number on you.");
        yield return new WaitForSeconds(2f);
        Setup("That coward is hiding out at the end of the circus.");
        yield return new WaitForSeconds(2.5f);
        Setup("Why don't you go get him and teach him a lesson he'll never forget.");
        yield return new WaitForSeconds(5f);
        

    }

    private void Setup(string text) {
        textMeshPro.SetText(text);
        textMeshPro.ForceMeshUpdate();
        Vector2 textSize = textMeshPro.GetRenderedValues(false);
        
        Vector2 padding = new Vector2(4f, 2f);
        backgroundSpriteRenderer.size = textSize + padding;
    }
}
