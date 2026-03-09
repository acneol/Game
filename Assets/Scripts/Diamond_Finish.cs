using UnityEngine;

public class Diamond_Finish : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TimerManager.instance.FinishGame();
        }
    }
}
