using UnityEngine;

public class EC_Health : MonoBehaviour
{
    public int maxHealth;
    int currentHealth;

    // Components
    [SerializeField] Counter counter;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateCounter();
    }

    public void Damage(int value)
    {
        currentHealth -= value;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Kill();
        }
        UpdateCounter();
    }

    public void Heal(int value)
    {
        currentHealth += value;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        UpdateCounter();
    }

    void UpdateCounter()
    {
        if (counter == null) return;
        counter.SetText(currentHealth.ToString(), "Cards", 0);
    }

    public void Kill()
    {
        GetComponent<EC_Entity>().Remove();
    }
}