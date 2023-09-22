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

    private int coins;
    private int prestige;
    [SerializeField] private int actualCapacityDrink;
    [SerializeField] private int actualCapacityFood;
    [SerializeField] private int totalCapacityDrink;
    [SerializeField] private int totalCapacityFood;
    [SerializeField] private int prestigelevel;
    [SerializeField] private rarityRate[] actualRarityRate;

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
    }

    public void AddCapacityFood(int cap)
    {
        totalCapacityFood += cap;
    }

    public void UpdatePrestigeLevel()
    {
        if(prestigelevel >= 0 && prestigelevel <= 25)
        {
            prestigelevel = 1;
            actualRarityRate = (rarityRate[])rarityRatesLevel1.Clone();
        }
        else if (prestigelevel > 25 && prestigelevel <= 50)
        {
            prestigelevel = 2;
            actualRarityRate = (rarityRate[])rarityRatesLevel2.Clone();
        }
        else if (prestigelevel > 50 && prestigelevel <= 75)
        {
            prestigelevel = 3;
            actualRarityRate = (rarityRate[])rarityRatesLevel3.Clone();
        }
        else if (prestigelevel > 75 && prestigelevel <= 100)
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

        timesInStartNight = 0;
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
            rarity = "VeryRare";
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
                    if(inventory.drinklist[i].amount != 0)
                    {
                        indexDrink = i;
                        result = true;
                    }
                    break;
                }
            }
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
                        indexFood = i;
                        result = true;
                    }
                    break;
                }
            }
        }
        return result;
    }

    private void ConsumtionGestor()
    {
        int numberClients = GetNumberClients();

        int indexDrink = -1;
        int indexFood = -1;
        Debug.Log(actualCapacityDrink + " Capacida bebida");
        Debug.Log(actualCapacityFood + " Capacida food");
        for (int i = 1; i <= numberClients; i++)
        {
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
                        }
                        else
                        {
                            Debug.Log("Cliente insatisfecho bebida");
                            unhappyClients++;
                            calculationsPrestige.Add(-2);
                        }
                            
                    }
                    else
                    {
                        Debug.Log("Cliente insatisfecho bebida");
                        unhappyClients++;
                        calculationsPrestige.Add(-2);
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
                        }
                        else
                        {
                            Debug.Log("Cliente insatisfecho comida");
                            unhappyClients++;
                            calculationsPrestige.Add(-2);
                        }
                    }
                    else
                    {
                        Debug.Log("Cliente insatisfecho comida");
                        unhappyClients++;
                        calculationsPrestige.Add(-2);
                    }
                    break;
                case 2:
                    if((inventory.GetDrinkCount() == 0 && actualCapacityDrink == 0) || (inventory.GetFoodCount() <= 0 && actualCapacityFood <= 0))
                    {
                        Debug.Log("Cliente insatisfecho doble alimento");
                        unhappyClients++;
                        calculationsPrestige.Add(-2);
                    }
                    else
                    {
                        if (ConsumeDrink(ref indexDrink) && ConsumeFood(ref indexFood))
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
                        }
                        else
                        {
                            Debug.Log("Cliente insatisfecho doble alimento");
                            unhappyClients++;
                            calculationsPrestige.Add(-2);
                        }
                    }
                    break;
            }
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
        panelAdmin.GetGananciasEvento(costEventCoins,costEventPrestige);

        int coinsObtained = 0;
        int prestigeObtained = 0;

        foreach(int amount in calculationsCoins)
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

        inventory.RemoveShow();

        UpdatePrestigeLevel();

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
    }
}
