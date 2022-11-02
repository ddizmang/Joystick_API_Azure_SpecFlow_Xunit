Feature: WorkerServicePOC
	# WIKI Validation Page
	# https://dev.azure.com/hchb/HCHB/_wiki/wikis/HCHB.wiki/58836/VendorWorkerService
	# https://dev.azure.com/hchb/HCHB/_wiki/wikis/HCHB.wiki/53274/Worker-Interface-JSON-Spec


Scenario: WorkerServicePOC_v3_Dev_GET
	#
	# POST - Get BearerToken
	#
	Given api url %testrun_resources.idpauthurl%
	And header Content-Type = application/x-www-form-urlencoded
	And form content grant_type value %IDPGrantType%
	And form content client_id value %IDPClientId%
	And form content scope value %IDPScope%
	And form content resource_security_id value %IDPResourceSecurityId%
	And form content agency_secret value %IDPAgencySecret%
	When method POST
	Then assert api response status is equal to 200
	# Set %bearer% 
	Given var %bearer% is equal to response.access_token type string
	Then print var %bearer% debug
	#
	# TEST SETUP - Start
	# --------------------
	#
	# SQL - GetWorkerInfo.txt - Get latest worker id from workers table
	#
	Given request read sql file .\\Data\\SQLData\\WorkerService_v2_JSON\\GetWorkerInfo.txt
	When method sqlserver-select
	#
	Given var %workerId% is equal to response.sqlresult.[0].worker.id type string
	# --------------------
	# TEST SETUP - End
	#
	# TEST - Start
	# --------------------
	#
	# GET - Worker by workerid
	#
	Given api url %testrun_resources.worker_v3_url%
	And path Agent
	And path %workerId%
	And header Content-Type = application/json
	And header Authorization = Bearer %bearer%
	When method GET
	Then assert json response worker.id is equal to %workerId%
	And assert json response status.status is equal to Success
	And assert json response status.message is equal to Request is successful
	And assert json response worker.workerBase.workerId is equal to %workerId%
	#
	# --------------------
	# TEST - End

