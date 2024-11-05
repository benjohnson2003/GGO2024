using UnityEngine;
using UnityEngine.UI;

public class UIAnimator : MonoBehaviour
{
    [SerializeField] UIAnimation open;
    [SerializeField] UIAnimation closed;
    UIAnimation currentAnimation;

    // Components
    RectTransform tr;
    Image image;

    void Start()
    {
        tr = GetComponent<RectTransform>();
        image = GetComponent<Image>();

        currentAnimation = open;
        tr.anchoredPosition = currentAnimation.position;
        image.color = currentAnimation.color;
    }

    void Update()
    {
        if (currentAnimation == null)
            return;

        tr.anchoredPosition = Vector2.Lerp(tr.anchoredPosition, currentAnimation.position, Time.deltaTime * currentAnimation.speed);
        image.color = Color.Lerp(image.color, currentAnimation.color, Time.deltaTime * currentAnimation.speed);
    }

    public void Open(bool instant = false)
    {
        currentAnimation = open;

        if (instant)
            ResetPosition();
    }

    public void Close(bool instant = false)
    {
        currentAnimation = closed;

        if (instant)
            ResetPosition();
    }

    void ResetPosition()
    {
        tr.anchoredPosition = currentAnimation.position;
        image.color = currentAnimation.color;
    }
}

[System.Serializable]
public class UIAnimation
{
    public Vector2 position;
    public Color color = Color.black;
    public float speed = 10;
}
