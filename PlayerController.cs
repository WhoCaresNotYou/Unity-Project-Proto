using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private LayerMask clickableLayer;

    private Resource res;

    private string resourceName;

    public Camera cam;

    public NavMeshAgent agent;

    private GameObject[] Hives;
    private GameObject temp;

    private bool gathering;
    private bool move, dropOff;

    void Start()
    {
        gathering = false;
        move = true;
        dropOff = false;

        Hives = GameObject.FindGameObjectsWithTag("Hive");
    }

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            
            /*CLICKED ON RESOURCE CHECK*/
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),
                                out hit, 100.0f, clickableLayer))
            {
                resourceName = hit.collider.tag;

                res = hit.transform.GetComponent<Resource>();

                temp = this.gameObject.transform.GetChild(0).gameObject;

                Gather();
            }

            if(Physics.Raycast(ray, out hit))
            {
                /*PLAYER MOVEMENT*/
                agent.SetDestination(hit.point);

                /*CHECK IF PLAYER MOVES*/
                if (move == false)
                    endGather();
            }
        }

        /*CHECK IF PLAYER'S GATHERING*/
        if (agent.velocity == Vector3.zero && gathering)
            move = false;

        /*CHECK FOR DROP_OFF CONDITIONS*/
        if (res != null && res.limitReached)
        {
            endGather();
            DropOff();
        }
    }

    void Gather()
    {
        res.clicked = true;

        gathering = true;

        dropOff = false;
    }

    void endGather()
    {
        res.clicked = false;

        gathering = false;

        move = true;
    }

    void DropOff()
    {
        int i = 0, shortestIndex = 0;
        float dist = Vector3.Distance(agent.transform.position, Hives[0].transform.position), temp = 0.0f;

        while(i > Hives.Length)
        {
            i++;
            temp = Vector3.Distance(agent.transform.position, Hives[i].transform.position);
            if (dist > temp)
            {
                dist = temp;
                shortestIndex = i;
            }
        }
        if (dropOff == false)
        {
            agent.SetDestination(Hives[shortestIndex].transform.position);            
            dropOff = true;
        }

        dist = Vector3.Distance(Hives[shortestIndex].transform.position, agent.transform.position);
        
        if (dist < 1.0f)
        {
            agent.velocity = Vector3.zero;
            Back2Res();
        }
    }

    void Back2Res()
    {
        agent.SetDestination(res.transform.position);

        move = true;

        Gather();

        res.limitReached = false;
    }
}