Scenario: WorkerServicePOC_v3_Dev_GENERATE_JSON_POST
	#
	# POST - Get BearerToken
	#
	Given api url %testrun_resources.idpauthurl%
	And header Content-Type = application/x-www-form-urlencoded
	And form content grant_type value %IDPGrantType%
	And form content client_id value %IDPClientId%
	And form content scope value %IDPScope%
	And form content resource_security_id value %IDPResourceSecurityId%
	And form content agency_secret value %IDPAgencySecret%
	When method POST
	Then assert api response status is equal to 200
	#
	# SET - %bearer% to equal access_token 
	#
	Given var %bearer% is equal to response.access_token type string
	Then print var %bearer% debug
	#
	# SQL - Get latest worker id from workers table
	#
	Given request sqlserver select top 1 wkr_id from WORKERS where wkr_lastname like '%ZZZ%' order by 1 desc
	When method sqlserver-select
	#
	Given var %id% is equal to response.sqlresult.[0].wkr_id type string
	#Given var %id% is equal to %testrun_resources.WorkerV3BaseId% type int
	#
	# GET - Get Base Worker Json
	#
	Given api url %testrun_resources.worker_v3_url%
	And path Agent
	And path %id%
	And header Content-Type = application/json
	And header Authorization = Bearer %bearer%
	When method GET
	Then assert api response status is equal to 200
	And assert json response worker.id is equal to %id%
	And assert json response status.status is equal to Success
	And assert json response status.message is equal to Request is successful
	And assert json response worker.workerBase.workerId is equal to %id%
	#
	# Set - Add worker json to variable
	#
	Given var %workerJson% is equal to response.worker type json
	# --------------------
	# Start - Pre-req variables
	#
	And var %nameDate% as now type date
	And format var %nameDate% as string MMdd_hhmmss
	And var %fName% as ZZAUTO_FIRST_ type string
	And var %lName% as ZZAUTO_LAST_ type string
	And var %uName% as ZZAUTO_ type string
	And var %firstName% = %fName% + %nameDate%
	And var %lastName% = %lName% + %nameDate%
	And var %fullName% = %lastName% + ,
	And var %fullName% = %fullName% + %firstName%
	And var %userName% = %uName% + %nameDate%
	# Replace json properties with new values
	Given replace json %workerJson% at path id with 0
	And replace json %workerJson% at path workerBase.workerId with 0
	And replace json %workerJson% at path workerBase.ableToBeAdmissionCoord with N
	And replace json %workerJson% at path workerBase.ableToPerformSocRecertEval with N
	And replace json %workerJson% at path workerBase.ableToPerformVisits with Y
	And replace json %workerJson% at path workerBase.ableToObtainLoginProfile with N
	# TODO: Address object
	And replace json %workerJson% at path workerBase.advancedBusinessAnalysisLicense with N
	And replace json %workerJson% at path workerBase.allowAutoApproveOrders with N
	And replace json %workerJson% at path workerBase.allowEmergentPrnOrders with N
	And replace json %workerJson% at path workerBase.allowPlanOfCareOrders with N
	And replace json %workerJson% at path workerBase.allowRapidReschedule with N
	And replace json %workerJson% at path workerBase.allowSignatureOn485PlanOfCare with N
	And replace json %workerJson% at path workerBase.careWatchUser with N
	And replace json %workerJson% at path workerBase.cellPhone with null
	And replace json %workerJson% at path workerBase.censusTract with null
	# May have an issue with this needing to be empty
	And replace json %workerJson% at path workerBase.chexId with null
	And replace json %workerJson% at path workerBase.chexProgram with N/A
	# May have an issue with this needing to be empty
	And replace json %workerJson% at path workerBase.cityTaxCode with null
	And replace json %workerJson% at path workerBase.cityTaxTable with null
	And replace json %workerJson% at path workerBase.comments with null
	And replace json %workerJson% at path workerBase.commuteMiles with null
	And replace json %workerJson% at path workerBase.contractor with null
	And replace json %workerJson% at path workerBase.dateHired with null
	And replace json %workerJson% at path workerBase.dateLock with null
	And replace json %workerJson% at path workerBase.dateTerminated with null
	And replace json %workerJson% at path workerBase.defaultProdPtg with 1
	And replace json %workerJson% at path workerBase.dlNumber with null
	And replace json %workerJson% at path workerBase.dlState with null
	And replace json %workerJson% at path workerBase.dob with 1980-01-01T00:00:00
	And replace json %workerJson% at path workerBase.email with null
	And replace json %workerJson% at path workerBase.enableSilentGps with Y
	And replace json %workerJson% at path workerBase.exempt with N
	And replace json %workerJson% at path workerBase.expectedHoursPerPeriod with null
	And replace json %workerJson% at path workerBase.exportDate with null
	And replace json %workerJson% at path workerBase.fdbLicense with N
	And replace json %workerJson% at path workerBase.federalExempt with null
	And replace json %workerJson% at path workerBase.federalStatus with null
	And replace json %workerJson% at path workerBase.firstName with %firstName%
	And replace json %workerJson% at path workerBase.fullName with %fullName%
	And replace json %workerJson% at path workerBase.fullTime with N
	And replace json %workerJson% at path workerBase.gender with F
	And replace json %workerJson% at path workerBase.hchbEmployee with N
	And replace json %workerJson% at path workerBase.hchbLicenseType.active with Y
	And replace json %workerJson% at path workerBase.hchbLicenseType.description with PRN AIDE FIELD
	And replace json %workerJson% at path workerBase.hchbLicenseType.hchbLicenseTypeId with 5
	And replace json %workerJson% at path workerBase.homePhone with null
	And replace json %workerJson% at path workerBase.instantPay with N
	And replace json %workerJson% at path workerBase.interactiveVideoAccessEffectiveToDate with null
	And replace json %workerJson% at path workerBase.interactiveVideoReportingAccess.accessId with 0
	And replace json %workerJson% at path workerBase.interactiveVideoReportingAccess.description with No Access
	And replace json %workerJson% at path workerBase.isFriInWorkWeek with Y
	And replace json %workerJson% at path workerBase.isMonInWorkWeek with Y
	And replace json %workerJson% at path workerBase.isSatInWorkWeek with N
	And replace json %workerJson% at path workerBase.isSunInWorkWeek with N
	And replace json %workerJson% at path workerBase.isThuInWorkWeek with Y
	And replace json %workerJson% at path workerBase.isTueInWorkWeek with Y
	And replace json %workerJson% at path workerBase.isWedInWorkWeek with Y
	And replace json %workerJson% at path workerBase.lastName with %lastName%
	And replace json %workerJson% at path workerBase.latitude with null
	And replace json %workerJson% at path workerBase.latLongMethod with ADDR_GEO
	And replace json %workerJson% at path workerBase.longitude with null
	And replace json %workerJson% at path workerBase.maritalStatus with null
	And replace json %workerJson% at path workerBase.maxHoursPerDay with null
	And replace json %workerJson% at path workerBase.maxHoursPerWeek with null
	And replace json %workerJson% at path workerBase.maxVisitsPerDay with null
	And replace json %workerJson% at path workerBase.maxVisitsPerWeek with null
	And replace json %workerJson% at path workerBase.mi with A
	And replace json %workerJson% at path workerBase.mileagePayMethod with null
	# 
	And replace json %workerJson% at path workerBase.mileagePayMethod with null
	And var %mileageJson% is equal to { "description": "ACTUAL MILEAGE", "methodCode": "AM" } type string
	And replace json %workerJson% at path workerBase.mileagePayMethod with %mileageJson%
	#
	And replace json %workerJson% at path workerBase.nickName with AUTO
	And replace json %workerJson% at path workerBase.npi with null
	And replace json %workerJson% at path workerBase.pager with null
	And replace json %workerJson% at path workerBase.payLock with null
	And replace json %workerJson% at path workerBase.payReportedMiles with false
	And replace json %workerJson% at path workerBase.payrollDepartment with null
	And replace json %workerJson% at path workerBase.payrollFrequencyId with null
	And replace json %workerJson% at path workerBase.payrollNumber with %nameDate%
	And replace json %workerJson% at path workerBase.payrollStatus with null
	And replace json %workerJson% at path workerBase.picturePath with null
	And replace json %workerJson% at path workerBase.primaryJobDescription.jobDescriptionId with 2
	And replace json %workerJson% at path workerBase.primaryWorkerClass with F
	And replace json %workerJson% at path workerBase.prodPointsPer8PdoHrs with 7
	And replace json %workerJson% at path workerBase.productivityPointsPeriod with W
	And replace json %workerJson% at path workerBase.r2UserName with %lastName%
	And replace json %workerJson% at path workerBase.race with null
	And replace json %workerJson% at path workerBase.referralName with null
	And replace json %workerJson% at path workerBase.referralPhone with null
	And replace json %workerJson% at path workerBase.regHrsPtg with 8
	And replace json %workerJson% at path workerBase.regularPay with null
	And replace json %workerJson% at path workerBase.signatureTitle with AIDE
	And replace json %workerJson% at path workerBase.ssn with 999-99-9999
	And replace json %workerJson% at path workerBase.stateExempt with null
	And replace json %workerJson% at path workerBase.stateStatus with null
	And replace json %workerJson% at path workerBase.stateTaxCode with null
	And replace json %workerJson% at path workerBase.stateTaxTable with null
	And replace json %workerJson% at path workerBase.teamMemberForAssignedBranches with Y
	And replace json %workerJson% at path workerBase.templateId with 0
	And replace json %workerJson% at path workerBase.userInput1 with null
	And replace json %workerJson% at path workerBase.userInput2 with null
	And replace json %workerJson% at path workerBase.userInput3 with AGENT
	And replace json %workerJson% at path workerBase.whoLock with null
	And replace json %workerJson% at path workerBase.workerDepartment with null
	#
	And replace json %workerJson% at path workerBase.workerJobTitle with null
	And var %workerJobTitleJson% is equal to { "descr": "AIDE", "jobTitleId": "2" } type json
	And replace json %workerJson% at path workerBase.workerJobTitle with %workerJobTitleJson%
	#
	And replace json %workerJson% at path workerBase.workerReferralCategory with null
	And replace json %workerJson% at path workerBase.workerStatus.descr with ACTIVE
	And replace json %workerJson% at path workerBase.workerStatus.id with 1
	And replace json %workerJson% at path workerBase.workerType.descr with EMPLOYEE
	And replace json %workerJson% at path workerBase.workerType.typeId with 1
	And replace json %workerJson% at path workerBase.workPhone with null
	And replace json %workerJson% at path workerHomeBranch.homeBranch.active with Y
	And replace json %workerJson% at path workerHomeBranch.homeBranch.branchCode with HH1
	And replace json %workerJson% at path workerHomeBranch.workerId with 0
	And remove json %workerJson% at path workerLoginProfiles.loginProfileList
	And replace json %workerJson% at path workerLoginProfiles.userName with %userName%
	And replace json %workerJson% at path workerRequestMeta.status with Y
	And replace json %workerJson% at path workerRequestMeta.action with GetWorker
	And replace json %workerJson% at path workerRequestMeta.matchedOn with 1
	And replace json %workerJson% at path workerRequestMeta.matchedOptions with 1
	# 
	And remove json %workerJson% at path workerServiceLineList
	And var %workerRequestServiceLineList% as [ { "active": "Y", "default": "Y", "serviceLine": { "description": "HOSPICE", "serviceLineId": 2}, "workerId": 0, "workerServiceLineId": 1717 } ] type string
	And replace json %workerJson% at path workerServiceLineList with %workerRequestServiceLineList%
	#
	And remove json %workerJson% at path workerBranchList
	And var %workerBranchList% as [ { "workerBranchId": 0, "workerId": 0, "active": "Y", "branch": { "active": "Y", "branchCode": "HH1"}, "payrollBranch": { "active": "Y","branchCode": "HH1" } } ] type string
	And replace json %workerJson% at path workerServiceLineList with %workerBranchList%
	#

	And replace json %workerJson% at path workerTeamList.[0].workerId with 0
	And replace json %workerJson% at path workerTeamList.[1].workerId with 0
	#And replace json %workerJson% at path workerBasePayrollDetailList.[0].workerId with Y
	And replace json %workerJson% at path workerBasePayrollDetailList.[0].workerId with 0
	# Replace json properties with null values
	And replace json %workerJson% at path workerScheduling.workerSchedulingAddress with null
	# Remove (or empty) json array properties
	And remove json %workerJson% at path workerScheduling.workerUnavailabilityList
	And remove json %workerJson% at path workerPointCareInteractiveVideoCurriculumList
	And remove json %workerJson% at path workerClinicalInteractiveVideoCurriculumList
	#
	# End - Pre-req variables
	# --------------------
	Given api url %testrun_resources.worker_v3_url%
	And path Agent
	And request json %workerJson%
	And header Authorization = Bearer %bearer%
	When method POST
	Then assert api response status is equal to 200
	#And assert json response workerResult.status.status is equal to Success
	#And assert json response workerResult.status.message is equal to Request is successful
	#
	# GET - New Worker by workerid variable
	#
	Given var %newId% is equal to response.workerResult.workerId type int
	Then print var %newId% debug
	#
	Given wait 5000
	#
	Given api url %testrun_resources.worker_v3_url%
	And path Agent
	And path %newId%
	And header Content-Type = application/json
	And header Authorization = Bearer %bearer%
	When method GET
	Then assert api response status is equal to 200
	And assert json response worker.id is equal to %newId%
	And assert json response status.status is equal to Success
	And assert json response status.message is equal to Request is successful

