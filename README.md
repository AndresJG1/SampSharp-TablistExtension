# SampSharp-TablistExtension

Example:

```
public void Test(BasePlayer player)
{
  var dialog = new TablistDialogExtension("Dialog Caption", 2);
  dialog.Add(new string[] { "Create vehicle", "free" }, (args) => { BaseVehicle.Create(...); });
  dialog.Add(new string[] { "Create vehicle", "free" }, (args) => { player.SendClientMessage("Hi!"); });
  dialog.ButtonLeft("Select", (args) => { player.SendClientMessage("Select"); });
  dialog.ButtonRight("Cancel", (args) => { player.SendClientMessage("Cancel"); });
  dialog.Show(player);
}
```
