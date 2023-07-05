using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LMS.Cards
{
    public class AttackCard : MonoBehaviour
    {
<<<<<<< Updated upstream
        [SerializeField] private AttackType attackType;

        public override void Initicalize(string imgName)
        {
            base.Initicalize(imgName);
            cardInfo = new CardInfo(CardBase.a_etTime[attackType], CardBase.a_dtTime[attackType], 1f, -1, Grade.EPIC);
            attackType = CardBase.atkTypes[imgName];
            SetCardSkill();
        }

        protected override void SetCardSkill()
        {
            switch (attackType)
            {
                case AttackType.a:
                    break;
            }
            skill = CardSkill.HpHeal;
        }
=======
       
>>>>>>> Stashed changes
    }
}