Scenario: WorkerServicePOC_v3_Dev_FindWorker_UsingPayrollNumber
	#
	# POST - Get BearerToken
	#
	Given api url %testrun_resources.idpauthurl%
	And header Content-Type = application/x-www-form-urlencoded
	And form content grant_type value %IDPGrantType%
	And form content client_id value %IDPClientId%
	And form content scope value %IDPScope%
	And form content resource_security_id value %IDPResourceSecurityId%
	And form content agency_secret value %IDPAgencySecret%
	When method POST
	Then assert api response status is equal to 200
	#
	Given var %bearer% is equal to response.access_token type string
	#
	# TEST SETUP - Start
	# --------------------
	#
	# SQL - SYSTEM_SETTINGS.txt - Update ss_setting MatchWorkerService to 0 to allow for payroll matching
	#
	Given sql query update system_settings set ss_value = 0 where ss_setting = 'MatchWorkerService'
	When method sqlserver-update
	#
	# SQL - GetWorkerInfo.txt - Get latest worker id from workers table
	#
	Given request read sql file .\\Data\\SQLData\\WorkerService_v2_JSON\\GetWorkerInfo.txt
	When method sqlserver-select
	#
	Given var %payrollNumber% is equal to response.sqlresult.[0].worker.payrollNumber type string
	And var %firstName% is equal to response.sqlresult.[0].worker.firstName type string
	And var %lastName% is equal to response.sqlresult.[0].worker.lastName type string
	And var %ssn% is equal to response.sqlresult.[0].worker.ssn type string
	And var %workerId% is equal to response.sqlresult.[0].worker.id type string
	And var %stagingId% is equal to 0 type string
	#
	# SQL - WORKER_BASE table - Update worker to have dob of '1990-01-01' - due to scramble script
	#
	Given sql query UPDATE dbo.WORKER_BASE set wkr_ssn = '999-99-9999' where wkr_id = %workerId%
	When method sqlserver-update
	#
	# --------------------
	# TEST SETUP - End
	#
	# TEST - Start
	# --------------------
	#
	# POST /Agent/SearchWorker - Verify search results
	#
	Given api url %testrun_resources.worker_v3_url%
	And path Agent
	And path FindWorker
	And header Content-Type = application/json
	And header Authorization = Bearer %bearer%
	And request read json file .\\Data\\JsonData\\ApiData\\WorkerService_v2_JSON\\JsonRequest\\FindClient_ByPayrollNumberAndStagingId.json
	When method POST
	Then assert api response status is equal to 200
	And assert json response firstName is equal to %firstName%
	And assert json response lastName is equal to %lastName%
	And assert json response ssn is equal to %ssn%
	And assert json response workerId is equal to %workerId%
	And assert json response matchedOn is equal to 1
	And assert json response action is equal to FindWorker
	And assert json response status is equal to Y
	# --------------------
	# TEST - End

Scenario: WorkerServicePOC_v3_Dev_FindWorker_UsingFirstNameLastNameAndSSN
	#
	# POST - Get BearerToken
	#
	Given api url %testrun_resources.idpauthurl%
	And header Content-Type = application/x-www-form-urlencoded
	And form content grant_type value %IDPGrantType%
	And form content client_id value %IDPClientId%
	And form content scope value %IDPScope%
	And form content resource_security_id value %IDPResourceSecurityId%
	And form content agency_secret value %IDPAgencySecret%
	When method POST
	Then assert api response status is equal to 200
	#
	Given var %bearer% is equal to response.access_token type string
	#
	# TEST SETUP - Start
	# --------------------
	#
	# SQL - SYSTEM_SETTINGS.txt - Update ss_setting
	Given sql query update system_settings set ss_value = 1 where ss_setting = 'MatchWorkerService'
	When method sqlserver-update
	#
	# SQL - GetWorkerInfo.txt - Get latest worker id from workers table
	#
	Given request read sql file .\\Data\\SQLData\\WorkerService_v2_JSON\\GetWorkerInfo.txt
	When method sqlserver-select
	#
	Given var %payrollNumber% is equal to ZZ12345 type string
	And var %firstName% is equal to response.sqlresult.[0].worker.firstName type string
	And var %lastName% is equal to response.sqlresult.[0].worker.lastName type string
	And var %ssn% is equal to response.sqlresult.[0].worker.ssn type string
	And var %workerId% is equal to response.sqlresult.[0].worker.id type string
	And var %stagingId% is equal to 0 type string
	Then print var %workerId% debug
	#
	# SQL - WORKER_BASE table - Update worker to have dob of '1990-01-01' - due to scramble script
	#
	Given sql query UPDATE dbo.WORKER_BASE set wkr_ssn = '999-99-9999' where wkr_id = %workerId%
	When method sqlserver-update
	#
	# SQL - WORKER_BASE table - Update worker to have payroll number of 'ZZ12345'
	#
	Given sql query UPDATE dbo.WORKER_BASE set wkr_payrollno = '%payrollNumber%' where wkr_id = %workerId%
	When method sqlserver-update
	#
	# --------------------
	# TEST SETUP - End
	#
	# TEST - Start
	# --------------------
	#
	# POST /Agent/SearchWorker - Verify search results
	#
	Given api url %testrun_resources.worker_v3_url%
	And path Agent
	And path FindWorker
	And header Content-Type = application/json
	And header Authorization = Bearer %bearer%
	And request read json file .\\Data\\JsonData\\ApiData\\WorkerService_v2_JSON\\JsonRequest\\FindClient_ByFirstNameLastNameAndSSN.json
	When method POST
	Then assert api response status is equal to 200
	And assert json response firstName is equal to %firstName%
	And assert json response lastName is equal to %lastName%
	And assert json response ssn is equal to %ssn%
	# BUGS Below, returning workerId 0, matchedOn should be 1?
	#And assert json response workerId is equal to %workerId%
	#And assert json response matchedOn is equal to 1
	And assert json response action is equal to FindWorker
	And assert json response status is equal to Y
	# --------------------
	# TEST - End

Scenario: WorkerServicePOC_v3_Dev_FindWorker_UsingWorkerId
	#
	# POST - Get BearerToken
	#
	Given api url %testrun_resources.idpauthurl%
	And header Content-Type = application/x-www-form-urlencoded
	And form content grant_type value %IDPGrantType%
	And form content client_id value %IDPClientId%
	And form content scope value %IDPScope%
	And form content resource_security_id value %IDPResourceSecurityId%
	And form content agency_secret value %IDPAgencySecret%
	When method POST
	Then assert api response status is equal to 200
	#
	Given var %bearer% is equal to response.access_token type string
	Then print var %bearer% debug
	#
	# TEST SETUP - Start
	# --------------------
	#
	# SQL - SYSTEM_SETTINGS.txt - Update ss_setting
	#
	Given sql query update system_settings set ss_value = 2 where ss_setting = 'MatchWorkerService'
	When method sqlserver-update
	#
	# SQL - GetWorkerInfo.txt - Get latest worker id from workers table
	#
	Given request read sql file .\\Data\\SQLData\\WorkerService_v2_JSON\\GetWorkerInfo.txt
	When method sqlserver-select
	#
	Given var %payrollNumber% is equal to ZZ12345 type string
	And var %firstName% is equal to response.sqlresult.[0].worker.firstName type string
	And var %lastName% is equal to response.sqlresult.[0].worker.lastName type string
	And var %ssn% is equal to response.sqlresult.[0].worker.ssn type string
	And var %workerId% is equal to response.sqlresult.[0].worker.id type string
	And var %stagingId% is equal to 0 type string
	#
	# SQL - WORKER_BASE table - Update worker to have dob of '1990-01-01' - due to scramble script
	#
	Given sql query UPDATE dbo.WORKER_BASE set wkr_ssn = '999-99-9999' where wkr_id = %workerId%
	When method sqlserver-update
	#
	# SQL - WORKER_BASE table - Update worker to have payroll number of 'ZZ12345'
	#
	Given sql query UPDATE dbo.WORKER_BASE set wkr_payrollno = '%payrollNumber%' where wkr_id = %workerId%
	When method sqlserver-update
	#
	# --------------------
	# TEST SETUP - End
	#
	# TEST - Start
	# --------------------
	#
	# POST /Agent/SearchWorker - Verify search results
	#
	Given api url %testrun_resources.worker_v3_url%
	And path Agent
	And path FindWorker
	And header Content-Type = application/json
	And header Authorization = Bearer %bearer%
	And request read json file .\\Data\\JsonData\\ApiData\\WorkerService_v2_JSON\\JsonRequest\\FindClient_ByWorkerId.json
	When method POST
	Then assert api response status is equal to 200
	And assert json response firstName is equal to %firstName%
	And assert json response lastName is equal to %lastName%
	And assert json response ssn is equal to %ssn%
	And assert json response workerId is equal to %workerId%
	And assert json response matchedOn is equal to 2
	And assert json response action is equal to FindWorker
	And assert json response status is equal to Y
	# --------------------
	# TEST - End

