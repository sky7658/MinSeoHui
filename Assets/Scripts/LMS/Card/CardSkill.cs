using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LMS.Card
{
    public class CardSkill
    {
        // CommonSkill
        public static IEnumerator HpHeal(GameObject obj, float executeTime)
        {
            Debug.Log("HpHeal�� �Ǿ����ϴ�.");
            yield break;
        }

        public static IEnumerator Teleport(GameObject obj, float executeTime)
        {
            float scaleX = 1f;
            float t = 0f;

            // Object�� �����̵� �ϴ� ����� ǥ��
            while(t < executeTime)
            {
                t += Time.smoothDeltaTime;
                obj.transform.localScale = new Vector3(scaleX - t / executeTime, 1f, 1f);
                yield return null;
            }

            Vector3 curPos = obj.transform.position;

            // �����̵��� ���� ������ �������� ������ ����
            float randX = Random.Range(-2f, 2f);
            float randZ = Random.Range(-2f, 2f);

            obj.transform.position = new Vector3(curPos.x + randX, curPos.y, curPos.z + randZ);

            obj.transform.localScale = new Vector3(1f, 1f, 1f);
            Debug.Log("Teleport�� �Ǿ����ϴ�.");
            yield break;
        }

        public static IEnumerator DarkSight(GameObject obj, float executeTime)
        {
            Debug.Log("DarkSight�� �Ǿ����ϴ�.");
            yield break;
        }

        public static IEnumerator Critical(GameObject obj, float executeTime)
        {
            Debug.Log("Critical�� �Ǿ����ϴ�."); 
            yield break;
        }

        //  AttackSkill
    }
}