using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using VRStandardAssets.Utils;

public class PaginationPanel : MonoBehaviour
{
	/// <summary>
	/// 当前页面索引
	/// </summary>
	private int m_PageIndex = 1;

	/// <summary>
	/// 总页数
	/// </summary>
	private int m_PageCount = 0;

	/// <summary>
	/// 元素总个数
	/// </summary>
	private int m_ItemsCount = 0;

	/// <summary>
	/// 元素列表
	/// </summary>
	private List<GridItem> m_ItemsList;

	/// <summary>
	/// 上一页
	/// </summary>
	public Button m_BtnPrevious;

	/// <summary>
	/// 下一页
	/// </summary>
	public Button m_BtnNext;

	/// <summary>
	/// 绑定GridLayout
	/// </summary>
	public GridLayoutGroup m_GridLayout;

	/// <summary>
	/// 显示当前页数的标签
	/// </summary>
	public Text m_PanelText;

	/// <summary>
	/// 每一页最多有多少个元素
	/// </summary>
	public int m_PerPageCount;


	void Start ()
	{
		InitGUI ();
		InitItems ();
	}

	/// <summary>
	/// 初始化GUI
	/// </summary>
	private void InitGUI ()
	{
		SelectionRadial m_SelectionRadialNext = m_BtnNext.GetComponent<SelectionRadial> ();
		m_SelectionRadialNext.OnSelectionComplete += HandleRadialCompleteNext;

		SelectionRadial m_SelectionRadialPrevious = m_BtnPrevious.GetComponent<SelectionRadial>();
		m_SelectionRadialPrevious.OnSelectionComplete += HandleRadialCompletePrevious;
	}

	/// <summary>
	/// 初始化元素
	/// </summary>
	private void InitItems ()
	{
		GridItem[] items = new GridItem[] {
			new GridItem ("1", "蜡笔1"),
			new GridItem ("2", "蜡笔2"),
			new GridItem ("3", "蜡笔3"),
			new GridItem ("4", "蜡笔4"),
			new GridItem ("5", "蜡笔5"),
			new GridItem ("6", "蜡笔6"),
			new GridItem ("7", "蜡笔7"),
			new GridItem ("8", "蜡笔8"),
			new GridItem ("9", "蜡笔9"),
			new GridItem ("10", "蜡笔10"),
			new GridItem ("11", "蜡笔11"),
			new GridItem ("12", "蜡笔12")
		};
			
		m_ItemsList = new List<GridItem> ();
		for (int i = 0; i < Random.Range (1, 1000); i++) {
			m_ItemsList.Add (items [Random.Range (0, items.Length)]);
		}

		//计算元素总个数
		m_ItemsCount = m_ItemsList.Count;
		//计算总页数
		m_PageCount = (m_ItemsCount % m_PerPageCount) == 0 ? m_ItemsCount / m_PerPageCount : (m_ItemsCount / m_PerPageCount) + 1;

		//更新界面页数
		m_PanelText.text = string.Format ("{0}/{1}", m_PageIndex.ToString (), m_PageCount.ToString ());
		BindPage (m_PageIndex);
	}

	/// <summary>
	/// 下一页
	/// </summary>
	private void HandleRadialCompleteNext ()
	{
		if (m_PageCount <= 0)
			return;
		//最后一页禁止向后翻页
		if (m_PageIndex >= m_PageCount)
			return;

		m_PageIndex += 1;
		if (m_PageIndex >= m_PageCount)
			m_PageIndex = m_PageCount;

		BindPage (m_PageIndex);

		//更新界面页数
		m_PanelText.text = string.Format ("{0}/{1}", m_PageIndex.ToString (), m_PageCount.ToString ());
	}