Scenario: WorkerServicePOC_v3_Dev_SearchWorker_ByPayrollNumber
	#
	# POST - Get BearerToken
	#
	Given api url %testrun_resources.idpauthurl%
	And header Content-Type = application/x-www-form-urlencoded
	And form content grant_type value %IDPGrantType%
	And form content client_id value %IDPClientId%
	And form content scope value %IDPScope%
	And form content resource_security_id value %IDPResourceSecurityId%
	And form content agency_secret value %IDPAgencySecret%
	When method POST
	Then assert api response status is equal to 200
	#
	Given var %bearer% is equal to response.access_token type string
	#
	# TEST SETUP - Start
	# --------------------
	#
	# SQL - GetWorkerInfo.txt - Get latest worker id from workers table
	#
	Given request read sql file .\\Data\\SQLData\\WorkerService_v2_JSON\\GetWorkerInfo.txt
	When method sqlserver-select
	#
	Given var %payrollNumber% is equal to response.sqlresult.[0].worker.payrollNumber type string
	And var %firstName% is equal to response.sqlresult.[0].worker.firstName type string
	And var %lastName% is equal to response.sqlresult.[0].worker.lastName type string
	And var %ssn% is equal to response.sqlresult.[0].worker.ssn type string
	And var %workerId% is equal to response.sqlresult.[0].worker.id type string
	And var %gender% is equal to response.sqlresult.[0].worker.gender type string
	And var %stagingId% is equal to 0 type string
	#
	# --------------------
	# TEST SETUP - End
	#
	# TEST - Start
	# --------------------
	#
	# POST /Agent/SearchWorker - Verify search results
	#
	Given api url %testrun_resources.worker_v3_url%
	And path Agent
	And path SearchWorker
	And param itemLimit value 0
	And header Content-Type = application/json
	And header Authorization = Bearer %bearer%
	And request read json file .\\Data\\JsonData\\ApiData\\WorkerService_v2_JSON\\JsonRequest\\SearchClient_ByPayrollNumber.json
	When method POST
	Then assert api response status is equal to 200
	And assert json response [0].firstName is equal to %firstName%
	And assert json response [0].lastName is equal to %lastName%
	And assert json response [0].ssn is equal to %ssn%
	And assert json response [0].workerId is equal to %workerId%
	And assert json response [0].gender is equal to %gender%
	And assert json response [0].payrollNumber is equal to %payrollNumber%
	# --------------------
	# TEST - End

@DevAndQALevelsOnly
Scenario: WorkerServicePOC_v3_Dev_SearchWorker_BySSN
	#
	# POST - Get BearerToken
	#
	Given api url %testrun_resources.idpauthurl%
	And header Content-Type = application/x-www-form-urlencoded
	And form content grant_type value %IDPGrantType%
	And form content client_id value %IDPClientId%
	And form content scope value %IDPScope%
	And form content resource_security_id value %IDPResourceSecurityId%
	And form content agency_secret value %IDPAgencySecret%
	When method POST
	Then assert api response status is equal to 200
	#
	Given var %bearer% is equal to response.access_token type string
	#
	# TEST SETUP - Start
	# --------------------
	#
	# SQL - GetWorkerInfo.txt - Get latest worker id from workers table
	#
	Given request read sql file .\\Data\\SQLData\\WorkerService_v2_JSON\\GetWorkerInfo.txt
	When method sqlserver-select
	#
	Given var %payrollNumber% is equal to response.sqlresult.[0].worker.payrollNumber type string
	And var %firstName% is equal to response.sqlresult.[0].worker.firstName type string
	And var %lastName% is equal to response.sqlresult.[0].worker.lastName type string
	And var %ssn% is equal to 999-99-9999 type string
	And var %workerId% is equal to response.sqlresult.[0].worker.id type string
	And var %gender% is equal to response.sqlresult.[0].worker.gender type string
	And var %stagingId% is equal to 0 type string
	#
	# SQL - WORKER_BASE table - Update worker to have dob of '1990-01-01' - due to scramble script
	#
	Given sql query UPDATE dbo.WORKER_BASE set wkr_ssn = '999-99-9999' where wkr_id = %workerId%
	When method sqlserver-update
	#
	# --------------------
	# TEST SETUP - End
	#
	# TEST - Start
	# --------------------
	#
	# POST /Agent/SearchWorker - Verify search results
	#
	Given api url %testrun_resources.worker_v3_url%
	And path Agent
	And path SearchWorker
	And param itemLimit value 0
	And header Content-Type = application/json
	And header Authorization = Bearer %bearer%
	And request read json file .\\Data\\JsonData\\ApiData\\WorkerService_v2_JSON\\JsonRequest\\SearchClient_BySSN.json
	When method POST
	Then assert api response status is equal to 200
	And assert json response [0].firstName is equal to %firstName%
	And assert json response [0].lastName is equal to %lastName%
	And assert json response [0].ssn is equal to %ssn%
	And assert json response [0].workerId is equal to %workerId%
	And assert json response [0].gender is equal to %gender%
	And assert json response [0].payrollNumber is equal to %payrollNumber%
	# --------------------
	# TEST CLEAN UP - End

@DevAndQALevelsOnly
Scenario: WorkerServicePOC_v3_Dev_SearchWorker_ByFirstNameLastNameAndGender
	#
	# POST - Get BearerToken
	#
	Given api url %testrun_resources.idpauthurl%
	And header Content-Type = application/x-www-form-urlencoded
	And form content grant_type value %IDPGrantType%
	And form content client_id value %IDPClientId%
	And form content scope value %IDPScope%
	And form content resource_security_id value %IDPResourceSecurityId%
	And form content agency_secret value %IDPAgencySecret%
	When method POST
	Then assert api response status is equal to 200
	#
	Given var %bearer% is equal to response.access_token type string
	#
	# TEST SETUP - Start
	# --------------------
	#
	# SQL - GetWorkerInfo.txt - Get latest worker id from workers table
	#
	Given request read sql file .\\Data\\SQLData\\WorkerService_v2_JSON\\GetWorkerInfo.txt
	When method sqlserver-select
	#
	Given var %payrollNumber% is equal to response.sqlresult.[0].worker.payrollNumber type string
	And var %firstName% is equal to response.sqlresult.[0].worker.firstName type string
	And var %lastName% is equal to response.sqlresult.[0].worker.lastName type string
	And var %workerId% is equal to response.sqlresult.[0].worker.id type string
	And var %dob% is equal to 01/01/1990 type string
	And var %gender% is equal to response.sqlresult.[0].worker.gender type string
	And var %stagingId% is equal to 0 type string
	#
	# SQL - WORKER_BASE table - Update worker to have dob of '1990-01-01' - due to scramble script
	#
	Given sql query UPDATE dbo.WORKER_BASE set wkr_dob = '1990-01-01' where wkr_id = %workerId%
	When method sqlserver-update
	#
	# --------------------
	# TEST SETUP - End
	#
	# TEST - Start
	# --------------------
	#
	# POST /Agent/SearchWorker - Verify search results
	#
	Given api url %testrun_resources.worker_v3_url%
	And path Agent
	And path SearchWorker
	And param itemLimit value 0
	And header Content-Type = application/json
	And header Authorization = Bearer %bearer%
	And request read json file .\\Data\\JsonData\\ApiData\\WorkerService_v2_JSON\\JsonRequest\\SearchClient_ByFirstNameLastNameAndGender.json
	When method POST
	Then assert api response status is equal to 200
	And assert json response [0].firstName is equal to %firstName%
	And assert json response [0].lastName is equal to %lastName%
	And assert json response [0].dob contains %dob%
	And assert json response [0].workerId is equal to %workerId%
	And assert json response [0].gender is equal to %gender%
	And assert json response [0].payrollNumber is equal to %payrollNumber%
	# --------------------
	# TEST - End

