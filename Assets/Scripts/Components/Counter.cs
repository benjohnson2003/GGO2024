using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour
{
    [SerializeField] List<Sprite> characters = new List<Sprite>();
    [SerializeField] GameObject numberPrefab;
    [SerializeField] float spacing;

    public void SetText(string text, string sortingLayer, int color = 0)
    {
        // Clear numbers
        foreach (Transform child in transform)
            Destroy(child.gameObject);

        // Create new numbers
        for (int i = 0; i < text.Length; i++)
        {
            string str = "numbers_" + color + text[i];

            GameObject number = Instantiate(numberPrefab, transform);
            number.transform.localPosition = new Vector3(i * spacing, 0, 0);
            SpriteRenderer sr = number.GetComponent<SpriteRenderer>();
            sr.sprite = CharToSprite(str);
            sr.sortingLayerName = sortingLayer;
        }
    }

    private Sprite CharToSprite(string str)
    {
        for (int i = 0; i < characters.Count; i++)
        {
            if (characters[i].name == str)
                return characters[i];
        }
        return null;
    }
}
