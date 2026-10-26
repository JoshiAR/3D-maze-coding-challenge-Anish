using UnityEngine;

public class CoinCollect : MonoBehaviour
{
    public float rotationSpeed = 100f;

    void Update()
    {
        transform.Rotate(0,rotationSpeed*Time.deltaTime,0,Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CoinManager.instance.AddCoin();

            Destroy(gameObject);
        }
    }
}