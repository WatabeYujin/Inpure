using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationControl : MonoBehaviour {
    [SerializeField]
    Vector2 moveRotate;
    float[] playerEulerAnglesY = new float[2];
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private float differenceRotateY;
    [SerializeField]
    Vector2 m_MoveLocalRotate;
    void Start () {
		
	}
	
	
	void Update () {
        moveRotate = new Vector2(Input.GetAxis("Horizontal")  ,Input.GetAxis("Vertical"));
        anim.SetBool("Trigger", Input.GetMouseButton(0));
        playerEulerAnglesY[0] = transform.eulerAngles.y;
        if (moveRotate == new Vector2(0, 0))
        {
            anim.SetFloat("Speed", 0);
            anim.SetFloat("MoveX", 0);
            anim.SetFloat("MoveY", 0);
        }
        else
        {
            anim.SetFloat("Speed", 1);
            playerEulerAnglesY[1] = Quaternion.LookRotation(new Vector3(moveRotate.x, 0, moveRotate.y)).eulerAngles.y;
            differenceRotateY = playerEulerAnglesY[0] - playerEulerAnglesY[1];
            differenceRotateY = differenceRotateY <= 0 ? differenceRotateY + 360 : differenceRotateY;//ここで向きと移動方向の差が求まる
            m_MoveLocalRotate = MoveLocalRotateCheck();
            anim.SetFloat("MoveX", m_MoveLocalRotate.x);
            anim.SetFloat("MoveY", m_MoveLocalRotate.y);
        }
	}

    private Vector2 MoveLocalRotateCheck()
    {
        Vector2 m_MoveRotate;
        float m_differenceRotateY;
        int[] m_mainus = new int[2];
        if (differenceRotateY <= 90)
        {
            m_mainus[0] = -1;
            m_mainus[1] = 1;
            m_differenceRotateY = differenceRotateY;
        }
        else if (differenceRotateY > 90&&differenceRotateY <= 180)
        {
            m_differenceRotateY = 90-(differenceRotateY-90);
            m_mainus[0] = -1;
            m_mainus[1] = -1;
            
        }
        else if (differenceRotateY > 180 && differenceRotateY <= 270)
        {
            m_differenceRotateY =differenceRotateY - 180;
            m_mainus[0] = 1;
            m_mainus[1] = -1;

        }
        else
        {
            m_differenceRotateY = 90 - (differenceRotateY - 270);
            m_mainus[0] = 1;
            m_mainus[1] = 1;

        }
        Debug.Log(m_differenceRotateY);
        m_MoveRotate.x = m_differenceRotateY / 90 * m_mainus[0];

        m_MoveRotate.y = (90- m_differenceRotateY) / 90 * m_mainus[1];

        return m_MoveRotate;
    }

    /// <summary>
    /// プレイヤーの移動方向をここに入力する。
    /// </summary>
    public Vector2 SetPlyerMoveRotate{
        set
        {
            moveRotate = value;
        }
    }
}
