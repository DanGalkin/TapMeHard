using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public bool m_isEmpty = true;
    
    // Start is called before the first frame update
    void Start()
    {
        m_isEmpty = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.transform.childCount == 0)
        {
            m_isEmpty = true;
        }
    }
}
