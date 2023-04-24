using System.Collections.Generic;

public class StageObjectData
{
    public bool isCreated;
    public List<MapObjectData> objectData;
    public List<MassObject> objects;

    public void Inisialize()
    {
        isCreated = false;
        objectData = new List<MapObjectData>();
        objects = new List<MassObject>();
    }

    public void FlushObjects()
    {
        foreach(MassObject obj in objects)
        {
            obj.ObjectDestroy();
        }

        isCreated = false;
        objects.Clear();
    }

    public void AddMapObject(MapObjectData data)
    {
        objectData.Add(data);
    }
}
