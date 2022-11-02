Feature: StockOutboundORU_MSH_Segment_Tests
	Tests to verify the MSH segment of the Stock Outbound ORU HL7 interface
	
	# CUSTOMER THAT USES THIS INTERFACE
	# DHHH (eCW Doylestown)

	Background: 
	Given var %vendorId% as 334 type int

@StockOutboundORU	@ApplicationID_416 @VendorId_334
Scenario: StockOutboundORU_SmokeTest_MSH_Segment_HH_With_vsb_vdefconfig4_value_OLD
	# AZ Test Case # - 383611
	#
	# ==== Pre-Req Start ====
	#
	Given var %serviceLineId% as 1 type int
	#
	# SQL - Get first id and branchcode of Branch with service line of 1
	#
	Given sql query select top 1 vsb_id, vsb_branchcode from VENDORS_SERVICELINES_BRANCHES where vsb_vid = %vendorId% and vsb_slid = %serviceLineId% and vsb_active = 'Y'
	When method SQLServer-SELECT
	#
	Given var %vsb_id% is equal to response.sqlresult.[0].vsb_id type string
	And var %branchName% is equal to response.sqlresult.[0].vsb_branchcode type string
	And var %vsb_vdefconfig4% = %branchName% + 1
	#
	# SQL - Update Branch to have a vsb_vdefconfig4 value of %vsb_vdefconfig4%
	#
	Given sql query UPDATE dbo.VENDORS_SERVICELINES_BRANCHES SET vsb_vdefconfig4='%vsb_vdefconfig4%' WHERE vsb_id=%vsb_id%
	When method SQLServer-UPDATE
	#
	# SQL - Get test client/order for branchid
	#
	Given request read sql file .\\Data\\SQLData\\StockOutboundORU\\GetClientInformation_ByBranchNameAndSLID.txt
	When method SQLServer-SELECT
	#
	Given var %orderId% is equal to response.sqlresult.[0].OrderId type string
	And var %episodeId% is equal to response.sqlresult.[0].EpisodeId type string
	And var %mrn% is equal to response.sqlresult.[0].MRN type string
	Then print var %episodeId% debug
	#
	# HL7 - Clear export directory
	#
	Given hl7 file location \\qbintsys111\g$\Archive\Orders2\StockOBORU\HCHB_QAODD
	When hl7 file location delete
	Then assert hl7 file location is empty
	#
	# SQL - get/set vt_vitid value from VENDOR_INTERFACE_TRANSACTIONS
	#
	Given sql query select vit_id from VENDOR_INTERFACE_TRANSACTIONS where vit_vid = 334 and vit_description = 'Physician Verbal Order Approval'
	When method SQLServer-SELECT
	#
	Given var %vt_vitid% is equal to response.sqlresult.[0].vit_id type int
	#
	# SQL - Add order to VENDOR_TRANSACTIONS table
	#
	Given sql read file .\\Data\\SQLData\\StockOutboundORU\\VENDOR_TRANSACTIONS_InsertOrder.txt
	When method SQLServer-INSERT
	#
	# SQL - Verify order is in VENDOR_TRANSACTIONS table and is status N
	#
	Given sql read file .\\Data\\SQLData\\StockOutboundORU\\VENDOR_TRANSACTIONS_GetDataByEpisodeAndEntityIdAndUser.txt
	When method SQLServer-SELECT
	Then assert json response sqlresult[0].processId is equal to N
	#
	# ==== Pre-Req End ====
	#
	# Wait for Intersystems Run
	Given wait 35000
	#
	# SQL - Verify order is in VENDOR_TRANSACTIONS table and is status Y
	#
	Given sql read file .\\Data\\SQLData\\StockOutboundORU\\VENDOR_TRANSACTIONS_GetDataByEpisodeAndEntityIdAndUser.txt
	When method SQLServer-SELECT
	Then assert json response sqlresult[0].processId is equal to Y
	And assert json response sqlresult[0].vendor_userName is equal to AUTOMATION
	And assert json response sqlresult[0].sessionId contains ORDERS2
	And assert json response sqlresult[0].statusMessage contains Message was successfully processed
	#
	# HL7 - Verify file's MSH has the correct values
	#
	# TODO: Need to assert the processDate in MSH7 and 10
	Given hl7 file location %TestRun_Resources.StockOutboundORU_DropLoc%
	When method GET-HL7-File
	Then assert hl7 response MSH.2 is equal to ^~\&
	And assert hl7 response MSH.5 is equal to eCW
	And assert hl7 response MSH.6 is equal to %vsb_vdefconfig4%
	And assert hl7 response MSH.9.1 is equal to ORU
	And assert hl7 response MSH.9.2 is equal to R01
	And assert hl7 response MSH.10 contains %mrn%
	And assert hl7 response MSH.11 is equal to P
	And assert hl7 response MSH.12 is equal to 2.5


