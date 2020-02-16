using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int startHealth = 10;
    [SerializeField]
    private int currentHealth = 10;
    [SerializeField]
    private int maxHealth = 10;

    public void DecreaseHealth(int amount)
    {
        currentHealth -= amount;
        Debug.Log($"{gameObject.name} health now {currentHealth}");
    }
}
