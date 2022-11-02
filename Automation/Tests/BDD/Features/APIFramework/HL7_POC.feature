Feature: HL7_POC
	POC of HL7 parser and operations 

Scenario: Stock OMP Outbound - Verify MSH Segment
	Given hl7 file location C:\temp\hl7location
	And hl7 file prefix TEST_
	#And hl7 file wait 1 mins
	#When hl7 file location delete
	#Then assert hl7 file location is empty
	When method GET-HL7
	Then assert hl7 response MSH.2 is equal to ^~\&
	And assert hl7 response MSH.5 is equal to Stock OMP
	And assert hl7 response MSH.9.1 is equal to OMP
	And assert hl7 response MSH.9.2 is equal to O09
	And assert hl7 response MSH.11 is equal to P
	And assert hl7 response MSH.12 is equal to 2.5

Scenario: Stock OMP Outbound - Verify PID Segment
	Given hl7 file location C:\temp\hl7location
	And hl7 file prefix TEST_
	And hl7 file wait 1 mins
	#When hl7 file location delete
	#Then assert hl7 file location is empty
	When method GET-HL7
	Then assert hl7 response MSH.2 is equal to ^~\&
	And assert hl7 response MSH.5 is equal to Stock OMP
	And assert hl7 response MSH.9.1 is equal to OMP
	And assert hl7 response MSH.9.2 is equal to O09
	And assert hl7 response MSH.11 is equal to P
	And assert hl7 response MSH.12 is equal to 2.5