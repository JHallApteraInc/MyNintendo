Feature: Login
	As a Nintendo gamer
	I want to login to my nintendo account
	so I can access resources

@Login
Scenario: Get Rewards
	Given I am on the My Nintendo site
	When I sign in
	Then I am logged into my account
	And I can collect my Rewards