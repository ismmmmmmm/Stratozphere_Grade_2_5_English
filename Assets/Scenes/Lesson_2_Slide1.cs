using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Lesson_2_Slide1 : Audio_Narration
{
    public GameObject[] UI;
    public Button ContinueButton;
    public GameObject linePrefab;
    public GameObject lineParent;
    public GameObject currentLine;
    public LineRenderer lineRenderer;
    public EdgeCollider2D edgeCollider;
    public List<Vector2> fingerPositions;
    public Material lineMaterial;
    public Color lineColor = Color.black;
    private Color selectedColor = Color.black;
    public GameObject clearButton;
    public GameObject saveButton;
    public GameObject colorButton, palette;
    public RectTransform canvasRect;
    public Button[] colorButtons;
    public TextMeshProUGUI textMeshProUGUI;
    public string urlToOpen = "https://www.zogleber.com";
    private int currentColorNumber;
    private List<GameObject> drawnLines = new List<GameObject>();
    void Start()
    {
        for (int i = 0; i < colorButtons.Length; i++)
        {
            int colorIndex = i; // Capture the index in a local variable to avoid closure issues
            colorButtons[i].onClick.AddListener(() => ChangeColor(colorIndex));
        }
        clearButton.SetActive(false);
        saveButton.SetActive(false);

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
        currentLine.transform.SetParent(lineParent.transform);  // Set the parent
        lineRenderer = currentLine.GetComponent<LineRenderer>();
        edgeCollider = currentLine.GetComponent<EdgeCollider2D>();
        fingerPositions.Clear();
        fingerPositions.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        fingerPositions.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        lineRenderer.SetPosition(0, fingerPositions[0]);
        lineRenderer.SetPosition(1, fingerPositions[1]);
        edgeCollider.points = fingerPositions.ToArray();

        lineRenderer.startColor = selectedColor; // Use the selected color
        lineRenderer.endColor = selectedColor;
        lineMaterial.color = selectedColor;

        lineRenderer.material = lineMaterial;
        drawnLines.Add(currentLine);

        clearButton.SetActive(true);
        saveButton.SetActive(true);
    }


    void UpdateLine(Vector2 newFingerPos)
    {
        lineRenderer.startColor = selectedColor; // Use the selected color
        lineRenderer.endColor = selectedColor;
        lineMaterial.color = selectedColor;

        lineRenderer.material = lineMaterial;
        fingerPositions.Add(newFingerPos);
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, newFingerPos);
        edgeCollider.points = fingerPositions.ToArray();
    }

    public void ClearLines()
    {
        Debug.Log("Clearing lines...");
        foreach (GameObject line in drawnLines)
        {
            Destroy(line);
        }
        drawnLines.Clear();
        clearButton.SetActive(false);
        saveButton.SetActive(false);
    }


    private void ChangeColor(int colorIndex)
    {
        selectedColor = GetColorFromIndex(colorIndex);
        colorButton.SetActive(true);
        palette.SetActive(false);
        textMeshProUGUI.fontMaterial.color = selectedColor;
    }
    private Color GetColorFromIndex(int index)
    {
        // Define your color palette here
        Color[] colorPalette = new Color[]
        {
            Color.red,
    Color.yellow,
    Color.blue,
    new Color(1.0f, 0.5f, 0.0f), // Orange
    Color.green,
    new Color(0.5f, 0.0f, 1.0f), // Violet
        };

        // Make sure the index is within the range of the color palette
        index = Mathf.Clamp(index, 0, colorPalette.Length - 1);

        return colorPalette[index];
    }

    public void NextCoroutine()
    {
        StartCoroutine(NextCanvas());
    }
    private IEnumerator NextCanvas()
    {
       yield return new WaitForSeconds(1);
    }

    public void GotoZogleber()
    {
        Application.OpenURL(urlToOpen);
        StartCoroutine(NextSceneCoroutine(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void Scene_Save()
    {
        saveButton.SetActive(false );
        clearButton.SetActive(false);
        LoadScene();
    }
}
