using GymManagement.Tests.Integration.Abstractions.Fixtures;

namespace GymManagement.Tests.Integration.Abstractions;

public abstract class ControllerTestsBase
{
    //protected HttpClient _sut;
    protected WebAppFactoryFixture _webAppFactory;

    public ControllerTestsBase(WebAppFactoryFixture webAppFactory)
    {
        //// 1. CreateClient 메서드를 호출하면 IAppMarker 인스턴스를 생성합니다.
        //// 2. CreateClient N번 호출해도 IAppMarker 인스턴스는 1번만 생성합니다.
        //_sut = webAppFactory.CreateClient();
        _webAppFactory = webAppFactory;

        ////var _sut = webAppFactory.CreateClient(new WebApplicationFactoryClientOptions
        ////{
        ////    AllowAutoRedirect = false
        ////});
    }
}