@StockOutboundORU	@ApplicationID_416 @VendorId_334
Scenario: StockOutboundORU_SmokeTest_MSH_Segment_HOSP_With_vsb_vdefconfig4_value
	# AZ Test Case # - 383614
	# ==== Pre-Req Start ====
	#
	Given var %serviceLineId% as 2 type int
	#
	# SQL - Get first id and branchcode of Branch with service line of 1
	#
	Given sql query select top 1 vsb_id, vsb_branchcode from VENDORS_SERVICELINES_BRANCHES where vsb_vid = %vendorId% and vsb_slid = %serviceLineId% and vsb_active = 'Y'
	When method SQLServer-SELECT
	#
	Given var %vsb_id% is equal to response.sqlresult.[0].vsb_id type string
	And var %branchName% is equal to response.sqlresult.[0].vsb_branchcode type string
	And var %vsb_vdefconfig4% = %branchName% + 1
	#
	# SQL - Update Branch to have a vsb_vdefconfig4 value of %vsb_vdefconfig4%
	#
	Given sql query UPDATE dbo.VENDORS_SERVICELINES_BRANCHES SET vsb_vdefconfig4='%vsb_vdefconfig4%' WHERE vsb_id=%vsb_id%
	When method SQLServer-UPDATE
	#
	# SQL - Get test client/order for branchid
	#
	Given request read sql file .\\Data\\SQLData\\StockOutboundORU\\GetClientInformation_ByBranchNameAndSLID.txt
	When method SQLServer-SELECT
	#
	Given var %orderId% is equal to response.sqlresult.[0].OrderId type string
	And var %episodeId% is equal to response.sqlresult.[0].EpisodeId type string
	And var %mrn% is equal to response.sqlresult.[0].MRN type string
	Then print var %episodeId% debug
	#
	# HL7 - Clear export directory
	#
	Given hl7 file location \\qbintsys111\g$\Archive\Orders2\StockOBORU\HCHB_QAODD
	When hl7 file location delete
	Then assert hl7 file location is empty
	#
	# SQL - get/set vt_vitid value from VENDOR_INTERFACE_TRANSACTIONS
	#
	Given sql query select vit_id from VENDOR_INTERFACE_TRANSACTIONS where vit_vid = 334 and vit_description = 'Physician Verbal Order Approval'
	When method SQLServer-SELECT
	#
	Given var %vt_vitid% is equal to response.sqlresult.[0].vit_id type int
	#
	# SQL - Add order to VENDOR_TRANSACTIONS table
	#
	Given sql read file .\\Data\\SQLData\\StockOutboundORU\\VENDOR_TRANSACTIONS_InsertOrder.txt
	When method SQLServer-INSERT
	#
	# SQL - Verify order is in VENDOR_TRANSACTIONS table and is status N
	#
	Given sql read file .\\Data\\SQLData\\StockOutboundORU\\VENDOR_TRANSACTIONS_GetDataByEpisodeAndEntityIdAndUser.txt
	When method SQLServer-SELECT
	Then assert json response sqlresult[0].processId is equal to N
	#
	# ==== Pre-Req End ====
	#
	Given wait 35000
	#
	# SQL - Verify order is in VENDOR_TRANSACTIONS table and is status Y
	#
	Given sql read file .\\Data\\SQLData\\StockOutboundORU\\VENDOR_TRANSACTIONS_GetDataByEpisodeAndEntityIdAndUser.txt
	When method SQLServer-SELECT
	Then assert json response sqlresult[0].processId is equal to Y
	And assert json response sqlresult[0].vendor_userName is equal to AUTOMATION
	And assert json response sqlresult[0].sessionId contains ORDERS2
	And assert json response sqlresult[0].statusMessage contains Message was successfully processed
	#
	# HL7 - Verify file's MSH has the correct values
	#
	# TODO: Need to assert the processDate in MSH7 and 10
	Given hl7 file location %TestRun_Resources.StockOutboundORU_DropLoc%
	When method GET-HL7-File
	Then assert hl7 response MSH.2 is equal to ^~\&
	And assert hl7 response MSH.5 is equal to eCW
	And assert hl7 response MSH.6 is equal to %vsb_vdefconfig4%
	And assert hl7 response MSH.9.1 is equal to ORU
	And assert hl7 response MSH.9.2 is equal to R01
	And assert hl7 response MSH.10 contains %mrn%
	And assert hl7 response MSH.11 is equal to P
	And assert hl7 response MSH.12 is equal to 2.5


