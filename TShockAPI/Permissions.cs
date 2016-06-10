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
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

namespace TShockAPI
{
	public static class Permissions
	{
		// tshock.account nodes

		[Description("注册的权限.")]
		public static readonly string canregister = "tshock.account.register";

		[Description("登入的权限.")]
		public static readonly string canlogin = "tshock.account.login";

		[Description("登出(logout, 亦作注销)的权限.")]
		public static readonly string canlogout = "tshock.account.logout";

		[Description("修改自己密码的权限.")]
		public static readonly string canchangepassword = "tshock.account.changepassword";

		// tshock.admin nodes

		[Description("可以修改保护状态的权限.")]
		public static readonly string antibuild = "tshock.admin.antibuild";

		[Description("不能被踢的权限.")]
		public static readonly string immunetokick = "tshock.admin.nokick";

		[Description("不能被封禁的权限.")]
		public static readonly string immunetoban = "tshock.admin.noban";

		[Description("接受特定日志的权限.")]
		public static readonly string logs = "tshock.admin.viewlogs";

		[Description("踢出玩家的权限.")]
		public static readonly string kick = "tshock.admin.kick";

		[Description("封禁玩家的权限.")]
		public static readonly string ban = "tshock.admin.ban";

		[Description("管理跃迁点(传送点)的权限.")]
		public static readonly string managewarp = "tshock.admin.warp";

		[Description("管理封禁物品的权限.")]
		public static readonly string manageitem = "tshock.admin.itemban";

		[Description("管理封禁弹幕的权限.")]
		public static readonly string manageprojectile = "tshock.admin.projectileban";

		[Description("管理封禁方块的权限.")]
		public static readonly string managetile = "tshock.admin.tileban";

		[Description("管理用户组的权限.")]
		public static readonly string managegroup = "tshock.admin.group";

		[Description("管理领地的权限.")]
		public static readonly string manageregion = "tshock.admin.region";

		[Description("可以禁言玩家的权限.")]
		public static readonly string mute = "tshock.admin.mute";

		[Description("查看在线玩家时可以查看到用户ID的权限.")]
		public static readonly string seeids = "tshock.admin.seeplayerids";

		[Description("可以保存服务器玩家存档的权限.")]
		public static readonly string savessc = "tshock.admin.savessi";

		[Description("可以修改其他用户的临时组的权限.")]
		public static readonly string settempgroup = "tshock.admin.tempgroup";

		[Description("可以广播公告的权限.")]
		public static readonly string broadcast = "tshock.admin.broadcast";

		[Description("可以获取其他用户信息的权限.")]
		public static readonly string userinfo = "tshock.admin.userinfo";

		// tshock.buff nodes

		[Description("给自己加状态的权限.")]
		public static readonly string buff = "tshock.buff.self";

		[Description("给其他玩家加状态的权限.")]
		public static readonly string buffplayer = "tshock.buff.others";

		// tshock.cfg nodes

		[Description("可以关闭服务器/收到服务器更新通知的权限.")]
		public static readonly string maintenance = "tshock.cfg.maintenance";

		[Description("修改白名单的权限.")]
		public static readonly string whitelist = "tshock.cfg.whitelist";

		[Description("更改服务器密码的权限.")]
		public static readonly string cfgpassword = "tshock.cfg.password";

		[Description("重载配置文件的权限.")]
		public static readonly string cfgreload = "tshock.cfg.reload";

		[Description("在服务器运行时下载插件的权限(未实装).")]
		public static readonly string updateplugins = "tshock.cfg.updateplugins";

		// tshock.ignore nodes

		[Description("异常方块破坏时不会被冻结的权限.")]
		public static readonly string ignorekilltiledetection = "tshock.ignore.removetile";

		[Description("异常方块放置时不会被冻结的权限.")]
		public static readonly string ignoreplacetiledetection = "tshock.ignore.placetile";

