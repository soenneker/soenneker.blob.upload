using Soenneker.Blob.Upload.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Blob.Upload.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public class BlobUploadUtilTests : HostedUnitTest
{
    private readonly IBlobUploadUtil _util;

    public BlobUploadUtilTests(Host host) : base(host)
    {
        _util = Resolve<IBlobUploadUtil>(true);
    }

    [Test]
    public void Default()
    {

    }
}
