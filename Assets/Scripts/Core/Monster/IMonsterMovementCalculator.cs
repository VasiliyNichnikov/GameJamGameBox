using UnityEngine;

namespace Core.Monster
{
    public interface IMonsterMovementCalculator
    {
        float GetDistanceFromPlayerToPointRadius(Vector3 point);

        float GetDistanceFromEnemyToPointRadius(Vector3 point);
    }
}