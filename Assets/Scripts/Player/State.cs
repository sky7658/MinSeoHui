using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 상태 인터페이스 클래스
public interface State
{
    public StateName stateName { get; }
    public void Enter(Player _player);
    void Action(Player _player);
    void Exit(Player _player);
}

public class Move : State
{
    public StateName stateName { get; } = StateName.MOVE;
    public void Enter(Player _player)
    {
        _player.anim.SetBool("isWalk", true);
        _player.applySpeed = _player.walkSpeed;
    }

    public void Action(Player _player) // 오버라이딩
    {
        _player.TryMove();
        _player.TryJump();
        _player.transform.Translate(Vector3.forward * _player.applySpeed * Time.deltaTime);
    }

    public void Exit(Player _player)
    {
        _player.anim.SetBool("isWalk", false);
    }
}

public class Sprint : State
{
    public StateName stateName { get; } = StateName.SPRINT;
    public void Enter(Player _player)
    {
        _player.anim.SetBool("isWalk", true);
        _player.anim.SetBool("isSprint", true);
        _player.applySpeed = _player.runSpeed;
    }
    public void Action(Player _player)  // 오버라이딩
    {
        _player.TryMove();
        _player.TryJump();
        _player.transform.Translate(Vector3.forward * _player.applySpeed * Time.deltaTime);
    }

    public void Exit(Player _player)
    {
        _player.anim.SetBool("isSprint", false);
        _player.anim.SetBool("isWalk", false);
    }
}

public class Idle : State
{
    public StateName stateName { get; } = StateName.IDLE;
    public void Enter(Player _player)
    {
    }
    
    public void Action(Player _player)  // 오버라이딩
    {
        _player.TryMove();
        _player.TryJump();
    }

    public void Exit(Player _player)
    {
    }
}

public class Jump : State
{
    public StateName stateName { get; } = StateName.JUMP;

    public void Enter(Player _player)
    {
        _player.anim.SetBool("isJump", true);
        _player.anim.SetTrigger("doJump");
        _player.transform.position += Vector3.up * 0.1f;
        _player.isGround = false;
        _player.rb.velocity = new Vector3(_player.rb.velocity.x, _player.jumpForce, _player.rb.velocity.z);
    }

    public void Action(Player _player) // 오버라이딩
    {
        if (Input.GetKey(KeyCode.W))
        {
            _player.transform.Translate(Vector3.forward * _player.applySpeed * Time.deltaTime);
        }

        if (_player.rb.velocity.y < 0)
        {
            _player.isGround =
                Physics.Raycast(_player.transform.position, Vector3.down, _player.capsulCol.bounds.extents.y / 2 + 0.1f);
            if (_player.isGround)
            {
                _player._stateMachine.ChangeState(StateName.IDLE);
            }
        }
    }

    public void Exit(Player _player)
    {
        _player.anim.SetBool("isJump", false);
    }
}

public class Hit : State
{
    public StateName stateName { get; } = StateName.HIT;
    public void Enter(Player _player)
    {
        _player.anim.SetTrigger("doHit");
        _player._stateMachine.ChangeState(StateName.IDLE);
    }

    public void Action(Player _player)
    {
    }

    public void Exit(Player _player)
    {
    }
}

public class BackMove : State
{
    public StateName stateName { get; } = StateName.BACK;
    public void Enter(Player _player)
    {
        _player.anim.SetBool("isBackWalk", true);
        _player.applySpeed = _player.walkSpeed;
    }

    public void Action(Player _player)
    {
        _player.transform.Translate(Vector3.back * _player.applySpeed * Time.deltaTime);
        _player.TryMove();
        _player.TryJump();
    }

    public void Exit(Player _player)
    {
        _player.anim.SetBool("isBackWalk", false);
    }
}