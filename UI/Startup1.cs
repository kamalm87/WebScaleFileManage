﻿using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using log4net;
using log4net.Config;
[assembly: OwinStartup(typeof(WebApiTest.Startup1))]

namespace WebApiTest
{
	public class Startup1
	{
        private static readonly ILog log = LogManager.GetLogger(typeof(Startup1));

        public void Configuration(IAppBuilder app)
		{
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
            var a = app;
		}
	}
}
