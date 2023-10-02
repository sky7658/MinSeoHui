using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LMS.UI;
using LMS.Utility;
using LMS.Manager;
using System;

namespace LMS.Cards
{
    public class CardHandler
    {
        // CardHandler 클래스도 생성자로 호출할 예정 -> Update를 사용하지 않음
        private List<Card> cards;
        // 카드 UI를 관리
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

            handType = true;

            attackLevel = 0;
            comboCount = 0;
            activeAtk = false;
        }

        public int GetCardCount() => cards.Count; // 현재 카드 갯수

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
        {
            if(cards.Count >= CardBase.maxCardCount)
            {
                Debug.Log("최대 카드 갯수 초과");
                return;
            }

            // test 코드
            var pref = GameManager.Instance.ResourceLoadObj(CardBase.cardPrefName);
            if (pref == null)
            {
                Debug.Log("프리팹을 불러올 수 없습니다.");
                return;
            }

            // 같은 카드가 드로우 될 경우 경험치 Up
            foreach(var card in cards)
            {
                if(card.cardInfo.name == CardBase.cardImgNames[index])
                {
                    card.ExpUpdate(20f);
                    attackLevel = AttackLevelUpdate();
                    return;
                }
            }

            var newCard = ObjectPool.Instance.GetObject<Card>(CardBase.cardPrefName);
            UtilFunction.TurnOnOff(ObjectPool.Instance.objectInfos[0], newCard.gameObject, true);

            newCard.Initialized(CardBase.cardImgNames[index], property);
            cards.Add(newCard);
            cardUI.CardAligment(cards, handType);
        }

        /// <summary>
        /// Card List에 저장된 특정 카드를 삭제합니다.
        /// </summary>
        /// <param name="index"></param>
        public void PopCard(Text text)
        {
            // 인덱스가 리스트 범위를 벗어나는 경우
            if (cards.Count < selectCardNum + 1 || selectCardNum == -1)
            {
                Debug.Log("그런 카드는 존재하지 않음");
                return;
            }

            cardUI.UpdateInfo(text);

            ObjectPool.Instance.ReturnObject(cards[selectCardNum], CardBase.cardPrefName);
            UtilFunction.TurnOnOff(ObjectPool.Instance.objectInfos[0], cards[selectCardNum].gameObject);

            cards.RemoveAt(selectCardNum);
            cardUI.CardAligment(cards, handType);
            selectCardNum = -1;
        }

        /// <summary>
        /// 선택 된 카드를 사용합니다.
        /// </summary>
        public void UseCard(GameObject obj, Vector3 direction, Text text)
        {
            // 인덱스가 리스트 범위를 벗어나는 경우
            if (cards.Count < selectCardNum + 1 || selectCardNum == -1)
            {
                Debug.Log("그런 카드는 존재하지 않음");
                return;
            }

            // 카드를 사용할 수 없는 상태일 경우
            // ex) if(player.state != PlayerState.IDLE) return;

            // 카드 스킬의 쿨타임일 경우
            if (cards[selectCardNum].delayEnabled)
            {
                Debug.Log("카드 쿨타임 입니다.");
                return;
            }

            // 카드의 스킬을 실행
            cards[selectCardNum].ExecuteSkill(obj, direction);

            // 횟수 제한이 있는 카드만 횟수를 차감
            if (cards[selectCardNum].cardInfo.count > 0)
            {
                // 모두 사용시 카드 삭제
                if(--cards[selectCardNum].cardInfo.count == 0)
                {
                    PopCard(text);
                    return;
                }
                cardUI.UpdateInfo(text, cards[selectCardNum].cardInfo);
            }
        }

        private bool handType;
        /// <summary>
        /// Hand에 있는 Card List를 보이게 하거나 숨기게 해줍니다.
        /// </summary>
        public void SetHand()
        {
            if (handType)
            {
                handType = false;
            }
            else
            {
                handType = true;
            }

            cardUI.CardAligment(cards, handType);
        }

        private int attackLevel;
        private int comboCount;
        private Coroutine coroutine;
        private bool activeAtk;
        public void ComboAttacks(GameObject obj, Action del)
        {
            if (activeAtk == true) return; // 공격 딜레이 중이라면 return

            float _attackDamage = CardBase.attackDamage[attackLevel];

            comboCount++;
            activeAtk = true;

            del();

            UtilFunction.OffCoroutine(coroutine);

            if(comboCount < 3)
            {
                GameManager.Instance.ExecuteCoroutine(CardSkill.SingleFire(obj, obj.transform.forward, _attackDamage));
                coroutine = GameManager.Instance.ExecuteCoroutine(SkillAction.RetentionTime(CardBase.comboTimeThreshold, () => comboCount = 0));
            }
            else
            {
                GameManager.Instance.ExecuteCoroutine(CardSkill.MultipleFire(obj, obj.transform.forward, _attackDamage * 2));
                comboCount = 0;
            }

            GameManager.Instance.ExecuteCoroutine(SkillAction.RetentionTime(CardBase.basicAtkDelay, () => activeAtk = false));
        }

        private int AttackLevelUpdate()
        {
            if (cards.Count != CardBase.maxCardCount) return attackLevel;

            int _minValue = 5;
            foreach(var card in cards)
            {
                if(card.cardInfo.cardLevel < _minValue) _minValue = card.cardInfo.cardLevel;
            }

            return _minValue;
        }
    }

}
