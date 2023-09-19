using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int initialCoins;
    [SerializeField] private int initialPrestige;
    [SerializeField] private EventSO[] allEvents;
    [SerializeField] private AsignarValoresEvent eventPrefab;
    [SerializeField] private EventSO calmNightEvent;

    private int coins;
    private int prestige;

    List<EventSO> possibleCommonEvents = new List<EventSO>();
    List<EventSO> possibleUncommonEvents = new List<EventSO>();
    List<EventSO> possibleRareEvents = new List<EventSO>();
    List<EventSO> possibleVeryRareEvents = new List<EventSO>();
    List<EventSO> possibleEvents = new List<EventSO>();
    private int timesInStartNight;

    public static Action<int> OnCoinsChange;
    public static Action<int> OnPrestigeChange;

    private IEnumerator Start()
    {
        yield return null;
        AddCoins(initialCoins);
        AddPrestige(initialPrestige);
    }

    public void AddCoins(int value)
    {
        coins += value;
        OnCoinsChange?.Invoke(coins);
    }

    public void AddPrestige(int value)
    {
        prestige += value;
        OnPrestigeChange?.Invoke(prestige);
    }

    public void StartNight()
    {
        possibleCommonEvents.Clear();
        possibleUncommonEvents.Clear();
        possibleRareEvents.Clear();
        possibleVeryRareEvents.Clear();

        foreach (EventSO eventSO in allEvents)
        {
            if (eventSO.min_purrstige <= prestige && eventSO.max_purrstige >= prestige)
            {
                switch(eventSO.rarity)
                {
                    case EventSO.Rarity.Common:
                        possibleCommonEvents.Add(eventSO);
                        break;
                    case EventSO.Rarity.Uncommon:
                        possibleUncommonEvents.Add(eventSO);
                        break;
                    case EventSO.Rarity.Rare:
                        possibleRareEvents.Add(eventSO);
                        break;
                    case EventSO.Rarity.VeryRare:
                        possibleVeryRareEvents.Add(eventSO);
                        break;
                }
            }
        }

        possibleEvents.Clear();

        int randomRarityValue = UnityEngine.Random.Range(0, 100);
        if(randomRarityValue <= 10)
        {
            possibleEvents = possibleVeryRareEvents;
        }
        else if(randomRarityValue <= 15)
        {
            possibleEvents = possibleRareEvents;
        }
        else if(randomRarityValue <= 25)
        {
            possibleEvents = possibleUncommonEvents;
        }
        else
        {
            possibleEvents = possibleCommonEvents;
        }

        if(possibleEvents.Count == 0)
        {
            if(timesInStartNight < 5)
            {
                timesInStartNight++;
                StartNight();
                return;
            }
            else
            {
                EventSO selectedEvent = calmNightEvent;
                AsignarValoresEvent newEvent = Instantiate(eventPrefab);
                newEvent.AssignEvent(selectedEvent);
            }
        }
        else
        {
            int randomEventPosition = UnityEngine.Random.Range(0, possibleEvents.Count);
            EventSO selectedEvent = possibleEvents[randomEventPosition];
            if (selectedEvent.dependence != null && !selectedEvent.dependence.options[selectedEvent.optionDependecyId].selected_option)
            {
                selectedEvent = calmNightEvent;
            }
            AsignarValoresEvent newEvent = Instantiate(eventPrefab);
            newEvent.AssignEvent(selectedEvent);
        }

        timesInStartNight = 0;
    }
}
