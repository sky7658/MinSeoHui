using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LMS.Cards
{
    public class CardBase
    {
        // 카드 최대치
        public static int maxCardCount = 5;

        public static string cardPrefName = "RawImage";
<<<<<<< Updated upstream
        public static string[] cardImgNames = new string[] { "a", "b", "c", "d", "e", "f", "Heal", "Teleport", "DarkSight", "Critical" };

        // 카드 Img에 따른 Type 결정
        public static Dictionary<string, AttackType> atkTypes = new Dictionary<string, AttackType>() // atk카드
        { {cardImgNames[0], AttackType.a} };
        public static Dictionary<string, CommonType> comTypes = new Dictionary<string, CommonType>() // com카드
        { {cardImgNames[6], CommonType.HEAL}, {cardImgNames[7], CommonType.TELEPORT}, {cardImgNames[8], CommonType.DARKSIGHT}, {cardImgNames[9], CommonType.CRITICAL} };

        // Delay Time 관리
        public static Dictionary<AttackType, float> a_dtTime = new Dictionary<AttackType, float>()
        { {AttackType.a, 0.2f } };
        public static Dictionary<CommonType, float> c_dtTime = new Dictionary<CommonType, float>()
        { {CommonType.HEAL, 0.2f }, {CommonType.TELEPORT, 2f }, {CommonType.DARKSIGHT, 0.2f }, {CommonType.CRITICAL, 0.2f } };

        // Execute Time 관리
        public static Dictionary<AttackType, float> a_etTime = new Dictionary<AttackType, float>()
        { {AttackType.a, 0.2f } };
        public static Dictionary<CommonType, float> c_etTime = new Dictionary<CommonType, float>()
        { {CommonType.HEAL, 0.2f }, {CommonType.TELEPORT, 0.2f }, {CommonType.DARKSIGHT, 0.2f }, {CommonType.CRITICAL, 0.2f } };
=======
        public static string[] cardImgNames = new string[] { "Single", "Multiple", "Spray", "Floor", "Explosion", "Special", "Heal", "Teleport", "DarkSight", "Critical" };
        public static string[] cardImgNames1 = new string[] { "LionRoar", "Meteors", "Slashes", "Heal" };


        // 카드 Img에 따른 Type 결정
        public static Dictionary<string, SkillType> skillTypes = new Dictionary<string, SkillType>()
        {
            {cardImgNames[0], SkillType.SINGLE}, {cardImgNames[1], SkillType.MULTIPLE}, {cardImgNames[2], SkillType.SPRAY},
            {cardImgNames[3], SkillType.FLOOR}, {cardImgNames[4], SkillType.EXPLOSION}, {cardImgNames[5], SkillType.SPECIAL},
            {cardImgNames[6], SkillType.HEAL}, {cardImgNames[7], SkillType.TELEPORT}, {cardImgNames[8], SkillType.DARKSIGHT}, {cardImgNames[9], SkillType.CRITICAL}
        };

        // Delay Time 관리
        public static Dictionary<SkillType, float> delayTimes = new Dictionary<SkillType, float>()
        {
            {SkillType.SINGLE, 0.2f}, {SkillType.MULTIPLE, 0.2f}, {SkillType.SPRAY, 1f},
            {SkillType.FLOOR, 1f}, {SkillType.EXPLOSION, 1f}, {SkillType.SPECIAL, 1f},
            {SkillType.HEAL, 0.2f}, {SkillType.TELEPORT, 2f}, {SkillType.DARKSIGHT, 0.2f}, {SkillType.CRITICAL, 0.2f}
        };

        // Execute Time 관리
        public static Dictionary<SkillType, float> executeTimes = new Dictionary<SkillType, float>()
        {
            {SkillType.SINGLE, 0.2f}, {SkillType.MULTIPLE, 0.2f}, {SkillType.SPRAY, 1f},
            {SkillType.FLOOR, 0.2f}, {SkillType.EXPLOSION, 0.2f}, {SkillType.SPECIAL, 0.2f},
            {SkillType.HEAL, 0.2f}, {SkillType.TELEPORT, 2f}, {SkillType.DARKSIGHT, 0.2f}, {SkillType.CRITICAL, 0.2f}
        };
>>>>>>> Stashed changes
    }
    public class CardInfo
    {
        public float executeTime { get; set; }
        public float delayTime { get; set; }
        public float spendMP { get; set; }
        public int count { get; set; }
        public Grade grade { get; set; }
        public SkillType type { get; set; }
        public CardProperty property { get; set; }

        // Card의 기본 정보 셋팅
        public CardInfo(float spendMP, int count, Grade grade, SkillType type, CardProperty property = CardProperty.NONE)
        {
            executeTime = CardBase.executeTimes[type];
            delayTime = CardBase.delayTimes[type];
            this.spendMP = spendMP;
            this.count = count;
            this.grade = grade;
            this.type = type;
            this.property = property;
        }
    }

    // 레어도 및 아이템 개수
    public enum Grade { NORMAL = 3, RARE = 2, EPIC = 1 }
<<<<<<< Updated upstream
    // AttackCard
    public enum AttackType { a/*난사공격*/, b/*단일공격*/, c/*다중공격*/, d/*장판공격*/, e/*폭발공격*/, f/*필살기*/ }
    // CommonCard
    public enum CommonType { HEAL, TELEPORT, DARKSIGHT, CRITICAL }
=======
    // 카드 스킬 타입
    public enum SkillType { SPRAY/*난사공격*/, SINGLE/*단일공격*/, MULTIPLE/*다중공격*/, FLOOR/*장판공격*/, EXPLOSION/*폭발공격*/, SPECIAL/*필살기*/,
        HEAL, TELEPORT, DARKSIGHT, CRITICAL }
    //public enum SkillType { LIONROAR, METEORS, SLASHES, HEAL} // 이게 찐임
    // 카드 속성
    public enum CardProperty { NONE, FIRE, ICE, POISON } // 불 얼음 독
>>>>>>> Stashed changes
}