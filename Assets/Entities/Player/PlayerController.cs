using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	public float speed = 10f;
	public float padding = 0.5f;
	public GameObject laserPrefab;
	public float laserSpeed;
	public float firingRate;
	public float health = 250f;
	public AudioClip fireSound;
	
	private float xMin;
	private float xMax;
	
	
	void Start(){
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0,0,distance));
		Vector3 rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1,0,distance));
		xMin = leftMost.x + padding;
		xMax = rightMost.x - padding;
	
	}
	
	// Update is called once per frame
	void Update () {
		MoveWithKeyboard();
	}
	
	void MoveWithKeyboard(){
		
		if (Input.GetKeyDown(KeyCode.Space)){
			InvokeRepeating("shootLaser",0.000001f,firingRate);
		}
		
		if (Input.GetKeyUp(KeyCode.Space)){
			CancelInvoke("shootLaser");
		}
		
		if (Input.GetKey(KeyCode.LeftArrow)){
			this.transform.position += Vector3.left*speed*Time.deltaTime;
		}
		else if (Input.GetKey(KeyCode.RightArrow)){
			this.transform.position += Vector3.right*speed*Time.deltaTime;
		}
		
		// Restrics the player to the game space
		float newX = Mathf.Clamp(this.transform.position.x,xMin,xMax);
		this.transform.position = new Vector3(newX, transform.position.y,transform.position.z);
		
	}
	
	void OnTriggerEnter2D (Collider2D collider){
		Projectile missile = collider.gameObject.GetComponent<Projectile>();
		
		if (missile){
			health -= missile.GetDamage();
			missile.Hit();
			
			if (health <= 0){
				LevelManager levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
				levelManager.LoadLevel("Win Screen");
				Destroy (gameObject);
			}
			
		}
	}
	
	void shootLaser (){
		GameObject laser = Instantiate (laserPrefab , this.transform.position , Quaternion.identity) as GameObject;
		AudioSource.PlayClipAtPoint(fireSound,transform.position);
		laser.GetComponent<Rigidbody2D>().velocity = new Vector3 (0,laserSpeed,0);
	}
	
}
