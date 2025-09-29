namespace PoshCode
{
    // Two rules for IPsMetadataSerializable implementations:
    // 1. Provide a public parameterless constructor
    // 2. Ensure FromPsMetadata can interpret the value produced by ToPsMetadata
    public interface IPsMetadataSerializable
    {
        string ToPsMetadata();
        void FromPsMetadata(string metadata);
    }
}
