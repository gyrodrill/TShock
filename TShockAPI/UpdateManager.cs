/*
TShock, a server mod for Terraria
Copyright (C) 2011-2015 Nyx Studios (fka. The TShock Team)

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using Newtonsoft.Json;

namespace TShockAPI
{
	public class UpdateManager
	{
		private string updateUrl = "http://tshockcn.com/latest/";

		/// <summary>
		/// Check once every X minutes.
		/// </summary>
		private int CheckXMinutes = 30;

		public UpdateManager()
		{
			Thread t = new Thread(() => {
				do {
					CheckForUpdates(null);	
				} while (true);
			});
			
			t.Name = "TShock Update Thread";
			t.IsBackground = true;
			
			t.Start();
		}

		private void CheckForUpdates(object state)
		{
			try
			{
				UpdateCheck(state);
				CheckXMinutes = 60;
			}
			catch (Exception)
			{
				// Skip this run and check more frequently...
				CheckXMinutes = 15;
			}
			
			Thread.Sleep(CheckXMinutes * 60 * 1000);
		}

		public void UpdateCheck(object o)
		{
			var updates = ServerIsOutOfDate();
			if (updates != null)
			{
				NotifyAdministrators(updates);
			}
		}

		/// <summary>
		/// Checks to see if the server is out of date.
		/// </summary>
		/// <returns></returns>
		private Dictionary<string, string> ServerIsOutOfDate()
		{
			var client = (HttpWebRequest)WebRequest.Create(updateUrl);
			client.Timeout = 5000;
			try
			{
				using (var resp = TShock.Utils.GetResponseNoException(client))
				{
					if (resp.StatusCode != HttpStatusCode.OK)
					{
						throw new IOException("服务器未能响应.");
					}

					using(var reader = new StreamReader(resp.GetResponseStream()))
					{
						string updatejson = reader.ReadToEnd();
						var update = JsonConvert.DeserializeObject<Dictionary<string, string>>(updatejson);
						var version = new Version(update["CN"]);
						var currentversion = new Version(TShock.CNVersion);
						if (currentversion.CompareTo(version) < 0)
							return update;
					}
				}
			}
			catch (Exception e)
			{
				TShock.Log.ConsoleError("无法检查更新.{0}", e);
				throw e;
			}

			return null;
		}

		private void NotifyAdministrators(Dictionary<string, string> update)
		{
			var changes = update["text"].Split(new[] {'\n'}, StringSplitOptions.RemoveEmptyEntries);
			var latesta = "TShockCN Beta " + update["CN"] + " (TShock " + update["TSversion"] + ") 对应游戏" + update["GameVer"] + " 插件API" + update["API"];
			NotifyAdministrator(TSPlayer.Server, changes, latesta);
			foreach (TSPlayer player in TShock.Players)
			{
				if (player != null && player.Active && player.HasPermission(Permissions.maintenance))
				{
					NotifyAdministrator(player, changes, latesta);
				}
			}
		}

		private void NotifyAdministrator(TSPlayer player, string[] changes, string latestaaa)
		{
			player.SendErrorMessage("服务器发现更新. 最新版本:");
			player.SendErrorMessage(latestaaa);
			for (int j = 0; j < changes.Length; j++)
			{
				player.SendErrorMessage(changes[j]);
			}
		}
	}
}