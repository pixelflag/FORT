public class Platoon
{
    public Unit captain { get; private set; }
    public Unit[] servants { get; private set; }

    public int maxServant => 20;


    public Platoon(UnitType captainType)
    {

    }

    public void AddServant(UnitType type)
    {
        // servants.Add(new Platoon());
    }

}
