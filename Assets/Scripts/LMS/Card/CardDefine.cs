using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace LMS.Cards
{
    public class CardBase
    {
        // ī�� �ִ�ġ
        public static int maxCardCount = 5;

        public static Vector3 handDistance = new Vector3(0f, 100f, 0f);     // �ڵ� �������� �Ÿ�
        public static Vector3 InitCardPos = new Vector3(-577f, -210f, 0f);  // ī�� ���� ��ġ
        public static Vector3 characterHeight = new Vector3(0f, 1.5f, 0f);  // ĳ���� ����
        public static Vector3 characterSkillHeight = new Vector3(0f, -0.416365f, 0f);

        public static float basicAtkDelay = 0.5f;       // �⺻ ���� ������
        public static float comboTimeThreshold = 0.7f;  // �޺� �� �����Ǵ� �ð�

        public static string cardPrefName = "RawImage"; // ī�� ������ �̸�

        public static string[] cardImgNames = new string[] { "LionRoar", "Meteors", "Slashes", "Spray", "Heal" };  // ī�� �̹��� �̸�
        public static float[] cardLevelMaxExp = new float[] { 100f, 200f, 300f, 400f }; // Max Level 5 (���� ����)


        public static Dictionary<SkillType, float[]> cardLevelDamage = new Dictionary<SkillType, float[]>()
        {
            { SkillType.LIONROAR, new float[] { 50f, 10f, 15f, 20f, 25f } },
            { SkillType.METEORS, new float[] { 2f, 3f, 4f, 5f, 6f } },
            { SkillType.SLASHES, new float[] { 5f, 10f, 15f, 20f, 25f } },
            { SkillType.SPRAY, new float[] { 1f, 1.5f, 2f, 2.5f, 3f } }
        };

        // ī�� Img�� ���� Type ����
        public static Dictionary<string, SkillType> skillTypes = new Dictionary<string, SkillType>()
        {
            {cardImgNames[0], SkillType.LIONROAR}, {cardImgNames[1], SkillType.METEORS}, {cardImgNames[2], SkillType.SLASHES}, {cardImgNames[3], SkillType.SPRAY},
            {cardImgNames[4], SkillType.HEAL}
        };

        // Delay Time ����
        public static Dictionary<SkillType, float> delayTimes = new Dictionary<SkillType, float>()
        {
            {SkillType.LIONROAR, 0.2f}, {SkillType.METEORS, 0.2f}, {SkillType.SLASHES, 1f}, {SkillType.SPRAY, 1f},
            {SkillType.HEAL, 0.2f}
        };

        // Execute Time ����
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
        public Grade grade { get; set; }
        public SkillType type { get; set; }
        public CardProperty property { get; set; }

        // Card�� �⺻ ���� ����
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

    // ��� �� ������ ����
    public enum Grade { NORMAL = 3, RARE = 2, EPIC = 1 }
    // ī�� ��ų Ÿ��
    public enum SkillType { LIONROAR, METEORS, SLASHES, SPRAY, HEAL} // �̰� ����
    // ī�� �Ӽ�
    public enum CardProperty { NONE, FIRE, ICE, POISON } // �� ���� ��
}