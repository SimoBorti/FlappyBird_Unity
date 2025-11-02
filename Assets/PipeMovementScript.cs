using UnityEngine;

public class PipeMovementScript : MonoBehaviour
{
    public float moveSpeed = 5; // Velocità base
    public float deadZone = -17; // X position at which the pipe resets
    private LogicManagerScript logic;
    private float currentSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicManagerScript>();
        currentSpeed = moveSpeed; // Inizia con la velocità base
    }

    // Update is called once per frame
    void Update()
    {
        // Non muovere i tubi finché il gioco non è iniziato
        if (logic != null && !logic.gameHasStarted)
        {
            return;
        }

        // Ferma il movimento se il gioco è finito
        if (logic != null && logic.isGameOver)
        {
            return;
        }

        // Aumenta la velocità in base al punteggio
        if (logic != null)
        {
            // Aumenta la velocità del 20% ogni 5 punti
            currentSpeed = moveSpeed + (logic.playerScore / 5) * 1f;
        }

        transform.position = transform.position + (Vector3.left * currentSpeed) * Time.deltaTime;
        
        if (transform.position.x < deadZone)
        {
            Debug.Log("Pipe Destroyed");
            Destroy(gameObject); // Distrugge il pipe una volta che esce dallo schermo
        }
    }
}
