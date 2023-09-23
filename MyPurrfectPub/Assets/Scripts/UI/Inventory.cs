using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory instance { get; private set; }

    [SerializeField] private int drinkcount;
    [SerializeField] public List<inventoryDrink> drinklist;

    [System.Serializable]
    public struct inventoryDrink
    {
        public string name_drink;
        public int purrstige;
        public int michicoinscost;
        public int amount;
        public int minprobability;
        public int maxprobability;
        public string rarity;

        public inventoryDrink(string name, int p, int m, int a, int mpr, int mmpr, string r)
        {
            name_drink = name;
            purrstige = p;
            michicoinscost = m;
            amount = a;
            minprobability = mpr;
            maxprobability = mmpr;
            rarity = r;
        }
    }
    [SerializeField] private TextMeshProUGUI drinktext;

    [SerializeField] private int foodcount;
    [SerializeField] public List<inventoryFood> foodlist;

    [System.Serializable]
    public struct inventoryFood
    {
        public string name_food;
        public int purrstige;
        public int michicoinscost;
        public int amount;
        public int minprobability;
        public int maxprobability;
        public string rarity;

        public inventoryFood(string name, int p, int m, int a, int mpr, int mmpr, string r)
        {
            name_food = name;
            purrstige = p;
            michicoinscost = m;
            amount = a;
            minprobability = mpr;
            maxprobability = mmpr;
            rarity = r;
        }
    }
    [SerializeField] private TextMeshProUGUI foodtext;

    [SerializeField] private int employeecount;
    [SerializeField] public List<inventoryEmployee> employeelist;

    [System.Serializable]
    public struct inventoryEmployee
    {
        public string name_employee;
        public int purrstige;
        public int michicoinssalary;
        public string rol;
        public int capacity;
        public List<EventSO> eventlist;

        public inventoryEmployee(string name, int p, int m, string r, int c, List<EventSO> l)
        {
            name_employee = name;
            purrstige = p;
            michicoinssalary = m;
            rol = r;
            capacity = c;
            eventlist = l;
        }
    }
    [SerializeField] private TextMeshProUGUI employeetext;

    [SerializeField] public bool ShowActive;

    [SerializeField] public inventoryShowActive ShowDetails;

    [System.Serializable]
    public struct inventoryShowActive
    {
        public string name_show;
        public int purrstige;
        public int cost;
        public List<EventSO> eventlist;

        public inventoryShowActive(string name, int p, int m, List<EventSO> l)
        {
            name_show = name;
            purrstige = p;
            cost = m;
            eventlist = l;
        }
    }
    [SerializeField] private TextMeshProUGUI showtext;

    // Start is called before the first frame update
    void Start()
    {
        UpdateFood();
        UpdateDrink();
        UpdateEmployees();
        UpdateShow();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetDrinkCount()
    {
        return drinkcount;
    }

    public int GetFoodCount()
    {
        return foodcount;
    }

    public void AddFood(FoodSO food)
    {
        var index = foodlist.FindIndex(item => item.name_food == food.name);
        
        foodlist[index] = new inventoryFood(food.name, food.prestige, food.cost, foodlist[index].amount + 1, food.minprobability, food.maxprobability, food.rarity);
        foodcount++;
        UpdateFood();
    }

    public void RemoveFood(inventoryFood food)
    {
        var index = foodlist.FindIndex(item => item.name_food == food.name_food);

        foodlist[index] = new inventoryFood(food.name_food, food.purrstige, food.michicoinscost, foodlist[index].amount - 1, food.minprobability, food.maxprobability, food.rarity);
        foodcount--;
        UpdateFood();
    }

    public void UpdateFood()
    {
        foodtext.text = foodcount + " comida";
    }

    public void AddDrink(DrinkSO drink)
    {
        var index = drinklist.FindIndex(item => item.name_drink == drink.name);

        drinklist[index] = new inventoryDrink(drink.name, drink.prestige, drink.cost, drinklist[index].amount + 1, drink.minprobability, drink.maxprobability, drink.rarity);
        drinkcount++;
        UpdateDrink();
    }

    public void RemoveDrink(inventoryDrink drink)
    {
        var index = drinklist.FindIndex(item => item.name_drink == drink.name_drink);

        drinklist[index] = new inventoryDrink(drink.name_drink, drink.purrstige, drink.michicoinscost, drinklist[index].amount - 1, drink.minprobability, drink.maxprobability, drink.rarity);
        drinkcount--;
        UpdateDrink();
    }

    public void UpdateDrink()
    {
        drinktext.text = drinkcount + " bebida";
    }

    public void AddEmployee(EmployeeSO employee)
    {
        employeelist.Add(new inventoryEmployee(employee.name, employee.prestigePerTurn, employee.costPerTurn, employee.rol, employee.capacity, employee.eventlist));
        employeecount = employeelist.Count;
        if(employee.rol == "Coctelero" || employee.rol == "Coctelera")
            GameManager.instance.AddCapacityDrink(employee.capacity);
        else
            GameManager.instance.AddCapacityFood(employee.capacity);
        UpdateEmployees();
    }

    public void RemoveEmployee(EmployeeSO employee)
    {
        employeelist.RemoveAll(a => a.name_employee == employee.name);
        employeecount = employeelist.Count;
        if (employee.rol == "Coctelero" || employee.rol == "Coctelera")
            GameManager.instance.AddCapacityDrink(employee.capacity * -1);
        else
            GameManager.instance.AddCapacityFood(employee.capacity * -1);
        UpdateEmployees();
    }

    public void UpdateEmployees()
    {
        employeetext.text = employeecount + " empleados";
    }

    public void AddShow(NightShowSO nshow)
    {
        ShowDetails = new inventoryShowActive(nshow.name, nshow.prestige, nshow.cost, nshow.eventlist);
        ShowActive = true;
        UpdateShow();
    }

    public void RemoveShow()
    {
        ShowDetails = new inventoryShowActive("", 0, 0, new List<EventSO>());
        ShowActive = false;
        UpdateShow();
    }

    public void UpdateShow()
    {
        showtext.text = ShowDetails.name_show;
    }

    private void Awake()
    {
        instance = this;
    }
}
