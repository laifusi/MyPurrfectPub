using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory instance { get; private set; }

    [SerializeField] private int drinkcount;
    [SerializeField] private List<inventoryDrink> drinklist;

    [System.Serializable]
    public struct inventoryDrink
    {
        public string name_drink;
        public int purrstige;
        public int michicoinscost;
        public int amount;

        public inventoryDrink(string name, int p, int m, int a)
        {
            name_drink = name;
            purrstige = p;
            michicoinscost = m;
            amount = a;
        }
    }
    [SerializeField] private TextMeshProUGUI drinktext;

    [SerializeField] private int foodcount;
    [SerializeField] private List<inventoryFood> foodlist;

    [System.Serializable]
    public struct inventoryFood
    {
        public string name_food;
        public int purrstige;
        public int michicoinscost;
        public int amount;

        public inventoryFood(string name, int p, int m, int a)
        {
            name_food = name;
            purrstige = p;
            michicoinscost = m;
            amount = a;
        }
    }
    [SerializeField] private TextMeshProUGUI foodtext;

    [SerializeField] private int employeecount;
    [SerializeField] private List<inventoryEmployee> employeelist;

    [System.Serializable]
    public struct inventoryEmployee
    {
        public string name_employee;
        public int purrstige;
        public int michicoinssalary;
        public string rol;

        public inventoryEmployee(string name, int p, int m, string r)
        {
            name_employee = name;
            purrstige = p;
            michicoinssalary = m;
            rol = r;
        }
    }
    [SerializeField] private TextMeshProUGUI employeetext;

    [SerializeField] public int moneycount;
    [SerializeField] private TextMeshProUGUI moneytext;

    [SerializeField] private int prestiegecount;
    [SerializeField] private TextMeshProUGUI prestigetext;

    [SerializeField] public bool ShowActive;

    [SerializeField] private inventoryShowActive ShowDetails;

    [System.Serializable]
    public struct inventoryShowActive
    {
        public string name_show;
        public int purrstige;
        public int cost;

        public inventoryShowActive(string name, int p, int m)
        {
            name_show = name;
            purrstige = p;
            cost = m;
        }
    }
    [SerializeField] private TextMeshProUGUI showtext;

    // Start is called before the first frame update
    void Start()
    {
        UpdateFood();
        UpdateDrink();
        UpdateEmployees();
        UpdateMoney();
        UpdatePurrstige();
        UpdateShow();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddFood(FoodSO food)
    {
        var index = foodlist.FindIndex(item => item.name_food == food.name);
        
        foodlist[index] = new inventoryFood(food.name, food.prestige, food.cost, foodlist[index].amount + 1);
        foodcount++;
        UpdateFood();
    }

    public void RemoveFood(FoodSO food)
    {
        var index = foodlist.FindIndex(item => item.name_food == food.name);

        foodlist[index] = new inventoryFood(food.name, food.prestige, food.cost, foodlist[index].amount - 1);
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

        drinklist[index] = new inventoryDrink(drink.name, drink.prestige, drink.cost, drinklist[index].amount + 1);
        drinkcount++;
        UpdateDrink();
    }

    public void RemoveDrink(DrinkSO drink)
    {
        var index = drinklist.FindIndex(item => item.name_drink == drink.name);

        drinklist[index] = new inventoryDrink(drink.name, drink.prestige, drink.cost, drinklist[index].amount - 1);
        drinkcount--;
        UpdateDrink();
    }

    public void UpdateDrink()
    {
        drinktext.text = drinkcount + " bebida";
    }

    public void AddEmployee(EmployeeSO employee)
    {
        employeelist.Add(new inventoryEmployee(employee.name, employee.prestigePerTurn, employee.costPerTurn, employee.rol));
        employeecount = employeelist.Count;
        UpdateEmployees();
    }

    public void RemoveEmployee(EmployeeSO employee)
    {
        employeelist.RemoveAll(a => a.name_employee == employee.name);
        employeecount = employeelist.Count;
        UpdateEmployees();
    }

    public void UpdateEmployees()
    {
        employeetext.text = employeecount + " empleados";
    }

    public void AddShow(NightShowSO nshow)
    {
        ShowDetails = new inventoryShowActive(nshow.name, nshow.prestige, nshow.cost);
        ShowActive = true;
        UpdateShow();
    }

    public void RemoveShow()
    {
        ShowDetails = new inventoryShowActive("", 0, 0);
        ShowActive = false;
        UpdateShow();
    }

    public void UpdateShow()
    {
        showtext.text = ShowDetails.name_show;
    }

    public void AddMichiCoins(int amount)
    {
        moneycount += amount;
        UpdateMoney();
    }

    public void RemoveMichiCoins(int amount)
    {
        moneycount -= amount;
        UpdateMoney();
    }

    public void UpdateMoney()
    {
        moneytext.text = moneycount + " Michimonedas";
    }

    public void AddPurrstige(int amount)
    {
        prestiegecount += amount;
        UpdatePurrstige();
    }

    public void RemovePurrstige(int amount)
    {
        prestiegecount -= amount;
        UpdatePurrstige();
    }

    public void UpdatePurrstige()
    {
        prestigetext.text = prestiegecount + " Purrstigio";
    }

    private void Awake()
    {
        instance = this;
    }
}
