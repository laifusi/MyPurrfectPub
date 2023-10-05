using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    [SerializeField] private int initialCoins;
    [SerializeField] private int initialPrestige;
    [SerializeField] private EventSO[] allEvents;
    [SerializeField] private AsignarValoresEvent eventPrefab;
    [SerializeField] private EventSO calmNightEvent;
    [SerializeField] private Inventory inventory;
    [SerializeField] private AdministrarNoche adminNightPrefab;
    [SerializeField] private GameObject[] managementPanels;
    [SerializeField] private PanelFinal PanelFinal;
    [SerializeField] private PanelProbabilidades pptext;
    [SerializeField] private Client clientePrefab;

    private AudioSource audioSource;

    private int coins;
    private int prestige;
    private int actualCapacityDrink;
    private int actualCapacityFood;
    private int clientsNumber;
    [SerializeField] private int totalCapacityDrink;
    [SerializeField] private int totalCapacityFood;
    [SerializeField] private int prestigelevel;
    [SerializeField] private rarityRate[] actualRarityRate;

    public AdministradorEvent eventAdministrator;

    List<EventSO> possibleCommonEvents = new List<EventSO>();
    List<EventSO> possibleUncommonEvents = new List<EventSO>();
    List<EventSO> possibleRareEvents = new List<EventSO>();
    List<EventSO> possibleVeryRareEvents = new List<EventSO>();
    List<EventSO> possibleEvents = new List<EventSO>();
    private int timesInStartNight;

    List<int> calculationsCoins = new List<int>();
    List<int> calculationsPrestige = new List<int>();

    public static Action<int> OnCoinsChange;
    public static Action<int> OnPrestigeChange;
    public static Action<int> OnFoodCapacityChange;
    public static Action<int> OnDrinkCapacityChange;

    [SerializeField] private rangePurrstige[] purrstigeRanges;

    [SerializeField] private rarityRate[] rarityRatesLevel1;
    [SerializeField] private rarityRate[] rarityRatesLevel2;
    [SerializeField] private rarityRate[] rarityRatesLevel3;
    [SerializeField] private rarityRate[] rarityRatesLevel4;

    private int totalClients;
    private int happyClients;
    private int unhappyClients;
    private int consumedDrinks;
    private int consumedDrinksCoins;
    private int consumedDrinksPrestige;
    private int consumedFood;
    private int consumedFoodCoins;
    private int consumedFoodPrestige;
    private int costEmployeeCoins;
    private int costEmployeePrestige;

    public int costEventCoins;
    public int costEventPrestige;

    public int createdEvents;

    [SerializeField] public List<Client> listado_clientes = new List<Client>();



    [System.Serializable]
    public struct rangePurrstige
    {
        public int minPurrstige;
        public int maxPurrstige;
        public int minClients;
        public int maxClients;
    }

    [System.Serializable]
    public struct rarityRate
    {
        public string rarity;
        public int minrate;
        public int maxrate;

        public rarityRate(string r, int p, int mp)
        {
            rarity = r;
            minrate = p;
            maxrate = mp;
        }
    }

    private IEnumerator Start()
    {
        audioSource = GetComponent<AudioSource>();

        foreach(GameObject panel in managementPanels)
        {
            panel.SetActive(true);
            panel.SetActive(false);
        }

        clientsNumber = 0;
        createdEvents = 0;
        ResetAdministratorNight();
        totalCapacityDrink = 0;
        totalCapacityFood = 0;
        prestigelevel = 1;
        actualRarityRate = (rarityRate[])rarityRatesLevel1.Clone();

        yield return null;

        AddCoins(initialCoins);
        AddPrestige(initialPrestige);
    }

    private void Awake()
    {
        instance = this;
    }

    public void AddCoins(int value)
    {
        coins += value;
        OnCoinsChange?.Invoke(coins);
    }

    public int GetCoins()
    {
        return coins;
    }

    public void AddPrestige(int value)
    {
        prestige += value;

        if (prestige < 0)
            prestige = 0;

        OnPrestigeChange?.Invoke(prestige);
    }

    public int GetPrestige()
    {
        return prestige;
    }

    public int GetPrestigeLevel()
    {
        return prestigelevel;
    }

    public void AddCapacityDrink(int cap)
    {
        totalCapacityDrink += cap;
        if (totalCapacityDrink < 0)
        {
            totalCapacityDrink = 0;
        }

        OnDrinkCapacityChange?.Invoke(totalCapacityDrink);
    }

    public void AddCapacityFood(int cap)
    {
        totalCapacityFood += cap;
        if (totalCapacityFood < 0)
        {
            totalCapacityFood = 0;
        }

        OnFoodCapacityChange?.Invoke(totalCapacityFood);
    }

    public void AddActualCapacityDrink(int cap)
    {
        actualCapacityDrink += cap;
        if(actualCapacityDrink < 0)
        {
            actualCapacityDrink = 0;
        }

        OnFoodCapacityChange?.Invoke(totalCapacityFood);
    }

    public void AddActualCapacityFood(int cap)
    {
        actualCapacityFood += cap;
        if (actualCapacityFood < 0)
        {
            actualCapacityFood = 0;
        }
    }

    public void AddClients(int clients)
    {
        clientsNumber += clients;

        if (clientsNumber < 0)
            clientsNumber = 0;
    }

    public void UpdatePrestigeLevel()
    {
        if(prestige >= 0 && prestige <= 25)
        {
            prestigelevel = 1;
            actualRarityRate = (rarityRate[])rarityRatesLevel1.Clone();
        }
        else if (prestige > 25 && prestige <= 50)
        {
            prestigelevel = 2;
            actualRarityRate = (rarityRate[])rarityRatesLevel2.Clone();
        }
        else if (prestige > 50 && prestige <= 75)
        {
            prestigelevel = 3;
            actualRarityRate = (rarityRate[])rarityRatesLevel3.Clone();
        }
        else if (prestige > 75 && prestige <= 100)
        {
            prestigelevel = 4;
            actualRarityRate = (rarityRate[])rarityRatesLevel4.Clone();
        }
    }

    public void AddListCalculationCoins(int new_calculation)
    {
        calculationsCoins.Add(new_calculation);
    }

    public void AddListCalculationPrestiege(int new_calculation)
    {
        calculationsPrestige.Add(new_calculation);
    }

    public void fillShowEventList()
    {
        if (inventory.ShowActive)
        {
            foreach (EventSO e in inventory.ShowDetails.eventlist)
            {
                switch (e.rarity)
                {
                    case EventSO.Rarity.Common:
                        eventAdministrator.showEvents.commonEventlist.Add(e);
                        break;
                    case EventSO.Rarity.Uncommon:
                        eventAdministrator.showEvents.uncommonEventlist.Add(e);
                        break;
                    case EventSO.Rarity.Rare:
                        eventAdministrator.showEvents.rareEventlist.Add(e);
                        break;
                    case EventSO.Rarity.VeryRare:
                        eventAdministrator.showEvents.veryRareEventlist.Add(e);
                        break;
                }
            }
        }
    }

    public void fillEmployeeEventList()
    {
        if (inventory.employeelist.Count > 0)
        {
            foreach (Inventory.inventoryEmployee emp in inventory.employeelist)
            {
                foreach (EventSO e in emp.eventlist)
                {
                    switch (e.rarity)
                    {
                        case EventSO.Rarity.Common:
                            eventAdministrator.employeeEvents.commonEventlist.Add(e);
                            break;
                        case EventSO.Rarity.Uncommon:
                            eventAdministrator.employeeEvents.uncommonEventlist.Add(e);
                            break;
                        case EventSO.Rarity.Rare:
                            eventAdministrator.employeeEvents.rareEventlist.Add(e);
                            break;
                        case EventSO.Rarity.VeryRare:
                            eventAdministrator.employeeEvents.veryRareEventlist.Add(e);
                            break;
                    }
                }
            }
        }
    }

    public void StartNight()
    {
        /*possibleCommonEvents.Clear();
        possibleUncommonEvents.Clear();
        possibleRareEvents.Clear();
        possibleVeryRareEvents.Clear();*/

        clientsNumber = GetNumberClients();

        eventAdministrator.ResetshowEvents();
        eventAdministrator.ChangepossibleEventShow(prestigelevel);
        if (inventory.ShowActive)
        {
            if (UnityEngine.Random.Range(1, 101) <= eventAdministrator.showEvents.posibility)
            {
                Debug.Log("Hay evento de Show");
                fillShowEventList();
                int numEventsShow = UnityEngine.Random.Range(1, eventAdministrator.showEvents.maxEventsDuringNight + 1);
                for (int i = 1; i <= numEventsShow; i++)
                {
                    string actualRaity = GetRarity(UnityEngine.Random.Range(1, 101));

                    int randomEventPosition;
                    EventSO selectedEvent = ScriptableObject.CreateInstance<EventSO>();
                    AsignarValoresEvent newEvent;
                    switch (actualRaity)
                    {

                        case "Common":
                            if (eventAdministrator.showEvents.commonEventlist.Count > 0)
                            {
                                randomEventPosition = UnityEngine.Random.Range(0, eventAdministrator.showEvents.commonEventlist.Count - 1);
                                selectedEvent = eventAdministrator.showEvents.commonEventlist[randomEventPosition];
                            }
                            break;
                        case "Uncommon":
                            if (eventAdministrator.showEvents.uncommonEventlist.Count > 0)
                            {
                                randomEventPosition = UnityEngine.Random.Range(0, eventAdministrator.showEvents.uncommonEventlist.Count - 1);
                                selectedEvent = eventAdministrator.showEvents.uncommonEventlist[randomEventPosition];
                            }
                            break;
                        case "Rare":
                            if (eventAdministrator.showEvents.rareEventlist.Count > 0)
                            {
                                randomEventPosition = UnityEngine.Random.Range(0, eventAdministrator.showEvents.rareEventlist.Count - 1);
                                selectedEvent = eventAdministrator.showEvents.rareEventlist[randomEventPosition];
                            }
                            break;
                        case "VeryRare":
                            if (eventAdministrator.showEvents.veryRareEventlist.Count > 0)
                            {
                                randomEventPosition = UnityEngine.Random.Range(0, eventAdministrator.showEvents.veryRareEventlist.Count - 1);
                                selectedEvent = eventAdministrator.showEvents.veryRareEventlist[randomEventPosition];
                            }
                            break;
                    }

                    if (selectedEvent.event_text != "")
                    {
                        if (selectedEvent.dependence != null)
                        {
                            if (selectedEvent.dependence.options[selectedEvent.optionDependecyId].selected_option)
                            {
                                newEvent = Instantiate(eventPrefab);
                                newEvent.AssignEvent(selectedEvent, this);
                                createdEvents++;
                            }
                        }
                        else
                        {
                            newEvent = Instantiate(eventPrefab);
                            newEvent.AssignEvent(selectedEvent, this);
                            createdEvents++;
                        }
                    }
                }
            }
        } 
        
        eventAdministrator.ResetEmployeeEvents();
        eventAdministrator.ChangepossibleEventEmployee(prestigelevel);
        if (UnityEngine.Random.Range(1, 101) <= eventAdministrator.employeeEvents.posibility)
        {
            fillEmployeeEventList();
            int numEventsEmployee = UnityEngine.Random.Range(1, eventAdministrator.employeeEvents.maxEventsDuringNight + 1);
            for (int i = 1; i <= numEventsEmployee; i++)
            {
                string actualRaity = GetRarity(UnityEngine.Random.Range(1, 101));

                int randomEventPosition;
                EventSO selectedEvent = ScriptableObject.CreateInstance<EventSO>();
                AsignarValoresEvent newEvent;
                switch (actualRaity)
                {

                    case "Common":
                        if (eventAdministrator.employeeEvents.commonEventlist.Count > 0)
                        {
                            randomEventPosition = UnityEngine.Random.Range(0, eventAdministrator.employeeEvents.commonEventlist.Count - 1);
                            selectedEvent = eventAdministrator.employeeEvents.commonEventlist[randomEventPosition];
                        }
                        break;
                    case "Uncommon":
                        if (eventAdministrator.employeeEvents.uncommonEventlist.Count > 0)
                        {
                            randomEventPosition = UnityEngine.Random.Range(0, eventAdministrator.employeeEvents.uncommonEventlist.Count - 1);
                            selectedEvent = eventAdministrator.employeeEvents.uncommonEventlist[randomEventPosition];
                        }
                        break;
                    case "Rare":
                        if (eventAdministrator.employeeEvents.rareEventlist.Count > 0)
                        {
                            randomEventPosition = UnityEngine.Random.Range(0, eventAdministrator.employeeEvents.rareEventlist.Count - 1);
                            selectedEvent = eventAdministrator.employeeEvents.rareEventlist[randomEventPosition];
                        }
                        break;
                    case "VeryRare":
                        if (eventAdministrator.employeeEvents.veryRareEventlist.Count > 0)
                        {
                            randomEventPosition = UnityEngine.Random.Range(0, eventAdministrator.employeeEvents.veryRareEventlist.Count - 1);
                            selectedEvent = eventAdministrator.employeeEvents.veryRareEventlist[randomEventPosition];
                        }
                        break;
                }


                if (selectedEvent != null && selectedEvent.event_text != null && selectedEvent.event_text != "")
                {
                    if (selectedEvent.dependence != null)
                    {
                        if (selectedEvent.dependence.options[selectedEvent.optionDependecyId].selected_option)
                        {
                            newEvent = Instantiate(eventPrefab);
                            newEvent.AssignEvent(selectedEvent, this);
                            createdEvents++;
                        }
                    }
                    else
                    {
                        newEvent = Instantiate(eventPrefab);
                        newEvent.AssignEvent(selectedEvent, this);
                        createdEvents++;
                    }
                }
            }
        }

        int numStandardEvent = UnityEngine.Random.Range(1, eventAdministrator.standarEvents.maxEventsDuringNight + 1);
        eventAdministrator.ChangepossibleEventStandar(prestigelevel);
        for (int i = 1; i <= numStandardEvent; i++)
        {
            string actualRaity = GetRarity(UnityEngine.Random.Range(1, 101));

            int randomEventPosition;
            EventSO selectedEvent = ScriptableObject.CreateInstance<EventSO>();
            AsignarValoresEvent newEvent;
            switch (actualRaity)
            {

                case "Common":
                    if (eventAdministrator.standarEvents.commonEventlist.Count > 0)
                    {
                        randomEventPosition = UnityEngine.Random.Range(0, eventAdministrator.standarEvents.commonEventlist.Count - 1);
                        selectedEvent = eventAdministrator.standarEvents.commonEventlist[randomEventPosition];
                    }
                    break;
                case "Uncommon":
                    if (eventAdministrator.standarEvents.uncommonEventlist.Count > 0)
                    {
                        randomEventPosition = UnityEngine.Random.Range(0, eventAdministrator.standarEvents.uncommonEventlist.Count - 1);
                        selectedEvent = eventAdministrator.standarEvents.uncommonEventlist[randomEventPosition];
                    }
                    break;
                case "Rare":
                    if (eventAdministrator.standarEvents.rareEventlist.Count > 0)
                    {
                        randomEventPosition = UnityEngine.Random.Range(0, eventAdministrator.standarEvents.rareEventlist.Count - 1);
                        selectedEvent = eventAdministrator.standarEvents.rareEventlist[randomEventPosition];
                    }
                    break;
                case "VeryRare":
                    if (eventAdministrator.standarEvents.veryRareEventlist.Count > 0)
                    {
                        randomEventPosition = UnityEngine.Random.Range(0, eventAdministrator.standarEvents.veryRareEventlist.Count - 1);
                        selectedEvent = eventAdministrator.standarEvents.veryRareEventlist[randomEventPosition];
                    }
                    break;
            }

            if(selectedEvent != null && selectedEvent.event_text != null && selectedEvent.event_text != "")
            {

                if (selectedEvent.dependence != null)
                {
                    if (selectedEvent.dependence.options[selectedEvent.optionDependecyId].selected_option)
                    {
                        newEvent = Instantiate(eventPrefab);
                        newEvent.AssignEvent(selectedEvent, this);
                        createdEvents++;
                    }
                }
                else
                {
                    newEvent = Instantiate(eventPrefab);
                    newEvent.AssignEvent(selectedEvent, this);
                    createdEvents++;
                }
            }
        }
        /*
        foreach (EventSO eventSO in allEvents)
        {
            if (eventSO.min_purrstige <= prestige && eventSO.max_purrstige >= prestige)
            {
                switch (eventSO.rarity)
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
        if (randomRarityValue <= 10)
        {
            possibleEvents = possibleVeryRareEvents;
        }
        else if (randomRarityValue <= 15)
        {
            possibleEvents = possibleRareEvents;
        }
        else if (randomRarityValue <= 25)
        {
            possibleEvents = possibleUncommonEvents;
        }
        else
        {
            possibleEvents = possibleCommonEvents;
        }

        if (possibleEvents.Count == 0)
        {
            if (timesInStartNight < 5)
            {
                timesInStartNight++;
                StartNight();
                return;
            }
            else
            {
                EventSO selectedEvent = calmNightEvent;
                AsignarValoresEvent newEvent = Instantiate(eventPrefab);
                newEvent.AssignEvent(selectedEvent, this);
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
            newEvent.AssignEvent(selectedEvent, this);
        }

        timesInStartNight = 0;*/
    }







    private int GetNumberClients()
    {
        int numberClients;
        int actualPrestige = GetPrestige();
        int indexActualPrestige = 0;

        foreach(rangePurrstige rp in purrstigeRanges)
        {
            if(rp.minPurrstige <= actualPrestige && rp.maxPurrstige >= actualPrestige)
            {
                break;
            }

            indexActualPrestige++;
        }

        numberClients = UnityEngine.Random.Range(purrstigeRanges[indexActualPrestige].minClients, purrstigeRanges[indexActualPrestige].maxClients + 1);

        totalClients = numberClients;

        return numberClients;
    }

    private string GetRarity(int randomRarity)
    {
        string rarity;

        if(randomRarity >= actualRarityRate[0].minrate && randomRarity <= actualRarityRate[0].maxrate)
        {
            rarity = "Common";
        }
        else if (randomRarity >= actualRarityRate[1].minrate && randomRarity <= actualRarityRate[1].maxrate)
        {
            rarity = "Uncommon";
        }
        else if (randomRarity >= actualRarityRate[2].minrate && randomRarity <= actualRarityRate[2].maxrate)
        {
            rarity = "Rare";
        }
        else
        {
            rarity = "Very Rare";
        }

        return rarity;
    }

    private bool ConsumeDrink(ref int indexDrink)
    {
        bool result = false;
        string actualRarity = GetRarity(UnityEngine.Random.Range(1, 101));

        int randomProbability = UnityEngine.Random.Range(1, 101);

        for(int i = 0; i < inventory.drinklist.Count; i++)
        {
            if(inventory.drinklist[i].rarity == actualRarity)
            {
                if(randomProbability >= inventory.drinklist[i].minprobability && randomProbability <= inventory.drinklist[i].maxprobability)
                {
                    if (inventory.drinklist[i].amount != 0)
                    {
                        result = true;
                    }
                    indexDrink = i;
                    break;
                }
            }
        }
        if(indexDrink == -1)
        {
            Debug.Log("probabilidad de: " + randomProbability + " y rareza aztual: " + actualRarity);
        }
        return result;
    }

    private bool ConsumeFood(ref int indexFood)
    {
        bool result = false;

        string actualRarity = GetRarity(UnityEngine.Random.Range(1, 101));

        int randomProbability = UnityEngine.Random.Range(1, 101);

        for (int i = 0; i < inventory.drinklist.Count; i++)
        {
            if (inventory.foodlist[i].rarity == actualRarity)
            {
                if (randomProbability >= inventory.foodlist[i].minprobability && randomProbability <= inventory.foodlist[i].maxprobability)
                {
                    if (inventory.foodlist[i].amount != 0)
                    {
                        result = true;
                    }
                    indexFood = i;
                    break;
                }
            }
        }

        if (indexFood == -1)
        {
            Debug.Log("probabilidad de: " + randomProbability + " y rareza aztual: " + actualRarity);
        }
        return result;
    }

    private void ConsumtionGestor()
    {

        int indexDrink = -1;
        int indexFood = -1;
        for (int i = 1; i <= clientsNumber; i++)
        {
            Client cliente = Instantiate(clientePrefab);
            switch(UnityEngine.Random.Range(0,3))
            {
                case 0:
                    if (inventory.GetDrinkCount() > 0 && actualCapacityDrink > 0)
                    {
                        if (ConsumeDrink(ref indexDrink))
                        {
                            calculationsCoins.Add(inventory.drinklist[indexDrink].michicoinscost * 2);
                            calculationsPrestige.Add(inventory.drinklist[indexDrink].purrstige);
                            inventory.RemoveDrink(inventory.drinklist[indexDrink]);
                            actualCapacityDrink--;
                            consumedDrinks++;
                            consumedDrinksCoins += (inventory.drinklist[indexDrink].michicoinscost * 2);
                            consumedDrinksPrestige += inventory.drinklist[indexDrink].purrstige;
                            happyClients++;
                            cliente.AddConsumicines(i, inventory.drinklist[indexDrink].name_drink, true, false);
                        }
                        else
                        {
                            unhappyClients++;
                            calculationsPrestige.Add(-2);
                            cliente.AddConsumicines(i, inventory.drinklist[indexDrink].name_drink, false, false);
                        }


                    }
                    else
                    {
                        unhappyClients++;
                        calculationsPrestige.Add(-2);
                        if(actualCapacityDrink <= 0)
                            cliente.AddConsumicines(i, "No hay suficiente capacidad de bebidas", false, true);
                        else
                            cliente.AddConsumicines(i, "No quedan bebidas", false, true);
                    }
                    break;
                case 1:
                    if (inventory.GetFoodCount() > 0 && actualCapacityFood > 0)
                    {
                        if (ConsumeFood(ref indexFood))
                        {
                            calculationsCoins.Add(inventory.foodlist[indexFood].michicoinscost * 2);
                            calculationsPrestige.Add(inventory.foodlist[indexFood].purrstige);
                            inventory.RemoveFood(inventory.foodlist[indexFood]);
                            actualCapacityFood--;
                            consumedFood++;
                            consumedFoodCoins += (inventory.foodlist[indexFood].michicoinscost * 2);
                            consumedFoodPrestige += (inventory.foodlist[indexFood].purrstige);
                            happyClients++;
                            cliente.AddConsumicines(i, inventory.foodlist[indexFood].name_food, true, false);
                        }
                        else
                        {
                            unhappyClients++;
                            calculationsPrestige.Add(-2);
                            cliente.AddConsumicines(i, inventory.foodlist[indexFood].name_food, false, false);
                        }
                    }
                    else
                    {
                        unhappyClients++;
                        calculationsPrestige.Add(-2);
                        if (actualCapacityFood <= 0)
                            cliente.AddConsumicines(i, "No hay suficiente capacidad de comidas", false, true);
                        else
                            cliente.AddConsumicines(i, "No quedan comidas", false, true);
                    }
                    break;
                case 2:
                    if((inventory.GetDrinkCount() <= 0 && actualCapacityDrink <= 0) || (inventory.GetFoodCount() <= 0 && actualCapacityFood <= 0))
                    {
                        unhappyClients++;
                        calculationsPrestige.Add(-2);

                        if (actualCapacityDrink <= 0)
                            cliente.AddConsumicines(i, "No hay suficiente capacidad de bebidas", false, true);
                        else if(inventory.GetDrinkCount() <= 0)
                            cliente.AddConsumicines(i, "No quedan bebidas", false, true);
                        else if (actualCapacityFood <= 0)
                            cliente.AddConsumicines(i, "No hay suficiente capacidad de comidas", false, true);
                        else
                            cliente.AddConsumicines(i, "No quedan comidas", false, true);
                    }
                    else
                    {
                        bool drinkConsumed = ConsumeDrink(ref indexDrink);
                        bool foodConsumed = ConsumeFood(ref indexFood);
                        if (drinkConsumed && foodConsumed)
                        {
                            calculationsCoins.Add(inventory.drinklist[indexDrink].michicoinscost * 2);
                            calculationsPrestige.Add(inventory.drinklist[indexDrink].purrstige);
                            inventory.RemoveDrink(inventory.drinklist[indexDrink]);
                            actualCapacityDrink--;
                            consumedDrinks++;
                            consumedDrinksCoins += (inventory.drinklist[indexDrink].michicoinscost * 2);
                            consumedDrinksPrestige += inventory.drinklist[indexDrink].purrstige;

                            calculationsCoins.Add(inventory.foodlist[indexFood].michicoinscost * 2);
                            calculationsPrestige.Add(inventory.foodlist[indexFood].purrstige);
                            inventory.RemoveFood(inventory.foodlist[indexFood]);
                            actualCapacityFood--;
                            consumedFood++;
                            consumedFoodCoins += (inventory.foodlist[indexFood].michicoinscost * 2);
                            consumedFoodPrestige += (inventory.foodlist[indexFood].purrstige);

                            happyClients++;

                            cliente.AddConsumicines(i, inventory.drinklist[indexDrink].name_drink, true, false);
                            cliente.AddConsumicines(i, inventory.foodlist[indexFood].name_food, true, false);
                        }
                        else
                        {
                            unhappyClients++;
                            calculationsPrestige.Add(-2);
                            cliente.AddConsumicines(i, inventory.drinklist[indexDrink].name_drink, drinkConsumed, false);
                            cliente.AddConsumicines(i, inventory.foodlist[indexFood].name_food, foodConsumed, false);
                        }
                    }
                    break;
            }
            listado_clientes.Add(cliente);
        }
    }

    private void ShowGestor()
    {
        if(inventory.ShowActive)
        {
            calculationsPrestige.Add(inventory.ShowDetails.purrstige);
            costEventPrestige += inventory.ShowDetails.purrstige;
        }
    }

    private void EmployeeCost()
    {
        if(inventory.employeelist.Count > 0)
        {
            foreach(Inventory.inventoryEmployee e in inventory.employeelist)
            {
                calculationsCoins.Add(e.michicoinssalary * -1);
                calculationsPrestige.Add(e.purrstige);

                costEmployeeCoins += (e.michicoinssalary * -1);
                costEmployeePrestige += e.purrstige;
            }
        }
    }

    public void DoNightCalculations()
    {
        createdEvents--;
        if (createdEvents <= 0)
        {
            createdEvents = 0;

            actualCapacityFood = totalCapacityFood;
            actualCapacityDrink = totalCapacityDrink;

            ConsumtionGestor();
            ShowGestor();
            EmployeeCost();

            AdministrarNoche panelAdmin = Instantiate(adminNightPrefab);
            panelAdmin.GetClientesTotales(totalClients);
            panelAdmin.GetClientesAtendidos(happyClients);
            panelAdmin.GetClientesDesatendidos(unhappyClients, unhappyClients * -2);
            panelAdmin.GetBebidasConsumidas(consumedDrinks, consumedDrinksCoins, consumedDrinksPrestige);
            panelAdmin.GetAlimentosConsumidos(consumedFood, consumedFoodCoins, consumedFoodPrestige);
            panelAdmin.GetCosteEmpleados(costEmployeeCoins, costEmployeePrestige);
            panelAdmin.GetGananciasEvento(costEventCoins, costEventPrestige);

            //adminNightPrefab.GetComponent<AdministradorPedidos>().fillList(listado_clientes);

            int coinsObtained = 0;
            int prestigeObtained = 0;

            foreach (int amount in calculationsCoins)
            {
                coinsObtained += amount;
            }

            foreach (int amount in calculationsPrestige)
            {
                prestigeObtained += amount;
            }

            panelAdmin.GetGananciasTotales(coinsObtained, prestigeObtained);

            AddCoins(coinsObtained);
            AddPrestige(prestigeObtained);

            panelAdmin.GetRecursosTotales(coins, prestige);

            inventory.RemoveShow();

            UpdatePrestigeLevel();
        }
    }

    public void ResetAdministratorNight()
    {
        totalClients = 0;
        happyClients = 0;
        unhappyClients = 0;
        consumedDrinks = 0;
        consumedDrinksCoins = 0;
        consumedDrinksPrestige = 0;
        consumedFood = 0;
        consumedFoodCoins = 0;
        consumedFoodPrestige = 0;
        costEmployeeCoins = 0;
        costEmployeePrestige = 0;
        costEventCoins = 0;
        costEventPrestige = 0;
        calculationsPrestige.Clear();
        calculationsCoins.Clear();
        foreach(Client c in listado_clientes)
        {
            Destroy(c.gameObject);
        }
        listado_clientes.Clear();
    }

    public void Ganar()
    {
        PanelFinal panelFinal = Instantiate(PanelFinal);
        panelFinal.Ganar();
    }

    public void Perder()
    {
        PanelFinal panelFinal = Instantiate(PanelFinal);
        panelFinal.Perder();
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
