using UnityEngine;

public class WingAnimationScript : MonoBehaviour
{
    public GameObject wingUp;
    public GameObject wingDown;
    public GameObject wing;
    public float flapSpeed = 0.2f; // Velocità di battito
    
    private bool isFlapping = false;
    private float flapTimer = 0f;

    void Update()
    {
        // Attiva il battito quando si preme spazio
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartFlap();
        }

        // Gestisci l'animazione del battito
        if (isFlapping)
        {
            wing.SetActive(true);
            flapTimer += Time.deltaTime;

            if (flapTimer < flapSpeed / 2)
            {
                // Prima metà: ali su
                wingUp.SetActive(true);
                wingDown.SetActive(false);
            }
            else if (flapTimer < flapSpeed)
            {
                // Seconda metà: ali giù
                wingUp.SetActive(false);
                wingDown.SetActive(true);
            }
            else
            {
                // Fine battito
                isFlapping = false;
                flapTimer = 0f;
                wingDown.SetActive(false);
            }
        }
    }

    void StartFlap()
    {
        isFlapping = true;
        flapTimer = 0f;
    }
}
