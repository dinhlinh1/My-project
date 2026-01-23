using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class EnemyAI : MonoBehaviour
{
    [Header("Cấu hình mục tiêu")]
    public Transform player;

    [Header("Tuần tra (Patrol)")]
    public List<Transform> waypoints; // Kéo các Point1, Point2 vào đây
    private int currentWaypointIndex = 0;
    public float patrolSpeed = 2f;
    public float chaseSpeed = 3.5f;

    [Header("Thông số khoảng cách")]
    public float lookRadius = 10f;
    public float stoppingDistance = 1.5f;

    private NavMeshAgent agent;
    private Animator anim;
    private float waitTimer;
    public float waitTimeAtWaypoint = 2f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        agent.stoppingDistance = stoppingDistance;

        if (waypoints.Count > 0) SetNextWaypoint();
    }

    void Update()
    {
        // Nếu không có Player được gán, quái chỉ đi tuần
        float distance = player != null ? Vector3.Distance(player.position, transform.position) : float.MaxValue;

        if (player != null && distance <= lookRadius)
        {
            // TRẠNG THÁI: ĐUỔI THEO
            agent.speed = chaseSpeed;
            agent.stoppingDistance = stoppingDistance; // Giữ khoảng cách khi đánh
            agent.SetDestination(player.position);

            if (distance <= agent.stoppingDistance)
            {
                FaceTarget();
                anim.SetBool("isAttacking", true);
            }
            else
            {
                anim.SetBool("isAttacking", false);
            }
        }
        else
        {
            // TRẠNG THÁI: QUAY LẠI ĐI TUẦN (Thay vì đứng yên)
            anim.SetBool("isAttacking", false);
            agent.speed = patrolSpeed;
            agent.stoppingDistance = 0; // Khi đi tuần thì cần đến sát điểm waypoint (0m)
            Patrol();
        }

        anim.SetFloat("Speed", agent.velocity.magnitude);
    }

    void Patrol()
    {
        if (!agent.pathPending && agent.remainingDistance <= 0.2f)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer >= waitTimeAtWaypoint)
            {
                SetNextWaypoint();
                waitTimer = 0;
            }
            else
            {
                anim.SetFloat("Speed", 0); // Đứng yên chờ
            }
        }
    }

    void SetNextWaypoint()
    {
        if (waypoints == null || waypoints.Count == 0) return;
        agent.SetDestination(waypoints[currentWaypointIndex].position);
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Count;
    }

    void FaceTarget()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}