Feature: Demoqa Practise Form
	This is a suite of tests to test the functionality of the Student Registration Form on the demoqa website. Url: https://demoqa.com/automation-practice-form

Background:
	# I recognise that it would be quicker to clear the page and keep it open between each scenario
	Given I have navigated to "https://demoqa.com/automation-practice-form"

@happyPath @positive
Scenario: 010 Valid Mandatory Data Should Yield Popup Displaying Sent Data
	When I enter student details as follows
		| Field         | Value       |
		| First Name    | b           |
		| Last Name     | a           |
		| Email         | blah@me.com |
		| Gender        | Male        |
		| Mobile Number | 1111111111  |
	And I press the submit button
	Then the popup appears
	And the popup title is "Thanks for submitting the form"
	And the popup screen table is as follows
		| Label          | Values      |
		| Student Name   | b a         |
		| Student Email  | blah@me.com |
		| Gender         |             |
		| Mobile         | 1111111111  |
		| Subjects       |             |
		| Hobbies        |             |
		| Picture        |             |
		| Address        |             |
		| State and City |             |

@happyPath @negative
Scenario: 020 Invalid Data Should Yield Validation Errors
	When I press the submit button
	Then the popup does not appear
	And the field border colours are as follows
		| Field           | Value |
		| First Name      | Red   |
		| Last Name       | Red   |
		| Email           | Green |
		| Gender          | Red   |
		| Mobile Number   | Red   |
		| Date of Birth   | Green |
		| Hobbies         | Green |
		| Current Address | Green |

@positive
Scenario: 030 All Fields Valid Should Yield Popup Displaying Sent Data
	When I enter student details as follows
		| Field         | Value       |
		| First Name    | blah        |
		| Last Name     | blah        |
		| Email         | blah@me.com |
		| Gender        | Male        |
		| Mobile Number | 1111111111  |
		| Date of Birth | 27 Nov 1992 |
		| Subjects      | English     |
		| Hobbies       | Music       |
		| State         | NCR         |
		| City          | Delhi       |
	And I upload a picture from file "Data\Pictures\Picture1.jpg"
	And I enter a current address as follows
		| Address       |
		| 123 On a Road |
		| Town          |
		| City          |
		| TE9 9WD       |
	And I press the submit button
	Then the popup appears
	And the popup title is "Thanks for submitting the form"
	And the popup screen table is as follows
		| Label          | Values                          |
		| Student Name   | blah blah                       |
		| Student Email  | blah@me.com                     |
		| Gender         |                                 |
		| Mobile         | 1111111111                      |
		| Date of Birth  | 27 November,1992                |
		| Subjects       | English                         |
		| Hobbies        |                                 |
		| Picture        | Picture1.jpg                    |
		| Address        | 123 On a Road Town City TE9 9WD |
		| State and City | NCR Delhi                       |

@positive
Scenario: 040 All Fields Except Address Entered Should Yield Popup Displaying Sent Data
	When I enter student details as follows
		| Field         | Value                   |
		| First Name    | David                   |
		| Last Name     | Davis                   |
		| Email         | david@davidjdavis.co.uk |
		| Gender        | Male                    |
		| Mobile Number | 2222222222              |
		| Date of Birth | 13 May 1993             |
		| Subjects      | Computer Science,Maths  |
		| Hobbies       | Music                   |
		| State         | NCR                     |
		| City          | Delhi                   |
	And I upload a picture from file "Data\Pictures\Picture1.jpg"
	And I press the submit button
	Then the popup appears
	And the popup title is "Thanks for submitting the form"
	And the popup screen table is as follows
		| Label          | Values                  |
		| Student Name   | David Davis             |
		| Student Email  | david@davidjdavis.co.uk |
		| Gender         |                         |
		| Mobile         | 2222222222              |
		| Date of Birth  | 13 May,1993             |
		| Subjects       | Computer Science, Maths |
		| Hobbies        |                         |
		| Picture        | Picture1.jpg            |
		| Address        |                         |
		| State and City | NCR Delhi               |

@negative
Scenario: 050 Valid Mandatory Plus Invalid Email Yields Validation Error On Email
	When I enter student details as follows
		| Field         | Value      |
		| First Name    | David      |
		| Last Name     | Davis      |
		| Email         | 2          |
		| Gender        | Male       |
		| Mobile Number | 2222222222 |
	And I press the submit button
	Then the popup does not appear
	And the field border colours are as follows
		| Field         | Value |
		| First Name    | Green |
		| Last Name     | Green |
		| Email         | Red   |
		| Gender        | Green |
		| Mobile Number | Green |

@negative
Scenario: 060 Valid Mandatory Plus Invalid Mobile Number Yields Validation Error On Mobile Number
	When I enter student details as follows
		| Field         | Value    |
		| First Name    | David    |
		| Last Name     | Davis    |
		| Email         | me@me.me |
		| Gender        | Male     |
		| Mobile Number | 1        |
	And I press the submit button
	Then the popup does not appear
	And the field border colours are as follows
		| Field         | Value |
		| First Name    | Green |
		| Last Name     | Green |
		| Email         | Green |
		| Gender        | Green |
		| Mobile Number | Red   |

@positive
Scenario: 070 Fill Student Form with Valid Data From Csv Yields No Errors
	When I load student test data
	And I fill in the screen with test data
	And I press the submit button
	Then the popup appears
	And the popup title is "Thanks for submitting the form"
	And the popup screen table is as follows
		| Label          | Values          |
		| Student Name   | Hi My           |
		| Student Email  | name@is.bob     |
		| Gender         |                 |
		| Mobile         | 3333333333      |
		| Date of Birth  | 12 January,1955 |
		| Subjects       |                 |
		| Hobbies        |                 |
		| Picture        |                 |
		| Address        |                 |
		| State and City |                 |

@positive
Scenario: 080 Check that field values remain on screen after entering
	When I enter student details as follows
		| Field         | Value       |
		| First Name    | blah        |
		| Last Name     | blah        |
		| Email         | blah@me.com |
		| Gender        | Male        |
		| Mobile Number | 1111111111  |
		| Date of Birth | 27 Nov 1992 |
		| Subjects      | English     |
		| Hobbies       | Music       |
		| State         | NCR         |
		| City          | Delhi       |
	And I enter a current address as follows
		| Address       |
		| 123 On a Road |
		| Town          |
		| City          |
		| TE9 9WD       |
	Then the student details on the form are as follows
		| Field         | Value       |
		| First Name    | blah        |
		| Last Name     | blah        |
		| Email         | blah@me.com |
		| Gender        | Male        |
		| Mobile Number | 1111111111  |
		| Date of Birth | 27 Nov 1992 |
		| Subjects      | English     |
		| Hobbies       | Music       |
		| State         | NCR         |
		| City          | Delhi       |
	And the current address field is as follows
		| Address       |
		| 123 On a Road |
		| Town          |
		| City          |
		| TE9 9WD       |