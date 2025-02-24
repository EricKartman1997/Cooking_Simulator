using UnityEngine;

public class BlenderPoints : MonoBehaviour
{
    private Transform _firstPoint;
    private Transform _secondPoint;
    private Transform _thirdPoint;

    public void Initialize(Transform _firstPoint,Transform _secondPoint,Transform _thirdPoint)
    {
        this._firstPoint = _firstPoint;
        this._secondPoint = _secondPoint;
        this._thirdPoint = _thirdPoint;
    }

    public Vector3 GetFirstPoint()
    {
        return _firstPoint.transform.position;
    }
    public Vector3 GetSecondPoint()
    {
        return _secondPoint.transform.position;
    }
    public Vector3 GetThirdPoint()
    {
        return _thirdPoint.transform.position;
    }
}
