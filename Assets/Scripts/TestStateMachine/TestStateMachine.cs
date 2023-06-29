using System;
using System.Collections.Generic;
using UnityEngine;

public class TestStateMachine : MonoBehaviour
{

    [SerializeField] 
    private TListType _listType;
    [SerializeField] 
    private TGetLIstType _startState;
    private TGetLIstType CurrentState;
    [SerializeField]
    private List<TestElementStateMachine> _list;
    private Dictionary<TElelementType, TestAbsState> _states = new Dictionary<TElelementType, TestAbsState>();

    
    private void Awake()
    {

        foreach (var VARIABLE in _list)
        {
            _listType.GetElementName(VARIABLE.Type);
        }
        
        foreach (var VARIABLE in _list)
        {
            _states.Add(VARIABLE.Type.GetElement(),VARIABLE.State);
        }
        
        StartSetState();

    }

    private void StartSetState()
    {
        _listType.GetElementName(_startState);

        if (_states.ContainsKey(_startState.GetElement()) == true)
        {
            CurrentState = _startState;
            Debug.Log(CurrentState.Name);
            _states[CurrentState.GetElement()].SelectState();
        }
    }

    public void SetState(TGetLIstType type)
    {
        if (_states.ContainsKey(type.GetElement()) == true)
        {
            _states[CurrentState.GetElement()].DiselectState();
            CurrentState = type;
            _states[CurrentState.GetElement()].SelectState();
            return;
        }

        Debug.LogError("Ошибка, такого типа нету в списке состояний " + type.GetElement().Name);
    }
}

[System.Serializable]
public class TestElementStateMachine
{
    public TGetLIstType Type => _type;
    [SerializeField]
    private TGetLIstType _type;

    public TestAbsState State => _state;
    [SerializeField]
    private TestAbsState _state;
}
