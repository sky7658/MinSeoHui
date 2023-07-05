using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LMS.Cards
{
    public class CommonCard : MonoBehaviour
    {
<<<<<<< Updated upstream
        [SerializeField] private CommonType commonType;

        public override void Initicalize(string imgName)
        {
            base.Initicalize(imgName);
            cardInfo = new CardInfo(CardBase.c_etTime[commonType], CardBase.c_dtTime[commonType], 1f, 2, Grade.EPIC);
            commonType = CardBase.comTypes[imgName];
            SetCardSkill();
        }

        protected override void SetCardSkill()
        {
            switch (commonType)
            {
                case CommonType.HEAL:
                    skill = CardSkill.HpHeal;
                    break;
                case CommonType.TELEPORT:
                    skill = CardSkill.Teleport;
                    break;
                case CommonType.DARKSIGHT:
                    skill = CardSkill.DarkSight;
                    break;
                case CommonType.CRITICAL:
                    skill = CardSkill.Critical;
                    break;
            }
        }
=======
        
>>>>>>> Stashed changes
    }
}
