using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Slide_20_Draw : Audio_Narration
{
    public GameObject[] UI;
    public Button ContinueButton;
    public GameObject linePrefab;
    public GameObject currentLine;
    public LineRenderer lineRenderer;
    public EdgeCollider2D edgeCollider;
    public List<Vector2> fingerPositions;
    public Material lineMaterial; 
    public Color lineColor = Color.black; 
    public GameObject clearButton;
    public GameObject undoButton;
    public RectTransform canvasRect;
    private List<GameObject> drawnLines = new List<GameObject>();
    public string urlToOpen = "https://www.zogleber.    com";
    public Animator glistening;
    void Start()
    {
       
        clearButton.SetActive(false);
        undoButton.SetActive(false);

    }
   
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CreateLine();
        }
        if (Input.GetMouseButton(0))
        {
            Vector2 tempFingerPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Vector2.Distance(tempFingerPos, fingerPositions[fingerPositions.Count - 1]) > .1f)
            {
                UpdateLine(tempFingerPos);

                // Check for collision with UI elements having the "Border" tag
                Vector2 screenPos = Input.mousePosition;
                Vector2 canvasPos;

                if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPos, Camera.main, out canvasPos))
                {
                    if (RectTransformUtility.RectangleContainsScreenPoint(edgeCollider.GetComponent<RectTransform>(), screenPos, Camera.main))
                    {
                        Debug.Log("Line collided with a UI border element.");
                        // Implement the behavior you want when a collision occurs.
                    }
                }
            }
        }
    }
    void CreateLine()
    {
        currentLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
        lineRenderer = currentLine.GetComponent<LineRenderer>();
        edgeCollider = currentLine.GetComponent<EdgeCollider2D>();
        fingerPositions.Clear();
        fingerPositions.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        fingerPositions.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        lineRenderer.SetPosition(0, fingerPositions[0]);
        lineRenderer.SetPosition(1, fingerPositions[1]);
        edgeCollider.points = fingerPositions.ToArray();

        lineRenderer.startColor = lineColor; // Set the start color
        lineRenderer.endColor = lineColor;   // Set the end color
        lineMaterial.color = lineColor;

        lineRenderer.material = lineMaterial;
        drawnLines.Add(currentLine);

        clearButton.SetActive(true);
        undoButton.SetActive(true);
    }
    void UpdateLine(Vector2 newFingerPos)
    {
        fingerPositions.Add(newFingerPos);
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, newFingerPos);
        edgeCollider.points = fingerPositions.ToArray();
    }

    public void ClearLines()
    {
        foreach (GameObject line in drawnLines)
        {
            Destroy(line);
        }
        drawnLines.Clear();
        clearButton.SetActive(false);
        undoButton.SetActive(false);
    }

    public void UndoLine()
    {
        if (drawnLines.Count > 0)
        {
            GameObject lastLine = drawnLines[drawnLines.Count - 1];
            drawnLines.Remove(lastLine);
            Destroy(lastLine);

            if (drawnLines.Count == 0)
            {
                clearButton.SetActive(false);
                undoButton.SetActive(false);
            }
        }
    }
}
