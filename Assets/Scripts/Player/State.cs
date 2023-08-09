using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 상태 인터페이스 클래스
public interface State
{
    public StateName stateName { get; }
    public void Enter(Player player);
    void Action(Player player);
    void Exit(Player player);
}

public class Move : State
{
    public StateName stateName { get; } = StateName.MOVE;
    public void Enter(Player player)
    {
        player.anim.SetBool("isWalk", true);
        player.applySpeed = player.walkSpeed;
    }

    public void Action(Player player) // 오버라이딩
    {
        player.Roll();
        player.TryMove();
        player.TryJump();
        player.transform.Translate(Vector3.forward * player.applySpeed * Time.deltaTime);
    }

    public void Exit(Player player)
    {
        player.anim.SetBool("isWalk", false);
    }
}

public class Sprint : State
{
    public StateName stateName { get; } = StateName.SPRINT;
    public void Enter(Player player)
    {
        player.anim.SetBool("isWalk", true);
        player.anim.SetBool("isSprint", true);
        player.applySpeed = player.runSpeed;
    }
    public void Action(Player player)  // 오버라이딩
    {
        player.Roll();
        player.TryMove();
        player.TryJump();
        player.transform.Translate(Vector3.forward * player.applySpeed * Time.deltaTime);
    }

    public void Exit(Player player)
    {
        player.anim.SetBool("isSprint", false);
        player.anim.SetBool("isWalk", false);
    }
}

public class Idle : State
{
    public StateName stateName { get; } = StateName.IDLE;
    public void Enter(Player player)
    {
    }
    
    public void Action(Player player)  // 오버라이딩
    {
        player.Roll();
        player.TryMove();
        player.TryJump();
    }

    public void Exit(Player player)
    {
    }
}

public class Jump : State
{
    public StateName stateName { get; } = StateName.JUMP;

    public void Enter(Player player)
    {
        player.anim.SetBool("isJump", true);
        player.anim.SetTrigger("doJump");
        player.transform.position += Vector3.up * 0.1f;
        player.isGround = false;
        player.rb.velocity = new Vector3(player.rb.velocity.x, player.jumpForce, player.rb.velocity.z);
    }

    public void Action(Player player) // 오버라이딩
    {
        if (Input.GetKey(KeyCode.W))
            player.transform.Translate(Vector3.forward * player.applySpeed * Time.deltaTime);
        
        player.Roll();
        
        if (player.rb.velocity.y < 0)
        {
            player.isGround =
                Physics.Raycast(player.transform.position, Vector3.down, player.capsulCol.bounds.extents.y / 2 + 0.1f);
            if (player.isGround)
            {
                player._stateMachine.ChangeState(StateName.IDLE);
            }
        }
    }

    public void Exit(Player player)
    {
        player.anim.SetBool("isJump", false);
    }
}

public class Hit : State
{
    public StateName stateName { get; } = StateName.HIT;
    public void Enter(Player player)
    {
        player.anim.SetTrigger("doHit");
        player._stateMachine.ChangeState(StateName.IDLE);
    }

    public void Action(Player player)
    {
    }

    public void Exit(Player player)
    {
    }
}

public class Attack : State
{
    public StateName stateName { get; } = StateName.ATTACK;
    public void Enter(Player player)
    {
        player.anim.SetBool("isAtk2",true);
    }

    public void Action(Player player)
    {
    }

    public void Exit(Player player)
    {
        player.anim.SetBool("isAtk2",false);
    }
}

public class BackMove : State
{
    public StateName stateName { get; } = StateName.BACK;
    public void Enter(Player player)
    {
        player.anim.SetBool("isBackWalk", true);
        player.applySpeed = player.walkSpeed;
    }

    public void Action(Player player)
    {
        player.Roll();
        player.transform.Translate(Vector3.back * player.applySpeed * Time.deltaTime);
        player.TryMove();
        player.TryJump();
    }

    public void Exit(Player player)
    {
        player.anim.SetBool("isBackWalk", false);
    }
}

public class Dead : State
{
    public StateName stateName { get; } = StateName.DEAD;
    public void Enter(Player player)
    {
        player.anim.SetTrigger("doDead");
    }

    public void Action(Player player)
    {
    }

    public void Exit(Player player)
    {
    }
}

public class Fall : State
{
    public StateName stateName { get; } = StateName.DEAD;

    public void Enter(Player player)
    {
        player.anim.SetTrigger("doFall");
        player.anim.SetBool("isJump", true);
    }

    public void Action(Player player)
    {
        if (Input.GetKey(KeyCode.W))
            player.transform.Translate(Vector3.forward * player.applySpeed * Time.deltaTime);

        player.isGround =
            Physics.Raycast(player.transform.position, Vector3.down, player.capsulCol.bounds.extents.y / 2 + 0.1f);
        if (player.isGround)
            player._stateMachine.ChangeState(StateName.IDLE);
    }

    public void Exit(Player player)
    {
        player.anim.SetBool("isJump", false);
    }
}

public class Roll : State
{
    public StateName stateName { get; } = StateName.ROLL;

    public void Enter(Player player)
    {
        player.anim.SetBool("isRoll", true);
        Debug.Log(player.anim.GetBool("isRoll"));
        player.transform.Translate(Vector3.forward * 2f * Time.deltaTime);
    }

    public void Action(Player player)
    {
    }

    public void Exit(Player player)
    {
        player.anim.SetBool("isRoll", false);
    }
}