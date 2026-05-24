using UnityEngine;

public class ObstacleLooper : MonoBehaviour
{
    public float speed = 6f;

    public float leftBorder = -15f;
    public float respawnX = 20f;

    void Update()
    {
        if (GameManager.instance != null &&
            GameManager.instance.isGameOver)
            return;

        
        transform.position += Vector3.left * speed * Time.deltaTime;

        
        if (transform.position.x < leftBorder)
        {
            
            transform.position = new Vector3(
                respawnX,
                transform.position.y,
                0f
            );
        }
    }
}