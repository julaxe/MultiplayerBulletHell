using UnityEngine;

public class Line : MonoBehaviour
{
    public GameObject arrowHead;
    private LineRenderer _lineRenderer;
    [SerializeField] private float resolution = 0.1f;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }
    
    public void UpdateLine(Vector2 newPos)
    {
        if (!CanAddNewPosition(newPos)) return;
        _lineRenderer.positionCount++;
        _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, newPos);
    }

    private bool CanAddNewPosition(Vector2 newPos)
    {
        if (_lineRenderer.positionCount == 0) return true;
        return Vector2.Distance(_lineRenderer.GetPosition(_lineRenderer.positionCount -1), newPos) > resolution;
    }

    public Vector3[] GetPositions()
    {
        Vector3[] positions = new Vector3[_lineRenderer.positionCount];
        _lineRenderer.GetPositions(positions);
        return positions;
    }
    
}