@StockOutboundORU	@ApplicationID_416 @VendorId_334
Scenario: StockOutboundORU_SmokeTest_MSH_Segment_PRIVATEDUTY_With_vsb_vdefconfig4_value
	# AZ Test Case # - 383616
	# ==== Pre-Req Start ====
	#
	Given var %serviceLineId% as 3 type int
	#
	# SQL - Get first id and branchcode of Branch with service line of 1
	#
	Given sql query select top 2 vsb_id, vsb_branchcode from VENDORS_SERVICELINES_BRANCHES where vsb_vid = %vendorId% and vsb_slid = %serviceLineId% and vsb_active = 'Y'
	When method SQLServer-SELECT
	#
	Given var %vsb_id% is equal to response.sqlresult.[1].vsb_id type string
	And var %branchName% is equal to response.sqlresult.[1].vsb_branchcode type string
	And var %vsb_vdefconfig4% = %branchName% + 1
	#
	# SQL - Update Branch to have a vsb_vdefconfig4 value of %vsb_vdefconfig4%
	#
	Given sql query UPDATE dbo.VENDORS_SERVICELINES_BRANCHES SET vsb_vdefconfig4='%vsb_vdefconfig4%' WHERE vsb_id=%vsb_id%
	When method SQLServer-UPDATE
	#
	# SQL - Get test client/order for branchid
	#
	Given request read sql file .\\Data\\SQLData\\StockOutboundORU\\GetClientInformation_ByBranchNameAndSLID.txt
	When method SQLServer-SELECT
	#
	Given var %orderId% is equal to response.sqlresult.[0].OrderId type string
	And var %episodeId% is equal to response.sqlresult.[0].EpisodeId type string
	And var %mrn% is equal to response.sqlresult.[0].MRN type string
	Then print var %episodeId% debug
	#
	# HL7 - Clear export directory
	#
	Given hl7 file location \\qbintsys111\g$\Archive\Orders2\StockOBORU\HCHB_QAODD
	When hl7 file location delete
	Then assert hl7 file location is empty
	#
	# SQL - get/set vt_vitid value from VENDOR_INTERFACE_TRANSACTIONS
	#
	Given sql query select vit_id from VENDOR_INTERFACE_TRANSACTIONS where vit_vid = 334 and vit_description = 'Physician Verbal Order Approval'
	When method SQLServer-SELECT
	#
	Given var %vt_vitid% is equal to response.sqlresult.[0].vit_id type int
	#
	# SQL - Add order to VENDOR_TRANSACTIONS table
	#
	Given sql read file .\\Data\\SQLData\\StockOutboundORU\\VENDOR_TRANSACTIONS_InsertOrder.txt
	When method SQLServer-INSERT
	#
	# SQL - Verify order is in VENDOR_TRANSACTIONS table and is status N
	#
	Given sql read file .\\Data\\SQLData\\StockOutboundORU\\VENDOR_TRANSACTIONS_GetDataByEpisodeAndEntityIdAndUser.txt
	When method SQLServer-SELECT
	Then assert json response sqlresult[0].processId is equal to N
	#
	# ==== Pre-Req End ====
	#
	Given wait 30000
	#
	# SQL - Verify order is in VENDOR_TRANSACTIONS table and is status Y
	#
	Given sql read file .\\Data\\SQLData\\StockOutboundORU\\VENDOR_TRANSACTIONS_GetDataByEpisodeAndEntityIdAndUser.txt
	When method SQLServer-SELECT
	Then assert json response sqlresult[0].processId is equal to Y
	And assert json response sqlresult[0].vendor_userName is equal to AUTOMATION
	And assert json response sqlresult[0].sessionId contains ORDERS2
	And assert json response sqlresult[0].statusMessage contains Message was successfully processed
	#
	# HL7 - Verify file's MSH has the correct values
	#
	# TODO: Need to assert the processDate in MSH7 and 10
	Given hl7 file location %TestRun_Resources.StockOutboundORU_DropLoc%
	When method GET-HL7-File
	Then assert hl7 response MSH.2 is equal to ^~\&
	And assert hl7 response MSH.5 is equal to eCW
	And assert hl7 response MSH.6 is equal to %vsb_vdefconfig4%
	And assert hl7 response MSH.9.1 is equal to ORU
	And assert hl7 response MSH.9.2 is equal to R01
	And assert hl7 response MSH.10 contains %mrn%
	And assert hl7 response MSH.11 is equal to P
	And assert hl7 response MSH.12 is equal to 2.5

