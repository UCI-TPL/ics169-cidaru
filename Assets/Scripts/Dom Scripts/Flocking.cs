using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
public class Flocking : MonoBehaviour {
	private List<GameObject> enemies = new List<GameObject>();
	private Rigidbody2D rb;
	public Collider2D flockingRange;
    float speed;
    Transform target;
    EnemyMovement mover;


	void Start () {
		rb = GetComponent<Rigidbody2D>();
        enemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
        speed = Random.Range(3, 5);
        target = GameObject.FindGameObjectWithTag("Player").transform;
        mover = GetComponent<EnemyMovement>();
	}
	
	// Update is called once per frame
	void Update () {
        mover.Move(target.position);
	}
	
	public Vector3 Separate(){
		Vector3 v = new Vector3();
		if(enemies.Count == 0)
			return v;
		foreach(GameObject go in enemies){
			v.x += go.transform.position.x - transform.position.x;
			v.y += go.transform.position.y - transform.position.y;
		}
		
		v.x /= enemies.Count;
		v.y /= enemies.Count;
		v.x *= -1f;
		v.y *= -1f;
		Vector3.Normalize(v);
		return v;
	}
	
	
	
	// Keep list of all enemies inside circle collider trigger
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag == "Enemy"){
			 if(!enemies.Contains(other.gameObject))
			 {
				 Debug.Log("New Enemy added" + enemies.Count);
				 enemies.Add(other.gameObject);
			 }
		}
	}
 
	void OnTriggerExit2D(Collider2D other)
	{
		 if(other.gameObject.tag == "Enemy"){
			 if(enemies.Contains(other.gameObject))
			 {
				 enemies.Remove(other.gameObject);
			 }
		 }
	}
}
