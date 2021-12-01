namespace SynologyDotNet
{
    internal interface ISynoClientConnectable
    {
        string[] GetApiNames();
        void SetClient(SynoClient client);
    }
}
