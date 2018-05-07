using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeViwer : MonoBehaviour {

    private Text scoreText;
    private int Time;             //総スコア（GetScoreにて参照可能）
    int fontSize;
    [SerializeField]
    int[] minMaxFontSize = { 70, 80 };     //字の最大と最小の大きさ（変動した際の演出にて使用）
	// Use this for initialization
	void Start () {
        scoreText = GetComponent<Text>();
	}
    IEnumerator TextAction()
    {
        for (fontSize = minMaxFontSize[1]; fontSize > minMaxFontSize[0]; fontSize--)
        {
            scoreText.fontSize = fontSize;
            yield return null;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public int GetSetScore
    {
        get
        {
            return Time;
        }
        set
        {
            StartCoroutine("TextAction");
            Time = value;
            scoreText.text = "TIME " + Time.ToString();
        }
    }
}
