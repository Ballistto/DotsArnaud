using Unity.Entities;

public struct TimerComponentData : IComponentData
{
    public float time;
    public bool timeOut;
}
