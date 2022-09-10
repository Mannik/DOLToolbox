using DOL.Database.Attributes;

namespace DOL.Database
{
	// Token: 0x0200001B RID: 27
	[DataTable(TableName = "DataQuestRewardQuest")]
	public class DBDQRewardQ : DataObject
	{
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00004F1B File Offset: 0x00003F1B
		// (set) Token: 0x060000E3 RID: 227 RVA: 0x00004F23 File Offset: 0x00003F23
		[PrimaryKey(AutoIncrement = true)]
		public int ID
		{
			get
			{
				return this.m_id;
			}
			set
			{
				this.m_id = value;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00004F2C File Offset: 0x00003F2C
		// (set) Token: 0x060000E5 RID: 229 RVA: 0x00004F34 File Offset: 0x00003F34
		[DataElement(Varchar = 255, AllowDbNull = false)]
		public string QuestName
		{
			get
			{
				return this.m_questName;
			}
			set
			{
				this.m_questName = value;
				this.Dirty = true;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00004F44 File Offset: 0x00003F44
		// (set) Token: 0x060000E7 RID: 231 RVA: 0x00004F4C File Offset: 0x00003F4C
		[DataElement(Varchar = 100, AllowDbNull = false)]
		public string StartNPC
		{
			get
			{
				return this.m_startNPC;
			}
			set
			{
				this.m_startNPC = value;
				this.Dirty = true;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00004F5C File Offset: 0x00003F5C
		// (set) Token: 0x060000E9 RID: 233 RVA: 0x00004F64 File Offset: 0x00003F64
		[DataElement(AllowDbNull = false)]
		public ushort StartRegionID
		{
			get
			{
				return this.m_startRegionID;
			}
			set
			{
				this.m_startRegionID = value;
				this.Dirty = true;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00004F74 File Offset: 0x00003F74
		// (set) Token: 0x060000EB RID: 235 RVA: 0x00004F7C File Offset: 0x00003F7C
		[DataElement(AllowDbNull = true)]
		public string StoryText
		{
			get
			{
				return this.m_storyText;
			}
			set
			{
				this.m_storyText = value;
				this.Dirty = true;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000EC RID: 236 RVA: 0x00004F8C File Offset: 0x00003F8C
		// (set) Token: 0x060000ED RID: 237 RVA: 0x00004F94 File Offset: 0x00003F94
		[DataElement(AllowDbNull = true)]
		public string Summary
		{
			get
			{
				return this.m_summary;
			}
			set
			{
				this.m_summary = value;
				this.Dirty = true;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000EE RID: 238 RVA: 0x00004FA4 File Offset: 0x00003FA4
		// (set) Token: 0x060000EF RID: 239 RVA: 0x00004FAC File Offset: 0x00003FAC
		[DataElement(AllowDbNull = true)]
		public string AcceptText
		{
			get
			{
				return this.m_acceptText;
			}
			set
			{
				this.m_acceptText = value;
				this.Dirty = true;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x00004FBC File Offset: 0x00003FBC
		// (set) Token: 0x060000F1 RID: 241 RVA: 0x00004FC4 File Offset: 0x00003FC4
		[DataElement(AllowDbNull = true)]
		public string QuestGoals
		{
			get
			{
				return this.m_questGoals;
			}
			set
			{
				this.m_questGoals = value;
				this.Dirty = true;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x00004FD4 File Offset: 0x00003FD4
		// (set) Token: 0x060000F3 RID: 243 RVA: 0x00004FDC File Offset: 0x00003FDC
		[DataElement(AllowDbNull = true)]
		public string GoalType
		{
			get
			{
				return this.m_goalType;
			}
			set
			{
				this.m_goalType = value;
				this.Dirty = true;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x00004FEC File Offset: 0x00003FEC
		// (set) Token: 0x060000F5 RID: 245 RVA: 0x00004FF4 File Offset: 0x00003FF4
		[DataElement(AllowDbNull = true)]
		public string GoalRepeatNo
		{
			get
			{
				return this.m_goalRepeatNo;
			}
			set
			{
				this.m_goalRepeatNo = value;
				this.Dirty = true;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x00005004 File Offset: 0x00004004
		// (set) Token: 0x060000F7 RID: 247 RVA: 0x0000500C File Offset: 0x0000400C
		[DataElement(AllowDbNull = true)]
		public string GoalTargetName
		{
			get
			{
				return this.m_goaltargetName;
			}
			set
			{
				this.m_goaltargetName = value;
				this.Dirty = true;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x0000501C File Offset: 0x0000401C
		// (set) Token: 0x060000F9 RID: 249 RVA: 0x00005024 File Offset: 0x00004024
		[DataElement(AllowDbNull = true)]
		public string GoalTargetText
		{
			get
			{
				return this.m_goaltargetText;
			}
			set
			{
				this.m_goaltargetText = value;
				this.Dirty = true;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060000FA RID: 250 RVA: 0x00005034 File Offset: 0x00004034
		// (set) Token: 0x060000FB RID: 251 RVA: 0x0000503C File Offset: 0x0000403C
		[DataElement(AllowDbNull = true)]
		public int StepCount
		{
			get
			{
				return this.m_stepCount;
			}
			set
			{
				this.m_stepCount = value;
				this.Dirty = true;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060000FC RID: 252 RVA: 0x0000504C File Offset: 0x0000404C
		// (set) Token: 0x060000FD RID: 253 RVA: 0x00005054 File Offset: 0x00004054
		[DataElement(AllowDbNull = true)]
		public string FinishNPC
		{
			get
			{
				return this.m_finishNPC;
			}
			set
			{
				this.m_finishNPC = value;
				this.Dirty = true;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060000FE RID: 254 RVA: 0x00005064 File Offset: 0x00004064
		// (set) Token: 0x060000FF RID: 255 RVA: 0x0000506C File Offset: 0x0000406C
		[DataElement(AllowDbNull = true)]
		public string AdvanceText
		{
			get
			{
				return this.m_goaladvanceText;
			}
			set
			{
				this.m_goaladvanceText = value;
				this.Dirty = true;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000100 RID: 256 RVA: 0x0000507C File Offset: 0x0000407C
		// (set) Token: 0x06000101 RID: 257 RVA: 0x00005084 File Offset: 0x00004084
		[DataElement(AllowDbNull = true)]
		public string CollectItemTemplate
		{
			get
			{
				return this.m_collectItemTemplate;
			}
			set
			{
				this.m_collectItemTemplate = value;
				this.Dirty = true;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000102 RID: 258 RVA: 0x00005094 File Offset: 0x00004094
		// (set) Token: 0x06000103 RID: 259 RVA: 0x0000509C File Offset: 0x0000409C
		[DataElement(AllowDbNull = false)]
		public ushort MaxCount
		{
			get
			{
				return this.m_maxCount;
			}
			set
			{
				this.m_maxCount = value;
				this.Dirty = true;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000104 RID: 260 RVA: 0x000050AC File Offset: 0x000040AC
		// (set) Token: 0x06000105 RID: 261 RVA: 0x000050B4 File Offset: 0x000040B4
		[DataElement(AllowDbNull = false)]
		public byte MinLevel
		{
			get
			{
				return this.m_minLevel;
			}
			set
			{
				this.m_minLevel = value;
				this.Dirty = true;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000106 RID: 262 RVA: 0x000050C4 File Offset: 0x000040C4
		// (set) Token: 0x06000107 RID: 263 RVA: 0x000050CC File Offset: 0x000040CC
		[DataElement(AllowDbNull = false)]
		public byte MaxLevel
		{
			get
			{
				return this.m_maxLevel;
			}
			set
			{
				this.m_maxLevel = value;
				this.Dirty = true;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000108 RID: 264 RVA: 0x000050DC File Offset: 0x000040DC
		// (set) Token: 0x06000109 RID: 265 RVA: 0x000050E4 File Offset: 0x000040E4
		[DataElement(AllowDbNull = true)]
		public long RewardMoney
		{
			get
			{
				return this.m_rewardMoney;
			}
			set
			{
				this.m_rewardMoney = value;
				this.Dirty = true;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600010A RID: 266 RVA: 0x000050F4 File Offset: 0x000040F4
		// (set) Token: 0x0600010B RID: 267 RVA: 0x000050FC File Offset: 0x000040FC
		[DataElement(AllowDbNull = true)]
		public long RewardXP
		{
			get
			{
				return this.m_rewardXP;
			}
			set
			{
				this.m_rewardXP = value;
				this.Dirty = true;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600010C RID: 268 RVA: 0x0000510C File Offset: 0x0000410C
		// (set) Token: 0x0600010D RID: 269 RVA: 0x00005114 File Offset: 0x00004114
		[DataElement(AllowDbNull = true)]
		public long RewardCLXP
		{
			get
			{
				return this.m_rewardCLXP;
			}
			set
			{
				this.m_rewardCLXP = value;
				this.Dirty = true;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600010E RID: 270 RVA: 0x00005124 File Offset: 0x00004124
		// (set) Token: 0x0600010F RID: 271 RVA: 0x0000512C File Offset: 0x0000412C
		[DataElement(AllowDbNull = true)]
		public long RewardRP
		{
			get
			{
				return this.m_rewardRP;
			}
			set
			{
				this.m_rewardRP = value;
				this.Dirty = true;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000110 RID: 272 RVA: 0x0000513C File Offset: 0x0000413C
		// (set) Token: 0x06000111 RID: 273 RVA: 0x00005144 File Offset: 0x00004144
		[DataElement(AllowDbNull = true)]
		public long RewardBP
		{
			get
			{
				return this.m_rewardBP;
			}
			set
			{
				this.m_rewardBP = value;
				this.Dirty = true;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000112 RID: 274 RVA: 0x00005154 File Offset: 0x00004154
		// (set) Token: 0x06000113 RID: 275 RVA: 0x0000515C File Offset: 0x0000415C
		[DataElement(AllowDbNull = true)]
		public string OptionalRewardItemTemplates
		{
			get
			{
				return this.m_optionalRewardItemTemplates;
			}
			set
			{
				this.m_optionalRewardItemTemplates = value;
				this.Dirty = true;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000114 RID: 276 RVA: 0x0000516C File Offset: 0x0000416C
		// (set) Token: 0x06000115 RID: 277 RVA: 0x00005174 File Offset: 0x00004174
		[DataElement(AllowDbNull = true)]
		public string FinalRewardItemTemplates
		{
			get
			{
				return this.m_finalRewardItemTemplates;
			}
			set
			{
				this.m_finalRewardItemTemplates = value;
				this.Dirty = true;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000116 RID: 278 RVA: 0x00005184 File Offset: 0x00004184
		// (set) Token: 0x06000117 RID: 279 RVA: 0x0000518C File Offset: 0x0000418C
		[DataElement(AllowDbNull = true)]
		public string FinishText
		{
			get
			{
				return this.m_finishText;
			}
			set
			{
				this.m_finishText = value;
				this.Dirty = true;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000118 RID: 280 RVA: 0x0000519C File Offset: 0x0000419C
		// (set) Token: 0x06000119 RID: 281 RVA: 0x000051A4 File Offset: 0x000041A4
		[DataElement(AllowDbNull = true)]
		public string QuestDependency
		{
			get
			{
				return this.m_questDependency;
			}
			set
			{
				this.m_questDependency = value;
				this.Dirty = true;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600011A RID: 282 RVA: 0x000051B4 File Offset: 0x000041B4
		// (set) Token: 0x0600011B RID: 283 RVA: 0x000051BC File Offset: 0x000041BC
		[DataElement(AllowDbNull = true, Varchar = 200)]
		public string AllowedClasses
		{
			get
			{
				return this.m_allowedClasses;
			}
			set
			{
				this.m_allowedClasses = value;
				this.Dirty = true;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600011C RID: 284 RVA: 0x000051CC File Offset: 0x000041CC
		// (set) Token: 0x0600011D RID: 285 RVA: 0x000051D4 File Offset: 0x000041D4
		[DataElement(AllowDbNull = true)]
		public string ClassType
		{
			get
			{
				return this.m_classType;
			}
			set
			{
				this.m_classType = value;
				this.Dirty = true;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600011E RID: 286 RVA: 0x000051E4 File Offset: 0x000041E4
		// (set) Token: 0x0600011F RID: 287 RVA: 0x000051EC File Offset: 0x000041EC
		[DataElement(AllowDbNull = true)]
		public string XOffset
		{
			get
			{
				return this.m_xOffset;
			}
			set
			{
				this.m_xOffset = value;
				this.Dirty = true;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000120 RID: 288 RVA: 0x000051FC File Offset: 0x000041FC
		// (set) Token: 0x06000121 RID: 289 RVA: 0x00005204 File Offset: 0x00004204
		[DataElement(AllowDbNull = true)]
		public string YOffset
		{
			get
			{
				return this.m_yOffset;
			}
			set
			{
				this.m_yOffset = value;
				this.Dirty = true;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000122 RID: 290 RVA: 0x00005214 File Offset: 0x00004214
		// (set) Token: 0x06000123 RID: 291 RVA: 0x0000521C File Offset: 0x0000421C
		[DataElement(AllowDbNull = true)]
		public string ZoneID
		{
			get
			{
				return this.m_zoneID;
			}
			set
			{
				this.m_zoneID = value;
				this.Dirty = true;
			}
		}

		// Token: 0x0400004A RID: 74
		private int m_id;

		// Token: 0x0400004B RID: 75
		private string m_questName;

		// Token: 0x0400004C RID: 76
		private string m_startNPC;

		// Token: 0x0400004D RID: 77
		private ushort m_startRegionID;

		// Token: 0x0400004E RID: 78
		private string m_storyText;

		// Token: 0x0400004F RID: 79
		private string m_summary;

		// Token: 0x04000050 RID: 80
		private string m_acceptText;

		// Token: 0x04000051 RID: 81
		private string m_questGoals;

		// Token: 0x04000052 RID: 82
		private string m_goalType;

		// Token: 0x04000053 RID: 83
		private string m_goalRepeatNo;

		// Token: 0x04000054 RID: 84
		private string m_goaltargetName;

		// Token: 0x04000055 RID: 85
		private string m_goaltargetText;

		// Token: 0x04000056 RID: 86
		private int m_stepCount;

		// Token: 0x04000057 RID: 87
		private string m_finishNPC;

		// Token: 0x04000058 RID: 88
		private string m_goaladvanceText;

		// Token: 0x04000059 RID: 89
		private string m_collectItemTemplate;

		// Token: 0x0400005A RID: 90
		private ushort m_maxCount;

		// Token: 0x0400005B RID: 91
		private byte m_minLevel;

		// Token: 0x0400005C RID: 92
		private byte m_maxLevel;

		// Token: 0x0400005D RID: 93
		private long m_rewardMoney;

		// Token: 0x0400005E RID: 94
		private long m_rewardXP;

		// Token: 0x0400005F RID: 95
		private long m_rewardCLXP;

		// Token: 0x04000060 RID: 96
		private long m_rewardRP;

		// Token: 0x04000061 RID: 97
		private long m_rewardBP;

		// Token: 0x04000062 RID: 98
		private string m_optionalRewardItemTemplates;

		// Token: 0x04000063 RID: 99
		private string m_finalRewardItemTemplates;

		// Token: 0x04000064 RID: 100
		private string m_finishText;

		// Token: 0x04000065 RID: 101
		private string m_questDependency;

		// Token: 0x04000066 RID: 102
		private string m_allowedClasses;

		// Token: 0x04000067 RID: 103
		private string m_classType;

		// Token: 0x04000068 RID: 104
		private string m_xOffset;

		// Token: 0x04000069 RID: 105
		private string m_yOffset;

		// Token: 0x0400006A RID: 106
		private string m_zoneID;
	}
}
