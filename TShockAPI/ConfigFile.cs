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
using Newtonsoft.Json;
using Rests;

namespace TShockAPI
{
	/// <summary>ConfigFile - The config file class, which contains the configuration for a server that is serialized into JSON and deserialized on load.</summary>
	public class ConfigFile
	{
		/// <summary>WorldSaveInTShockFolder - Load and save the world file in tshock folder.</summary>
		[Description("是否将地图保存到当前文件夹")]
		public bool WorldSaveInTShockFolder = false;
		
		/// <summary>WorldSaveInTShockFolder - Load and save the world file in tshock folder.</summary>
		[Description("是否将地图保存到当前文件夹")]
		public string 版本不同提示语 = "客户端版本不一致.";

		/// <summary>InvasionMultiplier - The equation for calculating invasion size = 100 + (multiplier * (number of active players > 200 hp)).</summary>
		[Description("入侵怪物数量=这个数字*200血以上玩家数量 + 100")]
		public int InvasionMultiplier = 1;

		/// <summary>DefaultMaximumSpawns - The default max spawns per wave.</summary>
		[Description("默认最大刷怪数量. 数字越大, 怪物越多.")]
		public int DefaultMaximumSpawns = 5;

		/// <summary>DefaultSpawnRate - The default spawn rate.</summary>
		[Description("默认刷怪频率. 数字越小, 刷怪越快.")]
		public int DefaultSpawnRate = 600;

		/// <summary>ServerPort - The configured server port.</summary>
		[Description("服务器端口.")]
		public int ServerPort = 7777;

		/// <summary>EnableWhitelist - boolean if the whitelist functionality should be turned on.</summary>
		[Description("是否开启白名单.")]
		public bool EnableWhitelist;

		/// <summary>InfiniteInvasion - Whether or not infinite invasion mode should be on.</summary>
		[Description("是否开启无限入侵（2000000+只怪物）."		)]
		public bool InfiniteInvasion;

		/// <summary>PvPMode - The server PvP mode (normal, always, or disabled).</summary>
		[Description("\"normal\"普通模式 \"always\"强制PVP模式 \"disabled\"禁止PVP模式.")]
		public string PvPMode = "normal";

		/// <summary>SpawnProtection - Enables the spawn protection system.</summary>
		[Description("是否进行复活点保护.")]
		public bool SpawnProtection = true;

		/// <summary>SpawnProtectionRadius - The spawn protection tile radius.</summary>
		[Description("复活点保护的范围.")]
		public int SpawnProtectionRadius = 10;

		/// <summary>MaxSlots - The server's max slots.</summary>
		[Description("最大玩家数量.")]
		public int MaxSlots = 8;

		/// <summary>RangeChecks - Whether or not the anti-grief system based on range should be enabled.</summary>
		[Description("范围检查.")]
		public bool RangeChecks = true;

		/// <summary>DisableBuild - Whether or not building should be enabled.</summary>
		[Description("全图保护.")]
		public bool DisableBuild;

		/// <summary>SuperAdminChatRGB - The chat color for the superadmin group.</summary>
		[Description("超级管理员聊天颜色.")]
		public int[] SuperAdminChatRGB = { 255, 0, 0 };

		/// <summary>SuperAdminChatPrefix - The superadmin chat prefix.</summary>
		[Description("超级管理员聊天前缀.")]
		public string SuperAdminChatPrefix = "(管理员) ";

		/// <summary>SuperAdminChatSuffix - The superadmin chat suffix.</summary>
		[Description("超级管理员聊天后缀.")]
		public string SuperAdminChatSuffix = "";

		/// <summary>BackupInterval - The backup frequency in minutes.</summary>
		[Description("备份间隔 单位分钟.")]
		public int BackupInterval;

		/// <summary>BackupKeepFor - Backup max age in minutes.</summary>
		[Description("地图备份保留时间.")]
		public int BackupKeepFor = 60;

		/// <summary>RememberLeavePos - Whether or not to remember where an IP player was when they left.</summary>
		[Description("记录玩家最后位置, 下次进入服务器时传送回去."		)]
		public bool RememberLeavePos;

		/// <summary>HardcoreOnly - Whether or not HardcoreOnly should be enabled.</summary>
		[Description("仅允许困难模式的玩家进入服务器.")]
		public bool HardcoreOnly;

