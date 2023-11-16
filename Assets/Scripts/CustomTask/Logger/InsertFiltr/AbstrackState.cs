using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class AbstrackState : MonoBehaviour
{

    public abstract void SelectState();


    public abstract void DiselectState();
}
