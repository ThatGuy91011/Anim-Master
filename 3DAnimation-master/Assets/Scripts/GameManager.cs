using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton
    public static GameManager instance;
    // Data game needs to run
    public List<GameObject> enemySpawnpoints;

    public UIManager ui;

    public bool isPaused = false;

    public GameObject pauseMenu;
    public void Awake()
    {
        // Create singleton
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject sp in GameObject.FindGameObjectsWithTag("Enemy Spawnpoint"))
        {
            enemySpawnpoints.Add(sp);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused)
            {
                Unpause();
            }
            else
            {
                Pause();
            }
        }
    }

    public GameObject randomEnemySpawn()
    {
        return enemySpawnpoints[Random.Range(0, enemySpawnpoints.Count)];
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        isPaused = true;
        Time.timeScale = 0f;
    }

    public void Unpause()
    {
        pauseMenu.SetActive(false);
        isPaused = false;
        Time.timeScale = 1f;
    }
}
