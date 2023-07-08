using System.Collections.Generic;
using UnityEngine;

public class UnitPath : MonoBehaviour
{
	[SerializeField] private List<Vector3> wayPoints = new();
	public int PathLength => wayPoints.Count;

	[SerializeField] bool drawPoints = true;

	public Vector3 GetWayPointByIndex(int index)
	{
		if (index >= PathLength) index = PathLength - 1;
		Debug.Log($"{index}/{PathLength}");
		return wayPoints[index];
	}

	private void OnDrawGizmos()
	{
		for (int i = 0; i < wayPoints.Count; i++)
		{
			Gizmos.color = Color.HSVToRGB((float)i / (wayPoints.Count - 1), 0.8f, 1);
			if (drawPoints)
			{
				Gizmos.DrawSphere(wayPoints[i], 0.25f);
			}
			if (i > 0)
			{
				Gizmos.DrawLine(wayPoints[i], wayPoints[i - 1]);
			}
		}
	}
}
