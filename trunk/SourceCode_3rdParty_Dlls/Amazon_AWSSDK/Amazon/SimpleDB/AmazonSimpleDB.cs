namespace Amazon.SimpleDB
{
    using Amazon.SimpleDB.Model;
    using System;

    public interface AmazonSimpleDB : IDisposable
    {
        BatchPutAttributesResponse BatchPutAttributes(BatchPutAttributesRequest request);
        CreateDomainResponse CreateDomain(CreateDomainRequest request);
        DeleteAttributesResponse DeleteAttributes(DeleteAttributesRequest request);
        DeleteDomainResponse DeleteDomain(DeleteDomainRequest request);
        DomainMetadataResponse DomainMetadata(DomainMetadataRequest request);
        GetAttributesResponse GetAttributes(GetAttributesRequest request);
        ListDomainsResponse ListDomains(ListDomainsRequest request);
        PutAttributesResponse PutAttributes(PutAttributesRequest request);
        SelectResponse Select(SelectRequest request);
    }
}