@DevAndQALevelsOnly
Scenario: WorkerServicePOC_v3_Dev_SearchWorker_InvalidFieldName
	#
	# POST - Get BearerToken
	#
	Given api url %testrun_resources.idpauthurl%
	And header Content-Type = application/x-www-form-urlencoded
	And form content grant_type value %IDPGrantType%
	And form content client_id value %IDPClientId%
	And form content scope value %IDPScope%
	And form content resource_security_id value %IDPResourceSecurityId%
	And form content agency_secret value %IDPAgencySecret%
	When method POST
	Then assert api response status is equal to 200
	#
	Given var %bearer% is equal to response.access_token type string
	#
	# TEST SETUP - Start
	# --------------------
	#
	# SQL - GetWorkerInfo.txt - Get latest worker id from workers table
	#
	Given request read sql file .\\Data\\SQLData\\WorkerService_v2_JSON\\GetWorkerInfo.txt
	When method sqlserver-select
	#
	Given var %payrollNumber% is equal to response.sqlresult.[0].worker.payrollNumber type string
	And var %firstName% is equal to response.sqlresult.[0].worker.firstName type string
	And var %lastName% is equal to response.sqlresult.[0].worker.lastName type string
	And var %workerId% is equal to response.sqlresult.[0].worker.id type string
	And var %dob% is equal to 01/01/1990 type string
	And var %gender% is equal to response.sqlresult.[0].worker.gender type string
	And var %stagingId% is equal to 0 type string
	#
	# SQL - WORKER_BASE table - Update worker to have dob of '1990-01-01' - due to scramble script
	#
	Given sql query UPDATE dbo.WORKER_BASE set wkr_dob = '1990-01-01' where wkr_id = %workerId%
	When method sqlserver-update
	#
	# --------------------
	# TEST SETUP - End
	#
	# TEST - Start
	# --------------------
	#
	# POST /Agent/SearchWorker - Verify error and message
	#
	Given api url %testrun_resources.worker_v3_url%
	And path Agent
	And path SearchWorker
	And param itemLimit value 0
	And header Content-Type = application/json
	And header Authorization = Bearer %bearer%
	And request read json file .\\Data\\JsonData\\ApiData\\WorkerService_v2_JSON\\JsonRequest\\SearchClient_InvalidGenderPropName.json
	When method POST
	Then assert api response status is equal to 400
	And assert json response [0].code is equal to Error
	And assert json response [0].field is equal to workerSearch
	And assert json response [0].message is equal to Invalid Search Criteria: At least one identifier (PayrollNumber, PayrollDepartment, Ssn) or a demographic set (LastName, FirstName, DOB, and Gender) must be included.
	# --------------------
	# TEST - End

Scenario: WorkerServicePOC_v3_Dev_SearchWorker_FirstNameNullValue
	#
	# POST - Get BearerToken
	#
	Given api url %testrun_resources.idpauthurl%
	And header Content-Type = application/x-www-form-urlencoded
	And form content grant_type value %IDPGrantType%
	And form content client_id value %IDPClientId%
	And form content scope value %IDPScope%
	And form content resource_security_id value %IDPResourceSecurityId%
	And form content agency_secret value %IDPAgencySecret%
	When method POST
	Then assert api response status is equal to 200
	#
	Given var %bearer% is equal to response.access_token type string
	#
	# TEST SETUP - Start
	# --------------------
	#
	# SQL - GetWorkerInfo.txt - Get latest worker id from workers table
	#
	Given request read sql file .\\Data\\SQLData\\WorkerService_v2_JSON\\GetWorkerInfo.txt
	When method sqlserver-select
	#
	Given var %payrollNumber% is equal to response.sqlresult.[0].worker.payrollNumber type string
	And var %firstName% is equal to response.sqlresult.[0].worker.firstName type string
	And var %lastName% is equal to response.sqlresult.[0].worker.lastName type string
	And var %workerId% is equal to response.sqlresult.[0].worker.id type string
	And var %dob% is equal to 01/01/1990 type string
	And var %gender% is equal to response.sqlresult.[0].worker.gender type string
	And var %stagingId% is equal to 0 type string
	#
	# --------------------
	# TEST SETUP - End
	#
	# TEST - Start
	# --------------------
	#
	# POST /Agent/SearchWorker - Verify error and message
	#
	Given api url %testrun_resources.worker_v3_url%
	And path Agent
	And path SearchWorker
	And param itemLimit value 0
	And header Content-Type = application/json
	And header Authorization = Bearer %bearer%
	And request read json file .\\Data\\JsonData\\ApiData\\WorkerService_v2_JSON\\JsonRequest\\SearchClient_ByFirstNameLastNameAndGender.json
	And replace json at path firstName with null
	When method POST
	Then assert api response status is equal to 400
	And assert json response [0].code is equal to Error
	And assert json response [0].field is equal to workerSearch
	And assert json response [0].message is equal to Invalid Search Criteria: At least one identifier (PayrollNumber, PayrollDepartment, Ssn) or a demographic set (LastName, FirstName, DOB, and Gender) must be included.
	# --------------------
	# TEST - End

Scenario: WorkerServicePOC_v3_CreateWorker_Hospice_WithAddressType_Home
	#
	# POST - Get BearerToken
	#
	Given api url %testrun_resources.idpauthurl%
	And header Content-Type = application/x-www-form-urlencoded
	And form content grant_type value %IDPGrantType%
	And form content client_id value %IDPClientId%
	And form content scope value %IDPScope%
	And form content resource_security_id value %IDPResourceSecurityId%
	And form content agency_secret value %IDPAgencySecret%
	When method POST
	Then assert api response status is equal to 200
	#
	Given var %bearer% is equal to response.access_token type string
	#
	# TEST SETUP - Start
	# --------------------
	#
	Given var %addressType% as HOME type string
	And var %serviceLine% as HOSPICE type string
	And var %serviceLineId% as 2 type int
	#
	And var %nameDate% as now type date
	And format var %nameDate% as string MMdd_hhmmss
	And var %fName% as ZZAUTO_FIRST_ type string
	And var %lName% as ZZAUTO_LAST_ type string
	And var %uName% as ZZAUTO_ type string
	And var %firstName% = %fName% + %nameDate%
	And var %lastName% = %lName% + %nameDate%
	And var %fullName% = %lastName% + ,
	And var %fullName% = %fullName% + %firstName%
	And var %userName% = %uName% + %nameDate%
	#
	# SQL - BRANCHES Table - Get active HOSPICE branchcode
	#
	Given request sqlserver select top 1 * from branches where branch_active = 'Y' and branch_rslid = 2
	When method SQLServer-SELECT
	#
	Given var %branchCode% is equal to response.sqlresult.[0].branch_code type string
	#
	# --------------------
	# TEST SETUP - End
	#
	#
	# TEST - Start
	# --------------------
	#
	# POST /Agent - Create Worker with BASE_CreateWorker json
	#
	Given api url %testrun_resources.worker_v3_url%
	And path Agent
	And header Authorization = Bearer %bearer%
	And header Content-Type = application/json
	And request read json file .\\Data\\JsonData\\ApiData\\WorkerService_v2_JSON\\JsonRequest\\BASE_CreateWorker.json
	When method POST
	Then assert api response status is equal to 200
	And assert json response workerResult.status.status is equal to Success
	And assert json response workerResult.status.message is equal to Request is successful
	#
	# GET /Agent/{id} - New Worker by workerid variable
	#
	Given var %workerId% is equal to response.workerResult.workerId type int
	Then print var %workerId% debug
	#
	Given wait 5000
	#
	Given api url %testrun_resources.worker_v3_url%
	And path Agent
	And path %workerId%
	And header Content-Type = application/json
	And header Authorization = Bearer %bearer%
	When method GET
	Then assert api response status is equal to 200
	And assert json response worker.id is equal to %workerId%
	And assert json response worker.workerBase.address.addressType is equal to %addressType%
	# --------------------
	# TEST - End
	#
	# TEST CLEAN UP - Start
	# --------------------
	Given var %firstName% is equal to response.worker.workerBase.firstName type string
	And var %fullName% is equal to response.worker.workerBase.fullName type string
	And var %gender% is equal to F type string
	And var %lastName% is equal to response.worker.workerBase.lastName type string
	And var %payrollNumber% is equal to response.worker.workerBase.payrollNumber type string
	And var %r2UserName% is equal to response.worker.workerBase.r2UserName type string
	And var %%workerLoginProfiles.userName%% is equal to response.worker.workerLoginProfiles.userName type string
	And var %workerServiceLineId% is equal to response.worker.workerServiceLineList.[0].workerServiceLineId type int
	#
	# POST /Agent - Update - Inactivate new worker
	#
	Given api url %testrun_resources.worker_v3_url%
	And path Agent
	And header Authorization = Bearer %bearer%
	And header Content-Type = application/json
	And request read json file .\\Data\\JsonData\\ApiData\\WorkerService_v2_JSON\\JsonRequest\\BASE_UpdateWorker.json
	And replace json JSONRequestBody at path workerBase.workerStatus.descr with INACTIVE
	And replace json JSONRequestBody at path workerBase.workerStatus.id with 4
	When method POST
	Then assert api response status is equal to 200
	And assert json response workerResult.status.status is equal to Success
	And assert json response workerResult.status.message is equal to Request is successful
	# --------------------
	# TEST CLEAN UP - End

