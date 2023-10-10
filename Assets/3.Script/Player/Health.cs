using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int curhealth;
    public int numOfHearts;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emtyHeart;

    private void Start()
    {
        //변수 초기화
        //curhealth = 3; //현재체력
        //numOfHearts = 3; //최대체력
    }
    private void Update()
    {
        //예외처리 - 현재체력> 최대체력일시 같게
        if (curhealth > numOfHearts)
        {
            curhealth = numOfHearts;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < curhealth) //i가 현재체력보다 작을 경우
            {
                hearts[i].sprite = fullHeart;//찬 하트
            }
            else
            {
                hearts[i].sprite = emtyHeart;//비워진 하트
            }
            if (i < numOfHearts) //i가 최대체력보다 작을 경우
            {
                hearts[i].enabled = true; //활성화
            }
            else
            {
                hearts[i].enabled = false; //비활성화
            }
        }
    }
}
