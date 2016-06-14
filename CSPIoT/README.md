# Deploying the Azure IoT Infrastructure

The first step in deploying a Cloud Services solution for Device Monitoring or Device Analytics, is to create the necessary infrastructure. This guide will aid the reader in deploying the solution using Azure Resource Manager (ARM) templates in a repeatable process that makes it easy to rapidly configure new IoT solutions.

## Technical Guide

This technical guide will show you how to deploy the entire Azure IoT CSP Solution to the CSP Azure Portal.  When you've completed this guide, you will have provisioned and configured the following resources:

+	A Cloud Service for IoT solution deployed to a single Resource Group in a single Azure region
+	A locally redundant Azure Storage Account, supporting data processing containers
+	An Azure IoT Hub
+	Azure Stream Analytics (ASA) Jobs supporting data routing and analytics logic from IoT Hub
+	Multiple ASA output sinks for Azure Storage and PowerBI.com


Before starting this guide, you will need to install a couple of tools on your local PC, this guide assumes you are running an RTM version of Windows 10.  

### Prerequisites

+  PC with an RTM version of Windows 10
+  A provisioned Azure Subscription: This guide is designed for Cloud Service Providers (CSP), but any relevant Azure Subscription will suffice.
     + Provisioned CSP account and customer subscription in portal
     + Administrative access to a CSP Subscription (this assumes you already have a co-admin login for a CSP subscription)
+  Visual Studio 2015 Community Edition or greater (Visual Studio Professional is the version used for this document) 
+  [Azure SDK for Visual Studio 2015] (March 2016 is the version used for this document)
+  [AZCopy.exe] is a cmd line tool for moving data in and out of Azure Storage (you do not need to download it locally to deploy this solution, but it is a part of the solution)

### Version
0.0.1

### Deployment from Visual Studio

Once you have cloned or pulled the solution into your local Visual Studio IDE, you can almost immediately deploy it to an Azure CSP Subscription. To deploy the solution, you will open the CSPIoT.sln file. This will open the Azure Resource Manager Visual Studio project.

