using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateName
{
    MOVE,
    SPRINT,
    IDLE,
    JUMP,
    HIT,
    BACK,
    DEAD,
    FALL,
    ROLL,
    ATTACK,
    SKILL
}

public class StateMachine
{
    public Player _player;
    public State _state { get; private set; }
    private Dictionary<StateName,State> _states = new Dictionary<StateName, State>();
    
    public StateMachine(StateName stateName, State state, Player player)
    {
        _player = player;
        _states.Add(stateName, state);
        _state = state;
    }
    
    public void AddState(StateName stateName, State state)
    {
        if (!_states.ContainsKey(stateName))
        {
            _states.Add(stateName,state);
        }
    }
    
    public bool GetState(StateName stateName)
    {
        if (_states[stateName] == _state)
            return true;

        return false;
    }
    
    public void DeleteState(StateName stateName)
    {
        if (_states.ContainsKey(stateName))
        {
            _states.Remove(stateName);
        }
    }
    
    public void ChangeState(StateName stateName)
    {
        if(_state == _states[stateName])
            return;
        _state?.Exit(_player);
        if(_states.TryGetValue(stateName, out State state))
        {
            _state = state;
            _player.stateName = stateName;
        }
        _state?.Enter(_player);
    }
    
    public void UpdateState()
    {
        _state?.Action(_player);
    }
}
