using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LMS.Cards;

namespace LMS.UI
{
    public class PlayerUIManger
    {
        private CardHandler cHandler;
        private Text infoText;

        public PlayerUIManger()
        {
            cHandler = new CardHandler();
        }
        public void CreateInfoText()
        {
            if(infoText == null)
            {
                var newText = GameObject.Instantiate(Manager.GameManager.Instance.ResourceLoadObj("Text")).GetComponent<Text>();
                newText.transform.parent = Manager.DataManager.Instance.test;
                newText.rectTransform.anchoredPosition = new Vector3(0, 0, 0);
                infoText = newText;
            }
        }
        /// <summary>
        /// Card List에 카드를 추가합니다.
        /// </summary>
        public void PushCard(int index, CardProperty property = CardProperty.NONE) => cHandler.PushCard(index, property);
        /// <summary>
        /// Card List에 저장된 특정 카드를 삭제합니다.
        /// </summary>
        /// <param name="index"></param>
        public void PopCard() => cHandler.PopCard(infoText);
        /// <summary>
        /// 선택 된 카드를 사용합니다.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="direction"></param>
        public void UseCard(GameObject obj, Vector3 direction) => cHandler.UseCard(obj, direction, infoText);
        /// <summary>
        /// Card List에 있는 특정 카드를 선택합니다.
        /// </summary>
        /// <param name="index"></param>
        public void SelectCard(int index) => cHandler.SelectCard(infoText, index);
    }
}


