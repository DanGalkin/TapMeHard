using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Circle : MonoBehaviour
{
    public bool m_isActive = false;
    public bool m_isTapped = false;

    public int m_level = 1;
    float m_avgTimeToUncover;
    public float m_uncoverProbInterval = 0.5f;
    float m_circleLife;

    public SpriteMask m_spriteMask;
    public int m_limitOfMasks = 60;

    GameObject m_scoreManager;
    
    CircleCollider2D m_collider;

    // Start is called before the first frame update
    void Start()
    {
        m_avgTimeToUncover = 2f * Mathf.Pow(0.9f, m_level);
        m_circleLife = 3f * Mathf.Pow(0.95f, m_level);
        m_scoreManager = GameObject.FindWithTag("ScoreManager");
        m_isActive = false;
        m_isTapped = false;
        m_collider = GetComponent<CircleCollider2D>();
        m_collider.enabled = false;

        StartCoroutine(Activate());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Activate()
    {
        float timeToUncover = Random.Range(m_avgTimeToUncover * (1f - 0.5f), m_avgTimeToUncover * (1f + 0.5f));
        StartCoroutine(CircleAppear(timeToUncover));
        yield return new WaitForSeconds(timeToUncover);

        yield return new WaitForSeconds(m_circleLife);
        Vanish();
    }

    void OnMouseDown()
    {
        m_isTapped = true;
        m_scoreManager.GetComponent<ScoreManager>().ScoreTappedCircle();
        Destroy(gameObject);
    }

    void Vanish()
    {
        m_scoreManager.GetComponent<ScoreManager>().ScoreVanishedCircle();
        Destroy(gameObject);
    }

    IEnumerator CircleAppear(float timeToUncover = 1f)
    {
        float maskRepeatRate = timeToUncover / m_limitOfMasks;
        int maskNumber = 0;
        float timeToNextMask = Time.time;

        while (maskNumber < m_limitOfMasks)
        {
            if (Time.time >= timeToNextMask)
            {
                DrawNextMask(maskNumber);
                maskNumber++;
                Debug.Log("Draw a " + maskNumber.ToString() + " mask");
                timeToNextMask = Time.time + maskRepeatRate;
            }
            yield return null;
        }

        m_collider.enabled = true;
        Destroy(transform.Find("GreyCircle").gameObject);
    }

    void DrawNextMask(int i)
    {
        if (m_spriteMask != null)
        {
            SpriteMask mask;
            mask = Instantiate(m_spriteMask, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity) as SpriteMask;
            mask.name = "AngleMask" + i.ToString();
            mask.transform.parent = transform.Find("GreyCircle");
            mask.transform.Rotate(0, 0, i * (360 / m_limitOfMasks));
        }
    }
}
