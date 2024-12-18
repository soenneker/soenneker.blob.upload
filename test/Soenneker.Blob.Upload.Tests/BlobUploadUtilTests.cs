using System.Threading.Tasks;
using Soenneker.Blob.Upload.Abstract;
using Soenneker.Tests.FixturedUnit;
using Xunit;


namespace Soenneker.Blob.Upload.Tests;

[Collection("Collection")]
public class BlobUploadUtilTests : FixturedUnitTest
{
    private readonly IBlobUploadUtil _util;

    public BlobUploadUtilTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
        _util = Resolve<IBlobUploadUtil>(true);
    }
}
