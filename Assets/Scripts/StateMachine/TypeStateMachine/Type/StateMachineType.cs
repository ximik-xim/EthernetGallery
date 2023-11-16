using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Key - ключ
/// TypeState - наследник AbstrackState
///
/// Устанавливает состояние по указ. типу
/// Этот класс нужен в случае, если ключ(Key) указываеться на прямую
/// Используеться обертка в нутрь класса FakT
/// </summary>
public class StateMachineType<Key,TypeState> : StateMachineTList<Key,FakT<Key>,TypeState> where TypeState : AbstrackState
{

}
