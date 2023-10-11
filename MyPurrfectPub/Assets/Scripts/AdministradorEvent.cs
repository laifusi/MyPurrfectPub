using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdministradorEvent : MonoBehaviour
{
    [SerializeField] private EventSO[] allEvents;

    [SerializeField] public structEvents standarEvents;

    [SerializeField] public structEvents employeeEvents;

    [SerializeField] public structEvents showEvents;

    [System.Serializable]
    public struct structEvents
    {
        public List<EventSO> commonEventlist;
        public List<EventSO> uncommonEventlist;
        public List<EventSO> rareEventlist;
        public List<EventSO> veryRareEventlist;
        public int maxEventsDuringNight;
        public int posibility;
    }

    private void Start()
    {
        foreach(EventSO nightEvent in allEvents)
        {
            nightEvent.Reset();
        }
    }

    public void AddEmployeeEvent(EventSO employeeEvent)
    {
        switch (employeeEvent.rarity)
        {
            case EventSO.Rarity.Common:
                employeeEvents.commonEventlist.Add(employeeEvent);
                break;
            case EventSO.Rarity.Uncommon:
                employeeEvents.uncommonEventlist.Add(employeeEvent);
                break;
            case EventSO.Rarity.Rare:
                employeeEvents.rareEventlist.Add(employeeEvent);
                break;
            case EventSO.Rarity.VeryRare:
                employeeEvents.veryRareEventlist.Add(employeeEvent);
                break;
        }
    }

    public void ResetEmployeeEvents()
    {
        employeeEvents.commonEventlist.Clear();
        employeeEvents.uncommonEventlist.Clear();
        employeeEvents.rareEventlist.Clear();
        employeeEvents.veryRareEventlist.Clear();
    }

    public void AddshowEvents(EventSO showEvent)
    {
        switch (showEvent.rarity)
        {
            case EventSO.Rarity.Common:
                showEvents.commonEventlist.Add(showEvent);
                break;
            case EventSO.Rarity.Uncommon:
                showEvents.uncommonEventlist.Add(showEvent);
                break;
            case EventSO.Rarity.Rare:
                showEvents.rareEventlist.Add(showEvent);
                break;
            case EventSO.Rarity.VeryRare:
                showEvents.veryRareEventlist.Add(showEvent);
                break;
        }
    }

    public void ResetshowEvents()
    {
        showEvents.commonEventlist.Clear();
        showEvents.uncommonEventlist.Clear();
        showEvents.rareEventlist.Clear();
        showEvents.veryRareEventlist.Clear();
    }

    public void ChangepossibleEventShow(int purrstigelevel)
    {
        showEvents.maxEventsDuringNight = 1;
        showEvents.posibility = 70;
    }

    public void ChangepossibleEventStandar(int purrstigelevel)
    {
        standarEvents.posibility = 80;

        switch (purrstigelevel)
        {
            case 1:
                standarEvents.maxEventsDuringNight = 2;
                break;
            case 2:
                standarEvents.maxEventsDuringNight = 3;
                break;
            case 3:
                standarEvents.maxEventsDuringNight = 4;
                break;
            case 4:
                standarEvents.maxEventsDuringNight = 5;
                break;
            /*case 5:
                standarEvents.maxEventsDuringNight = 3;
                break;
            case 6:
                standarEvents.maxEventsDuringNight = 4;
                break;
            case 7:
                standarEvents.maxEventsDuringNight = 4;
                break;*/
        }
    }

    public void ChangepossibleEventEmployee(int purrstigelevel)
    {
        employeeEvents.posibility = 40;

        switch (purrstigelevel)
        {
            case 1:
                employeeEvents.maxEventsDuringNight = 1;
                break;
            case 2:
                employeeEvents.maxEventsDuringNight = 2;
                break;
            case 3:
                employeeEvents.maxEventsDuringNight = 3;
                break;
            case 4:
                employeeEvents.maxEventsDuringNight = 4;
                break;
                /*  case 5:
                      employeeEvents.maxEventsDuringNight = 3;
                      break;
                  case 6:
                      employeeEvents.maxEventsDuringNight = 3;
                      break;
                  case 7:
                      employeeEvents.maxEventsDuringNight = 4;
                      break;*/
        }
    }
}
