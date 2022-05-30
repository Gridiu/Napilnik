public static void InstantiateNewObject()
{
    //Создание объекта на карте
}

public static void SetChance()
{
    _chance = Random.Range(0, 100);
}

public static int CalculateSalary(int hoursWorked)
{
    return _hourlyRate * hoursWorked;
}