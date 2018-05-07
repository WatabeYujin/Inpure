using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour {
    /// <summary>
    /// カラーを変更する際のスクリプト
    /// 色の取得と変化はこのスクリプトで行う。
    /// </summary>
    
    [SerializeField]
    protected Renderer[] changeRenderers;           //変更するRenderer
    public enum ColorName{
        Cyan,                                       //シアン（水色）
        Magenta,                                    //マゼンタ（紫）
        Yellow,                                     //イエロー
        Red,                                        //赤（マゼンタ+イエロー)
        Green,                                      //緑（シアン+イエロー）
        Blue,                                       //青（マゼンタ+シアン）
        White,                                      //白（白）
        Black                                       //黒（黒・即死）
    }
    [SerializeField]
    private Color[] changeColors = new Color[8] {
        new Color(        0,200f/255f,200f/255f),   //シアン
        new Color(200f/255f,        0,200f/255f),   //マゼンタ
        new Color(200f/255f,200f/255f,        0),   //イエロー
        new Color(200f/255f,        0,        0),   //赤
        new Color(        0,200f/255f,        0),   //緑
        new Color(        0,        0,200f/255f),   //青
        new Color(255f/255f,255f/255f,255f/255f),   //白
        new Color(        0,        0,        0)    //黒
    };

    [SerializeField]
    protected ColorName nowColor = ColorName.White; //現在の色(GetNowColor)
    private ColorName changeColor = ColorName.White;//弾丸によって変更された色
    private ColorName panelColor=ColorName.White;   //パネルによって追加された色

    [SerializeField]
    protected Renderer[] changeRenderersChild;      //子の色に変更するrenderer
                                                    //例）青の場合、[0]=マゼンタ、[1]=シアン、[2]=マゼンタ、[3]=シアン

    ///////////////////////////////////////////////////////////////////////////////////////////
    
    /// <summary>
    /// 実際に変化させる色を決定する処理
    /// </summary>
    void ColorChangeProcess(){
        switch (changeColor)
        {
            case ColorName.Cyan:
                switch (panelColor){
                    case ColorName.Magenta:
                        nowColor = ColorName.Blue;
                        break;
                    case ColorName.Yellow:
                        nowColor = ColorName.Green;
                        break;
                    default:
                        nowColor = ColorName.Cyan;
                        break;
                }
                break;
            case ColorName.Magenta:
                switch (panelColor){
                    case ColorName.Cyan:
                        nowColor = ColorName.Blue;
                        break;
                    case ColorName.Yellow:
                        nowColor = ColorName.Red;
                        break;
                    default:
                        nowColor = ColorName.Magenta;
                        break;
                }
                break;
            case ColorName.Yellow:
                switch (panelColor){
                    case ColorName.Cyan:
                        nowColor = ColorName.Green;
                        break;
                    case ColorName.Magenta:
                        nowColor = ColorName.Red;
                        break;
                    default:
                        nowColor = ColorName.Yellow;
                        break;
                }
                break;
        }
        MainColorChange();
        SubColorChange();
    }

    //changeRenderersに設定したモノの色を変更する。
    void MainColorChange()
    {
        if (changeRenderers.Length != 0)
        {
            //インスペクタよりchangeRenderersが設定されていた場合
            //設定されたrendererの色を変更する。
            for (int i = 0; i < changeRenderers.Length; i++)
            {
                changeRenderers[i].material.color = changeColors[(int)nowColor];
            }
        }
        else
        {
            //設定されていなかった場合このオブジェクト自体の色を変更する

            GetComponent<Renderer>().material.color = changeColors[(int)nowColor];
        }
    }

    //changeRenderersChildに設定したモノの色を変更する。
    void SubColorChange()
    {
        if (changeRenderersChild.Length != 0)
        {
            if ((int)nowColor >= 3 && (int)nowColor <= 5)
            {
                int[] m_chaildColorName = new int[2];
                
                //二色の子の色を取得する
                switch (nowColor)
                {
                    case ColorName.Red:
                        m_chaildColorName[0] = (int)ColorName.Magenta;
                        m_chaildColorName[1] = (int)ColorName.Yellow;
                        break;
                    case ColorName.Green:
                        m_chaildColorName[0] = (int)ColorName.Yellow;
                        m_chaildColorName[1] = (int)ColorName.Cyan;
                        break;
                    default:
                        m_chaildColorName[0] = (int)ColorName.Magenta;
                        m_chaildColorName[1] = (int)ColorName.Cyan;
                        break;
                }
                for (int i = 0; i < changeRenderersChild.Length; i++)
                {
                    changeRenderersChild[i].material.color = changeColors[(int)m_chaildColorName[(1 + i) % 2]];
                }
            }
            else
            {
                for (int i = 0; i < changeRenderersChild.Length; i++)
                {
                    changeRenderersChild[i].material.color = changeColors[(int)nowColor];
                }
            }
        }
    }

///////////////////////////////////////////////////////////////////////////////////////////

    /*///////////////////////////////////////////////////////////
                             全般
    ///////////////////////////////////////////////////////////*/

    /// <summary>
    /// 現在の色を取得・設定を行う
    /// </summary>
    public ColorName GetSetNowColor
    {
        get
        {
            return nowColor;
        }set
        {
            nowColor = value;
            ColorChangeProcess();
        }
    }

    /*///////////////////////////////////////////////////////////
                            プレイヤー用
    ///////////////////////////////////////////////////////////*/

    /// <summary>
    /// 弾丸による色変更を行う
    /// パネルの場合は"SetPanelColor"を使用してください。
    /// </summary>
    /// <param name="ChangeColor">弾丸により変更する色</param>
    public ColorName SetBulletColor
    {
        set
        {
            changeColor = value;
            ColorChangeProcess();
        }
    }

    /// <summary>
    /// パネルによる色の追加を行う
    /// パネルから出る場合はColorName.Whiteを入れてください。
    /// 弾丸の場合は"SetBuletColor"を使用してください。
    /// </summary>
    /// <param name="ChangeColor">パネルにより加わる色</param>
    public ColorName SetPanelColor{
        set
        {
            panelColor = value;
            ColorChangeProcess();
        }
    }

    /*///////////////////////////////////////////////////////////
                            エネミー用
    ///////////////////////////////////////////////////////////*/

    /// <summary>
    /// 受けた色で倒せるかをbool型で返します
    /// 倒せる場合true,倒せない場合false
    /// </summary>
    /// <param name="DamageColor">受ける色</param>
    /// <returns></returns>
    public bool DamageColorChack(ColorName DamageColor)
    {
        bool m_isdead;
        switch (nowColor)
        {
            case ColorName.Cyan:
                if (DamageColor == ColorName.Red) m_isdead = true;
                else m_isdead = false;
                break;
            case ColorName.Magenta:
                if (DamageColor == ColorName.Green) m_isdead = true;
                else m_isdead = false;
                break;
            case ColorName.Yellow:
                if (DamageColor == ColorName.Blue) m_isdead = true;
                else m_isdead = false;
                break;
            default:
                m_isdead = false;
                break;
        }
        return m_isdead;
    }
    public Color GetColor
    {
        get
        {
            return changeColors[(int)nowColor];
        }
    }
}