Scenario: WorkerServicePOC_v3_UpdateWorker_Hospice_WithAddressType_Work
	#
	# POST - Get BearerToken
	#
	Given api url %testrun_resources.idpauthurl%
	And header Content-Type = application/x-www-form-urlencoded
	And form content grant_type value %IDPGrantType%
	And form content client_id value %IDPClientId%
	And form content scope value %IDPScope%
	And form content resource_security_id value %IDPResourceSecurityId%
	And form content agency_secret value %IDPAgencySecret%
	When method POST
	Then assert api response status is equal to 200
	#
	Given var %bearer% is equal to response.access_token type string
	#
	# TEST SETUP - Start
	# --------------------
	#
	Given var %addressType% as HOME type string
	Given var %serviceLine% as HOSPICE type string
	And var %serviceLineId% as 2 type int
	#
	And var %nameDate% as now type date
	And format var %nameDate% as string MMdd_hhmmss
	And var %fName% as ZZAUTO_FIRST_ type string
	And var %lName% as ZZAUTO_LAST_ type string
	And var %uName% as ZZAUTO_ type string
	And var %firstName% = %fName% + %nameDate%
	And var %lastName% = %lName% + %nameDate%
	And var %fullName% = %lastName% + ,
	And var %fullName% = %fullName% + %firstName%
	And var %userName% = %uName% + %nameDate%
	#
	# SQL - BRANCHES Table -  Get active HOSPICE branchcode
	#
	Given request sqlserver select top 1 * from branches where branch_active = 'Y' and branch_rslid = %serviceLineId%
	When method SQLServer-SELECT
	#
	Given var %branchCode% is equal to response.sqlresult.[0].branch_code type string
	#
	# POST /Agent - Create Worker with BASE_CreateWorker json
	#
	Given api url %testrun_resources.worker_v3_url%
	And path Agent
	And header Authorization = Bearer %bearer%
	And header Content-Type = application/json
	And request read json file .\\Data\\JsonData\\ApiData\\WorkerService_v2_JSON\\JsonRequest\\BASE_CreateWorker.json
	When method POST
	Then assert api response status is equal to 200
	And assert json response workerResult.status.status is equal to Success
	And assert json response workerResult.status.message is equal to Request is successful
	#
	# GET /Agent/{id} - New Worker by workerid variable
	#
	Given var %workerId% is equal to response.workerResult.workerId type int
	Then print var %workerId% debug
	#
	Given wait 5000
	#
	Given api url %testrun_resources.worker_v3_url%
	And path Agent
	And path %workerId%
	And header Content-Type = application/json
	And header Authorization = Bearer %bearer%
	When method GET
	Then assert api response status is equal to 200
	And assert json response worker.id is equal to %workerId%
	#
	Given var %firstName% is equal to response.worker.workerBase.firstName type string
	And var %fullName% is equal to response.worker.workerBase.fullName type string
	And var %gender% is equal to F type string
	And var %lastName% is equal to response.worker.workerBase.lastName type string
	And var %payrollNumber% is equal to response.worker.workerBase.payrollNumber type string
	And var %r2UserName% is equal to response.worker.workerBase.r2UserName type string
	And var %%workerLoginProfiles.userName%% is equal to response.worker.workerLoginProfiles.userName type string
	And var %workerServiceLineId% is equal to response.worker.workerServiceLineList.[0].workerServiceLineId type int
	#
	# --------------------
	# TEST SETUP - End
	#
	#
	# TEST - Start
	# --------------------
	Given var %addressType% as WORK type string
	#
	# POST /Agent - Update Worker with addressType of WORK
	#
	Given api url %testrun_resources.worker_v3_url%
	And path Agent
	And header Authorization = Bearer %bearer%
	And header Content-Type = application/json
	And request read json file .\\Data\\JsonData\\ApiData\\WorkerService_v2_JSON\\JsonRequest\\BASE_UpdateWorker.json
	When method POST
	Then assert api response status is equal to 200
	And assert json response workerResult.status.status is equal to Success
	And assert json response workerResult.status.message is equal to Request is successful
	#
	# GET /Agent/{id} - Verify addressType updated to WORK
	#
	Given wait 5000
	#
	Given api url %testrun_resources.worker_v3_url%
	And path Agent
	And path %workerId%
	And header Content-Type = application/json
	And header Authorization = Bearer %bearer%
	When method GET
	Then assert api response status is equal to 200
	And assert json response worker.id is equal to %workerId%
	And assert json response worker.workerBase.address.addressType is equal to %addressType%
	# --------------------
	# TEST - End
	#
	# TEST CLEAN UP - Start
	# --------------------
	#
	Then print var %workerId% debug
	#
	# POST - Create Worker with BASE_CreateWorker json
	#
	Given api url %testrun_resources.worker_v3_url%
	And path Agent
	And header Authorization = Bearer %bearer%
	And header Content-Type = application/json
	And request read json file .\\Data\\JsonData\\ApiData\\WorkerService_v2_JSON\\JsonRequest\\BASE_UpdateWorker.json
	And replace json JSONRequestBody at path workerBase.workerStatus.descr with INACTIVE
	And replace json JSONRequestBody at path workerBase.workerStatus.id with 4
	When method POST
	Then assert api response status is equal to 200
	And assert json response workerResult.status.status is equal to Success
	And assert json response workerResult.status.message is equal to Request is successful
	# --------------------
	# TEST CLEAN UP - End

Scenario: WorkerServicePOC_v3_CreateWorker_HH_WithoutAddressType_Home
	#
	# POST - Get BearerToken
	#
	Given api url %testrun_resources.idpauthurl%
	And header Content-Type = application/x-www-form-urlencoded
	And form content grant_type value %IDPGrantType%
	And form content client_id value %IDPClientId%
	And form content scope value %IDPScope%
	And form content resource_security_id value %IDPResourceSecurityId%
	And form content agency_secret value %IDPAgencySecret%
	When method POST
	Then assert api response status is equal to 200
	#
	Given var %bearer% is equal to response.access_token type string
	#
	# TEST SETUP - Start
	# --------------------
	#
	Given var %serviceLine% as HOMEHEALTH type string
	And var %serviceLineId% as 1 type int
	#
	And var %nameDate% as now type date
	And format var %nameDate% as string MMdd_hhmmss
	And var %fName% as ZZAUTO_FIRST_ type string
	And var %lName% as ZZAUTO_LAST_ type string
	And var %uName% as ZZAUTO_ type string
	And var %firstName% = %fName% + %nameDate%
	And var %lastName% = %lName% + %nameDate%
	And var %fullName% = %lastName% + ,
	And var %fullName% = %fullName% + %firstName%
	And var %userName% = %uName% + %nameDate%
	#
	# SQL - BRANCHES Table - Get active HOSPICE branchcode
	#
	Given request sqlserver select top 1 * from branches where branch_active = 'Y' and branch_rslid = 2
	When method SQLServer-SELECT
	#
	Given var %branchCode% is equal to response.sqlresult.[0].branch_code type string
	#
	# --------------------
	# TEST SETUP - End
	#
	#
	# TEST - Start
	# --------------------
	#
	# POST /Agent - Create Worker with BASE_CreateWorker json
	#
	Given api url %testrun_resources.worker_v3_url%
	And path Agent
	And header Authorization = Bearer %bearer%
	And header Content-Type = application/json
	And request read json file .\\Data\\JsonData\\ApiData\\WorkerService_v2_JSON\\JsonRequest\\BASE_CreateWorker.json
	And remove json JSONRequestBody at path workerBase.address
	And var %newAddressJson% as { "city": "TESTPORT", "county": "", "state": "ME", "street": "658 MAIN ROAD", "zip": "04496"} type json
	And replace json JSONRequestBody at path workerBase.address with %newAddressJson%
	When method POST
	Then assert api response status is equal to 200
	And assert json response workerResult.status.status is equal to Success
	And assert json response workerResult.status.message is equal to Request is successful
	#
	# GET /Agent/{id} - New Worker by workerid variable
	#
	Given var %workerId% is equal to response.workerResult.workerId type int
	Then print var %workerId% debug
	#
	Given wait 5000
	#
	Given api url %testrun_resources.worker_v3_url%
	And path Agent
	And path %workerId%
	And header Content-Type = application/json
	And header Authorization = Bearer %bearer%
	When method GET
	Then assert api response status is equal to 200
	And assert json response worker.id is equal to %workerId%
	And assert json response worker.workerBase.address.addressType should be null
	# --------------------
	# TEST - End
	#
	# TEST CLEAN UP - Start
	# --------------------
	Given var %firstName% is equal to response.worker.workerBase.firstName type string
	And var %fullName% is equal to response.worker.workerBase.fullName type string
	And var %gender% is equal to F type string
	And var %lastName% is equal to response.worker.workerBase.lastName type string
	And var %payrollNumber% is equal to response.worker.workerBase.payrollNumber type string
	And var %r2UserName% is equal to response.worker.workerBase.r2UserName type string
	And var %%workerLoginProfiles.userName%% is equal to response.worker.workerLoginProfiles.userName type string
	And var %workerServiceLineId% is equal to response.worker.workerServiceLineList.[0].workerServiceLineId type int
	#
	# POST /Agent - Update - Inactivate new worker
	#
	Given api url %testrun_resources.worker_v3_url%
	And path Agent
	And header Authorization = Bearer %bearer%
	And header Content-Type = application/json
	And request read json file .\\Data\\JsonData\\ApiData\\WorkerService_v2_JSON\\JsonRequest\\BASE_UpdateWorker.json
	And remove json JSONRequestBody at path workerBase.address
	And var %newAddressJson% as { "city": "TESTPORT", "county": "", "state": "ME", "street": "658 MAIN ROAD", "zip": "04496"} type json
	And replace json JSONRequestBody at path workerBase.address with %newAddressJson%
	And replace json JSONRequestBody at path workerBase.workerStatus.descr with INACTIVE
	And replace json JSONRequestBody at path workerBase.workerStatus.id with 4
	When method POST
	Then assert api response status is equal to 200
	And assert json response workerResult.status.status is equal to Success
	And assert json response workerResult.status.message is equal to Request is successful
	# --------------------
	# TEST CLEAN UP - End

