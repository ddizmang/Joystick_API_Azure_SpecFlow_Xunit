Feature: MSH_Segment_Tests
	Verify the STOCK fields in the Stock Outbound ADT's MSH Segment

@Stock_Outbound_ADT_MSH
Scenario: Verify STOCK MSH fields
	#
	# TEST SETUP - Start
	# --------------------
	#
	# HL7 - Clear export directory
	#
	Given hl7 file location \\qbintsys111\g$\Archive\Demographics3\StandardADT\Playmaker\
	When hl7 file location delete
	Then assert hl7 file location is empty
	#
	# SQL - VENDORS table - Enable Playmaker in DB
	#
	Given sql query update vendors set v_active = 'Y'where v_id = 431
	When method SQLServer-UPDATE
	#
	# Wait for 10 seconds
	Given wait 10000
	#
	Given var %orderId% is equal to 41725 type string
	#
	# SQL - VENDOR_TRANSACTIONS table - Add mocked order
	#
	Given sql read file .\\Data\\SQLData\\StockOutboundOMP\\VENDOR_TRANSACTIONS_InsertOrder.txt
	When method SQLServer-INSERT
	#
	# Wait for 30 seconds
	Given wait 30000
	# --------------------
	# TEST SETUP - End
	#
	#
	# TEST - Start
	# --------------------
	Given hl7 file location \\qbintsys111\g$\Archive\Demographics3\StandardADT\Playmaker\
	When method GET-HL7-File
	Then assert hl7 response MSH.2 is equal to ^~\&
	And assert hl7 response MSH.9.1 is equal to ADT
	And assert hl7 response MSH.9.2 is equal to A08
	And assert hl7 response MSH.11 is equal to T
	And assert hl7 response MSH.12 is equal to 2.5
	# --------------------
	# TEST - End
	#
	# TEST CLEAN UP - Start
	# --------------------
	#
	# SQL - VENDORS table - Enable Playmaker in DB
	#
	Given sql query update vendors set v_active = 'N'where v_id = 431
	When method SQLServer-UPDATE
	# --------------------
	# TEST CLEAN UP - End