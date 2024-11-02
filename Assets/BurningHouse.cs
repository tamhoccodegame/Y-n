using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurningHouse : MonoBehaviour
{
    public List<GameObject> fires;
    
    public void ExtinguishFire()
    {
        foreach(GameObject obj in fires)
        {
            if(obj != null)
            {
                obj.SetActive(false);
			}
		}

		fires.Clear();

		// Kiểm tra nếu tất cả các BurningHouse khác không còn lửa
		BurningHouse[] burningHouses = FindObjectsOfType<BurningHouse>();
		foreach (var b in burningHouses)
		{
			if (b.fires.Count > 0) return;
		}

		GameManager.instance.TriggerEndQuest("1_2");

	}
}
