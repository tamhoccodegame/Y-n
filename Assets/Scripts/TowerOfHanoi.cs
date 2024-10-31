using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerOfHanoi : MonoBehaviour
{
	public GameObject[] pegs; // Cột
	public GameObject[] disks; // Đĩa
	public Transform pulledPosition;
	private Stack<Transform>[] towers; // Danh sách các cột
	private bool isMoving = false; // Kiểm tra trạng thái di chuyển đĩa
	private Transform movingDisk; // Đĩa đang di chuyển
	private int fromPegIndex; // Cột từ
	private int toPegIndex; // Cột đến

	int currentPegIndex = 0;

	private void Start()
	{
		towers = new Stack<Transform>[pegs.Length];
		for (int i = 0; i < pegs.Length; i++)
		{
			towers[i] = new Stack<Transform>();
		}

		// Tạo và thêm đĩa vào cột đầu tiên (cột 0)
		for (int i = 0; i < disks.Length; i++)
		{
			towers[0].Push(disks[i].transform);
			disks[i].transform.SetParent(pegs[0].transform);
			Debug.Log(i * 100);
			RectTransform diskRect = disks[i].GetComponent<RectTransform>();
			diskRect.anchoredPosition = Vector2.zero;
			disks[i].transform.localPosition += new Vector3(0, (i*100), 0);
		}
	}

	private void Update()
	{
		SelectDisk();
		ChooseDisk();
		if(isMoving)
		{
			pulledPosition.GetComponent<Image>().color = Color.black;
		}
		else if(!isMoving)
		{

			pulledPosition.GetComponent<Image>().color = Color.white;
		}
	}

	public void SelectDisk()
	{
		if (Input.GetKeyDown(KeyCode.A))
		{
			if(currentPegIndex == 0)
			{
				//jump to last peg
				pulledPosition.position = new Vector3(pegs[pegs.Length - 1].transform.position.x, pulledPosition.position.y, 0);
				currentPegIndex = pegs.Length - 1;
			}
			else
			{
				pulledPosition.position = new Vector3(pegs[currentPegIndex - 1].transform.position.x, pulledPosition.position.y, 0);
				currentPegIndex--;
			}
		}

		if (Input.GetKeyDown(KeyCode.D))
		{
			if (currentPegIndex == pegs.Length - 1)
			{
				//jump to last peg
				pulledPosition.position = new Vector3(pegs[0].transform.position.x, pulledPosition.position.y, 0);
				currentPegIndex = 0;
			}
			else
			{
				pulledPosition.position = new Vector3(pegs[currentPegIndex + 1].transform.position.x, pulledPosition.position.y, 0);
				currentPegIndex++;
			}
		}
	}

	public void ChooseDisk()
	{
		if(Input.GetKeyDown(KeyCode.Return))
		{
			if(!isMoving)
			{
				fromPegIndex = currentPegIndex;
				isMoving = true;
			}
			else if (isMoving)
			{
				MoveDisk(fromPegIndex, currentPegIndex);
				isMoving = false;
			}
		}
	}

	public void MoveDisk(int fromPeg, int toPeg)
	{
		//if (isMoving) return; // Nếu đang di chuyển đĩa thì không làm gì thêm
		if (towers[fromPeg].Count > 0)
		{
			movingDisk = towers[fromPeg].Pop();

			float movingDiskWidth = movingDisk.GetComponent<RectTransform>().rect.width;

			float peekDiskWidth = float.MaxValue; //Gan gia tri cho peek width de tranh truong hop peek null

			if (towers[toPeg].Count > 0)
			{
				peekDiskWidth = towers[toPeg].Peek().GetComponent<RectTransform>().rect.width;
			}

			
			if (towers[toPeg].Count == 0 || movingDiskWidth < peekDiskWidth)
			{
				towers[toPeg].Push(movingDisk);
				// Cập nhật vị trí đĩa 
				movingDisk.transform.SetParent(pegs[toPeg].transform);
				movingDisk.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

				movingDisk.transform.localPosition += new Vector3(0, (towers[toPeg].Count - 1) * 100, 0);

				//StartCoroutine(MoveDiskCoroutine(movingDisk, targetPosition));
			}
			else
			{
				towers[fromPeg].Push(movingDisk); // Trả lại đĩa nếu không di chuyển được
				Debug.Log("Khong the dat dia lon len dia nho");
			}
		}
	}

	private IEnumerator<WaitForSeconds> MoveDiskCoroutine(Transform disk, Vector3 targetPosition)
	{
		float duration = 0.5f; // Thời gian di chuyển
		Vector3 startPosition = disk.position;
		float elapsedTime = 0;

		while (elapsedTime < duration)
		{
			disk.position = Vector3.Lerp(startPosition, targetPosition, (elapsedTime / duration));
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		disk.position = targetPosition; // Đặt vị trí cuối cùng
		isMoving = false;
	}
}