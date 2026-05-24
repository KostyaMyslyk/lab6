using UnityEngine;

public class ObstacleMove : MonoBehaviour
{
    public float speed = 6f;

    void Update()
    {
        if (GameManager.instance != null && GameManager.instance.isGameOver)
            return;

        transform.position += Vector3.left * speed * Time.deltaTime;

        if (transform.position.x < -12f)
            Destroy(gameObject);
    }
}