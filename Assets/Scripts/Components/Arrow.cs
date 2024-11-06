using UnityEngine;

public class Arrow : MonoBehaviour
{
    // Components
    SpriteRenderer _sr;

    void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
        _sr.enabled = false;
    }

    public void SetRotation(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            SetVisibility(true);
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        else
        {
            SetVisibility(false);
        }
    }
    
    void SetVisibility(bool visible)
    {
        _sr.enabled = visible;
    }
}
