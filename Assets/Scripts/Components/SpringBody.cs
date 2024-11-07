using UnityEngine;

public class SpringBody : MonoBehaviour
{
    [Header("Spring Settings")]
    public float leanMultiplyer;
    public float maxLean;
    public float leanLerp;

    // Components
    Rigidbody2D _rb;

    void Start()
    {
        _rb = GetComponentInParent<Rigidbody2D>();
    }

    void Update()
    {
        float _targetLean = Mathf.Abs(_rb.velocity.x) * leanMultiplyer;
        transform.localRotation = Quaternion.Euler(0, 0, Mathf.Lerp(transform.localRotation.z, _targetLean, leanLerp * Time.deltaTime));
    }
}
