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
            var _spell = ObjectPool.Instance.GetObject<NormalProjectile>("Effect");
            UtilFunction.TurnOnOff(ObjectPool.Instance.objectInfos[4], _spell.gameObject, true);

            _spell.Initialized(direction, obj.transform.position + CardBase.characterHeight, 30f, "Effect", 5f); // 데미지 수정
            yield break;
        }
        public static IEnumerator MultipleFire(GameObject obj, Vector3 direction) // 기본 특별 공격
        {
            float[] _deg = new float[3] { 0f, 22.5f, -22.5f };
            for (int i = 0; i < 3; i++)
            {
                var _spell = ObjectPool.Instance.GetObject<NormalProjectile>("Effect");
                UtilFunction.TurnOnOff(ObjectPool.Instance.objectInfos[4], _spell.gameObject, true);

                _spell.transform.rotation = Quaternion.LookRotation(direction);

                var _curRot = _spell.transform.eulerAngles;
                var _y = _curRot.y + _deg[i];
                _spell.transform.rotation = Quaternion.Euler(_curRot.x, _y, _curRot.z);

                _spell.Initialized(_spell.transform.forward, obj.transform.position + CardBase.characterHeight, 30f, "Effect", 5f); // 데미지 수정
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

                _spell.transform.rotation = Quaternion.LookRotation(obj.transform.forward);

                var _randDeg = Random.Range(-5f, 5f);
                var _curRot = _spell.transform.eulerAngles;
                var _y = _curRot.y + _randDeg;
                _spell.transform.rotation = Quaternion.Euler(_curRot.x, _y, _curRot.z);

                //var _dir = obj.transform.forward + new Vector3(Random.Range(-0.1f, 0.1f), 0f, 0f);
                _spell.Initialized(_spell.transform.forward, obj.transform.position + CardBase.characterHeight, 30f, "Effect", info.damage, info);
                yield return new WaitForSeconds(info.executeTime / 5f);
            }
            yield break;
        }

        public static IEnumerator FloorFire(GameObject obj, Vector3 direction, CardInfo info)
        {
            // 수정
            var _spell = GameObject.Instantiate(Manager.GameManager.Instance.ResourceLoadObj("Effect")).GetComponent<Projectile>();
            _spell.Initialized(direction, obj.transform.position, 30f, "Effect", info.damage, info);
            yield break;
        }

        public static IEnumerator LinoRoar(GameObject obj, Vector3 direction, CardInfo info)
        {
            var _spell = ObjectPool.Instance.GetObject<LionRoar>("LionRoar");
            UtilFunction.TurnOnOff(ObjectPool.Instance.objectInfos[1], _spell.gameObject, true);
            _spell.Initialized(obj.transform.position, info.damage);
            yield break;
        }

        public static IEnumerator Meteors(GameObject obj, Vector3 direction, CardInfo info)
        {
            var _spell = ObjectPool.Instance.GetObject<Meteors>("Meteors");
            UtilFunction.TurnOnOff(ObjectPool.Instance.objectInfos[2], _spell.gameObject, true);
            _spell.Initialized(obj, obj.transform.position, info.damage, CardBase.meteorsCount[info.cardLevel]);
            yield break;
        }

        public static IEnumerator Slash(GameObject obj, Vector3 direction, CardInfo info)
        {
            for (int i = 0; i < 10; i++)
            {
                var _newSkill = GameObject.Instantiate(Manager.GameManager.Instance.ResourceLoadObj("Slash"));
                var _randY = Random.Range(0f, 360f);
                var _randZ = Random.Range(0f, 360f);

                _newSkill.transform.rotation = Quaternion.Euler(0f, _randY, _randZ);
                _newSkill.transform.SetParent(obj.transform);
                _newSkill.transform.position = obj.transform.position;
                yield return new WaitForSeconds(0.1f);
            }

            yield break;
        }
    }
}
