using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// InstObj-префаб обьекта, который будет создоваться
/// SetDataInObj - класс в который будут переданны данные об созданном префабе, класс должен реализовывать
/// интерфеис ISetterData для передачи в этот класс данных  
/// 
/// Засовывает данные об созданном префабе в скрипт, которому нужны эти данные
/// 
/// В режиме One -  будет один экземпляр префаба, который и будет передан во все скрипты которым нужны данные
/// В режиме Full - будет создан экземпляр префаба для каждого скрипта которому нужны данные
///
/// В списоке  _listInstObj храняться все созданные префабы, используемые для передачи их в один из элементов списка _listSetDataInstObj  
/// В список  _listInstObj можно засунуть через инспектор уже создданные на сцене экземпляры префабов и они будут исп. для передачи
/// их один из элементов списка _listSetDataInstObj при вызове моетода SetDataLenghtList
/// 
///
/// В списоке _listSetDataInstObj храняться все елементы куда были переданны данные об созданном префабе
/// В список _listSetDataInstObj можно засунуть через инспектор элементы которым нужно передать информ. об созданных префабах 
/// </summary>
public abstract class SetterDataDefault<InstObj, SetDataInObj> : MonoBehaviour where SetDataInObj : ISetterData<InstObj> where InstObj : MonoBehaviour
{
    [Header("Spawn")]
    [SerializeField] 
    protected Transform _parent;
    [SerializeField] 
    protected InstObj _prefab;
    [SerializeField] 
    protected List<InstObj> _listInstObj;

    [Header("Insert")]
    [SerializeField]
    protected List<SetDataInObj> _listSetDataInstObj;
    [SerializeField]
    protected Type _type;
    
    public enum Type
    {
        One,
        Full
    }

    /// <summary>
    /// Очистит списки от пустых элементов
    /// И если выбран тип One и не указ. созданный на сцене экземпляр префаба, то создаст этот экземпляр префаба
    /// </summary>
    protected virtual void Init()
    {
        CheckNull();
        
        if (_type == Type.One)
        {
            if (_listInstObj.Count == 0)
            {
                var obj = Instantiate(_prefab, _parent);

                _listInstObj.Add(obj);

            }
        }

    
    }

    /// <summary>
    /// Устанавливает всем элементам из списка _listSetDataInstObj информацию об префабах
    /// Если выбран режим One то уст. 1 элемент в списке созданных экземпляров префаба(_listInstObj)
    /// Если выбран режим FUll то проходит по всем имеющимся элементам в списке _listSetDataInstObj и
    /// передает значения элементов из списка _listInstObj (если нехватает элементов в списке _listInstObj,
    /// то создает новые экземпляры префабов)
    /// </summary>
    public virtual void SetDataLenghtList()
    {
        if (_type == Type.One)
        {
            foreach (var VARIABLE in _listSetDataInstObj)
            {
                VARIABLE.SetData(_listInstObj[0]);
            }
        }

        if (_type == Type.Full)
        {
            int targetCount = _listSetDataInstObj.Count;
            targetCount -= _listInstObj.Count;

            for (int i = 0; i < targetCount; i++)
            {
                var obj = Instantiate(_prefab, _parent);;
                
                _listInstObj.Add(obj);
            }

            for (int i = 0; i < _listSetDataInstObj.Count; i++)
            {
                _listSetDataInstObj[i].SetData(_listInstObj[i]);
            }

        }

    }
    /// <summary>
    /// Добавит элемент в список для передачи данных об префабе
    /// И передаст информацию об уже созданном префабе (если тип One)
    /// Или создаст новый экземпляр префаба и передаст информацию об новосозданном префабе
    /// </summary>
    public virtual void AddElementSetData(SetDataInObj loggerElementUI )
    {
        if (_type == Type.One)
        {
            _listSetDataInstObj.Add(loggerElementUI);
            loggerElementUI.SetData(_listInstObj[0]);
        }

        if (_type == Type.Full)
        {
            var obj = Instantiate(_prefab, _parent);;
            
            _listInstObj.Add(obj);
            _listSetDataInstObj.Add(loggerElementUI);
            
            
            loggerElementUI.SetData(obj);
        }
    }

    /// <summary>
    /// Проверяет списки на пустые элементы, и если находит то удаляет их
    /// </summary>
    protected void CheckNull()
    {
        int target = _listInstObj.Count;
        for (int i = 0; i < target; i++)
        {
            if (ChekNullElementListInt(_listInstObj[i]) == true)
            {
                _listInstObj.Remove(_listInstObj[i]);
                
                i--;
                target--;
            }
        }

        target = _listSetDataInstObj.Count;
        for (int i = 0; i < target; i++)
        {
            if (ChekNullElementListSetData(_listSetDataInstObj[i]) == true)
            {
                _listSetDataInstObj.Remove(_listSetDataInstObj[i]);
                
                i--;
                target--;
            }
        }
    }

    
    /// <summary>
    /// Проверка элементов списка _listInstObj на Null, в случаае если Null будет
    /// считаться не пустой элемент списка. а что то еще, то класс наследник должен будет переопределить метод
    /// </summary>
    protected virtual bool ChekNullElementListInt(InstObj element)
    {
        if (element == null)
        {
            return true;
        }

        return false;
    }
    
    /// <summary>
    /// Проверка элементов списка _listSetDataInstObj на Null, в случаае если Null будет
    /// считаться не пустой элемент списка. а что то еще, то класс наследник должен будет переопределить метод
    /// </summary>
    protected virtual bool ChekNullElementListSetData(SetDataInObj element)
    {
        if (element == null)
        {
            return true;
        }

        return false;
    }
}