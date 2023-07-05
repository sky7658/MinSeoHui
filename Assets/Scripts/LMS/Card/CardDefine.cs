using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LMS.Cards
{
    public class CardBase
    {
        // ī�� �ִ�ġ
        public static int maxCardCount = 5;

        public static string cardPrefName = "RawImage";
<<<<<<< Updated upstream
        public static string[] cardImgNames = new string[] { "a", "b", "c", "d", "e", "f", "Heal", "Teleport", "DarkSight", "Critical" };

        // ī�� Img�� ���� Type ����
        public static Dictionary<string, AttackType> atkTypes = new Dictionary<string, AttackType>() // atkī��
        { {cardImgNames[0], AttackType.a} };
        public static Dictionary<string, CommonType> comTypes = new Dictionary<string, CommonType>() // comī��
        { {cardImgNames[6], CommonType.HEAL}, {cardImgNames[7], CommonType.TELEPORT}, {cardImgNames[8], CommonType.DARKSIGHT}, {cardImgNames[9], CommonType.CRITICAL} };

        // Delay Time ����
        public static Dictionary<AttackType, float> a_dtTime = new Dictionary<AttackType, float>()
        { {AttackType.a, 0.2f } };
        public static Dictionary<CommonType, float> c_dtTime = new Dictionary<CommonType, float>()
        { {CommonType.HEAL, 0.2f }, {CommonType.TELEPORT, 2f }, {CommonType.DARKSIGHT, 0.2f }, {CommonType.CRITICAL, 0.2f } };

        // Execute Time ����
        public static Dictionary<AttackType, float> a_etTime = new Dictionary<AttackType, float>()
        { {AttackType.a, 0.2f } };
        public static Dictionary<CommonType, float> c_etTime = new Dictionary<CommonType, float>()
        { {CommonType.HEAL, 0.2f }, {CommonType.TELEPORT, 0.2f }, {CommonType.DARKSIGHT, 0.2f }, {CommonType.CRITICAL, 0.2f } };
=======
        public static string[] cardImgNames = new string[] { "Single", "Multiple", "Spray", "Floor", "Explosion", "Special", "Heal", "Teleport", "DarkSight", "Critical" };
        public static string[] cardImgNames1 = new string[] { "LionRoar", "Meteors", "Slashes", "Heal" };


        // ī�� Img�� ���� Type ����
        public static Dictionary<string, SkillType> skillTypes = new Dictionary<string, SkillType>()
        {
            {cardImgNames[0], SkillType.SINGLE}, {cardImgNames[1], SkillType.MULTIPLE}, {cardImgNames[2], SkillType.SPRAY},
            {cardImgNames[3], SkillType.FLOOR}, {cardImgNames[4], SkillType.EXPLOSION}, {cardImgNames[5], SkillType.SPECIAL},
            {cardImgNames[6], SkillType.HEAL}, {cardImgNames[7], SkillType.TELEPORT}, {cardImgNames[8], SkillType.DARKSIGHT}, {cardImgNames[9], SkillType.CRITICAL}
        };

        // Delay Time ����
        public static Dictionary<SkillType, float> delayTimes = new Dictionary<SkillType, float>()
        {
            {SkillType.SINGLE, 0.2f}, {SkillType.MULTIPLE, 0.2f}, {SkillType.SPRAY, 1f},
            {SkillType.FLOOR, 1f}, {SkillType.EXPLOSION, 1f}, {SkillType.SPECIAL, 1f},
            {SkillType.HEAL, 0.2f}, {SkillType.TELEPORT, 2f}, {SkillType.DARKSIGHT, 0.2f}, {SkillType.CRITICAL, 0.2f}
        };

        // Execute Time ����
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

        // Card�� �⺻ ���� ����
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

    // ��� �� ������ ����
    public enum Grade { NORMAL = 3, RARE = 2, EPIC = 1 }
<<<<<<< Updated upstream
    // AttackCard
    public enum AttackType { a/*�������*/, b/*���ϰ���*/, c/*���߰���*/, d/*���ǰ���*/, e/*���߰���*/, f/*�ʻ��*/ }
    // CommonCard
    public enum CommonType { HEAL, TELEPORT, DARKSIGHT, CRITICAL }
=======
    // ī�� ��ų Ÿ��
    public enum SkillType { SPRAY/*�������*/, SINGLE/*���ϰ���*/, MULTIPLE/*���߰���*/, FLOOR/*���ǰ���*/, EXPLOSION/*���߰���*/, SPECIAL/*�ʻ��*/,
        HEAL, TELEPORT, DARKSIGHT, CRITICAL }
    //public enum SkillType { LIONROAR, METEORS, SLASHES, HEAL} // �̰� ����
    // ī�� �Ӽ�
    public enum CardProperty { NONE, FIRE, ICE, POISON } // �� ���� ��
>>>>>>> Stashed changes
}