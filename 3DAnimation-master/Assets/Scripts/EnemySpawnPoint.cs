using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    public Color gizmoColor;
    public Vector3 boxSize = new Vector3(1, 2, 1);

    public GameObject enemy;

    public bool isEnemySpawned = false;

    public GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindWithTag("Game Manager").GetComponent<GameManager>();
        Gizmos.color = gizmoColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gm.isPaused)
        {
            if (!isEnemySpawned)
            {
               GameObject newEnemy = Instantiate(enemy, gm.randomEnemySpawn().transform.position, Quaternion.identity) as GameObject;
                isEnemySpawned = true;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        float boxOffsetY = boxSize.y / 2;
        Gizmos.DrawCube(transform.position + (boxOffsetY * Vector3.up), new Vector3(1, 2, 1));
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position + (boxOffsetY * Vector3.up), transform.forward);
    }
}
