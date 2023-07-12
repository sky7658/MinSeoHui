using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LMS.Cards
{
    public class ParabolicProjectile : Projectile
    {
        float gravity = 10f;
        private void Update()
        {
            rigidBody.velocity += new Vector3(0f, -Time.deltaTime * gravity, 0f);
        }
        protected override void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Finish") // ¼öÁ¤
            {
                Release();
                //var newFloor = Instantiate(Manager.GameManager.Instance.ResourceLoadObj("Floor")).GetComponent<Floor>();
                //newFloor.Initialized(Vector3.zero, new Vector3(transform.position.x, other.transform.position.y, transform.position.z), 1f, info, "Floor");
            }
        }

        protected override void Release()
        {
            base.Release();
            gravity = 0f;
            var _newFloor = Instantiate(Manager.GameManager.Instance.ResourceLoadObj("Floor")).GetComponent<Floor>();
            var _newPos = transform.position + new Vector3(0, 0.3f, 0);
            _newFloor.Initialized(Vector3.zero, _newPos, 1f, info, "Floor");
        }
    }
}
