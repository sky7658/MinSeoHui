using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LMS.UI;

namespace LMS.Cards
{
    public class Card : MonoBehaviour
    {
        public CardInfo cardInfo;
        public RawImage cardImg { get; set; }
        public Image cardMask { get; set; }

        // ī�� ��ų
<<<<<<< Updated upstream
        public delegate IEnumerator ActionDelegate(GameObject obj, float executeTime);
        public ActionDelegate skill { get; set; }
        public bool delayEnabled { get; set; }
=======
        public delegate IEnumerator ActionDelegate(GameObject obj, Vector3 direction, CardInfo info);
        protected ActionDelegate skill { get; set; }
>>>>>>> Stashed changes


        private void Awake()
        {
            cardImg = GetComponent<RawImage>();
            cardMask = transform.GetChild(0).GetComponent<Image>();
        }

<<<<<<< Updated upstream
        public virtual void Initicalize(string imgName)
        {
            delayEnabled = false;
=======
        public void Initialized(string imgName, CardProperty property = CardProperty.NONE)
        {
            delayEnabled = false;
            isHighlight = false;

            cardImg.transform.localScale = new Vector3(1f, 1f, 1f);
            cardMask.transform.localScale = new Vector3(1f, 1f, 1f);

            cardMask.gameObject.SetActive(false);

>>>>>>> Stashed changes
            SetCardImg(imgName);
            cardInfo = new CardInfo(1f, 2, Grade.EPIC, CardBase.skillTypes[imgName], property);
            SetCardSkill();
        }

        private void SetCardSkill()
        {
            switch (cardInfo.type)
            {
                case SkillType.SINGLE:
                    skill = CardSkill.SingleFire;
                    break;
                case SkillType.MULTIPLE:
                    skill = CardSkill.MultipleFire;
                    break;
                case SkillType.SPRAY:
                    skill = CardSkill.SprayFire;
                    break;
                case SkillType.FLOOR:
                    skill = CardSkill.FloorFire;
                    break;
                case SkillType.EXPLOSION:
                    skill = CardSkill.Explosion;
                    break;
                case SkillType.SPECIAL:
                    skill = CardSkill.Special;
                    break;
                case SkillType.HEAL:
                    skill = CardSkill.HpHeal;
                    break;
            }
        }

        private void SetCardImg(string name)
        {
            cardImg.texture = Manager.GameManager.Instance.ResourceLoadImg(name);
            if(cardImg.texture == null)
            {
                Debug.Log("�̹����� �����ϴ�.");
                return;
            }
        }

        /// <summary>
        /// ī���� ��ų�� �������ִ� �Լ�
        /// </summary>
        /// <param name="obj"> ��ų�� ���� ���</param>
        public void ExecuteSkill(GameObject obj)
        {
            delayEnabled = true;
            if(cardInfo.count - 1 != 0) // ī�带 ������� �� ������ �����ִٸ� ����
            {
                cardMask.gameObject.SetActive(true);
                Manager.GameManager.Instance.ExecuteCoroutine(CardAction.DelayAction(this));
            }
<<<<<<< Updated upstream
            Manager.GameManager.Instance.ExecuteCoroutine(skill(obj, cardInfo.executeTime));
=======
            Manager.GameManager.Instance.ExecuteCoroutine(skill(obj, direction, cardInfo));
>>>>>>> Stashed changes
        }

        /// <summary>
        /// ī���� ��ġ�� �̵����ִ� �Լ�
        /// </summary>
        /// <param name="targetPos"> </param>
        /// <param name="targetRot"> </param>
        /// <param name="duration"> ���� �ð�</param>
        public void MoveTo(Vector3 targetPos, Quaternion targetRot, float duration)
        {
            Manager.GameManager.Instance.ExecuteCoroutine(CardAction.MoveToAction(gameObject, targetPos, targetRot, duration));
        }
    }
}