		[Description("异常液体放置时不会被冻结的权限.")]
		public static readonly string ignoreliquidsetdetection = "tshock.ignore.liquid";

		[Description("异常弹幕时不会被冻结的权限.")]
		public static readonly string ignoreprojectiledetection = "tshock.ignore.projectile";

		[Description("异常涂漆时不会被冻结的权限.")]
		public static readonly string ignorepaintdetection = "tshock.ignore.paint";

		[Description("不会被clip检测冻结的权限.")]
		public static readonly string ignorenoclipdetection = "tshock.ignore.noclip";

		[Description("物品数量过高时不会被冻结的权限.")]
		public static readonly string ignorestackhackdetection = "tshock.ignore.itemstack";

		[Description("伤害过高时不会被冻结的权限.")]
		public static readonly string ignoredamagecap = "tshock.ignore.damage";

		[Description("绕过服务器端存档的检测的权限.")]
		public static readonly string bypassssc = "tshock.ignore.ssc";

		[Description("允许客户端无限制修改地图.")]
		public static readonly string allowclientsideworldedit = "tshock.ignore.sendtilesquare";

		[Description("可以丢出被封禁物品的权限.")]
		public static readonly string allowdroppingbanneditems = "tshock.ignore.dropbanneditem";

		[Description("血量过高时不会被冻结的权限.")]
		public static readonly string ignorehp = "tshock.ignore.hp";

		[Description("魔法过高时不会被冻结的权限.")]
		public static readonly string ignoremp = "tshock.ignore.mp";

		// tshock.item nodes

		[Description("使用命令刷物品的权限.")]
		public static readonly string give = "tshock.item.give";

		[Description("使用命令给自己物品的权限.")]
		public static readonly string item = "tshock.item.spawn";

		[Description("使用被封禁物品的权限.")]
		public static readonly string usebanneditem = "tshock.item.usebanned";

		// tshock.npc nodes

		[Description("可以编辑最大刷怪的权限.")]
		public static readonly string maxspawns = "tshock.npc.maxspawns";

		[Description("可以编辑刷新率的权限.")]
		public static readonly string spawnrate = "tshock.npc.spawnrate";

		[Description("可以使用命令召唤入侵的权限.")]
		public static readonly string invade = "tshock.npc.invade";

		[Description("可以伤害友好NPC的权限.")]
		public static readonly string hurttownnpc = "tshock.npc.hurttown";

		[Description("可以用命令刷Boss的权限.")]
		public static readonly string spawnboss = "tshock.npc.spawnboss";

		[Description("可以改NPC名的权限.")]
		public static readonly string renamenpc = "tshock.npc.rename";

		[Description("使用命令刷怪的权限.")]
		public static readonly string spawnmob = "tshock.npc.spawnmob";

		[Description("使用命令秒杀怪物的权限.")]
		public static readonly string butcher = "tshock.npc.butcher";

		[Description("可以使用物品召唤Boss的权限")]
		public static readonly string summonboss = "tshock.npc.summonboss";

		[Description("可以使用物品召唤入侵的权限.")]
		public static readonly string startinvasion = "tshock.npc.startinvasion";

		[Description("重设渔夫任务的权限.")]
		public static readonly string clearangler = "tshock.npc.clearanglerquests";

		// tshock.superadmin nodes

		[Description("超级管理员专用.")]
		public static readonly string authverify = "tshock.superadmin.authverify";

		[Description("超级管理员专用.")]
		public static readonly string user = "tshock.superadmin.user";

		// tshock.tp nodes

		[Description("可以把所有人传送过来的权限.")]
		public static readonly string tpallothers = "tshock.tp.allothers";

		[Description("可以传送到别人的权限.")]
		public static readonly string tp = "tshock.tp.self";

		[Description("可以把别人传送过来的权限.")]
		public static readonly string tpothers = "tshock.tp.others";

		[Description("可以传送到坐标的权限.")]
		public static readonly string tppos = "tshock.tp.pos";

