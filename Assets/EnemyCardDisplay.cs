using UnityEngine;
using UnityEngine.UI;

public class EnemyCardDisplay : CardDisplay
{
    [SerializeField]
    Image image;

    [SerializeField]
    GameObject toolBubble;

    [SerializeField]
    Image toolImage;

    [SerializeField]
    Text health;

    [SerializeField]
    Text damage;

    [SerializeField]
    Color silhouetteColor = Color.black;

    public void Set(EnemyCard card, EnemyCardData data)
    {
        image.sprite = card.Sprite;
        health.text = data.Health.ToString();
        damage.text = data.Damage.ToString();

        if (data.Modifier != null)
        {
            toolBubble.SetActive(true);

            toolImage.sprite = data.Modifier.Tool.Sprite;

            toolImage.color = Color.white;
        }
        else if (card.ToolType == null)
        {
            toolBubble.SetActive(false);
        }
        else
        {
            toolBubble.SetActive(true);

            toolImage.sprite = card.ToolType.Silhouette;

            toolImage.color = silhouetteColor;
        }
    }

    public override void Refresh(CardInstance instance)
    {
        base.Refresh(instance);

        Set(instance.Definition as EnemyCard, instance.Data as EnemyCardData);
    }
}
