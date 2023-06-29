using System;
using System.Collections.Generic;
using UnityEngine;
//Отвечает за выбр текущего типа логов  
public class LoggerStateMachine : MonoBehaviour
{

    [SerializeField] 
    private TListType _listType;
    [SerializeField] 
    private TGetLIstType _startState;
    private TGetLIstType _currentState;
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
            _currentState = _startState;
            Debug.Log(_currentState.Name);
            _states[_currentState.GetElement()].SelectState();
        }
    }

    public void SetState(TGetLIstType type)
    {
        if (_states.ContainsKey(type.GetElement()) == true)
        {
            _states[_currentState.GetElement()].DiselectState();
            _currentState = type;
            _states[_currentState.GetElement()].SelectState();
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