		/// <summary>MediumcoreOnly - Whether or not MediumCore only players should be enabled.</summary>
		[Description("仅允许中等模式的玩家进入服务器.")]
		public bool MediumcoreOnly;

		/// <summary>KickOnMediumcoreDeath - Whether or not to kick mediumcore players on death.</summary>
		[Description("踢出死亡的中等难度的玩家.")]
		public bool KickOnMediumcoreDeath;

		/// <summary>BanOnMediumcoreDeath - Whether or not to ban mediumcore players on death.</summary>
		[Description("封禁死亡的中等难度的玩家.")]
		public bool BanOnMediumcoreDeath;

		/// <summary>AutoSave - Whether or not to use Terraria's built-in world auto save.</summary>
		[Description("是否自动保存地图, 建议开启.")]
		public bool AutoSave = true;
		/// <summary>AnnounceSave - Whether or not to broadcast world saves.</summary>
		[Description("自动保存的时候是否进行提示.")]
		public bool AnnounceSave = true;

		/// <summary>MaximumLoginAttempts - Number of failed login attempts before kicking a player.</summary>
		[Description("允许输错密码次数.")]
		public int MaximumLoginAttempts = 3;

		/// <summary>ServerName - Used when replying to a REST /status request or sent to the client.</summary>
		[Description("远程服务器名.")]
		public string ServerName = "";
		/// <summary>UseServerName - Whether or not to use ServerName in place of the world name.</summary>
		[Description("用服务器名代替地图名.")]
		public bool UseServerName = false;
		/// <summary>MasterServer - Not implemented.</summary>
		[Obsolete("Not implemented")]
		[Description("没吊用.")]
		public string MasterServer = "127.0.0.1";

		/// <summary>StorageType - The type of SQL database to use when storing data (either "sqlite" or "mysql").</summary>
		[Description("数据库类型, \"sqlite\" 或 \"mysql\"")]
		public string StorageType = "sqlite";

		/// <summary>MySqlHost - The hostname and port to to use when connecting to a MySQL database.</summary>
		[Description("MySQL主机名.")]
		public string MySqlHost = "localhost:3306";
		/// <summary>MySqlDbName - The database name to use when connecting to a MySQL database.</summary>
		[Description("数据库名.")]
		public string MySqlDbName = "";
		/// <summary>MySqlUsername - The username for the login credentials used when connecting to a MySQL database.</summary>
		[Description("数据库用户名.")]
		public string MySqlUsername = "";
		/// <summary>MySqlPassword - The password for the login credentials used when connecting to a MySQL database.</summary>
		[Description("数据库密码.")]
		public string MySqlPassword = "";

		/// <summary>MediumcoreBanReason - The reason given if banning mediumcore players on death.</summary>
		[Description("封禁死亡的中等难度的玩家.")]
		public string MediumcoreBanReason = "死亡而被封禁";
		/// <summary>MediumcoreKickReason - The reason given if kicking mediumcore players on death.</summary>

		[Description("踢出死亡的中等难度的玩家.")]
		public string MediumcoreKickReason = "死亡而被踢出";
		/// <summary>EnableDNSHostResolution - Not implemented.</summary>
		[Obsolete("Not implemented")]
		[Description("使用GetGroupForIPExpensive.")]
		public bool EnableDNSHostResolution;

		/// <summary>EnableIPBans - Whether or not to kick players on join that match a banned IP address.</summary>
		[Description("自动封禁IP.")]
		public bool EnableIPBans = true;

		/// <summary>EnableUUIDBans - Whether or not to kick players on join that match a banned UUID.</summary>
		[Description("自动封禁UUID.")]
		public bool EnableUUIDBans = true;

		/// <summary>EnableBanOnUsernames - Whether or not to kick players on join that match a banned character name.</summary>
		[Description("自动封禁玩家名.")]
		public bool EnableBanOnUsernames;

		/// <summary>DefaultRegistrationGroupName - The default group name to place newly registered users under.</summary>
		[Description("默认注册玩家所在组.")]
		public string DefaultRegistrationGroupName = "default";

		/// <summary>DefaultGuestGroupName - The default group name to place unregistered players under.</summary>
		[Description("默认未注册玩家所在组.")]
		public string DefaultGuestGroupName = "guest";

