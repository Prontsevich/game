using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private HealthController healthController;

    // Start is called before the first frame update
    void Start()
    {
        healthController = GetComponent<HealthController>();
    }

    // Update is called once per frame
    void Update()
    {

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

    }
}
