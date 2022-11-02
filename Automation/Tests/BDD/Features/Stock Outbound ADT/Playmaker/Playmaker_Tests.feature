Feature: Playmaker_Tests
	Verify Playmaker specific areas in Stock Outbound ADT

@Stock_Outbound_ADT_Customer_Specific
Scenario: Verify MSH.5 Contains Playmaker
	Given hl7 file location \\qbintsys111\g$\Archive\Demographics3\StandardADT\Playmaker
	When method GET-HL7-File
	Then assert hl7 response MSH.5 is equal to Playmaker

Scenario: Verify MSH.5 Contains Playmaker DEMO
	Given hl7 file location \\qbintsys111\g$\Archive\Demographics3\StandardADT\Playmaker
	When method GET-HL7-File
	Then assert hl7 response MSH.2 is equal to ^~\
	And assert hl7 response MSH.9.1 is equal to ADT
	And assert hl7 response MSH.9.2 is equal to A08
	And assert hl7 response MSH.11 is equal to T
	And assert hl7 response MSH.12 is equal to 2.5
	And assert hl7 response PID1 is equal to 2.5