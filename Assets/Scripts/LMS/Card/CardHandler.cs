using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LMS.UI;
using LMS.Utility;

namespace LMS.Cards
{
    public class CardHandler
    {
        // CardHandler Ŭ������ �����ڷ� ȣ���� ���� -> Update�� ������� ����
        private List<Card> cards;
        // ī�� UI�� ����
        private CardUI cardUI;

        public CardHandler()
        {
            Initialized();
        }

        private void Initialized()
        {
            cards = new List<Card>();
            cardUI = new CardUI();
            selectCardNum = -1;
        }

        private int selectCardNum;
        /// <summary>
        /// Card List�� �ִ� Ư�� ī�带 �����մϴ�.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="selectNum"></param>
        public void SelectCard(Text text, int selectNum)
        {
            if(cards.Count < selectNum + 1)
            {
                Debug.Log("�׷� ī��� �������� ����");
                return;
            }
            if(selectCardNum == -1) // ���� ���õ� ī�尡 ���� ��
            {
                selectCardNum = selectNum;
            }
            else // ���� ���õ� ī�尡 ���� ��
            {
                if (selectCardNum == selectNum) // ���� ī�� ��ȣ�� �ι� �����ϸ� �� Ȱ��ȭ
                {
                    cards[selectCardNum].HighlightTrigger();
                    Debug.Log(selectCardNum + "�� ī�� ��Ȱ��ȭ");
                    selectCardNum = -1;
                    cardUI.UpdateInfo(text);
                    return;
                }
                else // �ٸ� ī�� ��ȣ�� ���� �� ���� ī��� ��Ȱ��ȭ �ϰ� ���� ī�� ��ȣ�� Ȱ��ȭ
                {
                    cards[selectCardNum].HighlightTrigger();
                    Debug.Log(selectCardNum + "�� ī�� ��Ȱ��ȭ");
                    selectCardNum = selectNum;
                }
            }

            cardUI.UpdateInfo(text, cards[selectCardNum].cardInfo);
            cards[selectCardNum].HighlightTrigger();
            Debug.Log(selectCardNum + "�� ī�� Ȱ��ȭ");
        }

        /// <summary>
        /// Card List�� ī�带 �߰��մϴ�.
        /// </summary>
        public void PushCard(int index, CardProperty property)
        {
            if(cards.Count >= CardBase.maxCardCount)
            {
                Debug.Log("�ִ� ī�� ���� �ʰ�");
                return;
            }

            // test �ڵ�
            var pref = Manager.GameManager.Instance.ResourceLoadObj(CardBase.cardPrefName);
            if (pref == null)
            {
                Debug.Log("�������� �ҷ��� �� �����ϴ�.");
                return;
            }

            var newCard = ObjectPool.Instance.GetObject<Card>(CardBase.cardPrefName);
            newCard.Initialized(CardBase.cardImgNames[index], property);
            cards.Add(newCard);
            cardUI.CardAligment(cards);
        }

        /// <summary>
        /// Card List�� ����� Ư�� ī�带 �����մϴ�.
        /// </summary>
        /// <param name="index"></param>
        public void PopCard(Text text)
        {
            // �ε����� ����Ʈ ������ ����� ���
            if (cards.Count < selectCardNum + 1 || selectCardNum == -1)
            {
                Debug.Log("�׷� ī��� �������� ����");
                return;
            }

            cardUI.UpdateInfo(text);
            ObjectPool.Instance.ReturnObject(cards[selectCardNum].gameObject, CardBase.cardPrefName);
            cards.RemoveAt(selectCardNum);
            cardUI.CardAligment(cards);
            selectCardNum = -1;
        }

        /// <summary>
        /// ���� �� ī�带 ����մϴ�.
        /// </summary>
        public void UseCard(GameObject obj, Vector3 direction, Text text)
        {
            // �ε����� ����Ʈ ������ ����� ���
            if (cards.Count < selectCardNum + 1 || selectCardNum == -1)
            {
                Debug.Log("�׷� ī��� �������� ����");
                return;
            }

            // ī�带 ����� �� ���� ������ ���
            // ex) if(player.state != PlayerState.IDLE) return;

            // ī�� ��ų�� ��Ÿ���� ���
            if (cards[selectCardNum].delayEnabled)
            {
                Debug.Log("ī�� ��Ÿ�� �Դϴ�.");
                return;
            }

            // ī���� ��ų�� ����
            cards[selectCardNum].ExecuteSkill(obj, direction);

            // Ƚ�� ������ �ִ� ī�常 Ƚ���� ����
            if (cards[selectCardNum].cardInfo.count > 0)
            {
                // ��� ���� ī�� ����
                if(--cards[selectCardNum].cardInfo.count == 0)
                {
                    PopCard(text);
                    return;
                }
                cardUI.UpdateInfo(text, cards[selectCardNum].cardInfo);
            }
        }
    }

}
