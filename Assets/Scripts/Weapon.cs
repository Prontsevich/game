using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int damage = 10;

    private BoxCollider collider;

    public void TurnOnAttack()
    {
        collider.enabled = true;
    }

    public void TurnOffAttack()
    {
        collider.enabled = false;
    }

    private void Start()
    {
        collider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            EnemyController ec = other.gameObject.GetComponent<EnemyController>();
            ec.TakeDamage(damage);
        }
    }
}