		/// <summary>DisableSpewLogs - Whether or not to send logs as messages to players with the log permission.</summary>
		[Description("不对玩家显示日志.")]
		public bool DisableSpewLogs = true;

		/// <summary>DisableSecondUpdateLogs - Prevents OnSecondUpdate() checks from writing to the log file.</summary>
		[Description("预防OnSecondUpdate刷出大量日志.")]
		public bool DisableSecondUpdateLogs = false;

		/// <summary>HashAlgorithm - The hash algorithm used to encrypt user passwords.
		/// Valid types: "sha512", "sha256" and "md5". Append with "-xp" for the xp supported algorithms.</summary>
		[Description("密码加密算法 \"sha512\", \"sha256\", \"md5\".")]
		public string HashAlgorithm = "sha512";

		/// <summary>BufferPackets - No longer used.</summary>
		[Obsolete("No longer used")]
		[Description("缓冲包.")]
		public bool BufferPackets = true;

		/// <summary>ServerFullReason - The reason given when kicking players when the server is full.</summary>
		[Description("服务器满时提示信息.")]
		public string ServerFullReason = "服务器已满.";

		/// <summary>WhitelistKickReason - The reason given when kicking players for not being on the whitelist.</summary>
		[Description("不在白名单而被踢时提示信息.")]
		public string WhitelistKickReason = "你不在白名单里.";

		/// <summary>ServerFullNoReservedReason - The reason given when kicking players when the server is full and there are no reserved slots open.</summary>
		[Description("服务器爆满时提示信息.")]
		public string ServerFullNoReservedReason = "服务器爆满.";

		/// <summary>SaveWorldOnCrash - Attempts to save world in the server crashes due to an unhandled exception.</summary>
		[Description("崩溃时保存地图.")]
		public bool SaveWorldOnCrash = true;

		/// <summary>EnableGeoIP - Whether or not to announce a player's location on join.</summary>
		[Description("显示玩家IP国家")]
		public bool EnableGeoIP;

		/// <summary>EnableTokenEndpointAuthentication - Whether or not to require token authentication for the public REST API endpoints.</summary>
		[Description("远程端口要求强制认证.")]
		public bool EnableTokenEndpointAuthentication;

		/// <summary>RestApiEnabled - Enable/disable the REST API.</summary>
		[Description("远程控制.")]
		public bool RestApiEnabled;

		/// <summary>RestApiPort - The port used by the REST API.</summary>
		[Description("远程控制端口.")]
		public int RestApiPort = 7878;

		/// <summary>DisableTombstones - Disable tombstone dropping during death for all players.</summary>
		[Description("禁止墓碑.")]
		public bool DisableTombstones = true;

		/// <summary>DisplayIPToAdmins - Displays a player's IP on join to everyone with the log permission.</summary>
		[Description("对管理员显示玩家IP.")]
		public bool DisplayIPToAdmins;

		/// <summary>KickProxyUsers - If the GeoIP service is running, this will kick users under a proxy.</summary>
		[Description("自动踢出代理玩家.")]
		public bool KickProxyUsers = true;

		/// <summary>DisableHardmode - If set to true, hardmode will not be activated by the Wall of Flesh or the /starthardmode command.</summary>
		[Description("锁定肉山前.")]
		public bool DisableHardmode;

		/// <summary>DisableDungeonGuardian - Disables the dungeon guardian from being spawned while sending players to their spawn point instead.</summary>
		[Description("禁止打地牢骷髅.")]
		public bool DisableDungeonGuardian;

		/// <summary>DisableClownBombs - Disables clown bomb projectiles from spawning.</summary>
		[Description("禁止小丑丢炸弹")]
		public bool DisableClownBombs;

		/// <summary>DisableSnowBalls - Disables snow ball projectiles from spawning.</summary>
		[Description("禁止雪人丢雪球")]
		public bool DisableSnowBalls;

		/// <summary>ChatFormat - Controls the in-game chat format. {0} = Group Name, {1} = Group Prefix, {2} = Player Name, {3} = Group Suffix, {4} = Chat Message.</summary>
		[Description("聊天格式 {0}组名 {1}前缀 {2}玩家名 {3}后缀 {4}说的话"		)]
		public string ChatFormat = "{1}{2}{3}: {4}";

