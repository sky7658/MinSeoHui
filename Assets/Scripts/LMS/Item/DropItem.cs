using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LMS.Manager;

namespace LMS.Item
{
    enum ItemType { LionRoar, Meteor, Slash, Spray, Potion, None }
    public class DropItem : MonoBehaviour
    {
        private ItemType type;
        private Vector3 upPos, downPos;
        private bool arrow;

        private Coroutine bounceCoroutine;

        private void Start()
        {
            Initialized(transform.position);
        }
        public void Initialized(Vector3 pos)
        {
            if(bounceCoroutine != null)
            {
                GameManager.Instance.QuitCoroutine(bounceCoroutine);
                bounceCoroutine = null;
            }

            type = CreateItemInfo();

            if(type == ItemType.None)
            {
                //Utility.ObjectPool.Instance.ReturnObject(this, "Item");
                Destroy(gameObject);
                return;
            }

            transform.position = pos;

            upPos = pos + new Vector3(0f, 0.2f, 0f);
            downPos = pos + new Vector3(0f, -0.2f, 0f);

            bounceCoroutine = GameManager.Instance.ExecuteCoroutine(BounceObject());

        }

        private int ratio = 1;
        private int[] itemRatio = new int[3] { 4, 6, 10 }; // None, Potion, Card
        private int[] cardRatio = new int[4] { 3, 5, 7, 10 }; // LionRoar, Meteors, Slash, Spray
        private ItemType CreateItemInfo()
        {
            int _itemRand = Random.Range(0, ratio * 10);

            Debug.Log(_itemRand);

            if(_itemRand < itemRatio[0])
            {
                //return ItemType.None;
                return ItemType.LionRoar;
            }
            else if (_itemRand < itemRatio[1])
            {
                //return ItemType.Potion;
                return ItemType.LionRoar;
            }
            else
            {
                int _cardRand = Random.Range(0, ratio * 100);

                Debug.Log(_cardRand);

                if (_cardRand < cardRatio[0] * 10)
                {
                    return ItemType.LionRoar;
                }
                else if (_cardRand < cardRatio[1] * 10)
                {
                    return ItemType.Meteor;
                }
                else if (_cardRand < cardRatio[2] * 10)
                {
                    return ItemType.Slash;
                }
                else
                {
                    return ItemType.Spray;
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Player")
            {
                var _other = other.GetComponent<Player>();

                if(type == ItemType.Potion)
                {
                    if (_other.hp == _other.maxHp) return; // Ã¼·ÂÀÌ 100%¶ó¸é È¹µæ X
                }
                else
                {
                    if (_other.playerUIManger.GetCardCount() == Cards.CardBase.maxCardCount) return; // Ä«µå °¹¼ö°¡ ²ËÃ¡´Ù¸é È¹µæ X
                    _other.playerUIManger.PushCard((int)type);
                }

                GameManager.Instance.QuitCoroutine(bounceCoroutine);
                bounceCoroutine = null;

                //Utility.ObjectPool.Instance.ReturnObject(this, "Item");
                Destroy(gameObject);
            }
        }

        private IEnumerator BounceObject()
        {
            while(true)
            {
                if (arrow)
                {
                    GameManager.Instance.ExecuteCoroutine(UI.CardAction.MoveToAction(gameObject, upPos, transform.rotation, 1f));
                    yield return new WaitForSeconds(1f);
                    arrow = false;
                }
                else
                {
                    GameManager.Instance.ExecuteCoroutine(UI.CardAction.MoveToAction(gameObject, downPos, transform.rotation, 1f));
                    yield return new WaitForSeconds(1f);
                    arrow = true;
                }
                yield return null;
            }
        }
    }

}

