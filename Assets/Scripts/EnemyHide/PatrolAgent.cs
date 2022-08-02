using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolAgent : MonoBehaviour
{
    public static PatrolAgent instance;
    [SerializeField] private Transform[] points;
    [SerializeField] float RenmainingDistance = 2f;

    public float waitTime = 1f;
    float time = 0;
    public Transform GroundCheck;
    [SerializeField] float GroundCheckDistance = 0.4f;
    public LayerMask groundSwimMask;

    public GameObject cage; //cái lồng

    bool isGrounded;
    Animator animator;
    bool isMove ;

    int destinationPoint = 0;
    public NavMeshAgent agent;

    private float currentSpeedAgent;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        currentSpeedAgent = agent.speed;
        animator = GetComponentInChildren<Animator>();
        if(UIController.instance.isPlayGame==true)
        {
            moveToFirstPoint();
        }    

    }


    private void Update()
    {
        if(UIController.instance.isPlayGame ==false)
        {
            agent.speed = 0f;
            animator.SetFloat("MoveAnim", 0);
            animator.SetFloat("IdleAnim", 1);
            isMove = false;

        }
        if (cage.activeInHierarchy ==true)
        {
            agent.speed = 0f;
        }    

        if (!agent.pathPending && agent.remainingDistance < RenmainingDistance && UIController.instance.isPlayGame==true)
        {

            GoToNextPoint();
        }
        CheckGroundToSwim();
    }
    public void moveToFirstPoint()
    {
        int random = Random.Range(0, points.Length);
        agent.destination = points[random].position;
        animator.SetFloat("MoveAnim", 1);
    }    
    public void GoToNextPoint()
    {
        
        if (points.Length ==0)
        {
            Debug.Log(" bạn cần setup thêm point");
            enabled = false;
            return;
        }

        time += Time.deltaTime;
        if(time >waitTime)
        {
            agent.speed = currentSpeedAgent;
            animator.SetFloat("IdleAnim", 0);
            animator.SetFloat("MoveAnim", 1);
            agent.destination = points[destinationPoint].position;
            destinationPoint = (destinationPoint + 1) % points.Length;

            isMove = true;
            time = 0;
 
        }
        else
        {
            agent.speed = 0;
            animator.SetFloat("MoveAnim", 0);
            animator.SetFloat("IdleAnim", 1);
            isMove = false;

        }    
      
    }

    public void CheckGroundToSwim()
    {
        isGrounded = Physics.CheckSphere(GroundCheck.position, GroundCheckDistance, groundSwimMask);
        if (isGrounded)
        {
            animator.SetBool("isGroundSwim", true);
        }
        else
        {
            animator.SetBool("isGroundSwim", false);
        }

        if (isGrounded && isMove == false)
        {
            animator.SetBool("isGroundSwimIdle", true);
        }

        if (isGrounded && isMove == true)
        {
            animator.SetBool("isGroundSwimIdle", false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
       bool Checked = Physics.CheckSphere(GroundCheck.position, GroundCheckDistance, groundSwimMask);
        if (other.gameObject.name== "sensor")
        {
            cage.SetActive(true);
            agent.speed = 0f;
            animator.SetFloat("MoveAnim", 0);
            if (Checked == true )
            {
                animator.SetBool("isGroundSwimIdle", true);
            }
            else
            {
                animator.SetFloat("IdleAnim", 1);
            }    
        }

        if (other.gameObject.tag == "AIHide")
        {
            cage.SetActive(false);
            agent.speed = currentSpeedAgent;
            animator.SetFloat("MoveAnim", 1);
            animator.SetFloat("IdleAnim", 0);
        }

        if(other.gameObject.tag =="TrapSprites")
        {
            Destroy(this.gameObject);
        }

        if (other.gameObject.tag == "PaintsGlue")
        {
            StartCoroutine(changeMoveSpeedSlow());
        }

    }
    IEnumerator changeMoveSpeedSlow()
    {
        agent.speed = agent.speed/2.5f;
        yield return new WaitForSeconds(5f);
        agent.speed = currentSpeedAgent;
    }

}
