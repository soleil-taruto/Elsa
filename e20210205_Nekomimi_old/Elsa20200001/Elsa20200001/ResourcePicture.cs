using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.GameCommons;

namespace Charlotte
{
	public class ResourcePicture
	{
		public DDPicture Dummy = DDPictureLoaders.Standard(@"dat\General\Dummy.png");
		public DDPicture WhiteBox = DDPictureLoaders.Standard(@"dat\General\WhiteBox.png");
		public DDPicture WhiteCircle = DDPictureLoaders.Standard(@"dat\General\WhiteCircle.png");

		public DDPicture Copyright = DDPictureLoaders.Standard(@"dat\Logo\Copyright.png");

		public DDPicture MessageFrame_Message = DDPictureLoaders.Standard(@"dat\フリー素材\空想曲線\Messageframe_29\material\01 message\message.png");
		public DDPicture MessageFrame_Button = DDPictureLoaders.Standard(@"dat\フリー素材\空想曲線\Messageframe_29\material\02 button\button.png");
		public DDPicture MessageFrame_Button2 = DDPictureLoaders.Standard(@"dat\フリー素材\空想曲線\Messageframe_29\material\02 button\button2.png");
		public DDPicture MessageFrame_Button3 = DDPictureLoaders.Standard(@"dat\フリー素材\空想曲線\Messageframe_29\material\02 button\button3.png");
		public DDPicture MessageFrame_Auto = DDPictureLoaders.Standard(@"dat\フリー素材\空想曲線\Messageframe_29\material\03 system_button\auto.png");
		public DDPicture MessageFrame_Auto2 = DDPictureLoaders.Standard(@"dat\フリー素材\空想曲線\Messageframe_29\material\03 system_button\auto2.png");
		public DDPicture MessageFrame_Load = DDPictureLoaders.Standard(@"dat\フリー素材\空想曲線\Messageframe_29\material\03 system_button\load.png");
		public DDPicture MessageFrame_Load2 = DDPictureLoaders.Standard(@"dat\フリー素材\空想曲線\Messageframe_29\material\03 system_button\load2.png");
		public DDPicture MessageFrame_Log = DDPictureLoaders.Standard(@"dat\フリー素材\空想曲線\Messageframe_29\material\03 system_button\log.png");
		public DDPicture MessageFrame_Log2 = DDPictureLoaders.Standard(@"dat\フリー素材\空想曲線\Messageframe_29\material\03 system_button\log2.png");
		public DDPicture MessageFrame_Menu = DDPictureLoaders.Standard(@"dat\フリー素材\空想曲線\Messageframe_29\material\03 system_button\menu.png");
		public DDPicture MessageFrame_Menu2 = DDPictureLoaders.Standard(@"dat\フリー素材\空想曲線\Messageframe_29\material\03 system_button\menu2.png");
		public DDPicture MessageFrame_Save = DDPictureLoaders.Standard(@"dat\フリー素材\空想曲線\Messageframe_29\material\03 system_button\save.png");
		public DDPicture MessageFrame_Save2 = DDPictureLoaders.Standard(@"dat\フリー素材\空想曲線\Messageframe_29\material\03 system_button\save2.png");
		public DDPicture MessageFrame_Skip = DDPictureLoaders.Standard(@"dat\フリー素材\空想曲線\Messageframe_29\material\03 system_button\skip.png");
		public DDPicture MessageFrame_Skip2 = DDPictureLoaders.Standard(@"dat\フリー素材\空想曲線\Messageframe_29\material\03 system_button\skip2.png");

		public DDPicture Title = DDPictureLoaders.Standard(@"dat\仮の素材\Title.png");
		public DDPicture Config = DDPictureLoaders.Standard(@"dat\仮の素材\Config.png");
		public DDPicture 田舎の風景 = DDPictureLoaders.Standard(@"dat\仮の素材\田舎の風景.png");

		public DDPicture[] TitleMenuItem_はじめから = new DDPicture[]
		{
			DDPictureLoaders.Standard(@"dat\image\title\button_start.png"),
			DDPictureLoaders.Standard(@"dat\image\title\button_start2.png"),
			DDPictureLoaders.Standard(@"dat\image\title\button_start3.png"),
		};
		public DDPicture[] TitleMenuItem_つづきから = new DDPicture[]
		{
			DDPictureLoaders.Standard(@"dat\image\title\button_load.png"),
			DDPictureLoaders.Standard(@"dat\image\title\button_load2.png"),
			DDPictureLoaders.Standard(@"dat\image\title\button_load3.png"),
		};
		public DDPicture[] TitleMenuItem_コンフィグ = new DDPicture[]
		{
			DDPictureLoaders.Standard(@"dat\image\title\button_config.png"),
			DDPictureLoaders.Standard(@"dat\image\title\button_config2.png"),
			DDPictureLoaders.Standard(@"dat\image\title\button_config3.png"),
		};
		public DDPicture[] TitleMenuItem_回想モード = new DDPicture[]
		{
			DDPictureLoaders.Standard(@"dat\image\title\button_replay.png"),
			DDPictureLoaders.Standard(@"dat\image\title\button_replay2.png"),
			DDPictureLoaders.Standard(@"dat\image\title\button_replay3.png"),
		};
		public DDPicture[] TitleMenuItem_CGモード = new DDPicture[]
		{
			DDPictureLoaders.Standard(@"dat\image\title\button_cg.png"),
			DDPictureLoaders.Standard(@"dat\image\title\button_cg2.png"),
			DDPictureLoaders.Standard(@"dat\image\title\button_cg3.png"),
		};
		public DDPicture[] TitleMenuItem_終了 = new DDPicture[]
		{
			DDPictureLoaders.Standard(@"dat\image\title\button_exit2.png"),
			DDPictureLoaders.Standard(@"dat\image\title\button_exit2.png"),
			DDPictureLoaders.Standard(@"dat\image\title\button_exit2.png"),
		};
	}
}
