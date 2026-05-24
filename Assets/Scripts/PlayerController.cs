using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [Header("Lanes")]
    public float[] lanes = { 5f, 1f, -1f, -5f };

    [Header("Movement")]
    public float moveSpeed = 12f;

    [Header("Score UI")]
    public TextMeshProUGUI scoreText;

    private int laneIndex = 3;
    private Vector3 targetPos;

    private float score = 0f;
    private bool isDead = false;
    private bool isInvincible = false;

    void Start()
    {
        targetPos = transform.position;
    }

    void Update()
    {
        if (isDead) return;

        HandleInput();
        Move();
        UpdateScore();
    }

    void HandleInput()
    {
        bool pressed =
            (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame) ||
            (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame);

        if (pressed)
        {
            laneIndex = (laneIndex - 1 + lanes.Length) % lanes.Length;

            targetPos = new Vector3(
                transform.position.x,
                lanes[laneIndex],
                0f
            );
        }
    }

    void Move()
    {
        transform.position = Vector3.Lerp(
            transform.position,
            targetPos,
            moveSpeed * Time.deltaTime
        );
    }

    void UpdateScore()
    {
        score += Time.deltaTime * 10f;

        if (scoreText != null)
            scoreText.text = "Score: " + Mathf.FloorToInt(score);
    }

    public int GetScore()
    {
        return Mathf.FloorToInt(score);
    }

    public void SetInvincible(float time)
    {
        StartCoroutine(InvincibleRoutine(time));
    }

    System.Collections.IEnumerator InvincibleRoutine(float t)
    {
        isInvincible = true;
        yield return new WaitForSeconds(t);
        isInvincible = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isDead || isInvincible) return;

        if (other.CompareTag("Obstacle"))
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;

        GameManager.instance.GameOver(GetScore());
    }
}