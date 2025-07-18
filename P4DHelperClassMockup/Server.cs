namespace P4DHelperClassMockup;

public class Server
{
    public string Description { get; private set; }

    public string Name { get; private set; }

    public ServerType ServerType { get; private set; }

    public P4DVersion P4DVersion { get; private set; }

    public Server(ServerType servertype, P4DVersion p4dversion)
    {
        ServerType = servertype;
        P4DVersion = p4dversion;
        Description = $"Plant-4D {P4DVersion} {ServerType} Server";
    }

    public Server(ServerTypeAndP4DVersion servertypeAndp4dversion)
    {
        ServerType = BaseClass.GetServerTypeFromCombination(servertypeAndp4dversion);
        P4DVersion = BaseClass.GetP4DVersionFromCombination(servertypeAndp4dversion);
        Description = $"Plant-4D {P4DVersion} {ServerType} Server";
    }

    public Server(Server oldServer)
        : this(oldServer.ServerType, oldServer.P4DVersion)
    {
    }
}
