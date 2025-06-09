using Reqnroll;

namespace GymManagement.Tests.Scenario.Features;

[Binding]
public class SubscriptionStepDefinitions
{
    [Given("사용자가 Basic 등급의 Subscription을 가지고 있다.")]
    public void Given사용자가Basic등급의Subscription을가지고있다_()
    {
    }

    [Given("이 구독 등급은 최대 {int}개의 Gym까지 허용한다.")]
    public void Given이구독등급은최대개의Gym까지허용한다_(int p0)
    {
    }

    [Given("현재 {int}개의 Gym이 이미 등록되어 있다.")]
    public void Given현재개의Gym이이미등록되어있다_(int p0)
    {
    }

    [When("사용자가 새로운 Gym을 Subscription에 추가하려고 시도한다.")]
    public void When사용자가새로운Gym을Subscription에추가하려고시도한다_()
    {
    }

    [Then("시스템은 Gym 추가를 거부한다.")]
    public void Then시스템은Gym추가를거부한다_()
    {
    }

    [Then("사용자에게 {string}라는 오류 메시지를 표시한다.")]
    public void Then사용자에게라는오류메시지를표시한다_(string p0)
    {
    }
}