		/// <summary>ChatAboveHeadsFormat - Modifies the player name when using chat above heads. Same formatting options as ChatFormat.</summary>
		[Description("头顶显示聊天文字. 格式同上.")]
		public string ChatAboveHeadsFormat = "{2}";

		/// <summary>ForceTime - Can be either "normal", "day" or "night". When set to one of the latter two, the blocks other.</summary>
		[Description("锁定时间. normal正常 day白天 night晚上.")]
		public string ForceTime = "normal";

		/// <summary>TileKillThreshold - Disables/reverts a player if this number of tile kills is exceeded within 1 second.</summary>
		[Description("每秒方块破坏上限")]
		public int TileKillThreshold = 60;

		/// <summary>TilePlaceThreshold - Disables/reverts a player if this number of tile placements is exceeded within 1 second.</summary>
		[Description("每秒方块放置上限")]
		public int TilePlaceThreshold = 20;

		/// <summary>TileLiquidThreshold - Disables a player if this number of liquid sets is exceeded within 1 second.</summary>
		[Description("每秒液体放置上限")]
		public int TileLiquidThreshold = 15;

		/// <summary>ProjectileThreshold - Disables a player if this number of projectiles is created within 1 second.</summary>
		[Description("每秒弹幕发射上限")]
		public int ProjectileThreshold = 50;

		/// <summary>ProjIgnoreShrapnel - Whether or not to ignore shrapnel from crystal bullets for the projectile threshold count.</summary>
		[Description("忽略弹幕碎片")]
		public bool ProjIgnoreShrapnel = true;

		/// <summary>RequireLogin - Requires all players to register or login before being allowed to play.</summary>
		[Description("强制登录.")]
		public bool RequireLogin;

		/// <summary>DisableInvisPvP - Whether or not to turn a player invisible if using invisibility potions during PvP.</summary>
		[Description(
			"禁止隐身PVP"
		)]
		public bool DisableInvisPvP;

		/// <summary>MaxRangeForDisabled - The maximum distance, in tiles, that disabled players can move from.</summary>
		[Description("石化后可移动距离.")]
		public int MaxRangeForDisabled = 10;

		/// <summary>ServerPassword - The server password required to join the server.</summary>
		[Description("服务器密码")]
		public string ServerPassword = "";

		/// <summary>RegionProtectChests - Whether or not region protection should apply to chests.</summary>
		[Description("保护领地里的箱子")]
		public bool RegionProtectChests;

		/// <summary>RegionProtectGemLocks - Whether or not region protection should apply to gem locks.</summary>
		[Description("保护领地中的Gem lock.")]
		public bool RegionProtectGemLocks = true;

		/// <summary>DisableLoginBeforeJoin - This will prevent users from being able to login before connecting.</summary>
		[Description("禁止进服前登录.")]
		public bool DisableLoginBeforeJoin;

		/// <summary>DisableUUIDLogin - This will disable automatic login through a saved client UUID.</summary>
		[Description("强制密码登录.")]
		public bool DisableUUIDLogin;

		/// <summary>KickEmptyUUID - Kick clients that don't send a UUID to the server.</summary>
		[Description("踢出异常客户端.")]
		public bool KickEmptyUUID;

		/// <summary>AllowRegisterAnyUsername - Allows users to register a username that doesn't necessarily match their character name.</summary>
		[Description("允许自由注册.")]
		public bool AllowRegisterAnyUsername;

		/// <summary>AllowLoginAnyUsername - Allows users to login to any account even if the username doesn't match their character name.</summary>
		[Description("允许自由登录.")]
		public bool AllowLoginAnyUsername = true;

		/// <summary>The maximum damage a player/NPC can inflict.</summary>
		[Description("攻击力上限.")]
		public int MaxDamage = 1175;

		/// <summary>The maximum damage a projectile can inflict.</summary>
		[Description("弹幕攻击力上限.")]
		public int MaxProjDamage = 1175;

		/// <summary>KickOnDamageThresholdBroken - Whether or not to kick users when they surpass the MaxDamage threshold.</summary>
		[Description("踢出受伤超过攻击力上限的玩家")]
		public bool KickOnDamageThresholdBroken = false;

