using Common;
using HaskellPad.Styling;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HaskellPad
{
	public partial class MainWindow : Form
	{
		public Dictionary<string, MethodInfo> DependencyProviders;

		public MainWindow()
		{
			InitializeComponent();
			DependencyProviders = new Dictionary<string, MethodInfo>();

			//Resolve dependencies
			PopulateProviders();
			ResolveDependencies(this);
		}

		public void PopulateProviders()
		{
			var assemblies = new[] { Assembly.GetExecutingAssembly(), Assembly.Load("Common"), Assembly.Load("Core") };
			foreach (var asm in assemblies)
			{
				foreach (var type in asm.GetTypes())
				{
					foreach (var fn in type.GetMembers(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.InvokeMethod))
					{
						var attribute = fn.GetCustomAttribute<DependencyProvider>();
						if (attribute == null) continue;
						DependencyProviders[(string)attribute.matchName] = fn as MethodInfo;
					}
				}
			}
		}

		public void ResolveDependencies(Control parent)
		{
			foreach (var control in parent.Controls)
			{
				var members = control
					.GetType()
					.GetMembers(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField | BindingFlags.GetProperty)
					.Where(member => member.CustomAttributes.Any(att => att.AttributeType == typeof(Dependency)));

				foreach (var prop in members)
				{
					var attribute = prop.GetCustomAttribute<Dependency>();
					if (DependencyProviders.ContainsKey(attribute.matchName))
					{
						var mi = DependencyProviders[attribute.matchName];
						Type type = null;
						if (prop is PropertyInfo) type = ((PropertyInfo)prop).PropertyType;
						if (prop is FieldInfo) type = ((FieldInfo)prop).FieldType;
						try
						{
							if (prop is PropertyInfo) ((PropertyInfo)prop).SetValue(control, Delegate.CreateDelegate(type, mi));
							if (prop is FieldInfo) ((FieldInfo)prop).SetValue(control, Delegate.CreateDelegate(type, mi));
						}
						catch (ArgumentException ex)
						{
							throw new Exception("The provided dependency type is not compatible with the required type on type " + control.GetType() + " with dependency named " + attribute.matchName);
						}
					}
					else
					{
						throw new Exception("Unmatched dependency on type " + control.GetType() + " named " + attribute.matchName);
					}
				}
				if (control is Control) ResolveDependencies((Control)control);
			}
		}
	}
}
