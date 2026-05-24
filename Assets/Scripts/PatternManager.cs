using UnityEngine;

public class PatternManager : MonoBehaviour
{
    [Header("Patterns")]
    public GameObject[] patterns;

    [Header("Player")]
    public PlayerController player;

    [Header("Spawn X")]
    public float spawnX = 20f;
    public float secondX = 35f;

    [Header("Timing")]
    public float switchTime = 6f;
    private float timer;

    private int lastCombo = -1;

    void Start()
    {
        ActivateCombo();
    }

    void Update()
    {
        if (GameManager.instance != null &&
            GameManager.instance.isGameOver)
            return;

        timer += Time.deltaTime;

        if (timer >= switchTime)
        {
            ActivateCombo();
            timer = 0f;
        }
    }

    void ActivateCombo()
    {
        foreach (var p in patterns)
            p.SetActive(false);

        int combo = Random.Range(0, 3);

        while (combo == lastCombo)
            combo = Random.Range(0, 3);

        lastCombo = combo;

        switch (combo)
        {
            case 0:
                Activate(0, spawnX);
                Activate(2, secondX);
                break;

            case 1:
                Activate(1, spawnX);
                Activate(3, secondX);
                break;

            case 2:
                Activate(0, spawnX);
                Activate(3, secondX);
                break;
        }

        player.SetInvincible(1f);
    }

    void Activate(int index, float x)
    {
        patterns[index].SetActive(true);

        Vector3 pos = patterns[index].transform.position;
        patterns[index].transform.position = new Vector3(x, pos.y, 0);
    }
}