using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour
{
    public Rigidbody bullet;
    public float power = 1500f;
    public float moveSpeed = 2f;
    public float totalForce;
    public int maxBullets = 5;
    private Transform _tranform;
    private bool _fireIsDown;
    private Queue<Rigidbody> _bullets;
    // Use this for initialization
	void Start ()
	{
	    _tranform = transform;
        _bullets = new Queue<Rigidbody>(maxBullets);
	    for (int i = 0; i < maxBullets; i++)
	    {
	        var b = (Rigidbody) Instantiate(bullet);
            b.gameObject.active = false;
            _bullets.Enqueue(b);
	    }
	}
	
	// Update is called once per frame
	void Update ()
	{
	    var horizontal = Input.GetAxis("Horizontal")*Time.deltaTime*moveSpeed;
	    var vertical = Input.GetAxis("Vertical")*Time.deltaTime*moveSpeed;
        _tranform.Translate(new Vector3(horizontal, vertical));
        if (Input.GetButtonDown("Fire1"))
        {
            _fireIsDown = true;
        }
        if (_fireIsDown)
            totalForce += Time.deltaTime * power;
        if (Input.GetButtonUp("Fire1"))
        {
            _fireIsDown = false;
            
            var instance = _bullets.Dequeue();
            ResetBullet(instance);
            _bullets.Enqueue(instance);
            
            var forward = _tranform.TransformDirection(Vector3.forward);
            instance.AddForce(forward * totalForce);
            totalForce = 0f;
        }
	}

    private void ResetBullet(Rigidbody instance)
    {
        instance.gameObject.active = true;
        instance.velocity = Vector3.zero;
        instance.angularVelocity = Vector3.zero;
        instance.inertiaTensorRotation = Quaternion.identity;
        //instance.inertiaTensor = Vector3.zero;
        instance.isKinematic = true;
        instance.isKinematic = false;
        instance.position = _tranform.position;
        instance.rotation = _tranform.rotation;
        
    }
}
