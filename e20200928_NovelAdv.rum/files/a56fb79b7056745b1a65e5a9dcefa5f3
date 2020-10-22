using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class HTTPServer : SockServer
	{
		public Action<HTTPServerChannel> HTTPConnected = channel => { };

		// <---- prm

		public HTTPServer()
		{
			PortNo = 80;
			Connected = channel => HandleDam.Section(hDam =>
			{
				HTTPServerChannel hsChannel = new HTTPServerChannel();

				hsChannel.Channel = channel;
				hsChannel.RecvRequest();
				hsChannel.HDam = hDam;

				HTTPConnected(hsChannel);

				hsChannel.HDam = null;
				hsChannel.SendResponse();
			});
		}
	}
}
