using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New_Event", menuName = "ScriptableObjects/Evento")]

public class EventSO : ScriptableObject
{
    public int id;

    [TextArea(2, 2)]
    public string event_tittle;
    [TextArea(2,20)]
    public string event_text;

    //public int probility;
    public Rarity rarity;

    public int min_purrstige;

    public int max_purrstige;

    public EventSO dependence;
    public int optionDependecyId;

    public option[] options;

    public void Reset()
    {
        for(int i = 0; i < options.Length; i++)
        {
            options[i].selected_option = false;
        }
    }

    [System.Serializable]
    public struct option
    {
        public int id_option;
        public string text_option;
        public bool selected_option;
        public int purrstige;
        public int michicoins;
        public int capacity_drink;
        public int capacity_food;
        public int new_clients;
    }

    public enum Rarity
    {
        Common, Uncommon, Rare, VeryRare
    }
}
