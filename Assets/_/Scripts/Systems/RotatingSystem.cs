using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

partial struct RotatingSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        RotatingJob job = new RotatingJob();
        job.deltaTime = SystemAPI.Time.DeltaTime;
        job.ScheduleParallel();

        //foreach (var (transform, _rotation) 
        //    in SystemAPI.Query<RefRW<LocalTransform>, RefRO<RotatingSpeedComponentData>>())
        //{
        //    transform.ValueRW = transform.ValueRO.RotateY(SystemAPI.Time.DeltaTime * _rotation.ValueRO.Value);
        //    FindPrimeNumber(300);
        //}

    }

    public void OnDestroy(ref SystemState state)
    {
        
    }

    

    partial struct RotatingJob : IJobEntity
    {
        public float deltaTime;
        public void Execute(ref LocalTransform transform, in RotatingSpeedComponentData _rotationData, TimerComponentData _timerData)
        {
            if (_timerData.timeOut) return;
            FindPrimeNumber(300);
            transform = transform.RotateY(_rotationData.value * deltaTime);
        }

        private long FindPrimeNumber(int n)
        {
            int count = 0;
            long a = 2;
            while (count < n)
            {
                long b = 2;
                int prime = 1;// to check if found a prime
                while (b * b <= a)
                {
                    if (a % b == 0)
                    {
                        prime = 0;
                        break;
                    }
                    b++;
                }
                if (prime > 0)
                {
                    count++;
                }
                a++;
            }
            return (--a);
        }
    }
}
