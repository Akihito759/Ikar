using DataClient.Interfaces;
using DataClient.Mocks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading;
using Unity;

namespace DataClient
{
    public class IoCContainer
    {
        public IUnityContainer container;

        public static IoCContainer CrateContainer()
        {
            var container = new UnityContainer();
            container.RegisterType<ICommunication,SignalRClient >();
            container.RegisterType<IGridEyeReader, GridEyeMock>();
            //container.RegisterType<IGridEyeReader, I2CReader>();
            container.RegisterType<ILogger, ConsoleLogger>();



            return new IoCContainer { container = container};
        }
    }
}
