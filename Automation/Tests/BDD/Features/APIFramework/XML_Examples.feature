Feature: XML_Examples

Scenario: XML_POC_Test
	Given xml file location .\\Data\\XmlData\\TestFiles
	#And xml file prefix TEST_
	#And xml file wait 0 mins
	And var %id% as 10 type int
	#When xml file location delete
	#Then assert xml file location is empty
	#
	When method GET-XML-File
	Then assert xml response Students.Student.[0].@id is equal to %id%
	And assert xml response Students.Student.[0].@id contains %id%