using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleShuffle : MonoBehaviour
{
	public int gridSize = 3; // Kích thước lưới (3x3)
	public GameObject tilePrefab; // Prefab của mảnh ghép
	public GameObject emptyTilePrefab; // Ô trống
	private GameObject[,] grid; // Lưới chứa các mảnh ghép
	private Vector2Int emptyPosition; // Vị trí của ô trống trong grid
	public Transform gridContainer; // Container cho GridLayoutGroup

	public GameObject[] correctIndexPuzzle;

	private void Start()
	{
		InitializeGrid();
	}

	void InitializeGrid()
	{
		grid = new GameObject[gridSize, gridSize];
			// Đặt các mảnh ghép vào lưới và thiết lập ô trống
		for (int i = 0; i < gridSize; i++)
		{
			for (int j = 0; j < gridSize; j++)
			{
				// Vị trí trong lưới
				Vector2Int position = new Vector2Int(i, j);
				// Đặt các mảnh ghép vào grid, và để một ô trống
				if (i == gridSize - 1 && j == gridSize - 1)
				{
					Debug.Log("This is a last cell");
					// Vị trí cuối cùng là ô trống
					GameObject emptyTile = Instantiate(emptyTilePrefab, gridContainer);
					emptyTile.SetActive(true);
					grid[i, j] = emptyTile;
					emptyPosition = position;
				}
				else
				{
					// Đặt mảnh ghép vào vị trí trên lưới
					GameObject tile = Instantiate(tilePrefab, gridContainer);
					tile.SetActive(true);
					tile.gameObject.GetComponent<Image>().color = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f));
					grid[i, j] = tile;
				}
			}
		}
	}
	public void TryMoveTile(GameObject tile)
	{
		// Tìm vị trí của mảnh ghép trong grid
		Vector2Int tilePosition = FindTilePosition(tile);
		// Kiểm tra xem ô trống có kề với mảnh ghép không
		if (IsAdjacent(tilePosition, emptyPosition))
		{
			// Đổi vị trí của mảnh ghép và ô trống
			SwapTiles(tilePosition, emptyPosition);
		}
	}

	Vector2Int FindTilePosition(GameObject tile)
	{
		for (int i = 0; i < gridSize; i++)
		{
			for (int j = 0; j < gridSize; j++)
			{
				if (grid[i, j] == tile)
					return new Vector2Int(i, j);
			}
		}
		return Vector2Int.zero;
	}

	bool IsAdjacent(Vector2Int pos1, Vector2Int pos2)
	{
		// Kiểm tra xem 2 vị trí có kề nhau không
		return (Mathf.Abs(pos1.x - pos2.x) == 1 && pos1.y == pos2.y) ||
			   (Mathf.Abs(pos1.y - pos2.y) == 1 && pos1.x == pos2.x);
	}

	void SwapTiles(Vector2Int tilePosition, Vector2Int emptyPosition)
	{
		// Đổi vị trí trong mảng grid
		GameObject tempTile = grid[tilePosition.x, tilePosition.y];
		grid[tilePosition.x, tilePosition.y] = grid[emptyPosition.x, emptyPosition.y];
		grid[emptyPosition.x, emptyPosition.y] = tempTile;

		// Đổi thứ tự sibling index để cập nhật trên màn hình
		Transform tileTransform = grid[tilePosition.x, tilePosition.y].transform;
		Transform emptyTileTransform = grid[emptyPosition.x, emptyPosition.y].transform;

		int tileSiblingIndex = tileTransform.GetSiblingIndex();
		int emptyTileSiblingIndex = emptyTileTransform.GetSiblingIndex();

		// Đổi thứ tự hiển thị giữa hai tile
		tileTransform.SetSiblingIndex(emptyTileSiblingIndex);
		emptyTileTransform.SetSiblingIndex(tileSiblingIndex);

		// Cập nhật vị trí của ô trống
		this.emptyPosition = tilePosition;

		CheckWinCondition();
	}

	bool CheckWinCondition()
	{
		for(int i = 0; i < gridContainer.childCount; i++)
		{
			Transform child = gridContainer.GetChild(i);
			if (!child.gameObject.activeSelf) continue;

			if (child.gameObject != correctIndexPuzzle[i]) return false; 
		}
		return true;
	}
}
