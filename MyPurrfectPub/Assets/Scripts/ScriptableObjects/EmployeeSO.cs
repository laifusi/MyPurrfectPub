using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Employee", menuName = "ScriptableObjects/Employee")]
public class EmployeeSO : ScriptableObject
{
    [SerializeField] public new string name;
    [SerializeField] public int prestigePerTurn;
    [SerializeField] public int costPerTurn;
    [SerializeField] public string rol;
    [SerializeField] public int capacity;
    [SerializeField] public string description;
    [SerializeField] public bool bought;
    [SerializeField] public Sprite image;
    [SerializeField] public List<EventSO> eventlist;

    public void Reset()
    {
        bought = false;
    }
}
