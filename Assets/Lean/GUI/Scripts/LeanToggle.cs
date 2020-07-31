using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using Lean.Common;
using Lean.Transition;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Lean.Gui
{
	[ExecuteInEditMode]
	[HelpURL(LeanGui.HelpUrlPrefix + "LeanToggle")]
	[AddComponentMenu(LeanGui.ComponentMenuPrefix + "Toggle")]
	public class LeanToggle : MonoBehaviour
	{
		public static LinkedList<LeanToggle> Instances = new LinkedList<LeanToggle>();
		public bool On { set { Set(value); } get { return on; } } [SerializeField] private bool on;
		public bool TurnOffSiblings { set { if (turnOffSiblings = value) TurnOffSiblingsNow(); } get { return turnOffSiblings; } } [SerializeField] private bool turnOffSiblings;
		public LeanPlayer OnTransitions { get { if (onTransitions == null) onTransitions = new LeanPlayer(); return onTransitions; } } [SerializeField] private LeanPlayer onTransitions;
		public LeanPlayer OffTransitions { get { if (offTransitions == null) offTransitions = new LeanPlayer(); return offTransitions; } } [SerializeField] private LeanPlayer offTransitions;
		public UnityEvent OnOn { get { if (onOn == null) onOn = new UnityEvent(); return onOn; } } [SerializeField] private UnityEvent onOn;
		public UnityEvent OnOff { get { if (onOff == null) onOff = new UnityEvent(); return onOff; } } [SerializeField] private UnityEvent onOff;

		[System.NonSerialized]
		private LinkedListNode<LeanToggle> link;
		public void Set(bool value)
		{
			if (value == true)
			{
				TurnOn();
			}
			else
			{
				TurnOff();
			}
		}

		/// <summary>This allows you to toggle the state of this toggle (i.e. if it's turned on, then this will turn it off).</summary>
		[ContextMenu("Toggle")]
		public void Toggle()
		{
			On = !On;
		}

		/// <summary>If this toggle is turned off, then this will turn it on.</summary>
		[ContextMenu("Turn On")]
		public void TurnOn()
		{
			if (on == false)
			{
				on = true;

				TurnOnNow();
			}
		}

		[ContextMenu("Turn Off")]
		public void TurnOff()
		{
			if (on == true)
			{
				on = false;

				TurnOffNow();
			}
		}
		
		[ContextMenu("Turn Off Siblings Now")]
		public void TurnOffSiblingsNow()
		{
			var parent = transform.parent;

			if (parent != null)
			{
				var ignore = transform.GetSiblingIndex();

				for (var i = parent.childCount - 1; i >= 0; i--)
				{
					if (i != ignore)
					{
						var sibling = parent.GetChild(i).GetComponent<LeanToggle>();

						if (sibling != null)
						{
							sibling.TurnOff();
						}
					}
				}
			}
		}

		/// <summary>This will return true if all the active and enabled toggle instances with the specified GameObject name are turned on.</summary>
		public static bool AllOn(string name)
		{
			var node = Instances.First;
			var on   = false;

			for (var i = Instances.Count - 1; i >= 0; i--)
			{
				var instance = node.Value;

				if (instance.name == name)
				{
					if (instance.on == false)
					{
						return false;
					}

					on = true;
				}

				node = node.Next;
			}

			return on;
		}

		/// <summary>This will return true if all the active and enabled toggle instances with the specified GameObject name are turned off.</summary>
		public static bool AllOff(string name)
		{
			var node = Instances.First;
			var off   = false;

			for (var i = Instances.Count - 1; i >= 0; i--)
			{
				var instance = node.Value;

				if (instance.name == name)
				{
					if (instance.on == true)
					{
						return false;
					}

					off = true;
				}

				node = node.Next;
			}

			return off;
		}

		/// <summary>This allows you to set the <b>On</b> state of all the active and enabled toggle instances with the specified GameObject name.</summary>
		public static void SetAll(string name, bool on)
		{
			var node = Instances.First;

			for (var i = Instances.Count - 1; i >= 0; i--)
			{
				var instance = node.Value;

				if (instance.name == name)
				{
					instance.On = on;
				}

				node = node.Next;
			}
		}

		/// <summary>This allows you to toggle the state (i.e. if it's turned on, then this will turn it off) of all active and enabled toggle instances with the specified GameObject name.</summary>
		public static void ToggleAll(string name)
		{
			var node = Instances.First;

			for (var i = Instances.Count - 1; i >= 0; i--)
			{
				var instance = node.Value;

				if (instance.name == name)
				{
					instance.Toggle();
				}

				node = node.Next;
			}
		}

		/// <summary>This allows you to turn on every active and enabled toggle instance with the specified GameObject name.</summary>
		public static void TurnOnAll(string name)
		{
			var node = Instances.First;

			for (var i = Instances.Count - 1; i >= 0; i--)
			{
				var instance = node.Value;

				if (instance.name == name)
				{
					instance.TurnOn();
				}

				node = node.Next;
			}
		}

		/// <summary>This allows you to turn off each active and enabled LeanToggle instance with the specified GameObject name.</summary>
		public static void TurnOffAll(string name)
		{
			var node = Instances.First;

			for (var i = Instances.Count - 1; i >= 0; i--)
			{
				var instance = node.Value;

				if (instance.name == name)
				{
					instance.TurnOff();
				}

				node = node.Next;
			}
		}

		protected virtual void TurnOnNow()
		{
			if (turnOffSiblings == true)
			{
				TurnOffSiblingsNow();
			}

			if (onTransitions != null)
			{
				onTransitions.Begin();
			}

			if (onOn != null)
			{
				onOn.Invoke();
			}
		}

		protected virtual void TurnOffNow()
		{
			if (offTransitions != null)
			{
				offTransitions.Begin();
			}

			if (onOff != null)
			{
				onOff.Invoke();
			}
		}

		protected virtual void OnEnable()
		{
			link = Instances.AddLast(this);
		}

		protected virtual void OnDisable()
		{
			Instances.Remove(link);

			link = null;
		}
	}
}

#if UNITY_EDITOR
namespace Lean.Gui
{
	[CanEditMultipleObjects]
	[CustomEditor(typeof(LeanToggle), true)]
	public class LeanToggle_Editor : LeanInspector<LeanToggle>
	{
		protected override void DrawInspector()
		{
			if (Draw("on", "This lets you change the current toggle state of this UI element.") == true)
			{
				Each(t => t.On = serializedObject.FindProperty("on").boolValue, true);
			}

			if (Draw("turnOffSiblings", "If you enable this, then any sibling GameObjects (i.e. same parent GameObject) will automatically be turned off. This allows you to make radio boxes, or force only one panel to show at a time, etc.") == true)
			{
				Each(t => t.TurnOffSiblings = serializedObject.FindProperty("turnOffSiblings").boolValue, true);
			}

			EditorGUILayout.Separator();

			Draw("onTransitions", "This allows you to perform a transition when this toggle turns on. You can create a new transition GameObject by right clicking the transition name, and selecting Create. For example, the LeanGraphicColor (Graphic.color Transition) component can be used to change the color.\n\nNOTE: Any transitions you perform here should be reverted in the <b>Off Transitions</b> setting using a matching transition component.");
			Draw("offTransitions", "This allows you to perform a transition when this toggle turns off. You can create a new transition GameObject by right clicking the transition name, and selecting Create. For example, the LeanGraphicColor (Graphic.color Transition) component can be used to change the color.");

			EditorGUILayout.Separator();

			Draw("onOn");
			Draw("onOff");
		}
	}
}
#endif