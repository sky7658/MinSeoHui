using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace LMS.Cards
{
    public class CardBase
    {
        // 카드 최대치
        public static int maxCardCount = 5;

        public static readonly Vector3 handDistance = new Vector3(0f, 100f, 0f);     // 핸드 숨겨지는 거리
        public static readonly Vector3 InitCardPos = new Vector3(-577f, -210f, 0f);  // 카드 생성 위치
        public static readonly Vector3 characterHeight = new Vector3(0f, 1.5f, 0f);  // 캐릭터 높이
        public static readonly Vector3 characterSkillHeight = new Vector3(0f, -0.416365f, 0f);

        public static readonly string cardPrefName = "Card"; // 카드 프리팹 이름
        public static readonly string[] cardImgNames = new string[] { "LionRoar", "Meteors", "Slashes", "Spray", "Heal" };  // 카드 이미지 이름

        public static readonly float basicAtkDelay = 0.5f;       // 기본 공격 딜레이
        public static readonly float comboTimeThreshold = 0.7f;  // 콤보 시 유지되는 시간
        public static readonly float[] attackDamage = new float[] { 5f, 7f, 10f, 12f, 15f };
        public static readonly float[] cardLevelMaxExp = new float[] { 100f, 200f, 300f, 400f }; // Max Level 5 (수정 가능)

        public static readonly int[] meteorsCount = new int[] { 5, 8, 10, 15, 20 };
        public static readonly int[] slashesCount = new int[] { 4, 6, 8, 10, 12 };
        public static readonly int[] sprayCount = new int[] { 5, 7, 10, 12, 15 };


        public static Dictionary<SkillType, float[]> cardLevelDamage = new Dictionary<SkillType, float[]>()
        {
            { SkillType.LIONROAR, new float[] { 20f, 30f, 40f, 50f, 70f } },
            { SkillType.METEORS, new float[] { 5f, 5f, 6f, 6f, 6f } },
            { SkillType.SLASHES, new float[] { 3f, 4f, 5f, 6f, 10f } },
            { SkillType.SPRAY, new float[] { 5f, 6f, 6f, 7f, 8f } }
        };

        // 카드 Img에 따른 Type 결정
        public static Dictionary<string, SkillType> skillTypes = new Dictionary<string, SkillType>()
        {
            {cardImgNames[0], SkillType.LIONROAR}, {cardImgNames[1], SkillType.METEORS}, {cardImgNames[2], SkillType.SLASHES}, {cardImgNames[3], SkillType.SPRAY},
            {cardImgNames[4], SkillType.HEAL}
        };

        // Delay Time 관리
        public static Dictionary<SkillType, float> delayTimes = new Dictionary<SkillType, float>()
        {
            {SkillType.LIONROAR, 3f}, {SkillType.METEORS, 7f}, {SkillType.SLASHES, 3f}, {SkillType.SPRAY, 3f},
            {SkillType.HEAL, 0.2f}
        };

        // Execute Time 관리
        public static Dictionary<SkillType, float> executeTimes = new Dictionary<SkillType, float>()
        {
            {SkillType.LIONROAR, 0.2f}, {SkillType.METEORS, 0.2f}, {SkillType.SLASHES, 1f}, {SkillType.SPRAY, 1f},
            {SkillType.HEAL, 0.2f}
        };
    }
    public class CardInfo
    {
        public string name { get; set; }
        public float executeTime { get; set; }
        public float delayTime { get; set; }
        public float spendMP { get; set; }
        public int count { get; set; }
        public int cardLevel { get; set; }
        public float currentExp { get; set; }
        public float maxExp { get; set; }
        public float damage { get; set; }
        public Grade grade { get; set; }
        public SkillType type { get; set; }
        public CardProperty property { get; set; }

        // Card의 기본 정보 셋팅
        public CardInfo(float spendMP, int count, Grade grade, SkillType type, int cardLevel, float currentExp, CardProperty property = CardProperty.NONE)
        {
            name = CardBase.cardImgNames[(int)type];
            executeTime = CardBase.executeTimes[type];
            delayTime = CardBase.delayTimes[type];
            maxExp = CardBase.cardLevelMaxExp[cardLevel];
            damage = CardBase.cardLevelDamage[type][cardLevel];
            this.spendMP = spendMP;
            this.count = count;
            this.grade = grade;
            this.type = type;
            this.cardLevel = cardLevel;
            this.currentExp = currentExp;
            this.maxExp = maxExp;
            this.damage = damage;
            this.property = property;
        }
    }

    // 레어도 및 아이템 개수
    public enum Grade { NORMAL = 3, RARE = 2, EPIC = 1 }
    // 카드 스킬 타입
    public enum SkillType { LIONROAR, METEORS, SLASHES, SPRAY, HEAL} // 이게 찐임
    // 카드 속성
    public enum CardProperty { NONE, FIRE, ICE, POISON } // 불 얼음 독
}