Scenario: WorkerServicePOC_v3_CreateWorker_HH_WithInvalidAddressType
	#
	# POST - Get BearerToken
	#
	Given api url %testrun_resources.idpauthurl%
	And header Content-Type = application/x-www-form-urlencoded
	And form content grant_type value %IDPGrantType%
	And form content client_id value %IDPClientId%
	And form content scope value %IDPScope%
	And form content resource_security_id value %IDPResourceSecurityId%
	And form content agency_secret value %IDPAgencySecret%
	When method POST
	Then assert api response status is equal to 200
	#
	Given var %bearer% is equal to response.access_token type string
	#
	# TEST SETUP - Start
	# --------------------
	#
	Given var %addressType% as ABCD type string
	And var %serviceLine% as HOMEHEALTH type string
	And var %serviceLineId% as 1 type int
	#
	And var %nameDate% as now type date
	And format var %nameDate% as string MMdd_hhmmss
	And var %fName% as ZZAUTO_FIRST_ type string
	And var %lName% as ZZAUTO_LAST_ type string
	And var %uName% as ZZAUTO_ type string
	And var %firstName% = %fName% + %nameDate%
	And var %lastName% = %lName% + %nameDate%
	And var %fullName% = %lastName% + ,
	And var %fullName% = %fullName% + %firstName%
	And var %userName% = %uName% + %nameDate%
	#
	# SQL - BRANCHES Table - Get active HOSPICE branchcode
	#
	Given request sqlserver select top 1 * from branches where branch_active = 'Y' and branch_rslid = 2
	When method SQLServer-SELECT
	#
	Given var %branchCode% is equal to response.sqlresult.[0].branch_code type string
	#
	# --------------------
	# TEST SETUP - End
	#
	#
	# TEST - Start
	# --------------------
	#
	# POST /Agent - Verify Error for invalid addressType
	#
	Given api url %testrun_resources.worker_v3_url%
	And path Agent
	And header Authorization = Bearer %bearer%
	And header Content-Type = application/json
	And request read json file .\\Data\\JsonData\\ApiData\\WorkerService_v2_JSON\\JsonRequest\\BASE_CreateWorker.json
	When method POST
	Then assert api response status is equal to 200
	And assert json response workerResult.status.status is equal to Error
	And assert json response workerResult.status.message contains Not a valid Address Type: %addressType%
	And assert json response workerResult.workerId is equal to 0
	And assert json response activeDirectoryStatus is equal to Error
	And assert json response activeDirectoryMessage is equal to Invalid Fields
	# --------------------
	# TEST - End

Scenario: WorkerServicePOC_v3_UpdateWorker_Hospice_WithInvalidAddressType_Work
	#
	# POST - Get BearerToken
	#
	Given api url %testrun_resources.idpauthurl%
	And header Content-Type = application/x-www-form-urlencoded
	And form content grant_type value %IDPGrantType%
	And form content client_id value %IDPClientId%
	And form content scope value %IDPScope%
	And form content resource_security_id value %IDPResourceSecurityId%
	And form content agency_secret value %IDPAgencySecret%
	When method POST
	Then assert api response status is equal to 200
	#
	Given var %bearer% is equal to response.access_token type string
	#
	# TEST SETUP - Start
	# --------------------
	#
	Given var %addressType% as HOME type string
	And var %serviceLine% as HOSPICE type string
	And var %serviceLineId% as 2 type int
	#
	And var %nameDate% as now type date
	And format var %nameDate% as string MMdd_hhmmss
	And var %fName% as ZZAUTO_FIRST_ type string
	And var %lName% as ZZAUTO_LAST_ type string
	And var %uName% as ZZAUTO_ type string
	And var %firstName% = %fName% + %nameDate%
	And var %lastName% = %lName% + %nameDate%
	And var %fullName% = %lastName% + ,
	And var %fullName% = %fullName% + %firstName%
	And var %userName% = %uName% + %nameDate%
	#
	# SQL - BRANCHES Table -  Get active HOSPICE branchcode
	#
	Given request sqlserver select top 1 * from branches where branch_active = 'Y' and branch_rslid = %serviceLineId%
	When method SQLServer-SELECT
	#
	Given var %branchCode% is equal to response.sqlresult.[0].branch_code type string
	#
	# POST /Agent - Create Worker with BASE_CreateWorker json
	#
	Given api url %testrun_resources.worker_v3_url%
	And path Agent
	And header Authorization = Bearer %bearer%
	And header Content-Type = application/json
	And request read json file .\\Data\\JsonData\\ApiData\\WorkerService_v2_JSON\\JsonRequest\\BASE_CreateWorker.json
	When method POST
	Then assert api response status is equal to 200
	And assert json response workerResult.status.status is equal to Success
	And assert json response workerResult.status.message is equal to Request is successful
	#
	# GET /Agent/{id} - New Worker by workerid variable
	#
	Given var %workerId% is equal to response.workerResult.workerId type int
	Then print var %workerId% debug
	#
	Given wait 5000
	#
	Given api url %testrun_resources.worker_v3_url%
	And path Agent
	And path %workerId%
	And header Content-Type = application/json
	And header Authorization = Bearer %bearer%
	When method GET
	Then assert api response status is equal to 200
	And assert json response worker.id is equal to %workerId%
	#
	Given var %firstName% is equal to response.worker.workerBase.firstName type string
	And var %fullName% is equal to response.worker.workerBase.fullName type string
	And var %gender% is equal to F type string
	And var %lastName% is equal to response.worker.workerBase.lastName type string
	And var %payrollNumber% is equal to response.worker.workerBase.payrollNumber type string
	And var %r2UserName% is equal to response.worker.workerBase.r2UserName type string
	And var %%workerLoginProfiles.userName%% is equal to response.worker.workerLoginProfiles.userName type string
	And var %workerServiceLineId% is equal to response.worker.workerServiceLineList.[0].workerServiceLineId type int
	#
	# --------------------
	# TEST SETUP - End
	#
	#
	# TEST - Start
	# --------------------
	Given var %addressType% as ABCD type string
	#
	# POST /Agent - Update Worker with addressType of WORK
	#
	Given api url %testrun_resources.worker_v3_url%
	And path Agent
	And header Authorization = Bearer %bearer%
	And header Content-Type = application/json
	And request read json file .\\Data\\JsonData\\ApiData\\WorkerService_v2_JSON\\JsonRequest\\BASE_UpdateWorker.json
	When method POST
	Then assert api response status is equal to 200
	And assert json response workerResult.status.status is equal to Error
	And assert json response workerResult.status.message contains Not a valid Address Type: %addressType%
	And assert json response workerResult.workerId is equal to 0
	And assert json response activeDirectoryStatus is equal to Error
	And assert json response activeDirectoryMessage is equal to Invalid Fields
	#
	# GET /Agent/{id} - Verify addressType updated to WORK
	#
	Given wait 5000
	#
	Given var %addressType% as HOME type string
	#
	Given api url %testrun_resources.worker_v3_url%
	And path Agent
	And path %workerId%
	And header Content-Type = application/json
	And header Authorization = Bearer %bearer%
	When method GET
	Then assert api response status is equal to 200
	And assert json response worker.id is equal to %workerId%
	And assert json response worker.workerBase.address.addressType is equal to %addressType%
	# --------------------
	# TEST - End
	#
	# TEST CLEAN UP - Start
	# --------------------
	#
	Then print var %workerId% debug
	#
	# POST - Create Worker with BASE_CreateWorker json
	#
	Given api url %testrun_resources.worker_v3_url%
	And path Agent
	And header Authorization = Bearer %bearer%
	And header Content-Type = application/json
	And request read json file .\\Data\\JsonData\\ApiData\\WorkerService_v2_JSON\\JsonRequest\\BASE_UpdateWorker.json
	And replace json JSONRequestBody at path workerBase.workerStatus.descr with INACTIVE
	And replace json JSONRequestBody at path workerBase.workerStatus.id with 4
	When method POST
	Then assert api response status is equal to 200
	And assert json response workerResult.status.status is equal to Success
	And assert json response workerResult.status.message is equal to Request is successful
	# --------------------
	# TEST CLEAN UP - End