@StockOutboundORU	@ApplicationID_416 @VendorId_334
Scenario: StockOutboundORU_SmokeTest_MSH_Segment_PRIVATEDUTY_Without_vsb_vdefconfig4_value
	# AZ Test Case # - 383617
	# ==== Pre-Req Start ====
	#
	Given var %serviceLineId% as 3 type int
	#
	# SQL - Get first id and branchcode of Branch with service line of 1
	#
	Given sql query select top 2 vsb_id, vsb_branchcode from VENDORS_SERVICELINES_BRANCHES where vsb_vid = %vendorId% and vsb_slid = %serviceLineId% and vsb_active = 'Y'
	When method SQLServer-SELECT
	#
	Given var %vsb_id% is equal to response.sqlresult.[1].vsb_id type string
	And var %branchName% is equal to response.sqlresult.[1].vsb_branchcode type string
	And var %vsb_vdefconfig4% = %branchName% + 1
	#
	# SQL - Update Branch to have a vsb_vdefconfig4 value of null
	#
	Given request sqlserver UPDATE dbo.VENDORS_SERVICELINES_BRANCHES SET vsb_vdefconfig4=NULL WHERE vsb_id=%vsb_id%
	When method SQLServer-UPDATE
	#
	# SQL - Get test client/order for branchid
	#
	Given request read sql file .\\Data\\SQLData\\StockOutboundORU\\GetClientInformation_ByBranchNameAndSLID.txt
	When method SQLServer-SELECT
	#
	Given var %orderId% is equal to response.sqlresult.[0].OrderId type string
	And var %episodeId% is equal to response.sqlresult.[0].EpisodeId type string
	And var %mrn% is equal to response.sqlresult.[0].MRN type string
	Then print var %episodeId% debug
	#
	# HL7 - Clear export directory
	#
	Given hl7 file location \\qbintsys111\g$\Archive\Orders2\StockOBORU\HCHB_QAODD
	When hl7 file location delete
	Then assert hl7 file location is empty
	#
	# SQL - get/set vt_vitid value from VENDOR_INTERFACE_TRANSACTIONS
	#
	Given sql query select vit_id from VENDOR_INTERFACE_TRANSACTIONS where vit_vid = 334 and vit_description = 'Physician Verbal Order Approval'
	When method SQLServer-SELECT
	#
	Given var %vt_vitid% is equal to response.sqlresult.[0].vit_id type int
	#
	# SQL - Add order to VENDOR_TRANSACTIONS table
	#
	Given sql read file .\\Data\\SQLData\\StockOutboundORU\\VENDOR_TRANSACTIONS_InsertOrder.txt
	When method SQLServer-INSERT
	#
	# SQL - Verify order is in VENDOR_TRANSACTIONS table and is status N
	#
	Given sql read file .\\Data\\SQLData\\StockOutboundORU\\VENDOR_TRANSACTIONS_GetDataByEpisodeAndEntityIdAndUser.txt
	When method SQLServer-SELECT
	Then assert json response sqlresult[0].processId is equal to N
	#
	# ==== Pre-Req End ====
	#
	Given wait 30000
	#
	# SQL - Verify order is in VENDOR_TRANSACTIONS table and is status Y
	#
	Given sql read file .\\Data\\SQLData\\StockOutboundORU\\VENDOR_TRANSACTIONS_GetDataByEpisodeAndEntityIdAndUser.txt
	When method SQLServer-SELECT
	Then assert json response sqlresult[0].processId is equal to Y
	And assert json response sqlresult[0].vendor_userName is equal to AUTOMATION
	And assert json response sqlresult[0].sessionId contains ORDERS2
	And assert json response sqlresult[0].statusMessage contains Message was successfully processed
	#
	# HL7 - Verify file's MSH has the correct values
	#
	# TODO: Need to assert the processDate in MSH7 and 10
	Given hl7 file location %TestRun_Resources.StockOutboundORU_DropLoc%
	When method GET-HL7-File
	Then assert hl7 response MSH.2 is equal to ^~\&
	And assert hl7 response MSH.5 is equal to eCW
	And assert hl7 response MSH.6 should be empty
	And assert hl7 response MSH.9.1 is equal to ORU
	And assert hl7 response MSH.9.2 is equal to R01
	And assert hl7 response MSH.10 contains %mrn%
	And assert hl7 response MSH.11 is equal to P
	And assert hl7 response MSH.12 is equal to 2.5


	@StockOutboundORU	@ApplicationID_416 @VendorId_334
