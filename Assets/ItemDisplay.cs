using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDisplay : MonoBehaviour
{
    [SerializeField]
    Text text;

    [SerializeField]
    Image image;

    public void Set(Item item, int count)
    {
        image.sprite = item.Sprite;
        text.text = count.ToString();

        gameObject.SetActive(true);
    }

    public void Clear()
    {
        gameObject.SetActive(false);
    }
}
