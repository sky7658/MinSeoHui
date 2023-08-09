using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LMS.Utility;

namespace LMS.Cards
{
    public partial class CardSkill // CommonSkill
    {
        public static IEnumerator HpHeal(GameObject obj, Vector3 direction, CardInfo info)
        {
            Debug.Log("HpHeal이 되었습니다.");
            yield break;
        }
        public static IEnumerator SingleFire(GameObject obj, Vector3 direction) // 기본 공격
        {
            // 수정
            var _spell = ObjectPool.Instance.GetObject<NormalProjectile>("Effect");
            UtilFunction.TurnOnOff(ObjectPool.Instance.objectInfos[4], _spell.gameObject, true);
            _spell.Initialized(direction, obj.transform.position, 30f, "Effect");
            yield break;
        }
        public static IEnumerator MultipleFire(GameObject obj, Vector3 direction) // 기본 특별 공격
        {
            for (float i = -0.5f; i < 1f; i += 0.5f)
            {
                // 수정
                var _spell = ObjectPool.Instance.GetObject<NormalProjectile>("Effect");
                UtilFunction.TurnOnOff(ObjectPool.Instance.objectInfos[4], _spell.gameObject, true);
                var _dir = direction + new Vector3(i, 0f, 0f);
                _spell.Initialized(_dir, obj.transform.position, 30f, "Effect");
            }
            yield break;
        }
    }

    public partial class CardSkill // AttackSkill
    {
        public static IEnumerator SprayFire(GameObject obj, Vector3 direction, CardInfo info)
        {
            for (int i = 0; i < 5; i++)
            {
                // 수정
                var _spell = ObjectPool.Instance.GetObject<NormalProjectile>("Effect");
                UtilFunction.TurnOnOff(ObjectPool.Instance.objectInfos[4], _spell.gameObject, true);
                var _dir = obj.transform.forward + new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.05f, 0.05f), 0f);
                _spell.Initialized(_dir, obj.transform.position, 30f, "Effect", info);
                yield return new WaitForSeconds(info.executeTime / 5f);
            }
            yield break;
        }

        public static IEnumerator FloorFire(GameObject obj, Vector3 direction, CardInfo info)
        {
            // 수정
            var _spell = GameObject.Instantiate(Manager.GameManager.Instance.ResourceLoadObj("Effect")).GetComponent<Projectile>();
            _spell.Initialized(direction, obj.transform.position, 30f, "Effect", info);
            yield break;
        }

        public static IEnumerator LinoRoar(GameObject obj, Vector3 direction, CardInfo info)
        {
            var _spell = ObjectPool.Instance.GetObject<LionRoar>("LionRoar");
            UtilFunction.TurnOnOff(ObjectPool.Instance.objectInfos[1], _spell.gameObject, true);
            _spell.Initialized(obj.transform.position);
            yield break;
        }

        public static IEnumerator Meteors(GameObject obj, Vector3 direction, CardInfo info)
        {
            var _spell = ObjectPool.Instance.GetObject<Meteors>("Meteors");
            UtilFunction.TurnOnOff(ObjectPool.Instance.objectInfos[2], _spell.gameObject, true);
            _spell.Initialized(obj, obj.transform.position);
            yield break;
        }
    }
}