		[Description("可以使用命令获取当前坐标的权限.")]
		public static readonly string getpos = "tshock.tp.getpos";

		[Description("玩家可以传送到NPC处的权限.")]
		public static readonly string tpnpc = "tshock.tp.npc";

		[Description("让其他玩家无法传送到自己的权限.")]
		public static readonly string tpallow = "tshock.tp.block";

		[Description("可以忽略对方拒绝状态传送的权限.")]
		public static readonly string tpoverride = "tshock.tp.override";

		[Description("没有提示就传送玩家的权限.")]
		public static readonly string tpsilent = "tshock.tp.silent";

		[Description("玩家可以使用回复活点的权限.")]
		public static readonly string home = "tshock.tp.home";

		[Description("玩家可以使用回程的命令")]
		public static readonly string spawn = "tshock.tp.spawn";

		[Description("使用传送法杖的权限.")] 
		public static readonly string rod = "tshock.tp.rod";

		[Description("使用虫洞药水的权限..")]
		public static readonly string wormhole = "tshock.tp.wormhole";

		// tshock.world nodes

		[Description("使用命令开关专家模式的权限.")]
		public static readonly string toggleexpert = "tshock.world.toggleexpert";

		[Description("允许修改地图出生点的权限.")]
		public static readonly string editspawn = "tshock.world.editspawn";

		[Description("允许修改受保护的领地的权限.")]
		public static readonly string editregion = "tshock.world.editregion";

		[Description("召唤血月模式的权限.")]
		public static readonly string bloodmoon = "tshock.world.time.bloodmoon";

		[Description("使用命令刷时间的权限.")]
		public static readonly string time = "tshock.world.time.set";

		[Description("玩家可以使用日冕的权限.")]
		public static readonly string usesundial = "tshock.world.time.usesundial";

		[Description("种植物的权限.")]		public static readonly string grow = "tshock.world.grow";

		[Description("使用命令跳转肉前肉后的权限.")]
		public static readonly string hardmode = "tshock.world.hardmode";

		[Description("修改NPC住房的权限..")]
		public static readonly string movenpc = "tshock.world.movenpc";

		[Description("用户可以修改神圣/腐化的权限.")]
		public static readonly string converthardmode = "tshock.world.converthardmode";

		[Description("开关万圣模式的权限.")]
		public static readonly string halloween = "tshock.world.sethalloween";

		[Description("开关圣诞模式的权限.")]
		public static readonly string xmas = "tshock.world.setxmas";

		[Description("使用命令保存地图的权限.")]
		public static readonly string worldsave = "tshock.world.save";

		[Description("快速平衡液体的权限.")]
		public static readonly string worldsettle = "tshock.world.settleliquids";

		[Description("获取地图信息的权限.")]
		public static readonly string worldinfo = "tshock.world.info";

		[Description("设置地图出生点的权限.")]
		public static readonly string worldspawn = "tshock.world.setspawn";

	    [Description("可以设置地牢位置的权限." )]
        public static readonly string dungeonposition = "tshock.world.setdungeon";

		[Description("User can drop a meteor.")]
		[Description("使用命令召唤陨石的权限.")]
		public static readonly string dropmeteor = "tshock.world.time.dropmeteor";

		[Description("使用命令召唤日食的权限.")]
		public static readonly string eclipse = "tshock.world.time.eclipse";

		[Description("设定满月的权限.")]
		public static readonly string fullmoon = "tshock.world.time.fullmoon";

		[Description("可以对地图进行操作的权限.")]
		public static readonly string canbuild = "tshock.world.modify";
		
		[Description("给墙涂色的权限.")]
		public static readonly string canpaint = "tshock.world.paint";

		[Description("使用命令开关下雨状态的权限.")]
		public static readonly string rain = "tshock.world.rain";

		[Description("使用命令修改风速的权限.")]
		public static readonly string wind = "tshock.world.wind";

