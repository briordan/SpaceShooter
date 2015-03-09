using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    public float ProjectileSpeed;
    public GameObject ExplosionPrefab;

    private Enemy enemy;
    private Transform myTransform;
 
    // Use this for initialization
    void Start()
    {
        enemy = (Enemy) GameObject.Find("Enemy").GetComponent("Enemy");
    }
	
	// Update is called once per frame
	void Update () 
    {
        float amtToMove = ProjectileSpeed * Time.deltaTime;
        transform.Translate(Vector3.up * amtToMove);

        if (transform.position.y > 6.4f)
            Destroy(this.gameObject);
	}

    void OnTriggerEnter(Collider otherObject)
    {
        if (otherObject.tag == "enemy")
        {  
            
            Instantiate(ExplosionPrefab, enemy.transform.position, enemy.transform.rotation);
            enemy.MinSpeed += 0.5f;
            enemy.MaxSpeed += 1f;

            enemy.SetPositionAndSpeed();
            Destroy(gameObject);
            Player.Score += 100;

            if (Player.Score >= 5000)
                Application.LoadLevel(3); // Win Scene
        }
    }
}
