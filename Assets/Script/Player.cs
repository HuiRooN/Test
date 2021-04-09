using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public float speed;
	public float jumppower;

	public int coin;
	public int heart;
	public int maxCoin;
	public int maxHeart;

	float hAxis;
	float vAxis;

	bool walkDown;
	bool jumpDown;
	bool DodgeDown;
	bool isJump;
	bool isDodge;
	bool isBorder;
	bool isDamage;
	public bool isDead;
	public bool isClear;

	private AudioSource audio1;
	private AudioSource audio2;
	private AudioSource audio3;
	private AudioSource audio4;

	public AudioClip jumpsound;
	public AudioClip hitsound;
	public AudioClip itemsound;
	public AudioClip dodgesound;

	Vector3 moveVec;

	Rigidbody rigid;
	Animator anim;
	MeshRenderer[] meshs;

	// Start is called before the first frame update
    void Start()
    {
		rigid = GetComponent<Rigidbody>();
		anim = GetComponentInChildren<Animator>();
		meshs = GetComponentsInChildren<MeshRenderer>();
		this.audio1 = this.gameObject.AddComponent<AudioSource>();
		this.audio1.clip = this.jumpsound;
		this.audio1.loop = false;
		this.audio2 = this.gameObject.AddComponent<AudioSource>();
		this.audio2.clip = this.hitsound;
		this.audio2.loop = false;
		this.audio3 = this.gameObject.AddComponent<AudioSource>();
		this.audio3.clip = this.itemsound;
		this.audio3.loop = false;
		this.audio4 = this.gameObject.AddComponent<AudioSource>();
		this.audio4.clip = this.dodgesound;
		this.audio4.loop = false;
	}

    // Update is called once per frame
    void Update()
    {
		GetInput();
		Move();
		Jump();
		Dodge();
	}

	void GetInput()
	{
		hAxis = Input.GetAxisRaw("Horizontal");
		vAxis = Input.GetAxisRaw("Vertical");
		walkDown = Input.GetButton("Walk");
		jumpDown = Input.GetButtonDown("Jump");
		DodgeDown = Input.GetButtonDown("Dodge");
	}

	void Move()
	{
		moveVec = new Vector3(hAxis, 0, vAxis).normalized;

		if (walkDown)
			transform.position += moveVec * speed * 0.3f * Time.deltaTime;
		else
			transform.position += moveVec * speed * Time.deltaTime;

		anim.SetBool("isRun", moveVec != Vector3.zero);
		anim.SetBool("isWalk", walkDown);

		transform.LookAt(transform.position + moveVec);
	}

	void Jump()
	{
		if(jumpDown && !isJump && !isDodge && !isDead)
		{
			rigid.AddForce(Vector3.up * jumppower, ForceMode.Impulse);
			anim.SetBool("isJump",true);
			anim.SetTrigger("Jumping");
			isJump = true;

			this.audio1.volume = 0.2f;
			this.audio1.Play();
		}
	}

	void Dodge()
	{
		if (DodgeDown && moveVec != Vector3.zero && !isJump && !isDodge && !isDead)
		{
			speed *= 1.3f;
			anim.SetTrigger("Dodging");
			isDodge = true;

			Invoke("DodgeEnd", 1);

			this.audio4.volume = 0.2f;
			this.audio4.Play();
		}
	}

	void DodgeEnd()
	{
		speed = 10;
		isDodge = false;
	}

	void FreezeRotation()
	{
		rigid.angularVelocity = Vector3.zero;
	}

	void FixedUpdate()
	{
		FreezeRotation();
	}

	void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.tag == "Floor")
		{
			isJump = false;
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Item" && other.gameObject != null)
		{
			Item item = other.GetComponent<Item>();
			switch (item.type)
			{
				case Item.Type.Coin:
					coin += 1;
					if (coin >= maxCoin)
						coin = maxCoin;
					break;
				case Item.Type.Heart:
					heart += 1;
					if (heart >= maxHeart)
						heart = maxHeart;
					break;
			}
			Destroy(other.gameObject);

			this.audio3.volume = 0.2f;
			this.audio3.Play();
		}
		else if (other.tag == "Attack")
		{
			if (!isDamage)
			{
				Attack attack = other.GetComponent<Attack>();
				heart -= attack.damage;
				Vector3 reactVec = transform.position - other.transform.position;

				StartCoroutine(OnDamage(reactVec));

				this.audio2.volume = 1f;
				this.audio2.Play();
			}
		}
		else if (other.tag == "Goal" && coin == maxCoin)
			isClear = true;
	}

	IEnumerator OnDamage(Vector3 reactVec)
	{
		isDamage = true;
		foreach(MeshRenderer mesh in meshs)
		{
			if (heart > 0)
			{
				mesh.material.color = Color.red;
				reactVec = reactVec.normalized;
				reactVec += Vector3.up;
				rigid.AddForce(reactVec * 1, ForceMode.Impulse);
			}
			else
				mesh.material.color = Color.gray;
		}

		yield return new WaitForSeconds(0.5f);

		isDamage = false;
		foreach(MeshRenderer mesh in meshs)
		{
			if (heart > 0)
				mesh.material.color = Color.white;
			else
			{
				mesh.material.color = Color.gray;
				Die();
			}
		}
	}

	void Die()
	{
		anim.SetTrigger("isDie");
	}

}
