using System.Collections;
using UnityEngine;
using UnityEngine.AI;
public class PlayerMove : MonoBehaviour
{
    public bool isAllowMovement = true;
    public Vector3 BasePosition;
    private GameObject Home;
    private NavMeshAgent m_Agent;
    private bool isPlayerHaveHouse=false;
    private bool movementActive = false;    
    RaycastHit m_HitInfo;

    void Start()
    {
        isAllowMovement = true;
        m_Agent = GetComponent<NavMeshAgent>();
    }

    public void CanMove(bool can)
    {
        isAllowMovement = can;
    }
    public bool IsMoving()
    {
        return isAllowMovement;
    }
    public bool HaveHouse()
    {
        return isPlayerHaveHouse;
    }
    public GameObject GetHouse()
    {
        return Home;
    }
    public void SetHome(GameObject homeBase)
    {
        isPlayerHaveHouse = true;
        Home = homeBase;
    }
    public void TeleportBackToBase()
    {
        Home.SetActive(true);
        m_Agent.Warp(BasePosition);
    }
    public Vector3 GetPlayerPosition()
    {
        return transform.position;
    }
    void Update()
    {
        if (movementActive)
        {
            if (m_Agent.remainingDistance <= m_Agent.stoppingDistance)
            {
                if (!m_Agent.hasPath || m_Agent.velocity.sqrMagnitude == 0f)
                {
                    movementActive = false;
                }
            }
        }
        if (Input.GetMouseButtonDown(0) && isAllowMovement)
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out m_HitInfo))
            {
                m_Agent.destination = m_HitInfo.point;
                movementActive = true;
               
               // Instantiate(crystalPref, moveToPos + new Vector3(0, 0.3f, 0), Quaternion.identity);

            }
        }
    }
}
