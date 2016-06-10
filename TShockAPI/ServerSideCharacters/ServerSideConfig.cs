using System;
using System.Collections.Generic;
using System.ComponentModel;
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

using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace TShockAPI.ServerSideCharacters
{
	public class ServerSideConfig
	{
		[Description("是否开启服务器端存档(强制开荒).")]
		public bool Enabled = false;

		[Description("玩家存档保存间隔, 单位是分钟.")]
		public int ServerSideCharacterSave = 5;

		[Description("开启强制开荒后, 玩家刚登陆无法丢物品的时间长度, 单位毫秒.")]
		public int LogonDiscardThreshold = 1000;

		[Description("初始血量.")] 
		public int StartingHealth = 100;

		[Description("初始魔法.")] 
		public int StartingMana = 20;

		[Description("初始物品.")] 
		public List<NetItem> StartingInventory = new List<NetItem>();

		public static ServerSideConfig Read(string path)
		{
			using (var reader = new StreamReader(path))
			{
				string txt = reader.ReadToEnd();
				var config = JsonConvert.DeserializeObject<ServerSideConfig>(txt);
				return config;
			}
		}

		public void Write(string path)
		{
			using (var writer = new StreamWriter(path))
			{
				writer.Write(JsonConvert.SerializeObject(this, Formatting.Indented));
			}
		}

		/// <summary>
		/// Dumps all configuration options to a text file in Markdown format
		/// </summary>
		public static void DumpDescriptions()
		{
			var sb = new StringBuilder();
			var defaults = new ServerSideConfig();

			foreach (var field in defaults.GetType().GetFields().OrderBy(f => f.Name))
			{
				if (field.IsStatic)
					continue;

				var name = field.Name;
				var type = field.FieldType.Name;

				var descattr =
					field.GetCustomAttributes(false).FirstOrDefault(o => o is DescriptionAttribute) as DescriptionAttribute;
				var desc = descattr != null && !string.IsNullOrWhiteSpace(descattr.Description) ? descattr.Description : "None";

				var def = field.GetValue(defaults);

				sb.AppendLine("{0}  ".SFormat(name));
				sb.AppendLine("类型: {0}  ".SFormat(type));
				sb.AppendLine("描述: {0}  ".SFormat(desc));
				sb.AppendLine("默认: \"{0}\"  ".SFormat(def));
				sb.AppendLine();
			}

			File.WriteAllText("SSCConfig介绍.txt", sb.ToString());
		}
	}
}
