using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LMS.UI;

public class TestScript : MonoBehaviour
{
    PlayerUIManger playerUI = new PlayerUIManger();
    private void Start()
    {
        playerUI.CreateInfoText();
    }
        
    void Update()
    {
        // 연습 코드
        if (Input.GetKeyDown(KeyCode.A))
        {
            int rand = Random.Range(0, 4);
            //int rand2 = Random.Range(1, 4);
            //playerUI.PushCard(rand, (LMS.Cards.CardProperty)rand2);
            playerUI.PushCard(3, (LMS.Cards.CardProperty)3);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            playerUI.SetHand();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            playerUI.ComboAttacks(gameObject, null);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            playerUI.SelectCard(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            playerUI.SelectCard(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            playerUI.SelectCard(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            playerUI.SelectCard(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            playerUI.SelectCard(4);
        }

        if (Input.GetMouseButtonDown(0))
        {
            playerUI.UseCard(gameObject, transform.forward);
        }
        if (Input.GetMouseButtonDown(1))
        {
            playerUI.PopCard();
        }
    }
}
