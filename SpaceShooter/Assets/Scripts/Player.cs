using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    // Playing
    // Explosion
    // Invincible
    enum State
    {
        Playing,
        Explosion,
        Invincible
    }

    private State state = State.Playing;

    public float PlayerSpeed;
    public GameObject ProjectilePrefab;
    public GameObject Explosion2Prefab;

    public static int Score = 0;
    public static int Lives = 3;
    public static int Missed = 0;

    public EasyButton FireButton;

    private float ProjectileOffset = 1.2f;
    private float shipInvisibleTime = 1.5f;
    private float shipMoveOnToScreenSpeed = 5f;
    private float blinkRate = 0.1f;
    private int numberOfTimesToBlink = 10;
    private int blinkCount = 0;

    // Use this for initialization
	void Start () 
    {
        // transform.position = new Vector3(-6, 5, transform.position.z);
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (state != State.Explosion)
        {

            //Amount to move 
            float amtToMove = Input.GetAxisRaw("Horizontal") * PlayerSpeed * Time.deltaTime;

            //Move the player
            transform.Translate(Vector3.right * amtToMove);

            //Wrap
            if (transform.position.x <= -7.5f)
                transform.position = new Vector3(7.4f, transform.position.y, transform.position.z);
            else if (transform.position.x >= 7.5f)
                transform.position = new Vector3(-7.4f, transform.position.y, transform.position.z);

            if (Input.GetKeyDown("space"))
            {
                // fire projectile
                Vector3 position = new Vector3(transform.position.x, transform.position.y + ProjectileOffset);
                Instantiate(ProjectilePrefab, position, Quaternion.identity);
            }
        }
	}

    void OnEnable()
    {
       EasyButton.On_ButtonDown += On_ButtonDown;
    }

    void OnDisable()
    {
        EasyButton.On_ButtonDown -= On_ButtonDown;
    }

    void OnDestroy()
    {
        EasyButton.On_ButtonDown -= On_ButtonDown;
    }

    void On_ButtonDown(string buttonName)
    {
       
        if (buttonName == "FireButton")
        {      
            // fire projectile
            Vector3 position = new Vector3(transform.position.x, transform.position.y + ProjectileOffset);
            Instantiate(ProjectilePrefab, position, Quaternion.identity);
        }

    }

    void OnGUI()
    {
        BuildUI();
    }

    void BuildUI()
    {
        GUI.Label(new Rect(10, 10, 120, 20), "Score: " + Player.Score.ToString());
        GUI.Label(new Rect(10, 30, 60, 20), "Lives: " + Player.Lives.ToString());
        GUI.Label(new Rect(10, 50, 120, 20), "Misses: " + Player.Missed.ToString());
    }

    void OnTriggerEnter(Collider otherObject)
    {
        if (otherObject.tag == "enemy" && state == State.Playing)
        {
            Player.Lives--;
            Enemy enemy = (Enemy)otherObject.gameObject.GetComponent("Enemy");
            enemy.SetPositionAndSpeed();

            StartCoroutine(DestroyShip());
        }
    }

    IEnumerator DestroyShip()
    {
        state = State.Explosion;
        Instantiate(Explosion2Prefab, transform.position, transform.rotation);
        gameObject.renderer.enabled = false;
        transform.position = new Vector3(0f, -5.5f, transform.position.z);
        yield return new WaitForSeconds(shipInvisibleTime);
        if (Player.Lives > 0)
        {
            gameObject.renderer.enabled = true;

            while (transform.position.y < -3.2)
            {
                float amtToMove = shipMoveOnToScreenSpeed * Time.deltaTime;
                transform.position = new Vector3(0f, transform.position.y + amtToMove, transform.position.z);
                yield return 0;
            }

            state = State.Invincible;

            while (blinkCount < numberOfTimesToBlink)
            {
                gameObject.renderer.enabled = !gameObject.renderer.enabled;

                if (gameObject.renderer.enabled == true)
                    blinkCount++;
                yield return new WaitForSeconds(blinkRate);
            }
            blinkCount = 0;
            state = State.Playing;
        }
        else
            Application.LoadLevel(2);
        
    }

}
