using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using VRStandardAssets.Utils;
using UnityEngine.UI;


namespace VRStandardAssets.Menu{
	public class JumpScript : MonoBehaviour 
	{

		[SerializeField]private SelectionRadial m_SelectionRadial;
		[SerializeField]private PanelManager m_PanelManager;
		[SerializeField]private GameObject m_FromPanel;
		[SerializeField]private GameObject m_ToPanel;
		 

		private void OnEnable()
		{
			if(m_SelectionRadial != null)
				m_SelectionRadial.OnSelectionComplete += HandleRadialComplete;
		}

		private void OnDisable()
		{
			if(m_SelectionRadial != null)
				m_SelectionRadial.OnSelectionComplete -= HandleRadialComplete;
		}
			

		private void HandleRadialComplete()
		{	
			if (m_PanelManager != null && m_FromPanel != null & m_ToPanel != null) {
				m_PanelManager.OpenPanel (m_FromPanel , m_ToPanel);
			}
		}

		void Start () {

		}
			
		void Update () {

		}
	}	
}