using UnityEngine;

public class Kicker : MonoBehaviour
{
    [Header("Kick Settings")]
    public LayerMask kickMask;
    public float kickCheckRange; // Radius to check for kickable
    public float kickForce; // Kick force velocity
    public float kickCooldown; // Time between kicks
    public float kickTimeSpeed = 1; // Kick slow time multiplyer
    public float kickTimeDeltaSpeed;

    // Variables
    bool _kicking;
    float _timeSinceKick;

    // Input Variables
    public bool KickWasPressed { get { return _kickWasPressed; } set { _kickWasPressed = value; } }
    bool _kickWasPressed;
    public bool KickIsPressed { get { return _kickIsPressed; } set { _kickIsPressed = value; } }
    bool _kickIsPressed;
    public Vector2 AimDirection { get { return _aimDirection; } set { _aimDirection = value; } }
    Vector2 _aimDirection;

    void Update()
    {
        // Get kickable if in range
        Collider2D _kickable = Physics2D.OverlapCircle(transform.position, kickCheckRange, kickMask);

        // Start kick?
        if (_kickIsPressed && _kickable && !_kicking && _timeSinceKick >= kickCooldown)
        {
            _kicking = true;
        }
        // Keep kicking?
        else if (_kickIsPressed && _kickable && _kicking)
        {
            
        }
        // End kick?
        else if (!_kickIsPressed && _kicking)
        {
            if (_kickable)
                Kick(_kickable);
            _kicking = false;
            _timeSinceKick = 0;
        }

        // Timer
        if (!_kicking)
            _timeSinceKick += Time.deltaTime;

        // Time speed
        Time.timeScale = Mathf.Lerp(Time.timeScale, _kicking ? kickTimeSpeed : 1, kickTimeDeltaSpeed * Time.deltaTime);
    }

    void Kick(Collider2D _kickable)
    {
        if (!_kickable)
            return;

        Rigidbody2D _krb = _kickable.GetComponent<Rigidbody2D>();
        _krb.velocity = _aimDirection.normalized * kickForce + _krb.velocity * 0.2f;
        _krb.angularVelocity = Random.Range(120.0f, 180.0f) * Utilities.RandomSign();
    }
}