using Lextm.SharpSnmpLib;
using Lextm.SharpSnmpLib.Messaging;
using Lextm.SharpSnmpLib.Objects;
using Lextm.SharpSnmpLib.Pipeline;
using Lextm.SharpSnmpLib.Security;
using System;
using System.Net;
using ModbusTcpToSnmp.PCL.ViewModel.IoC;
using ModbusTcpToSnmp.PCL.ViewModel.Logging;

namespace ModbusTcpToSnmp.PCL.ViewModel
{
    public class SnmpServer : IDisposable
    {
        public SnmpServer()
        {
            throw new NotSupportedException("please use parameterized constructor instead");
        }
        public SnmpServer(SnmpServerArgs args)
        {
            this.args = args;

            // this.store.Add(new MySysObjectId());
            this.store.Add(new SysUpTime());
            this.store.Add(new SysContact());
            this.store.Add(new SysName());
            this.store.Add(new SysLocation());
            this.store.Add(new SysServices());
            this.store.Add(new SysORLastChange());
            this.store.Add(new SysORTable());
            this.store.Add(new IfNumber());
            this.store.Add(new IfTable());

            var users = new UserRegistry();

            var getv1 = new GetV1MessageHandler();
            var getv1Mapping = new HandlerMapping("v1", "GET", getv1);

            var getv23 = new GetMessageHandler();
            var getv23Mapping = new HandlerMapping("v2,v3", "GET", getv23);

            var setv1 = new SetV1MessageHandler();
            var setv1Mapping = new HandlerMapping("v1", "SET", setv1);

            var setv23 = new SetMessageHandler();
            var setv23Mapping = new HandlerMapping("v2,v3", "SET", setv23);

            var getnextv1 = new GetNextV1MessageHandler();
            var getnextv1Mapping = new HandlerMapping("v1", "GETNEXT", getnextv1);

            var getnextv23 = new GetNextMessageHandler();
            var getnextv23Mapping = new HandlerMapping("v2,v3", "GETNEXT", getnextv23);

            var getbulk = new GetBulkMessageHandler();
            var getbulkMapping = new HandlerMapping("v2,v3", "GETBULK", getbulk);

            var v1 = new Version1MembershipProvider(new OctetString("public"), new OctetString("public"));
            var v2 = new Version2MembershipProvider(new OctetString("public"), new OctetString("public"));
            var membership = new ComposedMembershipProvider(new IMembershipProvider[] { v1, v2 });
            var handlerFactory = new MessageHandlerFactory(new[]
             {
                         getv1Mapping,
                         getv23Mapping,
                         setv1Mapping,
                         setv23Mapping,
                         getnextv1Mapping,
                         getnextv23Mapping,
                         getbulkMapping
                     });

            var rollingLogger = ServiceLocator.Default.Resolve<RollingLogger>();

            var pipelineFactory = new SnmpApplicationFactory(rollingLogger, store, membership, handlerFactory);
            this.engine = new SnmpEngine(pipelineFactory, new Listener { Users = users }, new EngineGroup());
            this.engine.ExceptionRaised += OnSnmpServerException;
        }
        private void OnSnmpServerException(object sender, ExceptionRaisedEventArgs e)
        {
            Logger.LogException(e.Exception);
            throw e.Exception;
        }

        private readonly SnmpEngine engine;
        public readonly SnmpServerArgs args;

        private ObjectStore store = new ObjectStore();

        public void AddToStore(ScalarObject obj)
        {
            this.store.Add(obj);
        }

        public void StartServer()
        {
            if (this.engine.Active)
            {
                return;
            }

            try
            {
                this.engine.Listener.ClearBindings();
                this.engine.Listener.AddBinding(new IPEndPoint(this.args.IpAddress, this.args.Port));
                this.engine.Start();
            }
            catch (PortInUseException ex)
            {
                Logger.LogException(ex);
            }
        }
        public void StopServer()
        {
            if (this.engine.Active)
            {
                this.engine.Stop();
            }
        }

        public void Dispose()
        {
            if (this.engine != null)
            {
                this.engine.Dispose();
            }
        }
    }
}
