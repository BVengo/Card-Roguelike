using UnityEngine;
using UnityEngine.UI;

public class CostDisplay : MonoBehaviour
{
    [SerializeField]
    Image image;

    [SerializeField]
    Text count;

    [SerializeField]
    string countFormat = "x {0}";

    public void Set(Sprite image, int count)
    {
        this.image.sprite = image;

        this.count.text = string.Format(countFormat, count);
    }
}
