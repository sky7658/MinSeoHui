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

        public Transform parent;

        public CardHandler()
        {
            cards = new List<Card>();
        }

<<<<<<< Updated upstream
        void Update()
        {
            // ���� �ڵ�
            if(Input.GetKeyDown(KeyCode.A))
            {
                PushCard();
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                UseCard(0);
            }
        }

        public void PushCard()
=======
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
>>>>>>> Stashed changes
        {
            if(cards.Count >= CardBase.maxCardCount)
            {
                Debug.Log("�ִ� ī�� ���� �ʰ�");
                return;
            }

<<<<<<< Updated upstream
            // test �ڵ�
            var pref = Manager.GameManager.Instance.ResourceLoadObj(CardBase.cardPrefName);
            if (pref == null)
            {
                Debug.Log("�������� �ҷ��� �� �����ϴ�.");
                return;
            }

            var newCard = Instantiate(pref.GetComponent<Card>());
            newCard.Initicalize(CardBase.cardImgNames[7]);
            newCard.transform.parent = parent;
=======
            var newCard = ObjectPool.Instance.GetObject<Card>(CardBase.cardPrefName);
            newCard.Initialized(CardBase.cardImgNames[index], property);
>>>>>>> Stashed changes
            cards.Add(newCard);
            cardUI.CardAligment(cards);
        }

<<<<<<< Updated upstream
        public void PopCard(int index)
=======
        /// <summary>
        /// Card List�� ����� Ư�� ī�带 �����մϴ�.
        /// </summary>
        /// <param name="index"></param>
        public void PopCard(Text text)
>>>>>>> Stashed changes
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

<<<<<<< Updated upstream
        public void UseCard(int index)
=======
        /// <summary>
        /// ���� �� ī�带 ����մϴ�.
        /// </summary>
        public void UseCard(GameObject obj, Vector3 direction, Text text)
>>>>>>> Stashed changes
        {
            // �ε����� ����Ʈ ������ ����� ���
            if (cards.Count < index + 1)
            {
                Debug.Log("�׷� ī��� �������� ����");
                return;
            }

            // ī�带 ����� �� ���� ������ ���
            // ex) if(player.state != PlayerState.IDLE) return;

            // ī�� ��ų�� ��Ÿ���� ���
            if (cards[index].delayEnabled)
            {
                Debug.Log("ī�� ��Ÿ�� �Դϴ�.");
                return;
            }

            // ī���� ��ų�� ����
            cards[index].ExecuteSkill(this.gameObject);

            // Ƚ�� ������ �ִ� ī�常 Ƚ���� ����
            if (cards[index].cardInfo.count > 0)
            {
                // ��� ���� ī�� ����
                if(--cards[index].cardInfo.count == 0)
                {
<<<<<<< Updated upstream
                    PopCard(index);
=======
                    PopCard(text);
                    return;
>>>>>>> Stashed changes
                }
                cardUI.UpdateInfo(text, cards[selectCardNum].cardInfo);
            }
        }
    }

}
