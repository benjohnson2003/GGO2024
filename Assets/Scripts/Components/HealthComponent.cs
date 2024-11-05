using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [HideInInspector] public int health { get; private set; }
    [HideInInspector] public int maxHealth { get; private set; }

    public void SetMaxHealth(int maxHealth, bool heal = true)
    {
        this.maxHealth = maxHealth;
        if (heal)
            Heal(maxHealth - health);
    }

    public void Heal(int value)
    {
        if (value <= 0)
            return;
        health += value;
        if (health > maxHealth)
            health = maxHealth;
    }

    public void Damage(int value)
    {
        if (value <= 0)
            return;
        health -= value;
        if (health < 0)
        {
            health = 0;
            Kill();
        }
    }

    public void Kill()
    {
        gameObject.SetActive(false);
    }
}
