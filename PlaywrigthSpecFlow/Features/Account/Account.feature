﻿@ICreateAccountByAPI
@IDeleteAccountByAPI
Feature: Account

Add account and verify status
Scenario: Add a new account
	Then I get success status code from API
	Then I get message that account deleted after cleanup
