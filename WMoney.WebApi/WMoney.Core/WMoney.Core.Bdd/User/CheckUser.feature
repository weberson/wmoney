Feature: CheckUser
	
Background: 
	Given the data context is fake
	And the user repository is fake containing:
	| UserId | Email         | Password |
	| 1      | user@mail.com | 1234     |
	And the user core is ready

Scenario: Check user with success
	Given the user has the email "user@mail.com"
	And the user has the password 1234
	When the user core receives an user check request
	Then the result should be "true"

Scenario: Check user with invalid password
	Given the user has the email "user@mail.com"
	And the user has the password abcd
	When the user core receives an user check request
	Then the result should be "false"

Scenario: Check user with unregistered email
	Given the user has the email "unregistered.user@mail.com"
	And the user has the password 1234
	When the user core receives an user check request
	Then an exception of type "System.ArgumentException" should be thrown