	/// <summary>LoadSprite
	/// 上一页
	/// </summary>
	private void HandleRadialCompletePrevious ()
	{
		if (m_PageCount <= 0)
			return;
		//第一页时禁止向前翻页
		if (m_PageIndex <= 1)
			return;
		m_PageIndex -= 1;
		if (m_PageIndex < 1)
			m_PageIndex = 1;

		BindPage (m_PageIndex);

		//更新界面页数
		m_PanelText.text = string.Format ("{0}/{1}", m_PageIndex.ToString (), m_PageCount.ToString ());
	}

	/// <summary>
	/// 绑定指定索引处的页面元素
	/// </summary>
	/// <param name="index">页面索引</param>
	private void BindPage (int index)
	{
		//列表处理
		if (m_ItemsList == null || m_ItemsCount <= 0)
			return;

		//索引处理
		if (index < 0 || index > m_ItemsCount)
			return;

		//按照元素个数可以分为1页和1页以上两种情况
		if (m_PageCount == 1) {
			int canDisplay = 0;
			for (int i = m_PerPageCount; i > 0; i--) {
				if (canDisplay < m_PerPageCount) {
					BindGridItem (m_GridLayout.transform.GetChild (canDisplay), m_ItemsList [m_PerPageCount - i]);
					m_GridLayout.transform.GetChild (canDisplay).gameObject.SetActive (true);
				} else {
					//对超过canDispaly的物体实施隐藏
					m_GridLayout.transform.GetChild (canDisplay).gameObject.SetActive (false);
				}
				canDisplay += 1;
			}
		} else if (m_PageCount > 1) {
			//1页以上需要特别处理的是最后1页
			//和1页时的情况类似判断最后一页剩下的元素数目
			//第1页时显然剩下的为12所以不用处理
			if (index == m_PageCount) {
				int canDisplay = 0;
				for (int i = m_PerPageCount; i > 0; i--) {
					//最后一页剩下的元素数目为 m_ItemsCount - 12 * (index-1)
					if (canDisplay < m_ItemsCount - m_PerPageCount * (index - 1)) {
						BindGridItem (m_GridLayout.transform.GetChild (canDisplay), m_ItemsList [m_PerPageCount * index - i]);
						m_GridLayout.transform.GetChild (canDisplay).gameObject.SetActive (true);
					} else {
						//对超过canDispaly的物体实施隐藏
						m_GridLayout.transform.GetChild (canDisplay).gameObject.SetActive (false);
					}
					canDisplay += 1;
				}
			} else {
				for (int i = m_PerPageCount; i > 0; i--) {
					BindGridItem (m_GridLayout.transform.GetChild (m_PerPageCount - i), m_ItemsList [m_PerPageCount * index - i]);
					m_GridLayout.transform.GetChild (m_PerPageCount - i).gameObject.SetActive (true);
				}
			}
		}
	}

	/// <summary>
	/// 加载一个Sprite
	/// </summary>
	/// <param name="assetName">资源名称</param>
	private Sprite LoadSprite (string assetName)
	{
		Texture texture = (Texture)Resources.Load (assetName);
		Sprite sprite = Sprite.Create ((Texture2D)texture, new Rect (0, 0, texture.width, texture.height), new Vector2 (0.5f, 0.5f));
		return sprite;
	}

	/// <summary>
	/// 将一个GridItem实例绑定到指定的Transform上
	/// </summary>
	/// <param name="trans"></param>
	/// <param name="gridItem"></param>
	private void BindGridItem (Transform trans, GridItem gridItem)
	{
		trans.GetComponent<Image> ().sprite = LoadSprite (gridItem.ItemSprite);
		trans.Find ("name").GetComponent<Text> ().text = gridItem.ItemName;
	}

	/// <summary>
	/// Shows the main. Android端可以调用UnityPlayer.UnitySendMessage("GridPaging", "ShowMain", json);来进行相互通信
	/// </summary>
	/// <param name="json">Json.</param>
	public void ShowMain(string json)
	{
		OriginalBean bean = JsonUtility.FromJson<OriginalBean> (json);
		transform.Find ("JsonText").GetComponent<Text> ().text = bean.Name;
	}
}