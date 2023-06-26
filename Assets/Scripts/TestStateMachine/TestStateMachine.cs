using System;
using System.Collections.Generic;
using UnityEngine;

public class TestStateMachine : MonoBehaviour
{
    [SerializeField] 
    private TListType _listType;
    [SerializeField]
    private TGetLIstType CurrentState;
    [SerializeField]
    private List<TestElementStateMachine> _list;
    private Dictionary<TElelementType, TestAbsState> _states = new Dictionary<TElelementType, TestAbsState>();

    
    private void Start()
    {
        throw new NotImplementedException();
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
    [SerializeField]
    private TGetLIstType _type;
    
    [SerializeField]
    private TestAbsState _state;
}
