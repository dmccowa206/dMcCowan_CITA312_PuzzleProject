using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder;

public class SlidePuzzle : MonoBehaviour
{
    [SerializeField] Transform piecePrefab;
    [SerializeField] float width = 1f;
    int size, emptyLocation;
    DoorControl doorCon;
    Vector3 adjustedStart;
    void Awake()
    {
        doorCon = GetComponent<DoorControl>();
        size = 3;
        adjustedStart = new Vector3(-width, 0f, width);
    }
    void Start()
    {
        CreateSlidePieces();
    }
    void CreateSlidePieces()
    {
        for (int row = 0; row < size; row++)
        {
            for(int col = 0; col < size; col++)
            {
                Transform piece = Instantiate(piecePrefab, gameObject.transform);
                piece.localPosition = new Vector3((width * col) - width,
                    0,
                    -(width * row) + width);
                piece.name = $"{row}{col}";
                if ((row == size -1) && (col == size -1))
                {
                    emptyLocation = (size * size) - 1;
                    piece.gameObject.SetActive(false);
                }
                else
                {
                    Mesh mesh = piece.GetComponent<MeshFilter>().mesh;
                    Vector2[] uv = new Vector2[4];
                    uv[0] = new Vector2((width * col), 1 - (width *(row + 1)));
                }
            }
        }
    }
    void OnFinish()
    {
        doorCon.OpenDoor();
    }
}