		[Description("玩家可以开关派对的权限.")]
		public static readonly string toggleparty = "tshock.world.toggleparty";

		// Non-grouped

		[Description("使用命令清理物品/弹幕的权限.")]
		public static readonly string clear = "tshock.clear";

		[Description("使用命令击杀玩家的权限.")]
		public static readonly string kill = "tshock.kill";
		
		[Description("在服务器满员时使用预留通道进入服务器的权限.")]
		public static readonly string reservedslot = "tshock.reservedslot";

		[Description("使用跃迁点(传送点)的权限.")]
		public static readonly string warp = "tshock.warp";

		[Description("使用命令扇耳光的权限.")]
		public static readonly string slap = "tshock.slap";

		[Description("私聊的权限.")]
		public static readonly string whisper = "tshock.whisper";

		[Description("使用命令干扰其他玩家的权限.")]
		public static readonly string annoy = "tshock.annoy";

		[Description("回别人的血的权限.")]
		public static readonly string heal = "tshock.heal";

		[Description("同队聊天的权限.")]
		public static readonly string canpartychat = "tshock.partychat";

		[Description("卖萌说话的权限.")]
		public static readonly string cantalkinthird = "tshock.thirdperson";

		[Description("查询服务器信息的权限.")]
		public static readonly string serverinfo = "tshock.info";

		[Description("给自己上帝模式的权限. 玩家受伤后会被回满血, 除非是秒杀.")]
		public static readonly string godmode = "tshock.godmode";

		[Description("给别人上帝模式的权限.")]
		public static readonly string godmodeother = "tshock.godmode.other";

		[Description("聊天说话的权限.")] 
		public static readonly string canchat = "tshock.canchat";

		[Description("发射被封禁弹幕的权限.")]
		public static readonly string canusebannedprojectiles = "tshock.projectiles.usebanned";

		[Description("放置被封禁方块的权限.")]
		public static readonly string canusebannedtiles = "tshock.tiles.usebanned";
		/// <summary>
		/// Lists all commands associated with a given permission
		/// </summary>
		/// <param name="perm">string permission - the permission to get information on</param>
		/// <returns>List of commands</returns>
		private static List<Command> GetCommands(string perm)
		{
			if (Commands.ChatCommands.Count < 1)
				Commands.InitCommands();
			return Commands.ChatCommands.Where(c => c.Permissions.Contains(perm)).ToList();
		}

		/// <summary>
		/// Dumps the descriptions of each permission to a file in Markdown format.
		/// </summary>
		public static void DumpDescriptions()
		{
			var sb = new StringBuilder();
			foreach (var field in typeof(Permissions).GetFields().OrderBy(f => f.Name))
			{
				var name = (string)field.GetValue(null);

				var descattr =
					field.GetCustomAttributes(false).FirstOrDefault(o => o is DescriptionAttribute) as DescriptionAttribute;
				var desc = descattr != null && !string.IsNullOrWhiteSpace(descattr.Description) ? descattr.Description : "None";

				var commands = GetCommands(name);
				var strs =
					commands.Select(
						c =>
						"/" + c.Name + (c.Names.Count > 1 ? "(/{0})".SFormat(string.Join(" /", c.Names.ToArray(), 1, c.Names.Count - 1)) : ""));

				sb.AppendLine("{0}".SFormat(name));
				sb.AppendLine("描述: {0}  ".SFormat(desc));
				sb.AppendLine("命令: {0}  ".SFormat(strs.Count() > 0 ? string.Join(" ", strs) : "无"));
				sb.AppendLine();
			}

			File.WriteAllText("权限介绍.txt", sb.ToString());
		}
	}

	[AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
	public sealed class TodoAttribute : Attribute
	{
		public string Info { get; private set; }

		public TodoAttribute(string info)
		{
			Info = info;
		}

		public TodoAttribute()
		{
		}
	}
}
