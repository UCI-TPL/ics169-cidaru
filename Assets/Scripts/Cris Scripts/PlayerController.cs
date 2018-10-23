using UnityEngine;

[RequireComponent(typeof(PlayerHealth))]
public class PlayerController : MonoBehaviour
{
    /* Controls player
    Based on:
    https://unity3d.com/learn/tutorials/projects/survival-shooter/player-character?playlist=17144
    */

    public float originalSpeed = 5f;
    //[HideInInspector]
    public float currentSpeed;
    public GameObject gameOverPanel;

    private Health health;

    private void Start()
    {
        health = GetComponent<Health>();
        gameOverPanel.SetActive(false);
        currentSpeed = originalSpeed;
    }

    void FixedUpdate()
    {
        if (!health.dead())
            Move();

        #region Reset Health (When Dead)
        else
        {
            gameOverPanel.SetActive(true);
        }
        #endregion
    }

    void Move()
    {
        /* Basic free movement */

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(x, y, 0);
        movement = movement.normalized * currentSpeed * Time.deltaTime;
        gameObject.transform.Translate(movement);
    }
}