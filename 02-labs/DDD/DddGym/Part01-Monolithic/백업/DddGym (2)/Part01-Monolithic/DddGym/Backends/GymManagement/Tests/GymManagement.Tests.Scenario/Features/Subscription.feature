Feature: Subscription

Subscription은 등급이 허용된 개수보다 많은 Gym을 추가할 수 없다.
A subscription cannot have more gyms than the subscription allows

# @tag1
Scenario: Basic 등급 – 최대 2개 Gym 제한
	Given 사용자가 Basic 등급의 Subscription을 가지고 있다.
    And 이 구독 등급은 최대 2개의 Gym까지 허용한다.
    And 현재 2개의 Gym이 이미 등록되어 있다.

	When 사용자가 새로운 Gym을 Subscription에 추가하려고 시도한다.
	
    Then 시스템은 Gym 추가를 거부한다.
    And 사용자에게 "허용된 Gym 개수를 초과했습니다"라는 오류 메시지를 표시한다.
