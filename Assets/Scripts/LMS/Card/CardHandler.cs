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
        // CardHandler 클래스도 생성자로 호출할 예정 -> Update를 사용하지 않음
        private List<Card> cards;
        // 카드 UI를 관리
        private CardUI cardUI;

        public Transform parent;

        public CardHandler()
        {
            cards = new List<Card>();
        }

<<<<<<< Updated upstream
        void Update()
        {
            // 연습 코드
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
        /// Card List에 있는 특정 카드를 선택합니다.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="selectNum"></param>
        public void SelectCard(Text text, int selectNum)
        {
            if(cards.Count < selectNum + 1)
            {
                Debug.Log("그런 카드는 존재하지 않음");
                return;
            }
            if(selectCardNum == -1) // 현재 선택된 카드가 없을 때
            {
                selectCardNum = selectNum;
            }
            else // 현재 선택된 카드가 있을 때
            {
                if (selectCardNum == selectNum) // 같은 카드 번호를 두번 선택하면 비 활성화
                {
                    cards[selectCardNum].HighlightTrigger();
                    Debug.Log(selectCardNum + "번 카드 비활성화");
                    selectCardNum = -1;
                    cardUI.UpdateInfo(text);
                    return;
                }
                else // 다른 카드 번호를 선택 시 이전 카드는 비활성화 하고 현재 카드 번호를 활성화
                {
                    cards[selectCardNum].HighlightTrigger();
                    Debug.Log(selectCardNum + "번 카드 비활성화");
                    selectCardNum = selectNum;
                }
            }

            cardUI.UpdateInfo(text, cards[selectCardNum].cardInfo);
            cards[selectCardNum].HighlightTrigger();
            Debug.Log(selectCardNum + "번 카드 활성화");
        }

        /// <summary>
        /// Card List에 카드를 추가합니다.
        /// </summary>
        public void PushCard(int index, CardProperty property)
>>>>>>> Stashed changes
        {
            if(cards.Count >= CardBase.maxCardCount)
            {
                Debug.Log("최대 카드 갯수 초과");
                return;
            }

<<<<<<< Updated upstream
            // test 코드
            var pref = Manager.GameManager.Instance.ResourceLoadObj(CardBase.cardPrefName);
            if (pref == null)
            {
                Debug.Log("프리팹을 불러올 수 없습니다.");
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
        /// Card List에 저장된 특정 카드를 삭제합니다.
        /// </summary>
        /// <param name="index"></param>
        public void PopCard(Text text)
>>>>>>> Stashed changes
        {
            // 인덱스가 리스트 범위를 벗어나는 경우
            if (cards.Count < selectCardNum + 1 || selectCardNum == -1)
            {
                Debug.Log("그런 카드는 존재하지 않음");
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
        /// 선택 된 카드를 사용합니다.
        /// </summary>
        public void UseCard(GameObject obj, Vector3 direction, Text text)
>>>>>>> Stashed changes
        {
            // 인덱스가 리스트 범위를 벗어나는 경우
            if (cards.Count < index + 1)
            {
                Debug.Log("그런 카드는 존재하지 않음");
                return;
            }

            // 카드를 사용할 수 없는 상태일 경우
            // ex) if(player.state != PlayerState.IDLE) return;

            // 카드 스킬의 쿨타임일 경우
            if (cards[index].delayEnabled)
            {
                Debug.Log("카드 쿨타임 입니다.");
                return;
            }

            // 카드의 스킬을 실행
            cards[index].ExecuteSkill(this.gameObject);

            // 횟수 제한이 있는 카드만 횟수를 차감
            if (cards[index].cardInfo.count > 0)
            {
                // 모두 사용시 카드 삭제
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
