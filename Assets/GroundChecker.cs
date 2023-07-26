using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private Player _player;
    private void OnCollisionEnter(Collision other)
    {
        _player.isGround = true;
    }
    
    private void OnCollisionExit(Collision other)
    {
        _player.isGround = false;
    }
}
