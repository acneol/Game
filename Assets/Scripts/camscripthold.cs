using UnityEngine;

public class camscripthold : MonoBehaviour
{
    public Transform cameraPosition;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = cameraPosition.position;
    }
}
