using UnityEngine;

public class CloudsMovementScript : MonoBehaviour
{
    public float moveSpeed = 10; // Velocità base
    public float deadZone = -17; // X position at which the pipe resets
    private LogicManagerScript logic;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        // Non muovere le nuvole finché il gioco non è iniziato
        if (logic != null && !logic.gameHasStarted)
        {
            return;
        }

        // Ferma il movimento se il gioco è finito
        if (logic != null && logic.isGameOver)
        {
            return;
        }

        transform.position = transform.position + (Vector3.left * moveSpeed) * Time.deltaTime;

        if (transform.position.x < deadZone)
        {
            Debug.Log("Cloud Destroyed");
            Destroy(gameObject); // Distrugge la nuvola una volta che esce dallo schermo
        }
    }
}
