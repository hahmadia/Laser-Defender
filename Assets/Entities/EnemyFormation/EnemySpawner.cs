using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
	
	public float spawnDelay = 0.5f;
	public GameObject enemyPrefab;
	public float width = 10f;
	public float height = 5f;
	public float speed = 10f;
	

	private float xMin;
	private float xMax;
	private bool movingRight = true; 
	
	// Use this for initialization
	void Start () {
		
		float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftEdge = Camera.main.ViewportToWorldPoint(new Vector3(0,0,distanceToCamera));
		Vector3 rightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1,0,distanceToCamera));
		xMin = leftEdge.x;
		xMax = rightEdge.x;

		SpawnUntilFull();	
	}
	
	void spawnEnemies(){
		foreach (Transform child in transform){
			// Returns a object but we have to turn it into a gameobject.
			GameObject enemy = Instantiate(enemyPrefab,child.transform.position,Quaternion.identity) as GameObject;
			// This has the enemy created under the Enemy Formation Gameobject to keep things
			// neat and tidy.
			enemy.transform.parent = child;
		}
	}
	
	void SpawnUntilFull() {
		Transform freePosition = NextFreePosition();
		if (freePosition){
			// Returns a object but we have to turn it into a gameobject.
			GameObject enemy = Instantiate(enemyPrefab,freePosition.position,Quaternion.identity) as GameObject;
			// This has the enemy created under the Enemy Formation Gameobject to keep things
			// neat and tidy.
			enemy.transform.parent = freePosition;
		}
		if (NextFreePosition()){
			Invoke ("SpawnUntilFull",spawnDelay);
		}
		
	}
	
	void OnDrawGizmos(){
		Gizmos.DrawWireCube(transform.position,new Vector3(width,height));
	}
	
	// Update is called once per frame
	void Update () {
		

		
		// Calculation the right edge and left edge of the enemy formation
		float rightEdgeEnemyFormation = this.transform.position.x + (width/2);
		float leftEdgeEnemyFormation = this.transform.position.x - (width/2);
		
		// This determines if the enemy formation should move right or left
		if (rightEdgeEnemyFormation >= xMax){
			movingRight = false;
		}
		else if (leftEdgeEnemyFormation <= xMin){
			movingRight = true;
		}	
		
		
		// This moves the enemy formation
		if (movingRight){
			this.transform.position += Vector3.right*speed*Time.deltaTime;
		}
		else{	
			this.transform.position += Vector3.left*speed*Time.deltaTime;
		}
		
		if (AllMembersDead()){
			SpawnUntilFull();
		}
		
		
	}
	
	Transform NextFreePosition(){
		foreach (Transform childPositionGameObject in transform){
			if (childPositionGameObject.childCount == 0){
				return childPositionGameObject;
			}
		}
		return null;
	}
	
	
	bool AllMembersDead() {
		foreach (Transform childPositionGameObject in transform){
			if (childPositionGameObject.childCount > 0){
				return false;
			}
		}
		return true;
	}
	
	
}
