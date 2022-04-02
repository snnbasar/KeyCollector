using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum LookForObstacles
{
    Look,
    DontLook
}
public class AIVision : MonoBehaviour
{
    private AIController aiController;
    private LookForObstacles lookForObstacles;
    public LayerMask layerMask;

    private Transform character;
    // Start is called before the first frame update
    void Start()
    {
        aiController = GetComponentInParent<AIController>();
        lookForObstacles = LookForObstacles.DontLook;
        character = KarakterKontrol.instance.transform;

    }

    private void Update()
    {
        switch (lookForObstacles)
        {
            case LookForObstacles.Look:
                DrawRayCast();
                break;
            case LookForObstacles.DontLook:
                //doNothing
                break;
            default:
                break;
        }
    }

    private void DrawRayCast()
    {
        RaycastHit hit;
        Vector3 fromPosition = aiController.transform.position;
        Vector3 toPosition = character.position;
        Vector3 direction = toPosition - fromPosition;

        
        if (Physics.Raycast(fromPosition, direction, out hit, layerMask))
        {
            Debug.Log(hit.collider.gameObject.name);
            if (hit.transform.CompareTag("Player") && IsPlayerAlive())
            {
                Debug.Log("Player Açýk ve net");
                aiController.ChangeAIStateToKillPlayer();
            }
        }

        Debug.DrawRay(fromPosition, direction, Color.red);
    }

    private bool IsPlayerAlive()
    {
        return GameManager.instance.GetPlayerAlive;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Triggera Girdi");
            lookForObstacles = LookForObstacles.Look;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Triggerdan Çýktý");
            lookForObstacles = LookForObstacles.DontLook;
        }
    }
}
