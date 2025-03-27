namespace DeltaCare.Common
{
    public static class SPConstant
    {
        #region Utilites
        // Stored procedure to get the max value from the table
        public static string Sp_GetMaxValue = "GetMaxValueFromTable";
        #endregion

        #region MasterDataController

        // Stored procedures related to configuration
        public static string Sp_Configuration = "usp_ManageSytemConfig";

        // Stored procedures related to managing specifications
        public static string Sp_Specimentype = "usp_ManageSpecimenTypes";

        // Stored procedures related to managing divisions
        public static string Sp_Division = "usp_ManageDivision";

        // Stored procedures related to managing sections
        public static string Sp_Section = "usp_ManageSection";

        // Stored procedures related to managing work centers
        public static string Sp_WorkCenter = "usp_ManageWorkCenter";

        // Stored procedures related to managing test sites
        public static string Sp_TestSite = "usp_ManageTestSites";

        // Stored procedures related to managing account prefixes
        public static string Sp_AccnPrefix = "usp_ManageAccnPrefix";

        // Stored procedures related to managing report templates
        public static string Sp_ResultsTemplates = "usp_ManageResultsTemplates";

        //stored procedure for managing dcotors.
        public static string Sp_ManageDoctors = "usp_ManageDoctorFile";

        //stored procedure for manage location file.
        public static string Sp_ManageLocationsFile = "usp_ManageLocationsFile";

        //stored procedure for Users Job Type..
        public static string Sp_ManageUserJobType = "usp_ManageUserJobType";

        //stored procedure for Tracking User Login History..
        public static string Sp_UserLoginHistory = "usp_UserLoginHistory";

        //stored procedure for Tracking User Login History..
        public static string Sp_PageTracking = "usp_ManageTrackPageRecord";

        // Stored procedures related to managing report main header
        public static string Sp_ReportMainHeader = "usp_ManageReportMainHeader";

        // Stored procedures related to managing report sub header
        public static string Sp_ReportSubHeader = "usp_ManageReportSubHeader";

        // Stored procedures related to managing lab result type
        public static string Sp_ResultType = "usp_ManageResultType";

        // Stored procedures related to managing account manager
        public static string Sp_AccountManager = "usp_ManageAccountManager";

        // Stored procedures related to managing driver
        public static string Sp_Driver = "usp_ManageDrivers";

        // Stored procedures related to managing site
        public static string Sp_Site = "usp_ManageSites";

        // Stored procedures related to managing site
        public static string Sp_SiteTests = "usp_ManageSiteTestsAssignment";

        // Stored procedures related to managing phlebotomy
        public static string Sp_Phlebotomy = "usp_ManagePhlebotomy";

        // Stored procedures related to managing specimen sites
        public static string Sp_SpecimenSites = "usp_ManageSepcimenSites";

        // Stored procedures related to managing clients
        public static string Sp_Clients = "usp_ManageClients";

        // Stored procedures related to managing special prices
        public static string Sp_SpecialPrices = "usp_ManageSpecialPrices";

        // Stored procedures related to managing clients
        public static string Sp_GetAllTests = "usp_ManageTests";

        // Stored procedures related to managing copyfromclient
        public static string Sp_CopyFromSourceToDestintation = "usp_ManageSpecialPrices_SourceAndDestinationCopy";

        // Stored procedures related to managing AR
        public static string Sp_ARTemplate = "usp_ManageAR";

        // Stored procedures related to General Result AR Result
        public static string SP_AlphaResponses = "usp_AlphaResponses";

        // Stored procedures related to General Result AV Result
        public static string SP_AlphaValues = "usp_AlphaValues";

        // Stored procedures related to General Result IV Result
        public static string SP_AInterpretiveValues = "usp_InterpretiveValues";

        // Stored procedures related to managing CNLCD
        public static string Sp_CNLCDTemplate = "usp_ManageCNLCD";

        // Stored procedures related to managing MN
        public static string Sp_MNTemplate = "usp_ManageMN";

        // Stored procedures related to managing MNI
        public static string Sp_MNITemplate = "usp_ManageMNI";

        // Stored procedures related to managing CopyfromClient
        public static string Sp_ManageCopyFromClient = "usp_ManageCopyFromClient";

        // Stored procedures related to managing CopyfromClient
        public static string Sp_Company = "usp_ManageCompany";
        // Stored procedures related to managing EVSampleTest
        public static string Sp_EVSampleTest = "usp_ManageEVSampleTest";


        #endregion

        #region User and User Sites
        //stored procedure for Users..
        public static string Sp_ManageUsers = "usp_ManageUsers";
        public static string Sp_ValidatePassword = "usp_ChangeUserPassword";
        //stored procedure for managing user accessible sites
        public static string Sp_ManageUserSiteAccess = "usp_ManageUserAccessibleSites";
        //stored procedure for managing user access
        public static string Sp_ManageUserAccess = "usp_ManageUserAcess";
        #endregion

        #region Anatomic Pathology

        // Stored procedures related to managing Anatomic Pathology
        public static string Sp_ManageAnatomicPathology = "usp_ManageAnatomicPathology";

        // Stored procedures related to managing results template for Anatomic Pathology
        public static string Sp_ManageRTForAP = "usp_ManageRTForAnatomyPathology";

        // Stored procedures related to managing AP Report
        public static string Sp_ManageAPReportStatus = "usp_ManageAPReportStatus";

        // Stored procedures related to find TAxis
        public static string Sp_ManageClinicalFinding = "usp_ManageClinicalFinding";

        // Stored procedures related to find path
        public static string Sp_ManagePathFinding = "usp_ManagePathFinding";

        // Stored procedures related to managing anatomic Pathology receiving
        public static string Sp_ManageAPReceiving = "usp_ManageAPReceiving";

        #endregion

        #region Microbiology

        // Stored procedures related to managing MicroBiology
        public static string Sp_ManageMicroBiology = "usp_ManageMicroBiology";

        // Stored procedures related to managing results template for Micro Biology
        public static string Sp_ManageRTForMB = "usp_ManageRTForMicroBiology";

        // Stored procedures related to managing MB Report
        public static string Sp_ManageMBReportStatus = "usp_ManageMBReportStatus";

        // Stored procedures related to managing MB Organism ISOL
        public static string Sp_ManageMicroBiologyIsol = "usp_ManageMicroBiologyIsol";

        // Stored procedures related to managing MB Sensitivity
        public static string Sp_ManageMicroBiologySensitivity = "usp_ManageMicroBiologySensitivity";

        // To bulk insert the Microbiology ISOL
        public static string Sp_InsertMicroBiologyIsol = "usp_InsertMbIsol";

        // Type of Database Table MbIsol
        public static string type_InsertMBIsol = "dbo.usp_Type_Mb_Isol";

        // To bulk insert the Microbiology Sensitivity
        public static string Sp_InsertMicroBiologySensitivity = "usp_InsertMbSensitivity";

        // Type of Database Table MbSensitivity
        public static string type_InsertMBSensitivity = "dbo.usp_Type_Mb_Sensitivity";

        #endregion

        #region Cytogenetics

        /// Manage Cytogenetic
        public static string Sp_ManageCytogenetics = "usp_ManageCytogenetics";

        /// Manage Cytogenetic TxtRes
        public static string usp_ManageCytogeneticsTxtRes = "usp_ManageCytogeneticsTxtRes";

        /// Bulk insert Txt_Res
        public static string Sp_InsertCytogeneticTxtRes = "usp_InsertTxtRes";

        // Type of Database table Txt_Res
        public static string type_InsertCGTxtRes = "dbo.usp_Type_Txt_Res";

        public static string Sp_ManageCytogeneticsReceiving = "usp_ManageCytogeneticsReceiving";

        public static string Sp_ManageCytogeneticsList = "usp_ManageCytogeneticsList";

        public static string sp_ClinicalImageEntry = "usp_ManageCinicalImageEntry";

        #endregion

        #region Order Entry

        // Stored procedures related to Accession Number
        public static string SP_GetRefSitefromSiteDetails = "usp_GetRefSitefromSiteDetails";

        public static string SP_ManageAccnPrefix = "usp_ManageAccnPrefix";

        public static string SP_GenerateAccnPrefix = "usp_GenerateAccnPrefix";

        // Stored procedures related to Test Directory Details that used by Order Entry
        public static string SP_ManageTestDirectory = "usp_ManageTestDirectory";

        // Stored procedures related to Test Directory Details that used by Order Entry for pricing
        public static string SP_GetTestsByDivision = "usp_GetTestsByDivision";

        //Stored procedures for dropdowns in the test directoy --
        public static string SP_CombosTestDirectory = "Usp_GetTestDefinition_Combos";

        // Stored procedures related to Pateint Registratio Details that used by Order Entry
        public static string SP_ManagePatientRegistration = "usp_ManagePatientRegistration";

        // Stored procedures related to Group Tests Detailed union woth Test Directory
        public static string SP_GroupTestsDetailedandTestDirectory = "usp_GetGroupTestsDetailedandTestDirectory";

        // Stored procedures related to Tests Directory Join with Group Test
        public static string SP_TestDirectoryandGrouptTest = "usp_GetTestDirectoryandGrouptTest";

        // Stored procedures related to New Order Number by getting the last ORDER created in Database
        public static string SP_GetLastOrdersTransactions = "usp_GetLastOrdersTransactions";

        // Stored procedures related to Active Tests Request
        public static string SP_ManageOrdersTransactions = "usp_ManageOrdersTransactions";

        // Stored procedures related to Order Transactions Join Doctor File, Client File and Locations File
        public static string SP_GetOrderTransactionsByJoin = "usp_GetOrderTransactionsByJoin";

        // Stored procedures related to Order Transactions union to Test Directory and Group Directory
        public static string SP_GetOrdersDetailsByUnion = "usp_GetOrdersDetailsByUnion";

        // Stored procedures related to Order Type
        public static string SP_ManageOrderType = "usp_ManageOrderType";

        // Stored procedures related to Active Tests Request
        public static string SP_ManageActiveTestsRequest = "usp_ManageActiveTestsRequest";

        // Stored procedures related to Order Details
        public static string SP_ManageOrdersDetails = "usp_ManageOrdersDetails";

        // Stored procedures related to Active Results File
        public static string SP_ManageActiveResultsFile = "usp_ManageActiveResultsFile";

        // Stored procedures related to barcode generation
        public static string SP_GenerateBarcode = "usp_GenerateBarcode";

        // Stored procedures related to Group Test Details
        public static string SP_ManageGroupTestsDetailed = "usp_ManageGroupTestsDetailed";

        //
        public static string SP_ManageCytogeneticsCases = "usp_ManageCytogeneticsCases";

        public static string SP_ManageCytogeneticsQualityControl = "usp_ManageCytogeneticsQualityControl";

        public static string SP_ManageAnatomicPathologyCases = "usp_ManageAnatomicPathologyCases";

        //For Mutiple Search USP
        public static string SP_MultipleSearch = "usp_MultipleSearch";
        public static string SP_MultipleSearchOrders = "usp_MultipleSearchOrders";

        //Store procedure for Orders Tracking
        public static string SP_ManageOrdersTracking = "usp_ManageOrdersTracking";
        public static string SP_GetSharedTable = "usp_GetSharedTable";
        #endregion

        #region Test Directory

        public static string SP_ManageTD = "usp_ManageTD";

        public static string SP_ManageRefRng = "usp_ManageREF_RNG";

        public static string SP_InsertTDReferenceRange = "usp_InsertTDReferenceRange";

        public static string type_Td_ReferenceRange = "dbo.usp_Type_Td_ReferenceRange";

        #endregion

        #region Test Directory Module  

        public static string SP_ManageAPTestDefinition = "dbo.usp_ManageApTestDefinition";
        public static string SP_ManageEVTestDefinition = "dbo.usp_ManageEVTestDefinition";
        public static string SP_ManageEVRefereceRange = "dbo.usp_ManageEVReferenceRange";
        public static string SP_ManageEVTestDefinitionProfile = "dbo.usp_ManageEVTestDefinitionProfile";
        public static string SP_ManageCGTestDefinition = "dbo.usp_ManageCGTestDefinition";
        public static string SP_ManageCGTestDefinitionProfile = "dbo.usp_ManageCGTestDefinitionProfile";
        public static string SP_MANAGE_CG_TEST_DEFINITION_PROFILE = "usp_ManageCG_TestDefinitionProfile";
        public static string SP_ManagePermission = "usp_ManagePermissions";
        public static string SP_ManageANLMethod = "dbo.usp_ManageANLMethod";
        public static string SP_ManageEVSubHeader = "dbo.usp_ManageEVSubHeader";


        #endregion

        #region Barcode

        public static int codeWidth = 4;

        #endregion

        #region Billing

        public static string SP_ManageBilling = "usp_ManageBilling";

        #endregion

        #region Test Directory Profile

        public static string SP_ManageTestDirectoryProfile = "usp_ManageTestDirectoryProfile";

        // Type of Database Table TestDirectory Profile
        public static string type_InsertTDProfile = "dbo.usp_Type_Td_Profile";

        // To bulk insert the TestDirectory Profile
        public static string Sp_InsertTestDirectoryProfile = "usp_InsertTdProfile";

        #endregion

        #region Environmental Orders

        public static string SP_ManageEnvironmentalOrders = "usp_ManageEVOrders";
        public static string SP_ManageEVOrdersList = "usp_ManageEVOrderList";
        public static string SP_ManageEVOrdersDetails = "usp_ManageEVOrderDetails";

        public static string SP_ManageEnvironmentalOrdersATR = "usp_ManageEnvironmentalOrderATR";

        public static string SP_ManageEnvironmentalSample = "usp_ManageEVSample";

        /// Bulk insert ATR
        public static string Sp_InsertEnvironmentalOrderATR = "usp_InsertEVATR";

        /// Type of Database table ATR
        public static string type_InsertEnvironmentalOrderATR = "dbo.usp_Type_EVATR";

        public static string SP_ManageAPImages = "usp_ManageAP_IMAGES";

        #endregion

        #region Environmental Result

        public static string SP_ManageEnvironmentalResult = "usp_ManageEVResult";

        public static string SP_UpdateEnvironmentalResult = "usp_UpdateEVResult";

        public static string SP_UpdateEVResultInstrument = "usp_UpdateEVResultInstrument";

        // Type of Database Table EV Result
        public static string type_UpdateEVResult = "dbo.usp_Type_Ev_Result";

        public static string SP_UpdateEnvironmentResultStatus = "usp_ManageEVResultStatus";

        #endregion


        #region General Result
        // Stored procedures related to Result Modified
        public static string SP_ManageResultsModified = "usp_ManageResultsModified";
        #endregion

        #region Manage Client Account
        public static string SP_ClientAccountDataEntry = "usp_ManageClientAccount";
        public static string SP_ClientAccountDetails = "usp_GetClientAccountDetails";
        public static string Sp_ClientAccountStatement = "usp_GetAccountStatement";
        // Stored procedures related to managingclient account entry
        public static string Sp_ClientAccountEntry = "usp_ManageClientAccountEntry";
        public static string Sp_ClientImageEntry = "usp_ManageClientImageEntry";
        public static string Sp_ManageCrossCheck = "usp_ManageClientAccountCrossCheck";
        public static string Sp_ManageCurrentStatus = "usp_ManageClientAccountCurrentStatus";



        #endregion

        #region CG Orders

        public static string SP_ManageCytogeneticOrders = "usp_ManageCGOrders";

        public static string SP_ManageCytogeneticOrdersATR = "usp_ManageCytogenicOrderATR";

        public static string SP_ManageCytogeneticSample = "usp_ManageCGSample";

        /// Bulk insert ATR
        public static string Sp_InsertCytogeneticOrderATR = "usp_InsertCGATR";

        /// Type of Database table ATR
        public static string type_InsertCytogeneticOrderATR = "dbo.usp_Type_CGATR";

        #endregion

        #region permission
        public static string SP_GetAllRolePermission = "usp_GetAllRolePermission";
        public static string SP_ManageOrderRolesPermission = "usp_ManageOrderRolesPermission";
        public static string SP_ManageResultPermission = "usp_ManageResultPermission";
        public static string SP_ManageManinAccessPermission = "usp_ManageManinAccessPermission";

        #endregion


    }
    public static class TableParamConstant
    {
        public const string Param_CGPROFILEDATA = "@CGProfileData";

        public const string Param_Permission = "@Permissions_Table";

    }   
}
