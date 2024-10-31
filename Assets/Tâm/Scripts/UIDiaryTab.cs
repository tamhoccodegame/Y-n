using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIDiaryTab : MonoBehaviour
{
	public Transform listContainer; // Container chứa các nút của nhật ký
	public Button buttonTemplate;
	public GameObject detailContainer; // Container cho chi tiết nhật ký, như hình ảnh và nội dung
	public Image diaryImage;
	public TextMeshProUGUI diaryName;
	public TextMeshProUGUI diaryLines;

	public List<Button> buttonList;
	private int currentButtonIndex;
	private int visibleEntries = 5;

	private List<Diary> unlockedDiaryList;

	private void Start()
	{
		if (buttonList.Count > 0)
		{
			EventSystem.current.SetSelectedGameObject(buttonList[currentButtonIndex].gameObject);
		}
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.S))
		{
			MoveSelection(1);
		}
		else if (Input.GetKeyDown(KeyCode.W))
		{
			MoveSelection(-1);
		}
	}

	public void UpdateVisual()
	{
		// Cập nhật danh sách nhật ký đã mở khóa
		this.unlockedDiaryList = GameManager.instance.GetUnlockedDiary();

		// Xóa các nút cũ
		foreach (Transform child in listContainer)
		{
			if (child.gameObject == buttonTemplate.gameObject) continue;
			Destroy(child.gameObject);
		}

		buttonList.Clear();

		// Tạo nút mới cho mỗi mục nhật ký đã mở khóa
		foreach (Diary d in unlockedDiaryList)
		{
			Button newButton = Instantiate(buttonTemplate, listContainer);
			newButton.gameObject.SetActive(true);
			buttonList.Add(newButton);
			detailContainer.SetActive(true); // Hiển thị phần chi tiết
			diaryImage.sprite = d.diaryPicture;
			diaryName.text = d.diaryName;
			diaryLines.text = d.diaryLines;
		}

		// Đặt nút đầu tiên được chọn nếu có ít nhất một nhật ký
		if (buttonList.Count > 0)
		{
			currentButtonIndex = 0;
			EventSystem.current.SetSelectedGameObject(buttonList[currentButtonIndex].gameObject);
		}

		// Cập nhật hiển thị danh sách nhật ký
		UpdateDiaryListDisplay();
	}


	private void MoveSelection(int direction)
	{
		int newIndex = currentButtonIndex + direction;

		// Kiểm tra xem có cần cuộn danh sách không
		if (newIndex >= 0 && newIndex < unlockedDiaryList.Count)
		{
			currentButtonIndex = newIndex;
			// Nếu đang ở giới hạn trên hoặc dưới, cuộn danh sách
			if (currentButtonIndex >= visibleEntries || newIndex < currentButtonIndex - visibleEntries + 1)
			{
				UpdateDiaryListDisplay();
			}
		}
	}


	private void UpdateDiaryListDisplay()
	{
		// Xóa các nút cũ
		foreach (Transform child in listContainer)
		{
			if (child.gameObject == buttonTemplate.gameObject) continue;
			Destroy(child.gameObject);
		}
		buttonList.Clear();

		// Tính chỉ số kết thúc
		int end = Mathf.Min(currentButtonIndex + visibleEntries, unlockedDiaryList.Count);

		// Tạo nút mới cho mỗi trang phục trong khoảng hiển thị
		for (int i = currentButtonIndex; i < end; i++)
		{
			Diary diary = unlockedDiaryList[i];
			Button newButton = Instantiate(buttonTemplate, listContainer);
			newButton.gameObject.SetActive(true);
			buttonList.Add(newButton);

			// Đăng ký sự kiện cho nút để hiển thị chi tiết khi nhấn
			int index = i; // Lưu chỉ số để tránh vấn đề closure
		}

		// Nếu có nút, chọn nút đầu tiên và hiển thị chi tiết
		if (buttonList.Count > 0)
		{
			EventSystem.current.SetSelectedGameObject(buttonList[0].gameObject);
		}
	}


}
