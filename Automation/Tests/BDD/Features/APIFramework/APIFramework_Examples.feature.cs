﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (https://www.specflow.org/).
//      SpecFlow Version:3.9.0.0
//      SpecFlow Generator Version:3.9.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace Automation.Tests.BDD.Features.APIFramework
{
    using TechTalk.SpecFlow;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.9.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public partial class APIFramework_ExamplesFeature : object, Xunit.IClassFixture<APIFramework_ExamplesFeature.FixtureData>, System.IDisposable
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private static string[] featureTags = ((string[])(null));
        
        private Xunit.Abstractions.ITestOutputHelper _testOutputHelper;
        
#line 1 "APIFramework_Examples.feature"
#line hidden
        
        public APIFramework_ExamplesFeature(APIFramework_ExamplesFeature.FixtureData fixtureData, Automation_XUnitAssemblyFixture assemblyFixture, Xunit.Abstractions.ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
            this.TestInitialize();
        }
        
        public static void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Tests/BDD/Features/APIFramework", "APIFramework_Examples", "Example tests against the public available API endpoints", ProgrammingLanguage.CSharp, featureTags);
            testRunner.OnFeatureStart(featureInfo);
        }
        
        public static void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        public void TestInitialize()
        {
        }
        
        public void TestTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public void ScenarioInitialize(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<Xunit.Abstractions.ITestOutputHelper>(_testOutputHelper);
        }
        
        public void ScenarioStart()
        {
            testRunner.OnScenarioStart();
        }
        
        public void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        void System.IDisposable.Dispose()
        {
            this.TestTearDown();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="API_Framework_VerifyStatus200")]
        [Xunit.TraitAttribute("FeatureTitle", "APIFramework_Examples")]
        [Xunit.TraitAttribute("Description", "API_Framework_VerifyStatus200")]
        [Xunit.TraitAttribute("Category", "APIFramework_Tests_Star_Wars")]
        [Xunit.TraitAttribute("Category", "Working")]
        public void API_Framework_VerifyStatus200()
        {
            string[] tagsOfScenario = new string[] {
                    "APIFramework_Tests_Star_Wars",
                    "Working"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("API_Framework_VerifyStatus200", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 5
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 6
 testRunner.Given("api url https://swapi.dev/api", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 7
 testRunner.And("path people", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 8
 testRunner.And("path 1", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 9
 testRunner.When("method GET", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 10
 testRunner.Then("assert api response status is equal to 200", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="API_Framework_VerifyStatus404")]
        [Xunit.TraitAttribute("FeatureTitle", "APIFramework_Examples")]
        [Xunit.TraitAttribute("Description", "API_Framework_VerifyStatus404")]
        [Xunit.TraitAttribute("Category", "APIFramework_Tests_Star_Wars")]
        [Xunit.TraitAttribute("Category", "Working")]
        public void API_Framework_VerifyStatus404()
        {
            string[] tagsOfScenario = new string[] {
                    "APIFramework_Tests_Star_Wars",
                    "Working"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("API_Framework_VerifyStatus404", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 13
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 14
 testRunner.Given("api url https://swapi.dev/api", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 15
 testRunner.And("path planets", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 16
 testRunner.And("path 123", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 17
 testRunner.When("method GET", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 18
 testRunner.Then("assert api response status is equal to 404", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="API_Framework_VerifyLukeInformationWithVariables")]
        [Xunit.TraitAttribute("FeatureTitle", "APIFramework_Examples")]
        [Xunit.TraitAttribute("Description", "API_Framework_VerifyLukeInformationWithVariables")]
        [Xunit.TraitAttribute("Category", "APIFramework_Tests_Star_Wars")]
        [Xunit.TraitAttribute("Category", "Working")]
        public void API_Framework_VerifyLukeInformationWithVariables()
        {
            string[] tagsOfScenario = new string[] {
                    "APIFramework_Tests_Star_Wars",
                    "Working"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("API_Framework_VerifyLukeInformationWithVariables", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 21
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 23
 testRunner.Given("var %name% as Luke Skywalker type string", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 24
 testRunner.And("var %id% as 1 type int", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 25
 testRunner.And("var %homePlanet% as Tatooine type string", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 27
 testRunner.Given("api url https://swapi.dev/api", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 28
 testRunner.And("path people", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 29
 testRunner.And("path %id%", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 30
 testRunner.When("method GET", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 31
 testRunner.Then("assert json response name is equal to %name%", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 32
 testRunner.And("assert json response gender is equal to male", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 34
 testRunner.Given("var %planetUrl% is equal to response.homeworld type string", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 36
 testRunner.Given("api url %planetUrl%", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 37
 testRunner.When("method GET", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 38
 testRunner.Then("assert api response status is equal to 200", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 39
 testRunner.Then("assert json response name is equal to %homePlanet%", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Bearer_Token_Documentation")]
        [Xunit.TraitAttribute("FeatureTitle", "APIFramework_Examples")]
        [Xunit.TraitAttribute("Description", "Bearer_Token_Documentation")]
        [Xunit.TraitAttribute("Category", "APIFramework_BearerToken_Documentation")]
        [Xunit.TraitAttribute("Category", "NotWorking")]
        public void Bearer_Token_Documentation()
        {
            string[] tagsOfScenario = new string[] {
                    "APIFramework_BearerToken_Documentation",
                    "NotWorking"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Bearer_Token_Documentation", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 43
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 44
 testRunner.Given("api url https://api.twitter.com", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 45
 testRunner.And("header Authorization = Basic ABCDEFGHI", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 46
 testRunner.And("header Content-Type = application/x-www-form-urlencoded", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 47
 testRunner.And("form content grant_type value client_credentials", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 49
 testRunner.When("method POST", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 50
 testRunner.Then("assert api response status is equal to 200", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 53
 testRunner.Given("var %bearer% as response.access_token type string", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 55
 testRunner.Given("api url https://api.twitter.com", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 56
 testRunner.And("header Authorization = Bearer %bearer%", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 57
 testRunner.When("method GET", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 58
 testRunner.Then("assert api response status is equal to 200", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Swagger_Petshop_CRUD")]
        [Xunit.TraitAttribute("FeatureTitle", "APIFramework_Examples")]
        [Xunit.TraitAttribute("Description", "Swagger_Petshop_CRUD")]
        [Xunit.TraitAttribute("Category", "APIFramework_Tests_Swagger_PetShop")]
        [Xunit.TraitAttribute("Category", "Working")]
        public void Swagger_Petshop_CRUD()
        {
            string[] tagsOfScenario = new string[] {
                    "APIFramework_Tests_Swagger_PetShop",
                    "Working"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Swagger_Petshop_CRUD", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 61
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 65
 testRunner.Given("api url https://petstore.swagger.io/v2/pet", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 66
 testRunner.And("path 1061", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 67
 testRunner.And("header Content-Type = application/json", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 68
 testRunner.When("method DELETE", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 70
 testRunner.Given("api url https://petstore.swagger.io/v2/pet", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 71
 testRunner.And("request json { \'id\': 1061, \'category\': {\'id\': 0,  \'name\': \'string\' }, \'name\': \'te" +
                        "st pet\', \'photoUrls\': [ \'string\'], \'tags\': [   {    \'id\': 0,    \'name\': \'string\'" +
                        "   } ], \'status\': \'available\'}", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 72
 testRunner.And("header Content-Type = application/json", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 73
 testRunner.When("method POST", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 74
 testRunner.Then("assert json response id is equal to 1061", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 75
 testRunner.Given("var %id% is equal to response.id type string", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 76
 testRunner.Then("print var %id% debug", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 78
 testRunner.Given("api url https://petstore.swagger.io/v2/pet", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 79
 testRunner.And("path %id%", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 80
 testRunner.And("header Content-Type = application/json", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 81
 testRunner.When("method GET", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 82
 testRunner.Then("assert api response status is equal to 200", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 84
 testRunner.And("assert json response id is equal to %id%", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 86
 testRunner.Given("api url https://petstore.swagger.io/v2/pet", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 88
 testRunner.And("request read json file .\\\\Data\\\\JsonData\\\\ApiData\\\\Swagger\\\\JsonRequest\\\\PetShopT" +
                        "estData.json", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 89
 testRunner.And("header Content-Type = application/json", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 90
 testRunner.When("method PUT", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 91
 testRunner.Then("assert api response status is equal to 200", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 92
 testRunner.And("assert json response tags[0].id is equal to 1061", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 93
 testRunner.And("assert json response name is equal to doggie update", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 95
 testRunner.And("assert json response matches json file .\\\\Data\\\\JsonData\\\\ApiData\\\\Swagger\\\\JsonR" +
                        "esponse\\\\SwaggerPetstore_POST_pet_response.json", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 97
 testRunner.And("assert json response matches schema file .\\\\Data\\\\JsonData\\\\ApiData\\\\Swagger\\\\Jso" +
                        "nSchemas\\\\SwaggerPetstore_POST_pet_schema.json", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 99
 testRunner.Given("api url https://petstore.swagger.io/v2/pet", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 100
 testRunner.And("path 1061", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 101
 testRunner.And("header Content-Type = application/json", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 102
 testRunner.When("method DELETE", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 103
 testRunner.Then("assert api response status is equal to 200", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Swagger_Petshop_CRUD_Failing_Contract_Example")]
        [Xunit.TraitAttribute("FeatureTitle", "APIFramework_Examples")]
        [Xunit.TraitAttribute("Description", "Swagger_Petshop_CRUD_Failing_Contract_Example")]
        [Xunit.TraitAttribute("Category", "APIFramework_Tests_Swagger_PetShop")]
        [Xunit.TraitAttribute("Category", "Working")]
        public void Swagger_Petshop_CRUD_Failing_Contract_Example()
        {
            string[] tagsOfScenario = new string[] {
                    "APIFramework_Tests_Swagger_PetShop",
                    "Working"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Swagger_Petshop_CRUD_Failing_Contract_Example", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 106
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 110
 testRunner.Given("api url https://petstore.swagger.io/v2/pet", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 111
 testRunner.And("path 1061", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 112
 testRunner.And("header Content-Type = application/json", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 113
 testRunner.When("method DELETE", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 115
 testRunner.Given("api url https://petstore.swagger.io/v2/pet", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 116
 testRunner.And("request json { \'id\': 1061, \'category\': {\'id\': 0,  \'name\': \'string\' }, \'name\': \'te" +
                        "st pet\', \'photoUrls\': [ \'string\'], \'tags\': [   {    \'id\': 0,    \'name\': \'string\'" +
                        "   } ], \'status\': \'available\'}", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 117
 testRunner.And("header Content-Type = application/json", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 118
 testRunner.When("method POST", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 119
 testRunner.Then("assert json response id is equal to 1061", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 120
 testRunner.Given("var %id% is equal to response.id type string", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 121
 testRunner.Then("print var %id% debug", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 123
 testRunner.Given("api url https://petstore.swagger.io/v2/pet", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 124
 testRunner.And("path %id%", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 125
 testRunner.And("header Content-Type = application/json", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 126
 testRunner.When("method GET", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 127
 testRunner.Then("assert api response status is equal to 200", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 129
 testRunner.And("assert json response id is equal to %id%", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 131
 testRunner.Given("api url https://petstore.swagger.io/v2/pet", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 133
 testRunner.And("request read json file .\\\\Data\\\\JsonData\\\\ApiData\\\\Swagger\\\\JsonRequest\\\\PetShopT" +
                        "estData.json", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 134
 testRunner.And("header Content-Type = application/json", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 135
 testRunner.When("method PUT", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 136
 testRunner.Then("assert api response status is equal to 200", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 137
 testRunner.And("assert json response tags[0].id is equal to 1061", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 138
 testRunner.And("assert json response name is equal to doggie update", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 140
 testRunner.And("assert json response matches json file .\\\\Data\\\\JsonData\\\\ApiData\\\\Swagger\\\\JsonR" +
                        "esponse\\\\SwaggerPetstore_POST_pet_response.json", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 142
 testRunner.And("assert json response matches schema file .\\\\Data\\\\JsonData\\\\ApiData\\\\Swagger\\\\Jso" +
                        "nSchemas\\\\SwaggerPetstore_POST_pet_schema_fail.json", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Date_Examples")]
        [Xunit.TraitAttribute("FeatureTitle", "APIFramework_Examples")]
        [Xunit.TraitAttribute("Description", "Date_Examples")]
        [Xunit.TraitAttribute("Category", "APIFramework_Tests_Swagger_PetShop")]
        [Xunit.TraitAttribute("Category", "NotWorking")]
        public void Date_Examples()
        {
            string[] tagsOfScenario = new string[] {
                    "APIFramework_Tests_Swagger_PetShop",
                    "NotWorking"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Date_Examples", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 147
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 148
 testRunner.Given("var %timestamp% as now type date", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 151
 testRunner.And("format var %timestamp% as string MM-dd-yyyy", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 152
 testRunner.And("var %id% as 3fa85f64-5717-4562-b3fc-2c963f66afa6 type string", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 153
 testRunner.And(@"request json { ""Id"": ""%id%"", ""eventType"": ""string"", ""timestamp"": ""%timestamp%"", ""event"": ""string"", ""eventData"": [ { ""name"": ""string"", ""value"": ""string""} ], ""location"": { ""Id"": ""%id%"", ""timestamp"": ""%timestamp%"", ""latitude"": 0, ""longitude"": 0, ""accuracy"": 0, ""altitude"": 0, ""altitudeAccuracy"": 0, ""direction"": 0, ""speed"": 0, ""satellite"": 0, ""csq"": 0, ""address"": ""string"", ""fix"": 0, ""locationMode"": ""string"" } }", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 154
 testRunner.Then("print var %timestamp% debug", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 155
 testRunner.And("print var %id% debug", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="SQL_Request_Example")]
        [Xunit.TraitAttribute("FeatureTitle", "APIFramework_Examples")]
        [Xunit.TraitAttribute("Description", "SQL_Request_Example")]
        [Xunit.TraitAttribute("Category", "APIFramework_Tests_SQLServer_Example")]
        [Xunit.TraitAttribute("Category", "NotWorking")]
        public void SQL_Request_Example()
        {
            string[] tagsOfScenario = new string[] {
                    "APIFramework_Tests_SQLServer_Example",
                    "NotWorking"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("SQL_Request_Example", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 159
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 160
 testRunner.Given("var %key% as 218 type int", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 161
 testRunner.And("request sqlserver select top 1 *  from table with (nolock) where key = %key%", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 162
 testRunner.And("sqlserver connection string Data Source=SQL Server Connection string", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 163
 testRunner.When("method sqlserver", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 164
 testRunner.Then("assert json response sqlresult[0].key is equal to %key%", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 165
 testRunner.Then("print response debug", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.9.0.0")]
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
        public class FixtureData : System.IDisposable
        {
            
            public FixtureData()
            {
                APIFramework_ExamplesFeature.FeatureSetup();
            }
            
            void System.IDisposable.Dispose()
            {
                APIFramework_ExamplesFeature.FeatureTearDown();
            }
        }
    }
}
#pragma warning restore
#endregion
