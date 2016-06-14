# Building & Deploying with Power BI Desktop
Power BI serves as the visual and interface for Cloud Service Providers (CSPs) to view device information and analytics. Power BI enables near real time notice of device failure, changes in device performance, or notification of threshold violations. This technical guide will aid in creating Power BI dashboards that connect to Cloud Services for IoT.

## Technical Guide

This technical guide will show you how to connect to Azure Storage, create a Power BI Desktop dashboard and supporting reports, and how to deploy the report to PowerBI.com.  When you've completed this guide you will have created the following resources:

+	A Power BI Desktop Workspace File (.pbix file)
+	Supporting PowerBI.com Reports deployed from the Power BI Desktop tool
+	A PowerBI.com Dashboard comprised of reports and visualizations built with the Desktop tool
+	A PowerBI.com report that leverages a control that is downloaded from the [Power BI Visuals Gallery]

Before starting this guide, you will need to download the PowerBI Desktop tool.  It can be found by logging into PowerBI.com and viewing the upper right hand corner Download menu.

![Power BI Downloads Menu](https://2lptww-ch3302.files.1drv.com/y3mflWl5iFe2dk4Vl3Mz57bRGucwg1jidommj8g9c283PF_IRE6HjnM_R8Pbmpi9Yv4J7tiokCYXL1NvJ18frv0FL-PRUp1BEKM0h6xTo7Wuy2zxIYZohxFHcGsA9TOYsI9nevMDmf0G8aw1VwCJeR_-BZCE1ogVKEFRDcvDO59YX8?width=256&height=43&cropmode=none)

Then select the Power BI Desktop option.

![Power BI Downloads Menu](https://2lpnww-ch3302.files.1drv.com/y3mibudd8s7baylMnqM8KBkmCAWoVkqEOi8EIp_zzOJPPzwpCEdQQllUTvFgt7i68fusSVDkJxGGS6_a5Pou0efQVu4wPQN8SgRDns12DYzCWfMDA1gAFIkjt9Lqs_lRqc1ReLvkcnLN8SQZeNOahBEMDvUcUInjH1caZSbZSfO7eg?width=256&height=180&cropmode=none)

From this menu, you should be prompted to download a .MSI file.  Once you've downloaded this, launch the .MSI file and go through the entire installation.  Ensure it installed successfully by opening up Power BI Desktop from your Start Menu or from the Cortana Personal Assistant menu in Windows 10.  I will use Cortana, but the same steps can be taken if you use the Start Menu.    

![Open the Power BI Desktop Tool](https://17psww-ch3302.files.1drv.com/y3mwOAxnWjB4gsDmoKDsSYzmq4W-nbA7uEKGTv7alQhuOrTpvwUbQVWrorEmQIQTslQQMAAeEW5j9Eb9uzp0hkKUCiFKIQNbGVCfnFEp5EUmIfXZhJIKTP2GKIRl3uppRkHD1R9jAZDVY6VC8Myelt0Af58v7_6SOMi0wglSd5gewg?width=151&height=256&cropmode=none)

Once the tool opens you should be taken to an initial splash screen.  This screen will guide you on "getting data", opening an existing Power BI Workspace, and creating a new Power BI Workspace.

![Power BI Desktop Tool Splash Screen](https://17prww-ch3302.files.1drv.com/y3m7otz0ntKhkMM4py8Bd4uTe8jAv-oaC_4sBy2lFXLSujmUi0gjlCO9Gvyj2xOEBiWaDdubhLKmIENPxY7fMoL1iffZk24EWBdLvT8Mrh2k2ffn4D55_G8_Ns9o4l6itYjrm-dyJw_HEahuQQ5L9ySDnFXUgGasCxV3N5d-9Y09mI?width=256&height=132&cropmode=none)

### Get Data

One of the core pillars of Power BI as a platform is that it makes getting data and connecting to data sources easy and simple for all types of end-users.  In the Device Monitoring CSP Solution Template, the Azure Stream Analytics job will write the stream query results to an Azure Storage Account, this is referred to as an Output Sink.

### Connecting to the Azure Stream Analytics Storage Account Output Sink

The storage account and container here will contain the output data from the stream query in the Azure Stream Analytics job.  In order to pull the data into Power BI, we will need to connect to the storage account container.  Open Power BI Desktop, select **Get Data**.  

When prompted, select **Microsoft Azure Blob Storage** as the data source type.  

![Power BI Microsoft Azure Blob Storage](https://z3pnma-ch3302.files.1drv.com/y3mTupwtMRdnwrZF_xZvJWSVnKPEzL76L4mkri-uEV9scKABG8MIAKvJCuJrmo3tR7cI54AmNEu-15TvGCB6TosFOyHfb7xydizp6xCaU432PMWkCd9aDbIucFYpn07jDu5GMrjwfw9ZiIHjCrOIoN_0ZPoZBT5yhrfwgJC2GYjUSo?width=236&height=256&cropmode=none)

This will launch the connection dialogue for Azure Blob Storage.  You will need the storage account name and the storage key (typically use key1) for the storage account.  In this first dialogue, enter the storage account name, click OK.

![Power BI Storage Account Name](https://z3pima-ch3302.files.1drv.com/y3mGLbjnq81RSH4oejBr0ytfo7iR1T2R8ZHbVh551Sg0-wOK4naxVsxGLFvm71PZLcRlxibgiuPI1Trz949mbC2Hq85QD3QLchK5FgIx3d6j77g1ov8QRv3PSVtL8OPBCQAGSzPj-TzvyU-rh4PCAsJh4WCyIs9uayouI27S0zyKTA?width=256&height=89&cropmode=none)

On this next dialogue, enter the storage key, click Connect.

![Power BI Storage Key](https://z3phma-ch3302.files.1drv.com/y3mZeA6kG0H-NcE-mmYxsAolwq7ZqV8_EqzcXWQyZ08zRGyxQdbUES4b1G5O1lwHe6AT7Uadv_jeEpVbB9ViEaou77XuEObqW_RwAcWk7bT_hTslI6RhZ8D_oBvD7W5BvNPlxc5rWQMXHpNJuwCVlu0UEwYhaKh7yYkMVMmoA8d3Kg?width=256&height=89&cropmode=none)

This will open the **Navigator** within this tool we'll select the files or container we will use for our reporting data.  

![Power BI Storage Navigator](https://znpqma-ch3302.files.1drv.com/y3mLCA2QFEgmB2or1P6tUoLLN_ZhwnpjhMVA7FYk6fcucUHyf5NlCONYB78XGGGgFKYnA_w4NFhNhiBNOJJARjBno4j_BrX4PtuHqQJrW3ANg74m3TOdz-_sxAraJTg0fON2q4KpjNHB6uLV2IXUHNjwRsQYRwn9J3hR9_bOwE41aE?width=256&height=204&cropmode=none)







[//]: # (These are reference links used in the body of this note and get stripped out when the markdown processor does its job. There is no need to format nicely because it shouldn't be seen. Thanks SO - http://stackoverflow.com/questions/4823468/store-comments-in-markdown-syntax)
   [Power BI Visuals Gallery]: <https://go.microsoft.com/fwlink/?linkid=746481&clcid=0x409>