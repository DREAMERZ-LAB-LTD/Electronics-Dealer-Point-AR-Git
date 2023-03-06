using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

/// <summary>
/// The object attatch with this script will take Damage in the there mesh Mesh after getting collision
/// </summary>
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(CollisionSystem))]
[RequireComponent(typeof(RayCastHitSystem))]
public class MeshDamager : MonoBehaviour
{
	
	[SerializeField] float minImpulse = 2;
	[SerializeField] float malleability = 0.05f;
	[SerializeField] float radius = 0.1f;
	[SerializeField] float impulse = 20f;

	[Tooltip("Deformate Mesh Collider is Hight Cost of CPU")]
	[SerializeField] bool canMeshColliderDeformate = false; 
	//Private
	private Mesh mesh;
	private MeshCollider meshCollider;
	private Vector3[] verts;
	//private Vector3[] iVerts;

	private void Start()
	{
		mesh = GetComponent<MeshFilter>().mesh;
		meshCollider = GetComponent<MeshCollider>();
		//iVerts = m.vertices;
	}	
	
	#region CallBack System
	CollisionSystem collisionSystem;
	RayCastHitSystem rayCastHitSystem;

   
    void OnEnable()
	{
		collisionSystem = GetComponent<CollisionSystem>();
		collisionSystem.CollisionEnterEvent += CollisionEnter;

		rayCastHitSystem = GetComponent<RayCastHitSystem>();
		rayCastHitSystem.rayCastHit += RayCastHit;
	}
	void OnDisable()
	{
		collisionSystem.CollisionEnterEvent -= CollisionEnter;

		rayCastHitSystem.rayCastHit -= RayCastHit;
	}
	#endregion

	private void CollisionEnter(Collision collision)
	{
		// Ignore Ingore rayCast layer
		if(collision.gameObject.layer == 2) return;
		//Calling deformation function
		Deformation(collision.GetContact(0).point, collision.GetContact(0).normal);
		
	}
	private void RayCastHit(RaycastHit hit)
	{
		Deformation(hit.point, -hit.normal);
	}

	
	private void Deformation(Vector3 tempPoint, Vector3 tempNormal)
	{
		//Get point, impulse mag, and normal
		Vector3 point = transform.InverseTransformPoint(tempPoint);
		Vector3 normal = transform.InverseTransformDirection(tempNormal);
		
		//impulse = collision.impulse.magnitude;
		if (impulse < minImpulse)
			return;

		//Deform vertices
		verts = mesh.vertices;
		//radius = collision.gameObject.transform.lossyScale.x;
		float scale; ///Declare outside of tight loop
		for (int i = 0; i < verts.Length; i++)
		{
			//Get deformation scale based on distance
			scale = Mathf.Clamp(radius - (point - verts[i]).magnitude, 0, radius);

			//Deform by impulse multiplied by scale and strength parameter
			verts[i] += normal * impulse * scale * malleability;
		}

		//Apply changes to collider and mesh
		mesh.vertices = verts;
		if(canMeshColliderDeformate) meshCollider.sharedMesh = mesh;
		
		//Recalculate mesh stuff
		///Currently gets unity to recalc normals. Could be optimized and improved by doing it ourselves.
		mesh.RecalculateNormals();
		mesh.RecalculateBounds();

	}

}