![Visual Studio Project Solution Explorer](https://znnfxg-ch3302.files.1drv.com/y3mad3srz2YjPgxZGXuCmMDV6AxWqe6DZDh5cRqio_4TM_3x-5CYavrVbv0Rs443O9ImdhPVbgdHZ2CB9RdK7JvENrsovF03hkxmEg7IDvOklixS4eLhTbVXB8B6lHEduMpWvjqnCqIlSE6xEzCP_wogOPX0cTjAaUtUvBYhJUkYsE?width=256&height=227&cropmode=none)


From Solution Explorer (pictured above), right-click the upper most project level node of the solution and select  **Deploy** and then **New Deployment**.  The **Deploy** menu is only available at the project level node in the Solution Explorer.  The text for the node you should click on will read: **CSPIoT** and will be located just below the most top level solution node, the top level reads:  **Solution 'CSPIoT' (1 Project)**.

![Visual Studio Solution Right-Click Deploy Menu](https://znnhxg-ch3302.files.1drv.com/y3msATpjVVmw19FwGyfbZNoPBterJlZX7XijvPR6KclZUhhDkUabnjAzSG76Ap9w0WTR2hD_zwUFYO4JpdahlgIT_9miQjkULIdkiAq3BinU-7hY650Mmc-6eaV18_8QqkhRD3vHhgCNUwt1R9Q8FX-U6OHa1dKdr47f7yb7QH9KSs?width=256&height=253&cropmode=none)

Clicking the **Deploy** menu item, should open the **Deploy to Resource Group** dialogue. 

![Azure Modal Subscription](https://znncxg-ch3302.files.1drv.com/y3m99jLRdxXxMwS_1Deg1kHs2_z_-bJyMhpVd1Sgyg8E4K2r4gvYygLY8CEe7imKuOZjJTOKQjnjDcf9GSLnbmOtULRMtUmb4ZMl0RB42YCFMlaauWM4ThQZ84qSDysYVprM2hvTssx-M5VclwxtmLq16X6py7Br1sq9_7q9WI8eJg?width=256&height=235&cropmode=none)


To ensure you are deploying to the correct account and subscription, in the **Deploy to Resource Group** dialogue, select the account from the drop down menu. 

![Azure Modal Subscription - Add Account](https://znnbxg-ch3302.files.1drv.com/y3mF8qIwbfPHr26B_3YX7Ffox1S7L9lTCTsYO3K_0UrJFbPSkXx82Zv_Zrif_DTe1jDettC_90YptlMEGZDAyIQGqTFl4jwnORolm1BLHZAmTGFyIXrPXMF4nGJuqZUvBW6syO0jkRgZi49oOX-WGdfBWKY7t7a6Q9KAc4P3QULQLw?width=256&height=236&cropmode=none)

If the desired account is not listed, click the **Add an account…** menu option. When prompted, login with the appropriate Azure CSP Account. 
Once the account is authenticated, you will need to fill out the deployment parameters.
**Subscription Parameter:** The subscription that should host the deployed solution when completed.
**Data Center Region:** The Azure Data Center Region that should host the deployed solution.
**Resource Group Name:** The name that will be used to reference the collection of Azure services once deployed. Create a new Resource Group if prompted by this dialogue. 
**Deployment Template & Template Parameter file:** These are the definitions of the services that will be deployed. It is very important to select the correct files. The following sections will outline options for each of these files.

###Deployment template options
> **Device Monitoring:** If the desired solution is intended to monitor devices only, with no long term storage or deep analytics.
> -----------------------
> Deployment template: cspiot.deploy.json
> Template parameters file: cspiot.parameters.json
> -----------------------
>
> **Device Analytics:** If deeper analytic capabilities are required, the HDInsights or DocDB options are suggested.
> **Option 1: HDInsight**
> -----------------------
> Deployment template: HDInsight.deploy.json
> Template parameters file: HDInsight.parameters.json 
> **Important Note:** This template requires a user name and password for both cluster management and SSH management. The template contains a default user name and password that should be modified prior to template execution.
> -----------------------
>
> **Option 2: DocDB**
> -----------------------
> Deployment template: cspIoTDocDB.deploy.json
> Template parameters file: cspiot.parameters.json
> -----------------------
>


###Deployment Execution

Once all fields are set, click the **Deploy** button at the bottom of the **Deploy to Resource Group** dialogue. The following is an example of the dialogue ready to be deployed:

![Azure Modal Subscription - Ready to Deploy](https://1xnkxg-ch3302.files.1drv.com/y3m_bTfIjM2GpSP_wotFKqLCEFs_k-yNC7NCzsuxjlApMo8CJe_RosMm-_0NxVKbVmBMmlUC8kC7G5z4GvPOuQGWiTXfFq7tTzdFA_OkILd5RZRVGlQP5KkAhflMyFqyP3YpbAIdUVKojnhlJd3wEeId1K4nAHlSxexu6iYzUJjKvQ?width=256&height=223&cropmode=none)

Click **Deploy**.  

**EXTREMELY IMPORTANT FOLLOW ON NOTE:** The deployment will output three key variables in the output window: iothubHostName, iotHubSharedAccessKeyName, iotHubSharedAccessKey These three field will be required to connect devices to the IoT Hub in later guides.

### Validating Deployment and Next Steps

Once the template has completed the provisioning process from Visual Studio, you can now login to the Azure Portal with the deployment credential and view the assets and services you deployed in the template.  Since everything in this solution is deployed to a single Azure Resource Group, you can easily validate the deployment by viewing the Resource Group contents.  Browse to Resource Groups by clicking the tab menu:

![Azure Resource Group Tab](https://03nfxg-ch3302.files.1drv.com/y3mn2wgs2u8SWb2Nk0qZH5lU2OQVDA8oPFUpdhw0MT9Z6r41JTAA4S3dfzDnLnIAFOmA_hyQ-WmXhsxSEuwRNik6wDE__Dp1sTMPpCHzplO8emrvOwPIeUq6saWcqCjP93YD-Pjug0JqIlQARaoTOA8cLP24-B2yM6DEGIwlYg8v4k?width=660&height=209&cropmode=none)

The name of the Resource Group fo this solution is **CSPIoT**, once you click on the Resource Group menu, in the blade that opens up, click on the Resource Group.  The portal blades should open up to the following screen:

![Azure Resource Group Details - CSPIoT](https://03nixg-ch3302.files.1drv.com/y3miLHUa3b4QFuThQP8FlZhZBI8efHs7CqTXuW5NJM1MwRVjtohK5qII3pFDWXmwDs1DX9UJbqChULEZ4d-STMtWtZH0QtsIKEQi7_qq4Qac5iro6OORpPZxpJ2SNAi62fl5OG3D8vUtiFo1WPiLUMhbrbehTfxfQFKvXRHRn-SfF8?width=660&height=367&cropmode=none)

Once you're here, you should validate that the following assets are deployed successfully.  This would be the following components:
+	Azure IoT Hub - cspiothub%
+	Azure Storage Account v2 - cspstorage%
+	Azure Stream Analytics Job - cspiotstream%
Some of the services mentioned above require globally and locally unique names or failure will take place.  Because of that, a suffix has been added to each service/module name to all of the assets to guarantee the required level of uniqueness across the cloud.  

Finally, we want to check the **Status** of each service to ensure that it was provisioned correctly.   Do this by clicking on each service.  For instance, clicking on the IoT Hub yields the following blades:

![Azure Resource Group - CSPIoT - IoT Hub Active Status](https://0nnmxg-ch3302.files.1drv.com/y3mE0tNvU_mQFivOow5fSIr3JKTdsl_d4DJHDgf8B_Pll4-HBZuH8Rz4Nm5dOz46VzoTiPlw8JkBcS2cPmRRIbTfjoUnQ5xXb_TVHOpHQwgaRgzjZ4gKLc7nU25ikPpGd9-mW3CVWBFAlXpBos9gm6l4F8dWoBZW0l1g5vxQN07J9U?width=660&height=370&cropmode=none)

Each of the other 2 services will have a similar view when you click on them from the Resource Group.  You are validating that the **Status** or **Deployment Status** is not in **Error** status.











[//]: # (These are reference links used in the body of this note and get stripped out when the markdown processor does its job. There is no need to format nicely because it shouldn't be seen. Thanks SO - http://stackoverflow.com/questions/4823468/store-comments-in-markdown-syntax)
   [Azure SDK for Visual Studio 2015]: <https://go.microsoft.com/fwlink/?linkid=746481&clcid=0x409>
   [AZCopy.exe]: <http://aka.ms/downloadazcopy>