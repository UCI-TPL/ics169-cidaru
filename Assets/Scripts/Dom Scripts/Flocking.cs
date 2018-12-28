using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
public class Flocking : MonoBehaviour {
	private List<GameObject> enemies = new List<GameObject>();
	private Rigidbody2D rb;
    Transform target;
    EnemyMovement mover;

    public float rotSpeed = 100;
    public float maxNeighborDist = 5;
    public float speed;

	void Start () {
		rb = GetComponent<Rigidbody2D>();
        enemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
        speed = Random.Range(0.5f, 2);
        target = GameObject.FindGameObjectWithTag("Player").transform;
        mover = GetComponent<EnemyMovement>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if (Random.Range(0, 5) < 1)
            Flock();

        float step = speed * Time.deltaTime;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        transform.position = this.transform.position;
    }
	
    void Flock()
    {
        Vector3 centerV = Vector3.zero;
        Vector3 avoidV = Vector3.zero;

        float dist;
        int flockCount = 0;

        foreach(GameObject go in enemies)
        {
            dist = Vector3.Distance(go.transform.position, this.transform.position);
            if(dist <= maxNeighborDist)
            {
                centerV += go.transform.position;
                flockCount++;

                if (dist < 1f)
                    avoidV = avoidV + (this.transform.position - go.transform.position);
            }
        }

        if (flockCount > 0)
        {
            centerV = centerV / flockCount + (target.position - this.transform.position);

            Vector3 direction = (centerV + avoidV) - transform.position;
            if (direction != Vector3.zero)
            {
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = Quaternion.Slerp(transform.rotation, q, rotSpeed * Time.deltaTime);
            }
        }
        
    }
}
