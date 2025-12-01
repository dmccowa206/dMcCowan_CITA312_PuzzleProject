using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

public class SlidePuzzle : MonoBehaviour
{
    [SerializeField] Transform piecePrefab;
    [SerializeField] float width = 1f;
    List<Transform> pieces;
    int size, emptyLocation;
    DoorControl doorCon;
    bool shuffling = false;
    void Awake()
    {
        doorCon = GetComponent<DoorControl>();
        size = 3;
        pieces = new List<Transform>();
    }
    void Start()
    {
        CreateSlidePieces();
        Shuffle();
    }
    void Update()
    {
        // if (!shuffling && CheckCompletion())
        // {
        //     shuffling = true;
        //     StartCoroutine(WaitShuffle(0.5f));
        // }
    }
    void CreateSlidePieces()
    {
        for (int row = 0; row < size; row++)
        {
            for(int col = 0; col < size; col++)
            {
                Transform piece = Instantiate(piecePrefab, gameObject.transform);
                pieces.Add(piece);
                piece.localPosition = new Vector3((width * col) - width,
                    0,
                    -(width * row) + width);
                piece.name = $"{row * size + col}";
                if ((row == size -1) && (col == size -1))
                {
                    emptyLocation = (size * size) - 1;
                    piece.gameObject.SetActive(false);
                }
                else
                {
                    Mesh mesh = piece.GetComponent<MeshFilter>().mesh;
                    // Debug.Log(mesh.vertices.Length);
                    Vector3[] vertices = mesh.vertices;
                    Vector2[] uv = new Vector2[vertices.Length];
                    // uv[0] = new Vector2(width * col, 1 - (width *(row + 1)));
                    // uv[1] = new Vector2(width * (col + 1), 1 - (width *(row + 1)));
                    // uv[2] = new Vector2(width * col, 1 - (width * row));
                    // uv[3] = new Vector2(width * (col + 1), 1 - (width * row));
                    for (int i = 0; i < uv.Length; i++)
                    {
                        if (vertices[i].y > 0)
                        {
                            if(vertices[i].z > 0)
                            {
                                if (vertices[i].x < 0)
                                {
                                    uv[i] = new Vector2(width * col, 1 - (width *(row + 1)));
                                }
                                else
                                {
                                    uv[i] = new Vector2(width * (col + 1), 1 - (width *(row + 1)));
                                }
                            }
                            else
                            {
                                if (vertices[i].x < 0)
                                {
                                    uv[i] = new Vector2(width * col, 1 - (width * row));
                                }
                                else
                                {
                                    uv[i] = new Vector2(width * (col + 1), 1 - (width * row));
                                }
                            }
                        }
                        //uv[i] = new Vector2(vertices[i].x, vertices[i].z);
                        Debug.Log(vertices[i]);
                    }
                    mesh.uv = uv;
                }
            }
        }
    }
    public void MoveEmpty(Transform selectedPiece)
    {
        for (int i = 0; i < pieces.Count; i++)
        {
            if (selectedPiece == pieces[i])
            {
                if (SwapIfValid(i, -size, size)) { break; }
                if (SwapIfValid(i, +size, size)) { break; }
                if (SwapIfValid(i, -1, 0)) { break; }
                if (SwapIfValid(i, +1, size - 1)) { break; }
            }
        }
        Debug.Log(selectedPiece.name + CheckCompletion());
        if (CheckCompletion())
        {
            OnFinish();
        }
    }
    bool SwapIfValid(int i, int offset, int colCheck)
    {
        if (((i % size) != colCheck) && ((i + offset) == emptyLocation))
        {
            Debug.Log($"{i} {size}  {colCheck} {offset}");
            (pieces[i], pieces[i + offset]) = (pieces[i + offset], pieces[i]);
            (pieces[i].localPosition, pieces[i + offset].localPosition) = (pieces[i + offset].localPosition, pieces[i].localPosition);
            emptyLocation = i;
            return true;
        }
        return false;
    }
    bool CheckCompletion()
    {
        for (int i = 0; i < pieces.Count; i++)
        {
            if (pieces[i].name != $"{i}")
            {
                return false;
            }
        }
        return true;
    }
    IEnumerator WaitShuffle(float duration)
    {
        yield return new WaitForSeconds(duration);
        Shuffle();
        shuffling = false;
    }
    void Shuffle()
    {
        int count = 0;
        int last = 0;
        while (count < (size * size * size))
        {
            int rand = Random.Range(0, size * size);
            if (rand == last)
            {
                continue;
            }
            last = emptyLocation;
                if (SwapIfValid(rand, -size, size)) { count++; }
                else if (SwapIfValid(rand, +size, size)) { count++; }
                else if (SwapIfValid(rand, -1, 0)) { count++; }
                else if (SwapIfValid(rand, +1, size - 1)) { count++; }
        }
    }
    void OnFinish()
    {
        doorCon.OpenDoor();
    }
}
