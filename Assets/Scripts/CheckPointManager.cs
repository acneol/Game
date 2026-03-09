using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    public static CheckPointManager instance;
    private Vector3 _lastCheckpointPosition;

    void Awake()
    {
        // Singleton pattern pentru a accesa managerul de oriunde
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        // Salvăm poziția inițială a jucătorului ca prim checkpoint
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) _lastCheckpointPosition = player.transform.position;
    }

    public void UpdateCheckpoint(Vector3 pos)
    {
        _lastCheckpointPosition = pos;
    }

    // Această funcție trebuie să fie AICI, între acoladele clasei, nu în Start!
    public void RespawnPlayer(GameObject hitObject)
    {
        // 1. Luăm rădăcina (FullPlayer)
        Transform fullPlayer = hitObject.transform.root;

        // 2. Găsim CharacterController (care probabil e pe 'Plr')
        CharacterController cc = fullPlayer.GetComponentInChildren<CharacterController>();

        // IMPORTANT: Dezactivăm CC-ul. Dacă rămâne activ, el "ține" capsula 
        // la vechile coordonate chiar dacă muți părintele.
        if (cc != null) cc.enabled = false;

        // 3. Teleportăm Părintele
        Vector3 spawnPos = _lastCheckpointPosition + Vector3.up * 1.5f;
        fullPlayer.position = spawnPos;

        // 4. FORȚĂM copiii să își reseteze poziția locală la zero
        // Astfel se vor alinia perfect cu părintele lor (FullPlayer)
        foreach (Transform child in fullPlayer)
        {
            child.localPosition = Vector3.zero;

            // Dacă ai un Rigidbody pe copii, îi resetăm viteza
            Rigidbody rb = child.GetComponent<Rigidbody>();
            if (rb != null) rb.linearVelocity = Vector3.zero;
        }

        // 5. Reactivăm Controller-ul
        if (cc != null) cc.enabled = true;

        Debug.Log("Teleportare forțată pentru tot grupul FullPlayer!");
    }
}