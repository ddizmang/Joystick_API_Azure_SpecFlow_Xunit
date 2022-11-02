Feature: US373672-Worker-BasePayRate

Scenario: Verify base pay rate
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
	# POST /Agent - Create Worker with BASE_CreateWorker json and workerBasePayrollDetail.basePayRate of 1.0
	#
	Given api url %testrun_resources.worker_v3_url%
	And path Agent
	And header Authorization = Bearer %bearer%
	And header Content-Type = application/json
	And var %basePayRate% as 1 type string
	And request read json file .\\Data\\JsonData\\ApiData\\WorkerService_v2_JSON\\JsonRequest\\BASE_CreateWorker.json
	And var %workerBasePayrollDetailList% read json file .\\Data\\JsonData\\ApiData\\WorkerService_v2_JSON\\JsonRequest\\BASE_workerBasePayrollDetailList_withBasePayRate.json
	And replace json JSONRequestBody at path workerBasePayrollDetailList.[0] with %workerBasePayrollDetailList%
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
	And assert json response worker.workerBasePayrollDetailList.[0].basePayRate contains %basePayRate%
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