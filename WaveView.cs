using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WaveView : MonoBehaviour
{
    /// <summary>
    /// ウェーブ数を表示するスクリプトです。
    /// </summary>
    private AudioSource se;
    int nowWave=1;
    int maxWave;
    Text WaveText;
    [SerializeField]
    int[] minMaxFontSize = { 70, 90 };

    ///////////////////////////////////////////////////////////////////

    void Start () {
        se = GetComponent<AudioSource>();
        WaveText =GetComponent<Text>();
	}
    
    IEnumerator TextAction()
    {
        se.Play();
        for (int i = minMaxFontSize[1]; i > minMaxFontSize[0]; i--)
        {
            WaveText.fontSize = i;
            yield return null;
        }
    }

    ///////////////////////////////////////////////////////////////////

    /// <summary>
    /// 次のウェーブに進む際はこちらを呼び出してください。
    /// 戻り値として移動ができない場合（最高ウェーブに到達）falseを返します。
    /// まだウェーブが進む場合trueを返します。
    /// </summary>
    public bool MoveNextWave()
    {
        if (nowWave != maxWave)
        {
            nowWave++;
            WaveText.text = "WAVE " + nowWave.ToString();
            StartCoroutine("TextAction");
            return true;
        }
        else return false;
    }

    /// <summary>
    /// 最大ウェーブを設定する際はこちらを呼び出してください。
    /// </summary>
    public int SetMaxWave
    {
        set
        {
            maxWave = value;
            transform.Find("MaxWave").GetComponent<Text>().text = maxWave.ToString();
        }
    }
}
