using Unity.Burst;
using Unity.Entities;

partial struct TimerSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        TimerJob job = new TimerJob();
        job.deltaTime = SystemAPI.Time.DeltaTime;
        job.ScheduleParallel();
    }

    [BurstCompile]
    public void OnDestroy(ref SystemState state)
    {
        
    }

    partial struct TimerJob : IJobEntity
    {
        public float deltaTime;

        public void Execute(ref TimerComponentData _timerData)
        {
            _timerData.time -= deltaTime;
            if (_timerData.time < 0)
                _timerData.timeOut = true;
        }
    }
}
