using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
	public Transform target;
	public BoxCollider AttackArea;
	public bool isChase;
	public bool isAttack;

	Rigidbody rigid;
	BoxCollider boxcollider;
	Material mat;
	NavMeshAgent nav;
	Animator anim;

	// Start is called before the first frame update
    void Start()
    {
		rigid = GetComponent<Rigidbody>();
		boxcollider = GetComponent<BoxCollider>();
		mat = GetComponentInChildren<MeshRenderer>().material;
		nav = GetComponent<NavMeshAgent>();
		anim = GetComponentInChildren<Animator>();

		Invoke("ChaseStart", 2);
	}

    // Update is called once per frame
    void Update()
    {
		if(isChase)
			nav.SetDestination(target.position);
	}

	void ChaseStart()
	{
		isChase = true;
		anim.SetBool("isWalk", true);
	}

	void FreezeVelocity()
	{
		if (isChase)
		{
			rigid.velocity = Vector3.zero;
			rigid.angularVelocity = Vector3.zero;
		}
	}

	void Targeting()
	{
		float targetRadius = 1.5f;
		float targetRange = 3f;

		RaycastHit[] rayHits =
			Physics.SphereCastAll(transform.position, targetRadius, transform.forward, targetRange, LayerMask.GetMask("Player"));

		if(rayHits.Length > 0 && !isAttack)
		{
			StartCoroutine(Attack());
		}
	}

	IEnumerator Attack()
	{
		isChase = false;
		isAttack = true;
		anim.SetBool("isAttack", true);

		yield return new WaitForSeconds(0.2f);
		AttackArea.enabled = true;

		yield return new WaitForSeconds(1f);
		AttackArea.enabled = false;

		yield return new WaitForSeconds(1f);
		isChase = true;
		isAttack = false;
		anim.SetBool("isAttack", false);
	}

	void FixedUpdate()
	{
		Targeting();
		FreezeVelocity();
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Item")
		{
			Item item = other.GetComponent<Item>();
			Destroy(other.gameObject);
		}
	}
}
