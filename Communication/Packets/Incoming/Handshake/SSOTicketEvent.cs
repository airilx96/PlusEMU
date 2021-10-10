using log4net;
using Plus.HabboHotel.GameClients;

namespace Plus.Communication.Packets.Incoming.Handshake
{
    public class SsoTicketEvent : IPacketEvent
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(SsoTicketEvent));

        public void Parse(GameClient session, ClientPacket packet)
        {
            if (session == null || session.GetHabbo() != null)
                return;

            string sso = packet.PopString();

            if (string.IsNullOrEmpty(sso) || sso.Length < 15)
            {
                log.Debug("Invalid SSO Ticket, disconnecting client");
                session.Disconnect();
                return;
            }

            session.TryAuthenticate(sso);
        }
    }
}