		/// <summary>IgnoreProjUpdate - Ignores checking to see if player 'can' update a projectile.</summary>
		[Description("忽略弹幕更新检查")]
		public bool IgnoreProjUpdate = false;

		/// <summary>IgnoreProjKill - Ignores checking to see if player 'can' kill a projectile.</summary>
		[Description("忽略弹幕消灭检查")]
		public bool IgnoreProjKill = false;

		/// <summary>IgnoreNoClip - Ignores all no clip checks for players.</summary>
		[Description("忽略所有no clip检查（什么鬼）")]
		public bool IgnoreNoClip = false;

		/// <summary>AlllowIce - Allows ice placement even where a user cannot usually build.</summary>
		[Description("地图被保护时允许玩家使用寒冰魔杖")]
		public bool AllowIce = false;

		/// <summary>AllowCrimsonCreep - Enables or disables crimson to spread when a world is in hardmode.</summary>
		[Description("允许血腥传播")]
		public bool AllowCrimsonCreep = true;

		/// <summary>AllowCorruptionCreep - Enables or disables corruption to spread when a world is in hardmode.</summary>
		[Description("允许腐化传播")]
		public bool AllowCorruptionCreep = true;

		/// <summary>AllowHallowCreep - Enables or disables hallow to spread when a world is in hardmode.</summary>
		[Description("允许神圣传播")]
		public bool AllowHallowCreep = true;

		/// <summary>StatueSpawn200 - How many NPCs a statue can spawn within 200 pixels(?) before it stops spawning.</summary>
		[Description("雕像200像素刷物品上限")]
		public int StatueSpawn200 = 3;

		/// <summary>StatueSpawn600 - How many NPCs a statue can spawn within 600 pixels(?) before it stops spawning.</summary>
		[Description("雕像600像素刷物品上限")]
		public int StatueSpawn600 = 6;

		/// <summary>StatueSpawnWorld - How many NPCs a statue can spawn before it stops spawning.</summary>
		[Description("雕像全图刷物品上限")]
		public int StatueSpawnWorld = 10;

		/// <summary>PreventBannedItemSpawn - Prevents banned items from being spawned with commands.</summary>
		[Description("预防命令刷出被封禁物品.")]
		public bool PreventBannedItemSpawn = false;

		/// <summary>PreventDeadModification - Prevent players from interacting with the world if dead.</summary>
		[Description("玩家死后不能修改地图.")]
		public bool PreventDeadModification = true;

		/// <summary>EnableChatAboveHeads - Whether or not to display chat messages above players' heads.</summary>
		[Description("头顶显示聊天文字.")]
		public bool EnableChatAboveHeads = false;

		/// <summary>ForceXmas - Force Christmas-only events to occur all year.</summary>
		[Description("开启圣诞")]
		public bool ForceXmas = false;

		/// <summary>AllowAllowedGroupsToSpawnBannedItems - Allows groups on the banned item allowed list to spawn banned items even if <see cref="PreventBannedItemSpawn"/> is set to true.</summary>
		[Description("允许特定组用命令刷出被封禁物品.")]
		public bool AllowAllowedGroupsToSpawnBannedItems = false;

		/// <summary>IgnoreChestStacksOnLoad - Allows stacks in chests to be beyond the stack limit during world load.</summary>
		[Description("忽略箱子中的作弊物品.")]
		public bool IgnoreChestStacksOnLoad = false;

		/// <summary>LogPath - The path of the directory where logs should be written to.</summary>
		[Description("日志保存文件夹.")]
		public string LogPath = "tshock";

		/// <summary>UseSqlLogs - Whether or not to save logs to a SQL database instead of a text file.</summary>
		[Description("保存SQL记录至文件.")]
		public bool UseSqlLogs = false;

		/// <summary>RevertToTextLogsOnSqlFailures - Number of times the SQL log must fail to insert logs before falling back to the text log.</summary>
		[Description("SQL记录前失败次数")]
		public int RevertToTextLogsOnSqlFailures = 10;

		/// <summary>PreventInvalidPlaceStyle - Prevents players from placing tiles with an invalid style.</summary>
		[Description("预防非法放置方式")]
		public bool PreventInvalidPlaceStyle = true;

