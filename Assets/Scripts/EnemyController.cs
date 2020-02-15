using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Animator animator;
    private CharacterController characterController;

    public int MaxHealth = 10;

    private int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = MaxHealth;
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("Die");
        characterController.enabled = false;
        animator.SetBool("Die", true);
    }
}
