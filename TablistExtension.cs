using System;
using System.Collections;
using System.Collections.Generic;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Display;
using SampSharp.GameMode.Events;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.Extension
{
    public class TablistDialogRows
	{
		protected Action<TablistDialogRows> ItemSelect;

		public object Tag { get; set; }
		
		public void OnItemSelect()
        {
			ItemSelect?.Invoke(this);
        }
		public void SetItemSelect(Action<TablistDialogRows> action)
        {
			ItemSelect = action;
        }
	}

	public class TablistDialogExtension
	{
		private readonly TablistDialog _listDialog;
		private readonly IList<TablistDialogRows> _listDialogRows;
		public TablistDialogExtra(string caption, IEnumerable<string> columns, string button1, string button2 = null)
		{
			_listDialog = new TablistDialog(caption, columns, button1, button2);
			_listDialog.Response += DialogResponse;

			_listDialogRows = new List<TablistDialogRows>();
		}

		public TablistDialogExtra(string caption, int columnCount)
		{
			_listDialog = new TablistDialog(caption, columnCount, "Ok");
			_listDialog.Response += DialogResponse;

			_listDialogRows = new List<TablistDialogRows>();
		}

		protected Action<Dialog> ClickOk;
		protected Action<Dialog> ClickCancel;

		public void Show(BasePlayer player)
        {
			_listDialog.Show(player);
        }

		public void ButtonLeft(string text)
        {
			_listDialog.Button1 = text;
        }

		public void ButtonLeft(string text, Action<Dialog> handler)
		{
			_listDialog.Button1 = text;
			ClickOk = handler;
		}

		public void ButtonRight(string text)
		{
			_listDialog.Button2 = text;
		}

		public void ButtonRight(string text, Action<Dialog> handler)
		{
			_listDialog.Button2 = text;
			ClickCancel = handler;
		}

		public void Add(string[] text, Action<TablistDialogRows> handler)
		{
			_listDialog.Add(text);
			var row = new TablistDialogRows();
			row.SetItemSelect(handler);
			_listDialogRows.Add(row);
		}

		public void Add(string[] text, Action<TablistDialogRows> handler, object tag)
        {
			_listDialog.Add(text);
			var row = new TablistDialogRows() { Tag = tag };
			row.SetItemSelect(handler);
			_listDialogRows.Add(row);
        }

		private void DialogResponse(object sender, DialogResponseEventArgs e)
		{
			if (e.DialogButton == DialogButton.Left)
			{
				_listDialogRows[e.ListItem].OnItemSelect();
				ClickOk?.Invoke(sender as Dialog);
			}
            else
            {
				ClickCancel?.Invoke(sender as Dialog);
			}
		}
	}
}
