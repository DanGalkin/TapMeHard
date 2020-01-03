using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float m_repeatSpawnTime = 1f;
    public Board m_gameBoard;

    float m_timeToNextSpawn;

    // Start is called before the first frame update
    void Start()
    {
        m_timeToNextSpawn = Time.time;
        m_gameBoard = GameObject.FindObjectOfType<Board>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
