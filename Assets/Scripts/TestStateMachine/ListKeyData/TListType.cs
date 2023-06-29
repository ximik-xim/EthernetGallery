using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[CreateAssetMenu(menuName = "Testerrrr")]
public class TListType:ScriptableObject
{
    [SerializeField]
    private List<TElelementType> _list;

    private Dictionary<string, TElelementType> _types = new Dictionary<string, TElelementType>();

    private void OnValidate()
    {
        
        //По хорошему немного переделать, сделать заполнение как при Awake У Monobeh, но т.к элементы списка сериализованы, пока и так сойдет
        _types = new Dictionary<string, TElelementType>();
        foreach (var VARIABLE in _list)
        {
            bool setValue=true;
            int countPasses=0;
            int countPassesMax = 10;
            while (setValue==true && countPasses<countPassesMax)
            {
                countPasses++;
                setValue = Chect(VARIABLE);
            }

            if (countPasses == countPassesMax) 
            {
                Debug.LogError("Недопустиммое имя, даже с испровлениями. Поменяте имя элемента под текущем именем " + VARIABLE.Name);
            }

        }
       
    }


    private bool Chect(TElelementType elelementType )
    {

        if (_types.ContainsKey(elelementType.Name)==false)
        {
            _types.Add(elelementType.Name,elelementType);
            return false;
        }
        
        elelementType.AddStringCurrentName("0");
        return true;
    }
    
    public void GetElementName(TGetLIstType type)
    {
        type.SetData(_types[type.Name]);
    }




}

[System.Serializable]
public class TElelementType
{
    public string Name => _name;
    [SerializeField]
    private string _name;

    public void AddStringCurrentName(string text)
    {
        _name += text;
    }

}
