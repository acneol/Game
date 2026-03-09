using UnityEngine;

public class KillerScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Verificam daca managerul exista in scena
            if (CheckPointManager.instance != null)
            {
                CheckPointManager.instance.RespawnPlayer(other.gameObject);
            }
            else
            {
                Debug.LogError("Nu am gasit CheckPointManager in scena!");
            }
        }
    }
}