Scenario: StockOutboundORU_SmokeTest_MSH_Segment_HH_With_vsb_vdefconfig4_value
	# AZ Test Case # - 383611
	#
	# TEST SETUP - Start
	# --------------------
	Given var %serviceLineId% as 1 type int
	#
	# SQL - GetClientInformation_ByBranchNameAndSLID.txt - Get Client Info
	#
	Given request read sql file .\\Data\\SQLData\\StockOutboundORU\\GetClientInformation_ByBranchNameAndSLID.txt
	When method SQLServer-SELECT
	#
	Given var %orderId% is equal to response.sqlresult.[0].OrderId type string
	And var %episodeId% is equal to response.sqlresult.[0].EpisodeId type string
	And var %mrn% is equal to response.sqlresult.[0].MRN type string
	And var %branchCode% is equal to response.sqlresult.[0].BranchCode type string
	Then print var %episodeId% debug
	#
	# SQL - VENDOR_SERVICELINES_BRANCHES table - get branch code
	#
	Given sql query select top 1 vsb_id from VENDORS_SERVICELINES_BRANCHES where vsb_branchcode = '%branchCode%' and vsb_vid = %vendorId%
	When method SQLServer-SELECT
	#
	Given var %vsb_id% is equal to response.sqlresult.[0].vsb_id type string
	And var %vsb_vdefconfig4% = %branchCode% + 1
	#
	# SQL - VENDOR_SERVICELINES_BRANCHES table - Update Branch to have a vsb_vdefconfig4 value of %vsb_vdefconfig4%
	#
	Given sql query UPDATE dbo.VENDORS_SERVICELINES_BRANCHES SET vsb_vdefconfig4='%vsb_vdefconfig4%' WHERE vsb_id=%vsb_id%
	When method SQLServer-UPDATE
	# Might be duplicate
	# SQL - Get test client/order for branchid
	#
	Given request read sql file .\\Data\\SQLData\\StockOutboundORU\\GetClientInformation_ByBranchNameAndSLID.txt
	When method SQLServer-SELECT
	#
	Given var %orderId% is equal to response.sqlresult.[0].OrderId type string
	And var %episodeId% is equal to response.sqlresult.[0].EpisodeId type string
	And var %mrn% is equal to response.sqlresult.[0].MRN type string
	Then print var %episodeId% debug
	#
	# HL7 - Clear export directory
	#
	Given hl7 file location %TestRun_Resources.StockOutboundORU_DropLoc%
	When hl7 file location delete
	Then assert hl7 file location is empty
	#
	# SQL - VENDOR_INTERFACE_TRANSACTIONS table - get/set vt_vitid value
	#
	Given sql query select vit_id from VENDOR_INTERFACE_TRANSACTIONS where vit_vid = 334 and vit_description = 'Physician Verbal Order Approval'
	When method SQLServer-SELECT
	#
	Given var %vt_vitid% is equal to response.sqlresult.[0].vit_id type int
	#
	# SQL - VENDOR_TRANSACTIONS table - Add mocked order
	#
	Given sql read file .\\Data\\SQLData\\StockOutboundORU\\VENDOR_TRANSACTIONS_InsertOrder.txt
	When method SQLServer-INSERT
	#
	# SQL - VENDOR_TRANSACTIONS table - Verify mocked order was added and is status N
	#
	Given sql read file .\\Data\\SQLData\\StockOutboundORU\\VENDOR_TRANSACTIONS_GetDataByEpisodeAndEntityIdAndUser.txt
	When method SQLServer-SELECT
	Then assert json response sqlresult[0].processId is equal to N
	#
	# Wait for Intersystems Run
	Given wait 35000
	#
	# SQL - VENDOR_TRANSACTIONS table - Verify mocked order is status Y
	#
	Given sql read file .\\Data\\SQLData\\StockOutboundORU\\VENDOR_TRANSACTIONS_GetDataByEpisodeAndEntityIdAndUser.txt
	When method SQLServer-SELECT
	Then assert json response sqlresult[0].processId is equal to Y
	And assert json response sqlresult[0].vendor_userName is equal to AUTOMATION
	And assert json response sqlresult[0].sessionId contains ORDERS2
	And assert json response sqlresult[0].statusMessage contains Message was successfully processed
	#
	# HL7 - Verify file's MSH has the correct values
	#
	# TODO: Need to assert the processDate in MSH7 and 10
	#Given hl7 file location %TestRun_Resources.StockOutboundORU_DropLoc%
	When method GET-HL7-File
	Then assert hl7 response MSH.2 is equal to ^~\&
	And assert hl7 response MSH.5 is equal to eCW
	And assert hl7 response MSH.6 is equal to %vsb_vdefconfig4%
	And assert hl7 response MSH.9.1 is equal to ORU
	And assert hl7 response MSH.9.2 is equal to R01
	And assert hl7 response MSH.10 contains %mrn%
	And assert hl7 response MSH.11 is equal to P
	And assert hl7 response MSH.12 is equal to 2.5