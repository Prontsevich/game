using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Animator animator;
    private CharacterController characterController;
    private HealthController healthController;

    private void Awake()
    {
        healthController = GetComponent<HealthController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    public void TakeDamage(int damage)
    {
        healthController.TakeDamage(damage);
    }

    private void OnEnable()
    {
        healthController.onDie += Die;
    }

    private void OnDisable()
    {
        healthController.onDie -= Die;
    }

    private void Die()
    {
        Debug.Log("Die");
        characterController.enabled = false;
        animator.SetBool("Die", true);
    }
}