		/// <summary>BroadCastRGB - The RGB values used for the color of broadcast messages.</summary>
		[Description("服务器广播颜色.")]
		public int[] BroadcastRGB = { 127, 255, 212 };

		// TODO: Get rid of this when the old REST permission model is removed.
		[Description(
			"远程使用新的权限"
		)]
		public bool RestUseNewPermissionModel = true;

		/// <summary>ApplicationRestTokens - A dictionary of REST tokens that external applications may use to make queries to your server.</summary>
		[Description("A dictionary of REST tokens that external applications may use to make queries to your server.")]
		public Dictionary<string, SecureRest.TokenData> ApplicationRestTokens = new Dictionary<string, SecureRest.TokenData>();

		/// <summary>ReservedSlots - The number of reserved slots past your max server slot that can be joined by reserved players.</summary>
		[Description("预留空位数.")]
		public int ReservedSlots = 20;

		/// <summary>LogRest - Whether or not to log REST API connections.</summary>
		[Description("是否将Rest API连接记入日志.")]
		public bool LogRest = false;

		/// <summary>RespawnSeconds - The number of seconds a player must wait before being respawned.</summary>
		[Description("复活时间.")]
		public int RespawnSeconds = 5;

		/// <summary>RespawnBossSeconds - "The number of seconds a player must wait before being respawned if there is a boss nearby.</summary>
		[Description("Boss战复活时间.")]
		public int RespawnBossSeconds = 10;

		/// <summary>TilePaintThreshold - Disables a player if this number of tiles is painted within 1 second.</summary>
		[Description("每秒涂漆上限")]
		public int TilePaintThreshold = 15;

		/// <summary>EnableMaxBytesInBuffer - Not implemented.</summary>
		[Obsolete("Not implemented")]
		[Description("开启最大缓冲.")]
		public bool EnableMaxBytesInBuffer = false;

		/// <summary>MaxBytesInBuffer - Not implemented.</summary>
		[Obsolete("Not implemented")]
		[Description("缓冲大小.")]
		public int MaxBytesInBuffer = 5242880;

		/// <summary>ForceHalloween - Forces Halloween-only events to occur all year.</summary>
		[Description("强制万圣.")]
		public bool ForceHalloween = false;

		/// <summary>AllowCutTilesAndBreakables - Allows players to break temporary tiles (grass, pots, etc) even if they cannot typically build in a region.</summary>
		[Description("允许任何人破坏草和罐子.")]
		public bool AllowCutTilesAndBreakables = false;

		/// <summary>CommandSpecifier - Specifies which string starts a command.
		/// Note: Will not function properly if the string length is bigger than 1.</summary>
		[Description("命令标志")]
		public string CommandSpecifier = "/";

		/// <summary>CommandSilentSpecifier - Specifies which string starts a command silently.
		/// Note: Will not function properly if the string length is bigger than 1.</summary>
		[Description("静默命令标志")]
		public string CommandSilentSpecifier = ".";
		
		/// <summary>KickOnHardcoreDeath - Whether or not to kick a hardcore player on death.</summary>
		[Description("困难难度死亡踢出")]
		public bool KickOnHardcoreDeath;
		
		/// <summary>BanOnHardcoreDeath - Whether or not to ban a hardcore player on death.</summary>
		[Description("困难难度死亡封禁")]
		public bool BanOnHardcoreDeath;
		
		/// <summary>HardcoreBanReason - The reason given when banning a hardcore player on death.</summary>
		[Description("困难难度死亡封禁")]
		public string HardcoreBanReason = "死亡而被封禁";
		
		/// <summary>HardcoreKickReason - The reason given when kicking a hardcore player on death.</summary>
		[Description("困难难度死亡踢出")]
		public string HardcoreKickReason = "死亡而被踢出";

		/// <summary>AnonymousBossInvasions - Whether or not to announce boss spawning or invasion starts.</summary>
		[Description("匿名召唤Boss或入侵")]
		public bool AnonymousBossInvasions = true;

		/// <summary>MaxHP - The maximum allowable HP, before equipment buffs.</summary>
		[Description("最大血量")]
		public int MaxHP = 500;

		/// <summary>MaxMP - The maximum allowable MP, before equipment buffs.</summary>
		[Description("最大魔法")]
		public int MaxMP = 200;

