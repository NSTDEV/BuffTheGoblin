using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using Unity.VisualScripting;

public class TargetDisplay : MonoBehaviour
{
    public Target target;

    public TMP_Text nameText, lifeText;
    public Image artworkImage;
    private Animator animator;

    void Start()
    {
        target.currentLife = target.maxLife;
        animator = artworkImage.GetComponent<Animator>();
        animator.SetBool("Life0", false);

        if (target != null)
        {
            PrintTarget();
        }
        else
        {
            Debug.LogError("Card is not assigned in the inspector");
        }
    }

    public void PrintTarget()
    {
        nameText.text = target.targetName.ToUpper();
        lifeText.text = target.currentLife.ToString().ToUpper();
        artworkImage.sprite = target.artwork;
    }

    public void TakeDamage(float damage)
    {
        target.currentLife -= damage;
        if (target.currentLife < 0) target.currentLife = 0; // Asegura que la vida no sea negativa

        PrintTarget();
        OutOfLife();
        Debug.Log("Vida total de " + target.targetName + ": " + target.currentLife);
    }

    public void Heal(float heal)
    {
        target.currentLife += heal;
        if (target.currentLife > target.maxLife) target.currentLife = target.maxLife; // Asegura que la vida no exceda el máximo

        PrintTarget();

        Debug.Log("Vida total de Buff: " + target.currentLife);
    }

    public void OutOfLife()
    {
        if (target.currentLife <= 0)
        {
            Debug.Log(target.targetName + " ha sido derrotado.");
            animator.SetBool("Life0", true);
            StartCoroutine(animationTarget(1.2f));
        }
    }

    IEnumerator animationTarget(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(artworkImage);
    }
}