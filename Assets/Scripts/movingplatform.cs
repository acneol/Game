using UnityEngine;
using System.Collections.Generic;

public class MovingPlatform_PingPong : MonoBehaviour
{
    [Header("Mișcare")]
    [Tooltip("Ex: (0,1,0) pentru a urca")]
    public Vector3 direction = Vector3.up;
    public float distance = 5f;
    public float speed = 2f;

    private Rigidbody _rb;
    private Vector3 _startPosition;
    private List<Transform> _onPlatform = new List<Transform>();

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.isKinematic = true;
        _rb.interpolation = RigidbodyInterpolation.Interpolate;

        _startPosition = transform.position;
    }

    void FixedUpdate()
    {
        // Mathf.PingPong(t, length) merge de la 0 la lungime și înapoi
        // Nu mai trece prin valori negative ca Sinus-ul
        float moveFactor = Mathf.PingPong(Time.time * speed, distance);

        Vector3 targetPosition = _startPosition + (direction.normalized * moveFactor);

        // Calculăm cât trebuie să se miște platforma în acest frame (Delta)
        Vector3 deltaMovement = targetPosition - _rb.position;

        // Mișcăm manual tot ce e pe platformă (Sticky Fix)
        foreach (Transform obj in _onPlatform)
        {
            if (obj != null) obj.position += deltaMovement;
        }

        // Mutăm platforma fără erori de linearVelocity
        _rb.MovePosition(targetPosition);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verificăm după Tag sau Componentă
        if (other.CompareTag("Player") || other.GetComponent<CharacterController>() != null)
        {
            if (!_onPlatform.Contains(other.transform))
                _onPlatform.Add(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _onPlatform.Remove(other.transform);
    }


}
