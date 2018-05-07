using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour {
    [SerializeField]
    GameObject[] UIobj=new GameObject[2];

	public void GameOverEvent()
    {
        StartCoroutine("GameOverAction");
    }
    IEnumerator GameOverAction()
    {
        for (float m_pivet_y = UIobj[0].transform.localPosition.y; m_pivet_y >= 0; m_pivet_y -= 30)
        {
            UIobj[0].transform.localPosition = new Vector3(UIobj[0].transform.localPosition.x, m_pivet_y, UIobj[0].transform.localPosition.z);
            yield return null;
        }
        yield return new WaitForSeconds(0.3f);
        for (int i = 0;i<15;i++)
        {
            UIobj[0].transform.eulerAngles = new Vector3(0, 0,-i);
            yield return null;
        }
        UIobj[1].SetActive(true);
    }
}
