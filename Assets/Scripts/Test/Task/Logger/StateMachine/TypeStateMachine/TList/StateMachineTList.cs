using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Key - ключ
/// ListElement - класс эелементов списка
/// TypeState - наследник AbstrackState
///
/// Устанавливает состояние по указ. типу
/// Этот класс нужен в случае, если ключ будет доставаться каким либо образом из элементов списка
/// В таком сулчае класс ListElement должен реализовывать интерфеис для получения ключа IGetKey
/// </summary>
public abstract class StateMachineTList<Key,ListType,TypeState> : MonoBehaviour where ListType:IGetKey<Key> where TypeState:AbstrackState
{
    [SerializeField] 
    protected ListType _startStateKey;
    protected Key _startState;
    protected Key _currentState;
    [SerializeField]
    protected List<ElementStateMachine<ListType,TypeState>> _elementState;
    protected Dictionary<Key, TypeState> _states;
    
    /// <summary>
    ///Очистит списки от пустых элементов и заполнит словарь 
    /// </summary>
    protected virtual void Init()
    {
        int target = _elementState.Count;
        for (int i = 0; i < target; i++)        
        {
            if (CheckNullElement(_elementState[i])==true)
            {
                _elementState.Remove(_elementState[i]);
                i--;
                target--;
            }
        }

        _states = new Dictionary<Key, TypeState>();
        foreach (var VARIABLE in _elementState)
        {
            _states.Add(VARIABLE.Key.GetKey(),VARIABLE.State);
        }
        
        _startState = _startStateKey.GetKey();
        StartSetState();
    }
    
    /// <summary>
    /// Устанавливает начальный State
    /// </summary>
    protected virtual void StartSetState()
    {
        if (_states.ContainsKey(_startState) == true)
        {
            _currentState = _startState;
            _states[_currentState].SelectState();
        }
    }
    
    /// <summary>
    /// Устанавливает State по ключу
    /// </summary>
    public virtual void SetState(Key type)
    {
        if (_states.ContainsKey(type) == true)
        {
            _states[_currentState].DiselectState();
            _currentState = type;
            _states[_currentState].SelectState();
            return;
        }

        Debug.LogError("Ошибка, такого типа нету в списке состояний " + type);
    }
    
    /// <summary>
    /// Проверка элементов на Null, в случаае если Null будет считаться не знач ключ. а что то еще, то класс наследник должен будет переопределить метод 
    /// </summary>
    protected virtual bool CheckNullElement(ElementStateMachine<ListType,TypeState> elementStateMachine)
    {
        if (elementStateMachine.Key.GetKey() == null)
        {
            return true;
        }

        return false;
    }
    
}

[System.Serializable]
public class ElementStateMachine<KeyElement, StateElement>
{
    public KeyElement Key => _key;
    public StateElement State => _state;
    
    [SerializeField]
    private KeyElement _key;
    [SerializeField]
    private StateElement _state;
}
