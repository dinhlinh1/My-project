using UnityEngine;
using UnityEngine.AI; // Cần thư viện này để điều khiển NavMesh

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player; // Kéo PlayableCharacter vào đây

    void Update()
    {
        // Luôn cập nhật vị trí người chơi làm điểm đến cho quái vật
        if (player != null)
        {
            agent.SetDestination(player.position);
        }
    }
}