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

	public int tsVitri = 4, tsNguyenNhan = 3, tsBieuHien = 3, tsVanDong = 2, tsCamGiac = 1;

	int viTri, nguyenNhan, bieuHien, vanDong, camGiac;

	public Dropdown dropdown;
	public Text optionTitle, resultTxt;
	public GameObject resultPanel;

	int index = 0;

    private void Start()
    {
		viTri = nguyenNhan = bieuHien = vanDong = camGiac = -1;
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
		resultPanel.SetActive(true);
		resultTxt.text = GetResult();
    }

	string GetResult()
    {
		int tongTs = tsVitri + tsNguyenNhan + tsVanDong + tsCamGiac  + tsBieuHien;
		string tenBenh = "";
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
				tenBenh = chanThuongList[i].name;
			}
		}
		return tenBenh;
    }

	public void Restart()
    {
		index = 0;
		viTri = nguyenNhan = bieuHien = vanDong = camGiac = -1;
		resultPanel.SetActive(false);
		Show();
	}
}

[System.Serializable]
public class ChanThuong
{
	public string name;
	public int viTri, nguyenNhan, bieuHien, vanDong, camGiac;
}

