using UnityEngine;

public class Target : MonoBehaviour
{
    public string targetName;
    public float maxLife = 100f;
    public float currentLife;

    void Start()
    {
        currentLife = maxLife;
    }

    public void TakeDamage(float damage)
    {
        currentLife -= damage;

        if (currentLife <= 0)
        {
            Destroy(gameObject);
        }

        Debug.Log("Vida total del " + targetName + " enemigo: " + currentLife);
    }

    public void Heal(float heal)
    {
        if (currentLife >= maxLife)
        {
            currentLife += 0;
        }
        else
        {
            currentLife += heal;
        }

        Debug.Log("Vida total de Buff: " + currentLife);
    }
}