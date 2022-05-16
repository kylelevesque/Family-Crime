using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypewriterEffect : MonoBehaviour
{
    [SerializeField] private float typewriterSpeed = 50f;

    public Coroutine Run(string text, TMP_Text label)
    {
        return StartCoroutine(TypeText(text, label));
    }

    IEnumerator TypeText(string text, TMP_Text label)
    {
        float t = 0;
        int charIndex = 0;

        while (charIndex < text.Length)
        {
            t += Time.deltaTime * typewriterSpeed;
            charIndex = Mathf.FloorToInt(t);
            charIndex = Mathf.Clamp(charIndex, 0, text.Length);

            label.text = text.Substring(0, charIndex);

            yield return null;
        }
        label.text = text;
    }
}