		/// <summary>SaveWorldOnLastPlayerExit - Whether or not to save the world when the last player disconnects.</summary>
		[Description("玩家退出时保存地图")]
		public bool SaveWorldOnLastPlayerExit = true;

		/// <summary>BCryptWorkFactor - Determines the BCrypt work factor to use. If increased, all passwords will be upgraded to new work-factor on verify.
		/// The number of computational rounds is 2^n. Increase with caution. Range: 5-31.</summary>
		[Description("BCrypt加密工作因子")]
		public int BCryptWorkFactor = 7;

		/// <summary>MinimumPasswordLength - The minimum password length for new user accounts.</summary>
		[Description("最小密码长度")]
		public int MinimumPasswordLength = 4;

		/// <summary>RESTMaximumRequestsPerInterval - The maximum REST requests in the bucket before denying requests.</summary>
		[Description("远程请求间隔")]
		public int RESTMaximumRequestsPerInterval = 5;

		/// <summary>RESTRequestBucketDecreaseIntervalMinutes - How often in minutes the REST requests bucket is decreased by one.</summary>
		[Description("远程请求减少速度")]
		public int RESTRequestBucketDecreaseIntervalMinutes = 1;

		/// <summary>RESTLimitOnlyFailedLoginRequests - Whether or not to limit only the max failed login requests, or all login requests.</summary>
		[Description("远程请求只限制错误密码")]
		public bool RESTLimitOnlyFailedLoginRequests = true;

		/// <summary>ShowBackupAutosaveMessages - Whether or not to show backup auto save messages.</summary>
		[Description("显示自动保存信息")]
		public bool ShowBackupAutosaveMessages = true;
		/// <summary>
		/// Reads a configuration file from a given path
		/// </summary>
		/// <param name="path">string path</param>
		/// <returns>ConfigFile object</returns>
		public static ConfigFile Read(string path)
		{
			if (!File.Exists(path))
			{
				return new ConfigFile();
			}
			using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				return Read(fs);
			}
		}

		/// <summary>
		/// Reads the configuration file from a stream
		/// </summary>
		/// <param name="stream">stream</param>
		/// <returns>ConfigFile object</returns>
		public static ConfigFile Read(Stream stream)
		{
			using (var sr = new StreamReader(stream))
			{
				var cf = JsonConvert.DeserializeObject<ConfigFile>(sr.ReadToEnd());
				if (ConfigRead != null)
					ConfigRead(cf);
				return cf;
			}
		}

		/// <summary>
		/// Writes the configuration to a given path
		/// </summary>
		/// <param name="path">string path - Location to put the config file</param>
		public void Write(string path)
		{
			using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Write))
			{
				Write(fs);
			}
		}

		/// <summary>
		/// Writes the configuration to a stream
		/// </summary>
		/// <param name="stream">stream</param>
		public void Write(Stream stream)
		{
			var str = JsonConvert.SerializeObject(this, Formatting.Indented);
			using (var sw = new StreamWriter(stream))
			{
				sw.Write(str);
			}
		}

		/// <summary>
		/// On config read hook
		/// </summary>
		public static Action<ConfigFile> ConfigRead;

		/// <summary>
		/// Dumps all configuration options to a text file in Markdown format
		/// </summary>
		public static void DumpDescriptions()
		{
			var sb = new StringBuilder();
			var defaults = new ConfigFile();

			foreach (var field in defaults.GetType().GetFields().OrderBy(f => f.Name))
			{
				if (field.IsStatic)
					continue;

				var name = field.Name;
				var type = field.FieldType.Name;

				var descattr =
					field.GetCustomAttributes(false).FirstOrDefault(o => o is DescriptionAttribute) as DescriptionAttribute;
				var desc = descattr != null && !string.IsNullOrWhiteSpace(descattr.Description) ? descattr.Description : "无";

				var def = field.GetValue(defaults);

				sb.AppendLine("{0}  ".SFormat(name));
				sb.AppendLine("类型: {0}  ".SFormat(type));
				sb.AppendLine("描述: {0}  ".SFormat(desc));
				sb.AppendLine("默认值: \"{0}\"  ".SFormat(def));
				sb.AppendLine();
			}

			File.WriteAllText("Config介绍.txt", sb.ToString());
		}
	}
}