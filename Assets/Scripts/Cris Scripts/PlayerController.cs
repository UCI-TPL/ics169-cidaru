using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /// Controls player's movement
    /// Based on:
    /// https://unity3d.com/learn/tutorials/projects/survival-shooter/player-character?playlist=17144
    ///

    public float speed = 5f;

    void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Move(x, y);
    }

    void Move(float x, float y)
    {
        Vector3 movement = new Vector3(x, y, 0);
        movement = movement.normalized * speed * Time.deltaTime;
        gameObject.transform.Translate(movement);
    }
}