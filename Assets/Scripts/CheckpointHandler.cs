using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Trimitem poziția ACESTUI checkpoint către Manager
            CheckPointManager.instance.UpdateCheckpoint(transform.position);
            Debug.Log("Checkpoint activat la: " + transform.position);
        }
    }
}