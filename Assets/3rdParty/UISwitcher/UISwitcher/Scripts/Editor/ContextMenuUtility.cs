using UnityEditor;
namespace UISwitcher {

#if UNITY_EDITOR
	public static class ContextMenuUtility {
		[MenuItem("GameObject/UIComponents/Switcher")]
		public static void CreateSwitcher(MenuCommand menuCommand) {
			CreateUtility.CreateUIElement("UISwitcher");
		}

		[MenuItem("GameObject/UIComponents/Switcher Outlined")]
		public static void CreateSwitcherOutlined(MenuCommand menuCommand) {
			CreateUtility.CreateUIElement("UISwitcherOutlined");
		}

		[MenuItem("GameObject/UIComponents/Switcher Thin")]
		public static void CreateSwitcherThin(MenuCommand menuCommand) {
			CreateUtility.CreateUIElement("UISwitcherThin");
		}
	}
#endif
}