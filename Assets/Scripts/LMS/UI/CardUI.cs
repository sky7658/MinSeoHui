using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
<<<<<<< Updated upstream:Assets/Scripts/LMS/Card/CardUI.cs
=======
using LMS.Cards;
>>>>>>> Stashed changes:Assets/Scripts/LMS/UI/CardUI.cs

namespace LMS.UI
{
    public class CardUI
    {
        Vector3 leftLocalPos;
        Vector3 leftLocalRot;
        Vector3 rightLocalPos;
        Vector3 rightLocalRot;
        public CardUI()
        {
            leftLocalPos = new Vector3(-145, -145, 0);
            leftLocalRot = new Vector3(0, 0, 15);
            rightLocalPos = new Vector3(145, -145, 0);
            rightLocalRot = new Vector3(0, 0, -15);

            infoDefaultText = "Grade : {0}\nSpend Mp : {1}\nCount : {2}\nProperty : {3}\nInfo : {4}";
        }
        public void CardAligment(List<Card> cards)
        {
            float[] objLerps = new float[cards.Count];

            // 위치 리턴
            switch (cards.Count)
            {
                case 1:
                    objLerps = new float[] { 0.5f };
                    break;
                case 2:
                    objLerps = new float[] { 0.27f, 0.73f };
                    break;
                default:
                    float interval = 1f / (cards.Count - 1);
                    for (int i = 0; i < cards.Count; i++)
                    {
                        objLerps[i] = interval * i;
                    }
                    break;
            }

            for (int i = 0; i < cards.Count; i++)
            {
                var targetPos = Vector3.Lerp(leftLocalPos, rightLocalPos, objLerps[i]);
                var targetRot = Quaternion.identity;
                if (cards.Count > 2)
                {
                    float curve = Mathf.Sqrt(Mathf.Pow(1, 2) - Mathf.Pow(objLerps[i] - 0.5f, 2));
                    targetPos.y += curve;
                    targetRot = Quaternion.Slerp(Quaternion.Euler(leftLocalRot), Quaternion.Euler(rightLocalRot), objLerps[i]);
                }
                cards[i].MoveTo(targetPos, targetRot, 0.2f);
            }
        }

        private string infoDefaultText;
        public void UpdateInfo(Text text, Cards.CardInfo info = null)
        {
            if (text == null)
            {
                Debug.Log("Text가 없습니다.");
                return;
            }
            if(info == null)
            {
                text.text = string.Format(infoDefaultText, "", "", "", "", "");
                return;
            }
            text.text = string.Format(infoDefaultText, info.grade, info.spendMP, info.count, info.property, "ㅎㅇ");
        }
    }
    public class CardAction
    {
        public static IEnumerator MoveToAction(GameObject obj, Vector3 targetPos, Quaternion targetRot, float duration)
        {
            Vector3 startPos = obj.transform.localPosition;
            Quaternion startRot = obj.transform.localRotation;

            float elapsed = 0.0f;
            while (elapsed < duration)
            {
<<<<<<< Updated upstream:Assets/Scripts/LMS/Card/CardUI.cs
                elapsed += Time.smoothDeltaTime;
                obj.transform.localPosition = Vector3.Lerp(startPos, targetPos, elapsed / duration);
                obj.transform.localRotation = Quaternion.Slerp(startRot, targetRot, elapsed / duration);
=======
                if (obj == null) yield break;
                _elapsed += Time.smoothDeltaTime;
                obj.transform.localPosition = Vector3.Lerp(_startPos, targetPos, _elapsed / duration);
                obj.transform.localRotation = Quaternion.Slerp(_startRot, targetRot, _elapsed / duration);
>>>>>>> Stashed changes:Assets/Scripts/LMS/UI/CardUI.cs
                yield return null;
            }

            if(obj == null) yield break;

            obj.transform.localPosition = targetPos;
            obj.transform.localRotation = targetRot;

            yield break;
        }

        public static IEnumerator DelayAction(Card card)
        {
            float t = 0f;

            while(t < card.cardInfo.delayTime)
            {
<<<<<<< Updated upstream:Assets/Scripts/LMS/Card/CardUI.cs
                card.cardMask.transform.localScale = new Vector3(1f, (card.cardInfo.delayTime - t) / card.cardInfo.delayTime, 1f);
                t += Time.smoothDeltaTime;
=======
                if (card == null) yield break;
                card.cardMask.transform.localScale = new Vector3(1f, (card.cardInfo.delayTime - _t) / card.cardInfo.delayTime, 1f);
                _t += Time.smoothDeltaTime;
>>>>>>> Stashed changes:Assets/Scripts/LMS/UI/CardUI.cs
                yield return null;
            }

            if (card == null) yield break;

            card.cardMask.transform.localScale = new Vector3(1f, 1f, 1f);
            card.cardMask.gameObject.SetActive(false);
            card.delayEnabled = false;

            yield break;
        }
<<<<<<< Updated upstream:Assets/Scripts/LMS/Card/CardUI.cs
=======

        public static IEnumerator SelectAction(Card card, bool isSelect = false)
        {
            float _t = 0f;
            Vector3 _scale;

            if(!isSelect)
            {
                while(_t < 0.2f)
                {
                    if(card == null) yield break;
                    card.cardImg.transform.localScale = new Vector3(1f + _t, 1f + _t, 1f);
                    _t += Time.smoothDeltaTime;
                    yield return null;
                }
                _scale = new Vector3(1.2f, 1.2f, 1f);
            }
            else
            {
                while (_t < 0.2f)
                {
                    if (card == null) yield break;
                    card.cardImg.transform.localScale = new Vector3(1.2f - _t, 1.2f - _t, 1f);
                    _t += Time.smoothDeltaTime;
                    yield return null;
                }
                _scale = new Vector3(1f, 1f, 1f);
            }

            if (card == null) yield break;
            card.cardMask.transform.localScale = _scale;

            yield break;
        }
>>>>>>> Stashed changes:Assets/Scripts/LMS/UI/CardUI.cs
    }
}