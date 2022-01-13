using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
	public List<ChanThuong> chanThuongList;
	public List<string> viTriList;
	public List<string> nguyenNhanList;
	public List<string> bieuHienList;
	public List<string> vanDongList;
	public List<string> camGiacList;

	public int tsVitri = 10, tsNguyenNhan = 3, tsBieuHien = 3, tsVanDong = 2, tsCamGiac = 1;

	int viTri, nguyenNhan, bieuHien, vanDong, camGiac;

	public Dropdown dropdown;
	public Text optionTitle, resultTxt;
	public GameObject resultPanel;

	int index = 0;

    private void Start()
    {
		SaveSystem.Init();
		viTri = nguyenNhan = bieuHien = vanDong = camGiac = -1;
		Load();
		Show();
	}


    public void Show()
    {
		dropdown.options.Clear();
		switch (index)
        {
			case 0:
				ShowViTriChanThuong();
				break;
			case 1:
				viTri = dropdown.value;
				ShowNguyenNhan();
				break;
			case 2:
				nguyenNhan = dropdown.value;
				ShowVanDong();
				break;
			case 3:
				vanDong = dropdown.value;
				ShowCamGiac();
				break;
			case 4:
				camGiac = dropdown.value;
				ShowBieuHien();
				break;
			case 5:
				bieuHien = dropdown.value;
				ShowResult();
				break;
        }
		dropdown.RefreshShownValue();
		dropdown.value = 0;
		index++;
    }

	public void Skip()
	{
		dropdown.options.Clear();
		switch (index)
		{
			case 0:
				ShowViTriChanThuong();
				break;
			case 1:
				ShowNguyenNhan();
				break;
			case 2:
				ShowVanDong();
				break;
			case 3:
				ShowCamGiac();
				break;
			case 4:
				ShowBieuHien();
				break;
			case 5:
				ShowResult();
				break;
		}
		dropdown.RefreshShownValue();
		dropdown.value = 0;
		index++;
	}

	void ShowViTriChanThuong()
    {
		optionTitle.text = "Hãy lựa chọn\nVị trí chấn thương";
		foreach (var item in viTriList)
		{
			dropdown.options.Add(new Dropdown.OptionData() { text = item });
		}
	}

	void ShowNguyenNhan()
    {
		optionTitle.text = "Hãy lựa chọn\nNguyên nhân";
		foreach (var item in nguyenNhanList)
		{
			dropdown.options.Add(new Dropdown.OptionData() { text = item });
		}
	}
	void ShowBieuHien()
    {
		optionTitle.text = "Hãy lựa chọn\nBiểu hiện";
		foreach (var item in bieuHienList)
		{
			dropdown.options.Add(new Dropdown.OptionData() { text = item });
		}
	}
	void ShowVanDong()
    {
		optionTitle.text = "Hãy lựa chọn\nKhả năng vận động";
		foreach (var item in vanDongList)
		{
			dropdown.options.Add(new Dropdown.OptionData() { text = item });
		}
	}
	void ShowCamGiac()
    {
		optionTitle.text = "Hãy lựa chọn\nCảm giác";
		foreach (var item in camGiacList)
		{
			dropdown.options.Add(new Dropdown.OptionData() { text = item });
		}
	}

	void ShowResult()
    {
		resultTxt.text = "";
		resultPanel.SetActive(true);
		List<ChanThuong> result = GetResult();
        for (int i = 0; i < result.Count; i++)
        {
			resultTxt.text += "<size=40><b>"+result[i].name+"</b></size>" + "\n" + "Lời khuyên: " + result[i].moTa + "\n";
		}
    }

	List<ChanThuong> GetResult()
    {
		int tongTs = tsVitri + tsNguyenNhan + tsVanDong + tsCamGiac  + tsBieuHien;
		List<ChanThuong> chanThuong = new List<ChanThuong>();
		float max = 0;
        for (int i = 0; i < chanThuongList.Count; i++)
        {
			float tdViTri = chanThuongList[i].viTri == viTri ? 1 : 0;
			float tdNguyenNhan = chanThuongList[i].nguyenNhan == nguyenNhan ? 1 : (nguyenNhan == -1 ? 0.5f : 0);
			float tdVanDong = chanThuongList[i].vanDong == vanDong ? 1 : (vanDong == -1 ? 0.5f : 0);
			float tdCamGiac = chanThuongList[i].camGiac == camGiac ? 1 : (camGiac == -1 ? 0.5f : 0);
			float tdBieuHien = chanThuongList[i].bieuHien == bieuHien ? 1 : (bieuHien == -1 ? 0.5f : 0);

			float s = (tdViTri * tsVitri + tdNguyenNhan * tsNguyenNhan + tdVanDong * tsVanDong
				+ tdCamGiac * tsCamGiac + tdBieuHien * tsBieuHien) / tongTs;
            if (max < s)
            {
				max = s;
				chanThuong.Clear();
				chanThuong.Add(chanThuongList[i]);
			}
            else if (max == s)
            {
				chanThuong.Add(chanThuongList[i]);
			}
			Debug.Log(chanThuongList[i].name + ": " + s);
		}
		return chanThuong;
    }

	public void Restart()
    {
		index = 0;
		viTri = nguyenNhan = bieuHien = vanDong = camGiac = -1;
		resultPanel.SetActive(false);
		Show();
	}

	void Save()
    {
		ChanThuongData data = new ChanThuongData
		{
			chanThuongList = chanThuongList
        };
		string json = JsonUtility.ToJson(data, true);
		SaveSystem.Save(json);
    }
	void Load()
    {
		string saveString = SaveSystem.Load();
        if (saveString != null)
        {
			ChanThuongData data = JsonUtility.FromJson<ChanThuongData>(saveString);
			chanThuongList = data.chanThuongList;
        }
    }
}

[System.Serializable]
public class ChanThuong
{
	public string name;
	public int viTri, nguyenNhan, bieuHien, vanDong, camGiac;
	public string moTa;
}

public class ChanThuongData
{
	public List<ChanThuong> chanThuongList;
}

