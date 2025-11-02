using UnityEngine;

public class BirdScript : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public float flapStrength;
    public LogicManagerScript logic;
    public bool birdIsAlive = true;
    private bool gameStarted = false; // Traccia se il giocatore ha iniziato a giocare

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicManagerScript>();
        
        // Disabilita la gravità all'inizio
        myRigidbody.gravityScale = 0f;
        myRigidbody.linearVelocity = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        // Permetti il flap SOLO se la schermata di benvenuto è chiusa
        if(Input.GetKeyDown(KeyCode.Space) && birdIsAlive && logic != null)
        {
            // Ignora l'input se la schermata di benvenuto è ancora aperta
            if (logic.welcomeScreen != null && logic.welcomeScreen.activeSelf)
            {
                return; // Non fare nulla se il menu è ancora aperto
            }

            // Al primo spazio (dopo che il menu è chiuso), avvia il gioco e abilita la gravità
            if (!gameStarted)
            {
                gameStarted = true;
                myRigidbody.gravityScale = 2f; // Abilita la gravità
                logic.StartGame(); // Avvia il gioco (spawna i tubi, ecc.")
            }
            
            // Esegui il flap
            myRigidbody.linearVelocity = Vector2.up * flapStrength;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Ignora collisioni con pavimento e soffitto
        if (collision.gameObject.CompareTag("Floor") || collision.gameObject.CompareTag("Ceiling"))
            return;

        // Tutte le altre collisioni (tubi) causano game over
        logic.gameOver();
        birdIsAlive = false;
    }
}
