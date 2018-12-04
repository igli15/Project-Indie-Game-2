using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

[RequireComponent(typeof(CompanionManager))]
public class CompanionController : MonoBehaviour
{
	[SerializeField] 
	private Transform m_feetPos;

    [SerializeField] 
	private Transform m_aimIndicatorPivot;
	
	private CompanionManager m_manager;
	private Camera m_mainCam;

	private bool m_startCharge = false;

	private const int m_scorllScale = 10;

	private float m_chargeCount;

	private float m_timeCharging = 0;

	private bool m_isCharging = false;

	private Vector3 m_mouseDir;

	private Vector3 m_initPivotPos;

	private Projector m_projector;
	
	public static Action<CompanionController, ACompanion> OnMouseCharging;
	public static Action<CompanionController, ACompanion> OnCompanionThrown;
	public static Action<CompanionController, ACompanion> OnMouseRelease;

	// Use this for initialization
	void Start ()
	{
		m_aimIndicatorPivot.gameObject.SetActive(false);
		m_projector = m_aimIndicatorPivot.GetComponentInChildren<Projector>();
		
		m_mainCam = Camera.main;
		m_manager = GetComponent<CompanionManager>();

	}
	
	// Update is called once per frame
	void Update () 
	{
		
		AssignMouseDirection();
		
		HandleScrollWheel();
		
		HandleMouseAim();

		HandleNumInput();
		
		if (Input.GetKeyDown(KeyCode.O))
		{
			m_manager.DropCompanion(m_manager.GetSelectedCompanion());
		}
		
	}

	private void HandleScrollWheel()
	{
		if (Input.mouseScrollDelta.y  * m_scorllScale > 0)
		{
			m_manager.SelectPreviousCompanion();
			m_chargeCount = m_manager.GetSelectedCompanion().ChargeTime;
		}
		if (Input.mouseScrollDelta.y * m_scorllScale < 0)
		{
			m_manager.SelectNextCompanion();
			m_chargeCount = m_manager.GetSelectedCompanion().ChargeTime;
		}
	}

	private void HandleMouseAim()
	{
		ChargeCompanion(m_manager.GetSelectedCompanion());
	}

	private void HandleNumInput()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			m_manager.SelectCompanion(1);
		}
		else if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			m_manager.SelectCompanion(2);
		}
		else if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			m_manager.SelectCompanion(3);
		}
	}

	private void ThrowAtMousePos(ACompanion companion)
	{
		Ray camRay = m_mainCam.ScreenPointToRay(Input.mousePosition);
		Plane plane = new Plane(Vector3.up,m_feetPos.position);

		float rayDistance;
		Vector3 dir = Vector3.negativeInfinity;
		if (plane.Raycast(camRay, out rayDistance))
		{
			Vector3 point = camRay.GetPoint(rayDistance);
			dir = point - transform.position;
		}

		if (dir != Vector3.negativeInfinity)
		{
			if (!companion.IsThrown)
			{
				companion.Throw(dir);
			}
			
		}
	}

	private void ChargeCompanion(ACompanion companion)
	{
		if (Input.GetMouseButtonDown(0))
		{
			m_aimIndicatorPivot.gameObject.SetActive(true);
			//m_aimIndicatorPivot.position += transform.forward * companion.ThrowRange/4;
			m_chargeCount = companion.ChargeTime;
			m_isCharging = true;
			if (companion.OnStartCharging != null) companion.OnStartCharging(companion);
		}
		if (Input.GetMouseButton(0))
		{
			m_chargeCount -= Time.deltaTime;
			m_timeCharging += Time.deltaTime;
			
			m_aimIndicatorPivot.transform.rotation = Quaternion.LookRotation(m_mouseDir,transform.up);

			/*float width = 0;
			if (m_mouseDir.magnitude >= companion.ThrowRange)
			{
				width = companion.ThrowRange;
			}
			else
			{
				Debug.Log("hello");
				width = m_mouseDir.magnitude;
			}
			*/

			Vector3 localForward = transform.worldToLocalMatrix.MultiplyVector(m_aimIndicatorPivot.forward);
			
			float height = m_projector.orthographicSize * 2;
			m_projector.aspectRatio = companion.ThrowRange * 2/ height;
			
			m_aimIndicatorPivot.localPosition = localForward * companion.ThrowRange/2;
			

			if (OnMouseCharging != null) OnMouseCharging(this, companion);
			if(companion.OnCharging != null) companion.OnCharging(companion,m_timeCharging);	
			
			if (m_chargeCount <= 0)
			{
				if(companion.OnChargeFinished != null) companion.OnChargeFinished(companion);
				companion.IsCharged = true;
				m_chargeCount = companion.ChargeTime;
			}

		}
		
		if (Input.GetMouseButtonUp(0))
		{
			m_aimIndicatorPivot.gameObject.SetActive(false);
			m_aimIndicatorPivot.localPosition = Vector3.zero;
			if (OnMouseRelease != null) OnMouseRelease(this, companion);
			m_isCharging = false;
			m_timeCharging = 0;
			if (companion.IsCharged)
			{
				if (OnCompanionThrown != null) OnCompanionThrown(this, companion);
				TeleportCompanion(companion);
				ThrowAtMousePos(companion);
			}
			else
			{
				companion.Reset();
			}
		}
		
	}


	private void TeleportCompanion(ACompanion companion)
	{
		companion.SteeringComponent.NavMeshAgent.enabled = false;
		companion.transform.SetParent(m_feetPos.transform,true);
		companion.transform.position = m_feetPos.transform.position + m_feetPos.forward * 2;
	}
	
	
	private void OnTriggerStay(Collider other)
	{
		if (other.transform.CompareTag("PickupSphere"))
		{
			if (!other.transform.parent.GetComponent<Companion>().IsInParty && Input.GetKeyDown(KeyCode.E))
			{
				m_manager.PickCompanion(other.transform.parent.GetComponent<Companion>());
			}
		}
	}

	public void AssignMouseDirection()
	{
		Ray camRay = m_mainCam.ScreenPointToRay(Input.mousePosition);
		Plane plane = new Plane(Vector3.up,transform.position);

		float rayDistance;
		if (plane.Raycast(camRay, out rayDistance))
		{
			Vector3 point = camRay.GetPoint(rayDistance);
			m_mouseDir = point - transform.position;
		}

	}

	public float TimeCharging
	{
		get { return m_timeCharging; }
	}

	public Vector3 MouseDir
	{
		get { return m_mouseDir; }
	}

	public bool IsCharging
	{
		get { return m_isCharging; }
	}

}
