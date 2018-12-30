using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Flocking : MonoBehaviour {
	private List<GameObject> enemies = new List<GameObject>();
    private List<GameObject> obstacles = new List<GameObject>();
	private Rigidbody2D rb;
    Transform target;
    EnemyMovement mover;
    Quaternion newDirection;
    public Vector3 avoidance;

    public float rotSpeed = 100;
    public float maxNeighborDist = 5f;
    public float maxAvoidDist = 3f;
    public float speed;
    public int flockCount;
    public int avoidCount;
    public float avoidanceMultiplier = 3f;
    public float steerMultiplier = 0.5f;
    

	void Start () {
		rb = GetComponent<Rigidbody2D>();
        enemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
        speed = Random.Range(2f, 5f);
        target = GameObject.FindGameObjectWithTag("Player").transform;
        mover = GetComponent<EnemyMovement>();
        obstacles = new List<GameObject>(GameObject.FindGameObjectsWithTag("FlockAvoid"));
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if (Random.Range(0, 5) < 1)
            Flock();

        float step = speed * Time.deltaTime;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector3 velocity3d =   (newDirection * (transform.right * speed));
        Vector3 steering = (velocity3d * steerMultiplier) + (avoidance * avoidanceMultiplier);
        rb.velocity += new Vector2(steering.x, steering.y);

        if(rb.velocity.magnitude > speed)
        {
            rb.velocity = rb.velocity.normalized * speed;
        }
    }
	
    void Flock()
    {
        Vector3 centerV = Vector3.zero;
        Vector3 avoidV = Vector3.zero;

        float dist;
        flockCount = 0;
        avoidCount = 0;

        foreach (GameObject go in enemies)
        {
            dist = Vector3.Distance(go.transform.position, this.transform.position);
            if (dist <= maxNeighborDist)
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
                newDirection = Quaternion.Slerp(transform.rotation, q, rotSpeed * Time.deltaTime);
            }
        }
        
        if(obstacles.Count > 0)
        {
            avoidance = Vector3.zero;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, newDirection * transform.right, maxAvoidDist, LayerMask.GetMask("Obstacle Layer"));
            Debug.DrawRay(transform.position, newDirection * transform.right, Color.magenta, 1);
            Debug.Log(hit.collider.tag);
            if (hit && hit.collider.gameObject.tag == "FlockAvoid")
            {
                avoidance.x = (newDirection * transform.up).x - hit.collider.transform.position.x;
                avoidance.y = (newDirection * transform.up).y - hit.collider.transform.position.y;

                avoidance.Normalize();
                Debug.Log("Hit = " + hit.collider.tag);
            }
            else
                avoidance = Vector3.zero;

            Debug.DrawRay(transform.position, avoidance, Color.cyan, 1);
        }
    }
}
