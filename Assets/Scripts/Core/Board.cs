using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public float m_repeatSpawnTime = 1f;
    public SpawnPoint[] m_spawnPoints = new SpawnPoint[6];
    public Circle m_circle;

    public Color[] m_allColors = new Color[4];

    public bool m_gameOver = false;

    float m_timeToNextSpawn;

    int m_level = 1;

    // Start is called before the first frame update
    void Start()
    {
        m_timeToNextSpawn = Time.time;
        m_gameOver = false;
        StartCoroutine(LevelUpper());
    }

    // Update is called once per frame
    void Update()
    {
        if(m_gameOver == false && Time.time > m_timeToNextSpawn)
        {
            SpawnCircle(GetRndSpawnPoint());
            m_timeToNextSpawn = Time.time + Random.Range(m_repeatSpawnTime * (1f - 0.4f), m_repeatSpawnTime * (1f + 0.4f)) * Mathf.Pow(0.95f, m_level);
        }
    }

    void SpawnCircle(SpawnPoint spawnPoint)
    {
        if(spawnPoint && m_circle)
        {
            Circle circle = Instantiate(m_circle, spawnPoint.transform.position, Quaternion.identity) as Circle;
            circle.transform.parent = spawnPoint.transform;
            circle.GetComponent<SpriteRenderer>().color = GetRandomColor();
            circle.m_level = m_level;
            spawnPoint.m_isEmpty = false;
        }
        else
        {
            Debug.Log("Warning! No spawmPoint or circle is given to spawner");
            return;
        } 
    }

    Color GetRandomColor()
    {
        int i = Random.Range(0, m_allColors.Length);
        return m_allColors[i];
    }

    SpawnPoint GetRndSpawnPoint()
    {
        List<int> m_emptySpawnPoints = new List<int>();

        for (int i = 0; i < m_spawnPoints.Length; i++)
        {
            if(m_spawnPoints[i].m_isEmpty == true)
            {
                m_emptySpawnPoints.Add(i);
            }
        }

        if(m_emptySpawnPoints.Count == 0)
        {
            Debug.Log("No empty spawn points");
            return null;
        }
        else
        {
            int m_randomEmptySpawnPointIndex = Random.Range(0, m_emptySpawnPoints.Count);
            return m_spawnPoints[m_emptySpawnPoints[m_randomEmptySpawnPointIndex]];
        }
    }

    IEnumerator LevelUpper()
    {
        while(m_level<100)
        {
            yield return new WaitForSeconds(5);
            m_level++;
        }
    }
}
