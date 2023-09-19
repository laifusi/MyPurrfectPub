using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Employee", menuName = "ScriptableObjects/Employee")]
public class EmployeeSO : ScriptableObject
{
    [SerializeField] new string name;
    [SerializeField] int prestigePerTurn;
    [SerializeField] int costPerTurn;
    [SerializeField] Sprite image;
}