Scenario: WorkerServicePOC_v3_CreateWorker_HH_WithoutAddressType_UpdateAddress_WithAddressType
	#
	# POST - Get BearerToken
	#
	Given api url %testrun_resources.idpauthurl%
	And header Content-Type = application/x-www-form-urlencoded
	And form content grant_type value %IDPGrantType%
	And form content client_id value %IDPClientId%
	And form content scope value %IDPScope%
	And form content resource_security_id value %IDPResourceSecurityId%
	And form content agency_secret value %IDPAgencySecret%
	When method POST
	Then assert api response status is equal to 200
	#
	Given var %bearer% is equal to response.access_token type string
	#
	# TEST SETUP - Start
	# --------------------
	#
	Given var %serviceLine% as HOMEHEALTH type string
	And var %serviceLineId% as 1 type int
	#
	And var %nameDate% as now type date
	And format var %nameDate% as string MMdd_hhmmss
	And var %fName% as ZZAUTO_FIRST_ type string
	And var %lName% as ZZAUTO_LAST_ type string
	And var %uName% as ZZAUTO_ type string
	And var %firstName% = %fName% + %nameDate%
	And var %lastName% = %lName% + %nameDate%
	And var %fullName% = %lastName% + ,
	And var %fullName% = %fullName% + %firstName%
	And var %userName% = %uName% + %nameDate%
	#
	# SQL - BRANCHES Table - Get active HOSPICE branchcode
	#
	Given request sqlserver select top 1 * from branches where branch_active = 'Y' and branch_rslid = 2
	When method SQLServer-SELECT
	#
	Given var %branchCode% is equal to response.sqlresult.[0].branch_code type string
	#
	# --------------------
	# TEST SETUP - End
	#
	#
	# TEST - Start
	# --------------------
	#
	# POST /Agent - Create Worker with BASE_CreateWorker json
	#
	Given api url %testrun_resources.worker_v3_url%
	And path Agent
	And header Authorization = Bearer %bearer%
	And header Content-Type = application/json
	And request read json file .\\Data\\JsonData\\ApiData\\WorkerService_v2_JSON\\JsonRequest\\BASE_CreateWorker.json
	And remove json JSONRequestBody at path workerBase.address
	And var %newAddressJson% as { "city": "TESTPORT", "county": "", "state": "ME", "street": "658 MAIN ROAD", "zip": "04496"} type json
	And replace json JSONRequestBody at path workerBase.address with %newAddressJson%
	When method POST
	Then assert api response status is equal to 200
	And assert json response workerResult.status.status is equal to Success
	And assert json response workerResult.status.message is equal to Request is successful
	#
	# GET /Agent/{id} - New Worker by workerid variable
	#
	Given var %workerId% is equal to response.workerResult.workerId type int
	Then print var %workerId% debug
	#
	Given wait 5000
	#
	Given api url %testrun_resources.worker_v3_url%
	And path Agent
	And path %workerId%
	And header Content-Type = application/json
	And header Authorization = Bearer %bearer%
	When method GET
	Then assert api response status is equal to 200
	And assert json response worker.id is equal to %workerId%
	And assert json response worker.workerBase.address.addressType should be null
	#
	Given var %firstName% is equal to response.worker.workerBase.firstName type string
	And var %fullName% is equal to response.worker.workerBase.fullName type string
	And var %gender% is equal to F type string
	And var %lastName% is equal to response.worker.workerBase.lastName type string
	And var %payrollNumber% is equal to response.worker.workerBase.payrollNumber type string
	And var %r2UserName% is equal to response.worker.workerBase.r2UserName type string
	And var %%workerLoginProfiles.userName%% is equal to response.worker.workerLoginProfiles.userName type string
	And var %workerServiceLineId% is equal to response.worker.workerServiceLineList.[0].workerServiceLineId type int
	#
	#
	Given var %addressType% as WORK type string
	#
	# POST /Agent - Update Worker with addressType of WORK
	#
	Given api url %testrun_resources.worker_v3_url%
	And path Agent
	And header Authorization = Bearer %bearer%
	And header Content-Type = application/json
	And request read json file .\\Data\\JsonData\\ApiData\\WorkerService_v2_JSON\\JsonRequest\\BASE_UpdateWorker.json
	When method POST
	Then assert api response status is equal to 200
	And assert json response workerResult.status.status is equal to Success
	And assert json response workerResult.status.message is equal to Request is successful
	#
	# GET /Agent/{id} - Verify addressType updated to WORK
	#
	Given wait 5000
	#
	Given api url %testrun_resources.worker_v3_url%
	And path Agent
	And path %workerId%
	And header Content-Type = application/json
	And header Authorization = Bearer %bearer%
	When method GET
	Then assert api response status is equal to 200
	And assert json response worker.id is equal to %workerId%
	And assert json response worker.workerBase.address.addressType is equal to %addressType%
	# --------------------
	# TEST - End
	#
	# TEST CLEAN UP - Start
	# --------------------
	Given var %firstName% is equal to response.worker.workerBase.firstName type string
	And var %fullName% is equal to response.worker.workerBase.fullName type string
	And var %gender% is equal to F type string
	And var %lastName% is equal to response.worker.workerBase.lastName type string
	And var %payrollNumber% is equal to response.worker.workerBase.payrollNumber type string
	And var %r2UserName% is equal to response.worker.workerBase.r2UserName type string
	And var %%workerLoginProfiles.userName%% is equal to response.worker.workerLoginProfiles.userName type string
	And var %workerServiceLineId% is equal to response.worker.workerServiceLineList.[0].workerServiceLineId type int
	#
	# POST /Agent - Update - Inactivate new worker
	#
	Given api url %testrun_resources.worker_v3_url%
	And path Agent
	And header Authorization = Bearer %bearer%
	And header Content-Type = application/json
	And request read json file .\\Data\\JsonData\\ApiData\\WorkerService_v2_JSON\\JsonRequest\\BASE_UpdateWorker.json
	And replace json JSONRequestBody at path workerBase.workerStatus.descr with INACTIVE
	And replace json JSONRequestBody at path workerBase.workerStatus.id with 4
	When method POST
	Then assert api response status is equal to 200
	And assert json response workerResult.status.status is equal to Success
	And assert json response workerResult.status.message is equal to Request is successful
	# --------------------
	# TEST CLEAN UP - End