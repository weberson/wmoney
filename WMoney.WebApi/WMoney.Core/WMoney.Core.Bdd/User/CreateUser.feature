﻿Feature: CreateUser
	
Background: 
	Given the data context is fake
	And the user repository is fake containing:
	| UserId | Email                  | Password |
	| 1      | existent.user@mail.com | pass     |
	And the user core is ready

Scenario: Creates a user with success
	Given the user has the email "user@mail.com"
	And the user has the password 1234
	When the user core receives an user cration request
	Then the result should be an user with email "user@mail.com"
	And the result should be an user with password 1234
	And the user should be saved on data context user repository

Scenario: Creates a user that already exists
	Given the user has the email "existent.user@mail.com"
	And the user has the password 1234
	When the user core receives an user cration request
	Then an exception of type "System.DuplicateWaitObjectException" should be thrown