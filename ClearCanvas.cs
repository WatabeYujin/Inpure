using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearCanvas : MonoBehaviour
{
    [SerializeField] private Text m_timeText;
    [SerializeField] private Text m_scoreText;

    // Use this for initialization
    void Start()
    {
        m_timeText.text = "TimeLimit　"+ScoreManager.Instance.RemainingTime + " s";
        m_scoreText.text = "Score　" + ScoreManager.Instance.EarnScore + " P";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
