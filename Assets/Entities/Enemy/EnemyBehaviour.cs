using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {
	
	public GameObject projectile;
	public float projectileSpeed = 10;
	public float health = 200;
	public float shotsPerSeconds = 0.5f;
	public int scoreValue = 50;
	
	public AudioClip fireSound;
	public AudioClip deathSound;
	
	private ScoreKeeper scoreKeeper;
	
	void Start(){
		scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
	}
	
	
	
	void Update (){
		float probability = shotsPerSeconds * Time.deltaTime;
		if (Random.value < probability){
			Fire ();
		}
		
	}
	
	void Fire(){
		GameObject missile = Instantiate (projectile, transform.position, Quaternion.identity) as GameObject;
		AudioSource.PlayClipAtPoint(fireSound,transform.position);
		missile.GetComponent<Rigidbody2D>().velocity = new Vector2(0,-projectileSpeed);
	}
	
	
	
	void OnTriggerEnter2D (Collider2D collider){
		Projectile missile = collider.gameObject.GetComponent<Projectile>();
		
		if (missile){
			health -= missile.GetDamage();
			missile.Hit();
			
			if (health <= 0){
				AudioSource.PlayClipAtPoint(deathSound,transform.position);
				scoreKeeper.Score(scoreValue);
				Destroy (gameObject);
			}
		
		}
	}
}
