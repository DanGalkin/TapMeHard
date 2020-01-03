using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public Text m_scoreText;
    public Text m_lifeText;

    public GameObject m_gameOverPanel;
    Board m_board;

    int m_score = 0;
    int m_life = 3;


    public void ScoreTappedCircle()
    {
        m_score++;
        UpdateUIText();
    }

    public void ScoreVanishedCircle()
    {
        m_life--;
        if (m_life < 1)
        {
            GameOver();
        }
        else
        {
            ChangeLifeTextColor(m_life);
            UpdateUIText();
        }
    }

    void ChangeLifeTextColor(int life)
    {
        switch(life)
        {
            default:
                m_lifeText.color = new Color32(8, 238, 6, 255);
                break;
            case 1:
                m_lifeText.color = new Color32(238, 17, 6, 255);
                break;
            case 2:
                m_lifeText.color = new Color32(234, 238, 6, 255);
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        m_board = GameObject.FindObjectOfType<Board>();

        Reset();
        if (m_gameOverPanel)
        {
            m_gameOverPanel.SetActive(false);
        }
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Update is called once per frame
    void UpdateUIText()
    {
        if(m_scoreText)
        {
            m_scoreText.text = PadZero(m_score, 4);
        }
        if(m_lifeText)
        {
            m_lifeText.text = m_life.ToString();
        }
    }

    public void Reset()
    {
        m_score = 0;
        m_life = 3;
        UpdateUIText();
    }

    string PadZero(int n, int padDigits)
    {
        string nStr = n.ToString();

        while (nStr.Length < padDigits)
        {
            nStr = "0" + nStr;
        }
        return nStr;
    }

    void GameOver()
    {
        m_gameOverPanel.SetActive(true);
        m_gameOverPanel.transform.Find("ScoreText").GetComponent<Text>().text = PadZero(m_score, 4);
        m_board.m_gameOver = true;
    }
}
