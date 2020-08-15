using System;
using System.Collections;
using System.Collections.Generic;
using Objects;
using UnityEditor.Build.Content;
using UnityEngine;

public class CameraController : ObstacleController 
{
	public enum CameraDirection { Clockwise, AntiClockwise }

	[Serializable]
	public class TargetAngle
	{
		[Range(0,359)] public int angle;
		public bool changeDirection;
	}

	public List<TargetAngle> targetAngles;
	public float rotSpeed;
	public CameraDirection initCameraDirection;

	private int currentDirection;
	private int targetIndex;
	private TargetAngle PreviousTarget => targetAngles[IndexToRange(targetIndex - 1, targetAngles)];
	private TargetAngle CurrentTarget => targetAngles[IndexToRange(targetIndex, targetAngles)];
	
	private const float BaseRotationSpeed = 15f;

	private readonly List<Quaternion> paths = new List<Quaternion>();
	private int indexPath;
	private Quaternion CurrentPath => paths[IndexToRange(indexPath, paths)];

	private float time;
	
	private void Start () {
        currentDirection = initCameraDirection == CameraDirection.Clockwise ? -1 : 1;
        transform.rotation = Quaternion.Euler (0, 0, CurrentTarget.angle);
        NextTarget();
	}

	private void Update(){
		Rotate();
	}

	private void Rotate(){
		if (stop) return;

		transform.rotation = Quaternion.RotateTowards(transform.rotation, CurrentPath, BaseRotationSpeed * rotSpeed * Time.deltaTime);
		
		if (Math.Abs(transform.eulerAngles.z - CurrentPath.eulerAngles.z) < 1f)
		{
			transform.rotation = CurrentPath;
			// Debug.Log($"Target reached : {CurrentPath.eulerAngles}");
			NextPath();
			if (indexPath == 0)
			{
				if (CurrentTarget.changeDirection) currentDirection *= -1;
				NextTarget();
				StartCoroutine(Wait ());	
			}
		}
	}

	private void NextTarget()
	{
		targetIndex = IndexToRange(targetIndex + 1, targetAngles);
		CalculatePath();
		NextPath();
	}

	private void NextPath()
	{
		time = 0f;
		indexPath = IndexToRange(indexPath + 1, paths);
	}

	private void CalculatePath()
	{
		var previousRotation = PreviousTarget.angle;
		var targetRotation = CurrentTarget.angle;

		paths.Clear();

		paths.Add(Quaternion.Euler(0, 0, previousRotation));

		if (currentDirection * targetRotation < currentDirection * previousRotation)
			targetRotation += currentDirection * 360;
		var diffRotation = (targetRotation - previousRotation) * currentDirection;
		
		if (diffRotation == 0)
		{
			paths.AddRange(new []
			{
				Quaternion.Euler(0,0,previousRotation - 90 * currentDirection),
				Quaternion.Euler(0,0,previousRotation - 180 * currentDirection),
				Quaternion.Euler(0,0,previousRotation - 270 * currentDirection)
			});
		}
		else if (diffRotation > 180f)
		{
			var firstQuarterRotation = previousRotation + (targetRotation - previousRotation) / 3;
			var secondQuarterRotation = previousRotation + 2 * (targetRotation - previousRotation) / 3;
			paths.AddRange(new []
			{
				Quaternion.Euler(0,0,firstQuarterRotation),
				Quaternion.Euler(0,0,secondQuarterRotation),
			});
		}
		else if(diffRotation > 90f)
		{
			var middleRotation = previousRotation + (targetRotation - previousRotation) / 2;
			paths.Add(Quaternion.Euler(0, 0, middleRotation));
		}

		paths.Add(Quaternion.Euler(0, 0, targetRotation));
	}
	
	private int IndexToRange<T>(int index, IList<T> array) => (index + array.Count) % array.Count;
}
