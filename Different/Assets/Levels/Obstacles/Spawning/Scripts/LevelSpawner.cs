using UnityEngine;
using System.Collections;

public class LevelSpawner : MonoBehaviour
{
    public GameObject alert;
    private bool running = true;
    private int diff;
    private float timeElapsed = 0;
    public float spawnDelay;
    public float spawnDelayFactor;
    public float speedFactor;

    public void Start() { running = true; }
    public void Stop() { running = false; }

    public void Restart()
    {
        running = true;
        timeElapsed = 0;
    }

    private void Awake()
    {
        diff = (int)GameManager.Difficulty;
    }

    private void FixedUpdate()
    {
        if (running)
        {
            if (timeElapsed <= 0)
            {
                GameObject spawn = Instantiate(alert) as GameObject;

                int orient = Mathf.RoundToInt(Random.value);
                Vector2 pos;
                if (orient == 0)
                {
                    int plusMinus = 2 * Mathf.RoundToInt(Random.value) - 1;
                    pos = new Vector3(plusMinus * 10.6f, Random.Range(-5.4f, 5.4f));
                    spawn.GetComponent<AlertController>().direction = plusMinus * 90;
                }
                else
                {
                    int plusMinus = 2 * Mathf.RoundToInt(Random.value) - 1;
                    pos = new Vector3(Random.Range(-9.6f, 9.6f), plusMinus * 6.4f);
                    spawn.GetComponent<AlertController>().direction = 270 - plusMinus * 90;
                }
                spawn.transform.position = pos;
                spawn.GetComponent<AlertController>().obstacleType = (AlertController.ObstacleType)Mathf.FloorToInt(Random.Range(0, 3));

                timeElapsed = spawnDelay + (float)diff * spawnDelayFactor;
            }

            timeElapsed -= Time.deltaTime;
        }
    }
}
