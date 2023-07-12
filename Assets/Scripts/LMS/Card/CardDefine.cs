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

        public static string[] cardImgNames = new string[] { "LionRoar", "Meteors", "Slashes", "Heal" };


        // 카드 Img에 따른 Type 결정
        public static Dictionary<string, SkillType> skillTypes = new Dictionary<string, SkillType>()
        {
            {cardImgNames[0], SkillType.LIONROAR}, {cardImgNames[1], SkillType.METEORS}, {cardImgNames[2], SkillType.SLASHES},
            {cardImgNames[3], SkillType.HEAL}
        };

        // Delay Time 관리
        public static Dictionary<SkillType, float> delayTimes = new Dictionary<SkillType, float>()
        {
            {SkillType.LIONROAR, 0.2f}, {SkillType.METEORS, 0.2f}, {SkillType.SLASHES, 1f},
            {SkillType.HEAL, 0.2f}
        };

        // Execute Time 관리
        public static Dictionary<SkillType, float> executeTimes = new Dictionary<SkillType, float>()
        {
            {SkillType.LIONROAR, 0.2f}, {SkillType.METEORS, 0.2f}, {SkillType.SLASHES, 1f},
            {SkillType.HEAL, 0.2f}
        };

        // 기존 코드들
        /*public static Dictionary<string, SkillType> skillTypes = new Dictionary<string, SkillType>()
        {
            {cardImgNames[0], SkillType.SINGLE}, {cardImgNames[1], SkillType.MULTIPLE}, {cardImgNames[2], SkillType.SPRAY},
            {cardImgNames[3], SkillType.FLOOR}, {cardImgNames[4], SkillType.EXPLOSION}, {cardImgNames[5], SkillType.SPECIAL},
            {cardImgNames[6], SkillType.HEAL}, {cardImgNames[7], SkillType.TELEPORT}, {cardImgNames[8], SkillType.DARKSIGHT}, {cardImgNames[9], SkillType.CRITICAL}
        };

        public static Dictionary<SkillType, float> delayTimes = new Dictionary<SkillType, float>()
        {
            {SkillType.SINGLE, 0.2f}, {SkillType.MULTIPLE, 0.2f}, {SkillType.SPRAY, 1f},
            {SkillType.FLOOR, 1f}, {SkillType.EXPLOSION, 1f}, {SkillType.SPECIAL, 1f},
            {SkillType.HEAL, 0.2f}, {SkillType.TELEPORT, 2f}, {SkillType.DARKSIGHT, 0.2f}, {SkillType.CRITICAL, 0.2f}
        };

        public static Dictionary<SkillType, float> executeTimes = new Dictionary<SkillType, float>()
        {
            {SkillType.SINGLE, 0.2f}, {SkillType.MULTIPLE, 0.2f}, {SkillType.SPRAY, 1f},
            {SkillType.FLOOR, 0.2f}, {SkillType.EXPLOSION, 0.2f}, {SkillType.SPECIAL, 0.2f},
            {SkillType.HEAL, 0.2f}, {SkillType.TELEPORT, 2f}, {SkillType.DARKSIGHT, 0.2f}, {SkillType.CRITICAL, 0.2f}
        };*/
    }
    public class CardInfo
    {
        public string name { get; set; }
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
            name = CardBase.cardImgNames[(int)type];
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
    // 카드 스킬 타입
    public enum SkillType { LIONROAR, METEORS, SLASHES, HEAL} // 이게 찐임
    /*public enum SkillType
    {
        SPRAY, SINGLE, MULTIPLE, FLOOR, EXPLOSION, SPECIAL,
        HEAL, TELEPORT, DARKSIGHT, CRITICAL
    }*/
    // 카드 속성
    public enum CardProperty { NONE, FIRE, ICE, POISON } // 불 얼음 독
}