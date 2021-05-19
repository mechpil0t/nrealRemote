using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smoothOut : MonoBehaviour {

	public Transform target;

	public float smoothTime = 0.3F;
	public float rotspeed = 0.2f;

	float moveLimit = 0.001f;
	float journeyLength;
	float startTime;
	float startTimeR;

	Vector3 velocity = Vector3.zero;
	Vector3 startpoint;
	Vector3 lookpoint;
	Quaternion startrotation;
	Quaternion targetrotation;

  public bool exceptY = false;

	public bool _lookDirection;

	public bool _offset;
	public Vector3 _setOff;
 	Vector3 _mod;

	void Start ()
	{
		_mod = new Vector3(0,0,0);
	}

	void Update () {

		if(target != null)
		{
			journeyLength = Vector3.Distance(transform.position, target.position);

			targetrotation = target.rotation;

			if (journeyLength > moveLimit)
			{
                var temp_pos = Vector3.SmoothDamp(transform.position, target.position, ref velocity, smoothTime);
								if(_offset == true)
								{
									_mod.x = temp_pos.x + _setOff.x;
									_mod.y = temp_pos.y + _setOff.y;
									_mod.z = temp_pos.z + _setOff.z;
									temp_pos = Vector3.SmoothDamp(transform.position, _mod, ref velocity, smoothTime);
								}

                if (exceptY) temp_pos.y = transform.position.y;

                transform.position = temp_pos;
            }

			if (targetrotation != startrotation)
			{
					float timeProgressed = (Time.time - startTimeR) / rotspeed;
					transform.rotation = Quaternion.Slerp(startrotation, targetrotation, timeProgressed);
			}

			startTimeR = Time.time;
			startrotation = transform.rotation;
			startpoint = transform.position;

			if(_lookDirection == true)
			{
				if(journeyLength > 0.3f)
				{
					lookpoint = new Vector3(target.position.x,transform.position.y,target.position.z);
					transform.LookAt(lookpoint);
				}
			}
		}
	}
}
