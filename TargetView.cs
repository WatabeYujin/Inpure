using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetView : MonoBehaviour {
    /// <summary>
    /// 倒さなければならない敵の画像と残りの数を表示する
    /// 残りの数はSetEnemyCountに残りの数を入力してください。
    /// 敵の画像はインスペクタのtargetSpriteより設定お願いします。
    /// </summary>

    private Text targetText;
    [SerializeField]
    private Sprite targetSprite;        //倒さなければならない敵の画像
    int enemyCount;
    int[] minMaxFontSize = { 60, 75 };  //字の最大と最小の大きさ（変動した際の演出にて使用）
    
    //////////////////////////////////////////////////////////////////////////////////

    void Start()
    {
        targetText = GetComponent<Text>();
        transform.Find("TargetImage").GetComponent<Image>().sprite=targetSprite;
    }

    IEnumerator TextAction()
    {
        for (int i = minMaxFontSize[1]; i > minMaxFontSize[0]; i--)
        {
            targetText.fontSize = i;
            yield return null;
        }
    }

    //////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    /// 残りの敵の数を入力してください。
    /// 変動した際は必ず入力をお願いします（開幕にも）
    /// </summary>
    public int SetEnemyCount
    {
        set
        {
            StartCoroutine("TextAction");
            targetText.text = " X "+value.ToString();
        }
    }
}
