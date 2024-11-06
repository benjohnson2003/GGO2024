using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Kicker : MonoBehaviour
{
    [Header("Kick Settings")]
    public LayerMask kickMask;
    public float kickCheckRange; // Radius to check for kickable
    public float kickForce; // Kick force velocity
    public float kickCooldown; // Time between kicks
    [Range(0, 1)] public float kickTimeScale; // Time scale when kicking
    public float kickTimeDelta;
    public float pullForce;
    [Range(0, 1)] public float kickVelocityPreserve;

    // Variables
    List<Collider2D> _activeKickables;
    bool _kicking;
    float _timeSinceKick;
    float _targetTimeScale;

    // Input Variables
    public bool KickWasPressed { get { return _kickWasPressed; } set { _kickWasPressed = value; } }
    bool _kickWasPressed;
    public bool KickIsPressed { get { return _kickIsPressed; } set { _kickIsPressed = value; } }
    bool _kickIsPressed;
    public Vector2 AimDirection { get { return _aimDirection; } set { _aimDirection = value; } }
    Vector2 _aimDirection;

    void Start()
    {
        _activeKickables = new List<Collider2D>();
        _targetTimeScale = 1;
    }

    void Update()
    {
        // Get kickables in range
        List<Collider2D> _kickablesInRange = Physics2D.OverlapCircleAll(transform.position, kickCheckRange, kickMask).ToList();

        // Start kick?
        if (_kickIsPressed && !_kicking && _kickablesInRange.Count > 0 && _timeSinceKick >= kickCooldown)
        {
            // Start kick
            _kicking = true;
            _targetTimeScale = kickTimeScale;
        }

        if (_kicking)
        {
            List<Collider2D> _kickablesToAdd = new List<Collider2D>();
            List<Collider2D> _kickablesToRemove = new List<Collider2D>();

            // Add kickables?
            foreach (Collider2D _kickable in _kickablesInRange)
            {
                if (!_activeKickables.Contains(_kickable))
                    _kickablesToAdd.Add(_kickable);
            }
            foreach (Collider2D _kickable in _kickablesToAdd)
                AddActiveKickable(_kickable);
            _kickablesToAdd.Clear();
            // Remove kickables?
            foreach (Collider2D _kickable in _activeKickables)
            {
                if (!_kickablesInRange.Contains(_kickable))
                    _kickablesToRemove.Add(_kickable);
            }
            foreach (Collider2D _kickable in _kickablesToRemove)
                RemoveActiveKickable(_kickable);
            _kickablesToRemove.Clear();

            // Update active kickables
            foreach (Collider2D _kickable in _activeKickables)
            {
                _kickable.GetComponentInChildren<Arrow>().SetRotation(_aimDirection);
                Vector2 _force = (transform.position - _kickable.transform.position) * pullForce;
                _kickable.GetComponent<Rigidbody2D>().AddForce(_force);
            }

            // End kick?
            if (!_kickIsPressed || _activeKickables.Count <= 0)
            {
                _kicking = false;
                _timeSinceKick = 0;
                _targetTimeScale = 1;

                // Kick all acive kickables, if any
                foreach (Collider2D _kickable in _activeKickables)
                {
                    Kick(_kickable);
                    _kickablesToRemove.Add(_kickable);
                }
                foreach (Collider2D _kickable in _kickablesToRemove)
                    RemoveActiveKickable(_kickable);
                _kickablesToRemove.Clear();
            }
        }

        // Timer
        if (!_kicking)
            _timeSinceKick += Time.deltaTime;

        // Time scale
        Time.timeScale = Mathf.Lerp(Time.timeScale, _targetTimeScale, kickTimeDelta * Time.unscaledDeltaTime);
    }

    #region KickableManagement

    void AddActiveKickable(Collider2D _kickable)
    {
        _activeKickables.Add(_kickable);
    }

    void RemoveActiveKickable(Collider2D _kickable)
    {
        _activeKickables.Remove(_kickable);
        _kickable.GetComponentInChildren<Arrow>().SetRotation(Vector2.zero);
    }

    void Kick(Collider2D _kickable)
    {
        if (!_kickable) return;

        Rigidbody2D _krb = _kickable.GetComponent<Rigidbody2D>();
        _krb.velocity = _aimDirection.normalized * kickForce + _krb.velocity * kickVelocityPreserve;
        _krb.angularVelocity = Random.Range(120.0f, 180.0f) * Utilities.RandomSign();
    }

    #endregion
}