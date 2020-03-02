using UnityEngine;

public class Health : MonoBehaviour, IHealth
{
    [SerializeField]
    private int startHealth = 10;
    //[SerializeField]
    //private int currentHealth = 10;
    [SerializeField]
    private int maxHealth = 10;
    [SerializeField]
    private int minHealth = 0;

    public int CurrentHealth
    {
        get;
        private set;
    }

    public int MaxHealth
    {
        get { return maxHealth; }
    }

    public int MinHealth
    {
        get { return minHealth; }
    }

    void Start()
    {
        Reset();
    }

    public bool DecreaseHealth(int amount)
    {
        CurrentHealth = Mathf.Max(CurrentHealth - amount, MinHealth);
        //currentHealth -= amount;
        Debug.Log($"{gameObject.name} health now {CurrentHealth}");

        return CurrentHealth <= MinHealth;
    }

    public void IncreaseHealth(int amount)
    {
        CurrentHealth = Mathf.Min(CurrentHealth + amount, MaxHealth);
    }

    public void Reset()
    {
        //CurrentHealth = Mathf.Clamp(startHealth, MinHealth, MaxHealth);
        CurrentHealth = startHealth;
    }
}
