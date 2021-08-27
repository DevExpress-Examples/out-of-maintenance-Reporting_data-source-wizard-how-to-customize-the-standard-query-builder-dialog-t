<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128597970/15.2.5%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T333785)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [FrmDesigner.cs](./CS/ReplaceQueryBuilderSample/FrmDesigner.cs) (VB: [FrmDesigner.vb](./VB/ReplaceQueryBuilderSample/FrmDesigner.vb))
* [FrmQueryBuilder.cs](./CS/ReplaceQueryBuilderSample/FrmQueryBuilder.cs) (VB: [FrmQueryBuilder.vb](./VB/ReplaceQueryBuilderSample/FrmQueryBuilder.vb))
* [MyConfigureQueryPage.cs](./CS/ReplaceQueryBuilderSample/MyConfigureQueryPage.cs) (VB: [MyConfigureQueryPage.vb](./VB/ReplaceQueryBuilderSample/MyConfigureQueryPage.vb))
* [MyConfigureQueryPageEx.cs](./CS/ReplaceQueryBuilderSample/MyConfigureQueryPageEx.cs) (VB: [MyConfigureQueryPageEx.vb](./VB/ReplaceQueryBuilderSample/MyConfigureQueryPageEx.vb))
* [MyConfigureQueryPageView.cs](./CS/ReplaceQueryBuilderSample/MyConfigureQueryPageView.cs) (VB: [MyConfigureQueryPageView.vb](./VB/ReplaceQueryBuilderSample/MyConfigureQueryPageView.vb))
* **[MyWizardCustomizationService.cs](./CS/ReplaceQueryBuilderSample/MyWizardCustomizationService.cs) (VB: [MyWizardCustomizationService.vb](./VB/ReplaceQueryBuilderSample/MyWizardCustomizationService.vb))**
<!-- default file list end -->
# Data Source Wizard – How to customize the standard Query Builder dialog


<p>This example illustrates how to customize the standard Query Builder dialog of a <a href="https://documentation.devexpress.com/#XtraReports/CustomDocument115389">Data Source Wizard</a>.</p>
<p>The following image illustrates a custom form replacing the standard <a href="https://documentation.devexpress.com/#XtraReports/CustomDocument17308">Query Builder</a>.</p>
<p><img src="https://raw.githubusercontent.com/DevExpress-Examples/data-source-wizard-how-to-customize-the-standard-query-builder-dialog-t333785/15.2.5+/media/a0f83647-baf4-11e6-80bf-00155d62480c.png"><br><br>The standard Query Builder dialog is initialized and displayed by the <a href="https://documentation.devexpress.com/#XtraReports/CustomDocument4251">Create a Query or Select a Stored Procedure</a> wizard page.</p>
<p>To substitute the Query Builder, do the following.</p>
<p>1. Override the <strong>RunQueryBuilder</strong> virtual method of this page's <a href="https://documentation.devexpress.com/#XtraReports/CustomDocument115389">presenter</a> (<a href="https://documentation.devexpress.com/#CoreLibraries/clsDevExpressDataAccessWizardPresentersMultiQueryConfigurePage%7eTModel%7etopic">MultiQueryConfigurePage<TModel></a>).</p>
<p>In this method's body, implement the logic required to display a custom Query Builder form and obtain the results of the Query Builder execution.<br><br>2. To register a custom query configuration form, implement the <strong>IWizardCustomizationService</strong> interface.</p>
<p>To invoke the custom query customization form instead of the default Query Builder, implement the <strong>ISqlEditorsCustomizationService</strong> interface.</p>
<p><br>3. Add the custom services to the Report Designer by calling the <a href="https://documentation.devexpress.com/#XtraReports/DevExpressXtraReportsUserDesignerXRDesignMdiController_AddServicetopic">XRDesignMdiController.AddService</a> method at the application startup.</p>
<p> </p>
<p><strong>See also</strong>:</p>
<p>For a general code example of how to customize the Data Source Wizard using the <strong>IWizardCustomizationService</strong> interface, see <em><a href="https://www.devexpress.com/Support/Center/p/T140683">How to customize the New Report Wizard (introduced in the 2014 vol.1 release) in the End-User Designer</a></em>.</p>

<br/>


